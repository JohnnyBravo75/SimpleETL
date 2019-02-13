using System.Collections.Generic;
using System.Data;
using System.Xml.Serialization;
using DataConnectors.Adapter.FileAdapter;

namespace SimpleETL.Commands
{
    public class CsvReaderCommand : DataCommand<DataTable>
    {
        public CsvAdapter Adapter { get; set; } = new CsvAdapter();

        public override IEnumerable<DataTable> Execute(IEnumerable<DataTable> input)
        {
            foreach (var table in this.Adapter.ReadData(this.BlockSize))
            {
                yield return table;
            }
        }

        public override void Dispose()
        {
            this.Adapter?.Dispose();
        }
    }
}