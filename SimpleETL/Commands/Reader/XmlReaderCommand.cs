using System.Collections.Generic;
using System.Data;
using DataConnectors.Adapter.FileAdapter;

namespace SimpleETL.Commands
{
    public class XmlReaderCommand : DataCommand<DataTable>
    {
        public XmlAdapter Adapter { get; } = new XmlAdapter();

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