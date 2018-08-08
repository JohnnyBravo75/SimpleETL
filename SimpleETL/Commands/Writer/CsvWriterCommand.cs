using System.Collections.Generic;
using System.Data;
using System.Linq;
using DataConnectors.Adapter.FileAdapter;

namespace SimpleETL.Commands
{
    public class CsvWriterCommand : DataCommand<DataTable>
    {
        public CsvAdapter Adapter { get; } = new CsvAdapter();

        public override IEnumerable<DataTable> Execute(IEnumerable<DataTable> input)
        {
            this.Adapter.WriteData(input);
            return Enumerable.Empty<DataTable>();
        }

        public override void Dispose()
        {
            this.Adapter?.Dispose();
        }
    }
}