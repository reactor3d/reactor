# README #

Reactor is a graphics engine that runs on OpenTK and C#.  It targets Windows, Linux, OSX, iOS, and Android.  It's extremely lightweight and barebones at the moment, but work is underway to flesh it out.  This is version 2 of Reactor found here (http://www.github.com/gabereiser/reactor3d)

### What is this repository for? ###

* Mainstream repository for the Reactor graphics engine
* 0.0.1

### How do I get set up? ###

* Clone the repository
* Load up the solution in either Visual Studio 2015 or in Xamarin Studio
* Refresh NuGet packages
* Build

### Contribution guidelines ###

* Classes should wrap OpenGL as close to the main functions as possible and not create "management" classes other than the main factories and scene.  This is to reduce garbage and keep the engine running as fast as possible.
* Pointers are only allowed within the engine and can not be exposed through the API.
* Fork this repo and provide merge requests in order to add code.

### Who do I talk to? ###

* Speak with Gabriel Reiser (gabe@reisergames.com) on what is currently needed or [visit the roadmap](http://code.reisergames.com/reactor/wiki)