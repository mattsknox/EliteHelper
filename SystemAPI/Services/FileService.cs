using System.IO;
using System.Collections.Generic;

namespace SystemApi.Services
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

        public static IEnumerable<string> GetJournals(string userName)
        {
            string path = GetJournalPath(userName);
            var files = Directory.GetFiles(path);
            return files;
        }
    }
}