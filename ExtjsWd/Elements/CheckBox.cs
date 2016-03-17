using OpenQA.Selenium;

namespace ExtjsWd.Elements
{
    public class CheckBox : BaseElement
    {
        public CheckBox(IWebElement element, IWebDriver driver)
            : base(element, driver)
        {
            //The element is the table parent of the input, extjs does not use checkboxes but tables with layout
        }

        public bool Checked
        {
            get { return IsChecked(); }
            set
            {
                if (value != IsChecked())
                {
                    Element.FindElement(By.CssSelector("input")).Click();
                }
            }
        }

        public bool IsChecked()
        {
            return Element.HasClass("x-form-cb-checked");
        }
    }
}