using Abp.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MyCompany.Projects
{
    public class AsanaManager
    {
        public async Task GetAsanaWorkSpaces(string token)
        {
            var httpClient = new HttpClient();

            var request = new HttpRequestMessage(HttpMethod.Get, "workspaces");
            httpClient.BaseAddress = new Uri("https://app.asana.com/api/1.0/"); // to do: get uri from appsettings 
            httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            
            var response = await httpClient.SendAsync(request);

            if (response.StatusCode == HttpStatusCode.BadRequest)
                throw new UserFriendlyException("an error occurred");

            var a = await response.Content.ReadAsStringAsync();

            var _a = a;
        }
    }
}
