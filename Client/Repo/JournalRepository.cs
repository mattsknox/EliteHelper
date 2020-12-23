using System;
using System.Collections.Generic;

using EliteHelper.Event;

namespace EliteHelper
{
    public static class JournalRepository
    {
        private static Dictionary<string, List<IObserver<UpdateProcess>>> observers;

        private static string UnknownJournalProcessName = "UnknownJournals";

        private static List<UpdateProcess> _updateProcesses = new List<UpdateProcess>();

        public static List<UnknownJournalEventReport> UnknownJournals; 

        static JournalRepository()
        {
            observers = new Dictionary<string, List<IObserver<UpdateProcess>>>();

            observers.Add(UnknownJournalProcessName, new List<IObserver<UpdateProcess>>());
        }

        private static void UpdateProcessingStatus(UpdateStatus status, string processName)
        {
            UpdateProcess targetProcess = null;
            foreach (var process in _updateProcesses)
            {
                if (process.Name == processName)
                {
                    process.Status = status;
                    process.UpdatedAt = DateTime.Now;
                    targetProcess = process;
                    continue;
                }
            }
            if (targetProcess != null)
            {
                foreach(var observer in observers[processName]) {
                    observer.OnNext(targetProcess);
                }
            }
        }

        public static void UpdateUnknownJournals(List<UnknownJournalEventReport> unknownJournals)
        {
            UnknownJournals = unknownJournals;
            UpdateProcessingStatus(UpdateStatus.Updated, UnknownJournalProcessName);
        }

        #region Observable
        /*
            Give the name of the process you're concerned with, and yourself, and you get updated every time the subject you're concerned about is updated
        */
        public static IDisposable Subscribe(string updateProcessName, IObserver<UpdateProcess> observer)
        {
            if (!observers[updateProcessName].Contains(observer))
            {
                observers[updateProcessName].Add(observer);
            }
            return new Unsubscriber(observers[updateProcessName], observer);
        }

        private class Unsubscriber : IDisposable
        {
            private List<IObserver<UpdateProcess>> _observers;
            private IObserver<UpdateProcess> _observer;

            public Unsubscriber(List<IObserver<UpdateProcess>> observers, 
                                IObserver<UpdateProcess> observer)
            {
                this._observers = observers;
                this._observer = observer;
            }

            public void Dispose()
            {
                if (_observer != null && _observers.Contains(_observer))
                    _observers.Remove(_observer);
            }
        }
        #endregion
    }

    /*

    Repository maintains a list of UpdateProcesses organized by object name.

    */
    public enum UpdateStatus
    {
        Uninitialized,
        Working,
        Updated,
        Error
    }
    //Example: { "Name" : "UnknownJournals", "Status":"Updated", "UpdatedAt":"2020-12-23:12:32:00AM"}
    public class UpdateProcess
    {
        public string Name { get; set; }
        public UpdateStatus Status { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

}