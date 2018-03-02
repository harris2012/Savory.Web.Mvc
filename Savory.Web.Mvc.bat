nuget restore

msbuild^
 Savory.Web.Mvc.Net45\Savory.Web.Mvc.Net45.csproj^
 /t:rebuild /p:configuration=release;DocumentationFile=bin\Release\Savory.Web.Mvc.xml;DebugType=none

pushd Savory.Web.Mvc.Net45
reference^
 /csproj:Savory.Web.Mvc.Net45.csproj^
 /target:savory-lib\Savory.Web.Mvc.txt
popd

nuget pack Savory.Web.Mvc.nuspec

pause