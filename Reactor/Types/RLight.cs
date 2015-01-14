using Reactor.Math;

namespace Reactor.Types
{
    public struct RLight
    {
        public int Id;
        public string Name;
        public RLightType Type;
        public Vector3 Position;
        public float Radius;
        public float Phi;
        public float Theta;
        public RMesh Mesh;
    }
}