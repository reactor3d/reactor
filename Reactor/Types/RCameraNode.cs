using Reactor.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace Reactor.Types
{
    public class RCameraNode : RUpdateNode
    {
        internal Matrix4 viewMatrix;
        internal Matrix4 projMatrix;

        public Matrix4 Projection { get { return projMatrix; } set { projMatrix = value; } }

        public Matrix4 View { get { return viewMatrix; } set { viewMatrix = value; } }
    }
}
