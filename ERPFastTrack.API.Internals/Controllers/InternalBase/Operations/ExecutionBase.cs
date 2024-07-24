using ERPFastTrack.Abstraction.AbstractClass;
using ERPFastTrack.APIModels.OperationsModels.Response;
using ERPFastTrack.Common.Operations;
using ERPFastTrack.Common.Static;
using ERPFastTrack.DBGround.Context;
using ERPFastTrack.DBGround.CustomDBQueries;
using ERPFastTrack.DBGround.DBModels.Custom;
using Microsoft.EntityFrameworkCore;

namespace ERPFastTrack.API.Internals.Controllers.InternalBase.Operations
{
    
    
    public class ExecutionBase
    {
        private readonly ERPFastTrackUIContext _context;
        private readonly OrgRoleManagerAbstract _roleManager;

        public ExecutionBase(ERPFastTrackUIContext context, OrgRoleManagerAbstract roleManager)
        {
            _context = context;
            _roleManager = roleManager;
        }

        public async Task<GetAllExecutionResponse> GetAll()
        {
            GetAllExecutionResponse response = new();

            if (_context.Executions == null)
            {
                response.ErrorMessage = "Not Found";
                return response;
            }

            var executionCustomQueryRes = DbService.RawSqlQuery("SELECT S.Id as SID, E.Id as EID, E.Status as Status, S.Name, (SELECT COUNT(*) FROM [SchedulingDetails] SD WHERE SD.ScheduleId = S.Id) AS SD_Count, S.PId as PID, E.ScheduledAt as ScheduledAt from [Executions] E, [Schedulings] S WHERE E.ScheduleId = S.Id AND E.OrgId = " + _roleManager.Role.OrgId,
    x => {
        return new ExecutionCustomQuery { ScheduleId = (int)x[0], ExecutionId = (int)x[1], Status = (int)x[2], Name = (string)x[3], SDCount = (int)x[4], ProjectId = (int)x[5], ScheduledAt = (DateTime)x[6] };

    }, _context);

            if (executionCustomQueryRes != null)
            {
                response.Data = new();
                foreach (var item in executionCustomQueryRes)
                {
                    var project = await _context.Projects.FindAsync(item.ProjectId);

                    GetExecutionResponse obj = new();
                    obj.ScheduleName = item.Name;
                    obj.NumberOfJobs = item.SDCount;
                    obj.Id = item.ExecutionId;
                    var executionDetails = await _context.ExecutionDetails.Where(x => x.ExecutionId == item.ExecutionId).ToListAsync();
                    obj.ScheduledAt = item.ScheduledAt;
                    obj.SuccessfulJobs = executionDetails.Where(x => x.Status).ToList().Count;
                    obj.FailedJobs = executionDetails.Where(x => !x.Status).ToList().Count;
                    obj.JobDetails = new();
                    foreach (var executionDetail in executionDetails)
                    {
                        var job = await _context.Jobs.FindAsync(executionDetail.JobId);
                        var executionDet = _context.ExecutionDetailedHistories.Where(x => x.ExecutionDetailId == executionDetail.Id).FirstOrDefault();

                        JobDetails jobDetail = new JobDetails();
                        Utils.MapProperties(executionDetail, jobDetail);

                        jobDetail.Name = job.Name;
                        jobDetail.ExternalId = executionDet?.ExternalIdName;
                        var projectType = (ProjectTypesEnum)project.ProjectTypeId;
                        jobDetail.ProjectType = (int)projectType;
                        switch (projectType)
                        {
                            case ProjectTypesEnum.SQLSERVER:
                                var dbConnection = _context.DatabaseConnections.Where(x => x.Id == project.DbConnId).Select(q => q.Name).FirstOrDefault();
                                jobDetail.SourceName = dbConnection;
                                var queryConfiguration = _context.QueryConfigurations.Where(x => x.Id == job.QueryId).Select(q => q.QueryName).FirstOrDefault();
                                jobDetail.SourceDetName = queryConfiguration;
                                break;
                            case ProjectTypesEnum.FILESOURCE:
                                var fileSourceConnection = _context.FileSourceConnections.Where(x => x.Id == project.FsConnId).Select(q => q.Name).FirstOrDefault();
                                jobDetail.SourceName = fileSourceConnection;
                                var fileSourceDetails = _context.FileSourceDetails.Where(x => x.Id == job.FileSourceDetailId).Select(q => q.FileSourceDetailName).FirstOrDefault();
                                jobDetail.SourceDetName = fileSourceDetails;
                                break;
                            default:
                                break;
                        }
                        var destType = (DestinationTypesEnum)project.DestinationTypeId;
                        jobDetail.DestinationType = (int)destType;
                        switch (destType)
                        {
                            case DestinationTypesEnum.SALESFORCE:
                                var salesfoceConnection = _context.SalesforceConnections.Where(x => x.Id == project.SfConnId).Select(q => q.Name).FirstOrDefault();
                                jobDetail.DestName = salesfoceConnection;
                                jobDetail.DestDetName = job.SObjectName;
                                break;
                            case DestinationTypesEnum.CSV:
                                break;
                            default:
                                break;
                        }

                        obj.JobDetails.Add(jobDetail);
                    }
                    obj.ExecutionStatus = item.Status;
                    response.Data.Add(obj);
                }

                response.Status = true;
            }
            return response;

        }

