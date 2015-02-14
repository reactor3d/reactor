struct Light
{
	vec3 Position;
	vec4 Color;
	float Radius;
	float Phi;
	float Theta;
	int LightType;

};

uniform Light r_Lights[4];

uniform sampler2D diffuse;
uniform sampler2D ambient;
uniform sampler2D normal;
uniform sampler2D specular;
uniform sampler2D glow;
uniform sampler2D detail;

uniform vec4 diffuse_color;
uniform vec4 ambient_color;
uniform vec4 specular_color;
uniform vec4 glow_color;
uniform vec4 alpha_color;