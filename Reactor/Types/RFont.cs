//
// RFont.cs
//
// Author:
//       Gabriel Reiser <gabriel@reisergames.com>
//
// Copyright (c) 2015 2014
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
using System;
using System.Reflection;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Drawing.Text;
using System.Runtime.InteropServices;
using SharpFont;
using System.IO;


namespace Reactor.Types
{
    public class RFont
    {

        internal string FamilyName;
        internal Face font;
        public RFont(string fileName)
        {
        }

        public RFont()
        {
            RLog.Info("Creating default system font.");
            font = RFontResources.SystemFont;
            BuildTextureMap(16);

        }

        public void BuildTextureMap(int Size)
        {
            font.SetCharSize(0, Size, 0, 72);
            font.SetPixelSizes(0, (uint)Size);
            string table = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890-=!@#$%^&*()_+~`\\|]}[{'\";:/?.>,<";
            List<RTextureGlyph> glyphs = new List<RTextureGlyph>();
            foreach(char c in table)
            {

                font.LoadChar(c, (LoadFlags.Render|LoadFlags.Monochrome), LoadTarget.Normal);
                //font.Glyph.RenderGlyph(RenderMode.Normal);

                glyphs.Add(new RTextureGlyph(font.Glyph, c));
            }
        }

    }
    internal static class RFontResources
    {
        internal static Library FreetypeLibrary = new Library();
        internal static Assembly Assembly = Assembly.GetAssembly(typeof(RFontResources));
        internal static Face GetResource(string resource){
            System.IO.BinaryReader reader = new System.IO.BinaryReader(Assembly.GetManifestResourceStream(resource));
            byte[] buffer = new byte[reader.BaseStream.Length];
            reader.Read(buffer, 0, buffer.Length);
            reader.Close();

            Face face = FreetypeLibrary.NewMemoryFace(buffer, 0);
            return face;

        }

        internal static Face SystemFont = GetResource("Reactor.Fonts.coders_crux.ttf");

    }


}

