using System;
using System.Collections.Generic;

namespace SimpleETL
{
    public abstract class DataCommand<TData> : IDisposable
    {
        public int BlockSize { get; set; } = 100;

        public abstract IEnumerable<TData> Execute(IEnumerable<TData> input);

        public virtual void Dispose()
        {
        }
    }
}