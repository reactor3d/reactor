using System;
using System.Text;
using System.Runtime.Serialization;
using System.Diagnostics;
using Reactor.Math;

namespace Reactor.Types
{
    /// <summary>
    /// Describes a 32-bit packed RColor.
    /// </summary>
    
    [DebuggerDisplay("{DebugDisplayString,nq}")]
    public struct RColor : IEquatable<RColor>
    {
        static RColor()
        {
            TransparentBlack = new RColor(0);
            Transparent = new RColor(0);
            AliceBlue = new RColor(0xfffff8f0);
            AntiqueWhite = new RColor(0xffd7ebfa);
            Aqua = new RColor(0xffffff00);
            Aquamarine = new RColor(0xffd4ff7f);
            Azure = new RColor(0xfffffff0);
            Beige = new RColor(0xffdcf5f5);
            Bisque = new RColor(0xffc4e4ff);
            Black = new RColor(0xff000000);
            BlanchedAlmond = new RColor(0xffcdebff);
            Blue = new RColor(0xffff0000);
            BlueViolet = new RColor(0xffe22b8a);
            Brown = new RColor(0xff2a2aa5);
            BurlyWood = new RColor(0xff87b8de);
            CadetBlue = new RColor(0xffa09e5f);
            Chartreuse = new RColor(0xff00ff7f);
            Chocolate = new RColor(0xff1e69d2);
            Coral = new RColor(0xff507fff);
            CornflowerBlue = new RColor(0xffed9564);
            Cornsilk = new RColor(0xffdcf8ff);
            Crimson = new RColor(0xff3c14dc);
            Cyan = new RColor(0xffffff00);
            DarkBlue = new RColor(0xff8b0000);
            DarkCyan = new RColor(0xff8b8b00);
            DarkGoldenrod = new RColor(0xff0b86b8);
            DarkGray = new RColor(0xffa9a9a9);
            DarkGreen = new RColor(0xff006400);
            DarkKhaki = new RColor(0xff6bb7bd);
            DarkMagenta = new RColor(0xff8b008b);
            DarkOliveGreen = new RColor(0xff2f6b55);
            DarkOrange = new RColor(0xff008cff);
            DarkOrchid = new RColor(0xffcc3299);
            DarkRed = new RColor(0xff00008b);
            DarkSalmon = new RColor(0xff7a96e9);
            DarkSeaGreen = new RColor(0xff8bbc8f);
            DarkSlateBlue = new RColor(0xff8b3d48);
            DarkSlateGray = new RColor(0xff4f4f2f);
            DarkTurquoise = new RColor(0xffd1ce00);
            DarkViolet = new RColor(0xffd30094);
            DeepPink = new RColor(0xff9314ff);
            DeepSkyBlue = new RColor(0xffffbf00);
            DimGray = new RColor(0xff696969);
            DodgerBlue = new RColor(0xffff901e);
            Firebrick = new RColor(0xff2222b2);
            FloralWhite = new RColor(0xfff0faff);
            ForestGreen = new RColor(0xff228b22);
            Fuchsia = new RColor(0xffff00ff);
            Gainsboro = new RColor(0xffdcdcdc);
            GhostWhite = new RColor(0xfffff8f8);
            Gold = new RColor(0xff00d7ff);
            Goldenrod = new RColor(0xff20a5da);
            Gray = new RColor(0xff808080);
            Green = new RColor(0xff008000);
            GreenYellow = new RColor(0xff2fffad);
            Honeydew = new RColor(0xfff0fff0);
            HotPink = new RColor(0xffb469ff);
            IndianRed = new RColor(0xff5c5ccd);
            Indigo = new RColor(0xff82004b);
            Ivory = new RColor(0xfff0ffff);
            Khaki = new RColor(0xff8ce6f0);
            Lavender = new RColor(0xfffae6e6);
            LavenderBlush = new RColor(0xfff5f0ff);
            LawnGreen = new RColor(0xff00fc7c);
            LemonChiffon = new RColor(0xffcdfaff);
            LightBlue = new RColor(0xffe6d8ad);
            LightCoral = new RColor(0xff8080f0);
            LightCyan = new RColor(0xffffffe0);
            LightGoldenrodYellow = new RColor(0xffd2fafa);
            LightGray = new RColor(0xffd3d3d3);
            LightGreen = new RColor(0xff90ee90);
            LightPink = new RColor(0xffc1b6ff);
            LightSalmon = new RColor(0xff7aa0ff);
            LightSeaGreen = new RColor(0xffaab220);
            LightSkyBlue = new RColor(0xffface87);
            LightSlateGray = new RColor(0xff998877);
            LightSteelBlue = new RColor(0xffdec4b0);
            LightYellow = new RColor(0xffe0ffff);
            Lime = new RColor(0xff00ff00);
            LimeGreen = new RColor(0xff32cd32);
            Linen = new RColor(0xffe6f0fa);
            Magenta = new RColor(0xffff00ff);
            Maroon = new RColor(0xff000080);
            MediumAquamarine = new RColor(0xffaacd66);
            MediumBlue = new RColor(0xffcd0000);
            MediumOrchid = new RColor(0xffd355ba);
            MediumPurple = new RColor(0xffdb7093);
            MediumSeaGreen = new RColor(0xff71b33c);
            MediumSlateBlue = new RColor(0xffee687b);
            MediumSpringGreen = new RColor(0xff9afa00);
            MediumTurquoise = new RColor(0xffccd148);
            MediumVioletRed = new RColor(0xff8515c7);
            MidnightBlue = new RColor(0xff701919);
            MintCream = new RColor(0xfffafff5);
            MistyRose = new RColor(0xffe1e4ff);
            Moccasin = new RColor(0xffb5e4ff);
            NavajoWhite = new RColor(0xffaddeff);
            Navy = new RColor(0xff800000);
            OldLace = new RColor(0xffe6f5fd);
            Olive = new RColor(0xff008080);
            OliveDrab = new RColor(0xff238e6b);
            Orange = new RColor(0xff00a5ff);
            OrangeRed = new RColor(0xff0045ff);
            Orchid = new RColor(0xffd670da);
            PaleGoldenrod = new RColor(0xffaae8ee);
            PaleGreen = new RColor(0xff98fb98);
            PaleTurquoise = new RColor(0xffeeeeaf);
            PaleVioletRed = new RColor(0xff9370db);
            PapayaWhip = new RColor(0xffd5efff);
            PeachPuff = new RColor(0xffb9daff);
            Peru = new RColor(0xff3f85cd);
            Pink = new RColor(0xffcbc0ff);
            Plum = new RColor(0xffdda0dd);
            PowderBlue = new RColor(0xffe6e0b0);
            Purple = new RColor(0xff800080);
            Red = new RColor(0xff0000ff);
            RosyBrown = new RColor(0xff8f8fbc);
            RoyalBlue = new RColor(0xffe16941);
            SaddleBrown = new RColor(0xff13458b);
            Salmon= new RColor(0xff7280fa);
            SandyBrown = new RColor(0xff60a4f4);
            SeaGreen = new RColor(0xff578b2e);
            SeaShell = new RColor(0xffeef5ff);
            Sienna = new RColor(0xff2d52a0);
            Silver  = new RColor(0xffc0c0c0);
            SkyBlue  = new RColor(0xffebce87);
            SlateBlue= new RColor(0xffcd5a6a);
            SlateGray= new RColor(0xff908070);
            Snow= new RColor(0xfffafaff);
            SpringGreen= new RColor(0xff7fff00);
            SteelBlue= new RColor(0xffb48246);
            Tan= new RColor(0xff8cb4d2);
            Teal= new RColor(0xff808000);
            Thistle= new RColor(0xffd8bfd8);
            Tomato= new RColor(0xff4763ff);
            Turquoise= new RColor(0xffd0e040);
            Violet= new RColor(0xffee82ee);
            Wheat= new RColor(0xffb3def5);
            White= new RColor(uint.MaxValue);
            WhiteSmoke= new RColor(0xfff5f5f5);
            Yellow = new RColor(0xff00ffff);
            YellowGreen = new RColor(0xff32cd9a);
        }
	// ARGB
        private uint _packedValue;
	  
