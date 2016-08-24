out vec4 color;
in vec2 TexCoords;

uniform sampler2D diffuse;
uniform float exposure = 1.1f;

void main()
{             
    const float gamma = 2.2;
    vec3 hdrColor = texture(diffuse, TexCoords).rgb;

    // reinhard
    vec3 result = hdrColor / (hdrColor + vec3(1.0));
    // exposure
    //vec3 result = vec3(1.0) - exp(-hdrColor * exposure);
    // also gamma correct while we're at it       
    result = pow(result, vec3(1.0 / gamma));
    color = vec4(result, 1.0f);
}