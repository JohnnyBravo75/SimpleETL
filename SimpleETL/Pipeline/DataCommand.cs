using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using SimpleETL.Commands;

namespace SimpleETL
{
    [Serializable]
    [XmlInclude(typeof(AccessReaderCommand))]
    [XmlInclude(typeof(CsvReaderCommand))]
    [XmlInclude(typeof(DbReaderCommand))]
    [XmlInclude(typeof(ExcelReaderCommand))]
    [XmlInclude(typeof(Excel2007ReaderCommand))]
    [XmlInclude(typeof(FixedReaderCommand))]
    [XmlInclude(typeof(SqlReaderCommand))]
    [XmlInclude(typeof(XmlReaderCommand))]
    [XmlInclude(typeof(AccessReaderCommand))]
    [XmlInclude(typeof(CsvWriterCommand))]
    [XmlInclude(typeof(DbWriterCommand))]
    [XmlInclude(typeof(ExcelWriterCommand))]
    [XmlInclude(typeof(Excel2007WriterCommand))]
    [XmlInclude(typeof(FixedWriterCommand))]
    [XmlInclude(typeof(XmlWriterCommand))]
    public abstract class DataCommand<TData> : IDisposable
    {
        [XmlIgnore]
        public int BlockSize { get; set; } = 100;

        public abstract IEnumerable<TData> Execute(IEnumerable<TData> input);

        public virtual void Dispose()
        {
        }
    }
}