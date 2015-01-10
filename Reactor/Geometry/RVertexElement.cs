using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Reactor.Utilities;
using System.IO;

namespace Reactor.Geometry
{
    public struct RVertexElement
    {
        internal int _offset;
        internal RVertexElementFormat _format;
        internal RVertexElementUsage _usage;
        internal int _usageIndex;

        public int Offset
        {
            get
            {
                return this._offset;
            }
            set
            {
                this._offset = value;
            }
        }

        public RVertexElementFormat VertexElementFormat
        {
            get
            {
                return this._format;
            }
            set
            {
                this._format = value;
            }
        }

        public RVertexElementUsage VertexElementUsage
        {
            get
            {
                return this._usage;
            }
            set
            {
                this._usage = value;
            }
        }

        public int UsageIndex
        {
            get
            {
                return this._usageIndex;
            }
            set
            {
                this._usageIndex = value;
            }
        }

        public RVertexElement(int offset, RVertexElementFormat elementFormat, RVertexElementUsage elementUsage, int usageIndex)
        {
            this._offset = offset;
            this._usageIndex = usageIndex;
            this._format = elementFormat;
            this._usage = elementUsage;
        }

        public override int GetHashCode()
        {
            Stream stream = new MemoryStream();
            Serialization.WriteStruct<RVertexElement>(ref stream, (RVertexElement)this);
            return Hash.ComputeHash(ref stream);
        }

        public override string ToString()
        {
            return "{{Offset:" + this.Offset + " Format:" + this.VertexElementFormat + " Usage:" + this.VertexElementUsage + " UsageIndex: " + this.UsageIndex + "}}";
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            if (obj.GetType() != base.GetType())
            {
                return false;
            }
            return (this == ((RVertexElement)obj));
        }

        public static bool operator ==(RVertexElement left, RVertexElement right)
        {
            return ((((left._offset == right._offset) && (left._usageIndex == right._usageIndex)) && (left._usage == right._usage)) && (left._format == right._format));
        }

        public static bool operator !=(RVertexElement left, RVertexElement right)
        {
            return !(left == right);
        }

        internal static int OpenGLNumberOfElements(RVertexElementFormat elementFormat)
        {
            switch (elementFormat)
            {
                case RVertexElementFormat.Single:
                    return 1;

                case RVertexElementFormat.Vector2:
                    return 2;

                case RVertexElementFormat.Vector3:
                    return 3;

                case RVertexElementFormat.Vector4:
                    return 4;

                case RVertexElementFormat.Color:
                    return 4;

                case RVertexElementFormat.Byte4:
                    return 4;

                case RVertexElementFormat.Short2:
                    return 2;

                case RVertexElementFormat.Short4:
                    return 2;

                case RVertexElementFormat.NormalizedShort2:
                    return 2;

                case RVertexElementFormat.NormalizedShort4:
                    return 4;

                case RVertexElementFormat.HalfVector2:
                    return 2;

                case RVertexElementFormat.HalfVector4:
                    return 4;
            }

            throw new ArgumentException();
        }
        internal static VertexPointerType OpenGLVertexPointerType(RVertexElementFormat elementFormat)
        {
            switch (elementFormat)
            {
                case RVertexElementFormat.Single:
                    return VertexPointerType.Float;

                case RVertexElementFormat.Vector2:
                    return VertexPointerType.Float;

                case RVertexElementFormat.Vector3:
                    return VertexPointerType.Float;

                case RVertexElementFormat.Vector4:
                    return VertexPointerType.Float;

                case RVertexElementFormat.Color:
                    return VertexPointerType.Short;

                case RVertexElementFormat.Byte4:
                    return VertexPointerType.Short;

                case RVertexElementFormat.Short2:
                    return VertexPointerType.Short;

                case RVertexElementFormat.Short4:
                    return VertexPointerType.Short;

                case RVertexElementFormat.NormalizedShort2:
                    return VertexPointerType.Short;

                case RVertexElementFormat.NormalizedShort4:
                    return VertexPointerType.Short;

                case RVertexElementFormat.HalfVector2:
                    return VertexPointerType.Float;

                case RVertexElementFormat.HalfVector4:
                    return VertexPointerType.Float;
            }

            throw new ArgumentException();
        }

