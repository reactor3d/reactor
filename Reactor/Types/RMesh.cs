using Reactor.Geometry;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using Reactor.Loaders;
using Reactor.Math;
using OpenTK;


namespace Reactor.Types
{
    public class RMesh : RRenderNode, IDisposable
    {

        internal List<RMeshPart> Parts { get; set; }

        internal RShader Shader { get; set; }
        RMesh()
        {
            this.Matrix = Matrix4.Identity;
            this.Position = Vector3.Zero;
            this.Rotation = Quaternion.Identity;
            Parts = new List<RMeshPart>();
            Shader = new RShader();
            Shader.Load(RShaderResources.BasicEffectVert, RShaderResources.BasicEffectFrag, null);
        }

        #region IDisposable implementation
        public void Dispose()
        {
            Parts.Clear();
            Parts = null;
            Shader.Dispose();
        }
        #endregion


        public void LoadSourceModel(string filename)
        {
            this.LoadSource(RFileSystem.Instance.GetFilePath(filename));
        }

        public override void Render()
        {
            GL.Enable(EnableCap.DepthTest);
            REngine.CheckGLError();
            GL.DepthMask(true);
            REngine.CheckGLError();
            GL.DepthFunc(DepthFunction.Less);
            REngine.CheckGLError();


            GL.Enable(EnableCap.CullFace);
            REngine.CheckGLError();
            GL.FrontFace(FrontFaceDirection.Cw);
            REngine.CheckGLError();
            GL.CullFace(CullFaceMode.Back);
            REngine.CheckGLError();
            foreach(RMeshPart part in Parts)
            {
                
                part.Draw(Shader, PrimitiveType.Polygon, this.Matrix);
            }
        }
    }
}