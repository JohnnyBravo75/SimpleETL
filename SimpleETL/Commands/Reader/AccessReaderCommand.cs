using System.Collections.Generic;
using System.Data;
using DataConnectors.Adapter.DbAdapter;

namespace SimpleETL.Commands
{
    public class AccessReaderCommand : DataCommand<DataTable>
    {
        public AccessAdapter Adapter { get; } = new AccessAdapter();

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