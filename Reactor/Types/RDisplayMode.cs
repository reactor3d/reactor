using Reactor.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Reactor.Types
{
    public class RDisplayMode
    {
        #region Fields

        //private RSurfaceFormat format;
        private int height;
        private int refreshRate;
        private int width;

        #endregion Fields

        #region Properties

        public float AspectRatio
        {
            get { return (float)width / (float)height; }
        }

        /*public RSurfaceFormat Format
        {
            get { return format; }
        }*/

        public int Height
        {
            get { return this.height; }
        }

        public int RefreshRate
        {
            get { return this.refreshRate; }
        }

        public int Width
        {
            get { return this.width; }
        }

        public Rectangle TitleSafeArea
        {
            get { return new Rectangle(0, 0, Width, Height); }
        }

        #endregion Properties

        #region Constructors

        public RDisplayMode(int width, int height, int refreshRate)
        {
            this.width = width;
            this.height = height;
            this.refreshRate = refreshRate;
            //this.format = format;
        }

        #endregion Constructors

        #region Operators

        public static bool operator !=(RDisplayMode left, RDisplayMode right)
        {
            return !(left == right);
        }

        public static bool operator ==(RDisplayMode left, RDisplayMode right)
        {
            if (ReferenceEquals(left, right)) //Same object or both are null
            {
                return true;
            }
            if (ReferenceEquals(left, null) || ReferenceEquals(right, null))
            {
                return false;
            }
            return
                (left.height == right.height) &&
                (left.refreshRate == right.refreshRate) &&
                (left.width == right.width);
        }

        #endregion Operators

        #region Public Methods

        public override bool Equals(object obj)
        {
            return obj is RDisplayMode && this == (RDisplayMode)obj;
        }

        public override int GetHashCode()
        {
            return (this.width.GetHashCode() ^ this.height.GetHashCode() ^ this.refreshRate.GetHashCode());
        }

        public override string ToString()
        {
            return "{{Width:" + this.width + " Height:" + this.height + " RefreshRate:" + this.refreshRate + "}}";
        }

        #endregion Public Methods
    }
}
