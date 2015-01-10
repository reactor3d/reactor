using Reactor.Math;
using System;
using System.Runtime.CompilerServices;
using Reactor.Geometry;

namespace Reactor.Types
{
    public class RRenderNode : RUpdateNode
    {
        public RVertexBuffer VertexBuffer { get; internal set; }

        public virtual void Render()
        {
            if (IsDrawable)
            {
                if (OnRender != null)
                    OnRender(this, null);
            }
            
        }

        public event EventHandler OnRender;

        public bool IsDrawable { get; set; }

        
    }
}