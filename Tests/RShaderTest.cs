//
// Test.cs
//
// Author:
//       Gabriel Reiser <gabriel@reisergames.com>
//
// Copyright (c) 2015 2014
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
using NUnit.Framework;
using System;
using Reactor.Types;
using Reactor;

namespace Tests
{
    [TestFixture()]
    public class RShaderTests
    {
        string vert = " struct v2f { vec4 position; vec4 color; vec2 texCoord; }\r\n\r\nv2f vs(){ v2f output; output.position = position; output.color = vec4(1.0f, 1.0f, 1.0f, 1.0f); output.texCoord = texCoord; return output; }";
        string frag = " out vec4 output; in v2f input; void fs(){ output = input.color; }";

        RShader shader;
        REngine engine = new REngine();
        [SetUp]
        public void Setup()
        {
            engine.Init();
            shader = new RShader();
            shader.Load(vert, frag, null);
        }

        [TearDown]
        public void Teardown()
        {
            shader.Dispose();
        }
        [Test()]
        public void LoadShadersAndLinkThem()
        {
        }
    }
}

