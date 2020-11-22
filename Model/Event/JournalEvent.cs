using System;
using System.Text.Json.Serialization;

namespace EliteHelper
{
    public class JournalEvent
    {
        [JsonPropertyName("timestamp")]
        public string Timestamp { get; set; }
        [JsonPropertyName("event")]
        public string Event { get; set; }
    }
}
