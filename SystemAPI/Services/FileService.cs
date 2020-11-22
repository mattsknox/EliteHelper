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
                Journal journal = new Journal()
                {
                    Filename = journalFilename
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

        public static string GetContent(string userName, string likeFileName)
        {
            string path = GetJournalPath(userName);
            var files = Directory.GetFiles(path, $"*{likeFileName}*");
            return File.ReadAllText(files[0]);
        }
    }
}