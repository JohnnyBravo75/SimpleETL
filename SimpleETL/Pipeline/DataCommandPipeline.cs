using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DataConnectors.Common.Extensions;
using DataConnectors.Common.Helper;

namespace SimpleETL
{
    public class DataCommandPipeline<TData> : IDisposable
    {
        public IList<DataCommand<TData>> Commands { get; set; } = new List<DataCommand<TData>>();

        public IEnumerable<TData> Execute(IEnumerable<TData> input = null)
        {
            foreach (var command in this.Commands)
            {
                input = command.Execute(input);
            }

            if (input == null)
            {
                return Enumerable.Empty<TData>();
            }

            // materialize (execute the lazy evaluation)
            return input.ToList();
        }

        public void Dispose()
        {
            foreach (var cmd in this.Commands)
            {
                cmd.Dispose();
            }
        }

        public static void Save(string fileName, DataCommandPipeline<TData> pipeline)
        {
            var serializer = new XmlSerializerHelper<DataCommandPipeline<TData>>();
            serializer.Save(fileName, pipeline);
        }

        public static DataCommandPipeline<TData> Load(string fileName)
        {
            var serializer = new XmlSerializerHelper<DataCommandPipeline<TData>>();
            var pipeline = serializer.Load(fileName);

            return pipeline;
        }
    }
}