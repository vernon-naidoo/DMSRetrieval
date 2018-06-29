@ECHO ON

REM OpenCover-MSTest.bat
REM Run opencover against MSTest tests in your test project and show report of code coverage

REM Build Variables
SET BUILDMODE=Release
SET UNITTESTPROJECT=%WORKSPACE%\DSV.Webservice.Central.DMSRetrieval.Tests
SET ASSEMBLIES=DSV.Webservice.Central.DMSRetrieval
SET MSTEST_PATH=%PROGRAMFILES(X86)%\Microsoft Visual Studio 14.0\Common7\IDE

echo BUILDMODE: %BUILDMODE%
echo UNITTESTPROJECT: %UNITTESTPROJECT%
echo ASSEMBLIES: %ASSEMBLIES%
echo MSTEST_PATH: %MSTEST_PATH%
