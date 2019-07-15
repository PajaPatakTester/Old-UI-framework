using System.Diagnostics;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace Framework
{
    public abstract class TestBase
    {

        public Stopwatch Stopwatch = new Stopwatch();
        public TelemetryClient TelemetryClient { get; set; }

        [SetUp]
        public void Init()
        {
            TelemetryClient = new TelemetryClient();

            TelemetryClient.Context?.Properties.Add("TestClass", TestContext.CurrentContext.Test.ClassName);
            TelemetryClient.Context?.Properties.Add("TestCase", TestContext.CurrentContext.Test.MethodName);

            Stopwatch.Restart();

            WebDriver.Initialize();

            TestInitialization();
        }

        [TearDown]

        public void Cleanup()
        {
            Stopwatch.Stop();

            TelemetryClient.TrackEvent(TestContext.CurrentContext.Result.Outcome.Status.ToString());

            var metric = new MetricTelemetry($"{TestContext.CurrentContext.Test.FullName}.{TestContext.CurrentContext.Test.Name}", Stopwatch.Elapsed.TotalSeconds);

            TelemetryClient.TrackMetric(metric);
            TelemetryClient?.Flush();

            if (!TestCompletedWithoutErrors())
            {
                TakeScreenshot();
                ErrorCleanup();
            }

            else
            {
                TestCleanup();
            }

            WebDriver.CleanUp();
        }

        public abstract void TestInitialization();
        protected virtual void TestCleanup() { }
        protected virtual void ErrorCleanup() { }

        public bool TestCompletedWithoutErrors()
        {
            return TestContext.CurrentContext.Result.Outcome.Equals(ResultState.Success) ||
                   TestContext.CurrentContext.Result.Outcome.Equals(ResultState.Inconclusive);
        }

        private void TakeScreenshot()
        {
            var screenshot = WebDriver.TakeScreenshot(TestContext.CurrentContext.Test.Name);
            if (screenshot != null) TestContext.AddTestAttachment(screenshot);
        }
    }
}
