Contains a collection of useful utility scripts to make developers' lives easier.


## Requirements
* PowerShell (https://learn.microsoft.com/en-us/powershell/scripting/install/installing-powershell?view=powershell-7.4)
* GitHub CLI (https://cli.github.com/) authenticated with a user account that has access to the DfE GitHub organisation

## Scripts

* `setup-local-nuget-src.ps1` - Sets up a local NuGet source used for local FH package development.
* `npm-install.sh` - Runs npm install in every UI project which triggers wwwroot to be copied from web-components
