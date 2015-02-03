using Reactor.Geometry;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using Reactor.Loaders;
using Reactor.Math;


namespace Reactor.Types
{
    public class RMesh : RRenderNode, IDisposable
    {

        internal List<RMeshPart> Parts { get; set; }

        internal RMaterial Material { get; set; }
        RMesh()
        {
            this.Matrix = Matrix.Identity;
            this.Position = Vector3.Zero;
            this.Rotation = Quaternion.Identity;
            Parts = new List<RMeshPart>();
            Material = RMaterial.defaultMaterial;
            this.CullEnable = true;
            this.CullMode = Reactor.Types.States.RCullMode.CullClockwiseFace;
            this.BlendEnable = false;
            this.DepthWrite= true;

        }

        #region IDisposable implementation
        public void Dispose()
        {
            Parts.Clear();
            Parts = null;
            Material.Dispose();
        }
        #endregion


        public void LoadSourceModel(string filename)
        {
            this.LoadSource(RFileSystem.Instance.GetFilePath(filename));
        }

        public override void Render()
        {

            ApplyState();
            Material.Apply();
            foreach(RMeshPart part in Parts)
            {
                
                part.Draw(Material, PrimitiveType.Triangles, this.Matrix);
            }
            Material.Shader.Unbind();
        }
    }
}