[CmdletBinding()]
param (
	[Parameter()]
	[switch] $PreRelease,

	[Parameter()]
	[switch] $DebugBuild
)

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

$Release = 'Release'
if ($PSBoundParameters.ContainsKey('DebugBuild')) {
	$Release = 'Debug'
}

if ($PSBoundParameters.ContainsKey('PreRelease')) {
	Exec { & dotnet pack -c $Release -o .\artifacts --version-suffix=beta }
} else {
	Exec { & dotnet pack -c $Release -o .\artifacts }
}