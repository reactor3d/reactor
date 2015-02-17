using OpenTK.Graphics.OpenGL4;
using Reactor.Math;
using Reactor.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reactor.Core
{
    public class RAtmosphere : RSingleton<RAtmosphere>
    {
        private RMeshBuilder sky;

        private RShader skybox;

        private RColor globalSunLight;
        private RColor globalAmbientLight;
        
        public void CreateSkyBox(RTexture3D skyBoxTexture)
        {
            skybox = new RShader();
            skybox.Load(@"
#include ""headers.glsl""
uniform mat4 projection;
uniform mat4 view;
uniform mat4 world;

out vec2 texCoord;
void main()
{
gl_Position = gl_Vertex;
}
", @"
in vec2 texCoord;
uniform samplerCube cubemap;
void main()
{
gl_FragColor = textureCube(cubemap, texCoord);
}
", null);
            sky = RScene.Instance.CreateMeshBuilder("skybox");
            sky.CreateBox(Vector3.Zero, Vector3.One, true);
            sky.SetShader(skybox);
            sky.SetTexture((int)RTextureLayer.DIFFUSE, skyBoxTexture);
        }

        internal void Update()
        {
            if(sky != null)
            {
                sky.Position = REngine.Instance.GetCamera().Position;
                sky.Update();
            }
        }

        public void RenderSkybox()
        {
            GL.Disable(EnableCap.DepthTest);
            GL.Disable(EnableCap.CullFace);
            
            sky.Render();
            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.CullFace);
        }
    }
}
