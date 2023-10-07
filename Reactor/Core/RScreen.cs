// Author:
//       Gabriel Reiser <gabe@reisergames.com>
//
// Copyright (c) 2010-2016 Reiser Games, LLC.
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
using Reactor.Geometry;
using Reactor.Loaders;
using Reactor.Math;
using Reactor.Platform.GLFW;
using Reactor.Platform.OpenGL;
using Reactor.Types;
using Reactor.Types.States;

namespace Reactor
{
    public class RScreen : RSingleton<RScreen>
    {
        private static List<RFont> Fonts = new List<RFont>();
        private static Vector2 DPI;
        private RShader defaultShader;
        private RIndexBuffer indexQuad2D;
        private bool initialized;
        private RCamera oldCamera;
        private RMeshBuilder quad;
        private RVertexData2D[] quadVerts;
        private RVertexBuffer vertexQuad2D;

        public RScreen()
        {
            Camera = new RCamera2d();
            BlendState = RBlendState.AlphaBlend;
        }

        public RCamera2d Camera { get; set; }

        public RBlendState BlendState { get; set; }

        public static Vector2 GetScale()
        {
            if (DPI.X == 0)
            {
                var monitor = Glfw.PrimaryMonitor;
                DPI = new Vector2(monitor.ContentScale.X, monitor.ContentScale.Y);
            }

            return DPI;
        }

        internal void Init()
        {
            REngine.CheckGLError();

            defaultShader = new RShader();
            defaultShader.Load(RShaderResources.Basic2dEffectVert, RShaderResources.Basic2dEffectFrag, null);
            //Fonts.Add(RFont.Default);
            quad = new RMeshBuilder();
            quad.CreateQuad(new Vector2(0, 0), new Vector2(1, 1), true);
            quadVerts = new RVertexData2D[4];
            quadVerts[0] = new RVertexData2D(new Vector2(-1, -1), new Vector2(-1, -1));
            quadVerts[1] = new RVertexData2D(new Vector2(1, -1), new Vector2(1, -1));
            quadVerts[2] = new RVertexData2D(new Vector2(1, 1), new Vector2(1, 1));
            quadVerts[3] = new RVertexData2D(new Vector2(-1, 1), new Vector2(-1, 1));
            vertexQuad2D = new RVertexBuffer(quadVerts[0].Declaration, 4, RBufferUsage.WriteOnly);
            vertexQuad2D.SetData(quadVerts);
            indexQuad2D = new RIndexBuffer(typeof(short), 6, RBufferUsage.WriteOnly);
            indexQuad2D.SetData(new short[6] { 0, 1, 2, 0, 2, 3 }, 0, 6);
            initialized = true;
        }

        public void Begin()
        {
            var viewport = REngine.Instance.GetViewport();
            REngine.CheckGLError();
            oldCamera = REngine.Instance.GetCamera();
            GL.Disable(EnableCap.CullFace);
            GL.Disable(EnableCap.DepthTest);
            REngine.CheckGLError();
            GL.Viewport(0, 0, (int)viewport.Width, (int)viewport.Height);
            BlendState.PlatformApplyState();
            //GL.Disable(EnableCap.Blend);
            REngine.CheckGLError();
            GL.FrontFace(FrontFaceDirection.Cw);
            GL.CullFace(CullFaceMode.Back);
            REngine.CheckGLError();
            GL.PolygonMode(MaterialFace.Front, PolygonMode.Fill);
            REngine.CheckGLError();
            GL.Disable(EnableCap.CullFace);

            Camera.Update();
            REngine.Instance.SetCamera(Camera);
            REngine.CheckGLError();
        }

        public void End()
        {
            REngine.Instance.SetCamera(oldCamera);
            GL.Enable(EnableCap.CullFace);
            REngine.CheckGLError();
            GL.Enable(EnableCap.DepthTest);
            REngine.CheckGLError();
            //GL.Enable(EnableCap.Blend);
            REngine.CheckGLError();
        }


        public RFont LoadFont(string path)
        {
            var fnt = BitmapFontLoader.LoadFontFromBinaryFile(path);
            var font = new RFont(fnt);
            return font;
        }