        private RColor(uint packedValue)
        {
            _packedValue = packedValue;
			// ARGB
			//_packedValue = (packedValue << 8) | ((packedValue & 0xff000000) >> 24);
			// ABGR			
			//_packedValue = (packedValue & 0xff00ff00) | ((packedValue & 0x000000ff) << 16) | ((packedValue & 0x00ff0000) >> 16);
        }

	/// <summary>
        /// Creates a new instance of <see cref="RColor"/> struct.
        /// </summary>
        /// <param name="RColor">A <see cref="Vector4"/> representing RColor.</param>
        public RColor(Vector4 RColor)
        {
            _packedValue = 0;
			
			R = (byte)Reactor.Math.MathHelper.Clamp(RColor.X * 255, Byte.MinValue, Byte.MaxValue);
            G = (byte)Reactor.Math.MathHelper.Clamp(RColor.Y * 255, Byte.MinValue, Byte.MaxValue);
            B = (byte)Reactor.Math.MathHelper.Clamp(RColor.Z * 255, Byte.MinValue, Byte.MaxValue);
            A = (byte)Reactor.Math.MathHelper.Clamp(RColor.W * 255, Byte.MinValue, Byte.MaxValue);
        }

	/// <summary>
        /// Creates a new instance of <see cref="RColor"/> struct.
        /// </summary>
        /// <param name="RColor">A <see cref="Vector3"/> representing RColor.</param>
        public RColor(Vector3 RColor)
        {
            _packedValue = 0;

            R = (byte)Reactor.Math.MathHelper.Clamp(RColor.X * 255, Byte.MinValue, Byte.MaxValue);
            G = (byte)Reactor.Math.MathHelper.Clamp(RColor.Y * 255, Byte.MinValue, Byte.MaxValue);
            B = (byte)Reactor.Math.MathHelper.Clamp(RColor.Z * 255, Byte.MinValue, Byte.MaxValue);
            A = 255;
        }
	
	/// <summary>
        /// Creates a new instance of <see cref="RColor"/> struct.
        /// </summary>
        /// <param name="RColor">A <see cref="RColor"/> for RGB values of new <see cref="RColor"/> instance.</param>
        /// <param name="alpha">Alpha component value.</param>
        public RColor(RColor RColor, int alpha)
        {
            _packedValue = 0;

            R = RColor.R;
            G = RColor.G;
            B = RColor.B;
            A = (byte)Reactor.Math.MathHelper.Clamp(alpha, Byte.MinValue, Byte.MaxValue);
        }
	
	/// <summary>
        /// Creates a new instance of <see cref="RColor"/> struct.
        /// </summary>
        /// <param name="RColor">A <see cref="RColor"/> for RGB values of new <see cref="RColor"/> instance.</param>
        /// <param name="alpha">Alpha component value.</param>
        public RColor(RColor RColor, float alpha)
        {
            _packedValue = 0;

            R = RColor.R;
            G = RColor.G;
            B = RColor.B;
            A = (byte)Reactor.Math.MathHelper.Clamp(alpha * 255, Byte.MinValue, Byte.MaxValue);
        }
	
	/// <summary>
        /// Creates a new instance of <see cref="RColor"/> struct.
        /// </summary>
        /// <param name="r">Red component value.</param>
        /// <param name="g">Green component value.</param>
        /// <param name="b">Blue component value</param>
        public RColor(float r, float g, float b)
        {
            _packedValue = 0;
			
            R = (byte)Reactor.Math.MathHelper.Clamp(r * 255, Byte.MinValue, Byte.MaxValue);
            G = (byte)Reactor.Math.MathHelper.Clamp(g * 255, Byte.MinValue, Byte.MaxValue);
            B = (byte)Reactor.Math.MathHelper.Clamp(b * 255, Byte.MinValue, Byte.MaxValue);
            A = 255;
        }
	
