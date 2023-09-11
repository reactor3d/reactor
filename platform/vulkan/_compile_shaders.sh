#!/bin/sh

glslc ./shaders/tri-vert.glsl -o ./shaders/tri-vert.spv
glslc ./shaders/tri-frag.glsl -o ./shaders/tri-frag.spv