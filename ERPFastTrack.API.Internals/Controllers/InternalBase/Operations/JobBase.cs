using ERPFastTrack.DBGround.Context;
using ERPFastTrack.Abstraction.AbstractClass;
using Microsoft.EntityFrameworkCore;
using ERPFastTrack.DBGround.DBModels.Custom;
using ERPFastTrack.Common.Operations;
using ERPFastTrack.APIModels.OperationsModels.Response;
using ERPFastTrack.APIModels.OperationsModels.Request;
using Newtonsoft.Json.Linq;
using ERPFastTrack.Common.Static;

namespace ERPFastTrack.API.Internals.Controllers.InternalBase.Operations
{
    
    
    public class JobBase
    {
        private readonly ERPFastTrackUIContext _context;
        private readonly OrgRoleManagerAbstract _roleManager;

        public JobBase(ERPFastTrackUIContext context, OrgRoleManagerAbstract roleManager)
        {
            _context = context;
            _roleManager = roleManager;
        }

        public async Task<GetAllJobResponse> GetAll()
        {
            GetAllJobResponse response = new();

            if (_context.Jobs == null)
            {
                response.ErrorMessage = "Not Found";
                return response;
            }
            var data = await _context.Jobs.Where(x => x.OrgId == _roleManager.Role.OrgId).ToListAsync();

            if (data.Count > 0)
            {
                response.Data = new();

                foreach (var item in data)
                {
                    GetJobResponse obj = new();
                    Utils.MapProperties(item, obj);

                    var project = await _context.Projects.FindAsync(item.PId);
                    if (project == null)
                    {
                        return response;
                    }
                    switch ((ProjectTypesEnum)project.ProjectTypeId)
                    {
                        case ProjectTypesEnum.SQLSERVER:
                            obj.SrcSubDetailName = item.QueryConfiguration.QueryName;
                            obj.SrcSubDetailId = item.QueryConfiguration.Id;
                            obj.SrcDetailId = item.QueryConfiguration.DatabaseConnection.Id;
                            obj.Source = project.ProjectTypeId;
                            break;
                        case ProjectTypesEnum.FILESOURCE:
                            obj.SrcSubDetailName = item.FileSourceDetails.FileSourceDetailName;
                            obj.SrcSubDetailId = item.FileSourceDetails.Id;
                            obj.SrcDetailId = item.FileSourceDetails.FileSourceConnection.Id;
                            obj.Source = project.ProjectTypeId;
                            break;
                        default:
                            break;
                    }

                    switch ((DestinationTypesEnum)project.DestinationTypeId)
                    {
                        case DestinationTypesEnum.SALESFORCE:
                            obj.DestSubDetailName = item.SObjectName;
                            obj.DestSubDetailId = -1;
                            obj.DestDetailId = -1;
                            obj.Destination = 1;
                            break;
                        case DestinationTypesEnum.CSV:
                            break;
                        default:
                            break;
                    }

                    obj.Mapping = JToken.Parse(item.Mapping);
                    response.Data.Add(obj);
                }
                response.Status = true;
            }
            return response;

        }

        public async Task<GetJobResponse> Get(int id)
        {
            GetJobResponse response = new();

            if (_context.Jobs == null)
            {
                response.ErrorMessage = "Not Found";
                return response;
            }
            var job = await _context.Jobs.FindAsync(id);

            if (job == null)
            {
                response.ErrorMessage = "Not Found";
                return response;
            }

            Utils.MapProperties(job, response);

            var project = await _context.Projects.FindAsync(job.PId);
            if (project == null)
            {
                return response;
            }
            switch ((ProjectTypesEnum)project.ProjectTypeId)
            {
                case ProjectTypesEnum.SQLSERVER:
                    var queryConfig = await _context.QueryConfigurations.FindAsync(job.QueryId);
                    response.SrcSubDetailName = queryConfig.QueryName;
                    response.SrcSubDetailId = queryConfig.Id;
                    var dbConn = await _context.DatabaseConnections.FindAsync(queryConfig.DbConnId);
                    response.SrcDetailId = dbConn.Id;
                    response.Source = project.ProjectTypeId;
                    break;
                case ProjectTypesEnum.FILESOURCE:
                    var fileSourceDet = await _context.FileSourceDetails.FindAsync(job.FileSourceDetailId);
                    response.SrcSubDetailName = fileSourceDet.FileSourceDetailName;
                    response.SrcSubDetailId = fileSourceDet.Id;
                    var fsConn = await _context.FileSourceConnections.FindAsync(fileSourceDet.FsConnId);
                    response.SrcDetailId = fsConn.Id;
                    response.Source = project.ProjectTypeId;
                    break;
                default:
                    break;
            }

            switch ((DestinationTypesEnum)project.DestinationTypeId)
            {
                case DestinationTypesEnum.SALESFORCE:
                    response.DestSubDetailName = job.SObjectName;
                    response.DestSubDetailId = -1;
                    response.DestDetailId = -1;
                    response.Destination = 1;
                    break;
                case DestinationTypesEnum.CSV:
                    break;
                default:
                    break;
            }
            response.Mapping = JToken.Parse(job.Mapping);
            response.Status = true;

            return response;
        }

