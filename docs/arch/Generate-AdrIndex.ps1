#Requires -Version 7
#Requires -PSEdition Core

<#
    .DESCRIPTION
    Generates a summary table of ADRs and writes them to the README.md file in
    the script's directory.
#>

# Create an empty array to store the adr metadata
$adrs = @()

# Get all ADR files in the directory
$directory = Split-Path -Parent $MyInvocation.MyCommand.Definition
$adrFiles = Get-ChildItem -Path $directory -Filter "ADR*.md" -File

# Loop through each markdown file
foreach ($file in $adrFiles) {
    # Read the contents of the file
    $content = Get-Content -Path $file.FullName -Raw

    # Extract the title using a regular expression
    $match = $content -match "^# (.*?) - (.*?)\n"
    if ($match) {
        # Create an entry in the adrs array
        $adrs += [PSCustomObject]@{
            Id = $matches[1]
            Title = $matches[2]
            FileName = $file.Name
        }
    }
}

# Get README contents
$readmeFile = Get-ChildItem -Path $directory -Filter "README.md" -File
$readmeContents = (Get-Content -Path $readmeFile.FullName) -Split "`n"

# Find the line containing the summary title
$titleLineIndex = 0
for ($i = 0; $i -lt $readmeContents.Length; $i++) {
    if ($readmeContents[$i] -match "## Summary of ADRs") {
        $titleLineIndex = $i
    }
}

# Take all lines before and including that line
$newReadmeContents = $readmeContents[0..$titleLineIndex]
$newReadmeContents += "`n"

# Add a table header
$newReadmeContents += "| ID | Title |"
$newReadmeContents += "| --- | --- |"

# For each ADR, add a line to a table
foreach ($adr in $adrs) {
    $newReadmeContents += "| $($adr.Id) | [$($adr.Title)](./$($adr.FileName)) |"
}

Set-Content -Path $readmeFile.FullName -Value $newReadmeContents
