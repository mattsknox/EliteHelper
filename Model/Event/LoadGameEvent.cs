namespace EliteHelper.Event
{
    public class LoadGameEvent : JournalEvent
    {
        public string FID { get; set; }
        public string Commander { get; set; }
        public bool Horizons { get; set; }
        public string Ship { get; set; }
        public int ShipID { get; set; }
        public string ShipName { get; set; }
        public string ShipIdent { get; set; }
        public string FuelLevel { get; set; }
        public string FuelCapacity { get; set; }
        public string GameMode { get; set; }
        public string Group { get; set; }
        public long Credits { get; set; }
        public long Loan { get; set; }
    }
}