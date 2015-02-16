#include "lighting.glsl"

in vec2 out_texcoord;
in vec3 out_normal;
out vec4 color;
void main()
{
   color = diffuse_color * vec4(1.0f*out_normal.x,1.0f*out_normal.y,1.0f*out_normal.z,1.0f);
}
