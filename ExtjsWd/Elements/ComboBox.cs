using System.Linq.Expressions;
using NUnit.Framework;
using OpenQA.Selenium;

namespace ExtjsWd.Elements
{
    public abstract class ComboBox<T> : BaseContainerComponent where T : ComboBox<T>
    {
        protected readonly IWebElement Input;
        protected string CssClass;
        protected IWebElement Root;

        protected ComboBox(IWebDriver driver, By selector)
            : base(driver)
        {
            Input = Driver.FindElement(selector);
            WaitUntilComponentLoaded();
        }

        protected ComboBox(IWebDriver driver, string cssClass)
            : base(driver)
        {
            CssClass = cssClass;
            Root = driver.FindElement(By.CssSelector("." + cssClass));
            Input = Root.FindElement(By.TagName("input"));
        }

        protected ComboBox(IWebDriver driver, int timeoutInSeconds)
            : base(driver, timeoutInSeconds)
        {
            WaitUntilComponentLoaded();
        }

        public IWebElement ArrowDown
        {
            get {
                try
                {
                    return Root.FindElement(By.CssSelector("[role=\"button\"]"));
                }
                catch
                {
                    return Root.FindElement(By.CssSelector(".x-form-arrow-trigger"));
                }
                
             }
        }

        public virtual bool Enabled
        {
            get { return Input.Enabled; }
        }

        public bool IsDisplayed
        {
            get { return Root.Displayed; }
        }

        public bool Required
        {
            get { return Input.IsRequired(); }
        }

        public static void SelectFirstItem(IWebElement element)
        {
            element.Clear();
            if (ScenarioFixture.Instance.ExtVersion == 4)
            {
                element.SendKeys(Keys.ArrowDown);
                WebElementExtentions.WaitUntilAjaxLoadingDone(element);
            }
            element.SendKeys(Keys.ArrowDown);
            WebElementExtentions.WaitUntilAjaxLoadingDone(element);
            element.SendKeys(Keys.Return);
            element.SendKeys(Keys.Tab);
            WebElementExtentions.WaitUntilAjaxLoadingDone(element);
        }
        
        public static void SelectSecondItem(IWebElement element)
        {
            element.Clear();
            if (ScenarioFixture.Instance.ExtVersion == 4)
            {
                element.SendKeys(Keys.ArrowDown);
                WebElementExtentions.WaitUntilAjaxLoadingDone(element);
            }
            element.SendKeys(Keys.ArrowDown);
            WebElementExtentions.WaitUntilAjaxLoadingDone(element);
            element.SendKeys(Keys.ArrowDown);
            WebElementExtentions.WaitUntilAjaxLoadingDone(element);
            element.SendKeys(Keys.Return);
            element.SendKeys(Keys.Tab);
            WebElementExtentions.WaitUntilAjaxLoadingDone(element);
        }

        public void Clear()
        {
            Input.Clear();
            Input.SendKeys(Keys.Tab);
            this.WaitUntilAjaxLoadingDone();
        }

        public abstract T FillIn(string value);

        public virtual T SelectFirstItem()
        {
            SelectFirstItem(Input);
            return (T)this;
        }
        
        public virtual T SelectSecondItem()
        {
            SelectSecondItem(Input);
            return (T)this;
        }

        public void ShouldHaveNoErrorTip()
        {
            // WHY? Validation sometimes fails before FillIn() is called
            // this method is called in the constructor of BaseContainerComponent.
        }

        public T ShouldHaveNoValueFilledIn()
        {
            Assert.IsEmpty(Input.GetValue());
            return (T)this;
        }

        public T ShouldHaveSomeValueFilledIn()
        {
            Assert.IsNotEmpty(Input.GetValue());
            return (T)this;
        }

        public T ShouldHaveValueFilledIn(string item)
        {
            var val = Input.GetValue().Trim();
            Assert.IsTrue(val.StartsWith(item.Trim()), "Expected input value to contain '" + item + "' but was '" + val + "'");
            return (T)this;
        }

        public T ShouldNotSelectItem(string item)
        {
            FillIn(item);

            Assert.AreNotEqual(item.Trim(), Input.GetValue().Trim());

            return (T)this;
        }

        public T ShouldSelectItem(string item)
        {
            FillIn(item);

            Assert.IsTrue(Input.GetValue().Trim().Contains(item.Trim()));

            return (T)this;
        }

        public override void WaitUntilComponentLoaded()
        {
            WaitUntil(driver => Input.Displayed);
        }
    }
}
