using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Reactor.Types
{
    public enum RTextureLayer : int
    {
        DIFFUSE = TextureUnit.Texture0,
        NORMAL = TextureUnit.Texture1,
        AMBIENT = TextureUnit.Texture2,
        SPECULAR = TextureUnit.Texture3,
        GLOW = TextureUnit.Texture4,
        HEIGHT = TextureUnit.Texture5,
        DETAIL = TextureUnit.Texture6,
        TEXTURE7 = TextureUnit.Texture7,
        TEXTURE8 = TextureUnit.Texture8,
        TEXTURE9 = TextureUnit.Texture9,
        TEXTURE10 = TextureUnit.Texture10,
        TEXTURE11 = TextureUnit.Texture11,
        TEXTURE12 = TextureUnit.Texture12,
        TEXTURE13 = TextureUnit.Texture13,
        TEXTURE14 = TextureUnit.Texture14,
        TEXTURE15 = TextureUnit.Texture15
    }
    public enum RTextureUnit : int
    {
        DIFFUSE = 0,
        NORMAL = 1,
        AMBIENT = 2,
        SPECULAR = 3,
        GLOW = 4,
        HEIGHT = 5,
        DETAIL = 6,
        TEXTURE7 = 7,
        TEXTURE8 = 8,
        TEXTURE9 = 9,
        TEXTURE10 = 10,
        TEXTURE11 = 11,
        TEXTURE12 = 12,
        TEXTURE13 = 13,
        TEXTURE14 = 14,
        TEXTURE15 = 15
    }
}
