// Author:
//       Gabriel Reiser <gabe@reisergames.com>
//
// Copyright (c) 2010-2016 Reiser Games, LLC.
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using System;
using Newtonsoft.Json;
using Reactor.Platform.OpenGL;

namespace Reactor.Types
{
    public class RMaterial : IDisposable
    {

        //Needs to be set based on number of RMaterialLayer enum values!
        internal static int MAX_MATERIAL_LAYERS = 32;

        internal static RMaterial defaultMaterial = RMaterials.Instance.CreateMaterial("default");

        [JsonIgnore]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("textures")]
        public RTexture[] Textures { get; set; }

        [JsonProperty("baseColor")]
        public RColor Color { get; set; }

        [JsonProperty("metallic")]
        public float Metallic { get; set; }

        [JsonProperty("roughness")]
        public float Roughness { get; set; }

        [JsonProperty("emissiveColor")]
        public RColor EmissiveColor { get; set; }

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
            Metallic = 1;
            SpecularPower = 1;
            Shader = RShader.basicShader;
        }

        public RMaterial Clone(string name)
        {
            RMaterial clone = RMaterials.Instance.CreateMaterial(name);
            clone.Textures = this.Textures;
            clone.Color = this.Color;
            clone.Metallic = this.Metallic;
            clone.Roughness = this.Roughness;
            clone.SpecularPower = this.SpecularPower;
            clone.Shader = this.Shader;
            return clone;
        }
        public void SetTexture(int TextureLayer, RTexture texture)
        {
            Textures[GetTextureLayerIndex((RTextureLayer)TextureLayer)] = texture;
        }

        public void SetTexture(RTextureLayer TextureLayer, RTexture texture)
        {
            Textures[GetTextureLayerIndex(TextureLayer)] = texture;
        }

        public void SetTexture(int TextureLayer, uint TextureId)
        {
            RTexture texture = RTextures.GetTexture(TextureId);
            Textures[GetTextureLayerIndex((RTextureLayer)TextureLayer)] = texture;
        }
        public RTexture GetTexture(RTextureLayer TextureLayer)
        {
            if (Textures[GetTextureLayerIndex(TextureLayer)] != null)
                return Textures[GetTextureLayerIndex(TextureLayer)];
            else
                return null;
        }

        public RTexture GetTexture(int TextureLayer)
        {
            if(Textures[GetTextureLayerIndex((RTextureLayer)TextureLayer)] != null)
                return Textures[GetTextureLayerIndex((RTextureLayer)TextureLayer)];
            else
                return null;

        }

        public void Apply()
        {
            for(int i=0; i<Textures.Length; i++)
            {
                if(Textures[i]!=null)
                {
                    GL.ActiveTexture(i);
                    Textures[i].Bind();
                    Shader.SetSamplerValue(RTextureLayer.TEXTURE0 + i, Textures[i]);
                }
                
            }
            
        }


        internal int GetTextureLayerIndex(RTextureLayer layer)
        {
            return (int)layer;
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
        BASE = 0,
        NORMAL = 1,
        METALLIC = 2,
        ROUGHNESS = 3,
        AO = 4,
        EMISSIVE = 5,
        OPACITY = 6
    }
}
