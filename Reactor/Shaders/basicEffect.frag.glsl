#include "lighting.glsl"

in vec2 out_texcoord;
in vec3 out_normal;
out vec4 color;
void main()
{
   vec4 d = texture2D(diffuse, out_texcoord);
   vec4 n = texture2D(normal, out_texcoord) + texture2D(height, out_texcoord);
   color = ambient_color;
   color += d * diffuse_color;
}
