// common vertex format for all mesh's

layout(location = 0) in vec3 position;
layout(location = 1) in vec3 normal;
layout(location = 2) in vec2 texCoord;
#ifdef ANIM
  #ifndef EX
  #define EX
  #endif

  layout(location = 6) in int boneIndex;
  layout(location = 5) in vec4 weight;
  #ifdef EX
  layout(location = 3) vec3 bitangent;
  layout(location = 4) vec3 tangent;
  #endif
#endif

