using CsvHelper;
using CsvHelper.Configuration;
using ERPFastTrack.Abstraction.Interfaces;
using ERPFastTrack.Abstraction.Models.SourceData;
using System.Globalization;

namespace ERPFastTrack.SourceProcessor.Sources.File
{
    public class FileSource : IOperationsSource<FileSourceRequest>
    {
        public SourceDataDetails GetDetails(FileSourceRequest Req)
        {
            SourceDataDetails response = new();
            response.Fields = new();

            try
            {
                using (var reader = new StreamReader(Req.FilePath))
                using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    Delimiter = ",",
                    HasHeaderRecord = Req.HasHeader
                }))
                {
                    csv.Read();
                    if (Req.HasHeader)
                    {
                        csv.ReadHeader();

                        var headers = csv.HeaderRecord;

                        if (headers != null && headers.Length > 0)
                        {
                            for (int i = 0; i < headers.Length; i++)
                            {
                                SourceFieldDetail data = new()
                                {
                                    FieldId = (i + 1).ToString(),
                                    FieldName = headers[i]
                                };

                                response.Fields.Add(data);
                            }
                        }
                        else
                        {
                            Console.WriteLine("No headers found.");
                        }

                        Console.WriteLine("Sample values from first row with column numbers:");
                        csv.Read();
                    }

                    if (Req.HasHeader)
                    {
                        for (int i = 0; i < response.Fields.Count; i++)
                        {
                            response.Fields[i].SampleValue = csv.GetField(i);
                        }
                    }
                    else
                    {
                        for (int i = 0; i < csv.Parser.Record.Length; i++)
                        {
                            SourceFieldDetail data = new()
                            {
                                FieldId = (i + 1).ToString(),
                                FieldName = "Column " + (i + 1),
                                SampleValue = csv.GetField(i)
                            };

                            response.Fields.Add(data);
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
