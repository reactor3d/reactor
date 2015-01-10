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


        RMesh()
        {
            Parts = new List<RMeshPart>();
        }



        public void LoadSourceModel(string filename)
        {
            this.LoadSource(filename);
        }

        public override void Render()
        {
            foreach(RMeshPart part in Parts)
            {
                part.Draw(PrimitiveType.Triangles);
            }
        }
    }
}