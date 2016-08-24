struct Light
{
	vec3 Position;
	vec4 Color;
	float Radius;
	float Phi;
	float Theta;
	int LightType;

};

uniform Light r_Lights[6];
uniform int r_NumLights = 6;
uniform sampler2D diffuse;
uniform sampler2D ambient;
uniform sampler2D normal;
uniform sampler2D specular;
uniform sampler2D glow;
uniform sampler2D detail;
uniform sampler2D height;

uniform vec4 diffuse_color;
uniform vec4 ambient_color;
uniform vec4 specular_color;
uniform vec4 glow_color;
uniform vec4 alpha_color;

float blinnPhongSpecular(
  vec3 lightDirection,
  vec3 viewDirection,
  vec3 surfaceNormal,
  float shininess) {

  //Calculate Blinn-Phong power
  vec3 H = normalize(viewDirection + lightDirection);
  return pow(max(0.0, dot(surfaceNormal, H)), shininess);
}