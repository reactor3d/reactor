
layout(location=1) in vec3 position;
layout(location=2) in vec2 texcoord;

uniform mat4 world;
uniform mat4 view;
uniform mat4 projection;

    void main() {
        vec4 vPosition = vec4(position, 1.0f);

		gl_Position = projection * view * world * vPosition;
	}
