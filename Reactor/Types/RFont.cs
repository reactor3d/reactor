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
using SharpFont;
using System.Reflection;


namespace Reactor.Types
{
    public class RFont
    {

        internal string FamilyName;
        internal int FaceCount;
        internal FaceFlags FaceFlags;
        internal string StyleName;
        internal StyleFlags StyleFlags;

        public RFont(string fileName)
        {
            using (Library lib = new Library())
            {
                RLog.Info("FreeType version: " + lib.Version + "\n");

                using (Face face = lib.NewFace(RFileSystem.Instance.GetFilePath(fileName), 0))
                {

                    FamilyName = face.FamilyName;
                    FaceCount = face.FaceCount;
                    FaceFlags = face.FaceFlags;
                    StyleName = face.StyleName;
                    StyleFlags = face.StyleFlags;


                    //face.SetCharSize(0, 32, 0, 96);

                    //Console.WriteLine("\nWriting string \"Hello World!\":");
                    //Bitmap bmp = RenderString(face, "Hello World!");
                    //bmp.Save("helloworld.png", ImageFormat.Png);
                    //bmp.Dispose();

                    //Console.WriteLine("Done!\n");
                }
            }
        }

        public RFont()
        {
            RLog.Info("Creating default system font.");
            Face systemFont = RFontResources.SystemFont;
            FamilyName = systemFont.FamilyName;
            FaceCount = systemFont.FaceCount;
            FaceFlags = systemFont.FaceFlags;
            StyleName = systemFont.StyleName;
            StyleFlags = systemFont.StyleFlags;
        }
    }
    internal static class RFontResources
    {
        internal static Library Library = new Library();
        internal static Assembly Assembly = Assembly.GetAssembly(typeof(RFontResources));
        internal static Face GetResource(string resource){
            System.IO.BinaryReader reader = new System.IO.BinaryReader(Assembly.GetManifestResourceStream(resource));
            byte[] buffer = new byte[reader.BaseStream.Length];
            reader.Read(buffer, 0, buffer.Length);
            reader.Close();
            Face face;
            try
            {

                face = new Face(Library, buffer, 0);

                return face;
            }
            catch(Exception e)
            {
                RLog.Error(e);
                return null;
            }
        }

        internal static SharpFont.Face SystemFont = GetResource("Reactor.Fonts.coders_crux.ttf");

    }
}

