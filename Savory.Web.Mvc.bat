nuget restore

msbuild^
 Savory.Web.Mvc\Savory.Web.Mvc.csproj^
 /t:rebuild /p:configuration=release;DocumentationFile=bin\Release\Savory.Web.Mvc.xml;DebugType=none

pushd Savory.Web.Mvc
reference^
 /csproj:Savory.Web.Mvc.csproj^
 /target:savory-lib\Savory.Web.Mvc.txt
popd

nuget pack Savory.Web.Mvc.nuspec

pause