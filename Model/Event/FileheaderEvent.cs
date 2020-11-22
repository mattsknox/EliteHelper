using System;
using System.Text.Json.Serialization;

namespace EliteHelper
{
    public class FileheaderEvent : JournalEvent
    {
        [JsonPropertyName("part")]
        public int Part { get; set; }

        [JsonPropertyName("language")]
        public string Language { get; set; }

        [JsonPropertyName("gameversion")]
        public string GameVersion { get; set; }

        [JsonPropertyName("build")]
        public string Build { get; set; }
    }
}
