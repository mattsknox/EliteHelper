using System;
using System.Text.Json.Serialization;

namespace EliteHelper
{
    public class CommanderEvent : JournalEvent
    {
        public string FID { get; set; }
        public string Name { get; set; }
    }
}
