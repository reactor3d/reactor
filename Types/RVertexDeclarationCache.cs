using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reactor.Types
{
    internal class RVertexDeclarationCache<T>
        where T : struct, IVertexType
    {
        static private RVertexDeclaration _cached;

        static public RVertexDeclaration VertexDeclaration
        {
            get
            {
                if (_cached == null)
                    _cached = RVertexDeclaration.FromType(typeof(T));

                return _cached;
            }
        }
    }
}
