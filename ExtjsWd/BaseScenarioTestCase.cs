using ExtjsWd.Exceptions;
using NUnit.Framework;
using OpenQA.Selenium;

namespace ExtjsWd
{
    public abstract class BaseScenarioTestCase
    {
        protected IWebDriver Driver
        {
            get { return ScenarioFixture.Instance.Driver; }
        }

        [TearDown]
        public void PrintExceptionsIfAny()
        {
            WebDriverExceptionHandler.PrintExceptions();
        }
    }
}