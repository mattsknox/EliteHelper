using System;
using System.Text.Json.Serialization;

namespace EliteHelper.Event
{
    public class CommanderEvent : JournalEvent
    {
        public string FID { get; set; }
        public string Name { get; set; }
    }
}
