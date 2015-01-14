
layout(location=1) in vec3 position;
layout(location=2) in vec3 normal;
layout(location=3) in vec2 texcoord;

uniform mat4 world;
uniform mat4 view;
uniform mat4 projection;
out vec3 out_normal;
    void main() {
        vec4 vPosition = vec4(position, 1.0f);
        out_normal = normal;
		gl_Position = projection * view * world * vPosition;
	}
