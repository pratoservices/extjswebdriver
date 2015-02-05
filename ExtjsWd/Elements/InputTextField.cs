using OpenQA.Selenium;

namespace ExtjsWd.Elements
{
    public class InputTextField : BaseElement
    {
        public InputTextField(IWebElement element, IWebDriver driver)
            : base(element, driver)
        {
        }

        public InputTextField(IWebElement parent, By by, IWebDriver driver)
            : base(parent, by, driver)
        {
        }

        public InputTextField FillIn(string text)
        {
            Clear();
            SendKeys(text);
            SendKeys(Keys.Tab);

            return this;
        }
    }
}