in vec2 out_texcoords;
in vec4 out_color;

uniform sampler2D diffuse;
uniform vec4 diffuse_color;
uniform bool font = false;
out vec4 color;

void main(){
    vec4 t = texture(diffuse, out_texcoords.xy).rgba;
	if(font){
		color = t * diffuse_color;
    	color.a = t.a;
    } else {
    	color = t;
    }

}
