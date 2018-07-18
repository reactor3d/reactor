# README #

Reactor is a graphics engine that runs on OpenTK and C#.  It targets Windows, Linux, OSX, iOS, and Android.  It's extremely lightweight and barebones at the moment, but work is underway to flesh it out.  This is version 2.0 of Reactor3D, originally an XNA Engine, now a pure OpenGL engine as a complete rewrite.

### What is this repository for? ###

* Mainstream repository for the Reactor graphics engine
* 0.0.2

### How do I get set up? ###

* Clone the repository
* Load up the solution in either Visual Studio 2015 or in Xamarin Studio or Rider IDE.
* Refresh NuGet packages
* Build

### Contribution guidelines ###

* Classes should wrap OpenGL as close to the main functions as possible and not create "management" classes other than the main factories and scene.  This is to reduce garbage and keep the engine running as fast as possible.
* Casting should be avoided at all costs except for initial loading, but never during the event loops.
* Follow the Microsoft C# Coding Standards.
* Properties should be optimized for reads.
* Structs should be aligned.
* Pointers are only allowed within the engine and can not be exposed through the API.
* Fork this repo and provide pull requests in order to add code.


Copyright (c) Reiser Games LLC, 2006-2018.