        public async Task<UpsertJobResponse> Update(int id, UpsertJobRequest request)
        {
            UpsertJobResponse response = new();

            request.OrgId = _roleManager.Role.OrgId;
            request.Id = id;

            Job job = new();
            Utils.MapProperties(request, job);
            job.Mapping = request.Mapping.ToString();
            job.OrgId = _roleManager.Role.OrgId;

            var project = await _context.Projects.FindAsync(request.PId);
            if (project != null)
            {
                switch ((ProjectTypesEnum)project.ProjectTypeId)
                {
                    case ProjectTypesEnum.SQLSERVER:
                        job.QueryId = request.SrcSubDetailId;
                        
                        break;
                    case ProjectTypesEnum.FILESOURCE:
                        job.FileSourceDetailId = request.SrcSubDetailId;
                        break;
                    default:
                        throw new Exception("Invalid Source selected");
                }
                switch ((DestinationTypesEnum)project.DestinationTypeId)
                {
                    case DestinationTypesEnum.SALESFORCE:
                        job.SObjectName = request.DestSubDetailName;
                        break;
                    case DestinationTypesEnum.CSV:
                        break;
                    default:
                        break;
                }
                _context.Entry(job).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    if (!JobExists(id))
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
            }
            return response;
        }

        public async Task<UpsertJobResponse> Insert(UpsertJobRequest request)
        {
            UpsertJobResponse response = new();

            if (_context.Jobs == null)
            {
                response.ErrorMessage = "Entity set 'ERPFastTrackUIContext.Jobs'  is null.";
                return response;
            }
            request.OrgId = _roleManager.Role.OrgId;

            Job job = new();
            Utils.MapProperties(request, job);
            job.Mapping = request.Mapping.ToString();
            job.OrgId = _roleManager.Role.OrgId;

            var project = await _context.Projects.FindAsync(request.PId);
            if (project != null)
            {
                switch ((ProjectTypesEnum)project.ProjectTypeId)
                {
                    case ProjectTypesEnum.SQLSERVER:
                        job.QueryId = request.SrcSubDetailId;
                        break;
                    case ProjectTypesEnum.FILESOURCE:
                        job.FileSourceDetailId = request.SrcSubDetailId;
                        break;
                    default:
                        throw new Exception("Invalid Source selected");
                }
                switch ((DestinationTypesEnum)project.DestinationTypeId)
                {
                    case DestinationTypesEnum.SALESFORCE:
                        job.SObjectName = request.DestSubDetailName;
                        break;
                    case DestinationTypesEnum.CSV:
                        break;
                    default:
                        break;
                }

                _context.Jobs.Add(job);

                await _context.SaveChangesAsync();

                response.Status = true;
            }
            return response;
        }

        public async Task<UpsertJobResponse> Delete(int id)
        {
            UpsertJobResponse response = new();
            if (_context.Jobs == null)
            {
                response.ErrorMessage = "Not Found";
                return response;
            }
            var job = await _context.Jobs.FindAsync(id);
            if (job == null)
            {
                response.ErrorMessage = "Not Found";
                return response;
            }

            _context.Jobs.Remove(job);
            await _context.SaveChangesAsync();

            response.Status = true;

            return response;
        }

        private bool JobExists(int id)
        {
            return (_context.Jobs?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        public async Task<GetAllJobResponse> GetAllForProject(int id)
        {
            GetAllJobResponse response = new();

            if (_context.Jobs == null)
            {
                response.ErrorMessage = "Not Found";
                return response;
            }
            var data = await _context.Jobs.Where(x => x.OrgId == _roleManager.Role.OrgId && x.PId == id).ToListAsync();

            if (data.Count > 0)
            {
                response.Data = new();

                foreach (var item in data)
                {
                    GetJobResponse obj = new();
                    Utils.MapProperties(item, obj);
                    var project = await _context.Projects.FindAsync(item.PId);
                    if (project == null)
                    {
                        return response;
                    }
                    switch ((ProjectTypesEnum)project.ProjectTypeId)
                    {
                        case ProjectTypesEnum.SQLSERVER:
                            var queryConfig = await _context.QueryConfigurations.FindAsync(item.QueryId);
                            obj.SrcSubDetailName = queryConfig.QueryName;
                            obj.SrcSubDetailId = queryConfig.Id;
                            var dbConn = await _context.DatabaseConnections.FindAsync(queryConfig.DbConnId);
                            obj.SrcDetailId = dbConn.Id;
                            obj.Source = project.ProjectTypeId;
                            break;
                        case ProjectTypesEnum.FILESOURCE:
                            var fileSourceDet = await _context.FileSourceDetails.FindAsync(item.FileSourceDetailId);
                            obj.SrcSubDetailName = fileSourceDet.FileSourceDetailName;
                            obj.SrcSubDetailId = fileSourceDet.Id;
                            var fsConn = await _context.FileSourceConnections.FindAsync(fileSourceDet.FsConnId);
                            obj.SrcDetailId = fsConn.Id;
                            obj.Source = project.ProjectTypeId;
                            break;
                        default:
                            break;
                    }
                    switch ((DestinationTypesEnum)project.DestinationTypeId)
                    {
                        case DestinationTypesEnum.SALESFORCE:
                            obj.DestSubDetailName = item.SObjectName;
                            obj.DestSubDetailId = -1;
                            obj.DestDetailId = -1;
                            obj.Destination = 1;
                            break;
                        case DestinationTypesEnum.CSV:
                            break;
                        default:
                            break;
                    }
                    obj.Mapping = JToken.Parse(item.Mapping);
                    response.Data.Add(obj);
                }
                response.Status = true;
            }
            return response;
        }
    }
}
