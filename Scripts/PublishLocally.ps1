# Builds the nuget package for the SlugEnt FluentAssertions
# And Copies it to Local Repository.

$Projects =@(
  'FluentResults'
  'FluentResults.Extensions.AspNetCore'
  'FluentResults.Extensions.FluentAssertions'
)
Write-Host "Script= $PSScriptRoot"

& $PSScriptRoot\internalBuildAndDeployToNuget_ver0001.ps1 $Projects