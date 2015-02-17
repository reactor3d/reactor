using Reactor.Types;
using System.Collections.Generic;
using Reactor.Math;

namespace Reactor
{
    public class RTextures : RSingleton<RTextures>
    {

        internal static Dictionary<string, RTexture> Textures = new Dictionary<string, RTexture>();

        public RTextures()
        {
             
        }
        public RTexture CreateTexture<TRTexture>(byte[] data, string name, bool isCompressed) where TRTexture : RTexture, new()
        {
            if (Textures.ContainsKey(name))
                return Textures[name];
            RTexture texture = new TRTexture();
            texture.Name = name;
            texture.LoadFromData(data, name, isCompressed);
            Textures.Add(name, texture);
            return texture;
        }
        public RTexture CreateTexture<TRTexture>(string name, string filename) where TRTexture : RTexture, new()
        {
            if (Textures.ContainsKey(name))
                return Textures[name];
            RTexture texture = new TRTexture();
            texture.Name = name;
            texture.LoadFromDisk(filename);
            Textures.Add(name, texture);
            return texture;
        }
        public RTexture GetTexture(string name)
        {
            if(Textures.ContainsKey(name))
                return Textures[name];
            else
                return null;
        }
        public bool RemoveTexture(string name)
        {
            if(Textures.ContainsKey(name))
            {
                Textures[name].Dispose();
                Textures.Remove(name);
                return true;
            }
            else 
            {
                return false;
            }
        }


        internal static RTexture GetTexture(uint texture)
        {
            foreach(RTexture t in Textures.Values)
            {
                if(t.Id == texture){
                    return t;
                }
            }
            return null;
        }
            
    }
}