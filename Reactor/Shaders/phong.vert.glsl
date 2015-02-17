#include "headers.glsl"

uniform mat4 projection, world, view;

out vec3 normalInterp;
out vec3 vertPos;

void main(){
	gl_Position = projection * view * world * vec4(inputPosition, 1.0);
	vec4 vertPos4 = world * vec4(r_Position, 1.0);
	vertPos = vec3(vertPos4) / vertPos4.w;
	normalInterp = vec3(normalMat * vec4(inputNormal, 0.0));
}
