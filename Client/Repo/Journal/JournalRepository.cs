using System;
using System.Collections.Generic;

using EliteHelper.Event;
using EliteHelper.Repo.Journal;

namespace EliteHelper
{
    public static class JournalRepository
    {
        public static List<UnknownJournalEventReport> UnknownJournals; 

        static JournalRepository()
        {

        }

        public static void UpdateUnknownJournals(List<UnknownJournalEventReport> unknownJournals)
        {
            UnknownJournals = unknownJournals;
        }
    }
}