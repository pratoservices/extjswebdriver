using NUnit.Framework;
using OpenQA.Selenium;

namespace ExtjsWd.Test
{
    public abstract class WebDriverTest
    {
        private TestFixture _TestFixture;

        public IWebDriver Driver
        {
            get
            {
                return TestFixture.Driver;
            }
        }

        public TestFixture TestFixture
        {
            get
            {
                if (_TestFixture == null)
                {
                    _TestFixture = new TestFixture();
                }
                return _TestFixture;
            }
        }

        [TearDown]
        public void ClearChromeDriver()
        {
            if (_TestFixture != null)
            {
                _TestFixture.CleanupChromeDriver();
                _TestFixture = null;
            }
        }
    }
}