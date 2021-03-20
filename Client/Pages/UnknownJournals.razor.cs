using System;
using System.Net.Http;
using System.Threading.Tasks;
using EliteHelper.Services;

using Microsoft.AspNetCore.Components;

namespace EliteHelper
{
    public partial class UnknownJournals : ComponentBase, IObserver<ServiceOperation>
    {
        private IDisposable unsubscriber;
        [Inject]
        IJournalService JournalService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            string operationName = JournalService.FetchUnknownJournals();
            unsubscriber = ServiceMonitor.Subscribe("UnknownJournals", this);
        }

        public virtual void OnNext(ServiceOperation serviceOperation)
        {
            if (serviceOperation.Status == ServiceStatus.Complete)
            {
                //We have new journals!
            }
        }
        public virtual void OnError(Exception ex)
        {
            
        }
        public virtual void OnCompleted()
        {

        }
        public virtual void Unsubscribe()
        {
            unsubscriber.Dispose();
        }
    }
}