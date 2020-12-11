using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace EliteHelper
{
    public class UnknownJournalEventReport
        {
            [JsonPropertyName("logName")]
            public string LogName { get; set; }
            [JsonPropertyName("newEventData")]
            public List<JournalEventInfo> NewEventData { get; set; }
        }
}