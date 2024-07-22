# LSCore
### Free and open-source .NET Api framework

A .NET libraries which makes building your API faster and easier.

Check out sample-projects to see detailed implemetations

Join discord for discussions: https://discord.gg/PTWERkBV

Project has some missing parts since I am moving them from internal projects to here, so expect more things in future. If you have some idea which you want to include, feel free to contribute.

# Activity
![GitHub last commit (branch)](https://img.shields.io/github/last-commit/LimitlessSoft/LSCore/develop?label=Last%20develop%20commit)
![GitHub Release](https://img.shields.io/github/v/release/LimitlessSoft/LSCore)
<br>
![Nuget](https://img.shields.io/nuget/v/LSCore.Contracts?label=LSCore.Contracts%20nuget)
![Nuget](https://img.shields.io/nuget/dt/LSCore.Contracts?label=LSCore.Contracts%20nuget)
<br>
![Nuget](https://img.shields.io/nuget/v/LSCore.Domain?label=LSCore.Domain%20nuget)
![Nuget](https://img.shields.io/nuget/dt/LSCore.Domain?label=LSCore.Domain%20nuget)
<br>
![Nuget](https://img.shields.io/nuget/v/LSCore.Framework?label=LSCore.Framework%20nuget)
![Nuget](https://img.shields.io/nuget/dt/LSCore.Framework?label=LSCore.Framework%20nuget)
<br>
![Nuget](https://img.shields.io/nuget/v/LSCore.Repository?label=LSCore.Repository%20nuget)
![Nuget](https://img.shields.io/nuget/dt/LSCore.Repository?label=LSCore.Repository%20nuget)

# Contribution
To contribute fork this project, branch from `develop`, make change and make PR onto `develop` branch
Since there is no established protocol, your change may not be accepted.
Best thing to do is create issue and discuss change there until someone create protocol for the contributiors.
Code merged into `main` is automatically packed and pushed to nuget

# Upgrading nuget version
All projects have synchronized versions. When development is done, to upgrade versions go to `/tools/version-upgrade` and run `node version-upgrade.js`
This tool will update version number inside all projects and commit upgrade with appropriate message
Important!!! - run from withing the folder!
