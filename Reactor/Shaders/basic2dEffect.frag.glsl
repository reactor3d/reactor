in vec2 in_texcoords;
in vec4 in_color;

uniform sampler2D texture0;
uniform vec4 base_color;
out vec4 out_color;

void main(){
    vec4 t = texture(texture0, in_texcoords.xy).rgba;
    out_color = t * base_color * in_color;
}
