using ERPFastTrack.Abstraction.Delegates;
using ERPFastTrack.Abstraction.Interfaces;
using ERPFastTrack.Abstraction.Models.SourceData;
using ERPFastTrack.DBGround.Context;
using ERPFastTrack.APIModels.OperationsModels.Request;
using ERPFastTrack.APIModels.OperationsModels.Response;
using ERPFastTrack.SourceProcessor.Sources.File;
using ERPFastTrack.SourceProcessor.Sources.SQLServer;
using Microsoft.EntityFrameworkCore;

namespace ERPFastTrack.API.Internals.Controllers.InternalBase.Operations
{
    
    
    public class SourceOperationsBase
    {
        private readonly ERPFastTrackUIContext _context;
        private readonly ProcessorFactory _processorFactory;
        private readonly IProcessor _sourceDataProcessor;

        public SourceOperationsBase(ProcessorFactory processorFactory, ERPFastTrackUIContext context)
        {
            _context = context;
            _processorFactory = processorFactory;
            _sourceDataProcessor = processorFactory("SourceDataProcessor");
        }

        public async Task<GetSourceDetailsResponse> GetSourceDataDetails(GetSourceDetailsRequest request)
        {
            GetSourceDetailsResponse response = new();
            try
            {
                switch (request.SourceType)
                {
                    case Common.Static.ProjectTypesEnum.SQLSERVER:
                        var query = _context.QueryConfigurations.Where(x => x.Id == request.Id).Include(x=> x.DatabaseConnection).First();

                        SQLServerSourceRequest sqlServerSourceRequest = new();
                        sqlServerSourceRequest.Query = query.QueryDetails;
                        sqlServerSourceRequest.ConnStr = query.DatabaseConnection.ConnectionString;

                        SourceDataDetails result1 = await _sourceDataProcessor.RunAsync<SourceDataDetails, SQLServerSourceRequest>(sqlServerSourceRequest);

                        response.Details = result1;
                        response.Status = true;
                        break;
                    case Common.Static.ProjectTypesEnum.FILESOURCE:
                        var fileSourceDet = _context.FileSourceDetails.Where(x => x.Id == request.Id).Include(x => x.FileSourceConnection).First();
                        string filePath = Path.Combine(fileSourceDet.FileSourceConnection.FileLocation, fileSourceDet.FileName);

                        FileSourceRequest fileSourceRequest = new();
                        fileSourceRequest.FilePath = filePath;
                        fileSourceRequest.HasHeader = fileSourceDet.HasHeader;

                        SourceDataDetails result2 = await _sourceDataProcessor.RunAsync<SourceDataDetails, FileSourceRequest>(fileSourceRequest);

                        response.Details = result2;
                        response.Status = true;
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.ErrorMessage = ex.Message;
            }

            return response;
        }
    }
}
