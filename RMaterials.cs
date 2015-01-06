using Reactor.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reactor
{
    public class RMaterials : RSingleton<RMaterials>
    {
        internal static Dictionary<string, RMaterial> materials = new Dictionary<string, RMaterial>();

        public RMaterial CreateMaterial(string name)
        {
            if (!materials.ContainsKey(name))
            {
                RMaterial material = new RMaterial(name);
                materials.Add(name, material);
                return material;
            } else
            {
                return materials[name];
            }
        }

        public RMaterial GetMaterial(string name)
        {
            if (!materials.ContainsKey(name))
            {
                return materials[name];
            } else
            {
                return null;
            }
        }

        public void DeleteMaterial(string name)
        {
            if (!materials.ContainsKey(name))
            {
                materials.Remove(name);
            }
        }
    }
}
