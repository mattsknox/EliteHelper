using System.IO;
using System.Collections.Generic;
using System.Reflection;

using EliteHelper.Event;

namespace EliteHelper.SystemApi.Services
{
    public static class FileService
    {
        static string profilePath = @"C:\Users";
        static string journalPath = @"Saved Games\Frontier Developments\Elite Dangerous";

        // static string GetJournalPath()
        // {
        //     string userName = Environment.userName;
        //     string targetPath = $@"{profilePath}\{userName}\{journalPath}";
        //     return targetPath;
        // }
        static string GetJournalPath(string userName)
        {
            string targetPath = $@"{profilePath}\{userName}\{journalPath}";
            return targetPath;
        }

        public static IEnumerable<Journal> GetJournals(string userName)
        {
            string path = GetJournalPath(userName);
            var files = Directory.GetFiles(path, "*.log");
            List<Journal> journals = new List<Journal>();
            foreach (var file in files)
            {
                string journalFilename = Path.GetFileNameWithoutExtension(file);
                string dateStamp = journalFilename.Split('.')[1];
                string year = dateStamp.Substring(0,2);
                string month = dateStamp.Substring(2,2);
                string day = dateStamp.Substring(4,2);
                string time = dateStamp.Substring(6);
                string displayName = $"{month}/{day}/{year} - {time}";
                Journal journal = new Journal()
                {
                    Filename = journalFilename,
                    DisplayName = displayName
                };
                journals.Add(journal);
            }

            return journals;
        }
        public static IEnumerable<string> GetStuff(string userName)
        {
            string path = GetJournalPath(userName);
            var files = Directory.GetFiles(path, "*.json");
            List<string> journals = new List<string>();
            foreach (var file in files)
            {
                string journal = Path.GetFileNameWithoutExtension(file);
                journals.Add(journal);
            }

            return journals;
        }

        public static JournalEvent[] GetJournalDetails (string userName, string journalName)
        {
            var journalContent = GetContent(userName, journalName);
            JournalEvent[] journalDetails = GetJournalEvents(journalContent);
            return journalDetails;
        }

        public static IJournalEvent GetJournal(string userName, string journalName, int journalIndex)
        {
            var logLine = GetLogLine(userName, journalName, journalIndex);
            var journal = ParseJournalEvent(logLine);
            return journal;
        }

        public static IJournalEvent ParseJournalEvent(string logLine)
        {
            var journalEvent = System.Text.Json.JsonSerializer.Deserialize<JournalEvent>(logLine);

            var allTypes = Assembly.GetExecutingAssembly().GetTypes();
            allTypes = typeof(JournalEvent).Assembly.GetTypes();
            List<System.Type> eventTypes = new List<System.Type>();

            string debugTypes = $"TargetEvent:{journalEvent.Event};";
            foreach(var t in allTypes)
            {
                debugTypes += $"{t.Name};";
                string eventName = $"{journalEvent.Event}Event";
                if (t.Name == eventName)
                {
                    var methods = typeof(System.Text.Json.JsonSerializer).GetMethods();
                    foreach (var method in methods)
                    {
                        if (method.IsGenericMethod
                            && method.Name == "Deserialize")
                            {
                                var parameters = method.GetParameters();
                                if (parameters.Length == 2)
                                {
                                    var param1 = parameters[0];
                                    var param2 = parameters[1];
                                    var param1Match = typeof(System.String);
                                    var param1Name = param1.Name;
                                    var param1Type = param1.ParameterType;
                                    if (param1Type == typeof(System.String)
                                        && param1Name == "json")
                                        {
                                            MethodInfo generic  = method.MakeGenericMethod(t);
                                            var options = new System.Text.Json.JsonSerializerOptions();
                                            return (IJournalEvent)generic.Invoke(null, new object [] {logLine, null});
                                        }
                                }
                            }
                            debugTypes += $"{method.Name};";
                    }
                }
            }

            return new DeserializeExceptionEvent
            {
                //Message = $"No type definition found for {journalEvent.Event}"
                Message = debugTypes
            };
        }

        public static IJournalEvent ParseJournalEventSwitch(string logLine)
        {
            var journalEvent = System.Text.Json.JsonSerializer.Deserialize<JournalEvent>(logLine);
            switch (journalEvent.Event)
            {
                case "ReceiveText":
                    return System.Text.Json.JsonSerializer.Deserialize<ReceiveTextEvent>(logLine);
                break;
                case "Cargo":
                    return System.Text.Json.JsonSerializer.Deserialize<CargoEvent>(logLine);
                break;
                case "Commander":
                    return System.Text.Json.JsonSerializer.Deserialize<CommanderEvent>(logLine);
                break;
                case "Docked":
                    return System.Text.Json.JsonSerializer.Deserialize<DockedEvent>(logLine);
                break;
                case "EngineerProgress":
                    return System.Text.Json.JsonSerializer.Deserialize<EngineerProgressEvent>(logLine);
                break;
                case "Fileheader":
                    return System.Text.Json.JsonSerializer.Deserialize<FileheaderEvent>(logLine);
                break;
                case "LoadGame":
                    return System.Text.Json.JsonSerializer.Deserialize<LoadGameEvent>(logLine);
                break;
                case "Loadout":
                    return System.Text.Json.JsonSerializer.Deserialize<LoadoutEvent>(logLine);
                break;
                case "Location":
                    return System.Text.Json.JsonSerializer.Deserialize<LocationEvent>(logLine);
                break;
                case "Materials":
                    return System.Text.Json.JsonSerializer.Deserialize<MaterialsEvent>(logLine);
                break;
                case "Missions":
                    return System.Text.Json.JsonSerializer.Deserialize<MissionsEvent>(logLine);
                break;
                case "Music":
                    return System.Text.Json.JsonSerializer.Deserialize<MusicEvent>(logLine);
                break;
                case "Progress":
                    return System.Text.Json.JsonSerializer.Deserialize<ProgressEvent>(logLine);
                break;
                case "Rank":
                    return System.Text.Json.JsonSerializer.Deserialize<RankEvent>(logLine);
                break;
                case "Reputation":
                    return System.Text.Json.JsonSerializer.Deserialize<ReputationEvent>(logLine);
                break;
                case "Shutdown":
                    return System.Text.Json.JsonSerializer.Deserialize<ShutdownEvent>(logLine);
                break;
                case "Statistics":
                    return System.Text.Json.JsonSerializer.Deserialize<StatisticsEvent>(logLine);
                break;
            }

            return journalEvent;
        }

        static JournalEvent[] GetJournalEvents(string[] journalContents)
        {
            List<JournalEvent> journalEvents = new List<JournalEvent>();
            foreach (string journalEvent in journalContents)
            {
                var logEntry = System.Text.Json.JsonSerializer.Deserialize<JournalEvent>(journalEvent);
                journalEvents.Add(logEntry);
            }
            return journalEvents.ToArray();
        }
        public static string[] GetContent(string userName, string likeFileName)
        {
            string path = GetJournalPath(userName);
            var files = Directory.GetFiles(path, $"*{likeFileName}*");
            return File.ReadAllLines(files[0]);
        }
        
        public static string GetLogLine(string userName, string timestamp, int logIndex)
        {
            string path = GetJournalPath(userName);
            var lines = File.ReadAllLines($@"{path}\Journal.{timestamp}.01.log");
            return lines[logIndex];
        }

        static List<string> GetAllJournalTypes()
        {
            List<string> journalTypes = new List<string>();

            return journalTypes;
        }
    }
}