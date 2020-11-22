using System.Collections.Generic;

namespace EliteHelper
{
    public class EngineeringModEntry
    {
        public string Engineer { get; set; }
        public long EngineerID { get; set; }
        public long BlueprintID { get; set; }
        public string BlueprintName { get; set; }
        public int Level { get; set; }
        public float Quality { get; set; }
        public string ExperimentalEffect { get; set; }
        public string ExperimentalEffect_Localised { get; set; }
        public List<EngineeringModification> Modifiers { get; set; }
    }
}