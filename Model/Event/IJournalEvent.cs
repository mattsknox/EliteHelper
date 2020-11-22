

namespace EliteHelper
{
    //For a "GetJournal" method that can return any type
    //implementing this interface
    public interface IJournalEvent
    {
        string Timestamp { get; set; }
        string Event { get; set; }
    }
}
