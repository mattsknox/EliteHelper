using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using Microsoft.Extensions.Logging;

using EliteHelper.SystemApi;
using EliteHelper.Event;
using EliteHelper;


using EliteHelper.SystemApi.Services;

namespace SystemAPI.Controllers
{
    [ApiController]
    [EnableCors("AllowAll")]  
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
            var journalDetails = FileService.GetJournalDetails(Environment.UserName, journalName);
            return journalDetails;
        }

        [HttpGet]
        [Route("Entry/{journalName}/{journalIndex}")]
        public IJournalEvent GetJournal(string journalName, int journalIndex)
        {
            var journal = FileService.GetJournal(Environment.UserName, journalName, journalIndex);
            return journal;
        }
    }
}
