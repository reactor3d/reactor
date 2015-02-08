using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reactor.Types
{
    public class RDisplayModes : IEnumerable
    {
        private readonly List<RDisplayMode> modes;

        public IEnumerable<RDisplayMode> this[RSurfaceFormat format]
        {
            get
            {
                List<RDisplayMode> list = new List<RDisplayMode>();
                foreach (RDisplayMode mode in this.modes)
                {
                    //if (mode.Format == format)
                    //{
                        list.Add(mode);
                    //}
                }
                return list;

            }
        }

        public IEnumerator<RDisplayMode> GetEnumerator()
        {
            return modes.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return modes.GetEnumerator();
        }

        public RDisplayModes(List<RDisplayMode> setmodes)
        {
            modes = setmodes;
        }
    }
}
