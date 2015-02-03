using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Reactor.Types
{
    public class RMaterial : IDisposable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<RTexture> Textures { get; set; }
        public List<RColor> Colors { get; set; }
        public float Shininess { get; set; }
        public float SpecularPower { get; set; }
        public RShader Shader { get; set; }

        internal RMaterial(string name)
        {
            Name = name;
            Textures = new List<RTexture>(MAX_MATERIAL_LAYERS);
            Colors = new List<RColor>(MAX_MATERIAL_COLOR_LAYERS);
            Shininess = 1;
            SpecularPower = 1;
            Shader = RShader.basicShader;
        }

        public RMaterial Clone(string name)
        {
            RMaterial clone = RMaterials.Instance.CreateMaterial(name);
            clone.Textures = this.Textures;
            clone.Colors = this.Colors;
            clone.Shininess = this.Shininess;
            clone.SpecularPower = this.SpecularPower;
            clone.Shader = this.Shader;
            return clone;
        }
        public void SetTexture(int TextureLayer, RTexture texture)
        {
            Textures[TextureLayer] = texture;
        }

        public void SetTexture(RTextureLayer TextureLayer, RTexture texture)
        {
            Textures[(int)TextureLayer] = texture;
        }

        public void SetTexture(int TextureLayer, uint TextureId)
        {
            RTexture texture = RTextures.GetTexture(TextureId);
            Textures[TextureLayer] = texture;
        }
        public RTexture GetTexture(RTextureLayer TextureLayer)
        {
            if (Textures[(int)TextureLayer] != null)
                return Textures[(int)TextureLayer];
            else
                return null;
        }

        public RTexture GetTexture(int TextureLayer)
        {
            if(Textures[TextureLayer] != null)
                return Textures[TextureLayer];
            else
                return null;

        }

        internal void Apply()
        {
            Shader.Bind();
            for(int i=0; i<Textures.Count; i++)
            {
                GL.ActiveTexture(TextureUnit.Texture0 + i);
                Textures[i].Bind();
                Shader.SetSamplerValue(RTextureLayer.DIFFUSE + i, Textures[i]);
            }
            for(int i=0; i<Colors.Count; i++)
            {
                Shader.SetUniformValue(GetMaterialColorName(RMaterialColor.DIFFUSE + i), Colors[i]);
            }
            Shader.Unbind();
        }
        internal static RMaterial defaultMaterial = RMaterials.Instance.CreateMaterial("default");
        //Needs to be set based on number of RMaterialLayer enum values!
        internal static int MAX_MATERIAL_LAYERS = 7;
        internal static int MAX_MATERIAL_COLOR_LAYERS = 5;


        internal string GetMaterialColorName(RMaterialColor materialColor)
        {
            switch(materialColor)
            {
                case RMaterialColor.DIFFUSE:
                    return "diffuse_color";
                case RMaterialColor.AMBIENT:
                    return "ambient_color";
                case RMaterialColor.SPECULAR:
                    return "specular_color";
                case RMaterialColor.GLOW:
                    return "glow_color";
                case RMaterialColor.ALPHA:
                    return "alpha_color";
            }
            return "diffuse_color";
        }


        public void Dispose()
        {
            foreach (RTexture texture in Textures)
                texture.Dispose();
            
            if (Shader != null)
                Shader.Dispose();
        }
    }
    public enum RMaterialColor : int
    {
        DIFFUSE = 0,
        AMBIENT = 1,
        SPECULAR = 2,
        GLOW = 3,
        ALPHA = 4
    }
}
