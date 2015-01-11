using Reactor.Geometry;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using Reactor.Loaders;


namespace Reactor.Types
{
    public class RMesh : RRenderNode
    {

        internal List<RMeshPart> Parts { get; set; }

        internal RShader Shader { get; set; }
        RMesh()
        {
            Parts = new List<RMeshPart>();
            Shader = new RShader();
            Shader.Load(RShaderResources.BasicEffectVert, RShaderResources.BasicEffectFrag, null);
        }



        public void LoadSourceModel(string filename)
        {
            this.LoadSource(filename);
        }

        public override void Render()
        {
            foreach(RMeshPart part in Parts)
            {
                
                part.Draw(Shader, PrimitiveType.Triangles, this.Matrix);
            }
        }
    }
}