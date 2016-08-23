#include "headers.glsl"

uniform mat4 projection : PROJECTION;
uniform mat4 world : WORLD;
uniform mat4 view : VIEW;


out vec3 normalInterp;
out vec3 vertPos;

void main(){
	gl_Position = projection * view * world * vec4(r_Position, 1.0);
	vertPos = world * r_Position;
	normalInterp = r_Normal;
}
