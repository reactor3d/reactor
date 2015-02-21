using OpenTK.Graphics.OpenGL4;
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
        
        public void CreateSkyBox(RTexture3D skyBoxTexture)
        {
            skybox = new RShader();
            skybox.Load(@"
#include ""headers.glsl""
uniform mat4 world : WORLD;
uniform mat4 view : VIEW;
uniform mat4 proj : PROJECTION;
out vec3 texCoord;
void main()
{
mat4 mvp = proj * view * world;
gl_Position = mvp * vec4(r_Position, 1.0);
texCoord = r_Position;
}
", @"
in vec3 texCoord;
uniform samplerCube diffuse;
void main()
{
vec4 cubeColor = texture(diffuse, texCoord);
gl_FragColor = cubeColor;
}
", null);
            sky = RScene.Instance.CreateMeshBuilder("skybox");
            sky.CreateBox(Vector3.Zero, Vector3.One, true);
            sky.Matrix = Matrix.Identity;
            RMaterial skyBoxMaterial = new RMaterial("skybox");
            skyBoxMaterial.Shader = skybox;
            skyBoxMaterial.SetTexture(RTextureLayer.DIFFUSE, skyBoxTexture);
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
            GL.Disable(EnableCap.CullFace);
            
            sky.Render();
            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.CullFace);
        }
    }
}
