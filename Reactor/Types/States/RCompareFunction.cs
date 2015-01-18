using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reactor.Types.States
{
    public enum RCompareFunction
    {
        Always,
        Never,
        Less,
        LessEqual,
        Equal,
        GreaterEqual,
        Greater,
        NotEqual
    }
}
