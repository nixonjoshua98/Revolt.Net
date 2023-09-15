$currentScriptDirectory = Split-Path -Parent $MyInvocation.MyCommand.Definition

$rootSourceDirectory = Join-Path -Path $currentScriptDirectory -ChildPath '../Source'

$packages = @(
    @{ Name = "Revolt.Net"; Version = "0.1.0" },
    @{ Name = "Revolt.Net.Core"; Version = "0.1.1" },
    @{ Name = "Revolt.Net.Commands"; Version = "0.1.1" },
    @{ Name = "Revolt.Net.WebSocket"; Version = "0.1.0" },
    @{ Name = "Revolt.Net.Rest"; Version = "0.1.0" }
)

#$nugetSource = "https://api.nuget.org/v3/index.json"
$nugetSource = "C:\\Program Files (x86)\\Microsoft SDKs\\NuGetPackages\\"

$nugetApiKey = ""

foreach ($package in $packages) {

    $path = Join-Path -Path $rootSourceDirectory -ChildPath (Join-Path -Path $package.Name -ChildPath '\bin\Release')

    $nupkg = Join-Path -Path $path ($package.Name + "." + $package.Version + ".nupkg")

    if (!(Test-Path -Path $nupkg -PathType Leaf)) {
        throw "Error: Package file '$nupkg' does not exist."
    }

    # Assume local folder
    if ($nugetApiKey -Like "") {
        dotnet nuget push $nupkg -s $nugetSource --skip-duplicate
    }

    # Assume online source
    else {
        dotnet nuget push $nupkg -k $nugetApiKey -s $nugetSource --skip-duplicate
    }

}