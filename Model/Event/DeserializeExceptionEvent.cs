using System;
using System.Text.Json.Serialization;

namespace EliteHelper.Event
{
    public class DeserializeExceptionEvent : JournalEvent
    {
        public string Name { get; set; } = "Deserialize Exception";
        public string Message { get; set; }
    }
}
