$solutionName = "custom_full_name"

$conn = Get-CrmConnection -InteractiveMode

if (Test-Path ./temp) {
	Remove-Item `
		-Path ./temp `
		-Recurse `
		-Force
}

mkdir temp

Export-CrmSolution `
	-conn $conn `
	-SolutionName $solutionName `
	-SolutionFilePath ./temp `
	-SolutionZipFileName "$solutionName.zip"
Export-CrmSolution `
	-conn $conn `
	-SolutionName $solutionName `
	-SolutionFilePath ./temp `
	-SolutionZipFileName "$($solutionName)_managed.zip" `
	-Managed

$solutionPackagerPath = (Get-ChildItem -Path ".\Tools\Microsoft.CrmSdk.CoreTools*" -Filter SolutionPackager.exe -Recurse).FullName
Set-Alias SolutionPackager $solutionPackagerPath -Scope Global -Verbose

SolutionPackager `
	/action:Extract `
	/zipfile:temp/$solutionName.zip `
	/packagetype:Both `
	/folder:SolutionContents `
	/allowDelete:Yes

Stop-Process -Id $PID