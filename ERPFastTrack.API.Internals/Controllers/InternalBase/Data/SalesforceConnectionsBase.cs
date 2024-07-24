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
    
    
    public class SalesforceConnectionsBase
    {
        private readonly ERPFastTrackUIContext _context;
        private readonly OrgRoleManagerAbstract _roleManager;

        public SalesforceConnectionsBase(ERPFastTrackUIContext context, OrgRoleManagerAbstract roleManager)
        {
            _context = context;
            _roleManager = roleManager;
        }

        public async Task<GetAllSalesforceConnectionResponse> GetAll()
        {
            GetAllSalesforceConnectionResponse response = new();

            if (_context.SalesforceConnections == null)
            {
                response.ErrorMessage = "Not Found";
                return response;
            }
            var data = await _context.SalesforceConnections.Where(x => x.OrgId == _roleManager.Role.OrgId).ToListAsync();

            if (data.Count > 0)
            {
                response.Data = new();

                foreach (var item in data)
                {
                    GetSalesforceConnectionResponse obj = new();
                    Utils.MapProperties(item, obj);
                    response.Data.Add(obj);
                }
                response.Status = true;
            }
            return response;

        }

        public async Task<GetSalesforceConnectionResponse> Get(int id)
        {
            GetSalesforceConnectionResponse response = new();

            if (_context.SalesforceConnections == null)
            {
                response.ErrorMessage = "Not Found";
                return response;
            }
            var salesforceConnection = await _context.SalesforceConnections.FindAsync(id);

            if (salesforceConnection == null)
            {
                response.ErrorMessage = "Not Found";
                return response;
            }

            Utils.MapProperties(salesforceConnection, response);
            response.Status = true;

            return response;
        }

        public async Task<UpsertSalesforceConnectionResponse> Update(int id, UpsertSalesforceConnectionRequest request)
        {
            UpsertSalesforceConnectionResponse response = new();

            request.OrgId = _roleManager.Role.OrgId;
            request.Id = id;

            SalesforceConnection salesforceConnection = new();
            Utils.MapProperties(request, salesforceConnection);

            _context.Entry(salesforceConnection).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!SalesforceConnectionExists(id))
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

        public async Task<UpsertSalesforceConnectionResponse> Insert(UpsertSalesforceConnectionRequest request)
        {
            UpsertSalesforceConnectionResponse response = new();

            if (_context.SalesforceConnections == null)
            {
                response.ErrorMessage = "Entity set 'ERPFastTrackUIContext.SalesforceConnections'  is null.";
                return response;
            }
            request.OrgId = _roleManager.Role.OrgId;

            SalesforceConnection salesforceConnection = new();

            Utils.MapProperties(request, salesforceConnection);
            _context.SalesforceConnections.Add(salesforceConnection);
            await _context.SaveChangesAsync();

            response.Status = true;

            return response;
        }

        public async Task<UpsertSalesforceConnectionResponse> Delete(int id)
        {
            UpsertSalesforceConnectionResponse response = new();
            if (_context.SalesforceConnections == null)
            {
                response.ErrorMessage = "Not Found";
                return response;
            }
            var salesforceConnection = await _context.SalesforceConnections.FindAsync(id);
            if (salesforceConnection == null)
            {
                response.ErrorMessage = "Not Found";
                return response;
            }

            _context.SalesforceConnections.Remove(salesforceConnection);
            await _context.SaveChangesAsync();

            response.Status = true;

            return response;
        }

        private bool SalesforceConnectionExists(int id)
        {
            return (_context.SalesforceConnections?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
