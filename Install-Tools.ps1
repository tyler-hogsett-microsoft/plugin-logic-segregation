$currentModule = Get-InstalledModule `
	-Name Microsoft.Xrm.Data.Powershell
if ($currentModule -eq $null) {
	Write-Host "installing Microsoft.Xrm.Data.Powershell"
	Install-Module `
		-Name Microsoft.Xrm.Data.Powershell `
		-Scope CurrentUser `
		-Force
} else {
	$newestVersion = (Find-Module Microsoft.Xrm.Data.Powershell).Version.ToString()
	$currentVersion = $currentModule.Version.ToString()
	if ($newestVersion -ne $currentVersion) {
		Write-Host "updating Microsoft.Xrm.Data.Powershell"
		Update-Module `
			-Name Microsoft.Xrm.Data.Powershell `
			-Scope CurrentUser `
			-Force
	}
}

$targetNugetExe = ".\nuget.exe"
Set-Alias nuget $targetNugetExe -Scope Global -Verbose
if (Test-Path $targetNugetExe) {
	nuget update -self
} else {
	$sourceNugetExe = "https://dist.nuget.org/win-x86-commandline/latest/nuget.exe"
	Invoke-WebRequest $sourceNugetExe -OutFile $targetNugetExe
}

##
##Download CoreTools
##
$toolFolder = (Get-Item .\Tools\Microsoft.CrmSdk.CoreTools.*)
$currentVersion = $toolFolder.Name -replace "Microsoft.CrmSdk.CoreTools.", ""
$newestVersion = (nuget list packageId:Microsoft.CrmSdk.CoreTools) -Replace "Microsoft.CrmSdk.CoreTools ", ""
if ($currentVersion -ne $newestVersion) {
	Remove-Item `
		-Path $toolFolder `
		-Force `
		-Recurse
	./nuget install Microsoft.CrmSdk.CoreTools -O .\Tools
}

Stop-Process -Id $PID