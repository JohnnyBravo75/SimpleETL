using System.Collections.Generic;
using System.Data;
using DataConnectors.Adapter.DbAdapter;

namespace SimpleETL.Commands
{
    public class SqlReaderCommand : DataCommand<DataTable>
    {
        public DbAdapter Adapter { get; } = new DbAdapter();

        public string SqlTemplate { get; set; }

        public override IEnumerable<DataTable> Execute(IEnumerable<DataTable> input)
        {
            this.Adapter.Connect();
            return this.Adapter.ExecuteSql(this.SqlTemplate);
        }

        public override void Dispose()
        {
            this.Adapter?.Dispose();
        }
    }
}