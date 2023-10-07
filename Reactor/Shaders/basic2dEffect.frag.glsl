in vec2 texcoords;

uniform sampler2D texture0;
uniform vec4 base_color;
out vec4 out_color;

void main(){
    vec4 t = texture(texture0, texcoords).rgba;
    out_color = t * base_color;
}
