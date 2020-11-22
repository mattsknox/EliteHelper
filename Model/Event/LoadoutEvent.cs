using System.Collections.Generic;

namespace EliteHelper
{
    public class LoadoutEvent : JournalEvent
    {
        public string Ship { get; set; }
        public int ShipID { get; set; }
        public string ShipName { get; set; }
        public string ShipIdent { get; set; }
        public long HullValue { get; set; }
        public long ModulesValue { get; set; }
        public float HullHealth { get; set; }
        public float UnladenMass { get; set; }
        public int CargoCapacity { get; set; }
        public float MaxJumpRange { get; set; }
        public FuelCapacityEntry FuelCapacity { get; set; }
        public long Rebuy { get; set; }
        public List<ModuleEntry> Modules { get; set; }
    }
}