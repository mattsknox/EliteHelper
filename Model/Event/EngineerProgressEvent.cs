using System.Collections.Generic;

namespace EliteHelper
{
    public class EngineerProgressEvent : JournalEvent
    {
        public List<EngineerEntry> Engineers { get; set; }
    }
}