        internal static VertexAttribPointerType OpenGLVertexAttribPointerType(RVertexElementFormat elementFormat)
        {
            switch (elementFormat)
            {
                case RVertexElementFormat.Single:
                    return VertexAttribPointerType.Float;

                case RVertexElementFormat.Vector2:
                    return VertexAttribPointerType.Float;

                case RVertexElementFormat.Vector3:
                    return VertexAttribPointerType.Float;

                case RVertexElementFormat.Vector4:
                    return VertexAttribPointerType.Float;

                case RVertexElementFormat.Color:
                    return VertexAttribPointerType.UnsignedByte;

                case RVertexElementFormat.Byte4:
                    return VertexAttribPointerType.UnsignedByte;

                case RVertexElementFormat.Short2:
                    return VertexAttribPointerType.Short;

                case RVertexElementFormat.Short4:
                    return VertexAttribPointerType.Short;

                case RVertexElementFormat.NormalizedShort2:
                    return VertexAttribPointerType.Short;

                case RVertexElementFormat.NormalizedShort4:
                    return VertexAttribPointerType.Short;

                case RVertexElementFormat.HalfVector2:
                    return VertexAttribPointerType.HalfFloat;

                case RVertexElementFormat.HalfVector4:
                    return VertexAttribPointerType.HalfFloat;

            }

            throw new ArgumentException();
        }

        internal static bool OpenGLVertexAttribNormalized(RVertexElement element)
        {
            // TODO: This may or may not be the right behavor.  
            //
            // For instance the VertexElementFormat.Byte4 format is not supposed
            // to be normalized, but this line makes it so.
            //
            // The question is in MS XNA are types normalized based on usage or
            // normalized based to their format?
            //
            if (element.VertexElementUsage == RVertexElementUsage.Color)
                return true;

            switch (element.VertexElementFormat)
            {
                case RVertexElementFormat.NormalizedShort2:
                case RVertexElementFormat.NormalizedShort4:
                    return true;

                default:
                    return false;
            }
        }

        internal static ColorPointerType OpenGLColorPointerType(RVertexElementFormat elementFormat)
        {
            switch (elementFormat)
            {
                case RVertexElementFormat.Single:
                    return ColorPointerType.Float;

                case RVertexElementFormat.Vector2:
                    return ColorPointerType.Float;

                case RVertexElementFormat.Vector3:
                    return ColorPointerType.Float;

                case RVertexElementFormat.Vector4:
                    return ColorPointerType.Float;

                case RVertexElementFormat.Color:
                    return ColorPointerType.UnsignedByte;

                case RVertexElementFormat.Byte4:
                    return ColorPointerType.UnsignedByte;

                case RVertexElementFormat.Short2:
                    return ColorPointerType.Short;

                case RVertexElementFormat.Short4:
                    return ColorPointerType.Short;

                case RVertexElementFormat.NormalizedShort2:
                    return ColorPointerType.UnsignedShort;

                case RVertexElementFormat.NormalizedShort4:
                    return ColorPointerType.UnsignedShort;

                case RVertexElementFormat.HalfVector2:
                    return ColorPointerType.HalfFloat;

                case RVertexElementFormat.HalfVector4:
                    return ColorPointerType.HalfFloat;

            }

            throw new ArgumentException();
        }

        internal static NormalPointerType OpenGLNormalPointerType(RVertexElementFormat elementFormat)
        {
            switch (elementFormat)
            {
                case RVertexElementFormat.Single:
                    return NormalPointerType.Float;

                case RVertexElementFormat.Vector2:
                    return NormalPointerType.Float;

                case RVertexElementFormat.Vector3:
                    return NormalPointerType.Float;

                case RVertexElementFormat.Vector4:
                    return NormalPointerType.Float;

                case RVertexElementFormat.Color:
                    return NormalPointerType.Byte;

                case RVertexElementFormat.Byte4:
                    return NormalPointerType.Byte;

                case RVertexElementFormat.Short2:
                    return NormalPointerType.Short;

                case RVertexElementFormat.Short4:
                    return NormalPointerType.Short;

                case RVertexElementFormat.NormalizedShort2:
                    return NormalPointerType.Short;

                case RVertexElementFormat.NormalizedShort4:
                    return NormalPointerType.Short;

                case RVertexElementFormat.HalfVector2:
                    return NormalPointerType.HalfFloat;

                case RVertexElementFormat.HalfVector4:
                    return NormalPointerType.HalfFloat;
            }

            throw new ArgumentException();
        }
        internal static int GetSize(RVertexElementFormat elementFormat)
        {
            switch (elementFormat)
            {
                case RVertexElementFormat.Single:
                    return 4;

                case RVertexElementFormat.Vector2:
                    return 8;

                case RVertexElementFormat.Vector3:
                    return 12;

                case RVertexElementFormat.Vector4:
                    return 16;

                case RVertexElementFormat.Color:
                    return 4;

                case RVertexElementFormat.Byte4:
                    return 4;

                case RVertexElementFormat.Short2:
                    return 4;

                case RVertexElementFormat.Short4:
                    return 8;

                case RVertexElementFormat.NormalizedShort2:
                    return 4;

                case RVertexElementFormat.NormalizedShort4:
                    return 8;

                case RVertexElementFormat.HalfVector2:
                    return 4;

                case RVertexElementFormat.HalfVector4:
                    return 8;
            }
            return 0;
        }
    }
}
