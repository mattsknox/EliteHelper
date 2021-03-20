using EliteHelper;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using EliteHelper.Settings;
using System.Text.Json;
using Microsoft.AspNetCore.Components.WebAssembly.Http;

namespace EliteHelper.Services
{
    public class JournalService : IJournalService
    {
        //string BaseUri = "https://localhost:5001";
        string BaseUri = "https://localhost:44352";
        string Controller = "Journal";

        static readonly string UnknownJournalEndpoint = "UnknownJournalEventReport";
        static readonly string StatusEndpoint = "Status";

        string unknownJournalOpName = "GetUnkownJournals";
        string apiStatusOpName = "GetApiStatus";

        public bool IsApiConnected { get; set; } = false;

        public ServiceOperation LastStatus { get; set; }

        ServerClientConfig ClientSettings;
        HttpClient HttpClient { get; set; }

        public JournalService(IOptionsSnapshot<AppSettings> settings,
            HttpClient httpClient)
        {
            HttpClient = httpClient;
            //GetApiStatus();
            //ClientSettings = settings.Value.ServerClients["JournalService"];
            //BaseUri = ClientSettings.BaseURI;
            //Controller = ClientSettings.Endpoint;

        }

        async Task GetApiStatus()
        {
            ServiceMonitor.RegisterOperation(apiStatusOpName);
            string uri = $@"{BaseUri}/{Controller}/{StatusEndpoint}";
            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            request.SetBrowserRequestMode(BrowserRequestMode.Cors);
            try
            {
                using (var response = await HttpClient.SendAsync(request))
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var status = JsonSerializer.Deserialize<bool>(content);
                    IsApiConnected = status;
                    ServiceMonitor.CompleteOperation(apiStatusOpName);
                }
            }
            catch (Exception ex)
            {
                ServiceMonitor.PropagateOperationError(apiStatusOpName, ex.Message);
            }
            
        }

        async Task GetUnknownJournals()
        {
            ServiceMonitor.RegisterOperation(unknownJournalOpName);
            string uri = $@"{BaseUri}/{Controller}/{UnknownJournalEndpoint}";
           
            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            using (var response = await HttpClient.SendAsync(request))
            {
                var content = await response.Content.ReadAsStringAsync();
                var journals = JsonSerializer.Deserialize<List<UnknownJournalEventReport>>(content);
                JournalRepository.UpdateUnknownJournals(journals);
                ServiceMonitor.CompleteOperation(unknownJournalOpName);
            }
        }
                

        //Triggers the GET, returns an operation name
        public string FetchUnknownJournals()
        {
            Task.Factory.StartNew(() => GetUnknownJournals());
            return unknownJournalOpName;
        }

        public string FetchApiStatus()
        {
            Task.Factory.StartNew(() => GetApiStatus());
            return apiStatusOpName;
        }

    }
}