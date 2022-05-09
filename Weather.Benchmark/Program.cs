using System;
using BenchmarkDotNet.Running;

namespace Weather.Benchmark
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<PerformanceChecking>();
        }
    }
}