using ERPFastTrack.Abstraction.Interfaces;
using ERPFastTrack.APIModels.SalesforceOpsModels.Response;
using ERPFastTrack.SalesforceProcessor.Models;
using Newtonsoft.Json;
using System.Linq.Expressions;

namespace ERPFastTrack.SalesforceProcessor.Implementations
{
    public class SalesforceProcessor : IProcessor
    {
        private readonly HttpClient httpClient;

        public SalesforceProcessor(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<T> RunAsync<T, R>(R request) where T : new()
        {
            if (request != null)
            {
                SalesforceProcessorDetails detailsObj = (SalesforceProcessorDetails)(dynamic)request;

                switch (detailsObj.Operation)
                {
                    case OperationType.GETALLOBJECTS:
                        var allObjects = await GetAllObjects(detailsObj);
                        return allObjects ?? (dynamic)allObjects;
                    case OperationType.GETOBJECTDETAILS:
                    default:
                        var objectDetails = await GetObjectDetails(detailsObj);
                        return objectDetails ?? (dynamic)objectDetails;                       
                }
            }
            return default;
        }

        private async Task<SFsObjects> GetAllObjects(SalesforceProcessorDetails detailsObj)
        {
            SFsObjects response = null;
            httpClient.BaseAddress = new Uri(detailsObj.Url);
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", detailsObj.Token);

            var httpResponse = await httpClient.GetAsync($"/services/data/v58.0/sobjects");
            httpResponse.EnsureSuccessStatusCode();
            using (Stream stream = await httpResponse.Content.ReadAsStreamAsync())
            using (StreamReader streamReader = new StreamReader(stream))
            using (var jsonReader = new JsonTextReader(streamReader))
            {
                JsonSerializer serializer = new();
                response = serializer.Deserialize<SFsObjects>(jsonReader);
            }

            return response;
        }

        private async Task<SFObjectDetails> GetObjectDetails(SalesforceProcessorDetails detailsObj)
        {
            SFObjectDetails response = null;
            httpClient.BaseAddress = new Uri(detailsObj.Url);
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", detailsObj.Token);

            var httpResponse = await httpClient.GetAsync($"/services/data/v58.0/sobjects/{detailsObj.ObjectName}/describe");
            httpResponse.EnsureSuccessStatusCode();
            using (Stream stream = await httpResponse.Content.ReadAsStreamAsync())
            using (StreamReader streamReader = new StreamReader(stream))
            using (var jsonReader = new JsonTextReader(streamReader))
            {
                JsonSerializer serializer = new();
                response = serializer.Deserialize<SFObjectDetails>(jsonReader);
            }

            return response;
        }
    }
}
