using Newtonsoft.Json;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace Reactor.Types
{
    public class RMaterial : IDisposable
    {

        //Needs to be set based on number of RMaterialLayer enum values!
        internal static int MAX_MATERIAL_LAYERS = 7;
        internal static int MAX_MATERIAL_COLOR_LAYERS = 5;

        internal static RMaterial defaultMaterial = RMaterials.Instance.CreateMaterial("default");

        [JsonIgnore]
        public int Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("textures")]
        public RTexture[] Textures { get; set; }
        [JsonProperty("colors")]
        public RColor[] Colors { get; set; }
        [JsonProperty("shininess")]
        public float Shininess { get; set; }
        [JsonProperty("specularPower")]
        public float SpecularPower { get; set; }
        [JsonProperty("shader")]
        public RShader Shader { get; set; }

        internal RMaterial(string name)
        {
            Name = name;
            Textures = new RTexture[MAX_MATERIAL_LAYERS];
            for (int i = 0; i < Textures.Length; i++)
            {
                Textures[i] = RTexture.defaultWhite;
            }
            Colors = new RColor[MAX_MATERIAL_COLOR_LAYERS];
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

        public void SetColor(RMaterialColor materialColorLayer, RColor color)
        {
            Colors[(int)materialColorLayer] = color;
        }
        public void SetColor(int materialColorLayer, RColor color)
        {
            Colors[materialColorLayer] = color;
        }

        public void SetColor(int materialColorLayer, int color)
        {
            Colors[materialColorLayer] = new RColor((uint)color);
        }

        public RColor GetColor(RMaterialColor materialColorLayer)
        {
            return GetColor((int)materialColorLayer);
        }

        public RColor GetColor(int materialColorLayer)
        {
            if (Colors[materialColorLayer] != null)
                return Colors[materialColorLayer];
            else
                throw new InvalidOperationException("Attempted to retrieve a color from a material that has no color for that slot");
        }

        internal void Apply()
        {
            Shader.Bind();
            for(int i=0; i<Textures.Length; i++)
            {
                if(Textures[i]!=null)
                {
                    GL.ActiveTexture(TextureUnit.Texture0 + i);
                    Textures[i].Bind();
                    Shader.SetSamplerValue(RTextureLayer.DIFFUSE + i, Textures[i]);
                }
                
            }
            for(int i=0; i<Colors.Length; i++)
            {
                if(Colors[i]!=null)
                {
                    Shader.SetUniformValue(GetMaterialColorName(RMaterialColor.DIFFUSE + i), Colors[i]);
                }
                
            }
        }



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
