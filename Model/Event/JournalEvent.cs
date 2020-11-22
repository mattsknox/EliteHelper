using System;
using System.Text.Json.Serialization;

namespace EliteHelper
{
    //Base class for journal events
    public class JournalEvent : IJournalEvent
    {
        [JsonPropertyName("timestamp")]
        public string Timestamp { get; set; }
        [JsonPropertyName("event")]
        public string Event { get; set; }
    }
}