        public void RenderFullscreenQuad(RShader shader)
        {
            var viewport = REngine.Instance._viewport;
            quadVerts[0].Position = new Vector2(0, 0);
            quadVerts[0].TexCoord = new Vector2(0, 0);
            quadVerts[1].Position = new Vector2(1, 0);
            quadVerts[1].TexCoord = new Vector2(1, 0);
            quadVerts[2].Position = new Vector2(1, 1);
            quadVerts[2].TexCoord = new Vector2(1, 1);
            quadVerts[3].Position = new Vector2(0, 1);
            quadVerts[3].TexCoord = new Vector2(0, 1);
            vertexQuad2D.SetData(quadVerts);
            shader.Bind();

            vertexQuad2D.Bind();
            vertexQuad2D.BindVertexArray();
            indexQuad2D.Bind();

            vertexQuad2D.VertexDeclaration.Apply(shader, IntPtr.Zero);


            GL.DrawElements(BeginMode.Triangles, indexQuad2D.IndexCount, DrawElementsType.UnsignedShort, IntPtr.Zero);
            REngine.CheckGLError();

            indexQuad2D.Unbind();
            vertexQuad2D.UnbindVertexArray();
            vertexQuad2D.Unbind();
            shader.Unbind();
        }

        public void RenderTexture(RTexture texture, Rectangle bounds)
        {
            RenderTexture(texture, bounds, RColor.White);
        }

        public void RenderTexture(RTexture texture, Rectangle bounds, RColor color)
        {
            RenderTexture(texture, bounds, color, Matrix.Identity, false);
        }

        public void RenderTexture(RTexture texture, Rectangle bounds, RColor color, Matrix matrix, bool font)
        {
            var viewport = REngine.Instance._viewport;
            UpdateQuad(bounds);
            defaultShader.Bind();
            defaultShader.SetSamplerValue(RTextureLayer.TEXTURE0, texture);
            vertexQuad2D.Bind();
            vertexQuad2D.BindVertexArray();
            indexQuad2D.Bind();


            defaultShader.SetUniformValue("projection", Camera.Projection);
            defaultShader.SetUniformValue("view", Camera.View);
            defaultShader.SetUniformValue("base_color", color.ToVector4());
            defaultShader.SetUniformValue("model", matrix);
            //defaultShader.SetUniformValue("font", font);
            vertexQuad2D.VertexDeclaration.Apply(defaultShader, IntPtr.Zero);


            GL.DrawElements(BeginMode.Triangles, indexQuad2D.IndexCount, DrawElementsType.UnsignedShort, IntPtr.Zero);
            REngine.CheckGLError();

            indexQuad2D.Unbind();
            vertexQuad2D.UnbindVertexArray();
            vertexQuad2D.Unbind();
            defaultShader.Unbind();
        }

        public void RenderText(RFont font, Vector2 penPoint, string text)
        {
            RenderText(font, penPoint, text, RColor.White);
        }

        public void RenderText(RFont font, Vector2 penPoint, string text, RColor color)
        {
            defaultShader.Bind();
            REngine.CheckGLError();
            defaultShader.SetUniformValue("projection", Camera.Projection);
            defaultShader.SetUniformValue("view", Camera.View);
            defaultShader.SetUniformValue("base_color", color.ToVector4());
            defaultShader.SetUniformValue("model", Matrix.Identity);
            vertexQuad2D.Clear();
            font.Render(ref defaultShader, ref vertexQuad2D, ref indexQuad2D, text, penPoint, color, Matrix.Identity);
            REngine.CheckGLError();
            defaultShader.Unbind();
            REngine.CheckGLError();
        }

        internal void RenderFPS(int fps)
        {
            Begin();
            REngine.CheckGLError();
            RenderText(RFont.Default, new Vector2(5, 5), string.Format("{0}fps", fps), RColor.Red);
            REngine.CheckGLError();
            End();
        }

        internal void RenderFPS(float fps)
        {
            REngine.CheckGLError();
            if (float.IsInfinity(fps))
                return;
            Begin();
            REngine.CheckGLError();

            RenderText(RFont.Default, new Vector2(5f, 5), $"{System.Math.Round(fps)}fps", RColor.WhiteSmoke);
            REngine.CheckGLError();

            End();
            REngine.CheckGLError();
        }

        private void UpdateQuad(Rectangle placement)
        {
            quadVerts[0].Position = new Vector2(placement.X, placement.Y);
            quadVerts[0].TexCoord = new Vector2(0, 0);

            quadVerts[1].Position = new Vector2(placement.X + placement.Width, placement.Y);
            quadVerts[1].TexCoord = new Vector2(1, 0);

            quadVerts[2].Position = new Vector2(placement.X + placement.Width, placement.Y + placement.Height);
            quadVerts[2].TexCoord = new Vector2(1, 1);

            quadVerts[3].Position = new Vector2(placement.X, placement.Y + placement.Height);
            quadVerts[3].TexCoord = new Vector2(0, 1);

            vertexQuad2D.SetData(quadVerts);
        }
    }
}