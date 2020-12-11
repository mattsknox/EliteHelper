using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using Microsoft.Extensions.Logging;

using EliteHelper.SystemApi.Services;
using EliteHelper.Event;
using EliteHelper;

namespace SystemAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class JournalController : ControllerBase
    {

        private readonly ILogger<JournalController> _logger;

        public JournalController(ILogger<JournalController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Journal> Get()
        {
            string userName = Environment.UserName;
            return FileService.GetJournals(userName);
        }

        [HttpGet]
        [Route("Details/{journalName}")]
        public JournalEvent[] GetJournalDetails(string journalName)
        {
            var journalDetails = FileService.GetJournalFileEvents(Environment.UserName, journalName);
            return journalDetails;
        }

        [HttpGet]
        [Route("Entry/{journalName}/{journalIndex}")]
        public IJournalEvent GetJournal(string journalName, int journalIndex)
        {
            var journal = FileService.GetJournalEvent(Environment.UserName, journalName, journalIndex);
            return journal;
        }

        [HttpGet]
        [Route("ScanJournals")]
        public List<string> ScanJournals()
        {
            var names = FileService.LearnNewJournalTypes(Environment.UserName);
            return names;
        }

        [HttpGet]
        [Route("GetUnknownJournalEventReport")]
        public List<UnknownJournalEventReport> GetUnknownJournalEventReport()
        {
            var reports = FileService.GetUnknownJournalEvents(Environment.UserName);
            return reports;
        }

        [HttpGet]
        [Route("GetEventTranslationPlan/{journalName}/{eventIndex}")]
        public EventTranslationPlan GetEventTranslationPlan(string journalName, int logIndex)
        {
            var result = FileService.GetEventTranslationPlan(Environment.UserName, journalName, logIndex);
            return result;
        }

    }
}
