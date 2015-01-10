
layout(location=0) in vec3 position;
layout(location=1) in vec2 texcoord;

out vec4 out_position;
out vec2 out_texcoord;


   void main() {
      out_position = vec4(position,1.0f);
      out_texcoord = texcoord;

   }
