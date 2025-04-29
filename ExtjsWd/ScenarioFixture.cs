using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;

namespace ExtjsWd
{
    public abstract class ScenarioFixture : IDisposable
    {
        public static TimeSpan DefaultTimeoutForElements = TimeSpan.FromSeconds(10.0);
        public static TimeSpan DefaultTimeoutForPages = TimeSpan.FromSeconds(120.0);

        protected ScenarioFixture() : this(new EmptyScenarioFixtureInitializer())
        {
        }

        protected ScenarioFixture(IScenarioFixtureInitializer initializer)
        {
            initializer.InitializeFixture(this);
            StartChromeDriver();
        }

        public static ScenarioFixture Instance { get; set; }

        public int AjaxRequestsBusy
        {
            get { return int.Parse(EvalJS("return window.ajaxRequests").ToString()); }
        }

        public IWebDriver Driver { get; private set; }

        public int ExtVersion
        {
            get { return int.Parse(EvalJS("return Ext.versions.extjs.getMajor();").ToString()); }
        }

        public static IWebDriver CreateChromeDriver(TimeSpan timeout)
        {
            return new ChromeDriver(ChromeDriverService.CreateDefaultService(), ChromeOptions(), timeout);
        }

        public static void ForceKillChromeDriver()
        {
            ForceKillExe("chromedriver.exe");
            ForceKillExe("chrome.exe");
        }

        public static void TearDownFixture()
        {
            Instance.CleanupChromeDriver();
        }

        public void CleanupChromeDriver()
        {
            try
            {
                if (!String.IsNullOrEmpty(Driver.CurrentWindowHandle))
                {
                    Driver.Close();
                    Driver.Quit();
                }
            }
            catch (WebDriverException alreadyForceKilledEx)
            {
            }
        }

        public void Dispose()
        {
            CleanupChromeDriver();
        }

        public object EvalJS(string js)
        {
            try
            {
                return ((IJavaScriptExecutor)Driver).ExecuteScript(js);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public object EvalJS(IWebElement webElement, string js)
        {
            try
            {
                return ((IJavaScriptExecutor)Driver).ExecuteScript(js, webElement);
            }
            catch
            {
                // OMNOMNOM.
            }
            return null;
        }

        public abstract string ResolveHostAndPort();

        public void StartChromeDriver()
        {
            Driver = CreateChromeDriver(DefaultTimeoutForPages);

            Driver.Manage().Timeouts().ImplicitWait = new TimeSpan(0, 0, 0, 0, 500);
            Driver.Navigate().GoToUrl(ResolveHostAndPort());
        }

        public WebDriverWait Wait(int seconds)
        {
            return new WebDriverWait(Driver, TimeSpan.FromSeconds(seconds));
        }

        private static ChromeOptions ChromeOptions()
        {
            var options = new ChromeOptions();
            options.AddArgument("start-maximized");
            options.AddArgument("headless");
            options.AddArgument("disable-popup-blocking");
            options.AddArgument("disable-translate");
            options.AddArgument("ignore-certificate-errors");
            options.AddArgument("no-sandbox");
            options.AddArgument("disable-search-engine-choice-screen");
            options.AddArgument("--window-size=1920,1080");
            return options;
        }

        private static void ForceKillExe(string executable)
        {
            var process = new System.Diagnostics.Process();
            var startInfo = new System.Diagnostics.ProcessStartInfo
            {
                WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden,
                FileName = "cmd.exe",
                Arguments = "/C taskkill /F /IM " +
                            executable
            };
            process.StartInfo = startInfo;
            process.Start();
            process.WaitForExit(5000);
        }
    }
}