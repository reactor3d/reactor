﻿in vec2 out_texcoords;
in vec4 out_color;

uniform sampler2D diffuse;
uniform vec4 diffuse_color;

out vec4 color;

void main(){
    vec4 t = texture(diffuse, out_texcoords.xy).rrrr;
	color = (t + (diffuse_color*0.5f));
    color.a = diffuse_color.a;

}
