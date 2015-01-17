//
// RScreen.cs
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
using System.Collections.Generic;
using System.Drawing.Text;
using Reactor.Types;
using Reactor.Geometry;
using Reactor.Math;
namespace Reactor
{
    public class RScreen : RSingleton<RScreen>
    {
        static List<RFont> Fonts = new List<RFont>();
        static RFont defaultFont = new RFont();
        bool initialized=false;
        RMeshBuilder quad;
        RShader defaultShader;
        public RScreen()
        {

        }

        internal void Init()
        {
            defaultShader = new RShader();
            defaultShader.Load(RShaderResources.Basic2dEffectVert, RShaderResources.Basic2dEffectFrag, null);
            Fonts.Add(defaultFont);
            quad = new RMeshBuilder();
            quad.CreateFullscreenQuad();
            initialized = true;
        }
        void InitCheck()
        {
            if(!initialized)
                throw new ReactorException("You must first call Init() before using RScreen.");
        }

        public RFont LoadFont(string path)
        {
            InitCheck();
            RFont font = new RFont();
            font.Load(RFileSystem.Instance.GetFilePath(path));
            return font;

        }

        public RFont LoadTextureFont(string fontName, int size)
        {
            InitCheck();
            return null;
        }

        public void RenderFullscreenQuad()
        {
            InitCheck();
        }

        public void RenderFullscreenQuad(RShader shader)
        {
            InitCheck();
        }


    }
}

