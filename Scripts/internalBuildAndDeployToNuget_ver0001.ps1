param (
    [string[]]$projects,
	[switch]$useDebug = $false
)

$configuration = "Release"

if ($useDebug) {
	$configuration = "Debug"
}


# Make sure we have the project Folder Name variable.  
if ($projects -eq $null)
{
	Write-Host "[Error]  Failed to specify the list of projects to process.  This is required!" -ForegroundColor Red
	return
}


# Set the root folder for development projects
$devRoot = "D:\a_dev\"
if ($Env:SlugEnt_DevRoot -eq $null)
{
	Write-Host "[Warn]  Unable to locate the Sheakley_DevRoot environment variable.  Assuming it is D:\a_dev.  If this is incorrect please set the environment variable!" -ForegroundColor Yellow
}
else {
	$devRoot = $Env:SlugEnt_DevRoot
}


# We need the root folder of the Script.  
$scriptPath = $PSScriptRoot
Write-Host "The script is running from: $scriptPath"


# Get the Solution Root and src folders
$solutionRoot = [System.IO.Path]::GetDirectoryName($scriptpath)
$src = Join-Path $solutionRoot "src"


# Compute the Nuget Local Repository and Create it if it does not exist.
$nugetLocal = Join-Path $devRoot "LocalNugetPackages" 
New-Item -ItemType Directory -Force -Path $nugetLocal

# Compute Local Package directory for Solution and make any directories
$solutionPackages = Join-Path $solutionRoot "Packages"
New-Item -ItemType Directory -Force -Path $solutionPackages

# Write out the operating variables
Write-Host "A_Dev:              $devRoot"
Write-Host "Solution Root:      $solutionRoot"
Write-Host "Src:                $src"
Write-Host "Solution Packages:  $solutionPackages"
Write-Host "Local Nuget Repo:   $nugetLocal"

Write-Host ""
Write-Host "Will process the following Projects:"
foreach ($project in $projects) {
	Write-Host "Project Name:       $project" -ForegroundColor Cyan
}


# Compile the projects
foreach ($project in $projects) {
	Write-Host "-->  Compiling $project ..." -ForegroundColor Cyan
	$p = Join-Path $src $project
	dotnet msbuild /p:Configuration=$configuration $p
}
Write-Host 


# Make Nuget packages for each project
foreach ($project in $projects) {
	Write-Host "-->  Building Nuget Package for $project  ..." -ForegroundColor Cyan
	$p = Join-Path $src $project
	dotnet pack -o $solutionPackages --configuration $configuration $p 
}


# Copy Nuget package(s) to Local Repo
Write-Host
Write-Host "Moving Nuget DLL Packages to Repository $nugetLocal" -ForegroundColor Blue
Get-ChildItem -Path $solutionPackages -Filter *.nupkg | Copy-ITem -Destination $nugetLocal

Write-Host "Moving Nuget Documentation Packages to Repository $nugetLocal" -ForegroundColor Blue
Get-ChildItem -Path $solutionPackages -Filter *.snupkg | Copy-ITem -Destination $nugetLocal
Write-Host "Done!"
