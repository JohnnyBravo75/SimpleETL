using System.Collections.Generic;
using System.Data;
using System.Linq;
using DataConnectors.Adapter.DbAdapter;

namespace SimpleETL.Commands
{
    public class DbWriterCommand : DataCommand<DataTable>
    {
        public DbAdapter Adapter { get; set; } = new DbAdapter();

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