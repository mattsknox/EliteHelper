set-location "C:\users\mknox\documents\code\personal01\EliteHelper\Model>"

$stats = gci Statistics

foreach ($statFile in $stats)
{
    $statFile
    $filename = $statFile.Name
    $newFilename = $filename.Replace("Statistics", "")
    "New filename $newFilename"

    "`n File Content"
    get-content $statFile.FullName
}