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

        public InputTextField FillInSlow(string text)
        {
            Clear();

            if (string.IsNullOrEmpty(text))
            {
                SendKeys("");
            }
            else
            {
                foreach (var character in text)
                {
                    this.WaitForSomeMiliTime(200);
                    SendKeys(character.ToString());
                }
            }

            SendKeys(Keys.Tab);

            return this;
        }

        public InputTextField PressEnter()
        {
            SendKeys(Keys.Enter);
            return this;
        }
    }
}