	/// <summary>
        /// Creates a new instance of <see cref="RColor"/> struct.
        /// </summary>
        /// <param name="r">Red component value.</param>
        /// <param name="g">Green component value.</param>
        /// <param name="b">Blue component value</param>
        public RColor(int r, int g, int b)
        {
            _packedValue = 0;
            R = (byte)Reactor.Math.MathHelper.Clamp(r, Byte.MinValue, Byte.MaxValue);
            G = (byte)Reactor.Math.MathHelper.Clamp(g, Byte.MinValue, Byte.MaxValue);
            B = (byte)Reactor.Math.MathHelper.Clamp(b, Byte.MinValue, Byte.MaxValue);
            A = (byte)255;
        }

	/// <summary>
        /// Creates a new instance of <see cref="RColor"/> struct.
        /// </summary>
        /// <param name="r">Red component value.</param>
        /// <param name="g">Green component value.</param>
        /// <param name="b">Blue component value</param>
        /// <param name="alpha">Alpha component value.</param>
        public RColor(int r, int g, int b, int alpha)
        {
            _packedValue = 0;
            R = (byte)Reactor.Math.MathHelper.Clamp(r, Byte.MinValue, Byte.MaxValue);
            G = (byte)Reactor.Math.MathHelper.Clamp(g, Byte.MinValue, Byte.MaxValue);
            B = (byte)Reactor.Math.MathHelper.Clamp(b, Byte.MinValue, Byte.MaxValue);
            A = (byte)Reactor.Math.MathHelper.Clamp(alpha, Byte.MinValue, Byte.MaxValue);
        }
	
	/// <summary>
        /// Creates a new instance of <see cref="RColor"/> struct.
        /// </summary>
        /// <param name="r">Red component value.</param>
        /// <param name="g">Green component value.</param>
        /// <param name="b">Blue component value</param>
        /// <param name="alpha">Alpha component value.</param>
        public RColor(float r, float g, float b, float alpha)
        {
            _packedValue = 0;
			
            R = (byte)Reactor.Math.MathHelper.Clamp(r * 255, Byte.MinValue, Byte.MaxValue);
            G = (byte)Reactor.Math.MathHelper.Clamp(g * 255, Byte.MinValue, Byte.MaxValue);
            B = (byte)Reactor.Math.MathHelper.Clamp(b * 255, Byte.MinValue, Byte.MaxValue);
            A = (byte)Reactor.Math.MathHelper.Clamp(alpha * 255, Byte.MinValue, Byte.MaxValue);
        }

        /// <summary>
        /// Gets or sets the blue component of <see cref="RColor"/>.
        /// </summary>
        
        public byte B
        {
            get
            {
                unchecked
                {
                    return (byte) (this._packedValue >> 16);
                }
            }
            set
            {
                this._packedValue = (this._packedValue & 0xff00ffff) | ((uint)value << 16);
            }
        }

        /// <summary>
        /// Gets or sets the green component of <see cref="RColor"/>.
        /// </summary>
        
        public byte G
        {
            get
            {
                unchecked
                {
                    return (byte)(this._packedValue >> 8);
                }
            }
            set
            {
                this._packedValue = (this._packedValue & 0xffff00ff) | ((uint)value << 8);
            }
        }

        /// <summary>
        /// Gets or sets the red component of <see cref="RColor"/>.
        /// </summary>
        
        public byte R
        {
            get
            {
                unchecked
                {
                    return (byte) this._packedValue;
                }
            }
            set
            {
                this._packedValue = (this._packedValue & 0xffffff00) | value;
            }
        }

        /// <summary>
        /// Gets or sets the alpha component of <see cref="RColor"/>.
        /// </summary>
        
        public byte A
        {
            get
            {
                unchecked
                {
                    return (byte)(this._packedValue >> 24);
                }
            }
            set
            {
                this._packedValue = (this._packedValue & 0x00ffffff) | ((uint)value << 24);
            }
        }
		
	/// <summary>
        /// Compares whether two <see cref="RColor"/> instances are equal.
        /// </summary>
        /// <param name="a"><see cref="RColor"/> instance on the left of the equal sign.</param>
        /// <param name="b"><see cref="RColor"/> instance on the right of the equal sign.</param>
        /// <returns><c>true</c> if the instances are equal; <c>false</c> otherwise.</returns>
        public static bool operator ==(RColor a, RColor b)
        {
            return (a.A == b.A &&
                a.R == b.R &&
                a.G == b.G &&
                a.B == b.B);
        }
	
	/// <summary>
        /// Compares whether two <see cref="RColor"/> instances are not equal.
        /// </summary>
        /// <param name="a"><see cref="RColor"/> instance on the left of the not equal sign.</param>
        /// <param name="b"><see cref="RColor"/> instance on the right of the not equal sign.</param>
        /// <returns><c>true</c> if the instances are not equal; <c>false</c> otherwise.</returns>	
        public static bool operator !=(RColor a, RColor b)
        {
            return !(a == b);
        }

        /// <summary>
        /// Gets the hash code of this <see cref="RColor"/>.
        /// </summary>
        /// <returns>Hash code of this <see cref="RColor"/>.</returns>
        public override int GetHashCode()
        {
            return this._packedValue.GetHashCode();
        }
	
        /// <summary>
        /// Compares whether current instance is equal to specified object.
        /// </summary>
        /// <param name="obj">The <see cref="RColor"/> to compare.</param>
        /// <returns><c>true</c> if the instances are equal; <c>false</c> otherwise.</returns>
        public override bool Equals(object obj)
        {
            return ((obj is RColor) && this.Equals((RColor)obj));
        }

        #region RColor Bank
        /// <summary>
        /// TransparentBlack RColor (R:0,G:0,B:0,A:0).
        /// </summary>
        public static RColor TransparentBlack
        {
            get;
            private set;
        }
        
        /// <summary>
        /// Transparent RColor (R:0,G:0,B:0,A:0).
        /// </summary>
        public static RColor Transparent
        {
            get;
            private set;
        }
	
	/// <summary>
        /// AliceBlue RColor (R:240,G:248,B:255,A:255).
        /// </summary>
        public static RColor AliceBlue
        {
            get;
            private set;
        }
        
        /// <summary>
        /// AntiqueWhite RColor (R:250,G:235,B:215,A:255).
        /// </summary>
        public static RColor AntiqueWhite
        {
            get;
            private set;
        }
        
