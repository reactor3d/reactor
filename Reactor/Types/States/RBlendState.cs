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
using Reactor.Platform.OpenGL;
using Reactor.Utilities;

namespace Reactor.Types.States
{
    public class RBlendState : IDisposable
    {
        private static readonly RBlendState _additive;
        private static readonly RBlendState _alphaBlend;
        private static readonly RBlendState _nonPremultiplied;
        private static readonly RBlendState _opaque;
        private readonly RTargetBlendState[] _targetBlendState;

        static RBlendState()
        {
            _additive = new RBlendState
            {
                Name = "BlendState.Additive",
                ColorSourceBlend = RBlend.SourceAlpha,
                AlphaSourceBlend = RBlend.SourceAlpha,
                ColorDestinationBlend = RBlend.One,
                AlphaDestinationBlend = RBlend.One
            };

            _alphaBlend = new RBlendState
            {
                Name = "BlendState.AlphaBlend",
                ColorSourceBlend = RBlend.One,
                AlphaSourceBlend = RBlend.One,
                ColorDestinationBlend = RBlend.InverseSourceAlpha,
                AlphaDestinationBlend = RBlend.InverseSourceAlpha
            };

            _nonPremultiplied = new RBlendState
            {
                Name = "BlendState.NonPremultiplied",
                ColorSourceBlend = RBlend.SourceAlpha,
                AlphaSourceBlend = RBlend.SourceAlpha,
                ColorDestinationBlend = RBlend.InverseSourceAlpha,
                AlphaDestinationBlend = RBlend.InverseSourceAlpha
            };

            _opaque = new RBlendState
            {
                Name = "BlendState.Opaque",
                ColorSourceBlend = RBlend.One,
                AlphaSourceBlend = RBlend.One,
                ColorDestinationBlend = RBlend.Zero,
                AlphaDestinationBlend = RBlend.Zero
            };
        }

        public RBlendState()
        {
            _targetBlendState = new RTargetBlendState[4];
            _targetBlendState[0] = new RTargetBlendState();
            _targetBlendState[1] = new RTargetBlendState();
            _targetBlendState[2] = new RTargetBlendState();
            _targetBlendState[3] = new RTargetBlendState();

            BlendFactor = RColor.White;
            MultiSampleMask = int.MaxValue;
            IndependentBlendEnable = false;
            Name = "BlendState.Opaque";
            ColorSourceBlend = RBlend.Zero;
            AlphaSourceBlend = RBlend.Zero;
            ColorDestinationBlend = RBlend.Zero;
            AlphaDestinationBlend = RBlend.Zero;
        }

        /// <summary>
        ///     Returns the target specific blend state.
        /// </summary>
        /// <param name="index">The 0 to 3 target blend state index.</param>
        /// <returns>A target blend state.</returns>
        public RTargetBlendState this[int index] => _targetBlendState[index];

        public RBlendFunc AlphaBlendFunction
        {
            get => _targetBlendState[0].AlphaBlendFunction;
            set => _targetBlendState[0].AlphaBlendFunction = value;
        }

        public RBlend AlphaDestinationBlend
        {
            get => _targetBlendState[0].AlphaDestinationBlend;
            set => _targetBlendState[0].AlphaDestinationBlend = value;
        }

        public RBlend AlphaSourceBlend
        {
            get => _targetBlendState[0].AlphaSourceBlend;
            set => _targetBlendState[0].AlphaSourceBlend = value;
        }

        public RBlendFunc ColorBlendFunction
        {
            get => _targetBlendState[0].ColorBlendFunction;
            set => _targetBlendState[0].ColorBlendFunction = value;
        }

        public RBlend ColorDestinationBlend
        {
            get => _targetBlendState[0].ColorDestinationBlend;
            set => _targetBlendState[0].ColorDestinationBlend = value;
        }

        public RBlend ColorSourceBlend
        {
            get => _targetBlendState[0].ColorSourceBlend;
            set => _targetBlendState[0].ColorSourceBlend = value;
        }

        public RColorWriteChannels ColorWriteChannels
        {
            get => _targetBlendState[0].ColorWriteChannels;
            set => _targetBlendState[0].ColorWriteChannels = value;
        }

        public RColorWriteChannels ColorWriteChannels1
        {
            get => _targetBlendState[1].ColorWriteChannels;
            set => _targetBlendState[1].ColorWriteChannels = value;
        }

        public RColorWriteChannels ColorWriteChannels2
        {
            get => _targetBlendState[2].ColorWriteChannels;
            set => _targetBlendState[2].ColorWriteChannels = value;
        }

        public RColorWriteChannels ColorWriteChannels3
        {
            get => _targetBlendState[3].ColorWriteChannels;
            set => _targetBlendState[3].ColorWriteChannels = value;
        }

        public RColor BlendFactor { get; set; }

        public int MultiSampleMask { get; set; }

        /// <summary>
        ///     Enables use of the per-target blend states.
        /// </summary>
        public bool IndependentBlendEnable { get; set; }

        public string Name { get; set; }

        public static RBlendState Additive => _additive.Value;
        public static RBlendState AlphaBlend => _alphaBlend.Value;
        public static RBlendState NonPremultiplied => _nonPremultiplied.Value;
        public static RBlendState Opaque => _opaque.Value;

        public void Dispose()
        {
        }

        internal static void ResetStates()
        {
            _additive.Reset();
            _alphaBlend.Reset();
            _nonPremultiplied.Reset();
            _opaque.Reset();
        }

        internal void PlatformApplyState()
        {
            REngine.CheckGLError();
            GL.Enable(EnableCap.Blend);
            REngine.CheckGLError();
            GL.BlendFunc(ColorSourceBlend.GetBlendFactorSrc(), ColorDestinationBlend.GetBlendFactorDest());
            REngine.CheckGLError();
            GL.BlendColor(
                BlendFactor.R / 255.0f,
                BlendFactor.G / 255.0f,
                BlendFactor.B / 255.0f,
                BlendFactor.A / 255.0f);
            REngine.CheckGLError();

            GL.BlendEquationSeparate(
                ColorBlendFunction.GetBlendEquationMode(),
                AlphaBlendFunction.GetBlendEquationMode());
            REngine.CheckGLError();

            GL.BlendFuncSeparate(
                ColorSourceBlend.GetBlendFactorSrc(),
                ColorDestinationBlend.GetBlendFactorDest(),
                AlphaSourceBlend.GetBlendFactorSrc(),
                AlphaDestinationBlend.GetBlendFactorDest());
            REngine.CheckGLError();

            GL.ColorMask(
                (ColorWriteChannels & RColorWriteChannels.Red) != 0,
                (ColorWriteChannels & RColorWriteChannels.Green) != 0,
                (ColorWriteChannels & RColorWriteChannels.Blue) != 0,
                (ColorWriteChannels & RColorWriteChannels.Alpha) != 0);
            REngine.CheckGLError();
        }
    }
}