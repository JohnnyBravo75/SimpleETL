using System.Collections.Generic;
using System.Data;
using System.Text;
using DataConnectors.Common.Extensions;

namespace SimpleETL
{
    public class UnionAllCommand : DataCommand<DataTable>
    {
        readonly List<DataCommand<DataTable>>  sourceCommands = new List<DataCommand<DataTable>>();

        public override IEnumerable<DataTable> Execute(IEnumerable<DataTable> input)
        {
            foreach (var sourceCommand in this.sourceCommands)
            {
                foreach (var table in sourceCommand.Execute(null))
                {
                    yield return table;
                }
            }
        }

        public UnionAllCommand Add(DataCommand<DataTable> command)
        {
            this.sourceCommands.Add(command);
            return this;
        }
    }
}