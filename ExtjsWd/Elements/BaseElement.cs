using ExtjsWd.js;
using OpenQA.Selenium;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Interactions.Internal;
using System.Collections.ObjectModel;
using System.Drawing;

namespace ExtjsWd.Elements
{
    public class BaseElement : IWebElement, ILocatable
    {
        protected IWebDriver Driver;
        protected IWebElement Element;

        public BaseElement(IWebElement element, IWebDriver driver)
        {
            Element = element;
            Driver = driver;
        }

        public BaseElement(IWebElement parent, By by, IWebDriver driver)
        {
            Element = parent.FindElement(by);
            Driver = driver;
        }

        public ICoordinates Coordinates
        {
            get { return ((ILocatable)Element).Coordinates; }
        }

        public bool Displayed
        {
            get { return Element.Displayed; }
        }

        public bool Enabled
        {
            get { return Element.Enabled; }
        }

        public bool HasFocus
        {
            get { return Driver.SwitchTo().ActiveElement().Equals(Element); }
        }

        public Point Location
        {
            get { return Element.Location; }
        }

        public Point LocationOnScreenOnceScrolledIntoView
        {
            get { return ((ILocatable)Element).LocationOnScreenOnceScrolledIntoView; }
        }

        public bool Selected
        {
            get { return Element.Selected; }
        }

        public Size Size
        {
            get { return Element.Size; }
        }

        public string TagName
        {
            get { return Element.TagName; }
        }

        public string Text
        {
            get { return Element.Text; }
        }

        public string Value
        {
            get { return JSCommands.GetPropertyOfWebElement(this, "value"); }
        }

        public void Clear()
        {
            Element.Clear();
        }

        public virtual void Click()
        {
            // IE problem fix: see http://stackoverflow.com/questions/8718927/selenium-webdriver-click-fails-with-ie9
            var driver = Driver as InternetExplorerDriver;
            if (driver != null)
            {
                driver.ExecuteScript("arguments[0].click();", Element);
            }
            Element.Click();
        }

        public IWebElement FindElement(By @by)
        {
            return Element.FindElement(by);
        }

        public ReadOnlyCollection<IWebElement> FindElements(By @by)
        {
            return Element.FindElements(by);
        }

        public string GetAttribute(string attributeName)
        {
            return Element.GetAttribute(attributeName);
        }

        public string GetCssValue(string propertyName)
        {
            return Element.GetCssValue(propertyName);
        }

        public string GetDomAttribute(string attributeName)
        {
            return Element.GetDomAttribute(attributeName);
        }

        public string GetDomProperty(string propertyName)
        {
            return Element.GetDomProperty(propertyName);
        }

        public string GetProperty(string propertyName)
        {
            return propertyName;
        }

        public ISearchContext GetShadowRoot()
        {
            return Element.GetShadowRoot();
        }

        public void SendKeys(string text)
        {
            Element.SendKeys(text);
        }

        public void Submit()
        {
            Element.Submit();
        }
    }
}