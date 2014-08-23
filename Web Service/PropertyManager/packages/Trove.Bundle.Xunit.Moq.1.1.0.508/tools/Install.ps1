param($installPath, $toolsPath, $package, $project)

$project.ProjectItems | ForEach { if ($_.Name -eq "dummy.txt") { $_.Remove() } }
$projectPath = Split-Path $project.FullName -Parent
Join-Path $projectPath "dummy.txt" | Remove-Item