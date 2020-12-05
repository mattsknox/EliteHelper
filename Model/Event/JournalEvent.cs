using System;
using System.Text.Json.Serialization;

namespace EliteHelper.Event
{
    //Base class for journal events
    public class JournalEvent : IJournalEvent
    {
        [JsonPropertyName("timestamp")]
        public string Timestamp { get; set; }
        [JsonPropertyName("event")]
        public string Event { get; set; }
        //Line of the file to find the log entry
        public int LogIndex { get; set; }
        public string Filename { get; set; }
    }
}
