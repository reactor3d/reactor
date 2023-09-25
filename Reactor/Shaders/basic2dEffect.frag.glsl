in vec2 out_texcoords;
in vec4 out_color;

uniform sampler2D diffuse;
uniform vec4 color;
out vec4 out_color;

void main(){
    vec4 t = texture(diffuse, out_texcoords.xy).rgba;
    out_color = t * color;
}
