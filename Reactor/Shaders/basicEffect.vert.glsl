#include "headers.glsl"

uniform mat4 world;
uniform mat4 view;
uniform mat4 projection;
out vec3 out_normal;
    void main() {
        vec4 vPosition = vec4(r_Position, 1.0f);
        out_normal = vec4(r_Normal, 1.0f).xyz;
		gl_Position = projection * view * world * vPosition;
	}
