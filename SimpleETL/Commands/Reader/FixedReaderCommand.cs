using System.Collections.Generic;
using System.Data;
using DataConnectors.Adapter.FileAdapter;

namespace SimpleETL.Commands
{
    public class FixedReaderCommand : DataCommand<DataTable>
    {
        public FixedTextAdapter Adapter { get; set; } = new FixedTextAdapter();

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