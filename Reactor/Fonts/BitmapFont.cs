using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;


// AngelCode bitmap font parsing using C#
// https://www.cyotek.com/blog/angelcode-bitmap-font-parsing-using-csharp

// Copyright © 2012-2021 Cyotek Ltd.

// This work is licensed under the MIT License.
// See LICENSE.TXT for the full text

// Found this code useful?
// https://www.paypal.me/cyotek

// Some documentation derived from the BMFont file format specification
// http://www.angelcode.com/products/bmfont/doc/file_format.html

namespace Reactor.Fonts
{
  /// <summary>
  ///     A bitmap font.
  /// </summary>
  /// <seealso cref="T:System.Collections.Generic.IEnumerable{Cyotek.Drawing.BitmapFont.Character}" />
  public class BitmapFont : IEnumerable<Character>
    {
        #region Public Fields

        /// <summary>
        ///     When used with <see cref="MeasureFont(string,double)" />, specifies that no wrapping should occur.
        /// </summary>
        public const int NoMaxWidth = -1;

        #endregion Public Fields

        #region Public Constructors

        /// <summary> Default constructor. </summary>
        public BitmapFont()
        {
            InvalidChar = Character.Empty;
            Pages = new Page[0];
            Kernings = new Dictionary<Kerning, int>();
            Characters = new Dictionary<char, Character>();
        }

        #endregion Public Constructors

        #region Public Indexers

        /// <summary>
        ///     Indexer to get items within this collection using array index syntax.
        /// </summary>
        /// <param name="character">The character.</param>
        /// <returns>
        ///     The indexed item.
        /// </returns>
        public Character this[char character] => Characters.TryGetValue(character, out var value) ? value : InvalidChar;

        #endregion Public Indexers

        #region Private Fields

        #endregion Private Fields

        #region Public Properties

        /// <summary>
        ///     Gets or sets the alpha channel.
        /// </summary>
        /// <value>
        ///     The alpha channel.
        /// </value>
        /// <remarks>
        ///     Set to 0 if the channel holds the glyph data, 1 if it holds the outline, 2 if it holds the glyph and the
        ///     outline, 3 if its set to zero, and 4 if its set to one.
        /// </remarks>
        public int AlphaChannel { get; set; }

        /// <summary>
        ///     Gets or sets the number of pixels from the absolute top of the line to the base of the characters.
        /// </summary>
        /// <value>
        ///     The number of pixels from the absolute top of the line to the base of the characters.
        /// </value>
        public int BaseHeight { get; set; }

