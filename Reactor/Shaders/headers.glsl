// common vertex format for all mesh's

layout(location = 0) in vec3 position;
layout(location = 1) in vec3 normal;
layout(location = 2) in vec2 texCoord;
layout(location = 3) in vec3 bitangent;
layout(location = 4) in vec3 tangent;
#ifdef ANIM
  layout(location = 5) in vec4 weight;
  layout(location = 6) in int boneIndex;
#endif

