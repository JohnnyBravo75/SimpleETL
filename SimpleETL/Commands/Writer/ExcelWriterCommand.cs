using System.Collections.Generic;
using System.Data;
using System.Linq;
using DataConnectors.Adapter.FileAdapter;

namespace SimpleETL.Commands
{
    public class ExcelWriterCommand : DataCommand<DataTable>
    {
        public ExcelNativeAdapter Adapter { get; set; } = new ExcelNativeAdapter();

        public override IEnumerable<DataTable> Execute(IEnumerable<DataTable> input)
        {
            this.Adapter.Connect();
            this.Adapter.WriteData(input);
            this.Adapter.Disconnect();
            return Enumerable.Empty<DataTable>();
        }

        public override void Dispose()
        {
            this.Adapter?.Dispose();
        }
    }
}