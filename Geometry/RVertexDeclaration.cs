using OpenTK.Graphics.OpenGL;
using Reactor.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Reactor.Types;

namespace Reactor.Geometry
{
    public class RVertexDeclaration
    {
        private RVertexElement[] _elements;
        private int _vertexStride;

        /// <summary>
        /// A hash value which can be used to compare declarations.
        /// </summary>
        internal int HashKey { get; private set; }

        Dictionary<int, RVertexDeclarationAttributeInfo> shaderAttributeInfo = new Dictionary<int, RVertexDeclarationAttributeInfo>();

		internal void Apply(RShader shader, IntPtr offset)
		{
            RVertexDeclarationAttributeInfo attrInfo;
            int shaderHash = shader.GetHashCode();
            if (!shaderAttributeInfo.TryGetValue(shaderHash, out attrInfo))
            {
                // Get the vertex attribute info and cache it
                attrInfo = new RVertexDeclarationAttributeInfo(16);

                foreach (var ve in _elements)
                {
                    var attributeLocation = shader.GetAttribLocation(ve.VertexElementUsage, ve.UsageIndex);
                    
                    if (attributeLocation >= 0)
                    {
                        attrInfo.Elements.Add(new RVertexDeclarationAttributeInfo.Element()
                        {
                            Offset = ve.Offset,
                            AttributeLocation = attributeLocation,
                            NumberOfElements = RVertexElement.OpenGLNumberOfElements(ve.VertexElementFormat),
                            VertexAttribPointerType = RVertexElement.OpenGLVertexAttribPointerType(ve.VertexElementFormat),
                            Normalized = RVertexElement.OpenGLVertexAttribNormalized(ve),
                        });
                        attrInfo.EnabledAttributes[attributeLocation] = true;
                    }
                }

                shaderAttributeInfo.Add(shaderHash, attrInfo);
            }

            // Apply the vertex attribute info
            foreach (var element in attrInfo.Elements)
            {
                GL.VertexAttribPointer(element.AttributeLocation,
                    element.NumberOfElements,
                    element.VertexAttribPointerType,
                    element.Normalized,
                    this.VertexStride,
                    (IntPtr)(offset.ToInt64() + element.Offset));
                REngine.CheckGLError();
            }
            //GraphicsDevice.SetVertexAttributeArray(attrInfo.EnabledAttributes);
		}

        /// <summary>
        /// Vertex attribute information for a particular shader/vertex declaration combination.
        /// </summary>
        internal class RVertexDeclarationAttributeInfo
        {
            internal bool[] EnabledAttributes;

            internal class Element
            {
                public int Offset;
                public int AttributeLocation;
                public int NumberOfElements;
                public VertexAttribPointerType VertexAttribPointerType;
                public bool Normalized;
            }

            internal List<Element> Elements;

            internal RVertexDeclarationAttributeInfo(int maxVertexAttributes)
            {
                EnabledAttributes = new bool[maxVertexAttributes];
                Elements = new List<Element>();
            }
        }
        public RVertexDeclaration(params RVertexElement[] elements)
            : this(GetVertexStride(elements), elements)
        {
        }

        public RVertexDeclaration(int vertexStride, params RVertexElement[] elements)
        {
            if ((elements == null) || (elements.Length == 0))
                throw new ArgumentNullException("elements", "Elements cannot be empty");

            var elementArray = (RVertexElement[])elements.Clone();
            _elements = elementArray;
            _vertexStride = vertexStride;

            // TODO: Is there a faster/better way to generate a
            // unique hashkey for the same vertex layouts?
            {
                var signature = string.Empty;
                foreach (var element in _elements)
                    signature += element;

                var bytes = System.Text.Encoding.UTF8.GetBytes(signature);
                HashKey = Hash.ComputeHash(bytes);
            }
        }

        private static int GetVertexStride(RVertexElement[] elements)
        {
            int max = 0;
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

        public int VertexStride
        {
            get
            {
                return _vertexStride;
            }
        }
        internal static RVertexDeclaration FromType(Type vertexType)
        {
            if (vertexType == null)
                throw new ArgumentNullException("vertexType", "Cannot be null");

            if (!ReflectionHelpers.IsValueType(vertexType))
            {
                throw new ArgumentException("vertexType", "Must be value type");
            }

            var type = Activator.CreateInstance(vertexType) as IVertexType;
            if (type == null)
            {
                throw new ArgumentException("vertexData does not inherit IVertexType");
            }

            var vertexDeclaration = type.VertexDeclaration;
            if (vertexDeclaration == null)
            {
                throw new Exception("VertexDeclaration cannot be null");
            }

            return vertexDeclaration;
        }
    }
}
