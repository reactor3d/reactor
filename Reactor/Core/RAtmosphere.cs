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
using Reactor.Graphics.OpenGL4;
using Reactor.Math;
using Reactor.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reactor
{
    public class RAtmosphere : RSingleton<RAtmosphere>
    {
        private RMeshBuilder sky;

        private RShader skybox;

        private RColor globalSunLight;
        private RColor globalAmbientLight;
        private Vector3 sunDir;
        private RTexture2D[] lensFlares;
        
        public void CreateSkyBox(RTexture3D skyBoxTexture)
        {
            skybox = new RShader();
            skybox.Load(@"
#include ""headers.glsl""
uniform mat4 proj : PROJECTION;
uniform mat4 mv : MODELVIEW;

smooth out vec3 eye;

void main()
{
    mat3 imv = mat3(inverse(mv));
    mat4 inverseProjection = inverse(proj);
    vec3 unprojected = (inverseProjection * vec4(r_Position, 1.0)).xyz;
    eye = imv * unprojected;

    gl_Position = vec4(r_Position, 1.0);
}
", @"

smooth in vec3 eye;
uniform samplerCube diffuse;
out vec4 fragColor;
void main()
{
    fragColor = texture(diffuse, eye);
}
", null);
            sky = RScene.Instance.CreateMeshBuilder("skybox");
            sky.CreateBox(Vector3.Zero, Vector3.One, true);
            sky.Matrix = Matrix.Identity;
            sky.DepthWrite = false;
            RMaterial skyBoxMaterial = new RMaterial("skybox");
            skyBoxMaterial.Shader = skybox;
            skyBoxMaterial.SetTexture(RTextureLayer.DIFFUSE, skyBoxTexture);
            sky.Material = skyBoxMaterial;
        }

        public void CreateSkyBox(RShader skyBoxShader, RTexture3D skyBoxTexture)
        {
            sky = RScene.Instance.CreateMeshBuilder("skybox");
            sky.CreateBox(Vector3.Zero, Vector3.One, true);
            sky.Matrix = Matrix.Identity;
            sky.DepthWrite = false;
            RMaterial skyBoxMaterial = new RMaterial("skybox");
            skyBoxMaterial.Shader = skyBoxShader;
            skyBoxMaterial.SetTexture(RTextureLayer.DIFFUSE, skyBoxTexture);
            sky.Material = skyBoxMaterial;
        }

        public void CreateSkyBox(RShader skyBoxShader)
        {
            sky = RScene.Instance.CreateMeshBuilder("skybox");
            sky.CreateBox(Vector3.Zero, Vector3.One, true);
            sky.Matrix = Matrix.Identity;
            sky.DepthWrite = false;
            RMaterial skyBoxMaterial = new RMaterial("skybox");
            skyBoxMaterial.Shader = skyBoxShader;
            sky.Material = skyBoxMaterial;
        }

        internal void Update()
        {
            if(sky != null)
            {
                sky.Position = REngine.Instance.GetCamera().Position;
                sky.Scale = Vector3.One;
                sky.Update();
            }
        }

        public void RenderSkybox()
        {
            GL.Disable(EnableCap.DepthTest);

            sky.Render();
            GL.Enable(EnableCap.DepthTest);

        }
    }
}