        /// <summary>
        /// Aqua RColor (R:0,G:255,B:255,A:255).
        /// </summary>
	public static RColor Aqua
        {
            get;
            private set;
        }
	
	/// <summary>
        /// Aquamarine RColor (R:127,G:255,B:212,A:255).
        /// </summary>
        public static RColor Aquamarine
    {
        get;
        private set;
    }
        
        /// <summary>
        /// Azure RColor (R:240,G:255,B:255,A:255).
        /// </summary>
	public static RColor Azure
        {
            get;
            private set;
        }
	
	/// <summary>
        /// Beige RColor (R:245,G:245,B:220,A:255).
        /// </summary>
        public static RColor Beige
    {
        get;
        private set;
    }
        
        /// <summary>
        /// Bisque RColor (R:255,G:228,B:196,A:255).
        /// </summary>
        public static RColor Bisque
        {
            get;
            private set;
        }
        
        /// <summary>
        /// Black RColor (R:0,G:0,B:0,A:255).
        /// </summary>
        public static RColor Black
        {
            get;
            private set;
        }
        
        /// <summary>
        /// BlanchedAlmond RColor (R:255,G:235,B:205,A:255).
        /// </summary>
        public static RColor BlanchedAlmond
        {
            get;
            private set;
        }
        
        /// <summary>
        /// Blue RColor (R:0,G:0,B:255,A:255).
        /// </summary>
        public static RColor Blue
        {
            get;
            private set;
        }
        
        /// <summary>
        /// BlueViolet RColor (R:138,G:43,B:226,A:255).
        /// </summary>
        public static RColor BlueViolet
        {
            get;
            private set;
        }
        
        /// <summary>
        /// Brown RColor (R:165,G:42,B:42,A:255).
        /// </summary>
        public static RColor Brown
        {
            get;
            private set;
        }
        
        /// <summary>
        /// BurlyWood RColor (R:222,G:184,B:135,A:255).
        /// </summary>
        public static RColor BurlyWood
        {
            get;
            private set;
        }
        
        /// <summary>
        /// CadetBlue RColor (R:95,G:158,B:160,A:255).
        /// </summary>
        public static RColor CadetBlue
        {
            get;
            private set;
        }
        
        /// <summary>
        /// Chartreuse RColor (R:127,G:255,B:0,A:255).
        /// </summary>
        public static RColor Chartreuse
        {
            get;
            private set;
        }
         
        /// <summary>
        /// Chocolate RColor (R:210,G:105,B:30,A:255).
        /// </summary>
        public static RColor Chocolate
        {
            get;
            private set;
        }
        
        /// <summary>
        /// Coral RColor (R:255,G:127,B:80,A:255).
        /// </summary>
        public static RColor Coral
        {
            get;
            private set;
        }
        
        /// <summary>
        /// CornflowerBlue RColor (R:100,G:149,B:237,A:255).
        /// </summary>
        public static RColor CornflowerBlue
        {
            get;
            private set;
        }
        
        /// <summary>
        /// Cornsilk RColor (R:255,G:248,B:220,A:255).
        /// </summary>
	public static RColor Cornsilk
        {
            get;
            private set;
        }
	
	/// <summary>
        /// Crimson RColor (R:220,G:20,B:60,A:255).
        /// </summary>
        public static RColor Crimson
    {
        get;
        private set;
    }
        
        /// <summary>
        /// Cyan RColor (R:0,G:255,B:255,A:255).
        /// </summary>
        public static RColor Cyan
        {
            get;
            private set;
        }
        
        /// <summary>
        /// DarkBlue RColor (R:0,G:0,B:139,A:255).
        /// </summary>
	public static RColor DarkBlue
        {
            get;
            private set;
        }
	
	/// <summary>
        /// DarkCyan RColor (R:0,G:139,B:139,A:255).
        /// </summary>
        public static RColor DarkCyan
    {
        get;
        private set;
    }
        
        /// <summary>
        /// DarkGoldenrod RColor (R:184,G:134,B:11,A:255).
        /// </summary>
        public static RColor DarkGoldenrod
        {
            get;
            private set;
        }
        
        /// <summary>
        /// DarkGray RColor (R:169,G:169,B:169,A:255).
        /// </summary>
	public static RColor DarkGray
        {
            get;
            private set;
        }
	
	/// <summary>
        /// DarkGreen RColor (R:0,G:100,B:0,A:255).
        /// </summary>
        public static RColor DarkGreen
    {
        get;
        private set;
    }
        
        /// <summary>
        /// DarkKhaki RColor (R:189,G:183,B:107,A:255).
        /// </summary>
        public static RColor DarkKhaki
        {
            get;
            private set;
        }

        /// <summary>
        /// DarkMagenta RColor (R:139,G:0,B:139,A:255).
        /// </summary>
        public static RColor DarkMagenta
        {
            get;
            private set;
        }

        /// <summary>
        /// DarkOliveGreen RColor (R:85,G:107,B:47,A:255).
        /// </summary>
        public static RColor DarkOliveGreen
        {
            get;
            private set;
        }

        /// <summary>
        /// DarkOrange RColor (R:255,G:140,B:0,A:255).
        /// </summary>
        public static RColor DarkOrange
        {
            get;
            private set;
        }

        /// <summary>
        /// DarkOrchid RColor (R:153,G:50,B:204,A:255).
        /// </summary>
        public static RColor DarkOrchid
        {
            get;
            private set;
        }

        /// <summary>
        /// DarkRed RColor (R:139,G:0,B:0,A:255).
        /// </summary>
        public static RColor DarkRed
        {
            get;
            private set;
        }
        
	/// <summary>
        /// DarkSalmon RColor (R:233,G:150,B:122,A:255).
        /// </summary>
        public static RColor DarkSalmon
        {
            get;
            private set;
        }

        /// <summary>
        /// DarkSeaGreen RColor (R:143,G:188,B:139,A:255).
        /// </summary>
        public static RColor DarkSeaGreen
        {
            get;
            private set;
        }

        /// <summary>
        /// DarkSlateBlue RColor (R:72,G:61,B:139,A:255).
        /// </summary>
        public static RColor DarkSlateBlue
        {
            get;
            private set;
        }

