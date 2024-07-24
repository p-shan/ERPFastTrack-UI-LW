using CsvHelper.Configuration;
using CsvHelper;
using ERPFastTrack.Abstraction.Delegates;
using ERPFastTrack.Abstraction.Interfaces;
using ERPFastTrack.APIModels.DBOpsModels.Response;
using ERPFastTrack.DBGround.Context;
using ERPFastTrack.DBProcessor.Models;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace ERPFastTrack.API.Internals.Controllers
{
    [Route("api/[controller]")]
    public class DBOpsController : ControllerBase
    {
        private readonly ERPFastTrackUIContext _context;
        private readonly ProcessorFactory _processorFactory;
        private readonly IProcessor dbProcessor;
        private readonly IProcessor getSqlColumnsProcessor;

        public DBOpsController(ProcessorFactory processorFactory, ERPFastTrackUIContext context)
        {
            _context = context;
            _processorFactory = processorFactory;
            getSqlColumnsProcessor = processorFactory("GetSQLColumnsDBProcessor");
        }

        [HttpGet("getSQLColumns/{id}")]
        public async Task<GetSqlColumnsAPIResponse> GetSQLColumns(int id)
        {
            GetSqlColumnsAPIResponse response = new();
            try
            {
                SQLColumnsResult sqlColumnsResult = await getSqlColumnsProcessor.RunAsync<SQLColumnsResult, SQLColumnsRequest>(new SQLColumnsRequest()
                {
                    QueryText = (await _context.QueryConfigurations.FindAsync(id)).QueryDetails,
                    ProcessingType = DBProcessingType.COLUMNFROMSQL
                });

                response.Status = true;
                response.Columns = sqlColumnsResult.Columns;
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.ErrMessage = ex.Message;
            }

            return response;
        }

        [HttpGet("getFileHeaders/{id}")]
        public async Task<GetFileHeadersAPIResponse> GetFileHeaders(int id)
        {
            GetFileHeadersAPIResponse response = new();
            try
            {
                List<string> headers = new List<string>();
                var fileSourceDet = await _context.FileSourceDetails.FindAsync(id);
                var fileSourceConnection = await _context.FileSourceConnections.FindAsync(fileSourceDet.FsConnId);
                string filePath = Path.Combine(fileSourceConnection.FileLocation, fileSourceDet.FileName);

                using (var reader = new StreamReader(filePath))
                using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    Delimiter = ",",
                    HasHeaderRecord = true // Indicates that the first row contains headers
                }))
                {
                    csv.Read();
                    csv.ReadHeader();

                    foreach (string header in csv.HeaderRecord)
                    {
                        headers.Add(header);
                    }
                }

                response.Status = true;
                response.Columns = headers;
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.ErrMessage = ex.Message;
            }

            return response;
        }
    }
}
