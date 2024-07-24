using ERPFastTrack.DBGround.Context;
using ERPFastTrack.Abstraction.AbstractClass;
using Microsoft.EntityFrameworkCore;
using ERPFastTrack.DBGround.DBModels.Custom;
using ERPFastTrack.Common.Operations;
using ERPFastTrack.APIModels.OperationsModels.Response;
using ERPFastTrack.APIModels.OperationsModels.Request;
using ERPFastTrack.APIModels.Common;
using Cronos;
using ERPFastTrack.Common.Static;

namespace ERPFastTrack.API.Internals.Controllers.InternalBase.Operations
{
    
    
    public class SchedulingBase
    {
        private readonly ERPFastTrackUIContext _context;
        private readonly OrgRoleManagerAbstract _roleManager;

        public SchedulingBase(ERPFastTrackUIContext context, OrgRoleManagerAbstract roleManager)
        {
            _context = context;
            _roleManager = roleManager;
        }

        public async Task<GetAllSchedulingResponse> GetAll()
        {
            GetAllSchedulingResponse response = new();

            if (_context.Schedulings == null)
            {
                response.ErrorMessage = "Not Found";
                return response;
            }
            var data = await _context.Schedulings.Where(x => x.OrgId == _roleManager.Role.OrgId).ToListAsync();

            if (data.Count > 0)
            {
                response.Data = new();

                foreach (var item in data)
                {
                    GetSchedulingResponse obj = new();
                    Utils.MapProperties(item, obj);
                    var executions = _context.Executions.Where(x => x.ScheduleId == item.Id).ToList();
                    obj.ExecutionStatus = executions.OrderBy(x => x.Id).LastOrDefault()?.Status ?? 1;
                    obj.NoOfExecutions = executions.Count();

                    var project = await _context.Projects.FindAsync(item.PId);
                    obj.ProjectTypeId = project.ProjectTypeId;
                    obj.DestinationTypeId = project.DestinationTypeId;
                    obj.JobDetails = new();

                    var schedulingDetailLst = _context.SchedulingDetails.Where(x => x.ScheduleId == item.Id).ToList();
                    foreach (var schedulingDetails in item.SchedulingDetails)
                    {
                        var job = await _context.Jobs.FindAsync(schedulingDetails.JobId);

                        _context.Entry(job).Reference(x => x.QueryConfiguration).Load();
                        _context.Entry(job).Reference(x => x.FileSourceDetails).Load();


                        string destName = string.Empty;
                        switch ((DestinationTypesEnum)obj.DestinationTypeId)
                        {
                            case DestinationTypesEnum.SALESFORCE:
                                destName = job.SObjectName;
                                break;
                            case DestinationTypesEnum.CSV:
                                break;
                            default:
                                break;
                        }

                        switch ((ProjectTypesEnum)obj.ProjectTypeId)
                        {
                            case ProjectTypesEnum.SQLSERVER:

                                obj.JobDetails.Add(
                                    new JobDetail
                                    {
                                        Id = schedulingDetails.JobId,
                                        Name = job.Name,
                                        SourceName = job.QueryConfiguration.QueryName,
                                        DestName = destName
                                    });
                                break;
                            case ProjectTypesEnum.FILESOURCE:

                                obj.JobDetails.Add(
                                    new JobDetail
                                    {
                                        Id = schedulingDetails.JobId,
                                        Name = job.Name,
                                        SourceName = job.FileSourceDetails.FileSourceDetailName,
                                        DestName = destName
                                    });
                                break;
                            default:
                                break;
                        }
                    }

                    response.Data.Add(obj);
                }
                response.Status = true;
            }
            return response;

        }

