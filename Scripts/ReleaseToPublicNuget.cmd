Echo Creates Release Packages

set packages="..\packages\release"

set program="..\src\FluentResults"
dotnet msbuild /p:Configuration=Release %program%
dotnet pack -o %packages% %program%

rem dotnet msbuild /p:Configuration=Debug ..\src\ResourceHealthChecker.FileSystem
set program="..\src\FluentResults.Extensions.AspNetCore"
dotnet pack -o %packages% %program%

rem dotnet msbuild /p:Configuration=Debug ..\src\ResourceHealthChecker.SqlServer
set program="..\src\FluentResults.Extensions.FluentAssertions"
dotnet pack -o %packages% %program%

REM - Push Locally
REM for %%n in (..\packages\*.nupkg) do  dotnet nuget push -s d:\a_dev\LocalNugetPackages "%%n"

Rem 