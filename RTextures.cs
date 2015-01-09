using Reactor.Types;
using System.Collections.Generic;

namespace Reactor
{
    public class RTextures : RSingleton<RTextures>
    {

        internal static Dictionary<string, RTexture> Textures = new Dictionary<string, RTexture>();

        private RTextures()
        {
             
        }

        public RTexture GetTexture(string name)
        {
            if(Textures.ContainsKey(name))
                return Textures[name];
            else
                return null;
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