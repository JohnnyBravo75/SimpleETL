using System.Collections.Generic;
using System.Data;
using System.Text;
using DataConnectors.Common.Extensions;

namespace SimpleETL
{
    public class DistinctCommand : DataCommand<DataTable>
    {
 
        public override IEnumerable<DataTable> Execute(IEnumerable<DataTable> input)
        {
            foreach (var table in input)
            {
                yield return this.DistinctTable(table);
            }
        }

        Dictionary<string, object> visitedData = new Dictionary<string, object>();

        private DataTable DistinctTable(DataTable srcTable)
        {
            if (srcTable != null)
            {

                for (int i = srcTable.Rows.Count - 1; i >= 0; i--)
                {
                    DataRow row = srcTable.Rows[i];

                    string key = this.CreateKey(row);
                    if (this.visitedData.ContainsKey(key))
                    {
                        srcTable.Rows.Remove(row);
                    }
                    else
                    {
                        this.visitedData.Add(key, null);
                    }
                }

                srcTable.AcceptChanges();
            }

            return srcTable;
        }

        private string CreateKey(DataRow row)
        {
            var key = new StringBuilder();
            int i = 0;
            foreach (DataColumn column in row.Table.Columns)
            {
                key.Append(row[i].ToStringOrEmpty());
                i++;
            }

            return key.ToString();
        }
    }
}