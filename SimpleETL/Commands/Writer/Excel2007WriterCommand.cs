using System.Collections.Generic;
using System.Data;
using System.Linq;
using DataConnectors.Adapter.FileAdapter;

namespace SimpleETL.Commands
{
    public class Excel2007WriterCommand : DataCommand<DataTable>
    {
        public Excel2007NativeAdapter Adapter { get; } = new Excel2007NativeAdapter();

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