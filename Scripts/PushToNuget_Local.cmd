Echo Creates Debug Packages and pushes to Local Nuget Repo

rem #msbuild /t:pack /p:Configuration=Debug
dotnet pack -o ..\packages ..\src\FluentResults

rem dotnet msbuild /p:Configuration=Debug ..\src\FluentResults.Extensions.AspNetCore
dotnet pack -o ..\packages ..\src\FluentResults.Extensions.AspNetCore

rem dotnet msbuild /p:Configuration=Debug ..\src\FluentResults.Extensions.FliuentAssertions
dotnet pack -o ..\packages ..\src\FluentResults.Extensions.FluentAssertions

for %%n in (..\packages\*.nupkg) do  dotnet nuget push -s d:\a_dev\LocalNugetPackages "%%n"
