#include "headers.glsl"

uniform mat4 world : WORLD;
uniform mat4 view : VIEW;
uniform mat4 projection : PROJECTION;
uniform float near : NEAR_PLANE;
uniform float far : FAR_PLANE;

out vec3 out_normal;
out vec2 out_texcoord;
out float logz;

    void main() {
		vec4 vPosition = vec4(r_Position, 1.0f);
		out_normal = r_Normal;
        out_texcoord = r_TexCoord;
		gl_Position = projection * view * world * vPosition;
		
		float FC = 1.0f/log(far*near+1.0f); 

		//logz = gl_Position.w*C + 1;  //version with fragment code 
		logz = log(gl_Position.w*near + 1.0f)*FC;
		gl_Position.z = (2.0f*logz - 1.0f)*gl_Position.w;


	}
