using Reactor.Types;
using System;
using System.Collections.Generic;

namespace Reactor
{
    public class RScene : RSingleton<RScene>
    {
        private RSceneNode _root;
        private RScene()
        {
            _root = new RSceneNode();
            _root.Children = new List<RSceneNode>();
            _root.Parent = null;
        }

        public void LoadScene(string filename)
        {
            //TODO: Load a scene from binary file format...
        }

        public void SaveScene(string filename)
        {
            //TODO: Save a scene to binary file format...
        }

        public T Create<T>(string name) where T : class
        {
            if(typeof(T).BaseType != typeof(RSceneNode))
            {
                RLog.Error("Tried to create a scene node from a non-scene node object! Name: "+name);
                return null;
            } else {
                T node = (T)Activator.CreateInstance(typeof(T), true);
                return node;
            }
        }
        public RMesh CreateMesh(string name)
        {
            return CreateMesh(name, _root);
        }
        public RMesh CreateMesh(string name, RSceneNode parent)
        {
            RMesh mesh = new RMesh();
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
            RMeshBuilder mesh = new RMeshBuilder();
            mesh.Name = name;
            mesh.Children = new List<RSceneNode>();
            mesh.Parent = parent;
            parent.Children.Add(mesh);
            return mesh;
        }
    }
}