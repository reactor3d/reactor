using Reactor.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reactor.Types
{
    public class RCameraNode : RUpdateNode
    {
        internal Matrix viewMatrix;
        internal Matrix projMatrix;

        public Matrix Projection { get { return projMatrix; } set { projMatrix = value; } }

        public Matrix View { get { return viewMatrix; } set { viewMatrix = value; } }
    }
}
