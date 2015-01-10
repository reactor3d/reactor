using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Reactor.Types
{
    public enum RShaderEffectType : int
    {
        VERTEX = 0,
        FRAGMENT = 1,
        GEOMETRY = 2,
        TESS_CONTROL = 3,
        TESS_EVAL = 4,
        COMPUTE = 5
    }
}
