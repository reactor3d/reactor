using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reactor.Types.States
{
    public enum RDepthFormat
    {
        None = -1,
        Depth16 = 54,
        Depth24 = 51,
        Depth24Stencil8 = 48,
    }
}
