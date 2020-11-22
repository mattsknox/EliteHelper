using System.Collections.Generic;

namespace EliteHelper
{
    public class MaterialsEvent : JournalEvent
    {
        public List<MaterialEntry> Raw { get; set; }
        public List<MaterialEntry> Manufactured { get; set; }
        public List<MaterialEntry> Encoded { get; set; }
    }
}