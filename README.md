> [!IMPORTANT]
> Version 9.1.0 has just been released, however documentaiton is still being updated

# LSCore
Free and open-source .NET Api framework
A .NET libraries which makes building your API faster and easier.
Wiki can be found on [GitHub](https://github.com/LimitlessSoft/LSCore/wiki)
Join discord for discussions: https://discord.gg/PTWERkBV

# Activity
![GitHub Release](https://img.shields.io/github/v/release/LimitlessSoft/LSCore)

# Contribution
To contribute, fork this project, branch from `advance/X.X.X (indicating next version/patch)`, make change and make PR into that branch branch
Best thing to do is create issue and discuss change there until someone create protocol for the contributiors.
Since there is no established protocol, your change may not be accepted.
Code merged into `main` is automatically packed and pushed to nuget

# Upgrading nuget version
All projects have synchronized versions. When development is done, to upgrade versions go to `/tools/version-upgrade` and run `node version-upgrade.js`
This tool will update version number inside all projects and commit upgrade with appropriate message
If you want to upgrade minor version, add --upgrade-minor at the end of the command
Important!!! - run from withing the folder!

