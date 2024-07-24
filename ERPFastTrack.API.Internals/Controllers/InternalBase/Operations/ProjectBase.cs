using ERPFastTrack.DBGround.Context;
using ERPFastTrack.Abstraction.AbstractClass;
using Microsoft.EntityFrameworkCore;
using ERPFastTrack.DBGround.DBModels.Custom;
using ERPFastTrack.Common.Operations;
using ERPFastTrack.APIModels.OperationsModels.Response;
using ERPFastTrack.APIModels.OperationsModels.Request;
using ERPFastTrack.Common.Static;

namespace ERPFastTrack.API.Internals.Controllers.InternalBase.Operations
{
    
    
    public class ProjectBase
    {
        private readonly ERPFastTrackUIContext _context;
        private readonly OrgRoleManagerAbstract _roleManager;

        public ProjectBase(ERPFastTrackUIContext context, OrgRoleManagerAbstract roleManager)
        {
            _context = context;
            _roleManager = roleManager;
        }

        public async Task<GetAllProjectResponse> GetAll()
        {
            GetAllProjectResponse response = new();

            if (_context.Projects == null)
            {
                response.ErrorMessage = "Not Found";
                return response;
            }
            var data = await _context.Projects.Where(x => x.OrgId == _roleManager.Role.OrgId).ToListAsync();

            if (data.Count > 0)
            {
                response.Data = new();

                foreach (var item in data)
                {
                    GetProjectResponse obj = new();
                    Utils.MapProperties(item, obj);
                    obj.ProjectTypeId = item.ProjectTypeId;
                    var projectType = (ProjectTypesEnum)item.ProjectTypeId;
                    switch (projectType)
                    {
                        case ProjectTypesEnum.SQLSERVER:
                            var dbConnection = await _context.DatabaseConnections.FindAsync(item.DbConnId);
                            obj.SourceConnId = dbConnection.Id;
                            obj.SourceConnName = dbConnection.Name;
                            obj.SourceConnString = dbConnection.ConnectionString;
                            break;
                        case ProjectTypesEnum.FILESOURCE:
                            var fileSourceConnection = await _context.FileSourceConnections.FindAsync(item.FsConnId);
                            obj.SourceConnId = fileSourceConnection.Id;
                            obj.SourceConnName = fileSourceConnection.Name;
                            obj.SourceConnString = fileSourceConnection.FileLocation;
                            break;
                        default:
                            break;
                    }
                    obj.DestinationTypeId = item.DestinationTypeId;
                    var destinationType = (DestinationTypesEnum)item.DestinationTypeId;
                    switch (destinationType)
                    {
                        case DestinationTypesEnum.SALESFORCE:
                            var sourceConnection = await _context.SalesforceConnections.FindAsync(item.SfConnId);
                            obj.DestConnName = sourceConnection.Name;
                            obj.DestConnUrl = sourceConnection.Url;
                            obj.DestConnId = sourceConnection.Id;
                            break;
                        case DestinationTypesEnum.CSV:
                            break;
                        default:
                            break;
                    }

                    response.Data.Add(obj);
                }
                response.Status = true;
            }
            return response;

        }

        public async Task<GetProjectResponse> Get(int id)
        {
            GetProjectResponse response = new();

            if (_context.Projects == null)
            {
                response.ErrorMessage = "Not Found";
                return response;
            }
            var project = await _context.Projects.FindAsync(id);

            if (project == null)
            {
                response.ErrorMessage = "Not Found";
                return response;
            }

            Utils.MapProperties(project, response);

            response.ProjectTypeId = project.ProjectTypeId;
            var projectType = (ProjectTypesEnum)project.ProjectTypeId;
            switch (projectType)
            {
                case ProjectTypesEnum.SQLSERVER:
                    var dbConnection = await _context.DatabaseConnections.FindAsync(project.DbConnId);
                    response.SourceConnId = dbConnection.Id;
                    response.SourceConnName = dbConnection.Name;
                    response.SourceConnString = dbConnection.ConnectionString;
                    break;
                case ProjectTypesEnum.FILESOURCE:
                    var fileSourceConnection = await _context.FileSourceConnections.FindAsync(project.FsConnId);
                    response.SourceConnId = fileSourceConnection.Id;
                    response.SourceConnName = fileSourceConnection.Name;
                    response.SourceConnString = fileSourceConnection.FileLocation;
                    break;
                default:
                    break;
            }
            response.DestinationTypeId = project.DestinationTypeId;
            var destinationType = (DestinationTypesEnum)project.DestinationTypeId;
            switch (destinationType)
            {
                case DestinationTypesEnum.SALESFORCE:
                    var sourceConnection = await _context.SalesforceConnections.FindAsync(project.SfConnId);
                    response.DestConnName = sourceConnection.Name;
                    response.DestConnUrl = sourceConnection.Url;
                    response.DestConnId = sourceConnection.Id;
                    break;
                case DestinationTypesEnum.CSV:
                    break;
                default:
                    break;
            }

            response.Status = true;

            return response;
        }

        public async Task<UpsertProjectResponse> Update(int id, UpsertProjectRequest request)
        {
            UpsertProjectResponse response = new();

            request.OrgId = _roleManager.Role.OrgId;
            request.Id = id;

            Project project = new();
            Utils.MapProperties(request, project);

            _context.Entry(project).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!ProjectExists(id))
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

        public async Task<UpsertProjectResponse> Insert(UpsertProjectRequest request)
        {
            UpsertProjectResponse response = new();

            if (_context.Projects == null)
            {
                response.ErrorMessage = "Entity set 'ERPFastTrackUIContext.Projects'  is null.";
                return response;
            }
            request.OrgId = _roleManager.Role.OrgId;

            Project project = new();
            project.Name = request.Name;
            project.OrgId = _roleManager.Role.OrgId;
            switch ((ProjectTypesEnum)request.Source)
            {
                case ProjectTypesEnum.SQLSERVER:
                    project.ProjectTypeId = request.Source;
                    project.DbConnId = request.SrcDetails;

                    break;
                case ProjectTypesEnum.FILESOURCE:
                    project.ProjectTypeId = request.Source;
                    project.FsConnId = request.SrcDetails;

                    break;
                default:
                    throw new Exception("Invalid Source selected");
            }

            switch ((DestinationTypesEnum)request.Destination)
            {
                case DestinationTypesEnum.SALESFORCE:
                    project.DestinationTypeId = request.Destination;
                    project.SfConnId = request.DestDetails;
                    break;
                case DestinationTypesEnum.CSV:
                    break;
                default:
                    break;
            }

            _context.Projects.Add(project);
            await _context.SaveChangesAsync();

            response.Status = true;

            return response;
        }

        public async Task<UpsertProjectResponse> Delete(int id)
        {
            UpsertProjectResponse response = new();
            if (_context.Projects == null)
            {
                response.ErrorMessage = "Not Found";
                return response;
            }
            var project = await _context.Projects.FindAsync(id);
            if (project == null)
            {
                response.ErrorMessage = "Not Found";
                return response;
            }

            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();

            response.Status = true;

            return response;
        }

        private bool ProjectExists(int id)
        {
            return (_context.Projects?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
