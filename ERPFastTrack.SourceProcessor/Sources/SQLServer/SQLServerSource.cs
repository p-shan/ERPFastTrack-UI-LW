using ERPFastTrack.Abstraction.Interfaces;
using ERPFastTrack.Abstraction.Models.SourceData;
using System.Data;
using System.Data.SqlClient;

namespace ERPFastTrack.SourceProcessor.Sources.SQLServer
{
    public class SQLServerSource : IOperationsSource<SQLServerSourceRequest>
    {
        public SourceDataDetails GetDetails(SQLServerSourceRequest request)
        {
            SourceDataDetails response = new();
            response.Fields = new();
            try
            {
                using (SqlConnection connection = new SqlConnection(request.ConnStr))
                {
                    SqlCommand command = new SqlCommand(request.Query, connection);
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.SchemaOnly))
                    {
                        DataTable schemaTable = reader.GetSchemaTable();

                        foreach (DataRow row in schemaTable.Rows)
                        {
                            string columnName = row["ColumnName"].ToString();
                            Type columnType = (Type)row["DataType"];

                            SourceFieldDetail data = new();
                            data.FieldId = columnName;
                            data.FieldName = columnName;
                            data.FieldType = columnType.ToString();

                            response.Fields.Add(data);
                        }

                        // Close the DataReader before proceeding to the next command
                        reader.Close();

                        // Fetch sample data separately
                        foreach (var data in response.Fields)
                        {
                            // Construct parameterized query to fetch sample data for this column
                            string sampleDataQuery = $"SELECT TOP 1 [{data.FieldName}] FROM ( {request.Query} ) AS SampleDataQuery";
                            using (SqlCommand sampleDataCommand = new SqlCommand(sampleDataQuery, connection))
                            {
                                using (SqlDataReader sampleDataReader = sampleDataCommand.ExecuteReader())
                                {
                                    if (sampleDataReader.Read())
                                    {
                                        data.SampleValue = sampleDataReader[0].ToString();
                                    }
                                }
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return response;
        }
    }
}
