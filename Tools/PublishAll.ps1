$currentScriptDirectory = Split-Path -Parent $MyInvocation.MyCommand.Definition

$rootSourceDirectory = Join-Path -Path $currentScriptDirectory -ChildPath '../Source'

$packages = @(
    @{ Name = "Revolt.Net"; Version = "0.1.0" },
    @{ Name = "Revolt.Net.Core"; Version = "0.1.1" },
    @{ Name = "Revolt.Net.Commands"; Version = "0.1.1" },
    @{ Name = "Revolt.Net.WebSocket"; Version = "0.1.0" },
    @{ Name = "Revolt.Net.Rest"; Version = "0.1.0" }
)

$nugetSource = "https://api.nuget.org/v3/index.json"
$nugetApiKey = ""

foreach ($package in $packages) {

    $path = Join-Path -Path $rootSourceDirectory -ChildPath (Join-Path -Path $package.Name -ChildPath '\bin\Release')

    $nupkg = Join-Path -Path $path ($package.Name + "." + $package.Version + ".nupkg")

    Write-Host "$nupkg"

    if (!(Test-Path -Path $nupkg -PathType Leaf)) {
        throw "Error: Package file '$nupkg' does not exist."
    }

    dotnet nuget push $nupkg -k $nugetApiKey -s $nugetSource --skip-duplicate

}