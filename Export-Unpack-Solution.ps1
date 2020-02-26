$solutionName = "custom_full_name"

$conn = Get-CrmConnection -InteractiveMode

if (Test-Path ./packed-solutions) {
	Remove-Item `
		-Path ./packed-solutions `
		-Recurse `
		-Force
}

mkdir packed-solutions

Export-CrmSolution `
	-conn $conn `
	-SolutionName $solutionName `
	-SolutionFilePath ./packed-solutions `
	-SolutionZipFileName "$solutionName.zip"
Export-CrmSolution `
	-conn $conn `
	-SolutionName $solutionName `
	-SolutionFilePath ./packed-solutions `
	-SolutionZipFileName "$($solutionName)_managed.zip" `
	-Managed

$solutionPackagerPath = (Get-ChildItem -Path ".\Tools\Microsoft.CrmSdk.CoreTools*" -Filter SolutionPackager.exe -Recurse).FullName
Set-Alias SolutionPackager $solutionPackagerPath -Scope Global -Verbose

SolutionPackager `
	/action:Extract `
	/zipfile:packed-solutions/$solutionName.zip `
	/packagetype:Both `
	/folder:SolutionContents `
	/map:map.xml `
	/allowDelete:Yes

#Stop-Process -Id $PID