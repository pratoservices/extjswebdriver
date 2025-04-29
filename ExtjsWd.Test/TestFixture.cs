using System.IO;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;

namespace ExtjsWd.Test
{
    public class TestFixture : ScenarioFixture
    {
        public TestFixture()
        {
            Instance = this;
        }

        public override string ResolveHostAndPort()
        {
            return Path.Combine(TestContext.CurrentContext.TestDirectory,  "ExtSandBox/test4.html");
        }

        public override ChromeOptions ChromeOptions()
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
    }
}