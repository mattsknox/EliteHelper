using System.Collections.Generic;

namespace EliteHelper.Event
{
    public class CargoEvent : JournalEvent
    {
        public string Vessel { get; set; }
        public int Count { get; set; }
        public List<InventoryEntry> Inventory { get; set;}
    }
}