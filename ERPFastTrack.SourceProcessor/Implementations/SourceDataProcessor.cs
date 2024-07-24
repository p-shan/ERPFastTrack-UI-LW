using ERPFastTrack.Abstraction.Interfaces;
using ERPFastTrack.Abstraction.Models.SourceData;
using ERPFastTrack.SourceProcessor.Sources.File;
using ERPFastTrack.SourceProcessor.Sources.SQLServer;

namespace ERPFastTrack.SourceProcessor.Implementations
{
    public class SourceDataProcessor : IProcessor
    {
        public SourceDataProcessor(IOperationsSource<SQLServerSourceRequest> sqlServerSourceOperation, IOperationsSource<FileSourceRequest> fileSourceOperation)
        {
            SqlServerSourceOperation = sqlServerSourceOperation;
            FileSourceOperation = fileSourceOperation;
        }

        public IOperationsSource<SQLServerSourceRequest> SqlServerSourceOperation { get; }
        public IOperationsSource<FileSourceRequest> FileSourceOperation { get; }

        public async Task<T> RunAsync<T, R>(R request) where T : new()
        {
            SourceDataDetails response = null;
            if (request != null)
            {
                if(request is SQLServerSourceRequest)
                {
                    response = SqlServerSourceOperation.GetDetails((SQLServerSourceRequest)(dynamic)request);
                }
                else if (request is FileSourceRequest)
                {
                    response = FileSourceOperation.GetDetails((FileSourceRequest)(dynamic)request);
                }
            }

            return response ?? (dynamic)response;
        }
    }
}
