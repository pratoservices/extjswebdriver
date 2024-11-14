using OpenQA.Selenium.Remote;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace ExtjsWd.Exceptions
{
    public class WebDriverExceptionHandler
    {
        public static string Handle(Exception ex, IExceptionLogInfoResolver extraLogInfoResolver = null)
        {
            Debug.WriteLine(ex.Message);
            Debug.WriteLine(ex.InnerException);
            Debug.WriteLine(ex.StackTrace);
            var fileName = FileName();

            File.WriteAllText(fileName + ".txt",
                "-- Resolved URL: " + ScenarioFixture.Instance.ResolveHostAndPort() + Environment.NewLine +
                "-- Actual URL: " + ScenarioFixture.Instance.Driver.Url + Environment.NewLine +
                "-- Exception Message: " + ex.Message + Environment.NewLine +
                "-- Stacktrace: " + Environment.NewLine + ex.StackTrace + Environment.NewLine + Environment.NewLine +
                "-- Service log: " + Environment.NewLine + (extraLogInfoResolver ?? new NoLogInfoResolver()).ReadLog());

            RemoteWebDriver().GetScreenshot().SaveAsFile(fileName + ".png");

            PrintExceptions();

            return fileName;
        }

        public static void PrintExceptions()
        {
            var exceptions = RemoteWebDriver().ExecuteScript("return window.errors") as System.Collections.ObjectModel.ReadOnlyCollection<object>[];

            if (exceptions != null)
            {
                exceptions.ToList().ForEach(exception => Debug.WriteLine(exception.ToString()));
            }

            RemoteWebDriver().ExecuteScript("window.errors = [];");
        }

        public static void Wrap(Action action, IExceptionLogInfoResolver exceptionLogInfoResolver = null)
        {
            try
            {
                action.Invoke();
            }
            catch (Exception ex)
            {
                Handle(ex, exceptionLogInfoResolver);
            }
        }

        private static string FileName()
        {
            return "ex-" + DateTime.Now.ToString("yy-MM-dd-HH-mm-ss");
        }

        private static RemoteWebDriver RemoteWebDriver()
        {
            return (RemoteWebDriver)ScenarioFixture.Instance.Driver;
        }
    }
}