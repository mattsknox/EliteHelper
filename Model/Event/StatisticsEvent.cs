namespace EliteHelper.Event
{
    //In case more stastistics pop up:
    // Regex to transform properties from JSON: 
    //  "(.*)".*
    //  public long $1 { get; set; }

    // Regex to transform JSON to class files
    // "(.*)": \{
    // public class $1 {
    public class StatisticsEvent : JournalEvent
    {
        public BankAccountStatistics Bank_Account { get; set; }
        public CombatStatistics Combat { get; set; }
        public CrimeStatistics Crime { get; set; }
        public SmugglingStatistics Smuggling { get; set; }
        public TradingStatistics Trading { get; set; }
        public MiningStatistics Mining { get; set; }
        public ExplorationStatistics Exploration { get; set; }
        public PassengersStatistics Passengers { get; set; }
        public SearchAndRescueStatistics Search_And_Rescue { get; set; }
        public CraftingStatistics Crafting { get; set; }
        public CrewStatistics Crew { get; set; }
        public MulticrewStatistics Multicrew { get; set; }
        public MaterialTraderStatistics Material_Trader_Stats { get; set; }
        public CQCStatistics CQC { get; set; }
    }
    
}