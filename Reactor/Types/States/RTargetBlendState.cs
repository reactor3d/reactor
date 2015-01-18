using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reactor.Types.States
{
    public class RTargetBlendState
    {
        internal RTargetBlendState()
        {
            AlphaBlendFunction = RBlendFunc.Add;
            AlphaDestinationBlend = RBlend.Zero;
            AlphaSourceBlend = RBlend.One;
            ColorBlendFunction = RBlendFunc.Add;
            ColorDestinationBlend = RBlend.Zero;
            ColorSourceBlend = RBlend.One;
            ColorWriteChannels = RColorWriteChannels.All;
        }

        public RBlendFunc AlphaBlendFunction { get; set; }

        public RBlend AlphaDestinationBlend { get; set; }

        public RBlend AlphaSourceBlend { get; set; }

        public RBlendFunc ColorBlendFunction { get; set; }

        public RBlend ColorDestinationBlend { get; set; }

        public RBlend ColorSourceBlend { get; set; }

        public RColorWriteChannels ColorWriteChannels { get; set; }



    }
}
