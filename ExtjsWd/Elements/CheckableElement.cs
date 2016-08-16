using OpenQA.Selenium;

namespace ExtjsWd.Elements
{
    public abstract class CheckableElement : BaseElement
    {
        public CheckableElement(IWebElement element, IWebDriver driver)
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
                    TheClickableElement.Click();
                }
            }
        }

        protected abstract IWebElement TheClickableElement { get; }

        public override void Click()
        {
            TheClickableElement.Click();
        }

        public bool IsChecked()
        {
            return Element.HasClass("x-form-cb-checked");
        }
    }
}