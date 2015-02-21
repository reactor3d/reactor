using Reactor.Math;
using Reactor.Types;
using System;
using System.Collections.Generic;

namespace Reactor
{
    public class RScene : RSingleton<RScene>
    {
        private RSceneNode _root;
        private List<RLight> _lights;
        public RScene()
        {
            _root = new RSceneNode();
            _root.Children = new List<RSceneNode>();
            _root.Parent = null;
            _lights = new List<RLight>();
        }

        public void LoadScene(string filename)
        {
            //TODO: Load a scene from binary file format...
        }

        public void SaveScene(string filename)
        {
            //TODO: Save a scene to binary file format...
        }

        public T Create<T>(string name) where T : RSceneNode
        {
            T node = RSceneNode.Create<T>();
            RLog.Info("Created a new node named: "+name);

            return node;
        }
        public RMesh CreateMesh(string name)
        {
            return CreateMesh(name, _root);
        }
        public RMesh CreateMesh(string name, RSceneNode parent)
        {
            RMesh mesh = RMesh.Create<RMesh>();
            mesh.Name = name;
            mesh.Children = new List<RSceneNode>();
            mesh.Parent = parent;
            parent.Children.Add(mesh);
            return mesh;
        }

        public RMeshBuilder CreateMeshBuilder(string name)
        {
            return CreateMeshBuilder(name, _root);
        }
        public RMeshBuilder CreateMeshBuilder(string name, RSceneNode parent)
        {
            RMeshBuilder mesh = (RMeshBuilder)Activator.CreateInstance(typeof(RMeshBuilder), true);
            mesh.Name = name;
            mesh.Children = new List<RSceneNode>();
            mesh.Parent = parent;
            parent.Children.Add(mesh);
            return mesh;
        }

        public RLight CreateLight(RLightType lightType=RLightType.POINT)
        {
            var light = new RLight()
            {
                Id = _lights.Count,
                Type = lightType,
                Position = Vector3.Zero,
                Radius = 1.0f,
                Color = RColor.White
            };
            _lights.Add(light);
            return light;
        }

        public void DestroyAll()
        {
            _lights.Clear();
            _root.Children.Clear();
        }
    }
}