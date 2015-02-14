// common vertex format for all mesh's

layout(location = 0) in vec3 r_Position;
layout(location = 1) in vec3 r_Normal;
layout(location = 2) in vec2 r_TexCoord;
layout(location = 3) in vec3 r_Bitangent;
layout(location = 4) in vec3 r_Tangent;
#ifdef ANIM
  layout(location = 5) in vec4 r_BlendWeight;
  layout(location = 6) in int r_BlendIndices;
#endif