        /// <summary>
        /// DarkSlateGray RColor (R:47,G:79,B:79,A:255).
        /// </summary>
        public static RColor DarkSlateGray
        {
            get;
            private set;
        }

        /// <summary>
        /// DarkTurquoise RColor (R:0,G:206,B:209,A:255).
        /// </summary>
        public static RColor DarkTurquoise
        {
            get;
            private set;
        }

        /// <summary>
        /// DarkViolet RColor (R:148,G:0,B:211,A:255).
        /// </summary>
        public static RColor DarkViolet
        {
            get;
            private set;
        }
         
        /// <summary>
        /// DeepPink RColor (R:255,G:20,B:147,A:255).
        /// </summary>
        public static RColor DeepPink
        {
            get;
            private set;
        }

        /// <summary>
        /// DeepSkyBlue RColor (R:0,G:191,B:255,A:255).
        /// </summary>
        public static RColor DeepSkyBlue
        {
            get;
            private set;
        }

        /// <summary>
        /// DimGray RColor (R:105,G:105,B:105,A:255).
        /// </summary>
        public static RColor DimGray
        {
            get;
            private set;
        }

        /// <summary>
        /// DodgerBlue RColor (R:30,G:144,B:255,A:255).
        /// </summary>
        public static RColor DodgerBlue
        {
            get;
            private set;
        }

        /// <summary>
        /// Firebrick RColor (R:178,G:34,B:34,A:255).
        /// </summary>
        public static RColor Firebrick
        {
            get;
            private set;
        }

        /// <summary>
        /// FloralWhite RColor (R:255,G:250,B:240,A:255).
        /// </summary>
        public static RColor FloralWhite
        {
            get;
            private set;
        }

        /// <summary>
        /// ForestGreen RColor (R:34,G:139,B:34,A:255).
        /// </summary>
        public static RColor ForestGreen
        {
            get;
            private set;
        }
        
	/// <summary>
        /// Fuchsia RColor (R:255,G:0,B:255,A:255).
        /// </summary>
        public static RColor Fuchsia
        {
            get;
            private set;
        }

        /// <summary>
        /// Gainsboro RColor (R:220,G:220,B:220,A:255).
        /// </summary>
        public static RColor Gainsboro
        {
            get;
            private set;
        }

        /// <summary>
        /// GhostWhite RColor (R:248,G:248,B:255,A:255).
        /// </summary>
        public static RColor GhostWhite
        {
            get;
            private set;
        }
        /// <summary>
        /// Gold RColor (R:255,G:215,B:0,A:255).
        /// </summary>
        public static RColor Gold
        {
            get;
            private set;
        }

        /// <summary>
        /// Goldenrod RColor (R:218,G:165,B:32,A:255).
        /// </summary>
        public static RColor Goldenrod
        {
            get;
            private set;
        }

        /// <summary>
        /// Gray RColor (R:128,G:128,B:128,A:255).
        /// </summary>
        public static RColor Gray
        {
            get;
            private set;
        }

        /// <summary>
        /// Green RColor (R:0,G:128,B:0,A:255).
        /// </summary>
        public static RColor Green
        {
            get;
            private set;
        }

        /// <summary>
        /// GreenYellow RColor (R:173,G:255,B:47,A:255).
        /// </summary>
        public static RColor GreenYellow
        {
            get;
            private set;
        }

        /// <summary>
        /// Honeydew RColor (R:240,G:255,B:240,A:255).
        /// </summary>
        public static RColor Honeydew
        {
            get;
            private set;
        }

        /// <summary>
        /// HotPink RColor (R:255,G:105,B:180,A:255).
        /// </summary>
        public static RColor HotPink
        {
            get;
            private set;
        }
        
        /// <summary>
        /// IndianRed RColor (R:205,G:92,B:92,A:255).
        /// </summary>
        public static RColor IndianRed
        {
            get;
            private set;
        }
        
        /// <summary>
        /// Indigo RColor (R:75,G:0,B:130,A:255).
        /// </summary>
        public static RColor Indigo
        {
            get;
            private set;
        }
        
        /// <summary>
        /// Ivory RColor (R:255,G:255,B:240,A:255).
        /// </summary>
        public static RColor Ivory
        {
            get;
            private set;
        }
        
        /// <summary>
        /// Khaki RColor (R:240,G:230,B:140,A:255).
        /// </summary>
        public static RColor Khaki
        {
            get;
            private set;
        }
        
        /// <summary>
        /// Lavender RColor (R:230,G:230,B:250,A:255).
        /// </summary>
        public static RColor Lavender
        {
            get;
            private set;
        }
        
        /// <summary>
        /// LavenderBlush RColor (R:255,G:240,B:245,A:255).
        /// </summary>
        public static RColor LavenderBlush
        {
            get;
            private set;
        }
        
        /// <summary>
        /// LawnGreen RColor (R:124,G:252,B:0,A:255).
        /// </summary>
        public static RColor LawnGreen
        {
            get;
            private set;
        }

        /// <summary>
        /// LemonChiffon RColor (R:255,G:250,B:205,A:255).
        /// </summary>
        public static RColor LemonChiffon
        {
            get;
            private set;
        }

        /// <summary>
        /// LightBlue RColor (R:173,G:216,B:230,A:255).
        /// </summary>
        public static RColor LightBlue
        {
            get;
            private set;
        }

        /// <summary>
        /// LightCoral RColor (R:240,G:128,B:128,A:255).
        /// </summary>
        public static RColor LightCoral
        {
            get;
            private set;
        }
        
        /// <summary>
        /// LightCyan RColor (R:224,G:255,B:255,A:255).
        /// </summary>
        public static RColor LightCyan
        {
            get;
            private set;
        }

        /// <summary>
        /// LightGoldenrodYellow RColor (R:250,G:250,B:210,A:255).
        /// </summary>
        public static RColor LightGoldenrodYellow
        {
            get;
            private set;
        }
        
        /// <summary>
        /// LightGray RColor (R:211,G:211,B:211,A:255).
        /// </summary>
        public static RColor LightGray
        {
            get;
            private set;
        }

        /// <summary>
        /// LightGreen RColor (R:144,G:238,B:144,A:255).
        /// </summary>
        public static RColor LightGreen
        {
            get;
            private set;
        }

