using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using Microsoft.Extensions.Logging;

using EliteHelper.SystemApi;
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
    }
}
