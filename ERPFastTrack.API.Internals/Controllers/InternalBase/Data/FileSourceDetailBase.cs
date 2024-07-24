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
    
    
    public class FileSourceDetailBase
    {
        private readonly ERPFastTrackUIContext _context;
        private readonly OrgRoleManagerAbstract _roleManager;

        public FileSourceDetailBase(ERPFastTrackUIContext context, OrgRoleManagerAbstract roleManager)
        {
            _context = context;
            _roleManager = roleManager;
        }


        public async Task<GetAllFileSourceDetailResponse> GetAll()
        {
            GetAllFileSourceDetailResponse response = new();

            if (_context.FileSourceDetails == null)
            {
                response.ErrorMessage = "Not Found";
                return response;
            }
            var data = await _context.FileSourceDetails.ToListAsync();

            if (data.Count > 0)
            {
                response.Data = new();

                foreach (var item in data)
                {
                    GetFileSourceDetailResponse obj = new();
                    Utils.MapProperties(item, obj);
                    response.Data.Add(obj);
                }
                response.Status = true;
            }
            return response;

        }

        public async Task<GetAllFileSourceDetailResponse> GetAllForFileSourceConnection(int fileSourceConnectionId)
        {
            GetAllFileSourceDetailResponse response = new();

            if (_context.FileSourceDetails == null)
            {
                response.ErrorMessage = "Not Found";
                return response;
            }
            var data = await _context.FileSourceDetails.Where(x => x.FsConnId == fileSourceConnectionId).ToListAsync();

            if (data.Count > 0)
            {
                response.Data = new();

                foreach (var item in data)
                {
                    GetFileSourceDetailResponse obj = new();
                    Utils.MapProperties(item, obj);
                    response.Data.Add(obj);
                }
                response.Status = true;
            }
            else
            {
                response.Status = false;
                response.ErrorMessage = "List of files are not added in the file source connection.";
            }
            return response;

        }

        public async Task<GetFileSourceDetailResponse> Get(int id)
        {
            GetFileSourceDetailResponse response = new();

            if (_context.FileSourceDetails == null)
            {
                response.ErrorMessage = "Not Found";
                return response;
            }
            var fileSourceDetail = await _context.FileSourceDetails.FindAsync(id);

            if (fileSourceDetail == null)
            {
                response.ErrorMessage = "Not Found";
                return response;
            }

            Utils.MapProperties(fileSourceDetail, response);
            response.Status = true;

            return response;
        }

        public async Task<UpsertFileSourceDetailResponse> Update(int id, UpsertFileSourceDetailRequest request)
        {
            UpsertFileSourceDetailResponse response = new();

            //request.OrgId = _roleManager.Role.OrgId; <-- TODO: DO WE NEED ORG ID IN FILE DETAILS?
            request.Id = id;

            FileSourceDetails fileSourceDetail = new();
            Utils.MapProperties(request, fileSourceDetail);

            _context.Entry(fileSourceDetail).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!FileSourceDetailExists(id))
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

        public async Task<UpsertFileSourceDetailResponse> Insert(UpsertFileSourceDetailRequest request)
        {
            UpsertFileSourceDetailResponse response = new();

            if (_context.FileSourceDetails == null)
            {
                response.ErrorMessage = "Entity set 'ERPFastTrackUIContext.SalesforceConnections'  is null.";
                return response;
            }
            //request.OrgId = _roleManager.Role.OrgId; <-- TODO: DO WE NEED ORG ID IN FILE DETAILS?

            FileSourceDetails fileSourceDetail = new();

            Utils.MapProperties(request, fileSourceDetail);
            _context.FileSourceDetails.Add(fileSourceDetail);
            await _context.SaveChangesAsync();

            response.Status = true;

            return response;
        }

        public async Task<UpsertFileSourceDetailResponse> Delete(int id)
        {
            UpsertFileSourceDetailResponse response = new();
            if (_context.FileSourceDetails == null)
            {
                response.ErrorMessage = "Not Found";
                return response;
            }
            var fileSourceDetail = await _context.FileSourceDetails.FindAsync(id);
            if (fileSourceDetail == null)
            {
                response.ErrorMessage = "Not Found";
                return response;
            }

            _context.FileSourceDetails.Remove(fileSourceDetail);
            await _context.SaveChangesAsync();

            response.Status = true;

            return response;
        }

        private bool FileSourceDetailExists(int id)
        {
            return (_context.FileSourceDetails?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
