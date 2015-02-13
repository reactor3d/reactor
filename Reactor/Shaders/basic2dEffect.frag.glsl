in vec2 out_texcoords;
in vec4 out_color;

uniform sampler2D diffuse;
uniform vec4 diffuse_color;
uniform bool font = false;
out vec4 color;

void main(){
    vec4 t = texture(diffuse, out_texcoords.xy);
	if(font){
		color = vec4(t.r) * diffuse_color;
    	color.a = t.r;
    } else {
    	color = diffuse_color * t;
    }

}
