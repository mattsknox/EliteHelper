using System.Collections.Generic;

namespace EliteHelper.Event
{
    public class MaterialsEvent : JournalEvent
    {
        public List<MaterialEntry> Raw { get; set; }
        public List<MaterialEntry> Manufactured { get; set; }
        public List<MaterialEntry> Encoded { get; set; }
    }
}