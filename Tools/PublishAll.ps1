$currentScriptDirectory = Split-Path -Parent $MyInvocation.MyCommand.Definition

$rootSourceDirectory = Join-Path -Path $currentScriptDirectory -ChildPath '../Source/Revolt.Net'

$version = "0.3.1"

$packages = @(
    @{ Name = "Revolt.Net"; Version = $version },
    @{ Name = "Revolt.Net.Core"; Version = $version },
    @{ Name = "Revolt.Net.Commands"; Version = $version },
    @{ Name = "Revolt.Net.WebSocket"; Version = $version },
    @{ Name = "Revolt.Net.Rest"; Version = $version }
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