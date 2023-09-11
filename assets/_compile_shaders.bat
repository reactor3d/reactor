@ECHO OFF
ECHO Compiling Shaders in %cd%
glslc -fshader-stage=vert ./shaders/tri-vert.glsl  -o ./shaders/tri-vert.spv
glslc -fshader-stage=frag ./shaders/tri-frag.glsl -o ./shaders/tri-frag.spv
@ECHO ON