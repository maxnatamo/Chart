﻿using BenchmarkDotNet.Reports;
using BenchmarkDotNet.Running;

namespace Chart.Core.Parsers.Benchmarks
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            Summary summary = BenchmarkRunner.Run<SchemaParserBenchmark>();
        }
    }
}