        public async Task<GetSchedulingResponse> Get(int id)
        {
            GetSchedulingResponse response = new();

            if (_context.Schedulings == null)
            {
                response.ErrorMessage = "Not Found";
                return response;
            }
            var scheduling = await _context.Schedulings.FindAsync(id);

            if (scheduling == null)
            {
                response.ErrorMessage = "Not Found";
                return response;
            }

            Utils.MapProperties(scheduling, response);
            var executions = _context.Executions.Where(x => x.ScheduleId == scheduling.Id);
            response.ExecutionStatus = executions.OrderBy(x => x.Id).LastOrDefault()?.Status ?? 1;
            response.NoOfExecutions = executions.Count();

            var project = await _context.Projects.FindAsync(scheduling.PId);
            response.ProjectTypeId = project.ProjectTypeId;
            response.DestinationTypeId = project.DestinationTypeId;
            response.JobDetails = new();

            var schedulingDetailLst = _context.SchedulingDetails.Where(x => x.ScheduleId == scheduling.Id).ToList();
            foreach (var schedulingDetails in schedulingDetailLst)
            {
                var job = await _context.Jobs.FindAsync(schedulingDetails.JobId);

                _context.Entry(job).Reference(x => x.QueryConfiguration).Load();
                _context.Entry(job).Reference(x => x.FileSourceDetails).Load();

                string destName = string.Empty;
                switch ((DestinationTypesEnum)response.DestinationTypeId)
                {
                    case DestinationTypesEnum.SALESFORCE:
                        destName = job.SObjectName;
                        break;
                    case DestinationTypesEnum.CSV:
                        break;
                    default:
                        break;
                }

                switch ((ProjectTypesEnum)response.ProjectTypeId)
                {
                    case ProjectTypesEnum.SQLSERVER:

                        response.JobDetails.Add(
                            new JobDetail
                            {
                                Id = schedulingDetails.JobId,
                                Name = job.Name,
                                SourceName = job.QueryConfiguration.QueryName,
                                DestName = destName
                            });
                        break;
                    case ProjectTypesEnum.FILESOURCE:

                        response.JobDetails.Add(
                            new JobDetail
                            {
                                Id = schedulingDetails.JobId,
                                Name = job.Name,
                                SourceName = job.FileSourceDetails.FileSourceDetailName,
                                DestName = destName
                            });
                        break;
                    default:
                        break;
                }
            }

            response.Status = true;

            return response;
        }

