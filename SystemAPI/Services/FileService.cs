using System.IO;
using System.Collections.Generic;
using System.Reflection;
using System.Text.Json;

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

        /*
            Return all of the events in a single log file, expressed
            as JournalEvents
        */
        public static JournalEvent[] GetJournalFileEvents (string userName, string journalName)
        {
            var journalContent = GetContent(userName, journalName);
            JournalEvent[] journalDetails = GetJournalEvents(journalContent, journalName);
            return journalDetails;
        }

        public static IJournalEvent GetJournalEvent(string userName, string journalName, int journalIndex)
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

            foreach(var t in allTypes)
            {
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
                    }
                }
            }

            return new DeserializeExceptionEvent
            {
                Message = $"No type definition found for {journalEvent.Event}"
            };
        }

        /*
            Return all of the journal events in a single journal file
        */
        static JournalEvent[] GetJournalEvents(string[] journalContents, string journalFilename)
        {
            List<JournalEvent> journalEvents = new List<JournalEvent>();
            int i = 0;
            foreach (string journalEvent in journalContents)
            {
                var logEntry = System.Text.Json.JsonSerializer.Deserialize<JournalEvent>(journalEvent);
                logEntry.LogIndex = i;
                logEntry.Filename = journalFilename;
                journalEvents.Add(logEntry);
                i++;
            }
            return journalEvents.ToArray();
        }
        public static string[] GetContent(string userName, string likeFileName)
        {
            string path = GetJournalPath(userName);
            var files = Directory.GetFiles(path, $"*{likeFileName}*");
            return File.ReadAllLines(files[0]);
        }

        public static string GetLogLine(string userName, int logIndex, string logFileName)
        {
            string path = GetJournalPath(userName);
            var lines = File.ReadAllLines($@"{path}\{logFileName}");
            return lines[logIndex];
        }
        
        public static string GetLogLine(string userName, string timestamp, int logIndex)
        {
            string logFileName = $"Journal.{timestamp}.01.log";
            return GetLogLine(userName, logIndex, logFileName);
        }

        public static string GetLogLine(string userName, JournalEvent journalEvent)
        {
            var logLine = GetLogLine(userName, journalEvent.LogIndex, $"{journalEvent.Filename}.log");
            return logLine;
        }

        //Won't need this, but keeping it for reference
        public static List<string> LearnNewJournalTypes(string userName)
        {
            List<string> journalTypes = new List<string>();
            var allTypes = Assembly.GetExecutingAssembly().GetTypes();
            allTypes = typeof(JournalEvent).Assembly.GetTypes();
            List<System.Type> eventTypes = new List<System.Type>();
            List<string> typeNames = new List<string>();
            foreach(var t in allTypes)
            {
                typeNames.Add(t.Name);
            }
            
            var journalFiles = GetJournals(userName);

            foreach (var journalFile in journalFiles)
            {
                foreach(var journalEvent in GetJournalFileEvents(userName, journalFile.Filename))
                {
                    if (!journalTypes.Contains(journalEvent.Event))
                    {
                        journalTypes.Add(journalEvent.Event);
                        string eventTypeName = $"{journalEvent.Event}Event";
                        if(!typeNames.Contains(eventTypeName))
                        {
                            //Unknown type
                            string logLine = GetLogLine(userName, journalEvent);
                            var template = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, string>>(logLine);
                            ProcessNewEventTemplate(template, typeNames);
                            //TODO: finish
                        }
                    }
                }
            }

            return journalTypes;
        }
        //Won't need this, but keeping it for reference
        static void ProcessSubTemplate(Dictionary<string, string> eventTemplate, string newClassName)
        {
            //Write sub-template file
        }

        public static string GuessPropertyType(JsonProperty property, string valueExpression)
        {
            valueExpression = valueExpression.Trim();
            string propertyType = "";
            if (valueExpression.Contains(".")
            && decimal.TryParse(valueExpression, out decimal decValue))
            {
                propertyType = "float";
            }
            else if (valueExpression.Length < 4
            && int.TryParse(valueExpression, out int intValue))
            {
                propertyType = "int";
            }
            else if (long.TryParse(valueExpression, out long longValue))
            {
                propertyType = "long";
            }
            else if (valueExpression.StartsWith("{"))
            {
                propertyType = "object";

                // var subTemplate = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, string>>(valueExpression);
                // //type name is the memberName
                // if (!knownTypeNames.Contains(memberName))
                // {
                //     ProcessSubTemplate(subTemplate, memberName);
                // }
            }
            else if (valueExpression.StartsWith("["))
            {
                propertyType = "array";
                //TODO: Finish
                //Have to get user input around here for arrays, I think
            }
            else
            {
                propertyType = "string";
            }
            return propertyType;
        }

        //Won't need this, but keeping it for reference
        static void ProcessNewEventTemplate(Dictionary<string, string> eventTemplate, List<string> knownTypeNames)
        {
            var memberNames = eventTemplate.Keys;
            List<string> blackList = new List<string> {"timestamp", "event"};
            List<JournalEventPropertyInfo> newTypeMembers = new List<JournalEventPropertyInfo>();
            foreach (string memberName in memberNames)
            {
                if (!blackList.Contains(memberName))
                {

                    string propertyType = "";
                    string propertyValueExpression = eventTemplate[memberName].Trim();
                    //Factoring this out into GuessPRopertyType
                    if (propertyValueExpression.Contains(".")
                    && decimal.TryParse(propertyValueExpression, out decimal decValue))
                    {
                        propertyType = "float";
                    }
                    else if (propertyValueExpression.Length < 4
                    && int.TryParse(propertyValueExpression, out int intValue))
                    {
                        propertyType = "int";
                    }
                    else if (long.TryParse(propertyValueExpression, out long longValue))
                    {
                        propertyType = "long";
                    }
                    else if (propertyValueExpression.StartsWith("{"))
                    {
                        var subTemplate = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, string>>(propertyValueExpression);
                        //type name is the memberName
                        if (!knownTypeNames.Contains(memberName))
                        {
                            ProcessSubTemplate(subTemplate, memberName);
                        }
                    }
                    else if (propertyValueExpression.StartsWith("["))
                    {
                        propertyType = "array";
                        //TODO: Finish
                        //Have to get user input around here for arrays, I think
                    }
                    else
                    {
                        propertyType = "string";
                    }

                    var propertyInfo = new JournalEventPropertyInfo
                    {
                        PropertyName = memberName,
                        ValueType = propertyType    
                    };
                    newTypeMembers.Add(propertyInfo);
                }
            }
            //newTypeMembers should have a good list of type names for the object
            
        }

        public static EventTranslationPlan GetEventTranslationPlan(string userName, string journalName, int logIndex)
        {
            if (!journalName.EndsWith(".log"))
            {
                journalName += ".log";
            }
            var line = GetLogLine(userName, logIndex, journalName);

            EventTranslationPlan eventTranslationPlan
                = new EventTranslationPlan();
            eventTranslationPlan.Members = new List<MemberTranslationPlan>();
            eventTranslationPlan.Filename = journalName;
            eventTranslationPlan.FileContents = line;
            List<string> blackList = new List<string> {"timestamp", "event"};

            var template = System.Text.Json.JsonSerializer.Deserialize<object>(line);
            var newJson = JsonDocument.Parse(line);
            var root = newJson.RootElement;

            var subElements = root.EnumerateObject();

            while(subElements.MoveNext())
            {
                var subElement = subElements.Current;
                string name = subElement.Name;
                if (!blackList.Contains(name))
                {
                    var valueExpression = subElement.Value.ToString();
                    var plan = new MemberTranslationPlan();
                        plan.ValueType = GuessPropertyType(subElement, valueExpression);
                        plan.Name = subElement.Name;

                    eventTranslationPlan.Members.Add(plan);
                }
            }

            return eventTranslationPlan;
        }

        public static List<UnknownJournalEventReport> GetUnknownJournalEvents(string userName)
        {
            List<UnknownJournalEventReport> journalEventReports
                = new List<UnknownJournalEventReport>();

            List<string> journalTypes = new List<string>();
            var allTypes = Assembly.GetExecutingAssembly().GetTypes();
            allTypes = typeof(JournalEvent).Assembly.GetTypes();
            List<System.Type> eventTypes = new List<System.Type>();
            List<string> typeNames = new List<string>();
            foreach(var t in allTypes)
            {
                typeNames.Add(t.Name);
            }
            
            var journalFiles = GetJournals(userName);

            foreach (var journalFile in journalFiles)
            {
                var report = new UnknownJournalEventReport();
                report.LogName = journalFile.Filename;
                int i = 0;
                foreach(var journalEvent in GetJournalFileEvents(userName, journalFile.Filename))
                {
                    if (!journalTypes.Contains(journalEvent.Event))
                    { //Indicates an unknown type on this log line
                        var eventInfo = new JournalEventInfo();
                        eventInfo.LogIndex = i;
                        eventInfo.EventName = journalEvent.Event;
                        if (report.NewEventData == null)
                        {
                            report.NewEventData = new List<JournalEventInfo>();
                        }
                        report.NewEventData.Add(eventInfo);

                        journalTypes.Add(journalEvent.Event);
                    }
                    i++;
                }
                if (report.NewEventData != null
                && report.NewEventData.Count > 0)
                {
                    journalEventReports.Add(report);
                }
            }

            return journalEventReports;
        }


        class JournalEventPropertyInfo
        {
            public string ValueType { get; set; }
            public string PropertyName { get; set; }
        }
    }
        

        public class MemberTranslationPlan
        {
            public string Name { get; set; }
            public string ValueType { get; set; }
        }

        public class EventTranslationPlan
        {
            public string Filename { get; set; }
            public string FileContents { get; set; }
            public List<MemberTranslationPlan> Members { get; set; }
        }
}