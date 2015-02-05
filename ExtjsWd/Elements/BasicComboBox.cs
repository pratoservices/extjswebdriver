using OpenQA.Selenium;

namespace ExtjsWd.Elements
{
    public class BasicComboBox : ComboBox<BasicComboBox>
    {
        public BasicComboBox(IWebDriver driver, By selector)
            : base(driver, selector)
        {
        }

        public BasicComboBox(IWebDriver driver, string cssClass)
            : base(driver, cssClass)
        {
        }

        public BasicComboBox(IWebDriver driver, int timeoutInSeconds)
            : base(driver, timeoutInSeconds)
        {
        }

        public override BasicComboBox FillIn(string value)
        {
            Input.SendKeys(value);
            return this;
        }
    }
}