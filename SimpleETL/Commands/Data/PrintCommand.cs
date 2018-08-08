using System.Collections.Generic;
using System.Data;
using DataConnectors.Common.Extensions;

namespace SimpleETL
{
    public class PrintCommand : DataCommand<DataTable>
    {
        public override IEnumerable<DataTable> Execute(IEnumerable<DataTable> input)
        {
            foreach (var table in input)
            {
                table.PrintToConsole(25);
                yield return table;
            }
        }
    }
}