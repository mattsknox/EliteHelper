using EliteHelper;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
namespace EliteHelper.Services
{
    public static class JournalService
    {
        static readonly string BaseUri = "https://localhost:5001";
        static readonly string Controller = "Journal";
        static readonly string UnknownJournalEndpoint = "UnknownJournalEventReport";

        public static async Task<List<UnknownJournalEventReport>> GetUnknownJournals(HttpClient client)
        {
            string uri = $@"{BaseUri}/{Controller}/{UnknownJournalEndpoint}";

            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            var response = await client.SendAsync(request);
            var content = await response.Content.ReadAsStringAsync();
            var journals = System.Text.Json.JsonSerializer.Deserialize<List<UnknownJournalEventReport>>(content);
            return journals;
        }

        //Makes the response available on the Observable
        public static async Task FetchUnknownJournals(HttpClient client)
        {
            var journals = await GetUnknownJournals(client);
            JournalRepository.UpdateUnknownJournals(journals);
        }
    }
}