using System.Collections.Generic;
using System.Data;
using DataConnectors.Common.Extensions;

namespace SimpleETL
{
    public class FilterCommand : DataCommand<DataTable>
    {
        public Conditions FilterConditions { get; set; } = new Conditions();

        public override IEnumerable<DataTable> Execute(IEnumerable<DataTable> input)
        {
            foreach (var table in input)
            {
                yield return this.FilterTable(table);
            }
        }

        private DataTable FilterTable(DataTable srcTable)
        {
            if (srcTable != null)
            {
                int filtered = 0;

                for (int i = srcTable.Rows.Count - 1; i >= 0; i--)
                {
                    DataRow row = srcTable.Rows[i];

                    if (this.FilterConditions != null && this.FilterConditions.Count > 0 &&
                        !ConditionEvaluator.CheckMatchingConditions(this.FilterConditions, row.ToDictionary()))
                    {
                        srcTable.Rows.Remove(row);
                        filtered++;
                    }
                }

                srcTable.AcceptChanges();
            }

            return srcTable;
        }
    }
}