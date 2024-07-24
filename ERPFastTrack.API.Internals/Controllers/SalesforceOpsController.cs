using ERPFastTrack.Abstraction.Interfaces;
using ERPFastTrack.SalesforceProcessor.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using ERPFastTrack.DBGround.Context;
using ERPFastTrack.Abstraction.Delegates;
using ERPFastTrack.APIModels.SalesforceOpsModels.Response;
using static System.Runtime.InteropServices.JavaScript.JSType;
using ERPFastTrack.APIModels.SalesforceOpsModels.Request;
using Newtonsoft.Json.Linq;
using Azure.Core;
using Microsoft.EntityFrameworkCore;
using ERPFastTrack.Common.Operations;

namespace ERPFastTrack_DataMapperUI.Controllers
{
    [Route("api/[controller]")]
    public class SalesforceOpsController : ControllerBase
    {
        private readonly ERPFastTrackUIContext _context;
        private readonly ProcessorFactory _processorFactory;
        private readonly IProcessor salesforceProcessor;

        public SalesforceOpsController(ProcessorFactory processorFactory, ERPFastTrackUIContext context)
        {
            _context = context;
            _processorFactory = processorFactory;
            salesforceProcessor = processorFactory("SalesforceProcessor");
        }

        [HttpPost("getsfobjectdetails")]
        public async Task<GetSFObjectDetailsAPIResponse> GetSalesforceObjectDetails([FromBody] GetSFObjectDetailsAPIRequest request)
        {
            GetSFObjectDetailsInternalAPIResponse internalResponse = new();
            GetSFObjectDetailsAPIResponse response = new();
            response.Data = new();
            try
            {
                var salesforceConnection = await _context.SalesforceConnections.FindAsync(request.SalesforceConnectionId);
                var token = salesforceConnection.Token;

                SFObjectDetails objectDetails = await salesforceProcessor.RunAsync<SFObjectDetails, SalesforceProcessorDetails>(new SalesforceProcessorDetails() { Token = token, ObjectName = request.ObjectName, Operation = OperationType.GETOBJECTDETAILS, Url = salesforceConnection.Url });

                response.Status = true;
                internalResponse.ObjectDetails = objectDetails;

                foreach (var item in internalResponse.ObjectDetails.Fields)
                {

                    if (item.Nillable == false && item.DefaultedOnCreate == false && item.Createable == true)
                    {
                        response.Data.Add(new()
                        {
                            Id = item.Name,
                            Name = item.Label + " (" + item.Name + ")",
                            Category = "Required",
                            IsExternalId = item.ExternalId,
                            IsLookup = item.ReferenceTo != null && item.ReferenceTo.Count > 0,
                            LookupName = item.ReferenceTo?.FirstOrDefault()
                        });
                    }
                    else if (item.Custom == true)
                    {
                        response.Data.Add(new()
                        {
                            Id = item.Name,
                            Name = item.Label + " (" + item.Name + ")",
                            Category = "Custom",
                            IsExternalId = item.ExternalId,
                            IsLookup = item.ReferenceTo != null && item.ReferenceTo.Count > 0,
                            LookupName = item.ReferenceTo?.FirstOrDefault()
                        });
                    }
                    else if (item.SoapType == "tns:ID")
                    {
                        response.Data.Add(new()
                        {
                            Id = item.Name,
                            Name = item.Label + " (" + item.Name + ")",
                            Category = "Ids",
                            IsExternalId = item.ExternalId,
                            IsLookup = item.ReferenceTo != null && item.ReferenceTo.Count > 0,
                            LookupName = item.ReferenceTo?.FirstOrDefault()
                        });
                    }
                    else
                    {
                        response.Data.Add(new()
                        {
                            Id = item.Name,
                            Name = item.Label + " (" + item.Name + ")",
                            Category = "Unmapped",
                            IsExternalId = item.ExternalId,
                            IsLookup = item.ReferenceTo != null && item.ReferenceTo.Count > 0,
                            LookupName = item.ReferenceTo?.FirstOrDefault()
                        });
                    }
                }
                if (response.Data?.Count > 0)
                    response.Data = response.Data.OrderBy(x => x.Name).ToList();
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.ErrorMessage = ex.Message;
            }

            return response;
        }

