using ERPFastTrack.DBGround.Context;
using ERPFastTrack.Abstraction.AbstractClass;
using Microsoft.EntityFrameworkCore;
using ERPFastTrack.DBGround.DBModels.Custom;
using ERPFastTrack.APIModels.DataModels.Response;
using ERPFastTrack.Common.Operations;
using ERPFastTrack.APIModels.DataModels.Request;

namespace ERPFastTrack.API.Internals.Controllers.InternalBase.Data
{
    
    
    public class FileSourceConnectionsBase
    {
        private readonly ERPFastTrackUIContext _context;
        private readonly OrgRoleManagerAbstract _roleManager;

        public FileSourceConnectionsBase(ERPFastTrackUIContext context, OrgRoleManagerAbstract roleManager)
        {
            _context = context;
            _roleManager = roleManager;
        }

        public async Task<GetAllFileSourceConnectionResponse> GetAll()
        {
            GetAllFileSourceConnectionResponse response = new();

            if (_context.FileSourceConnections == null)
            {
                response.ErrorMessage = "Not Found";
                return response;
            }
            var data = await _context.FileSourceConnections.Where(x => x.OrgId == _roleManager.Role.OrgId).ToListAsync();

            if (data.Count > 0)
            {
                response.Data = new();

                foreach (var item in data)
                {
                    GetFileSourceConnectionResponse obj = new();
                    Utils.MapProperties(item, obj);

                    obj.FileSourceDetails = new();
                    var fileSourceDetails = await _context.FileSourceDetails.Where(x => x.FsConnId == obj.Id).ToListAsync();
                    foreach (var subItem in fileSourceDetails)
                    {
                        GetFileSourceDetailResponse subObj = new();
                        Utils.MapProperties(subItem, subObj);
                        obj.FileSourceDetails.Add(subObj);
                    }

                    response.Data.Add(obj);
                }
                response.Status = true;
            }
            return response;

        }

        public async Task<GetFileSourceConnectionResponse> Get(int id)
        {
            GetFileSourceConnectionResponse response = new();

            if (_context.FileSourceConnections == null)
            {
                response.ErrorMessage = "Not Found";
                return response;
            }
            var fileSourceConnection = await _context.FileSourceConnections.FindAsync(id);

            if (fileSourceConnection == null)
            {
                response.ErrorMessage = "Not Found";
                return response;
            }

            Utils.MapProperties(fileSourceConnection, response);

            response.FileSourceDetails = new();
            var fileSourceDetails = await _context.FileSourceDetails.Where(x => x.FsConnId == response.Id).ToListAsync();
            foreach (var subItem in fileSourceDetails)
            {
                GetFileSourceDetailResponse subObj = new();
                Utils.MapProperties(subItem, subObj);
                response.FileSourceDetails.Add(subObj);
            }

            response.Status = true;

            return response;
        }

        public async Task<UpsertFileSourceConnectionResponse> Update(int id, UpsertFileSourceConnectionRequest request)
        {
            UpsertFileSourceConnectionResponse response = new();

            request.OrgId = _roleManager.Role.OrgId;
            request.Id = id;

            FileSourceConnection fileSourceConnection = new();
            Utils.MapProperties(request, fileSourceConnection);

            _context.Entry(fileSourceConnection).State = EntityState.Modified;

            try
            {
                var existingFileSourceConnection = await _context.FileSourceConnections.FindAsync(id);
                if (existingFileSourceConnection != null)
                {
                    _context.Entry(existingFileSourceConnection).Collection(e => e.FileSourceDetails).Load();
                }

                foreach (var fileSourceDetails in existingFileSourceConnection.FileSourceDetails)
                {
                    _context.FileSourceDetails.Remove(fileSourceDetails);
                }

                if (request.FileSourceDetails != null && request.FileSourceDetails.Count > 0)
                    foreach (var sourceDetails in request.FileSourceDetails)
                    {
                        FileSourceDetails fileSourceDetails = new();
                        Utils.MapProperties(sourceDetails, fileSourceDetails);
                        fileSourceDetails.FsConnId = id;
                        _context.FileSourceDetails.Add(fileSourceDetails);
                    }

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!FileSourceConnectionExists(id))
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

        public async Task<UpsertFileSourceConnectionResponse> Insert(UpsertFileSourceConnectionRequest request)
        {
            UpsertFileSourceConnectionResponse response = new();

            if (_context.FileSourceConnections == null)
            {
                response.ErrorMessage = "Entity set 'ERPFastTrackUIContext.FileSourceConnections'  is null.";
                return response;
            }
            request.OrgId = _roleManager.Role.OrgId;

            FileSourceConnection fileSourceConnection = new();

            Utils.MapProperties(request, fileSourceConnection);
            _context.FileSourceConnections.Add(fileSourceConnection);
            await _context.SaveChangesAsync();

            if (request.FileSourceDetails != null && request.FileSourceDetails.Count > 0)
                foreach (var sourceDetails in request.FileSourceDetails)
                {
                    FileSourceDetails fileSourceDetails = new();
                    Utils.MapProperties(sourceDetails, fileSourceDetails);
                    fileSourceDetails.FsConnId = fileSourceConnection.Id;
                    _context.FileSourceDetails.Add(fileSourceDetails);
                }

            await _context.SaveChangesAsync();

            response.Status = true;

            return response;
        }

        public async Task<UpsertFileSourceConnectionResponse> Delete(int id)
        {
            UpsertFileSourceConnectionResponse response = new();
            if (_context.FileSourceConnections == null)
            {
                response.ErrorMessage = "Not Found";
                return response;
            }
            var fileSourceConnection = await _context.FileSourceConnections.FindAsync(id);
            if (fileSourceConnection == null)
            {
                response.ErrorMessage = "Not Found";
                return response;
            }

            _context.FileSourceConnections.Remove(fileSourceConnection);
            await _context.SaveChangesAsync();

            response.Status = true;

            return response;
        }

        private bool FileSourceConnectionExists(int id)
        {
            return (_context.FileSourceConnections?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