        public async Task<UpsertSchedulingResponse> Update(int id, UpsertSchedulingRequest request)
        {
            UpsertSchedulingResponse response = new();

            request.OrgId = _roleManager.Role.OrgId;
            request.Id = id;

            Scheduling scheduling = new();

            switch (request.ScheduleType)
            {
                case (int)APIModels.Common.ScheduleType.IMMIDIATELY:
                    request.StartDate = null;
                    request.EndDate = null;
                    request.HourlyMinutes = null;
                    request.MonthDay = null;
                    request.MonthTime = null;
                    request.DailyTime = null;
                    break;
                case (int)APIModels.Common.ScheduleType.HOURLY:
                    request.MonthDay = null;
                    request.MonthTime = null;
                    request.DailyTime = null;
                    break;
                case (int)APIModels.Common.ScheduleType.DAILY:
                    request.HourlyMinutes = null;
                    request.MonthDay = null;
                    request.MonthTime = null;
                    break;
                case (int)APIModels.Common.ScheduleType.MONTHLY:
                    request.HourlyMinutes = null;
                    request.DailyTime = null;
                    break;
                default:
                    break;
            }
            Utils.MapProperties(request, scheduling);

            _context.Entry(scheduling).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            var existingScheduling = await _context.Schedulings.FindAsync(id);
            if (existingScheduling != null)
            {
                _context.Entry(existingScheduling).Collection(e => e.SchedulingDetails).Load();
            }

            foreach (var schedulingDetail in existingScheduling.SchedulingDetails)
            {
                _context.SchedulingDetails.Remove(schedulingDetail);
            }

            int i = 1;
            foreach (var jobId in request.JobIds)
            {
                SchedulingDetails schedulingDetail = new();
                schedulingDetail.JobId = jobId;
                schedulingDetail.ScheduleId = id;
                schedulingDetail.Priority = i++;
                _context.SchedulingDetails.Add(schedulingDetail);
            }

            var executions = _context.Executions.FirstOrDefault(x => x.ScheduleId == id);
            if (executions == null)
            {
                DateTime? nextExecutionDateTime = DateTime.Now;
                GetScheduleTimings(request, ref nextExecutionDateTime);

                Execution execution = new();
                execution.ScheduleId = scheduling.Id;
                execution.OrgId = request.OrgId;
                execution.Status = (int)ExecutionStatus.SCHEDULED;
                execution.ScheduledAt = (DateTime)nextExecutionDateTime;
                _context.Executions.Add(execution);
                await _context.SaveChangesAsync();
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!SchedulingExists(id))
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



        public async Task<UpsertSchedulingResponse> Insert(UpsertSchedulingRequest request)
        {
            UpsertSchedulingResponse response = new();

            if (_context.Schedulings == null)
            {
                response.ErrorMessage = "Entity set 'ERPFastTrackUIContext.Schedulings'  is null.";
                return response;
            }
            request.OrgId = _roleManager.Role.OrgId;

            Scheduling scheduling = new();

            DateTime? nextExecutionDateTime = DateTime.Now;
            GetScheduleTimings(request, ref nextExecutionDateTime);

            Utils.MapProperties(request, scheduling);
            _context.Schedulings.Add(scheduling);
            await _context.SaveChangesAsync();

            int i = 1;
            foreach (var jobId in request.JobIds)
            {
                SchedulingDetails schedulingDetail = new();
                schedulingDetail.JobId = jobId;
                schedulingDetail.ScheduleId = scheduling.Id;
                schedulingDetail.Priority = i++;
                _context.SchedulingDetails.Add(schedulingDetail);
            }
            await _context.SaveChangesAsync();

            if (request.IsActive)
            {
                Execution execution = new();
                execution.ScheduleId = scheduling.Id;
                execution.OrgId = request.OrgId;
                execution.Status = (int)ExecutionStatus.SCHEDULED;
                execution.ScheduledAt = (DateTime)nextExecutionDateTime;
                _context.Executions.Add(execution);
                await _context.SaveChangesAsync();
            }

            response.Status = true;

            return response;
        }

        private static void GetScheduleTimings(UpsertSchedulingRequest request, ref DateTime? nextExecutionDateTime)
        {
            CronExpression expression = null;
            switch (request.ScheduleType)
            {
                case (int)APIModels.Common.ScheduleType.IMMIDIATELY:
                    request.StartDate = null;
                    request.EndDate = null;
                    request.HourlyMinutes = null;
                    request.MonthDay = null;
                    request.MonthTime = null;
                    request.DailyTime = null;
                    nextExecutionDateTime = DateTime.Now;
                    break;
                case (int)APIModels.Common.ScheduleType.HOURLY:
                    request.MonthDay = null;
                    request.MonthTime = null;
                    request.DailyTime = null;
                    DateTime startDate0 = request.StartDate < DateTime.Now ? DateTime.Now : (DateTime)request.StartDate;
                    DateTime utcStartDate0 = startDate0.Kind == DateTimeKind.Utc ? startDate0 : startDate0.ToUniversalTime();
                    expression = CronExpression.Parse($"{request.HourlyMinutes} 0-23 * * *");
                    nextExecutionDateTime = expression.GetNextOccurrence(utcStartDate0, TimeZoneInfo.Local).Value.ToLocalTime();
                    break;
                case (int)APIModels.Common.ScheduleType.DAILY:
                    request.HourlyMinutes = null;
                    request.MonthDay = null;
                    request.MonthTime = null;
                    DateTime startDate1 = request.StartDate < DateTime.Now ? DateTime.Now : (DateTime)request.StartDate;
                    DateTime utcStartDate1 = startDate1.Kind == DateTimeKind.Utc ? startDate1 : startDate1.ToUniversalTime();
                    expression = CronExpression.Parse($"{request.DailyTime.Value.Minutes} {request.DailyTime.Value.Hours} * * *");
                    nextExecutionDateTime = expression.GetNextOccurrence(utcStartDate1, TimeZoneInfo.Local).Value.ToLocalTime();
                    break;
                case (int)APIModels.Common.ScheduleType.MONTHLY:
                    request.HourlyMinutes = null;
                    request.DailyTime = null;
                    DateTime startDate2 = request.StartDate < DateTime.Now ? DateTime.Now : (DateTime)request.StartDate;
                    DateTime utcStartDate2 = startDate2.Kind == DateTimeKind.Utc ? startDate2 : startDate2.ToUniversalTime();
                    expression = CronExpression.Parse($"{request.MonthTime.Value.Minutes} {request.MonthTime.Value.Hours} {request.MonthDay} * *");
                    nextExecutionDateTime = expression.GetNextOccurrence(utcStartDate2, TimeZoneInfo.Local).Value.ToLocalTime();
                    break;
                default:
                    break;
            }
        }

        public async Task<UpsertSchedulingResponse> Delete(int id)
        {
            UpsertSchedulingResponse response = new();
            if (_context.Schedulings == null)
            {
                response.ErrorMessage = "Not Found";
                return response;
            }

            var scheduling = await _context.Schedulings.FindAsync(id);
            if (scheduling == null)
            {
                response.ErrorMessage = "Not Found";
                return response;
            }

            var existingScheduling = await _context.Schedulings.FindAsync(id);

            var schedulingDetailLst = _context.SchedulingDetails.Where(x => x.ScheduleId == existingScheduling.Id);
            foreach (var schedulingDetail in schedulingDetailLst)
            {
                _context.SchedulingDetails.Remove(schedulingDetail);
            }

            _context.Schedulings.Remove(scheduling);
            await _context.SaveChangesAsync();

            response.Status = true;

            return response;
        }

        private bool SchedulingExists(int id)
        {
            return (_context.Schedulings?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        public async Task<GetAllSchedulingResponse> GetAllForProject(int id)
        {
            GetAllSchedulingResponse response = new();

            if (_context.Schedulings == null)
            {
                response.ErrorMessage = "Not Found";
                return response;
            }
            var data = await _context.Schedulings.Where(x => x.OrgId == _roleManager.Role.OrgId && x.PId == id).ToListAsync();

            if (data.Count > 0)
            {
                response.Data = new();

                foreach (var item in data)
                {
                    GetSchedulingResponse obj = new();
                    Utils.MapProperties(item, obj);
                    var executions = _context.Executions.Where(x => x.ScheduleId == item.Id).ToList();
                    obj.ExecutionStatus = executions.OrderBy(x => x.Id).LastOrDefault()?.Status ?? 1;
                    obj.NoOfExecutions = executions.Count();

                    var project = await _context.Projects.FindAsync(item.PId);
                    obj.ProjectTypeId = project.ProjectTypeId;
                    obj.DestinationTypeId = project.DestinationTypeId;
                    obj.JobDetails = new();

                    var schedulingDetailLst = _context.SchedulingDetails.Where(x => x.ScheduleId == item.Id).ToList();
                    foreach (var schedulingDetails in schedulingDetailLst)
                    {
                        var job = await _context.Jobs.FindAsync(schedulingDetails.JobId);

                        _context.Entry(job).Reference(x => x.QueryConfiguration).Load();
                        _context.Entry(job).Reference(x => x.FileSourceDetails).Load();

                        string destName = string.Empty;
                        switch ((DestinationTypesEnum)obj.DestinationTypeId)
                        {
                            case DestinationTypesEnum.SALESFORCE:
                                destName = job.SObjectName;
                                break;
                            case DestinationTypesEnum.CSV:
                                break;
                            default:
                                break;
                        }

                        switch ((ProjectTypesEnum)obj.ProjectTypeId)
                        {
                            case ProjectTypesEnum.SQLSERVER:

                                obj.JobDetails.Add(
                                    new JobDetail
                                    {
                                        Id = schedulingDetails.JobId,
                                        Name = job.Name,
                                        SourceName = job.QueryConfiguration.QueryName,
                                        DestName = destName
                                    });
                                break;
                            case ProjectTypesEnum.FILESOURCE:

                                obj.JobDetails.Add(
                                    new JobDetail
                                    {
                                        Id = schedulingDetails.JobId,
                                        Name = job.Name,
                                        SourceName = job.FileSourceDetails.FileSourceDetailName,
                                        DestName = destName
                                    });
                                break;
                            default:
                                break;
                        }
                    }

                    response.Data.Add(obj);
                }
                response.Status = true;
            }
            return response;

        }
    }
}
