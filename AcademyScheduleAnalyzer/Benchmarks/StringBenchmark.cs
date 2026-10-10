using System.Text;
using BenchmarkDotNet.Attributes;

namespace AcademyScheduleAnalyzer.Benchmarks
{
    [MemoryDiagnoser]
    public class StringBenchmark
    {
        [Params(100, 1000, 10000, 100000)]
        public int Iterations;

        private const string TextToAppend = "Session";

        [Benchmark]
        public string StringConcatenation()
        {
            string result = "";
            for (int i = 0; i < Iterations; i++)
            {
                result += TextToAppend;
            }
            return result;
        }

        [Benchmark]
        public string StringBuilderConcatenation()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < Iterations; i++)
            {
                sb.Append(TextToAppend);
            }
            return sb.ToString();
        }
    }
}
