using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using EliteHelper.Services;

using Microsoft.AspNetCore.Components;

namespace EliteHelper.Shared
{
    public partial class ServerInfoView : ComponentBase, IObserver<ServiceOperation>
    {
        [Inject]
        HttpClient HttpClient { get; set; }

        [Inject]
        IJournalService JournalService { get; set; }
        ServiceOperation JournalServiceStatus { get; set; }
        public string JournalServiceStatusOperationName { get; set; }

        protected override Task OnInitializedAsync()
        {
            //JournalServiceStatusOperationName = JournalService.FetchApiStatus();
            //ServiceMonitor.Subscribe(JournalServiceStatusOperationName, this);

            return Task.CompletedTask;
        }
        protected override Task OnParametersSetAsync()
        {
            JournalService.FetchApiStatus();
            return Task.CompletedTask;
        }

        public void OnCompleted()
        {
            throw new NotImplementedException();
        }

        public void OnError(Exception error)
        {
            throw new NotImplementedException();
        }

        public void OnNext(ServiceOperation operation)
        {
            if (operation.Name == JournalServiceStatusOperationName)
            {
                JournalServiceStatus = operation;
                StateHasChanged();
            }
        }

        void Refresh()
        {
            JournalServiceStatusOperationName = JournalService.FetchApiStatus();

            ServiceMonitor.Subscribe(JournalServiceStatusOperationName, this);
        }

    }
}
