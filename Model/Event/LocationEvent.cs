using System.Collections.Generic;

namespace EliteHelper.Event
{
    public class LocationEvent : JournalEvent
    {
        public bool Docked { get; set; }
        public string StationName { get; set; }
        public string StationType { get; set; }
        public long MarketID { get; set; }
        public Faction StationFaction { get; set; }
        public string StationGovernment { get; set; }
        public string StationGovernment_Localised { get; set; }
        public List<string> StationServices { get; set;}
        public string StationEconomy { get; set; }
        public string StationEconomy_Localised { get; set; }
        public List<EconomyStatus> StationEconomies { get; set; }
        public string StarSystem { get; set; }
        public long SystemAddress { get; set; }
        public List<float> StarPos { get; set; }
        public string SystemAllegiance { get; set; }
        public string SystemEconomy { get; set; }
        public string SystemEconomy_Localised { get; set; }
        public string SystemSecondEconomy { get; set; }
        public string SystemSecondEconomy_Localised { get; set; }
        public string SystemGovernment { get; set; }
        public string SystemGovernment_Localised { get; set; }
        public string SystemSecurity { get; set; }
        public string SystemSecurity_Localised { get; set; }
        public long Population { get; set; }
        public string Body { get; set; }
        public int BodyID { get; set; }
        public string BodyType { get; set; }
        public List<Faction> Factions { get; set; }
        public Faction SystemFaction { get; set; }
        public List<ConflictEntry> Conflicts { get; set; }
    }
}