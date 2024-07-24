using ERPFastTrack.Abstraction.AbstractClass;
using ERPFastTrack.API.Internals.Controllers.InternalBase.Operations;
using ERPFastTrack.APIModels.OperationsModels.Request;
using ERPFastTrack.APIModels.OperationsModels.Response;
using ERPFastTrack.DBGround.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ERPFastTrack.API.Internals.Controllers.Exposed
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperationsController : ControllerBase
    {
        private readonly ERPFastTrackUIContext _context;
        private readonly OrgRoleManagerAbstract _roleManager;
        private readonly ProjectBase _projectBase;
        private readonly JobBase _jobBase;
        private readonly SchedulingBase _schedulingBase;
        private readonly ExecutionBase _executionBase;
        private readonly UtilityBase _utilityBase;
        private readonly SourceOperationsBase _sourceOperationsBase;

        public OperationsController(ERPFastTrackUIContext context, OrgRoleManagerAbstract roleManager, ProjectBase projectBase, JobBase jobBase, SchedulingBase schedulingBase, ExecutionBase executionBase, UtilityBase utilityBase, SourceOperationsBase sourceOperationsBase)
        {
            _context = context;
            _roleManager = roleManager;
            _projectBase = projectBase;
            _jobBase = jobBase;
            _schedulingBase = schedulingBase;
            _executionBase = executionBase;
            _utilityBase = utilityBase;
            _sourceOperationsBase = sourceOperationsBase;
        }

        #region Utilities
        [HttpPost("utility/testconnection")]
        public async Task<TestConnectionResponse> TestConnection(TestConnectionRequest request)
        {
            return await _utilityBase.TestConnection(request);
        }
        #endregion Utilities

        #region Projects
        [HttpGet("project")]
        public async Task<GetAllProjectResponse> GetSalesforceConnections()
        {
            return await _projectBase.GetAll();
        }

        [HttpGet("project/{id}")]
        public async Task<GetProjectResponse> GetSalesforceConnection(int id)
        {
            return await _projectBase.Get(id);
        }

        [HttpPut("project/{id}")]
        public async Task<UpsertProjectResponse> PutSalesforceConnection(int id, UpsertProjectRequest request)
        {
            return await _projectBase.Update(id, request);
        }

        [HttpPost("project")]
        public async Task<UpsertProjectResponse> PostSalesforceConnection(UpsertProjectRequest request)
        {
            return await _projectBase.Insert(request);
        }

        [HttpDelete("project/{id}")]
        public async Task<UpsertProjectResponse> DeleteSalesforceConnection(int id)
        {
            return await _projectBase.Delete(id);
        }
        #endregion Projects

        #region Jobs
        [HttpGet("job")]
        public async Task<GetAllJobResponse> GetJobs()
        {
            return await _jobBase.GetAll();
        }

        [HttpGet("job/project/{id}")]
        public async Task<GetAllJobResponse> GetProjectJobs(int id)
        {
            return await _jobBase.GetAllForProject(id);
        }

        [HttpGet("job/{id}")]
        public async Task<GetJobResponse> GetJob(int id)
        {
            return await _jobBase.Get(id);
        }

        [HttpPut("job/{id}")]
        public async Task<UpsertJobResponse> PutJob(int id, UpsertJobRequest request)
        {
            return await _jobBase.Update(id, request);
        }

        [HttpPost("job")]
        public async Task<UpsertJobResponse> PostJob(UpsertJobRequest request)
        {
            return await _jobBase.Insert(request);
        }

        [HttpDelete("job/{id}")]
        public async Task<UpsertJobResponse> DeleteJob(int id)
        {
            return await _jobBase.Delete(id);
        }
        #endregion Jobs

        #region Schedulings

        [HttpGet("scheduling/project/{id}")]
        public async Task<GetAllSchedulingResponse> GetProjectSchedules(int id)
        {
            return await _schedulingBase.GetAllForProject(id);
        }

        [HttpGet("scheduling")]
        public async Task<GetAllSchedulingResponse> GetSchedulings()
        {
            return await _schedulingBase.GetAll();
        }

        [HttpGet("scheduling/{id}")]
        public async Task<GetSchedulingResponse> GetScheduling(int id)
        {
            return await _schedulingBase.Get(id);
        }

        [HttpPut("scheduling/{id}")]
        public async Task<UpsertSchedulingResponse> PutScheduling(int id, UpsertSchedulingRequest request)
        {
            return await _schedulingBase.Update(id, request);
        }

        [HttpPost("scheduling")]
        public async Task<UpsertSchedulingResponse> PostScheduling(UpsertSchedulingRequest request)
        {
            return await _schedulingBase.Insert(request);
        }

        [HttpDelete("scheduling/{id}")]
        public async Task<UpsertSchedulingResponse> DeleteScheduling(int id)
        {
            return await _schedulingBase.Delete(id);
        }
        #endregion Schedulings

        #region Executions

        [HttpGet("execution/schedule/{id}")]
        public async Task<GetAllExecutionResponse> GetScheduleExecutions(int id)
        {
            return await _executionBase.GetAllForSchedule(id);
        }

        [HttpGet("execution")]
        public async Task<GetAllExecutionResponse> GetExecutions()
        {
            return await _executionBase.GetAll();
        }

        [HttpGet("execution/{id}")]
        public async Task<GetExecutionResponse> GetExecution(int id)
        {
            return await _executionBase.Get(id);
        }

        [HttpGet("execution/details/{id}")]
        public async Task<GetAllExecutionDetailsHistoryResponse> GetAllExecutionDetailsHistory(int id)
        {
            return await _executionBase.GetHistoryForExecutionDetails(id);
        }

        [HttpGet("execution/details/history/{id}")]
        public async Task<GetExecutionDetailsHistoryResponse> GetExecutionHistory(int id)
        {
            return await _executionBase.GetExecutionHistory(id);
        }

        #endregion Executions

        #region SourceOperations
        [HttpPost("source/datadetails")]
        public async Task<GetSourceDetailsResponse> GetSourceDataDetails(GetSourceDetailsRequest request)
        {
            return await _sourceOperationsBase.GetSourceDataDetails(request);
        }
        #endregion SourceOperations
    }
}
