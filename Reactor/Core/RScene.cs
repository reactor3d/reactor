// Author:
//       Gabriel Reiser <gabe@reisergames.com>
//
// Copyright (c) 2010-2016 Reiser Games, LLC.
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
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
        public List<RLight> GetLightsForBounds(BoundingBox aabb)
        {
            List<RLight> affectedLights = new List<RLight>();
            foreach(var light in _lights)
            {
                if (light.Type == RLightType.DIRECTIONAL)
                    affectedLights.Add(light);
                if(light.Type == RLightType.POINT)
                {
                    BoundingSphere sphere = new BoundingSphere(light.Position, light.Radius);
                    if (sphere.Intersects(aabb))
                        affectedLights.Add(light);
                    if (sphere.Contains(aabb) != ContainmentType.Disjoint)
                        affectedLights.Add(light);
                }
                if (affectedLights.Count == 5)
                    break;
            }
            return affectedLights;
        }

        public List<RLight> GetLightsForBounds(BoundingSphere bounds)
        {
            List<RLight> affectedLights = new List<RLight>();
            foreach (var light in _lights)
            {
                if (light.Type == RLightType.DIRECTIONAL)
                    affectedLights.Add(light);
                if (light.Type == RLightType.POINT)
                {
                    BoundingSphere sphere = new BoundingSphere(light.Position, light.Radius);
                    if (sphere.Intersects(bounds))
                        affectedLights.Add(light);
                    if (sphere.Contains(bounds) != ContainmentType.Disjoint)
                        affectedLights.Add(light);
                }
                if (affectedLights.Count == 5)
                    break;
            }
            return affectedLights;
        }
        public void DestroyAll()
        {
            _lights.Clear();
            _root.Children.Clear();
        }
    }
    public class ROctree
    {

        private const int ChildCount = 8;

        private float looseness = 0;

        private int depth = 0;

        private Vector3 center = Vector3.Zero;

        private float length = 0f;

        private BoundingBox bounds = default(BoundingBox);

        private List<RRenderNode> objects = new List<RRenderNode>();

        private ROctree[] children = null;

        private float worldSize = 0f;

        public ROctree(float worldSize, float looseness, int depth)
            : this(worldSize, looseness, depth, 0, Vector3.Zero)
        {
        }

        public ROctree(float worldSize, float looseness, int depth, Vector3 center)
            : this(worldSize, looseness, depth, 0, center)
        {
        }

        private ROctree(float worldSize, float looseness,
            int maxDepth, int depth, Vector3 center)
        {
            this.worldSize = worldSize;
            this.looseness = looseness;
            this.depth = depth;
            this.center = center;
            this.length = this.looseness * this.worldSize / (float)System.Math.Pow(2, this.depth);
            float radius = this.length / 2f;

            // Create the bounding box.
            Vector3 min = this.center + new Vector3(-radius);
            Vector3 max = this.center + new Vector3(radius);
            this.bounds = new BoundingBox(min, max);

            // Split the octree if the depth hasn't been reached.
            if (this.depth < maxDepth)
            {
                this.Split(maxDepth);
            }
        }

        public void Remove(ref RRenderNode obj)
        {
            objects.Remove(obj);
        }

        public bool HasChanged(BoundingBox transformebbox)
        {
            return this.bounds.Contains(transformebbox) == ContainmentType.Contains;
        }

        public bool StillInside(Vector3 center, float radius)
        {
            Vector3 min = center - new Vector3(radius);
            Vector3 max = center + new Vector3(radius);
            BoundingBox bounds = new BoundingBox(min, max);

            if (this.children != null)
                return false;

            if (this.bounds.Contains(bounds) == ContainmentType.Contains)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool StillInside(BoundingBox bounds)
        {
            if (this.children != null)
                return false;

            if (this.bounds.Contains(bounds) == ContainmentType.Contains)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public ROctree Add(ref RRenderNode o, Vector3 center, float radius)
        {
            Vector3 min = center - new Vector3(radius);
            Vector3 max = center + new Vector3(radius);
            BoundingBox bounds = new BoundingBox(min, max);

            if (this.bounds.Contains(bounds) == ContainmentType.Contains)
            {
                return this.Add(ref o, bounds, center, radius);
            }
            return null;
        }

        public ROctree Add(ref RRenderNode o, BoundingBox transformebbox)
        {
            float radius = (transformebbox.Max - transformebbox.Min).Length() / 2;
            Vector3 center = (transformebbox.Max + transformebbox.Min) / 2;

            if (this.bounds.Contains(transformebbox) == ContainmentType.Contains)
            {
                return this.Add(ref o, transformebbox, center, radius);
            }
            return null;
        }

        private ROctree Add(ref RRenderNode o, BoundingBox bounds, Vector3 center, float radius)
        {
            if (this.children != null)
            {
                // Find which child the object is closest to based on where the
                // object's center is located in relation to the octree's center.
                int index = (center.X <= this.center.X ? 0 : 1) +
                    (center.Y >= this.center.Y ? 0 : 4) +
                    (center.Z <= this.center.Z ? 0 : 2);

                // Add the object to the child if it is fully contained within
                // it.
                if (this.children[index].bounds.Contains(bounds) == ContainmentType.Contains)
                {
                    return this.children[index].Add(ref o, bounds, center, radius);

                }
            }
            this.objects.Add(o);
            return this;
        }

        public int Draw(Matrix view, Matrix projection, List<RSceneNode> objects)
        {
            BoundingFrustum frustum = new BoundingFrustum(view * projection);
            ContainmentType containment = frustum.Contains(this.bounds);

            return this.Draw(frustum, view, projection, containment, objects);
        }

        private int Draw(BoundingFrustum frustum, Matrix view, Matrix projection,
            ContainmentType containment, List<RSceneNode> objects)
        {
            int count = 0;

            if (containment != ContainmentType.Contains)
            {
                containment = frustum.Contains(this.bounds);
            }

            // Draw the octree only if it is atleast partially in view.
            if (containment != ContainmentType.Disjoint)
            {
                // Draw the octree's bounds if there are objects in the octree.
                if (this.objects.Count > 0)
                {
                    objects.AddRange(this.objects);
                    count++;
                }

                // Draw the octree's children.
                if (this.children != null)
                {
                    foreach (ROctree child in this.children)
                    {
                        count += child.Draw(frustum, view, projection, containment, objects);
                    }
                }
            }

            return count;
        }

        private void Split(int maxDepth)
        {
            this.children = new ROctree[ROctree.ChildCount];
            int depth = this.depth + 1;
            float quarter = this.length / this.looseness / 4f;

            this.children[0] = new ROctree(this.worldSize, this.looseness,
                maxDepth, depth, this.center + new Vector3(-quarter, quarter, -quarter));
            this.children[1] = new ROctree(this.worldSize, this.looseness,
                maxDepth, depth, this.center + new Vector3(quarter, quarter, -quarter));
            this.children[2] = new ROctree(this.worldSize, this.looseness,
                maxDepth, depth, this.center + new Vector3(-quarter, quarter, quarter));
            this.children[3] = new ROctree(this.worldSize, this.looseness,
                maxDepth, depth, this.center + new Vector3(quarter, quarter, quarter));
            this.children[4] = new ROctree(this.worldSize, this.looseness,
                maxDepth, depth, this.center + new Vector3(-quarter, -quarter, -quarter));
            this.children[5] = new ROctree(this.worldSize, this.looseness,
                maxDepth, depth, this.center + new Vector3(quarter, -quarter, -quarter));
            this.children[6] = new ROctree(this.worldSize, this.looseness,
                maxDepth, depth, this.center + new Vector3(-quarter, -quarter, quarter));
            this.children[7] = new ROctree(this.worldSize, this.looseness,
                maxDepth, depth, this.center + new Vector3(quarter, -quarter, quarter));
        }

    }
}