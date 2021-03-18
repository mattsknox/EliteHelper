using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EliteHelper.Services
{
    public class ServiceOperation
    {
        public string Name { get; set; }
        public bool Success { get; set; }
        public ServiceStatus Status { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string Message { get; set; }
    }
}
