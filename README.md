# LSCore
This framework aims to simplify the development of .NET applications.

Its core principles are **Simplicity** and **Abstraction**. To that end, features are divided into separate assemblies. This gives you the flexibility to include only the assemblies containing the functionality you need, preventing your codebase from being cluttered with unnecessary code.

It is free and open-sourced, do whatever you want with it.

Wiki can be found on [GitHub](https://github.com/LimitlessSoft/LSCore/wiki)
Join discord for discussions: https://discord.gg/PTWERkBV

# Latest release
![GitHub Release](https://img.shields.io/github/v/release/LimitlessSoft/LSCore)

# Contribution
To contribute, fork this project, branch from `advance/X.X.X (indicating next version/patch)`, make change and make PR into that branch branch.
If you want to implement feature, discuss it inside [Request Feature](https://github.com/LimitlessSoft/LSCore/discussions/categories/request-feature) to make sure it is accepted before you start implementing
If you have bug, report it at [Issues](https://github.com/LimitlessSoft/LSCore/issues)
Also check out `Releases/Next Release` inside [Discussions](https://github.com/LimitlessSoft/LSCore/discussions) to see wether given feature/bugfix is already being worked on

# (Maintainer Note) Upgrading nuget version
All projects have synchronized versions. When development is done, to upgrade versions go to `/tools/version-upgrade` and run `node version-upgrade.js`
This tool will update version number inside all projects and commit upgrade with appropriate message
If you want to upgrade minor version, add --upgrade-minor at the end of the command
Important!!! - run from withing the folder!

