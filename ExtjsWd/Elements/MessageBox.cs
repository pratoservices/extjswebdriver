using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;

namespace ExtjsWd.Elements
{
    public class MessageBox : BaseContainerComponent
    {
        private readonly bool _checkMessageInsteadOfTitle;
        private readonly IWebElement _messageBoxRoot;

        public MessageBox(IWebDriver driver, string messageText, bool checkMessageInsteadOfTitle = false)
            : base(driver)
        {
            _checkMessageInsteadOfTitle = checkMessageInsteadOfTitle;
            MessageText = messageText;
            WaitUntil(20, x => FindMessageBoxes(driver).Any());
            _messageBoxRoot = FindMessageBoxes(driver).Last();
            WaitUntilComponentLoaded();
        }

        public string MessageText { get; private set; }

        private static By CssButton
        {
            get { return By.CssSelector(".x-btn-button"); }
        }

        private static By CssInputField
        {
            get { return By.CssSelector(".x-form-text"); }
        }

        private IEnumerable<IWebElement> Buttons
        {
            get { return _messageBoxRoot.FindElements(CssButton); }
        }

        private IEnumerable<IWebElement> InputFields
        {
            get { return _messageBoxRoot.FindElements(CssInputField); }
        }

        public static bool IsOpen(IWebDriver driver)
        {
            return FindMessageBoxes(driver).Any(box => box.Displayed);
        }

        public void Accept(string text = "Ja")
        {
            Buttons.Single(btn => btn.Text.Equals(text)).Click();
            this.WaitUntilAjaxLoadingDone();
        }

        public void DontSave(string text = "Niet opslaan")
        {
            Buttons.Single(btn => btn.Text.Equals(text)).Click();
            this.WaitUntilAjaxLoadingDone();
        }

        public void Ok(string text = "OK")
        {
            Buttons.Single(btn => btn.Text.Equals(text)).Click();
            this.WaitUntilAjaxLoadingDone();
        }

        public MessageBox Prompt(string text = "")
        {
            InputFields.First(field => field.Enabled).SendKeys(text);
            return this;
        }

        public void Reject(string text = "Nee")
        {
            Buttons.Single(btn => btn.Text.Equals(text)).Click();
            this.WaitUntilAjaxLoadingDone();
        }

        public void Save(string text = "Opslaan")
        {
            Buttons.Single(btn => btn.Text.Equals(text)).Click();
            this.WaitUntilAjaxLoadingDone();
        }

        public void ShouldHaveNoErrorTip()
        {
        }

        public override void WaitUntilComponentLoaded()
        {
            if (_checkMessageInsteadOfTitle)
            {
                WaitUntil(x => GetMessageBoxRoot().HTMLContent().Contains(MessageText));
            }
            else
            {
                WaitUntil(x => GetMessageBoxRoot().Text.Contains(MessageText));
            }
        }

        private static IEnumerable<IWebElement> FindMessageBoxes(IWebDriver driver)
        {
            return driver.FindElements(By.CssSelector(".x-message-box"));
        }

        private IWebElement GetMessageBoxRoot()
        {
            if (_checkMessageInsteadOfTitle)
            {
                return _messageBoxRoot;
            }

            return _messageBoxRoot.FindElement(By.CssSelector(".x-header"));
        }
    }
}