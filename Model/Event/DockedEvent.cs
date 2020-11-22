using System.Collections.Generic;

namespace EliteHelper
{
    public class DockedEvent : JournalEvent
    {
        public string StationName { get; set; }
        public string StationType { get; set; }
        public string StarSystem { get; set; }
        public long SystemAddress { get; set; }
        public long MarketID { get; set; }
        public Faction StationFaction { get; set; }
        public string StationGovernment { get; set; }
        public string StationGovernment_Localised { get; set; }
        public List<string> StationServices { get; set; }
        public string StationEconomy { get; set; }
        public string StationEconomy_Localised { get; set; }
        public List<EconomyStatus> StationEconomies { get; set; }
        public float DistFromStarLS { get; set; }
    }
}