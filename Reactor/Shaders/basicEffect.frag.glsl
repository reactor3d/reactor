#include "lighting.glsl"

in vec2 out_texcoord;
in vec3 out_normal;
in float logz;
out vec4 color;
void main()
{
   vec4 d = texture(texture0, out_texcoord);
   vec4 n = texture(texture1, out_texcoord);
   color = vec4(0.0);
   color += d * base_color;
   //gl_FragDepth = logz;
}
