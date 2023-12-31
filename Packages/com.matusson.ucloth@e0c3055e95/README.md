# Prototype Burst-based cloth simulation for Unity

![UCloth - Burst-based cloth prototype for Unity](/Editor/Icons/banner1.png "UCloth")

### <i>WARNING: This project is not production ready and no further work will be done. No PRs will be accepted. It is primarily intented to be a learning resource.</i>

<br>

## Summary
This package implements custom cloth physics for Unity. It runs on the CPU, doing most of the simulation work on a background thread (some main thread cost still applies). It utilizes the Jobs system for partial multithreading - a single cloth object can't be split into multiple threads, but two cloth objects can be simulated at the same time on different threads.

<br>

Note that by default, this is a purely visual effect and all physics support only one-way coupling (the cloth will not affect other physics objects). You can implement custom logic that interacts with the cloth. A short introduction to the API is included in [API.md](/Documentation/API.md)

<br>

The project contains many issues that would need to be fixed if you want to use it in a production environment. A list of known ones is included in [Issues.md](/Documentation/Issues.md). I do not take responsibility for any, and I will not provide support. You are free to use this project as a starting point or a basic learning resource for your own simulation.

See [Setup.md](/Documentation/Setup.md) for information on how to set up the simulation, and [UCClothReference.md](/Documentation/UCClothReference.md) for a brief explanation of all parameters.


<br>

## Installation
This repository is a package compatible with UPM. Open the Package Manager window in Unity and click the "plus" icon. Select "Add packager from git URL" and paste the link to this repository (https://github.com/Matusson/UCloth.git). Alternatively, you can also clone the repo and add package from disk.

Designed for Unity 2021.3.

<br>



## License
This project is provided as-is under the MIT license.