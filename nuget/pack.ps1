$root = (split-path -parent $MyInvocation.MyCommand.Definition) + '\..'

Write-Host "Setting .nuspec version tag to $env:APPVEYOR_BUILD_VERSION"

$content = (Get-Content $root\nuget\Cake.DoInDirectory.nuspec)
$content = $content -replace '\$version\$', $env:APPVEYOR_BUILD_VERSION

$content | Out-File $root\nuget\compiled.nuspec

& nuget pack $root\nuget\compiled.nuspec
