$solutionName = "custom_full_name"

$solutionPackagerPath = (Get-ChildItem -Path ".\Tools\Microsoft.CrmSdk.CoreTools*" -Filter SolutionPackager.exe -Recurse).FullName
Set-Alias SolutionPackager $solutionPackagerPath -Scope Global -Verbose

SolutionPackager `
	/action:Pack `
	/zipfile:packed-solutions/$solutionName.zip `
	/packagetype:Both `
	/map:map.xml `
	/folder:SolutionContents

Stop-Process -Id $PID