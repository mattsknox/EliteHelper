namespace EliteHelper.Event
{
    public class ReceiveTextEvent : JournalEvent
    {
        public string From { get; set; }
        public string Message { get; set; }
        public string Message_Localised { get; set; }
        public string Channel { get; set; }
    }
}