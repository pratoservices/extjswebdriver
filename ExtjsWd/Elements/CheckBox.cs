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
                    TheClickableCheckbox.Click();
                }
            }
        }

        private IWebElement TheClickableCheckbox
        {
            get { return Element.FindElement(By.CssSelector(".x-form-checkbox")); }
        }

        public override void Click()
        {
            TheClickableCheckbox.Click();
        }

        public bool IsChecked()
        {
            return Element.HasClass("x-form-cb-checked");
        }
    }
}