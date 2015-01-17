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
using OpenTK.Graphics.OpenGL;


namespace Reactor
{
    public class RScreen : RSingleton<RScreen>
    {
        static List<RFont> Fonts = new List<RFont>();
        static RFont defaultFont = new RFont();
        bool initialized=false;
        RMeshBuilder quad;
        RShader defaultShader;
        RCamera camera2d;
        RCamera oldCamera;
        RVertexBuffer vertexQuad2D;
        public RScreen()
        {
            camera2d = new RCamera();
        }

        public void Init()
        {
            defaultShader = new RShader();
            defaultShader.Load(RShaderResources.Basic2dEffectVert, RShaderResources.Basic2dEffectFrag, null);
            Fonts.Add(defaultFont);
            quad = new RMeshBuilder();
            quad.CreateFullscreenQuad();
            RVertexData2D[] vertices = new RVertexData2D[6];
            vertices[0] = new RVertexData2D(new Vector2(0, 0), new Vector2(0,0));
            vertices[1] = new RVertexData2D(new Vector2(0, 1), new Vector2(0,1));
            vertices[2] = new RVertexData2D(new Vector2(1, 1), new Vector2(1,1));
            vertices[3] = new RVertexData2D(new Vector2(0, 0), new Vector2(0,0));
            vertices[4] = new RVertexData2D(new Vector2(1, 1), new Vector2(1,1));
            vertices[5] = new RVertexData2D(new Vector2(1, 0), new Vector2(1,0));
            vertexQuad2D = new RVertexBuffer(vertices[0].Declaration, 6, RBufferUsage.WriteOnly);
            vertexQuad2D.SetData<RVertexData2D>(vertices);

            initialized = true;
        }

        public void Begin()
        {
            oldCamera = REngine.Instance.GetCamera();
            var viewport = REngine.Instance._viewport;
            GL.Viewport(0, 0, (int)viewport.Width, (int)viewport.Height);
            GL.Disable(EnableCap.CullFace);
            GL.Disable(EnableCap.DepthTest);
            GL.DepthFunc(DepthFunction.Always);
            Matrix projection = Matrix.CreateOrthographicOffCenter(0, viewport.Width, viewport.Height, 0, 0, 1);
            //projection.M41 += -0.5f * projection.M11;
            //projection.M42 += -0.5f * projection.M22;
            camera2d.Projection = projection;
            camera2d.View = Matrix.Identity;
            REngine.Instance.SetCamera(camera2d);
        }

        public void End()
        {
            var viewport = REngine.Instance._viewport;
            viewport.Bind();
            REngine.Instance.SetCamera(oldCamera);
            GL.Enable(EnableCap.CullFace);
            GL.Enable(EnableCap.DepthTest);
            GL.DepthFunc(DepthFunction.Less);
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
            quad.Render();
        }

        public void RenderFullscreenQuad(RShader shader)
        {
            InitCheck();
            quad.SetShader(shader);
            quad.Render();
        }

        public void DrawTexture(RTexture2D texture, Rectangle screenBounds)
        {
            defaultShader.Bind();
            defaultShader.SetSamplerValue(RTextureLayer.DIFFUSE, texture);
            vertexQuad2D.Bind();
            vertexQuad2D.BindVertexArray();
            Matrix m = Matrix.CreateOrthographicOffCenter(screenBounds.Left, screenBounds.Right, screenBounds.Bottom, screenBounds.Top, -1, 1);
            defaultShader.SetUniformValue("projection", m);
            GL.DrawArrays(PrimitiveType.Triangles, 0, 6);
            vertexQuad2D.UnbindVertexArray();
            vertexQuad2D.Unbind();
            defaultShader.Unbind();

        }


    }
}

