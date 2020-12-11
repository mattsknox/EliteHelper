using System;
using System.Text.Json.Serialization;

namespace EliteHelper
{
    public class Journal
    {
        [JsonPropertyName("filename")]
        public string Filename { get; set; }
        [JsonPropertyName("displayName")]
        public string DisplayName { get; set; }
    }
}
