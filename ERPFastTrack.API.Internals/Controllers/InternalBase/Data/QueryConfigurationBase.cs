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
    
    
    public class QueryConfigurationBase
    {
        private readonly ERPFastTrackUIContext _context;
        private readonly OrgRoleManagerAbstract _roleManager;

        public QueryConfigurationBase(ERPFastTrackUIContext context, OrgRoleManagerAbstract roleManager)
        {
            _context = context;
            _roleManager = roleManager;
        }

        public async Task<GetAllQueryConfigurationResponse> GetAll()
        {
            GetAllQueryConfigurationResponse response = new();

            if (_context.QueryConfigurations == null)
            {
                response.ErrorMessage = "Not Found";
                return response;
            }
            var data = await _context.QueryConfigurations.Where(x => x.OrgId == _roleManager.Role.OrgId).ToListAsync();

            if (data.Count > 0)
            {
                response.Data = new();

                foreach (var item in data)
                {
                    GetQueryConfigurationResponse obj = new();
                    Utils.MapProperties(item, obj);
                    response.Data.Add(obj);
                }
                response.Status = true;
            }
            return response;

        }

        public async Task<GetAllQueryConfigurationResponse> GetAllForDatabaseConnection(int dbConnId)
        {
            GetAllQueryConfigurationResponse response = new();

            if (_context.QueryConfigurations == null)
            {
                response.ErrorMessage = "Not Found";
                return response;
            }
            var data = await _context.QueryConfigurations.Where(x => x.OrgId == _roleManager.Role.OrgId && x.DbConnId == dbConnId).ToListAsync();

            if (data.Count > 0)
            {
                response.Data = new();

                foreach (var item in data)
                {
                    GetQueryConfigurationResponse obj = new();
                    Utils.MapProperties(item, obj);
                    response.Data.Add(obj);
                }
                response.Status = true;
            }
            else
            {
                response.Status = false;
                response.ErrorMessage = "List of queries are not added in the database connection.";
            }
            return response;

        }

        public async Task<GetQueryConfigurationResponse> Get(int id)
        {
            GetQueryConfigurationResponse response = new();

            if (_context.QueryConfigurations == null)
            {
                response.ErrorMessage = "Not Found";
                return response;
            }
            var queryConfiguration = await _context.QueryConfigurations.FindAsync(id);

            if (queryConfiguration == null)
            {
                response.ErrorMessage = "Not Found";
                return response;
            }

            Utils.MapProperties(queryConfiguration, response);
            response.Status = true;

            return response;
        }

        public async Task<UpsertQueryConfigurationResponse> Update(int id, UpsertQueryConfigurationRequest request)
        {
            UpsertQueryConfigurationResponse response = new();

            request.OrgId = _roleManager.Role.OrgId;
            request.Id = id;

            QueryConfiguration queryConfiguration = new();
            Utils.MapProperties(request, queryConfiguration);

            _context.Entry(queryConfiguration).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!QueryConfigurationExists(id))
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

        public async Task<UpsertQueryConfigurationResponse> Insert(UpsertQueryConfigurationRequest request)
        {
            UpsertQueryConfigurationResponse response = new();

            if (_context.QueryConfigurations == null)
            {
                response.ErrorMessage = "Entity set 'ERPFastTrackUIContext.SalesforceConnections'  is null.";
                return response;
            }
            request.OrgId = _roleManager.Role.OrgId;

            QueryConfiguration queryConfiguration = new();

            Utils.MapProperties(request, queryConfiguration);
            _context.QueryConfigurations.Add(queryConfiguration);
            await _context.SaveChangesAsync();

            response.Status = true;

            return response;
        }

        public async Task<UpsertQueryConfigurationResponse> Delete(int id)
        {
            UpsertQueryConfigurationResponse response = new();
            if (_context.QueryConfigurations == null)
            {
                response.ErrorMessage = "Not Found";
                return response;
            }
            var queryConfiguration = await _context.QueryConfigurations.FindAsync(id);
            if (queryConfiguration == null)
            {
                response.ErrorMessage = "Not Found";
                return response;
            }

            _context.QueryConfigurations.Remove(queryConfiguration);
            await _context.SaveChangesAsync();

            response.Status = true;

            return response;
        }

        private bool QueryConfigurationExists(int id)
        {
            return (_context.QueryConfigurations?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
