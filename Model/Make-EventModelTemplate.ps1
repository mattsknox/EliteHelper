$location = Get-Location


$eventName = "Shutdown"

$fileContents = @"
namespace EliteHelper
{
    public class $eventName`Event : JournalEvent
    {
        //TODO: this
    }
}
"@

$filepath = "$location\Event\$eventName`Event.cs"

[System.IO.File]::WriteAllText($filepath, $fileContents)