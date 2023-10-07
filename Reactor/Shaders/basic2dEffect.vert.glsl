layout(location=0) in vec2 r_Position;
layout(location=1) in vec2 r_TexCoord;

uniform mat4 projection;
uniform mat4 view;
uniform mat4 model;

out vec2 texcoords;

void main(){
	texcoords = r_TexCoord;
	gl_Position = projection * model * vec4(r_Position, 0, 1);
}
