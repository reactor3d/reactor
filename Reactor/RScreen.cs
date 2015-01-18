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
using Reactor.Types.States;


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
        RIndexBuffer indexQuad2D;
        RVertexData2D[] quadVerts;
        RBlendState blendState;
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
            blendState = RBlendState.Opaque;
            defaultShader = new RShader();
            defaultShader.Load(RShaderResources.Basic2dEffectVert, RShaderResources.Basic2dEffectFrag, null);
            Fonts.Add(defaultFont);
            quad = new RMeshBuilder();
            quad.CreateFullscreenQuad();
            quadVerts = new RVertexData2D[4];
            quadVerts[0] = new RVertexData2D(new Vector2(-1, -1), new Vector2(0, 0));
            quadVerts[1] = new RVertexData2D(new Vector2(1, -1), new Vector2(1, 0));
            quadVerts[2] = new RVertexData2D(new Vector2(1, 1), new Vector2(1, 1));
            quadVerts[3] = new RVertexData2D(new Vector2(-1, 1), new Vector2(0, 1));
            vertexQuad2D = new RVertexBuffer(quadVerts[0].Declaration, 4, RBufferUsage.WriteOnly);
            vertexQuad2D.SetData<RVertexData2D>(quadVerts);
            indexQuad2D = new RIndexBuffer(typeof(short), 6, RBufferUsage.WriteOnly);
            indexQuad2D.SetData<short>(new short[6]{0,1,2,0,2,3}, 0, 6);
            initialized = true;
        }

        public void Begin()
        {
            oldCamera = REngine.Instance.GetCamera();
            REngine.Instance.SetCamera(camera2d);
            
            //GL.Viewport(0, (int)viewport.Width, 0, (int)viewport.Height);
            blendState.ColorWriteChannels = RColorWriteChannels.All;
            blendState.PlatformApplyState();
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
            GL.Disable(EnableCap.Blend);
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

        public void RenderTexture(RTexture texture, Rectangle bounds)
        {
            RenderTexture(texture, bounds, RColor.White);
        }
        public void RenderTexture(RTexture texture, Rectangle bounds, RColor color)
        {
           RenderTexture(texture, bounds, color, Matrix.Identity);
        }
        public void RenderTexture(RTexture texture, Rectangle bounds, RColor color, Matrix matrix)
        {
            RViewport viewport = REngine.Instance._viewport;
            UpdateQuad(bounds);
            defaultShader.Bind();
            defaultShader.SetSamplerValue(RTextureLayer.DIFFUSE, texture);
            vertexQuad2D.Bind();
            vertexQuad2D.BindVertexArray();
            indexQuad2D.Bind();


            defaultShader.SetUniformValue("projection", camera2d.Projection);
            defaultShader.SetUniformValue("view", camera2d.View);
            defaultShader.SetUniformValue("diffuse_color", color.ToVector4());
            defaultShader.SetUniformValue("model", matrix);
            vertexQuad2D.VertexDeclaration.Apply(defaultShader, IntPtr.Zero);


            GL.DrawElements(PrimitiveType.Triangles, indexQuad2D.IndexCount, DrawElementsType.UnsignedShort, IntPtr.Zero);
            REngine.CheckGLError();


            indexQuad2D.Unbind();
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

        public RBlendFunc AlphaBlendMode
        {
            get { return blendState.AlphaBlendFunction; }
            set { blendState.AlphaBlendFunction = value; }
        }

        public RBlend AlphaDestinationBlend
        {
            get { return blendState.AlphaDestinationBlend;}
            set { blendState.AlphaDestinationBlend = value; }
        }
        public RBlend AlphaSourceBlend
        {
            get { return blendState.AlphaSourceBlend;}
            set { blendState.AlphaSourceBlend = value; }
        }
        public RColor BlendFactor
        {
            get { return blendState.BlendFactor; }
            set { blendState.BlendFactor=value; }
        }
        public RBlendFunc ColorBlendMode
        {
            get { return blendState.ColorBlendFunction; }
            set { blendState.ColorBlendFunction = value; }
        }
        public RBlend ColorDestinationBlend
        {
            get { return blendState.ColorDestinationBlend;}
            set { blendState.ColorDestinationBlend = value; }
        }
        public RBlend ColorSourceBlend
        {
            get { return blendState.ColorSourceBlend;}
            set { blendState.ColorSourceBlend = value; }
        }
        public int MultiSampleMask
        {
            get { return blendState.MultiSampleMask; }
            set { blendState.MultiSampleMask = value; }
        }
    }
}

