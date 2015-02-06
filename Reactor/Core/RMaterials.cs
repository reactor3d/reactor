using Reactor.Types;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reactor
{
    public class RMaterials : RSingleton<RMaterials>
    {
        internal static Dictionary<string, RMaterial> materials = new Dictionary<string, RMaterial>();

        public RMaterials()
        {

        }
        public RMaterial CreateMaterial(string name)
        {
            if (!materials.ContainsKey(name))
            {
                RMaterial material = new RMaterial(name);
                material.Id = materials.Count;
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

        public MemoryStream SaveMaterial(RMaterial material)
        {
            return RFileSystem.Instance.Save<RMaterial>(material);
        }
        public RMaterial LoadMaterial(string name, MemoryStream stream)
        {
            RMaterial material = RFileSystem.Instance.Load<RMaterial>(stream);
            material.Id = materials.Count + 1;
            materials.Add(name, material);
            return material;
        }
    }
}