        public async Task<GetExecutionResponse> Get(int id)
        {
            GetExecutionResponse response = new();

            if (_context.Executions == null)
            {
                response.ErrorMessage = "Not Found";
                return response;
            }
            var execution = DbService.RawSqlQuery("SELECT S.Id as SID, E.Id as EID, E.Status as Status, S.Name, (SELECT COUNT(*) FROM [SchedulingDetails] SD WHERE SD.ScheduleId = S.Id) AS SD_Count, S.PId as PID, E.ScheduledAt as ScheduledAt from [Executions] E, [Schedulings] S WHERE E.ScheduleId = S.Id AND E.OrgId = " + _roleManager.Role.OrgId + " AND E.Id = " + id,
    x => {
        return new ExecutionCustomQuery { ScheduleId = (int)x[0], ExecutionId = (int)x[1], Status = (int)x[2], Name = (string)x[3], SDCount = (int)x[4], ProjectId = (int)x[5], ScheduledAt = (DateTime)x[6] };

    }, _context).FirstOrDefault();

            if (execution == null)
            {
                response.ErrorMessage = "Not Found";
                return response;
            }



            response.ScheduleName = execution.Name;
            response.NumberOfJobs = execution.SDCount;
            var executionDetails = await _context.ExecutionDetails.Where(x => x.ExecutionId == execution.ExecutionId).ToListAsync();
            response.ScheduledAt = execution.ScheduledAt;
            response.SuccessfulJobs = executionDetails.Where(x => x.Status).ToList().Count;
            response.FailedJobs = executionDetails.Where(x => !x.Status).ToList().Count;
            response.JobDetails = new();

            var project = await _context.Projects.FindAsync(execution.ProjectId);
            foreach (var executionDetail in executionDetails)
            {
                var job = await _context.Jobs.FindAsync(executionDetail.JobId);
                var executionDet = _context.ExecutionDetailedHistories.Where(x => x.ExecutionDetailId == executionDetail.Id).FirstOrDefault();

                JobDetails jobDetail = new JobDetails();
                Utils.MapProperties(executionDetail, jobDetail);

                jobDetail.Name = job.Name;
                jobDetail.ExternalId = executionDet?.ExternalIdName;
                var projectType = (ProjectTypesEnum)project.ProjectTypeId;
                jobDetail.ProjectType = (int)projectType;
                switch (projectType)
                {
                    case ProjectTypesEnum.SQLSERVER:
                        var dbConnection = _context.DatabaseConnections.Where(x => x.Id == project.DbConnId).Select(q => q.Name).FirstOrDefault();
                        jobDetail.SourceName = dbConnection;
                        var queryConfiguration = _context.QueryConfigurations.Where(x => x.Id == job.QueryId).Select(q => q.QueryName).FirstOrDefault();
                        jobDetail.SourceDetName = queryConfiguration;
                        break;
                    case ProjectTypesEnum.FILESOURCE:
                        var fileSourceConnection = _context.FileSourceConnections.Where(x => x.Id == project.FsConnId).Select(q => q.Name).FirstOrDefault();
                        jobDetail.SourceName = fileSourceConnection;
                        var fileSourceDetails = _context.FileSourceDetails.Where(x => x.Id == job.FileSourceDetailId).Select(q => q.FileSourceDetailName).FirstOrDefault();
                        jobDetail.SourceDetName = fileSourceDetails;
                        break;
                    default:
                        break;
                }
                var destType = (DestinationTypesEnum)project.DestinationTypeId;
                jobDetail.DestinationType = (int)destType;
                switch (destType)
                {
                    case DestinationTypesEnum.SALESFORCE:
                        var salesfoceConnection = _context.SalesforceConnections.Where(x => x.Id == project.SfConnId).Select(q => q.Name).FirstOrDefault();
                        jobDetail.DestName = salesfoceConnection;
                        jobDetail.DestDetName = job.SObjectName;
                        break;
                    case DestinationTypesEnum.CSV:
                        break;
                    default:
                        break;
                }
                response.JobDetails.Add(jobDetail);
            }
            response.ExecutionStatus = execution.Status;
            response.Status = true;
            return response;
        }

