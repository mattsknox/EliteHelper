using System.IO;
using System.Collections.Generic;

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
            switch (journalEvent.Event)
            {
                case "ReceiveText":
                    return System.Text.Json.JsonSerializer.Deserialize<ReceiveTextEvent>(logLine);
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
        
        public static string GetLogLine(string userName, string likeFileName, int logIndex)
        {
            string path = GetJournalPath(userName);
            var files = Directory.GetFiles(path, $"*{likeFileName}*");
            var lines = File.ReadAllLines(files[0]);
            return lines[logIndex];
        }
    }
}