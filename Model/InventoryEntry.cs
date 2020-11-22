//"Inventory":[ { "Name":"drones", "Name_Localised":"Limpet", "Count":147, "Stolen":0 } ]

using System;
using System.Text.Json.Serialization;

namespace EliteHelper
{
    public class InventoryEntry
    {
        public string Name { get; set; }
        public string Name_Localised { get; set; }
        public int Count { get; set; }
        public int Stolen { get; set; }
    }
}
