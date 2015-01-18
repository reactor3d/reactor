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
        RCamera2d camera2d;
        RCamera oldCamera;
        RVertexBuffer vertexQuad2D;
        RVertexData2D[] quadVerts;
        public RScreen()
        {
            camera2d = new RCamera2d();
        }

        public RCamera2d Camera
        {
            get { return camera2d; }
            set { camera2d = value; }
        }

        public void Init()
        {
            defaultShader = new RShader();
            defaultShader.Load(RShaderResources.Basic2dEffectVert, RShaderResources.Basic2dEffectFrag, null);
            Fonts.Add(defaultFont);
            quad = new RMeshBuilder();
            quad.CreateFullscreenQuad();
            quadVerts = new RVertexData2D[4];
            quadVerts[0] = new RVertexData2D(new Vector2(-1, 1), new Vector2(0, 0));
            quadVerts[1] = new RVertexData2D(new Vector2(-1, -1), new Vector2(0, 1));
            quadVerts[2] = new RVertexData2D(new Vector2(1, 1), new Vector2(1, 0));
            quadVerts[3] = new RVertexData2D(new Vector2(1, -1), new Vector2(1, 1));
            vertexQuad2D = new RVertexBuffer(quadVerts[0].Declaration, 6, RBufferUsage.WriteOnly);
            vertexQuad2D.SetData<RVertexData2D>(quadVerts);

            initialized = true;
        }

        public void Begin()
        {
            oldCamera = REngine.Instance.GetCamera();
            REngine.Instance.SetCamera(camera2d);
            
            //GL.Viewport(0, (int)viewport.Width, 0, (int)viewport.Height);
            GL.Disable(EnableCap.DepthTest);
            REngine.CheckGLError();
            GL.DepthMask(false);
            REngine.CheckGLError();
            GL.DepthFunc(DepthFunction.Less);
            REngine.CheckGLError();

            GL.FrontFace(FrontFaceDirection.Ccw);
            GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Fill);
            REngine.CheckGLError();
            GL.Disable(EnableCap.CullFace);
            camera2d.Update();
            
        }

        public void End()
        {
            
            REngine.Instance.SetCamera(oldCamera);
            GL.Enable(EnableCap.CullFace);
            GL.Enable(EnableCap.DepthTest);
            GL.DepthFunc(DepthFunction.Less);

            REngine.CheckGLError();
            GL.DepthMask(true);
            REngine.CheckGLError();
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

        public void DrawTexture(RTexture2D texture, Rectangle bounds)
        {
            RViewport viewport = REngine.Instance._viewport;
            UpdateQuad(bounds);
            defaultShader.Bind();
            defaultShader.SetSamplerValue(RTextureLayer.DIFFUSE, texture);
            vertexQuad2D.Bind();
            vertexQuad2D.BindVertexArray();
            //Matrix m = Matrix.CreateOrthographicOffCenter(screenBounds.X*viewport.AspectRatio, screenBounds.Width * viewport.AspectRatio, screenBounds.Height, screenBounds.Y, 0, 1);
            defaultShader.SetUniformValue("projection", camera2d.Projection);
            defaultShader.SetUniformValue("view", camera2d.View);
            vertexQuad2D.VertexDeclaration.Apply(defaultShader, IntPtr.Zero);

            GL.DrawArrays(PrimitiveType.Quads, 0, 4);
            vertexQuad2D.UnbindVertexArray();
            vertexQuad2D.Unbind();
            defaultShader.Unbind();

        }
        void UpdateQuad(Rectangle placement)
        {
            quadVerts[0].Position = new Vector2(placement.X, placement.Y);
            quadVerts[0].TexCoord = new Vector2(0, 0);
            quadVerts[1].Position = new Vector2(placement.X + placement.Width, placement.Y);
            quadVerts[1].TexCoord = new Vector2(1, 0);
            quadVerts[2].Position = new Vector2(placement.X + placement.Width, placement.Y + placement.Height);
            quadVerts[2].TexCoord = new Vector2(1, 1);
            quadVerts[3].Position = new Vector2(placement.X, placement.Y + placement.Height);
            quadVerts[3].TexCoord = new Vector2(0, 1);
            vertexQuad2D.SetData<RVertexData2D>(quadVerts);
        }


    }
}

