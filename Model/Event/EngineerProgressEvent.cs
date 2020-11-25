using System.Collections.Generic;

namespace EliteHelper.Event
{
    public class EngineerProgressEvent : JournalEvent
    {
        public List<EngineerEntry> Engineers { get; set; }
    }
}