        private bool ExecutionExists(int id)
        {
            return (_context.Executions?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        public async Task<GetAllExecutionDetailsHistoryResponse> GetHistoryForExecutionDetails(int id)
        {
            GetAllExecutionDetailsHistoryResponse response = new();

            if (_context.Executions == null)
            {
                response.ErrorMessage = "Not Found";
                return response;
            }
            var data = await _context.ExecutionDetailedHistories.Where(x => x.ExecutionDetail.Execution.OrgId == _roleManager.Role.OrgId && x.ExecutionDetailId == id).ToListAsync();

            if (data.Count > 0)
            {
                response.Data = new();

                foreach (var item in data)
                {
                    GetExecutionDetailsHistoryResponse obj = new();
                    Utils.MapProperties(item, obj);

                    response.Data.Add(obj);
                }
                response.Status = true;
            }
            return response;
        }

        public async Task<GetExecutionDetailsHistoryResponse> GetExecutionHistory(int id)
        {
            GetExecutionDetailsHistoryResponse response = new();

            if (_context.Executions == null)
            {
                response.ErrorMessage = "Not Found";
                return response;
            }
            var item = await _context.ExecutionDetailedHistories.Where(x => x.ExecutionDetail.Execution.OrgId == _roleManager.Role.OrgId && x.Id == id).FirstAsync();

            Utils.MapProperties(item, response);
            response.Status = true;

            return response;
        }

        public async Task<GetAllExecutionResponse> GetAllForSchedule(int id)
        {
            GetAllExecutionResponse response = new();

            if (_context.Executions == null)
            {
                response.ErrorMessage = "Not Found";
                return response;
            }

            var executionCustomQueryRes = DbService.RawSqlQuery("SELECT S.Id as SID, E.Id as EID, E.Status as Status, S.Name, (SELECT COUNT(*) FROM [SchedulingDetails] SD WHERE SD.ScheduleId = S.Id) AS SD_Count, S.PId as PID, E.ScheduledAt as ScheduledAt from [Executions] E, [Schedulings] S WHERE E.ScheduleId = S.Id AND E.OrgId = " + _roleManager.Role.OrgId + " AND E.ScheduleId = " + id,
   x => new ExecutionCustomQuery { ScheduleId = (int)x[0], ExecutionId = (int)x[1], Status = (int)x[2], Name = (string)x[3], SDCount = (int)x[4], ProjectId = (int)x[5], ScheduledAt = (DateTime)x[6] }, _context);

            if (executionCustomQueryRes != null)
            {
                response.Data = new();
                foreach (var item in executionCustomQueryRes)
                {
                    var project = await _context.Projects.FindAsync(item.ProjectId);

                    GetExecutionResponse obj = new();
                    obj.ScheduleName = item.Name;
                    obj.NumberOfJobs = item.SDCount;
                    obj.Id = item.ExecutionId;
                    obj.ScheduledAt = item.ScheduledAt;

                    var executionDetails = await _context.ExecutionDetails.Where(x => x.ExecutionId == item.ExecutionId).ToListAsync();

                    obj.SuccessfulJobs = executionDetails.Where(x => x.Status).ToList().Count;
                    obj.FailedJobs = executionDetails.Where(x => !x.Status).ToList().Count;
                    obj.JobDetails = new();
                    foreach (var executionDetail in executionDetails)
                    {
                        var job = await _context.Jobs.FindAsync(executionDetail.JobId);
                        var executionDet = _context.ExecutionDetailedHistories.Where(x => x.ExecutionDetailId == executionDetail.Id).FirstOrDefault();

                        JobDetails jobDetail = new JobDetails();
                        Utils.MapProperties(executionDetail, jobDetail);

                        jobDetail.Name = job.Name;
                        jobDetail.ExternalId = executionDet?.ExternalIdName;
                        var projectType = (ProjectTypesEnum)project.ProjectTypeId;
                        jobDetail.ProjectType = (int)projectType;
                        switch (projectType)
                        {
                            case ProjectTypesEnum.SQLSERVER:
                                var dbConnection = _context.DatabaseConnections.Where(x => x.Id == project.DbConnId).Select(q => q.Name).FirstOrDefault();
                                jobDetail.SourceName = dbConnection;
                                var queryConfiguration = _context.QueryConfigurations.Where(x => x.Id == job.QueryId).Select(q => q.QueryName).FirstOrDefault();
                                jobDetail.SourceDetName = queryConfiguration;
                                break;
                            case ProjectTypesEnum.FILESOURCE:
                                var fileSourceConnection = _context.FileSourceConnections.Where(x => x.Id == project.FsConnId).Select(q => q.Name).FirstOrDefault();
                                jobDetail.SourceName = fileSourceConnection;
                                var fileSourceDetails = _context.FileSourceDetails.Where(x => x.Id == job.FileSourceDetailId).Select(q => q.FileSourceDetailName).FirstOrDefault();
                                jobDetail.SourceDetName = fileSourceDetails;
                                break;
                            default:
                                break;
                        }
                        var destType = (DestinationTypesEnum)project.DestinationTypeId;
                        jobDetail.DestinationType = (int)destType;
                        switch (destType)
                        {
                            case DestinationTypesEnum.SALESFORCE:
                                var salesfoceConnection = _context.SalesforceConnections.Where(x => x.Id == project.SfConnId).Select(q => q.Name).FirstOrDefault();
                                jobDetail.DestName = salesfoceConnection;
                                jobDetail.DestDetName = job.SObjectName;
                                break;
                            case DestinationTypesEnum.CSV:
                                break;
                            default:
                                break;
                        }

                        obj.JobDetails.Add(jobDetail);
                    }
                    obj.ExecutionStatus = item.Status;
                    response.Data.Add(obj);
                }

                response.Status = true;
            }
            return response;
        }
    }
}
