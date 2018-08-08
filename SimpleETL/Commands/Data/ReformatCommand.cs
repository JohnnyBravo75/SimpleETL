using System.Collections.Generic;
using System.Data;

namespace SimpleETL
{
    public class ReformatCommand : DataCommand<DataTable>
    {
        public List<ColumnMapping> ColumnMappings { get; set; } = new List<ColumnMapping>();

        public string TableName { get; set; }

        public override IEnumerable<DataTable> Execute(IEnumerable<DataTable> input)
        {
            foreach (var table in input)
            {
                yield return this.ReFormatTable(table);
            }
        }

        private DataTable ReFormatTable(DataTable table)
        {
            // Remove columns not in the mapping list
            for (int i = table.Columns.Count - 1; i >= 0; i--)
            {
                string columnName = table.Columns[i].ColumnName;

                if (!this.ColumnMappings.Exists(x => x.SourceColumn == columnName))
                {
                    table.Columns.RemoveAt(i);
                }
            }

            // Reorder
            foreach (var columnMapping in this.ColumnMappings)
            {
                if (!string.IsNullOrEmpty(columnMapping.SourceColumn))
                {
                    if (table.Columns.Contains(columnMapping.SourceColumn))
                    {
                        table.Columns[columnMapping.SourceColumn].SetOrdinal(this.ColumnMappings.IndexOf(columnMapping));
                    }
                }
            }

            // Rename
            foreach (var columnMapping in this.ColumnMappings)
            {
                if (!string.IsNullOrEmpty(columnMapping.SourceColumn))
                {
                    if (table.Columns.Contains(columnMapping.SourceColumn))
                    {
                        table.Columns[columnMapping.SourceColumn].ColumnName = columnMapping.TargetColumn;
                    }
                }
            }

            table.AcceptChanges();

            return table;
        }
    }

    public class ColumnMapping
    {
        private string targetColumn;

        public string TargetColumn
        {
            get
            {
                if (string.IsNullOrEmpty(this.targetColumn))
                {
                    return this.SourceColumn;
                }
                return this.targetColumn;
            }
            set { this.targetColumn = value; }
        }

        public string SourceColumn { get; set; }
    }
}