using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERPFastTrack.DBGround.Context;

namespace ERPFastTrack.DBGround.CustomDBQueries
{
    public static class DbService
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

        public static List<T> RawSqlQuery<T>(string query, Func<DbDataReader, T> map, ERPFastTrackUIContext context)
        {
            List<T> entities = null;
            using (var command = context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = query;
                command.CommandType = CommandType.Text;

                context.Database.OpenConnection();

                using (var result = command.ExecuteReader())
                {
                    entities = new List<T>();

                    while (result.Read())
                    {
                        entities.Add(map(result));
                    }
                    result.Close();
                }

                context.Database.CloseConnection();
            }

            return entities;
        }
    }
}
