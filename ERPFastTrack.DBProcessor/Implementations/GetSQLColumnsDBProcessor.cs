using Carbunql;
using ERPFastTrack.Abstraction.Interfaces;
using ERPFastTrack.DBProcessor.Common;
using ERPFastTrack.DBProcessor.Models;
using Microsoft.Extensions.Options;

namespace ERPFastTrack.DBProcessor.Implementations
{
    public class GetSQLColumnsDBProcessor : IProcessor
    {
        public async Task<T> RunAsync<T, R>(R request) where T : new()
        {
            SQLColumnsResult response = null;
            if (request != null)
            {
                SQLColumnsRequest detailsObj = (SQLColumnsRequest)(dynamic)request;
                if (detailsObj.ProcessingType == DBProcessingType.COLUMNFROMSQL)
                {
                    response = new SQLColumnsResult() { Columns = ProcessByColumnInSQL(detailsObj) };
                }
            }

            return response ?? (dynamic)response;
        }

        private List<string> ProcessByColumnInSQL(SQLColumnsRequest detailsObj)
        {
            List<string> columns = new();

            var sq = new SelectQuery(detailsObj.QueryText);
            sq.SelectClause!.Items.ForEach(item =>
            {
                //columns.Add(item.Value.ToOneLineCommand().CommandText);
                columns.Add(item.Alias);
            });

            return columns;
        }
    }
}
