precision mediump float;

in vec3 normalInterp;
in vec3 vertPos;

uniform int mode;

const vec3 lightPos = vec3(1.0, 1.0, 1.0);
const vec3 diffuseColor = vec3(0.5, 0.0, 0.0);
const vec3 specColor = vec3(1.0, 1.0, 1.0);

void main() {

	vec3 normal = normalize(normalInterp);
	vec3 lightDir = normalize(lightPos - vertPos);

	float lambertian = max(dot(lightDir, normal), 0.0);
	float specular = 0.0;

	if (lambertian > 0.0) {

		vec3 reflectDir = reflect(-lightDir, normal);
		vec3 viewDir = normalize(-vertPos);

		float specAngle = max(dot(reflectDir, viewDir), 0.0);
		specular = pow(specAngle, 4.0);

		// the exponent controls the shininess (try mode 2)
		if (mode == 2)  specular = pow(specAngle, 16.0);

		// according to the rendering equation we would need to multiply
		// with the the "lambertian", but this has little visual effect
		if (mode == 3) specular *= lambertian;

		// switch to mode 4 to turn off the specular component
		if (mode == 4) specular *= 0.0;

	}

	gl_FragColor = vec4(lambertian*diffuseColor +
		specular*specColor, 1.0);
}
