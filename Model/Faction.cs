using System.Collections.Generic;

namespace EliteHelper
{
    public class Faction
    {   
        public string Name { get; set; }
        public string FactionState { get; set; }
        public string Government { get; set; }
        public float Influence { get; set; }
        public string Allegiance { get; set; }
        public string Happiness { get; set; }
        public string Happiness_Localised { get; set; }
        public float MyReputation { get; set; }
        public List<FactionStateEntry> RecoveringStates { get; set; }
        public List<FactionStateEntry> ActiveStates { get; set; }
    }
}