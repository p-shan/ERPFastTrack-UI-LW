using ERPFastTrack.Abstraction.AbstractClass;
using ERPFastTrack.API.Internals.Controllers.InternalBase.Data;
using ERPFastTrack.APIModels.DataModels.Request;
using ERPFastTrack.APIModels.DataModels.Response;
using ERPFastTrack.DBGround.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ERPFastTrack.API.Internals.Controllers.Exposed
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {
        private readonly ERPFastTrackUIContext _context;
        private readonly OrgRoleManagerAbstract _roleManager;
        private readonly SalesforceConnectionsBase _salesforceConnectionsBase;
        private readonly DatabaseConnectionsBase _databaseConnectionsBase;
        private readonly QueryConfigurationBase _queryConfigurationBase;
        private readonly FileSourceConnectionsBase _fileSourceConnectionsBase;
        private readonly FileSourceDetailBase _fileSourceDetailBase;

        public DataController(ERPFastTrackUIContext context, OrgRoleManagerAbstract roleManager, SalesforceConnectionsBase salesforceConnectionsBase, DatabaseConnectionsBase databaseConnectionsBase, QueryConfigurationBase queryConfigurationBase, FileSourceConnectionsBase fileSourceConnectionsBase, FileSourceDetailBase fileSourceDetailBase)
        {
            _context = context;
            _roleManager = roleManager;
            _salesforceConnectionsBase = salesforceConnectionsBase;
            _databaseConnectionsBase = databaseConnectionsBase;
            _queryConfigurationBase = queryConfigurationBase;
            _fileSourceConnectionsBase = fileSourceConnectionsBase;
            _fileSourceDetailBase = fileSourceDetailBase;
        }

        #region Salesforce Connections
        [HttpGet("salesforceconnection")]
        public async Task<GetAllSalesforceConnectionResponse> GetSalesforceConnections()
        {
            return await _salesforceConnectionsBase.GetAll();
        }

        [HttpGet("salesforceconnection/{id}")]
        public async Task<GetSalesforceConnectionResponse> GetSalesforceConnection(int id)
        {
            return await _salesforceConnectionsBase.Get(id);
        }

        [HttpPut("salesforceconnection/{id}")]
        public async Task<UpsertSalesforceConnectionResponse> PutSalesforceConnection(int id, UpsertSalesforceConnectionRequest request)
        {
            return await _salesforceConnectionsBase.Update(id, request);
        }

        [HttpPost("salesforceconnection")]
        public async Task<UpsertSalesforceConnectionResponse> PostSalesforceConnection(UpsertSalesforceConnectionRequest request)
        {
            return await _salesforceConnectionsBase.Insert(request);
        }

        [HttpDelete("salesforceconnection/{id}")]
        public async Task<UpsertSalesforceConnectionResponse> DeleteSalesforceConnection(int id)
        {
            return await _salesforceConnectionsBase.Delete(id);
        }
        #endregion Salesforce Connections

        #region Database Connections

        [HttpGet("databaseconnection")]
        public async Task<GetAllDatabaseConnectionResponse> GetDatabaseConnections()
        {
            return await _databaseConnectionsBase.GetAll();
        }

        [HttpGet("databaseconnection/{id}")]
        public async Task<GetDatabaseConnectionResponse> GetDatabaseConnection(int id)
        {
            return await _databaseConnectionsBase.Get(id);
        }

        [HttpPut("databaseconnection/{id}")]
        public async Task<UpsertDatabaseConnectionResponse> PutDatabaseConnection(int id, UpsertDatabaseConnectionRequest request)
        {
            return await _databaseConnectionsBase.Update(id, request);
        }

        [HttpPost("databaseconnection")]
        public async Task<UpsertDatabaseConnectionResponse> PostDatabaseConnection(UpsertDatabaseConnectionRequest request)
        {
            return await _databaseConnectionsBase.Insert(request);
        }

        [HttpDelete("databaseconnection/{id}")]
        public async Task<UpsertDatabaseConnectionResponse> DeleteDatabaseConnection(int id)
        {
            return await _databaseConnectionsBase.Delete(id);
        }
        #endregion Database Connections

        #region Query Configuration


        [HttpGet("queryconfiguration/databaseconnection/{id}")]
        public async Task<GetAllQueryConfigurationResponse> GetQueryConfigurationsForDatabaseConnection(int id)
        {
            return await _queryConfigurationBase.GetAllForDatabaseConnection(id);
        }

        [HttpGet("queryconfiguration")]
        public async Task<GetAllQueryConfigurationResponse> GetQueryConfigurations()
        {
            return await _queryConfigurationBase.GetAll();
        }

        [HttpGet("queryconfiguration/{id}")]
        public async Task<GetQueryConfigurationResponse> GetQueryConfiguration(int id)
        {
            return await _queryConfigurationBase.Get(id);
        }

        [HttpPut("queryconfiguration/{id}")]
        public async Task<UpsertQueryConfigurationResponse> PutQueryConfiguration(int id, UpsertQueryConfigurationRequest request)
        {
            return await _queryConfigurationBase.Update(id, request);
        }

        [HttpPost("queryconfiguration")]
        public async Task<UpsertQueryConfigurationResponse> PostQueryConfiguration(UpsertQueryConfigurationRequest request)
        {
            return await _queryConfigurationBase.Insert(request);
        }

        [HttpDelete("queryconfiguration/{id}")]
        public async Task<UpsertQueryConfigurationResponse> DeleteQueryConfiguration(int id)
        {
            return await _queryConfigurationBase.Delete(id);
        }
        #endregion Query Configuration


        #region File Source Connections

        [HttpGet("filesourceconnection")]
        public async Task<GetAllFileSourceConnectionResponse> GetFileSourceConnections()
        {
            return await _fileSourceConnectionsBase.GetAll();
        }

        [HttpGet("filesourceconnection/{id}")]
        public async Task<GetFileSourceConnectionResponse> GetFileSourceConnection(int id)
        {
            return await _fileSourceConnectionsBase.Get(id);
        }

        [HttpPut("filesourceconnection/{id}")]
        public async Task<UpsertFileSourceConnectionResponse> PutFileSourceConnection(int id, UpsertFileSourceConnectionRequest request)
        {
            return await _fileSourceConnectionsBase.Update(id, request);
        }

        [HttpPost("filesourceconnection")]
        public async Task<UpsertFileSourceConnectionResponse> PostFileSourceConnection(UpsertFileSourceConnectionRequest request)
        {
            return await _fileSourceConnectionsBase.Insert(request);
        }

        [HttpDelete("filesourceconnection/{id}")]
        public async Task<UpsertFileSourceConnectionResponse> DeleteFileSourceConnection(int id)
        {
            return await _fileSourceConnectionsBase.Delete(id);
        }
        #endregion Database Connections


        #region FileSourceDetails
        [HttpGet("filesourcedetail/filesourceconnection/{id}")]
        public async Task<GetAllFileSourceDetailResponse> GetQueryConfigurationsForFileSourceConnection(int id)
        {
            return await _fileSourceDetailBase.GetAllForFileSourceConnection(id);
        }

        [HttpGet("filesourcedetail")]
        public async Task<GetAllFileSourceDetailResponse> GetFileSourceDetails()
        {
            return await _fileSourceDetailBase.GetAll();
        }

        [HttpGet("filesourcedetail/{id}")]
        public async Task<GetFileSourceDetailResponse> GetFileSourceDetail(int id)
        {
            return await _fileSourceDetailBase.Get(id);
        }

        [HttpPut("filesourcedetail/{id}")]
        public async Task<UpsertFileSourceDetailResponse> PutFileSourceDetail(int id, UpsertFileSourceDetailRequest request)
        {
            return await _fileSourceDetailBase.Update(id, request);
        }

        [HttpPost("filesourcedetail")]
        public async Task<UpsertFileSourceDetailResponse> PostFileSourceDetail(UpsertFileSourceDetailRequest request)
        {
            return await _fileSourceDetailBase.Insert(request);
        }

        [HttpDelete("filesourcedetail/{id}")]
        public async Task<UpsertFileSourceDetailResponse> DeleteFileSourceDetail(int id)
        {
            return await _fileSourceDetailBase.Delete(id);
        }
        #endregion FileSourceDetails
    }
}
