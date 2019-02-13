using System.Collections.Generic;
using System.Data;
using System.Linq;
using DataConnectors.Adapter.FileAdapter;

namespace SimpleETL.Commands
{
    public class FixedWriterCommand : DataCommand<DataTable>
    {
        public FixedTextAdapter Adapter { get; set; } = new FixedTextAdapter();

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