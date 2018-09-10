using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SimpleETL.Test
{
    [TestClass]
    public class NumberTestPipe
    {
        [TestMethod]
        public void Execute()
        {
            var pipe1 = new DataCommandPipeline<string>();
            pipe1.Commands.Add(new TestReaderCommand());
            pipe1.Commands.Add(new TestWriterCommand());
            pipe1.Execute();

            Console.WriteLine("Finished");
            //Console.ReadLine();
        }
    }

    public class TestReaderCommand : DataCommand<string>
    {
        public override IEnumerable<string> Execute(IEnumerable<string> input)
        {
            Console.WriteLine("Start importing...");
            foreach (var str in this.Reader())
            {
                yield return str;
            }
            Console.WriteLine("End importing...");
        }

        private IEnumerable<string> Reader()
        {
            foreach (var n in Enumerable.Range(1, 5))
            {
                yield return n.ToString();
            }
        }
    }

    public class TestWriterCommand : DataCommand<string>
    {
        public override IEnumerable<string> Execute(IEnumerable<string> input)
        {
            Console.WriteLine("Start exporting...");
            this.Writer(input);
            Console.WriteLine("End exporting...");
            return Enumerable.Empty<string>();
        }

        private void Writer(IEnumerable<string> input)
        {
            foreach (var str in input)
            {
                Console.WriteLine(str);
            }
        }
    }
}