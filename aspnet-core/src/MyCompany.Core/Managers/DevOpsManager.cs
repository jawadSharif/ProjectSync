using Abp.UI;
using MyCompany.DevOpsProject.Dto;
using MyCompany.Managers.Dto;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MyCompany.Managers
{
    public class DevOpsManager : IDevOpsManager
    {
        public Tuple<string> AccessToken(string input)
        {
            var val = Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(string.Format("{0}:{1}", "", input)));
            return new Tuple<string>(val);
        }

        public async Task<List<KeyValuePair<string, string>>> GetProjects(GetProjectsInput input)
        {
            var httpClient = new HttpClient();

            var request = new HttpRequestMessage(HttpMethod.Get, $"{input.Organization}/_apis/projects?api-version={input.Version}");
            httpClient.BaseAddress = new Uri("https://dev.azure.com/"); // to do: get uri from appsettings 
            httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
            httpClient.DefaultRequestHeaders.Add("Authorization", $"Basic {input.Token}");

            var response = await httpClient.SendAsync(request);

            if (response.StatusCode == HttpStatusCode.BadRequest)
                throw new UserFriendlyException("an error occurred");

            var stringData = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<BaseDevOps<DevOpsProjectList>>(stringData);
            var list = new List<KeyValuePair<string, string>>();

            result.value.ForEach(a =>
            {
                list.Add(new KeyValuePair<string, string>(a.id, a.name));
            });

            return list;
        }

        public async Task<List<DevOpsValue>> GetProjectWorkItems(GetWorkItemsInput input)
        {
            var query = JsonConvert.SerializeObject(new DevopsWorkItemQuery() { query = "Select [System.Title] From WorkItems order by [System.CreatedDate] desc" });
            var list = new List<KeyValuePair<int, string>>();

            var client = new RestClient($"https://dev.azure.com/{input.Organization}/{input.Project}/_apis/wit/wiql?api-version={input.Version}");
            var request = new RestRequest(Method.POST);
            request.AddHeader("Authorization", $"basic {input.Token}");
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", query, ParameterType.RequestBody);
            IRestResponse response = await client.ExecuteAsync(request);

            if (response.StatusCode == HttpStatusCode.BadRequest)
                throw new UserFriendlyException("an error occurred");

            var stringData = response.Content;
            var result = JsonConvert.DeserializeObject<WorkItemRoot>(stringData);

            result.workItems.ForEach(a =>
            {
                list.Add(new KeyValuePair<int, string>(a.id, a.url));
            });

            var ids = string.Join(",", list.Select(a => a.Key).ToArray());

            using (var httpClient = new HttpClient())
            {
                var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"{input.Organization}/{input.Project}/_apis/wit/workitems?ids={ids}&api-version=6.0");
                httpClient.BaseAddress = new Uri("https://dev.azure.com/"); // to do: get uri from appsettings 
                httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
                httpClient.DefaultRequestHeaders.Add("Authorization", $"Basic {input.Token}");

                var httpResponse = await httpClient.SendAsync(httpRequest);

                if (httpResponse.StatusCode == HttpStatusCode.BadRequest)
                    throw new UserFriendlyException("an error occurred");

                var stringworkItemsData = await httpResponse.Content.ReadAsStringAsync();
                var workItemDetail = JsonConvert.DeserializeObject<DevOpsWorkItem>(stringworkItemsData);
                return workItemDetail.value;
            }
        }

        public async Task CreateTask(CreateDevOpsTaskDto input)
        {
            string jsonData = JsonConvert.SerializeObject(input.data);

            var client = new RestClient($"https://dev.azure.com/{input.Organization}/{input.Project}/_apis/wit/workitems/$task?api-version={input.Version}");
            var request = new RestRequest(Method.POST);
            request.AddHeader("Authorization", $"basic {input.Token}");
            request.AddHeader("Content-Type", "application/json-patch+json");
            request.AddParameter("application/json-patch+json", jsonData, ParameterType.RequestBody);
            IRestResponse response = await client.ExecuteAsync(request);

            if (response.StatusCode == HttpStatusCode.BadRequest)
                throw new UserFriendlyException("an error occurred");

        }


    }
}
