function Exec {
	[CmdletBinding()]
	param (
		[Parameter(Mandatory, Position = 0)]
		[ScriptBlock] $Cmd,

		[Parameter(Position = 1)]
		[string] $ErrorMessage = ("Bad Command: {0}" -f $cmd)
	)
	process {
		& $Cmd
		if ($lastexitcode -ne 0) {
			throw ("Exec: " + $ErrorMessage)
		}
	}
}

if (Test-Path .\artifacts) {
	Remove-Item .\artifacts -Force -Recurse
}

Get-ChildItem -Path .\src -Directory | ForEach-Object {
	Remove-Item $_\bin -Recurse -Force
	Remove-Item $_\obj -Recurse -Force
}

Exec { & dotnet pack -c Release -o .\artifacts }