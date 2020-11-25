namespace EliteHelper.Event
{
    public class ReputationEvent : JournalEvent
    {
        public float Empire { get; set; }
        public float Federation { get; set; }
        public float Independent { get; set; }
        public float Alliance { get; set; }
    }
}