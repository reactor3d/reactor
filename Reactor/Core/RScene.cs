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

using System;
using System.Collections.Generic;
using Reactor.Math;
using Reactor.Types;

namespace Reactor
{
    public class RScene : RSingleton<RScene>
    {
        private readonly List<RLight> _lights;
        private readonly RSceneNode _root;

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
            var node = RNode.Create<T>();
            RLog.Info("Created a new node named: " + name);

            return node;
        }

        public RMesh CreateMesh(string name)
        {
            return CreateMesh(name, _root);
        }

        public RMesh CreateMesh(string name, RSceneNode parent)
        {
            var mesh = RNode.Create<RMesh>();
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
            var mesh = (RMeshBuilder)Activator.CreateInstance(typeof(RMeshBuilder), true);
            mesh.Name = name;
            mesh.Children = new List<RSceneNode>();
            mesh.Parent = parent;
            parent.Children.Add(mesh);
            return mesh;
        }

        public RLight CreateLight(RLightType lightType = RLightType.POINT)
        {
            var light = new RLight
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
            var affectedLights = new List<RLight>();
            foreach (var light in _lights)
            {
                if (light.Type == RLightType.DIRECTIONAL)
                    affectedLights.Add(light);
                if (light.Type == RLightType.POINT)
                {
                    var sphere = new BoundingSphere(light.Position, light.Radius);
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
            var affectedLights = new List<RLight>();
            foreach (var light in _lights)
            {
                if (light.Type == RLightType.DIRECTIONAL)
                    affectedLights.Add(light);
                if (light.Type == RLightType.POINT)
                {
                    var sphere = new BoundingSphere(light.Position, light.Radius);
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

        public List<RLight> GetClosestLights(Vector3 WorldPosition)
        {
            var list = new List<RLight>(_lights);
            list.Sort((light1, light2) =>
            {
                return Vector3.Distance(light1.Position, WorldPosition)
                    .CompareTo(Vector3.Distance(light2.Position, WorldPosition));
            });
            return list;
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

        private BoundingBox bounds = default;

        private readonly Vector3 center = Vector3.Zero;

        private ROctree[] children;

        private readonly int depth;

        private readonly float length;

        private readonly float looseness;

        private readonly List<RRenderNode> objects = new List<RRenderNode>();

        private readonly float worldSize;

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
            length = this.looseness * this.worldSize / (float)System.Math.Pow(2, this.depth);
            var radius = length / 2f;

            // Create the bounding box.
            var min = this.center + new Vector3(-radius);
            var max = this.center + new Vector3(radius);
            bounds = new BoundingBox(min, max);

            // Split the octree if the depth hasn't been reached.
            if (this.depth < maxDepth) Split(maxDepth);
        }

        public void Remove(ref RRenderNode obj)
        {
            objects.Remove(obj);
        }

        public bool HasChanged(BoundingBox transformebbox)
        {
            return bounds.Contains(transformebbox) == ContainmentType.Contains;
        }

        public bool StillInside(Vector3 center, float radius)
        {
            var min = center - new Vector3(radius);
            var max = center + new Vector3(radius);
            var bounds = new BoundingBox(min, max);

            if (children != null)
                return false;

            if (this.bounds.Contains(bounds) == ContainmentType.Contains)
                return true;
            return false;
        }

        public bool StillInside(BoundingBox bounds)
        {
            if (children != null)
                return false;

            if (this.bounds.Contains(bounds) == ContainmentType.Contains)
                return true;
            return false;
        }

        public ROctree Add(ref RRenderNode o, Vector3 center, float radius)
        {
            var min = center - new Vector3(radius);
            var max = center + new Vector3(radius);
            var bounds = new BoundingBox(min, max);

            if (this.bounds.Contains(bounds) == ContainmentType.Contains) return Add(ref o, bounds, center, radius);
            return null;
        }

        public ROctree Add(ref RRenderNode o, BoundingBox transformebbox)
        {
            var radius = (transformebbox.Max - transformebbox.Min).Length() / 2;
            var center = (transformebbox.Max + transformebbox.Min) / 2;

            if (bounds.Contains(transformebbox) == ContainmentType.Contains)
                return Add(ref o, transformebbox, center, radius);
            return null;
        }

        private ROctree Add(ref RRenderNode o, BoundingBox bounds, Vector3 center, float radius)
        {
            if (children != null)
            {
                // Find which child the object is closest to based on where the
                // object's center is located in relation to the octree's center.
                var index = (center.X <= this.center.X ? 0 : 1) +
                            (center.Y >= this.center.Y ? 0 : 4) +
                            (center.Z <= this.center.Z ? 0 : 2);

                // Add the object to the child if it is fully contained within
                // it.
                if (children[index].bounds.Contains(bounds) == ContainmentType.Contains)
                    return children[index].Add(ref o, bounds, center, radius);
            }

            objects.Add(o);
            return this;
        }

        public int Draw(Matrix view, Matrix projection, ref List<RSceneNode> objects)
        {
            var frustum = new BoundingFrustum(view * projection);
            var containment = frustum.Contains(bounds);

            return Draw(frustum, view, projection, containment, ref objects);
        }

        private int Draw(BoundingFrustum frustum, Matrix view, Matrix projection,
            ContainmentType containment, ref List<RSceneNode> objects)
        {
            var count = 0;

            if (containment != ContainmentType.Contains) containment = frustum.Contains(bounds);

            // Draw the octree only if it is atleast partially in view.
            if (containment != ContainmentType.Disjoint)
            {
                // Draw the octree's bounds if there are objects in the octree.
                if (this.objects.Count > 0)
                {
                    foreach (var obj in this.objects) obj.Render();
                    count++;
                }

                // Draw the octree's children.
                if (children != null)
                    foreach (var child in children)
                        count += child.Draw(frustum, view, projection, containment, ref objects);
            }

            return count;
        }

        private void Split(int maxDepth)
        {
            children = new ROctree[ChildCount];
            var depth = this.depth + 1;
            var quarter = length / looseness / 4f;

            children[0] = new ROctree(worldSize, looseness,
                maxDepth, depth, center + new Vector3(-quarter, quarter, -quarter));
            children[1] = new ROctree(worldSize, looseness,
                maxDepth, depth, center + new Vector3(quarter, quarter, -quarter));
            children[2] = new ROctree(worldSize, looseness,
                maxDepth, depth, center + new Vector3(-quarter, quarter, quarter));
            children[3] = new ROctree(worldSize, looseness,
                maxDepth, depth, center + new Vector3(quarter, quarter, quarter));
            children[4] = new ROctree(worldSize, looseness,
                maxDepth, depth, center + new Vector3(-quarter, -quarter, -quarter));
            children[5] = new ROctree(worldSize, looseness,
                maxDepth, depth, center + new Vector3(quarter, -quarter, -quarter));
            children[6] = new ROctree(worldSize, looseness,
                maxDepth, depth, center + new Vector3(-quarter, -quarter, quarter));
            children[7] = new ROctree(worldSize, looseness,
                maxDepth, depth, center + new Vector3(quarter, -quarter, quarter));
        }
    }
}