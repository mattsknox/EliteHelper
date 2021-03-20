using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EliteHelper.Pages
{
    public partial class Index
    {
        List<Journal> journals = new List<Journal>();

        protected override async Task OnInitializedAsync()
        {
            //var request = new HttpRequestMessage(HttpMethod.Get, uri);
            //request.SetBrowserRequestMode(BrowserRequestMode.Cors);
            //var response = await httpClient.SendAsync(request);
            //var content = await response.Content.ReadAsStringAsync();
            //journals = System.Text.Json.JsonSerializer.Deserialize<List<Journal>>(content);
        }
    }
}
