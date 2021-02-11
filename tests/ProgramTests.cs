using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WeatherService.Tests
{
    public class ProgramTests
    {
        private CancellationTokenSource _cancel;
        
        [SetUp]
        public void Setup()
        {
            _cancel = new CancellationTokenSource(1000);
            Program.CancellationToken = _cancel.Token;
        }

        [TearDown]
        public void TearDown()
        {
            _cancel.Dispose();
        }

        [Test]
        public async Task ShouldRun() =>
            await Program.Main();
    }
}