using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Text;
using System.Xml;


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
  /// A bitmap font.
  /// </summary>
  /// <seealso cref="T:System.Collections.Generic.IEnumerable{Cyotek.Drawing.BitmapFont.Character}"/>
  public class BitmapFont : IEnumerable<Character>
  {
    #region Public Fields

    /// <summary>
    /// When used with <see cref="MeasureFont(string,double)"/>, specifies that no wrapping should occur.
    /// </summary>
    public const int NoMaxWidth = -1;

    #endregion Public Fields

    #region Private Fields

    private int _alphaChannel;

    private int _baseHeight;

    private int _blueChannel;

    private bool _bold;

    private IDictionary<char, Character> _characters;

    private string _charset;

    private string _familyName;

    private int _fontSize;

    private int _greenChannel;

    private Character _invalid;

    private bool _italic;

    private IDictionary<Kerning, int> _kernings;

    private int _lineHeight;

    private int _outlineSize;

    private bool _packed;

    private Padding _padding;

    private Page[] _pages;

    private int _redChannel;

    private bool _smoothed;

    private Point _spacing;

    private int _stretchedHeight;

    private int _superSampling;

    private Size _textureSize;

    private bool _unicode;

    #endregion Private Fields

    #region Public Constructors

    /// <summary> Default constructor. </summary>
    public BitmapFont()
    {
      _invalid = Character.Empty;
      _pages = new Page[0];
      _kernings = new Dictionary<Kerning, int>();
      _characters = new Dictionary<char, Character>();
    }

    #endregion Public Constructors

    #region Public Properties

    /// <summary>
    /// Gets or sets the alpha channel.
    /// </summary>
    /// <value>
    /// The alpha channel.
    /// </value>
    /// <remarks>Set to 0 if the channel holds the glyph data, 1 if it holds the outline, 2 if it holds the glyph and the outline, 3 if its set to zero, and 4 if its set to one.</remarks>
    public int AlphaChannel
    {
      get { return _alphaChannel; }
      set { _alphaChannel = value; }
    }

    /// <summary>
    /// Gets or sets the number of pixels from the absolute top of the line to the base of the characters.
    /// </summary>
    /// <value>
    /// The number of pixels from the absolute top of the line to the base of the characters.
    /// </value>
    public int BaseHeight
    {
      get { return _baseHeight; }
      set { _baseHeight = value; }
    }

    /// <summary>
    /// Gets or sets the blue channel.
    /// </summary>
    /// <value>
    /// The blue channel.
    /// </value>
    /// <remarks>Set to 0 if the channel holds the glyph data, 1 if it holds the outline, 2 if it holds the glyph and the outline, 3 if its set to zero, and 4 if its set to one.</remarks>
    public int BlueChannel
    {
      get { return _blueChannel; }
      set { _blueChannel = value; }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the font is bold.
    /// </summary>
    /// <value>
    /// <c>true</c> if the font is bold, otherwise <c>false</c>.
    /// </value>
    public bool Bold
    {
      get { return _bold; }
      set { _bold = value; }
    }

    /// <summary>
    /// Gets or sets the characters that comprise the font.
    /// </summary>
    /// <value>
    /// The characters that comprise the font.
    /// </value>
    public IDictionary<char, Character> Characters
    {
      get { return _characters; }
      set { _characters = value; }
    }

    /// <summary>
    /// Gets or sets the name of the OEM charset used.
    /// </summary>
    /// <value>
    /// The name of the OEM charset used (when not unicode).
    /// </value>
    public string Charset
    {
      get { return _charset; }
      set { _charset = value; }
    }

    /// <summary>
    /// Gets or sets the name of the true type font.
    /// </summary>
    /// <value>
    /// The font family name.
    /// </value>
    public string FamilyName
    {
      get { return _familyName; }
      set { _familyName = value; }
    }

    /// <summary>
    /// Gets or sets the size of the font.
    /// </summary>
    /// <value>
    /// The size of the font.
    /// </value>
    public int FontSize
    {
      get { return _fontSize; }
      set { _fontSize = value; }
    }

    /// <summary>
    /// Gets or sets the green channel.
    /// </summary>
    /// <value>
    /// The green channel.
    /// </value>
    /// <remarks>Set to 0 if the channel holds the glyph data, 1 if it holds the outline, 2 if it holds the glyph and the outline, 3 if its set to zero, and 4 if its set to one.</remarks>
    public int GreenChannel
    {
      get { return _greenChannel; }
      set { _greenChannel = value; }
    }

    /// <summary> Gets the fallback character used when a matching character cannot be found. </summary>
    /// <value> The fallback character. </value>
    public Character InvalidChar
    {
      get { return _invalid; }
      set { _invalid = value; }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the font is italic.
    /// </summary>
    /// <value>
    /// <c>true</c> if the font is italic, otherwise <c>false</c>.
    /// </value>
    public bool Italic
    {
      get { return _italic; }
      set { _italic = value; }
    }

    /// <summary>
    /// Gets or sets the character kernings for the font.
    /// </summary>
    /// <value>
    /// The character kernings for the font.
    /// </value>
    public IDictionary<Kerning, int> Kernings
    {
      get { return _kernings; }
      set { _kernings = value; }
    }

    /// <summary>
    /// Gets or sets the distance in pixels between each line of text.
    /// </summary>
    /// <value>
    /// The distance in pixels between each line of text.
    /// </value>
    public int LineHeight
    {
      get { return _lineHeight; }
      set { _lineHeight = value; }
    }

    /// <summary>
    /// Gets or sets the outline thickness for the characters.
    /// </summary>
    /// <value>
    /// The outline thickness for the characters.
    /// </value>
    public int OutlineSize
    {
      get { return _outlineSize; }
      set { _outlineSize = value; }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the monochrome characters have been packed into each of the texture channels.
    /// </summary>
    /// <value>
    /// <c>true</c> if the characters are packed, otherwise <c>false</c>.
    /// </value>
    /// <remarks>
    /// When packed, the <see cref="AlphaChannel"/> property describes what is stored in each channel.
    /// </remarks>
    public bool Packed
    {
      get { return _packed; }
      set { _packed = value; }
    }

    /// <summary>
    /// Gets or sets the padding for each character.
    /// </summary>
    /// <value>
    /// The padding for each character.
    /// </value>
    public Padding Padding
    {
      get { return _padding; }
      set { _padding = value; }
    }

    /// <summary>
    /// Gets or sets the texture pages for the font.
    /// </summary>
    /// <value>
    /// The pages.
    /// </value>
    public Page[] Pages
    {
      get { return _pages; }
      set { _pages = value; }
    }

    /// <summary>
    /// Gets or sets the red channel.
    /// </summary>
    /// <value>
    /// The red channel.
    /// </value>
    /// <remarks>Set to 0 if the channel holds the glyph data, 1 if it holds the outline, 2 if it holds the glyph and the outline, 3 if its set to zero, and 4 if its set to one.</remarks>
    public int RedChannel
    {
      get { return _redChannel; }
      set { _redChannel = value; }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the font is smoothed.
    /// </summary>
    /// <value>
    /// <c>true</c> if the font is smoothed, otherwise <c>false</c>.
    /// </value>
    public bool Smoothed
    {
      get { return _smoothed; }
      set { _smoothed = value; }
    }

    /// <summary>
    /// Gets or sets the spacing for each character.
    /// </summary>
    /// <value>
    /// The spacing for each character.
    /// </value>
    public Point Spacing
    {
      get { return _spacing; }
      set { _spacing = value; }
    }

    /// <summary>
    /// Gets or sets the font height stretch.
    /// </summary>
    /// <value>
    /// The font height stretch.
    /// </value>
    /// <remarks>100% means no stretch.</remarks>
    public int StretchedHeight
    {
      get { return _stretchedHeight; }
      set { _stretchedHeight = value; }
    }

    /// <summary>
    /// Gets or sets the level of super sampling used by the font.
    /// </summary>
    /// <value>
    /// The super sampling level of the font.
    /// </value>
    /// <remarks>A value of 1 indicates no super sampling is in use.</remarks>
    public int SuperSampling
    {
      get { return _superSampling; }
      set { _superSampling = value; }
    }

    /// <summary>
    /// Gets or sets the size of the texture images used by the font.
    /// </summary>
    /// <value>
    /// The size of the texture.
    /// </value>
    public Size TextureSize
    {
      get { return _textureSize; }
      set { _textureSize = value; }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the font is Unicode.
    /// </summary>
    /// <value>
    /// <c>true</c> if the font is Unicode, otherwise <c>false</c>.
    /// </value>
    public bool Unicode
    {
      get { return _unicode; }
      set { _unicode = value; }
    }

    #endregion Public Properties

    #region Public Indexers

    /// <summary>
    /// Indexer to get items within this collection using array index syntax.
    /// </summary>
    /// <param name="character">The character.</param>
    /// <returns>
    /// The indexed item.
    /// </returns>
    public Character this[char character]
    {
      get { return _characters.TryGetValue(character, out Character value) ? value : _invalid; }
    }

    #endregion Public Indexers

    #region Public Methods

    /// <summary>
    /// Returns an enumerator that iterates through the collection.
    /// </summary>
    /// <returns>
    /// A <see cref="T:System.Collections.Generic.IEnumerator`1" /> that can be used to iterate through
    /// the collection.
    /// </returns>
    /// <seealso cref="M:System.Collections.Generic.IEnumerable{Cyotek.Drawing.BitmapFont.Character}.GetEnumerator()"/>
    public IEnumerator<Character> GetEnumerator()
    {
      foreach (KeyValuePair<char, Character> pair in _characters)
      {
        yield return pair.Value;
      }
    }

    /// <summary>
    /// Gets the kerning for the specified character combination.
    /// </summary>
    /// <param name="previous">The previous character.</param>
    /// <param name="current">The current character.</param>
    /// <returns>
    /// The spacing between the specified characters.
    /// </returns>
    public int GetKerning(char previous, char current)
    {
      Kerning key;

      key = new Kerning(previous, current, 0);

      if (!_kernings.TryGetValue(key, out int result))
      {
        result = 0;
      }

      return result;
    }

    

    
    /// <summary>
    /// Provides the size, in pixels, of the specified text when drawn with this font.
    /// </summary>
    /// <param name="text">The text to measure.</param>
    /// <returns>
    /// The <see cref="Size"/>, in pixels, of <paramref name="text"/> drawn with this font.
    /// </returns>
    public Size MeasureFont(string text)
    {
      return this.MeasureFont(text, NoMaxWidth);
    }

    /// <summary>
    /// Provides the size, in pixels, of the specified text when drawn with this font, automatically wrapping to keep within the specified with.
    /// </summary>
    /// <param name="text">The text to measure.</param>
    /// <param name="maxWidth">The maximum width.</param>
    /// <returns>
    /// The <see cref="Size"/>, in pixels, of <paramref name="text"/> drawn with this font.
    /// </returns>
    /// <remarks>The MeasureText method uses the <paramref name="maxWidth"/> parameter to automatically wrap when determining text size.</remarks>
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
        currentLineHeight = _lineHeight;
        blockWidth = 0;
        blockHeight = 0;
        lineHeights = new List<int>();

        for (int i = 0; i < length; i++)
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
              currentLineHeight = _lineHeight;
            }
          }
          else
          {
            Character data;
            int width;

            data = this[character];
            width = data.XAdvance + this.GetKerning(previousCharacter, character);

            if (maxWidth != NoMaxWidth && currentLineWidth + width >= maxWidth)
            {
              lineHeights.Add(currentLineHeight);
              blockWidth = System.Math.Max(blockWidth, currentLineWidth);
              currentLineWidth = 0;
              currentLineHeight = _lineHeight;
            }

            currentLineWidth += width;
            currentLineHeight = System.Math.Max(currentLineHeight, data.Height + data.YOffset);
            previousCharacter = character;
          }
        }

        // finish off the current line if required
        if (currentLineHeight != 0)
        {
          lineHeights.Add(currentLineHeight);
        }

        // reduce any lines other than the last back to the base
        for (int i = 0; i < lineHeights.Count - 1; i++)
        {
          lineHeights[i] = _lineHeight;
        }

        // calculate the final block height
        foreach (int lineHeight in lineHeights)
        {
          blockHeight += lineHeight;
        }

        result = new Size(System.Math.Max(currentLineWidth, blockWidth), blockHeight);
      }
      else
      {
        result = Size.Empty;
      }

      return result;
    }
    
    /// <summary>
    /// Updates <see cref="Page"/> data with a fully qualified path
    /// </summary>
    /// <param name="font">The <see cref="BitmapFont"/> to update.</param>
    /// <param name="resourcePath">The path where texture resources are located.</param>
    internal static void QualifyResourcePaths(BitmapFont font, string resourcePath)
    {
      Page[] pages;

      pages = font.Pages;

      for (int i = 0; i < pages.Length; i++)
      {
        Page page;

        page = pages[i];
        page.FileName = Path.Combine(resourcePath, page.FileName);
        pages[i] = page;
      }

      font.Pages = pages;
    }
    /// <summary>
    /// Gets the enumerator.
    /// </summary>
    /// <returns>
    /// The enumerator.
    /// </returns>
    IEnumerator IEnumerable.GetEnumerator()
    {
      return this.GetEnumerator();
    }

    #endregion Public Methods

    #region Private Methods


    #endregion Private Methods
  }
}
