using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERPFastTrack.DBGround.Context;
using System.Data.SqlClient;

namespace ERPFastTrack.DBGround.CustomDBQueries
{
    public static class SqlChecker
    {
        public static DbDataReader RawSqlQuery(string query, ERPFastTrackUIContext context)
        {
            using (var command = context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = query;
                command.CommandType = CommandType.Text;

                context.Database.OpenConnection();

                using (var result = command.ExecuteReader())
                {
                    return result;
                }
            }
        }

        public static bool CheckSchemaAndTableExist(ERPFastTrackUIContext context)
        {
            var schema = Environment.GetEnvironmentVariable("ERPFT_DB_SCHEMA");
            if (string.IsNullOrWhiteSpace(schema))
                schema = "dbo";

            string query = @"
            SELECT CASE 
                WHEN EXISTS (
                    SELECT 1 
                    FROM information_schema.tables 
                    WHERE table_schema = @SchemaName 
                    AND table_name = @TableName
                )
                THEN CAST(1 AS BIT)
                ELSE CAST(0 AS BIT)
            END";

            using (var command = context.Database.GetDbConnection().CreateCommand())
            {
                command.Parameters.Add(new SqlParameter("@SchemaName", SqlDbType.VarChar) { Value = schema });
                command.Parameters.Add(new SqlParameter("@TableName", SqlDbType.VarChar) { Value = "Organization" });

                context.Database.OpenConnection();
                bool exists = (bool)command.ExecuteScalar();
                context.Database.OpenConnection();
                return exists;
            }
        }
    }
}
