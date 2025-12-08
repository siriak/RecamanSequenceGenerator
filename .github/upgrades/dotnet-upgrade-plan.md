# .NET 10.0 Upgrade Plan

## Execution Steps

Execute steps below sequentially one by one in the order they are listed.

1. Validate that a .NET 10.0 SDK required for this upgrade is installed on the machine and if not, help to get it installed.
2. Ensure that the SDK version specified in global.json files is compatible with the .NET 10.0 upgrade.
3. Upgrade RecamanSequenceGenerator\RecamanSequenceGenerator.csproj
4. Upgrade RecamanSequenceGenerator.Tests\RecamanSequenceGenerator.Tests.csproj
5. Run unit tests to validate upgrade in the projects listed below:
   - RecamanSequenceGenerator.Tests\RecamanSequenceGenerator.Tests.csproj

## Settings

This section contains settings and data used by execution steps.

### Excluded projects

Table below contains projects that do belong to the dependency graph for selected projects and should not be included in the upgrade.

| Project name                                   | Description                 |
|:-----------------------------------------------|:---------------------------:|
| (none)                                         |                             |

### Aggregate NuGet packages modifications across all projects

NuGet packages used across all selected projects or their dependencies that need version update in projects that reference them.

| Package Name                        | Current Version | New Version | Description                                   |
|:------------------------------------|:---------------:|:-----------:|:----------------------------------------------|
| Newtonsoft.Json                     | 13.0.2          | 13.0.4      | Recommended for .NET 10.0                     |

### Project upgrade details

This section contains details about each project upgrade and modifications that need to be done in the project.

#### RecamanSequenceGenerator\RecamanSequenceGenerator.csproj modifications

Project properties changes:
  - Target framework should be changed from `net5.0` to `net10.0`

NuGet packages changes:
  - Newtonsoft.Json should be updated from `13.0.2` to `13.0.4` (*recommended for .NET 10.0*)

#### RecamanSequenceGenerator.Tests\RecamanSequenceGenerator.Tests.csproj modifications

Project properties changes:
  - Target framework should be changed from `net5.0` to `net10.0`