        /// <summary>
        /// LightPink RColor (R:255,G:182,B:193,A:255).
        /// </summary>
        public static RColor LightPink
        {
            get;
            private set;
        }

        /// <summary>
        /// LightSalmon RColor (R:255,G:160,B:122,A:255).
        /// </summary>
        public static RColor LightSalmon
        {
            get;
            private set;
        }

        /// <summary>
        /// LightSeaGreen RColor (R:32,G:178,B:170,A:255).
        /// </summary>
        public static RColor LightSeaGreen
        {
            get;
            private set;
        }

        /// <summary>
        /// LightSkyBlue RColor (R:135,G:206,B:250,A:255).
        /// </summary>
        public static RColor LightSkyBlue
        {
            get;
            private set;
        }

        /// <summary>
        /// LightSlateGray RColor (R:119,G:136,B:153,A:255).
        /// </summary>
        public static RColor LightSlateGray
        {
            get;
            private set;
        }

        /// <summary>
        /// LightSteelBlue RColor (R:176,G:196,B:222,A:255).
        /// </summary>
        public static RColor LightSteelBlue
        {
            get;
            private set;
        }

        /// <summary>
        /// LightYellow RColor (R:255,G:255,B:224,A:255).
        /// </summary>
        public static RColor LightYellow
        {
            get;
            private set;
        }

        /// <summary>
        /// Lime RColor (R:0,G:255,B:0,A:255).
        /// </summary>
        public static RColor Lime
        {
            get;
            private set;
        }

        /// <summary>
        /// LimeGreen RColor (R:50,G:205,B:50,A:255).
        /// </summary>
        public static RColor LimeGreen
        {
            get;
            private set;
        }

        /// <summary>
        /// Linen RColor (R:250,G:240,B:230,A:255).
        /// </summary>
        public static RColor Linen
        {
            get;
            private set;
        }

        /// <summary>
        /// Magenta RColor (R:255,G:0,B:255,A:255).
        /// </summary>
        public static RColor Magenta
        {
            get;
            private set;
        }

        /// <summary>
        /// Maroon RColor (R:128,G:0,B:0,A:255).
        /// </summary>
        public static RColor Maroon
        {
            get;
            private set;
        }

        /// <summary>
        /// MediumAquamarine RColor (R:102,G:205,B:170,A:255).
        /// </summary>
        public static RColor MediumAquamarine
        {
            get;
            private set;
        }

        /// <summary>
        /// MediumBlue RColor (R:0,G:0,B:205,A:255).
        /// </summary>
        public static RColor MediumBlue
        {
            get;
            private set;
        }

        /// <summary>
        /// MediumOrchid RColor (R:186,G:85,B:211,A:255).
        /// </summary>
        public static RColor MediumOrchid
        {
            get;
            private set;
        }

        /// <summary>
        /// MediumPurple RColor (R:147,G:112,B:219,A:255).
        /// </summary>
        public static RColor MediumPurple
        {
            get;
            private set;
        }

        /// <summary>
        /// MediumSeaGreen RColor (R:60,G:179,B:113,A:255).
        /// </summary>
        public static RColor MediumSeaGreen
        {
            get;
            private set;
        }

        /// <summary>
        /// MediumSlateBlue RColor (R:123,G:104,B:238,A:255).
        /// </summary>
        public static RColor MediumSlateBlue
        {
            get;
            private set;
        }

        /// <summary>
        /// MediumSpringGreen RColor (R:0,G:250,B:154,A:255).
        /// </summary>
        public static RColor MediumSpringGreen
        {
            get;
            private set;
        }

        /// <summary>
        /// MediumTurquoise RColor (R:72,G:209,B:204,A:255).
        /// </summary>
        public static RColor MediumTurquoise
        {
            get;
            private set;
        }

        /// <summary>
        /// MediumVioletRed RColor (R:199,G:21,B:133,A:255).
        /// </summary>
        public static RColor MediumVioletRed
        {
            get;
            private set;
        }

        /// <summary>
        /// MidnightBlue RColor (R:25,G:25,B:112,A:255).
        /// </summary>
        public static RColor MidnightBlue
        {
            get;
            private set;
        }

        /// <summary>
        /// MintCream RColor (R:245,G:255,B:250,A:255).
        /// </summary>
        public static RColor MintCream
        {
            get;
            private set;
        }

        /// <summary>
        /// MistyRose RColor (R:255,G:228,B:225,A:255).
        /// </summary>
        public static RColor MistyRose
        {
            get;
            private set;
        }

        /// <summary>
        /// Moccasin RColor (R:255,G:228,B:181,A:255).
        /// </summary>
        public static RColor Moccasin
        {
            get;
            private set;
        }

        /// <summary>
        /// NavajoWhite RColor (R:255,G:222,B:173,A:255).
        /// </summary>
        public static RColor NavajoWhite
        {
            get;
            private set;
        }

        /// <summary>
        /// Navy RColor (R:0,G:0,B:128,A:255).
        /// </summary>
        public static RColor Navy
        {
            get;
            private set;
        }

        /// <summary>
        /// OldLace RColor (R:253,G:245,B:230,A:255).
        /// </summary>
        public static RColor OldLace
        {
            get;
            private set;
        }

        /// <summary>
        /// Olive RColor (R:128,G:128,B:0,A:255).
        /// </summary>
        public static RColor Olive
        {
            get;
            private set;
        }

        /// <summary>
        /// OliveDrab RColor (R:107,G:142,B:35,A:255).
        /// </summary>
        public static RColor OliveDrab
        {
            get;
            private set;
        }

        /// <summary>
        /// Orange RColor (R:255,G:165,B:0,A:255).
        /// </summary>
        public static RColor Orange
        {
            get;
            private set;
        }

        /// <summary>
        /// OrangeRed RColor (R:255,G:69,B:0,A:255).
        /// </summary>
        public static RColor OrangeRed
        {
            get;
            private set;
        }

        /// <summary>
        /// Orchid RColor (R:218,G:112,B:214,A:255).
        /// </summary>
        public static RColor Orchid
        {
            get;
            private set;
        }

