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


namespace Reactor.Types
{
    public class RFont
    {

        internal string FamilyName;
        internal FontFamily font;
        public RFont(string fileName)
        {
        }

        public RFont()
        {
            RLog.Info("Creating default system font.");
            font = RFontResources.SystemFont;
            Font f = new Font(font, 1.0f);
            int charHeight = f.Height;
            Rectangle bounds = new Rectangle(0,0,1,1);
            bounds.Inflate(0, charHeight);

        }

        public void BuildTextureMap(uint Size)
        {


        }
        public Bitmap RenderString(string text)
        {

        }
    }
    internal static class RFontResources
    {
        internal static PrivateFontCollection fonts = new PrivateFontCollection();
        internal static Assembly Assembly = Assembly.GetAssembly(typeof(RFontResources));
        internal static FontFamily GetResource(string resource){
            System.IO.BinaryReader reader = new System.IO.BinaryReader(Assembly.GetManifestResourceStream(resource));
            byte[] buffer = new byte[reader.BaseStream.Length];
            reader.Read(buffer, 0, buffer.Length);
            reader.Close();
            FontFamily font;
            try
            {
                fonts.AddMemoryFont((IntPtr)buffer, buffer.Length);
                font = new FontFamily("coders_crux", fonts);

                return font;
            }
            catch(Exception e)
            {
                RLog.Error(e);
                return null;
            }
        }

        internal static FontFamily SystemFont = GetResource("Reactor.Fonts.coders_crux.ttf");

    }
}

