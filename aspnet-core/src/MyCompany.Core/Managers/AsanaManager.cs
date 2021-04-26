using Abp.UI;
using MyCompany.Managers.Dto;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace MyCompany.Managers
{
    public class AsanaManager : IAsanaManager
    {
        public async Task<List<KeyValuePair<string, string>>> GetAsanaWorkSpaces(BasicToken input)
        {
            var httpClient = new HttpClient();

            var request = new HttpRequestMessage(HttpMethod.Get, "workspaces");
            httpClient.BaseAddress = new Uri("https://app.asana.com/api/1.0/"); // to do: get uri from appsettings 
            httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {input.Token}");

            var response = await httpClient.SendAsync(request);


            if (response.StatusCode == HttpStatusCode.BadRequest)
                throw new UserFriendlyException("an error occurred");

            var stringData = await response.Content.ReadAsStringAsync();
            var workspaces = JsonConvert.DeserializeObject<BaseAsana<AsanaWorkspace>>(stringData);
            var list = new List<KeyValuePair<string, string>>();

            workspaces.data.ForEach(a =>
            {
                list.Add(new KeyValuePair<string, string>(a.gid, a.name));
            });

            return list;
        }

        public async Task<List<KeyValuePair<string, string>>> GetAllProjectInWorkSpace(ProjectInput input)
        {
            var httpClient = new HttpClient();

            var request = new HttpRequestMessage(HttpMethod.Get, $"workspaces/{input.WorkspaceId}/projects");
            httpClient.BaseAddress = new Uri("https://app.asana.com/api/1.0/"); // to do: get uri from appsettings 
            httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {input.Token}");

            var response = await httpClient.SendAsync(request);

            if (response.StatusCode == HttpStatusCode.BadRequest)
                throw new UserFriendlyException("an error occurred");

            var stringData = await response.Content.ReadAsStringAsync();
            var workspaces = JsonConvert.DeserializeObject<BaseAsana<AsanaProject>>(stringData);
            var list = new List<KeyValuePair<string, string>>();

            workspaces.data.ForEach(a =>
            {
                list.Add(new KeyValuePair<string, string>(a.gid, a.name));
            });

            return list;
        }

        public async Task<AsanaTaskDetail> GetAllTasksInProject(AsanaTaskInput input)
        {
            var httpClient = new HttpClient();

            var request = new HttpRequestMessage(HttpMethod.Get, $"projects/{input.ProjectId}/tasks?opt_fields=resource_type,name,approval_status,completed,completed_at,completed_by,created_at,html_notes,memberships,modified_at,assignee,parent,tags,notes   ");
            httpClient.BaseAddress = new Uri("https://app.asana.com/api/1.0/"); // to do: get uri from appsettings 
            httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {input.Token}");

            var response = await httpClient.SendAsync(request);

            if (response.StatusCode == HttpStatusCode.BadRequest)
                throw new UserFriendlyException("an error occurred");

            var stringData = await response.Content.ReadAsStringAsync();
            var asanaTasksDetails = JsonConvert.DeserializeObject<AsanaTaskDetail>(stringData);

            return asanaTasksDetails !=null ? asanaTasksDetails : new AsanaTaskDetail();
   
        }

        public async Task CreateTaskInProject(CreateTaskDtoRoot input)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {input.Token}");
            var data = JsonConvert.SerializeObject(input.task);

            var stringContent = new StringContent(data);
            var response = await httpClient.PostAsync("https://app.asana.com/api/1.0/tasks", stringContent);
            if (response.StatusCode == HttpStatusCode.BadRequest)
                throw new UserFriendlyException("an error occurred");

            //var responseString = await response.Content.ReadAsStringAsync();

        }

    }
}