        /// <summary>
        /// PaleGoldenrod RColor (R:238,G:232,B:170,A:255).
        /// </summary>
        public static RColor PaleGoldenrod
        {
            get;
            private set;
        }

        /// <summary>
        /// PaleGreen RColor (R:152,G:251,B:152,A:255).
        /// </summary>
        public static RColor PaleGreen
        {
            get;
            private set;
        }

        /// <summary>
        /// PaleTurquoise RColor (R:175,G:238,B:238,A:255).
        /// </summary>
        public static RColor PaleTurquoise
        {
            get;
            private set;
        }
        /// <summary>
        /// PaleVioletRed RColor (R:219,G:112,B:147,A:255).
        /// </summary>
        public static RColor PaleVioletRed
        {
            get;
            private set;
        }

        /// <summary>
        /// PapayaWhip RColor (R:255,G:239,B:213,A:255).
        /// </summary>
        public static RColor PapayaWhip
        {
            get;
            private set;
        }

        /// <summary>
        /// PeachPuff RColor (R:255,G:218,B:185,A:255).
        /// </summary>
        public static RColor PeachPuff
        {
            get;
            private set;
        }

        /// <summary>
        /// Peru RColor (R:205,G:133,B:63,A:255).
        /// </summary>
        public static RColor Peru
        {
            get;
            private set;
        }

        /// <summary>
        /// Pink RColor (R:255,G:192,B:203,A:255).
        /// </summary>
        public static RColor Pink
        {
            get;
            private set;
        }

        /// <summary>
        /// Plum RColor (R:221,G:160,B:221,A:255).
        /// </summary>
        public static RColor Plum
        {
            get;
            private set;
        }

        /// <summary>
        /// PowderBlue RColor (R:176,G:224,B:230,A:255).
        /// </summary>
        public static RColor PowderBlue
        {
            get;
            private set;
        }

        /// <summary>
        ///  Purple RColor (R:128,G:0,B:128,A:255).
        /// </summary>
        public static RColor Purple
        {
            get;
            private set;
        }

        /// <summary>
        /// Red RColor (R:255,G:0,B:0,A:255).
        /// </summary>
        public static RColor Red
        {
            get;
            private set;
        }

        /// <summary>
        /// RosyBrown RColor (R:188,G:143,B:143,A:255).
        /// </summary>
        public static RColor RosyBrown
        {
            get;
            private set;
        }

        /// <summary>
        /// RoyalBlue RColor (R:65,G:105,B:225,A:255).
        /// </summary>
        public static RColor RoyalBlue
        {
            get;
            private set;
        }

    	/// <summary>
        /// SaddleBrown RColor (R:139,G:69,B:19,A:255).
        /// </summary>
        public static RColor SaddleBrown
        {
            get;
            private set;
        }
    	 
        /// <summary>
        /// Salmon RColor (R:250,G:128,B:114,A:255).
        /// </summary>
        public static RColor Salmon
        {
            get;
            private set;
        }
        
        /// <summary>
        /// SandyBrown RColor (R:244,G:164,B:96,A:255).
        /// </summary>
        public static RColor SandyBrown
        {
            get;
            private set;
        }
        
        /// <summary>
        /// SeaGreen RColor (R:46,G:139,B:87,A:255).
        /// </summary>
        public static RColor SeaGreen
        {
            get;
            private set;
        }
        
    	/// <summary>
        /// SeaShell RColor (R:255,G:245,B:238,A:255).
        /// </summary>
        public static RColor SeaShell
        {
            get;
            private set;
        }
        
    	/// <summary>
        /// Sienna RColor (R:160,G:82,B:45,A:255).
        /// </summary>
        public static RColor Sienna
        {
            get;
            private set;
        }
        
    	/// <summary>
        /// Silver RColor (R:192,G:192,B:192,A:255).
        /// </summary>
        public static RColor Silver
        {
            get;
            private set;
        }
        
       /// <summary>
       /// SkyBlue RColor (R:135,G:206,B:235,A:255).
       /// </summary>
       public static RColor SkyBlue
        {
            get;
            private set;
        }
       
        /// <summary>
        /// SlateBlue RColor (R:106,G:90,B:205,A:255).
        /// </summary>
        public static RColor SlateBlue
       {
           get;
           private set;
       }
      
        /// <summary>
        /// SlateGray RColor (R:112,G:128,B:144,A:255).
        /// </summary>
        public static RColor SlateGray
        {
            get;
            private set;
        }
      
        /// <summary>
        /// Snow RColor (R:255,G:250,B:250,A:255).
        /// </summary>
        public static RColor Snow
        {
            get;
            private set;
        }
      
        /// <summary>
        /// SpringGreen RColor (R:0,G:255,B:127,A:255).
        /// </summary>
        public static RColor SpringGreen
        {
            get;
            private set;
        }
      
        /// <summary>
        /// SteelBlue RColor (R:70,G:130,B:180,A:255).
        /// </summary>
        public static RColor SteelBlue
        {
            get;
            private set;
        }
      
        /// <summary>
        /// Tan RColor (R:210,G:180,B:140,A:255).
        /// </summary>
        public static RColor Tan
        {
            get;
            private set;
        }
       
        /// <summary>
        /// Teal RColor (R:0,G:128,B:128,A:255).
        /// </summary>
        public static RColor Teal
        {
            get;
            private set;
        }
       
        /// <summary>
        /// Thistle RColor (R:216,G:191,B:216,A:255).
        /// </summary>
        public static RColor Thistle
        {
            get;
            private set;
        }
       
        /// <summary>
        /// Tomato RColor (R:255,G:99,B:71,A:255).
        /// </summary>
        public static RColor Tomato
        {
            get;
            private set;
        }
        
    	/// <summary>
        /// Turquoise RColor (R:64,G:224,B:208,A:255).
        /// </summary>
        public static RColor Turquoise
        {
            get;
            private set;
        }
        
        /// <summary>
        /// Violet RColor (R:238,G:130,B:238,A:255).
        /// </summary>
        public static RColor Violet
        {
            get;
            private set;
        }
        
        /// <summary>
        /// Wheat RColor (R:245,G:222,B:179,A:255).
        /// </summary>
	public static RColor Wheat
        {
            get;
            private set;
        }
	
