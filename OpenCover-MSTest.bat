@ECHO ON

REM OpenCover-MSTest.bat
REM Run opencover against MSTest tests in your test project and show report of code coverage

REM Build Variables
SET BUILDMODE=DEBUG
SET UNITTESTPROJECT=%WORKSPACE%\DSV.Webservice.Central.DMSRetrieval.Tests
SET ASSEMBLIES=DSV.Webservice.Central.DMSRetrieval
SET MSTEST_PATH=%PROGRAMFILES(X86)%\Microsoft Visual Studio 14.0\Common7\IDE

REM 'Clean Unit Tests Reports'
if exist "%WORKSPACE%\TestResults" (
rd /S /Q TestResults
)
md TestResults\HTMLJSReport

REM 'Run the .NET Unit Tests with Coverage.'
"C:\Tools\OpenCover\OpenCover.Console.exe" -target:"%MSTEST_PATH%\MSTest.exe" -targetargs:"/nologo /noisolation /testcontainer:\"%UNITTESTPROJECT%\bin\%BUILDMODE%\%ASSEMBLIES%.Tests.dll\"" -filter:"+[%ASSEMBLIES%*]*" -register -mergebyhash -hideskipped:Filter -output:"TestResults\OpenCoverCodeCoverage.xml"
