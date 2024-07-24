using Microsoft.AspNetCore.Mvc;
using ERPFastTrack.DBGround.Context;
using ERPFastTrack.Abstraction.AbstractClass;
using Microsoft.EntityFrameworkCore;
using ERPFastTrack.DBGround.DBModels.Custom;
using ERPFastTrack.APIModels.DataModels.Response;
using ERPFastTrack.Common.Operations;
using ERPFastTrack.APIModels.DataModels.Request;

namespace ERPFastTrack.API.Internals.Controllers.InternalBase.Data
{
    
    
    public class DatabaseConnectionsBase
    {
        private readonly ERPFastTrackUIContext _context;
        private readonly OrgRoleManagerAbstract _roleManager;

        public DatabaseConnectionsBase(ERPFastTrackUIContext context, OrgRoleManagerAbstract roleManager)
        {
            _context = context;
            _roleManager = roleManager;
        }

        public async Task<GetAllDatabaseConnectionResponse> GetAll()
        {
            GetAllDatabaseConnectionResponse response = new();

            if (_context.DatabaseConnections == null)
            {
                response.ErrorMessage = "Not Found";
                return response;
            }
            var data = await _context.DatabaseConnections.Where(x => x.OrgId == _roleManager.Role.OrgId).ToListAsync();

            if (data.Count > 0)
            {
                response.Data = new();

                foreach (var item in data)
                {
                    GetDatabaseConnectionResponse obj = new();
                    Utils.MapProperties(item, obj);

                    obj.QueryConfigurations = new();
                    var queryConfigurations = await _context.QueryConfigurations.Where(x => x.DbConnId == obj.Id).ToListAsync();
                    foreach (var subItem in queryConfigurations)
                    {
                        GetQueryConfigurationResponse subObj = new();
                        Utils.MapProperties(subItem, subObj);
                        obj.QueryConfigurations.Add(subObj);
                    }

                    response.Data.Add(obj);
                }
                response.Status = true;
            }
            return response;

        }

        public async Task<GetDatabaseConnectionResponse> Get(int id)
        {
            GetDatabaseConnectionResponse response = new();

            if (_context.DatabaseConnections == null)
            {
                response.ErrorMessage = "Not Found";
                return response;
            }
            var databaseConnection = await _context.DatabaseConnections.FindAsync(id);

            if (databaseConnection == null)
            {
                response.ErrorMessage = "Not Found";
                return response;
            }

            Utils.MapProperties(databaseConnection, response);

            response.QueryConfigurations = new();
            var queryConfigurations = await _context.QueryConfigurations.Where(x => x.DbConnId == response.Id).ToListAsync();
            foreach (var subItem in queryConfigurations)
            {
                GetQueryConfigurationResponse subObj = new();
                Utils.MapProperties(subItem, subObj);
                response.QueryConfigurations.Add(subObj);
            }

            response.Status = true;

            return response;
        }

        public async Task<UpsertDatabaseConnectionResponse> Update(int id, UpsertDatabaseConnectionRequest request)
        {
            UpsertDatabaseConnectionResponse response = new();

            request.OrgId = _roleManager.Role.OrgId;
            request.Id = id;

            DatabaseConnection databaseConnection = new();
            Utils.MapProperties(request, databaseConnection);

            _context.Entry(databaseConnection).State = EntityState.Modified;

            try
            {

                var existingDatabaseConnection = await _context.DatabaseConnections.FindAsync(id);
                if (existingDatabaseConnection != null)
                {
                    _context.Entry(existingDatabaseConnection).Collection(e => e.QueryConfigurations).Load();
                }

                foreach (var queryConfiguration in existingDatabaseConnection.QueryConfigurations)
                {
                    _context.QueryConfigurations.Remove(queryConfiguration);
                }

                if (request.QueryConfigurations != null && request.QueryConfigurations.Count > 0)
                    foreach (var queryConfigs in request.QueryConfigurations)
                    {
                        QueryConfiguration queryConfiguration = new();
                        Utils.MapProperties(queryConfigs, queryConfiguration);
                        queryConfiguration.OrgId = _roleManager.Role.OrgId;
                        queryConfiguration.DbConnId = id;
                        _context.QueryConfigurations.Add(queryConfiguration);
                    }

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!DatabaseConnectionExists(id))
                {
                    response.ErrorMessage = "Not Found";
                    return response;
                }
                else
                {
                    response.ErrorMessage = ex.Message;
                    return response;
                }
            }

            response.Status = true;
            return response;
        }

        public async Task<UpsertDatabaseConnectionResponse> Insert(UpsertDatabaseConnectionRequest request)
        {
            UpsertDatabaseConnectionResponse response = new();

            if (_context.DatabaseConnections == null)
            {
                response.ErrorMessage = "Entity set 'ERPFastTrackUIContext.SalesforceConnections'  is null.";
                return response;
            }
            request.OrgId = _roleManager.Role.OrgId;

            DatabaseConnection databaseConnection = new();

            Utils.MapProperties(request, databaseConnection);
            _context.DatabaseConnections.Add(databaseConnection);
            await _context.SaveChangesAsync();

            if (request.QueryConfigurations != null && request.QueryConfigurations.Count > 0)
                foreach (var queryConfigs in request.QueryConfigurations)
                {
                    QueryConfiguration queryConfiguration = new();
                    Utils.MapProperties(queryConfigs, queryConfiguration);
                    queryConfiguration.OrgId = _roleManager.Role.OrgId;
                    queryConfiguration.DbConnId = databaseConnection.Id;
                    _context.QueryConfigurations.Add(queryConfiguration);
                }

            await _context.SaveChangesAsync();

            response.Status = true;

            return response;
        }

        public async Task<UpsertDatabaseConnectionResponse> Delete(int id)
        {
            UpsertDatabaseConnectionResponse response = new();
            if (_context.DatabaseConnections == null)
            {
                response.ErrorMessage = "Not Found";
                return response;
            }
            var databaseConnection = await _context.DatabaseConnections.FindAsync(id);
            if (databaseConnection == null)
            {
                response.ErrorMessage = "Not Found";
                return response;
            }

            _context.DatabaseConnections.Remove(databaseConnection);
            await _context.SaveChangesAsync();

            response.Status = true;

            return response;
        }

        private bool DatabaseConnectionExists(int id)
        {
            return (_context.DatabaseConnections?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
