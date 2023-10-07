using System;
using System.Drawing;

// AngelCode bitmap font parsing using C#
// https://www.cyotek.com/blog/angelcode-bitmap-font-parsing-using-csharp

// Copyright © 2012-2020 Cyotek Ltd.

// This work is licensed under the MIT License.
// See LICENSE.TXT for the full text

// Found this code useful?
// https://www.paypal.me/cyotek

namespace Reactor.Fonts
{
  /// <summary>
  ///     Represents the definition of a single character in a <see cref="BitmapFont" />
  /// </summary>
  public struct Character
    {
        #region Public Fields

        /// <summary>
        ///     Gets a <see cref="Character" /> structure that has a Height and Width value of 0.
        /// </summary>
        public static readonly Character Empty = new Character();

        #endregion Public Fields

        #region Private Fields

        #endregion Private Fields

        #region Public Constructors

        /// <summary> Constructor. </summary>
        /// <param name="character">  The character. </param>
        /// <param name="x">  The X-coordinate. </param>
        /// <param name="y">  The Y-coordinate. </param>
        /// <param name="width">  The character width. </param>
        /// <param name="height"> The character height. </param>
        /// <param name="xOffset">  The X offset. </param>
        /// <param name="yOffset">  The Y offset. </param>
        /// <param name="xAdvance">
        ///     How much the current position should be advanced after drawing the
        ///     character.
        /// </param>
        /// <param name="texturePage">  The texture page where the character image is found. </param>
        /// <param name="channel">  The texture channel where the character image is found. </param>
        public Character(char character, int x, int y, int width, int height, int xOffset, int yOffset, int xAdvance,
            int texturePage, int channel)
        {
            Char = character;
            X = x;
            Y = y;
            Width = width;
            Height = height;
            XAdvance = xAdvance;
            TexturePage = texturePage;
            Channel = channel;
            XOffset = xOffset;
            YOffset = yOffset;
        }

        #endregion Public Constructors

        #region Public Properties

        /// <summary>
        ///     Gets or sets the bounds of the character image in the source texture.
        /// </summary>
        /// <value>
        ///     The bounds of the character image in the source texture.
        /// </value>
        [Obsolete(
            "This property will be removed in a future update to the library. Please use the X, Y, Width and Height properties instead.")]
        public Rectangle Bounds
        {
            get => new Rectangle(X, Y, Width, Height);
            set
            {
                X = value.X;
                Y = value.Y;
                Width = value.Width;
                Height = value.Height;
            }
        }

        /// <summary>
        ///     Gets or sets the texture channel where the character image is found.
        /// </summary>
        /// <value>
        ///     The texture channel where the character image is found.
        /// </value>
        /// <remarks>
        ///     1 = blue, 2 = green, 4 = red, 8 = alpha, 15 = all channels
        /// </remarks>
        public int Channel { get; set; }

        /// <summary>
        ///     Gets or sets the character.
        /// </summary>
        /// <value>
        ///     The character.
        /// </value>
        public char Char { get; set; }

        /// <summary> Gets or sets the character height. </summary>
        /// <value> The height. </value>
        public int Height { get; set; }

        /// <summary>
        ///     Tests whether this <see cref="Character" /> structure has width and height of 0.
        /// </summary>
        /// <value>
        ///     This property returns <c>true</c> when this <see cref="Character" /> structure has both a width
        ///     and height of 0; otherwise, <c>false</c>.
        /// </value>
        public bool IsEmpty => Width == 0 && Height == 0;

        /// <summary>
        ///     Gets or sets the offset when copying the image from the texture to the screen.
        /// </summary>
        /// <value>
        ///     The offset when copying the image from the texture to the screen.
        /// </value>
        [Obsolete(
            "This property will be removed in a future update to the library. Please use the XOffset and YOffset properties instead.")]
        public Point Offset
        {
            get => new Point(XOffset, YOffset);
            set
            {
                XOffset = value.X;
                YOffset = value.Y;
            }
        }

        /// <summary>
        ///     Gets or sets the texture page where the character image is found.
        /// </summary>
        /// <value>
        ///     The texture page where the character image is found.
        /// </value>
        public int TexturePage { get; set; }

        /// <summary> Gets or sets the character width. </summary>
        /// <value> The width. </value>
        public int Width { get; set; }

        /// <summary> Gets or sets the X-coordinate. </summary>
        /// <value> The X-coordinate. </value>
        public int X { get; set; }

        /// <summary>
        ///     Gets or sets the value used to advance the current position after drawing the character.
        /// </summary>
        /// <value>
        ///     How much the current position should be advanced after drawing the character.
        /// </value>
        public int XAdvance { get; set; }

        /// <summary> Gets or sets the X offset. </summary>
        /// <value> The X-coordinate offset. </value>
        public int XOffset { get; set; }

        /// <summary> Gets or sets the Y-coordinate. </summary>
        /// <value> The Y-coordinate. </value>
        public int Y { get; set; }

        /// <summary> Gets or sets the Y offset. </summary>
        /// <value> The Y-coordinate offset. </value>
        public int YOffset { get; set; }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        ///     Returns the fully qualified type name of this instance.
        /// </summary>
        /// <returns>
        ///     A <see cref="T:System.String" /> containing a fully qualified type name.
        /// </returns>
        /// <seealso cref="M:System.ValueType.ToString()" />
        public override string ToString()
        {
            return Char.ToString();
        }

        #endregion Public Methods
    }
}