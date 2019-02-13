using System.Collections.Generic;
using System.Data;
using DataConnectors.Adapter.FileAdapter;

namespace SimpleETL.Commands
{
    public class ExcelReaderCommand : DataCommand<DataTable>
    {
        public ExcelNativeAdapter Adapter { get; set; } = new ExcelNativeAdapter();

        public override IEnumerable<DataTable> Execute(IEnumerable<DataTable> input)
        {
            this.Adapter.Connect();

            foreach (var table in this.Adapter.ReadData(this.BlockSize))
            {
                yield return table;
            }

            this.Adapter.Disconnect();
        }

        public override void Dispose()
        {
            this.Adapter?.Dispose();
        }
    }
}