using System;
using System.Data;
using DataConnectors.Adapter.DbAdapter.ConnectionInfos;
using DataConnectors.Common.Model;
using DataConnectors.Converters;
using SimpleETL.Commands;

namespace SimpleETL
{
    public class Program
    {
        private static void Main(string[] args)
        {
            using (var pipe = new DataCommandPipeline<DataTable>())
            {
                var csvReader = new CsvReaderCommand();
                csvReader.Adapter.FileName = @"C:\Develop\c#\mixed\SimpleETL\SimpleETL.Test\TestData\Master Works of Art.csv";
                csvReader.Adapter.Separator = ",";
                csvReader.Adapter.Enclosure = "\"";
                csvReader.Adapter.TableName = "MasterWorksofArt";
                csvReader.Adapter.FieldDefinitions.Add(new FieldDefinition("Artist", "Artist", typeof(string)));
                csvReader.Adapter.FieldDefinitions.Add(new FieldDefinition("Title", "Title", typeof(string)));
                csvReader.Adapter.FieldDefinitions.Add(new FieldDefinition("Year (Approximate)", "Year_Approximate", typeof(float), new StringToNumberAutoConverter()));
                csvReader.Adapter.FieldDefinitions.Add(new FieldDefinition("Movement", "Movement", typeof(string)));
                csvReader.Adapter.FieldDefinitions.Add(new FieldDefinition("Total Height (cm)", "Total_Height_cm", typeof(float), new StringToNumberAutoConverter()));

                pipe.Commands.Add(csvReader);

                var dbWriter = new DbWriterCommand();
                var connInfo = new SqLiteConnectionInfo();
                connInfo.Database = @"C:\Develop\c#\mixed\SimpleETL\SimpleETL.Test\TestData\result.sqlite";
                dbWriter.Adapter.ConnectionInfo = connInfo;

                pipe.Commands.Add(dbWriter);

                DataCommandPipeline<DataTable>.Save(@"C:\Temp\pipe.xml", pipe);
                pipe.Execute();

                Console.WriteLine("Finished");
                Console.ReadLine();
            }
        }
    }
}