using System.Text.Json.Serialization;

namespace EliteHelper
{
    public class JournalEventInfo
    {
        [JsonPropertyName("logIndex")]
        public int LogIndex { get; set; }
        [JsonPropertyName("eventName")]
        public string EventName { get; set; }
    }
}