using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace EliteHelper.Services
{
    public interface IJournalService
    {
        public string FetchUnknownJournals();
        public string FetchApiStatus();
        ServiceOperation LastStatus { get; set; }
        public bool IsApiConnected{ get; set; }
    }
}
