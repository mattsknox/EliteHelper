using System;
using System.Text.Json.Serialization;

namespace EliteHelper.Event
{
    public class MusicEvent : JournalEvent
    {
        public string MusicTrack { get; set; }
    }
}
