layout(location=0) in vec2 r_Position;
layout(location=1) in vec2 r_TexCoord;
layout(location=2) in vec4 r_Color;

uniform mat4 projection;
uniform mat4 view;
uniform mat4 model;

out vec2 out_texcoords;
out vec4 out_color;

void main(){
	out_texcoords = r_TexCoord;
	out_color = r_Color;
	gl_Position = projection*(model*vec4(r_Position, 0, 1));
}
