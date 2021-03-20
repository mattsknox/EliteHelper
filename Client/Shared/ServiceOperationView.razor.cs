using EliteHelper.Services;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EliteHelper.Shared
{
    public partial class ServiceOperationView : ComponentBase
    {
        [Parameter]
        public ServiceOperation Operation { get; set; }
    }
}