        [HttpGet("getsfobjects/{id}")]
        public async Task<GetAllsObjectsAPIResponse> GetSalesforceObjects(int id)
        {
            GetAllsObjectsAPIResponse response = new();
            var salesforceConnection = await _context.SalesforceConnections.FindAsync(id);
            /*if (salesforceConnection.TokenExpiry < DateTime.UtcNow)
            {
                response.Status = false;
                response.ErrorMessage = "Salesforce session token expired. Please generate again.";
                return response;
            }*/

            string token = "";
            if (salesforceConnection.TokenExpiry > DateTime.UtcNow)
            {
                token = salesforceConnection.Token;
            }
            else
            {
                var httpClient = new HttpClient();

                var parameters = new Dictionary<string, string>
                    {
                        { "grant_type", "refresh_token" },
                        { "refresh_token", salesforceConnection.RefreshToken },
                        { "client_id", Utils.IsLocal() ? Utils.LOCAL_CLIENTID : Utils.SVR_CLIENTID },
                        { "client_secret", Utils.IsLocal() ? Utils.LOCAL_CLIENTSECRET : Utils.SVR_CLIENTSECRET }
                    };

                var content = new FormUrlEncodedContent(parameters);

                var res = await httpClient.PostAsync("https://login.salesforce.com/services/oauth2/token", content);

                if (res.IsSuccessStatusCode)
                {
                    // Ensure the request was successful
                    res.EnsureSuccessStatusCode();

                    // Read the response content
                    string responseContent = await res.Content.ReadAsStringAsync();

                    // Parse the JSON response into a JObject
                    JObject jsonResponse = JObject.Parse(responseContent);

                    var access_token = jsonResponse.SelectToken("access_token")?.ToString();
                    var instance_url = jsonResponse.SelectToken("instance_url")?.ToString();
                    if (!string.IsNullOrEmpty(access_token))
                    {
                        var newSfConn = await _context.SalesforceConnections.FindAsync(salesforceConnection.Id);
                        newSfConn.Token = access_token;
                        newSfConn.TokenExpiry = DateTime.UtcNow.AddMinutes(2);
                        newSfConn.Url = instance_url;
                        _context.Entry(salesforceConnection).State = EntityState.Modified;
                        await _context.SaveChangesAsync();
                        token = access_token;
                    }
                }
            }

            try
            {
                SFsObjects objects = await salesforceProcessor.RunAsync<SFsObjects, SalesforceProcessorDetails>(new SalesforceProcessorDetails() { Token = token, Operation = OperationType.GETALLOBJECTS, Url = salesforceConnection.Url });

                response.Status = true;
                response.Data = objects.sobjects;
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.ErrorMessage = ex.Message;
            }

            return response;
        }

        //    //[Authorize(Roles = "SalesforceUser")]
        //    [HttpPost("/getsfdbmapping")]
        //    public async Task<GetSFDBMappingAPIResponse> GetSalesforceDBMapping([FromBody] GetSFDBMappingAPIRequest request)
        //    {
        //        GetSFDBMappingAPIResponse response = new();
        //        try
        //        {
        //            var cosmosDBRequest = new CosmosDBRequest()
        //            {
        //                Container = "Configurations",
        //                PartitionKey = "SF_DB_ObjectMapping",
        //                Operation = CosmosDBOperation.GET
        //            };

        //            cosmosDBRequest.WhereClauses = new() { { new WhereClause() { IdentifierName = "ObjectName",
        //                IdentifierValue = request.ObjectName} },
        //                { new WhereClause() { IdentifierName = "TableName",
        //                IdentifierValue = request.TableName} }};

        //            CosmosDBResult cosmosDBResult = await dbProcessor.RunAsync<CosmosDBResult, CosmosDBRequest>(cosmosDBRequest);

        //            response.Status = true;
        //            response.Data = cosmosDBResult.JsonArray == null ? cosmosDBResult.Json : cosmosDBResult.JsonArray;
        //        }
        //        catch (Exception ex)
        //        {
        //            response.Status = false;
        //            response.ErrMessage = ex.Message;
        //        }

