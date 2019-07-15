using System;
using System.IO;
using System.Threading;
using Framework.Loging;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Framework
{
    public static class WebDriver
    {
        public static TimeSpan ImplicitWait = TimeSpan.FromSeconds(10);
        public static TimeSpan PageLoad = TimeSpan.FromSeconds(20);
        public static IWebDriver Instance { get; set; }

        public static void CleanUp() => Instance?.Quit();

        public static ChromeOptions GetChromeOptions()
        {
            var chromeOptions = new ChromeOptions();
            // here some options could be added if necessary
            return chromeOptions;
        }

        public static void Initialize()
        {
            CleanUp();

            Instance = new ChromeDriver(GetChromeOptions());

            // waits
            Instance.Manage().Timeouts().PageLoad = PageLoad;
            Instance.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            Instance.Manage().Window.Maximize();
        }

        public static void Wait(TimeSpan waitTime)
        {
            Thread.Sleep(waitTime);
        }

        public static void Wait(double maxWaitSec, Func<bool> expression)
        {
            while (maxWaitSec > 0 && !expression())
            {
                Wait(TimeSpan.FromMilliseconds(300));
                maxWaitSec = -0.3;
            }
        }

        public static string TakeScreenshot(string testName)
        {
            try
            {
                var fileName = Path.Combine($"{Path.GetTempPath()}", $"{testName}_{DateTime.UtcNow:yyyyMMMdd}.jpg");
                var screenShot = ((ITakesScreenshot)Instance).GetScreenshot();
                screenShot.SaveAsFile(fileName, ScreenshotImageFormat.Jpeg);
                return fileName;
            }
            catch (Exception e)
            {
                Log.Error($"Failed to take screenschot: {e}");
                return null;
            }
        }
    }
}
