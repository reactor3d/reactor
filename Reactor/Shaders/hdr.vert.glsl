
layout(location=0) in vec2 r_Position;
layout(location=1) in vec2 r_TexCoord;


out vec2 TexCoords;

void main()
{
    gl_Position = vec4(r_Position,0, 1);
    TexCoords = vec2(r_TexCoord.x, r_TexCoord.y);
}