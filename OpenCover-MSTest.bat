@ECHO OFF

REM OpenCover-MSTest.bat

REM Run opencover against MSTest tests in your test project and show report of code coverage

REM Build Variables
SET BUILDMODE=Release
SET UNITTESTPROJECT=%WORKSPACE%\DSV.Webservice.Central.DMSRetrieval.Tests
SET ASSEMBLIES=DSV.Webservice.Central.DMSRetrieval
SET MSTEST_PATH=%PROGRAMFILES(X86)%\Microsoft Visual Studio 14.0\Common7\IDE

REM 'Clean Unit Tests Reports'
rd /S /Q TestResults
md TestResults\HTMLJSReport

REM 'Run the .NET Unit Tests with Coverage.'
"C:\Tools\OpenCover\OpenCover.Console.exe" -target:"%MSTEST_PATH%\MSTest.exe" -targetargs:"/nologo /noisolation /testcontainer:%UNITTESTPROJECT%\bin\%BUILDMODE%\%UNITTESTPROJECT%.dll /resultsfile:TestResults\UnitTestReport.trx" -filter:"+[%ASSEMBLIES%*]*" -register -mergebyhash -hideskipped:Filter -output:"TestResults\OpenCoverCodeCoverage.xml"

REM 'Convert MSTest report to JUnit report'
"C:\Tools\MSTestToJUnit\msxsl.exe" TestResults\UnitTestReport.trx C:\Tools\MSTestToJUnit\mstest-to-junit.xsl -o TestResults\UnitTestsJUnitReport.xml

REM 'Convert OpenConver report to Cobertura report'
"C:\Tools\OpenCoverToCobertura\OpenCoverToCoberturaConverter.exe" -input:TestResults\OpenCoverCodeCoverage.xml -output:TestResults\CoberturaCodeCoverage.xml -sources:%WORKSPACE%

REM 'Create HTML Report from OpenCover report.'
"C:\Tools\ReportGenerator\ReportGenerator.exe" -reports:TestResults\CoberturaCodeCoverage.xml -targetDir:TestResults\HTMLCSReport
