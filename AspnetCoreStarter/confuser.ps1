# Set the directory containing your DLLs and EXEs
$directory = "D:\MyWork\Eventus\UI_Work\Vuexy\vuexyadmin-970\vuexy-admin-v9.7.0\aspnet-core\ERPFastTrack.v2.UI.LW\AspnetCoreStarter\bin\Release\net7.0\linux-x64"

# Start the XML content for the ConfuserEx project file
$xmlContent = @"
<project outputDir="Confused" baseDir="$directory">
  <rule pattern="true">
    <protection id="rename" />
    <protection id="ctrl flow" />
    <protection id="resources" />
    <protection id="anti debug" />
    <protection id="anti tamper" />
  </rule>
"@

# Add all DLL and EXE files in the directory
Get-ChildItem -Path $directory -Filter *.dll | ForEach-Object {
    $xmlContent += "  <module path=""$($_.FullName)"" />`n"
}
Get-ChildItem -Path $directory -Filter *.exe | ForEach-Object {
    $xmlContent += "  <module path=""$($_.FullName)"" />`n"
}

# Close the XML content
$xmlContent += "</project>"

# Save the content to a .crproj file
$xmlContent | Out-File -FilePath "yourproject.crproj" -Encoding UTF8