        /// <summary>
        /// White RColor (R:255,G:255,B:255,A:255).
        /// </summary>
        public static RColor White
    {
        get;
        private set;
    }
       
        /// <summary>
        /// WhiteSmoke RColor (R:245,G:245,B:245,A:255).
        /// </summary>
        public static RColor WhiteSmoke
        {
            get;
            private set;
        }
        
        /// <summary>
        /// Yellow RColor (R:255,G:255,B:0,A:255).
        /// </summary>
        public static RColor Yellow
        {
            get;
            private set;
        }
        
        /// <summary>
        /// YellowGreen RColor (R:154,G:205,B:50,A:255).
        /// </summary>
        public static RColor YellowGreen
        {
            get;
            private set;
        }
        #endregion

        /// <summary>
        /// Performs linear interpolation of <see cref="RColor"/>.
        /// </summary>
        /// <param name="value1">Source <see cref="RColor"/>.</param>
        /// <param name="value2">Destination <see cref="RColor"/>.</param>
        /// <param name="amount">Interpolation factor.</param>
        /// <returns>Interpolated <see cref="RColor"/>.</returns>
        public static RColor Lerp(RColor value1, RColor value2, Single amount)
        {
            amount = Reactor.Math.MathHelper.Clamp(amount, 0, 1);
            return new RColor(   
                (int)Reactor.Math.MathHelper.Lerp(value1.R, value2.R, amount),
                (int)Reactor.Math.MathHelper.Lerp(value1.G, value2.G, amount),
                (int)Reactor.Math.MathHelper.Lerp(value1.B, value2.B, amount),
                (int)Reactor.Math.MathHelper.Lerp(value1.A, value2.A, amount) );
        }
		
	/// <summary>
        /// Multiply <see cref="RColor"/> by value.
        /// </summary>
        /// <param name="value">Source <see cref="RColor"/>.</param>
        /// <param name="scale">Multiplicator.</param>
        /// <returns>Multiplication result.</returns>
	public static RColor Multiply(RColor value, float scale)
	{
	    return new RColor((int)(value.R * scale), (int)(value.G * scale), (int)(value.B * scale), (int)(value.A * scale));
	}
	
	/// <summary>
        /// Multiply <see cref="RColor"/> by value.
        /// </summary>
        /// <param name="value">Source <see cref="RColor"/>.</param>
        /// <param name="scale">Multiplicator.</param>
        /// <returns>Multiplication result.</returns>
	public static RColor operator *(RColor value, float scale)
        {
            return new RColor((int)(value.R * scale), (int)(value.G * scale), (int)(value.B * scale), (int)(value.A * scale));
        }

    /// <summary>
    /// Gets a three-component <see cref="Vector3"/> representation for this object.
    /// </summary>
    /// <returns>A three-component <see cref="Vector3"/> representation for this object.</returns>
        public Vector3 ToVector3()
        {
            return new Vector3(R / 255.0f, G / 255.0f, B / 255.0f);
        }

        /// <summary>
        /// Gets a four-component <see cref="Vector4"/> representation for this object.
        /// </summary>
        /// <returns>A four-component <see cref="Vector4"/> representation for this object.</returns>
        public Vector4 ToVector4()
        {
            return new Vector4(R / 255.0f, G / 255.0f, B / 255.0f, A / 255.0f);
        }
	
	/// <summary>
        /// Gets or sets packed value of this <see cref="RColor"/>.
        /// </summary>
        [CLSCompliant(false)]
        public UInt32 PackedValue
        {
            get { return _packedValue; }
            set { _packedValue = value; }
        }


        internal string DebugDisplayString
        {
            get
            {
                return string.Concat(
                    this.R.ToString(), "  ",
                    this.G.ToString(), "  ",
                    this.B.ToString(), "  ",
                    this.A.ToString()
                );
            }
        }


        /// <summary>
        /// Returns a <see cref="String"/> representation of this <see cref="RColor"/> in the format:
        /// {R:[red] G:[green] B:[blue] A:[alpha]}
        /// </summary>
        /// <returns><see cref="String"/> representation of this <see cref="RColor"/>.</returns>
	public override string ToString ()
	{
        StringBuilder sb = new StringBuilder(25);
        sb.Append("{R:");
        sb.Append(R);
        sb.Append(" G:");
        sb.Append(G);
        sb.Append(" B:");
        sb.Append(B);
        sb.Append(" A:");
        sb.Append(A);
        sb.Append("}");
        return sb.ToString();
	}
	
	/// <summary>
        /// Translate a non-premultipled alpha <see cref="RColor"/> to a <see cref="RColor"/> that contains premultiplied alpha.
        /// </summary>
        /// <param name="vector">A <see cref="Vector4"/> representing RColor.</param>
        /// <returns>A <see cref="RColor"/> which contains premultiplied alpha data.</returns>
        public static RColor FromNonPremultiplied(Vector4 vector)
        {
            return new RColor(vector.X * vector.W, vector.Y * vector.W, vector.Z * vector.W, vector.W);
        }
	
	/// <summary>
        /// Translate a non-premultipled alpha <see cref="RColor"/> to a <see cref="RColor"/> that contains premultiplied alpha.
        /// </summary>
        /// <param name="r">Red component value.</param>
        /// <param name="g">Green component value.</param>
        /// <param name="b">Blue component value.</param>
        /// <param name="a">Alpha component value.</param>
        /// <returns>A <see cref="RColor"/> which contains premultiplied alpha data.</returns>
        public static RColor FromNonPremultiplied(int r, int g, int b, int a)
        {
            return new RColor((byte)(r * a / 255),(byte)(g * a / 255), (byte)(b * a / 255), a);
        }

        #region IEquatable<RColor> Members
	
	/// <summary>
        /// Compares whether current instance is equal to specified <see cref="RColor"/>.
        /// </summary>
        /// <param name="other">The <see cref="RColor"/> to compare.</param>
        /// <returns><c>true</c> if the instances are equal; <c>false</c> otherwise.</returns>
        public bool Equals(RColor other)
        {
	    return this.PackedValue == other.PackedValue;
        }

        #endregion
    }
}
