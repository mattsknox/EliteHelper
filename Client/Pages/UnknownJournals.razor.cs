using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Components;

namespace EliteHelper
{
    public partial class UnknownJournals : ComponentBase, IObserver<UpdateProcess>
    {
        private IDisposable unsubscriber;

        protected override async Task OnInitializedAsync()
        {
            unsubscriber = JournalRepository.Subscribe("UnknownJournals", this);
        }

        public virtual void OnNext(UpdateProcess updateProcess)
        {
            if (updateProcess.Status == UpdateStatus.Updated)
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