#!/bin/bash

NAME="reactor-native"

echo -e "\x1b[32mDependency compilation starting...\n\x1b[0m"
BUILD_SRC=$PWD
rm -rf target
mkdir target

# BUILD SDL2
cd sdl
rm -rf target
mkdir target
cd target
cmake -B build -D CMAKE_BUILD_TYPE=Release -D BUILD_SHARED_LIBS=ON -D CMAKE_INSTALL_PREFIX=$BUILD_SRC/target/install ..
cmake --build build
cmake --build build --target install

# BUILD FREETYPE2
cd ../../freetype2
rm -rf target
mkdir target
cd target
cmake -B build -D CMAKE_BUILD_TYPE=Release -D BUILD_SHARED_LIBS=ON -D CMAKE_INSTALL_PREFIX=$BUILD_SRC/target/install ..
cmake --build build
cmake --build build --target install

# BUILD ASSIMP
cd ../../assimp
rm -rf target
mkdir target
cd target
cmake -B build -D ASSIMP_BUILD_TESTS=OFF -D BUILD_SHARED_LIBS=ON -D ASSIMP_NO_EXPORT=ON -D CMAKE_BUILD_TYPE=Release -D CMAKE_INSTALL_PREFIX=$BUILD_SRC/target/install ..
cmake --build build
cmake --build build --target install

# COMBINE STATIC LIBRARIES
cd $BUILD_SRC/target/install/lib


echo "Libraries built..."
ls *.dylib

echo "Press Enter to continue"
read line
echo "Copying libraries to Red src tree..."
cp *.dylib ../../../../Red

echo -e "\x1b[32;5mDependency compilation complete!\n\x1b[0m"

