using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reactor.Geometry
{
    internal class RVertexDeclarationCache<T>
        where T : IVertexType
    {
        static RVertexDeclaration _cached;

        static public RVertexDeclaration VertexDeclaration
        {
            get
            {
                if (_cached == null)
                {
                    _cached = (Activator.CreateInstance(typeof(T)) as IVertexType).VertexDeclaration;
                    if(_cached == null)
                        throw new NullReferenceException("RVertexDeclarationCache was unable to get a VertexDeclaration for an uncached IVertexType!");
                }

                return _cached;
            }
        }
    }
}
