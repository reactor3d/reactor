// Author:
//       Gabriel Reiser <gabe@reisergames.com>
//
// Copyright (c) 2007-2020 Reiser Games, LLC.
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
using System.Text;
using Reactor.Platform.OpenGL;
using Reactor.Types;
using Reactor.Utilities;

namespace Reactor.Geometry
{
    public class RVertexDeclaration
    {
        private readonly RVertexElement[] _elements;

        private readonly Dictionary<int, RVertexDeclarationAttributeInfo> shaderAttributeInfo =
            new Dictionary<int, RVertexDeclarationAttributeInfo>();

        public RVertexDeclaration(params RVertexElement[] elements)
            : this(GetVertexStride(elements), elements)
        {
        }

        public RVertexDeclaration(int vertexStride, params RVertexElement[] elements)
        {
            if (elements == null || elements.Length == 0)
                throw new ArgumentNullException("elements", "Elements cannot be empty");

            var elementArray = (RVertexElement[])elements.Clone();
            _elements = elementArray;
            VertexStride = vertexStride;

            // TODO: Is there a faster/better way to generate a
            // unique hashkey for the same vertex layouts?
            {
                var signature = string.Empty;
                foreach (var element in _elements)
                    signature += element;

                var bytes = Encoding.UTF8.GetBytes(signature);
                HashKey = Hash.ComputeHash(bytes);
            }
        }

        /// <summary>
        ///     A hash value which can be used to compare declarations.
        /// </summary>
        internal int HashKey { get; private set; }

        public int VertexStride { get; }

        internal void Apply(RShader shader, IntPtr offset)
        {
            RVertexDeclarationAttributeInfo attrInfo;
            var shaderHash = shader.GetHashCode();
            if (!shaderAttributeInfo.TryGetValue(shaderHash, out attrInfo))
            {
                // Get the vertex attribute info and cache it
                attrInfo = new RVertexDeclarationAttributeInfo(16);

                foreach (var ve in _elements)
                {
                    var attributeLocation = shader.GetAttribLocation(ve.VertexElementUsage);

                    if (attributeLocation >= 0)
                    {
                        attrInfo.Elements.Add(new RVertexDeclarationAttributeInfo.Element
                        {
                            Offset = ve.Offset,
                            AttributeLocation = attributeLocation,
                            NumberOfElements = RVertexElement.OpenGLNumberOfElements(ve.VertexElementFormat),
                            VertexAttribPointerType =
                                RVertexElement.OpenGLVertexAttribPointerType(ve.VertexElementFormat),
                            Normalized = RVertexElement.OpenGLVertexAttribNormalized(ve)
                        });
                        attrInfo.EnabledAttributes[attributeLocation] = true;
                    }
                }

                shaderAttributeInfo.Add(shaderHash, attrInfo);
            }

            // Apply the vertex attribute info
            foreach (var element in attrInfo.Elements)
            {
                GL.EnableVertexAttribArray(element.AttributeLocation);
                REngine.CheckGLError();
                GL.VertexAttribPointer(element.AttributeLocation,
                    element.NumberOfElements,
                    element.VertexAttribPointerType,
                    element.Normalized,
                    VertexStride,
                    (IntPtr)(offset.ToInt64() + element.Offset));
                REngine.CheckGLError();
            }
            //GraphicsDevice.SetVertexAttributeArray(attrInfo.EnabledAttributes);
        }

        private static int GetVertexStride(RVertexElement[] elements)
        {
            var max = 0;
            for (var i = 0; i < elements.Length; i++)
            {
                var start = elements[i].Offset + RVertexElement.GetSize(elements[i].VertexElementFormat);
                if (max < start)
                    max = start;
            }

            return max;
        }

        public RVertexElement[] GetVertexElements()
        {
            return (RVertexElement[])_elements.Clone();
        }

        internal static RVertexDeclaration FromType(Type vertexType)
        {
            if (vertexType == null)
                throw new ArgumentNullException("vertexType", "Cannot be null");

            if (!ReflectionHelpers.IsValueType(vertexType))
                throw new ArgumentException("vertexType", "Must be value type");

            var type = Activator.CreateInstance(vertexType) as IVertexType;
            if (type == null) throw new ArgumentException("vertexData does not inherit IVertexType");

            var vertexDeclaration = type.Declaration;
            if (vertexDeclaration == null) throw new Exception("VertexDeclaration cannot be null");

            return vertexDeclaration;
        }

        /// <summary>
        ///     Vertex attribute information for a particular shader/vertex declaration combination.
        /// </summary>
        internal class RVertexDeclarationAttributeInfo
        {
            internal List<Element> Elements;
            internal bool[] EnabledAttributes;

            internal RVertexDeclarationAttributeInfo(int maxVertexAttributes)
            {
                EnabledAttributes = new bool[maxVertexAttributes];
                Elements = new List<Element>();
            }

            internal class Element
            {
                public int AttributeLocation;
                public bool Normalized;
                public int NumberOfElements;
                public int Offset;
                public VertexAttribPointerType VertexAttribPointerType;
            }
        }
    }
}