        //        return response;
        //    }

        //    [HttpPost("/getallsfdbmappings")]
        //    public async Task<GetSFDBMappingAPIResponse> GetAllSalesforceDBMappings()
        //    {
        //        GetSFDBMappingAPIResponse response = new();
        //        try
        //        {
        //            var cosmosDBRequest = new CosmosDBRequest()
        //            {
        //                Container = "Configurations",
        //                PartitionKey = "SF_DB_ObjectMapping",
        //                Operation = CosmosDBOperation.GETMULTIPLE
        //            };

        //            CosmosDBResult cosmosDBResult = await dbProcessor.RunAsync<CosmosDBResult, CosmosDBRequest>(cosmosDBRequest);

        //            response.Status = true;
        //            response.Data = cosmosDBResult.JsonArray == null ? cosmosDBResult.Json : cosmosDBResult.JsonArray;
        //        }
        //        catch (Exception ex)
        //        {
        //            response.Status = false;
        //            response.ErrMessage = ex.Message;
        //        }

        //        return response;
        //    }

        //    [HttpPost("/getalltables")]
        //    public async Task<GetAllTablesAPIResponse> getAllTables()
        //    {
        //        GetAllTablesAPIResponse response = new();
        //        try
        //        {
        //            var cosmosDBRequest = new CosmosDBRequest()
        //            {
        //                Container = "Configurations",
        //                PartitionKey = "QueryConfig",
        //                Operation = CosmosDBOperation.GETMULTIPLE
        //            };

        //            CosmosDBResult cosmosDBResult = await dbProcessor.RunAsync<CosmosDBResult, CosmosDBRequest>(cosmosDBRequest);

        //            response.Status = true;
        //            response.Data = cosmosDBResult.JsonArray;
        //        }
        //        catch (Exception ex)
        //        {
        //            response.Status = false;
        //            response.ErrMessage = ex.Message;
        //        }

        //        return response;
        //    }

        //    [HttpPost("/upsertsfdbmapping")]
        //    public async Task<UpsertAPIResponse> UpsertSalesforceDBMapping([FromBody] UpsertSFDBMappingAPIRequest request)
        //    {
        //        UpsertAPIResponse response = new();

        //        UpsertSFDBMappingRequest mappingRequest = new UpsertSFDBMappingRequest();
        //        mappingRequest.Id = request.Id;
        //        mappingRequest.TableName = request.TableName;
        //        mappingRequest.ObjectName = request.ObjectName;
        //        mappingRequest.SalesforceId = "1234567";
        //        mappingRequest.Mapping = new List<Mapping>();
        //        mappingRequest.LookupsToLoad = new List<string>();
        //        bool hasData = false;
        //        foreach (var mapping in request.Mappings)
        //        {
        //            string key = mapping.Key;
        //            ValueMap value = mapping.Value?.ToObject<ValueMap>();

        //            if(value.LookupFlag)
        //            {
        //                mappingRequest.LookupsToLoad.Add(value.LookupName);
        //            }

        //            if (!string.IsNullOrEmpty(key) && value != null)
        //            {
        //                hasData = true;
        //                key = key.Replace("_value", "");

        //                mappingRequest.Mapping.Add(new() { Key = key, Value = value });
        //            }
        //        }

        //        if (hasData)
        //        {
        //            try
        //            {
        //                CosmosDBResult cosmosDBResult = await dbProcessor.RunAsync<CosmosDBResult, CosmosDBRequest>(new CosmosDBRequest()
        //                {
        //                    Container = "Configurations",
        //                    PartitionKey = "SF_DB_ObjectMapping",
        //                    Operation = CosmosDBOperation.UPSERT,
        //                    Json = JObject.FromObject(mappingRequest)
        //                });

        //                response.Status = true;
        //            }
        //            catch (Exception ex)
        //            {
        //                response.Status = false;
        //                response.ErrMessage = ex.Message;
        //            }
        //        }
        //        return response;
        //    }
    }
}
