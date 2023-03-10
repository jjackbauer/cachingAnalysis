# Caching Analysis
## Ricardo Medeiro's Graduation Project
A plataform for evaluation of caching impact in transaction authorization
## Getting Started
This project uses .NET 7.0, you need to have the .NET SDK installed to build it.
You can use the following links to guide you through the install process in the desired Operational System:

  - [Windows](https://learn.microsoft.com/en-us/dotnet/core/install/windows?tabs=net70)

  - [macOs](https://learn.microsoft.com/en-us/dotnet/core/install/macos?tabs=net70)

  - [Linux](https://learn.microsoft.com/en-us/dotnet/core/install/linux?tabs=net70)
## Building and Running the Application
The repository contains a Makefile with the most usefull commands need to make it work. It has 4 commands.
### make pub-client
it builds the client application in Realease mode, the folder that the binaries are generated will vary depending on the architecture of the system. but will be something like:

```path
client\caching-client/bin/Release/net7.0/{architecture}/publish
```

### make pub-server
it builds the server application in Realease mode, the folder that the binaries are generated will vary depending on the architecture of the system. but will be something like:

```path
server/server-rdb/bin/Release/net7.0/{architecture}}/publish/
```

### make run-client
it runs the client application, it was made for linux-x64 architecture, but you can change the path to the desired one. The client expects host and port to consume or will use default values, in the current makefile, the standard port and host are used. But it can be changed in order to access the server application remotely. 
### make run-server
it runs the server application, it was made for linux-x64 architecture, but you can change the path to the desired one,


