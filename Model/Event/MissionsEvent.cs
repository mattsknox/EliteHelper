using System.Collections.Generic;

namespace EliteHelper.Event
{
    public class MissionsEvent : JournalEvent
    {
        public List<MissionEntry> Active { get; set; }
        public List<MissionEntry> Failed { get; set; }
        public List<MissionEntry> Complete { get; set; }
    }
}