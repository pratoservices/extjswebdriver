using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;

namespace ExtjsWd
{
    public static class DriverExtentions
    {
        public static DefaultWait<IWebDriver> GetWait(this IWebDriver driver, int seconds)
        {
            var wait = new DefaultWait<IWebDriver>(driver)
            {
                PollingInterval = TimeSpan.FromMilliseconds(100),
                Timeout = new TimeSpan(0, 0, seconds)
            };
            return wait;
        }

        public static void SendComboKey(this IWebDriver driver, string keyOne, string keyTwo)
        {
            new Actions(driver)
                 .KeyDown(keyOne)
                 .SendKeys(keyTwo)
                 .KeyUp(keyOne)
                 .Build()
                 .Perform();
        }
    }
}