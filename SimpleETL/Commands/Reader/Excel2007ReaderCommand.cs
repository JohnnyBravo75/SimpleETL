using System.Collections.Generic;
using System.Data;
using DataConnectors.Adapter.FileAdapter;

namespace SimpleETL.Commands
{
    public class Excel2007ReaderCommand : DataCommand<DataTable>
    {
        public Excel2007NativeAdapter Adapter { get; } = new Excel2007NativeAdapter();

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