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