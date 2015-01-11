using Reactor.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reactor.Types
{
    public class RCameraNode : RSceneNode
    {
        internal Matrix viewMatrix;
        internal Matrix projMatrix;
        internal Quaternion rotation;
        public Quaternion Rotation { get { return rotation;} set{rotation = value;} }
        public Vector3 Position { get; set; }

        public Matrix Projection { get { return projMatrix; } set { projMatrix = value; } }

        public Matrix View { get { return viewMatrix; } set { viewMatrix = value; } }
    }
}
