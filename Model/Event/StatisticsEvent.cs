using EliteHelper.Statistics;

namespace EliteHelper.Event
{
    //In case more statistics pop up:
    // Regex to transform properties from JSON: 
    //  "(.*)".*
    //  public long $1 { get; set; }

    // Regex to transform JSON to class files
    // "(.*)": \{
    // public class $1 {
    public class StatisticsEvent : JournalEvent
    {
        public BankAccount Bank_Account { get; set; }
        public Combat Combat { get; set; }
        public Crime Crime { get; set; }
        public Smuggling Smuggling { get; set; }
        public Trading Trading { get; set; }
        public Mining Mining { get; set; }
        public Exploration Exploration { get; set; }
        public Passengers Passengers { get; set; }
        public SearchAndRescue Search_And_Rescue { get; set; }
        public Crafting Crafting { get; set; }
        public Crew Crew { get; set; }
        public Multicrew Multicrew { get; set; }
        public MaterialTrader Material_Trader_Stats { get; set; }
        public CQC CQC { get; set; }
    }
    
}