using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using Weather.Core.Services;

namespace Weather.Benchmark
{
    [MemoryDiagnoser(false)]
    public class PerformanceChecking
    {
        [Benchmark]
        public async Task BenchmarkWithAsyncAwait()
        {
            var weatherService = new WeatherService();
            var result = await weatherService.GetWeatherOfCityAsyncWithAsync("london", default(CancellationToken));
        }
        
        [Benchmark]
        public async Task BenchmarkNoAsyncAwait()
        {
            var weatherService = new WeatherService();
            var result = await weatherService.GetWeatherOfCityAsyncNoAsync("london", default(CancellationToken));
        }
    }
}