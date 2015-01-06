using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Reactor.Types
{
    public class RMaterial
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<RTexture> Textures { get; set; }
        public List<RColor> Colors { get; set; }
        public float Shininess { get; set; }
        public float SpecularPower { get; set; }
        public RShader Shader { get; set; }

        public RMaterial(string name)
        {
            Name = name;
            Textures = new List<RTexture>(MAX_MATERIAL_LAYERS);
            Colors = new List<RColor>(MAX_MATERIAL_LAYERS);
            Shininess = 1;
            SpecularPower = 1;
            Shader = new RShader();
        }

        //Needs to be set based on number of RMaterialLayer enum values!
        internal static int MAX_MATERIAL_LAYERS = 7;
    }
}