        /// <summary>
        ///     Gets or sets the blue channel.
        /// </summary>
        /// <value>
        ///     The blue channel.
        /// </value>
        /// <remarks>
        ///     Set to 0 if the channel holds the glyph data, 1 if it holds the outline, 2 if it holds the glyph and the
        ///     outline, 3 if its set to zero, and 4 if its set to one.
        /// </remarks>
        public int BlueChannel { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether the font is bold.
        /// </summary>
        /// <value>
        ///     <c>true</c> if the font is bold, otherwise <c>false</c>.
        /// </value>
        public bool Bold { get; set; }

        /// <summary>
        ///     Gets or sets the characters that comprise the font.
        /// </summary>
        /// <value>
        ///     The characters that comprise the font.
        /// </value>
        public IDictionary<char, Character> Characters { get; set; }

        /// <summary>
        ///     Gets or sets the name of the OEM charset used.
        /// </summary>
        /// <value>
        ///     The name of the OEM charset used (when not unicode).
        /// </value>
        public string Charset { get; set; }

        /// <summary>
        ///     Gets or sets the name of the true type font.
        /// </summary>
        /// <value>
        ///     The font family name.
        /// </value>
        public string FamilyName { get; set; }

        /// <summary>
        ///     Gets or sets the size of the font.
        /// </summary>
        /// <value>
        ///     The size of the font.
        /// </value>
        public int FontSize { get; set; }

        /// <summary>
        ///     Gets or sets the green channel.
        /// </summary>
        /// <value>
        ///     The green channel.
        /// </value>
        /// <remarks>
        ///     Set to 0 if the channel holds the glyph data, 1 if it holds the outline, 2 if it holds the glyph and the
        ///     outline, 3 if its set to zero, and 4 if its set to one.
        /// </remarks>
        public int GreenChannel { get; set; }

        /// <summary> Gets the fallback character used when a matching character cannot be found. </summary>
        /// <value> The fallback character. </value>
        public Character InvalidChar { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether the font is italic.
        /// </summary>
        /// <value>
        ///     <c>true</c> if the font is italic, otherwise <c>false</c>.
        /// </value>
        public bool Italic { get; set; }

        /// <summary>
        ///     Gets or sets the character kernings for the font.
        /// </summary>
        /// <value>
        ///     The character kernings for the font.
        /// </value>
        public IDictionary<Kerning, int> Kernings { get; set; }

        /// <summary>
        ///     Gets or sets the distance in pixels between each line of text.
        /// </summary>
        /// <value>
        ///     The distance in pixels between each line of text.
        /// </value>
        public int LineHeight { get; set; }

        /// <summary>
        ///     Gets or sets the outline thickness for the characters.
        /// </summary>
        /// <value>
        ///     The outline thickness for the characters.
        /// </value>
        public int OutlineSize { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether the monochrome characters have been packed into each of the texture
        ///     channels.
        /// </summary>
        /// <value>
        ///     <c>true</c> if the characters are packed, otherwise <c>false</c>.
        /// </value>
        /// <remarks>
        ///     When packed, the <see cref="AlphaChannel" /> property describes what is stored in each channel.
        /// </remarks>
        public bool Packed { get; set; }

        /// <summary>
        ///     Gets or sets the padding for each character.
        /// </summary>
        /// <value>
        ///     The padding for each character.
        /// </value>
        public Padding Padding { get; set; }

        /// <summary>
        ///     Gets or sets the texture pages for the font.
        /// </summary>
        /// <value>
        ///     The pages.
        /// </value>
        public Page[] Pages { get; set; }

        /// <summary>
        ///     Gets or sets the red channel.
        /// </summary>
        /// <value>
        ///     The red channel.
        /// </value>
        /// <remarks>
        ///     Set to 0 if the channel holds the glyph data, 1 if it holds the outline, 2 if it holds the glyph and the
        ///     outline, 3 if its set to zero, and 4 if its set to one.
        /// </remarks>
        public int RedChannel { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether the font is smoothed.
        /// </summary>
        /// <value>
        ///     <c>true</c> if the font is smoothed, otherwise <c>false</c>.
        /// </value>
        public bool Smoothed { get; set; }

        /// <summary>
        ///     Gets or sets the spacing for each character.
        /// </summary>
        /// <value>
        ///     The spacing for each character.
        /// </value>
        public Point Spacing { get; set; }

        /// <summary>
        ///     Gets or sets the font height stretch.
        /// </summary>
        /// <value>
        ///     The font height stretch.
        /// </value>
        /// <remarks>100% means no stretch.</remarks>
        public int StretchedHeight { get; set; }

        /// <summary>
        ///     Gets or sets the level of super sampling used by the font.
        /// </summary>
        /// <value>
        ///     The super sampling level of the font.
        /// </value>
        /// <remarks>A value of 1 indicates no super sampling is in use.</remarks>
        public int SuperSampling { get; set; }

        /// <summary>
        ///     Gets or sets the size of the texture images used by the font.
        /// </summary>
        /// <value>
        ///     The size of the texture.
        /// </value>
        public Size TextureSize { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether the font is Unicode.
        /// </summary>
        /// <value>
        ///     <c>true</c> if the font is Unicode, otherwise <c>false</c>.
        /// </value>
        public bool Unicode { get; set; }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        ///     Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        ///     A <see cref="T:System.Collections.Generic.IEnumerator`1" /> that can be used to iterate through
        ///     the collection.
        /// </returns>
        /// <seealso cref="M:System.Collections.Generic.IEnumerable{Cyotek.Drawing.BitmapFont.Character}.GetEnumerator()" />
        public IEnumerator<Character> GetEnumerator()
        {
            foreach (var pair in Characters) yield return pair.Value;
        }

        /// <summary>
        ///     Gets the kerning for the specified character combination.
        /// </summary>
        /// <param name="previous">The previous character.</param>
        /// <param name="current">The current character.</param>
        /// <returns>
        ///     The spacing between the specified characters.
        /// </returns>
        public int GetKerning(char previous, char current)
        {
            Kerning key;

            key = new Kerning(previous, current, 0);

            if (!Kernings.TryGetValue(key, out var result)) result = 0;

            return result;
        }


        /// <summary>
        ///     Provides the size, in pixels, of the specified text when drawn with this font.
        /// </summary>
        /// <param name="text">The text to measure.</param>
        /// <returns>
        ///     The <see cref="Size" />, in pixels, of <paramref name="text" /> drawn with this font.
        /// </returns>
        public Size MeasureFont(string text)
        {
            return MeasureFont(text, NoMaxWidth);
        }

        /// <summary>
        ///     Provides the size, in pixels, of the specified text when drawn with this font, automatically wrapping to keep
        ///     within the specified with.
        /// </summary>
        /// <param name="text">The text to measure.</param>
        /// <param name="maxWidth">The maximum width.</param>
        /// <returns>
        ///     The <see cref="Size" />, in pixels, of <paramref name="text" /> drawn with this font.
        /// </returns>
        /// <remarks>
        ///     The MeasureText method uses the <paramref name="maxWidth" /> parameter to automatically wrap when determining
        ///     text size.
        /// </remarks>
        public Size MeasureFont(string text, double maxWidth)
        {
            Size result;

            if (!string.IsNullOrEmpty(text))
            {
                char previousCharacter;
                int currentLineWidth;
                int currentLineHeight;
                int blockWidth;
                int blockHeight;
                int length;
                List<int> lineHeights;

                length = text.Length;
                previousCharacter = ' ';
                currentLineWidth = 0;
                currentLineHeight = LineHeight;
                blockWidth = 0;
                blockHeight = 0;
                lineHeights = new List<int>();

                for (var i = 0; i < length; i++)
                {
                    char character;

                    character = text[i];

                    if (character == '\n' || character == '\r')
                    {
                        if (character == '\n' || i + 1 == length || text[i + 1] != '\n')
                        {
                            lineHeights.Add(currentLineHeight);
                            blockWidth = System.Math.Max(blockWidth, currentLineWidth);
                            currentLineWidth = 0;
                            currentLineHeight = LineHeight;
                        }
                    }
                    else
                    {
                        Character data;
                        int width;

                        data = this[character];
                        width = data.XAdvance + GetKerning(previousCharacter, character);

                        if (maxWidth != NoMaxWidth && currentLineWidth + width >= maxWidth)
                        {
                            lineHeights.Add(currentLineHeight);
                            blockWidth = System.Math.Max(blockWidth, currentLineWidth);
                            currentLineWidth = 0;
                            currentLineHeight = LineHeight;
                        }

                        currentLineWidth += width;
                        currentLineHeight = System.Math.Max(currentLineHeight, data.Height + data.YOffset);
                        previousCharacter = character;
                    }
                }

                // finish off the current line if required
                if (currentLineHeight != 0) lineHeights.Add(currentLineHeight);

                // reduce any lines other than the last back to the base
                for (var i = 0; i < lineHeights.Count - 1; i++) lineHeights[i] = LineHeight;

                // calculate the final block height
                foreach (var lineHeight in lineHeights) blockHeight += lineHeight;

                result = new Size(System.Math.Max(currentLineWidth, blockWidth), blockHeight);
            }
            else
            {
                result = Size.Empty;
            }

            return result;
        }

        /// <summary>
        ///     Updates <see cref="Page" /> data with a fully qualified path
        /// </summary>
        /// <param name="font">The <see cref="BitmapFont" /> to update.</param>
        /// <param name="resourcePath">The path where texture resources are located.</param>
        internal static void QualifyResourcePaths(BitmapFont font, string resourcePath)
        {
            Page[] pages;

            pages = font.Pages;

            for (var i = 0; i < pages.Length; i++)
            {
                Page page;

                page = pages[i];
                page.FileName = Path.Combine(resourcePath, page.FileName);
                pages[i] = page;
            }

            font.Pages = pages;
        }

        /// <summary>
        ///     Gets the enumerator.
        /// </summary>
        /// <returns>
        ///     The enumerator.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion Public Methods

        #region Private Methods

        #endregion Private Methods
    }
}