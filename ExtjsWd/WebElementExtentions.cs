using ExtjsWd.Elements;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Linq;
using ExtjsWd.js;

namespace ExtjsWd
{
    public static class WebElementExtentions
    {
        public static bool Checked(this IWebElement webElement)
        {
            return webElement.GetAttribute("class")
                .Contains("x-form-cb-checked");
        }

        public static IWebElement DoubleClick(this IWebElement webElement)
        {
            new Actions(ScenarioFixture.Instance.Driver)
                .DoubleClick(webElement)
                .Build()
                .Perform();
            return webElement;
        }

        public static void FillInRandomValue(this IWebElement webElement)
        {
            switch (webElement.GetExtJsFormFieldType())
            {
                case WebElementFormFieldType.CheckBox:
                    webElement.Click();
                    break;

                case WebElementFormFieldType.TextArea:
                    webElement.Clear();
                    webElement.SendKeys("This is a textarea");
                    break;

                case WebElementFormFieldType.CodeCombobox:
                    BasicComboBox.SelectFirstItem(webElement);
                    break;

                case WebElementFormFieldType.RadioButton:
                    webElement.Click();
                    break;

                case WebElementFormFieldType.DateField:
                    webElement.Clear();
                    webElement.SendKeys("27/10/2014");
                    break;

                case WebElementFormFieldType.TimeField:
                    webElement.Clear();
                    webElement.SendKeys("10:00");
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public static DateTime GetDateValue(string dateValue)
        {
            return DateTime.Parse(dateValue.Substring(dateValue.Length - 10, 10));
        }

        public static DateTime GetDateValueFromDisplayField(this IWebElement webElement)
        {
            string dateString = webElement.HTMLContent();
            return GetDateValue(dateString);
        }

        public static DateTime GetDateValueFromTextField(this IWebElement webElement)
        {
            string dateString = webElement.GetAttribute("value");
            return GetDateValue(dateString);
        }

        /// <summary>
        /// Gets the value attribute from the webelement
        /// </summary>
        /// <param name="webElement">The web element.</param>
        /// <returns></returns>
        public static string GetValue(this IWebElement webElement)
        {
            return webElement.GetAttribute("value");
        }

        public static bool HasClass(this IWebElement webElement, string className)
        {
            return webElement.GetAttribute("class")
                .Contains(className);
        }

        public static bool HasHtmlContent(this IWebElement webElement)
        {
            var contents = webElement.GetAttribute("innerHTML");
            return contents != null && contents.Trim().Length > 0;
        }

        public static string HTMLContent(this IWebElement webElement)
        {
            return webElement.GetAttribute("innerHTML");
        }

        public static bool IsEnabled(this IWebElement webElement)
        {
            var classAttribute = webElement.GetAttribute("class");
            var readOnlyAttribute = webElement.GetAttribute("readonly");
            var disabledAttribute = webElement.GetAttribute("disabled");
            return !classAttribute
                .Contains("x-item-disabled")
                && readOnlyAttribute == null
                && disabledAttribute == null;
        }

        public static T ClickUsingJavascript<T>(this T webElement) where T : IWebElement
        {
            JSCommands.ClickUsingJavascript(webElement.Location.X, webElement.Location.Y);
            return webElement;
        }

        public static T ClickUsingAction<T>(this T webElement) where T : IWebElement
        {
            new Actions(ScenarioFixture.Instance.Driver)
                   .MoveToElement(webElement)
                   .Click()
                   .Build()
                   .Perform();
            return webElement;
         
        }

        public static bool IsRequired(this IWebElement webElement)
        {
            var inputOfElement = webElement;
            if (webElement.TagName != "input")
            {
                inputOfElement = webElement.FindElement(By.XPath(".//input"));
            }
            return webElement.GetAttribute("class")
                .Split(' ')
                .ToList()
                .Contains("x-form-required-field") ||
                inputOfElement.GetAttribute("class")
                .Split(' ')
                .ToList()
                .Contains("x-form-required-field");
        }

        public static IWebElement MoveTo(this IWebElement webElement)
        {
            new Actions(ScenarioFixture.Instance.Driver)
                 .MoveToElement(webElement)
                 .Build()
                 .Perform();
            return webElement;
        }

        public static T SetFieldValue<T>(this T webElement, string fieldName, string text) where T : IWebElement
        {
            var field = webElement.FindElement(By.Name(fieldName));
            field.Clear();
            field.SendKeys(text);
            field.SendKeys(Keys.Tab);
            return webElement;
        }

        public static IWebElement Type(this IWebElement webElement, string text)
        {
            webElement.Clear();
            webElement.SendKeys(text);
            webElement.SendKeys(Keys.Tab);
            return webElement;
        }

        public static void UntilNotDisplayed(this DefaultWait<IWebDriver> webDriverWait, By selector)
        {
            webDriverWait.Until(driver =>
            {
                var elements = driver.FindElements(selector);
                if (!elements.Any())
                    return true;
                return elements.All(NotDisplayedOrAlreadyGoneFromDOMTree);
            });
        }

        public static WebDriverWait Wait(this IWebDriver driver, int seconds)
        {
            return new WebDriverWait(driver, TimeSpan.FromSeconds(seconds));
        }

        public static WebDriverWait Wait(this IWebElement element, int timeout)
        {
            return ScenarioFixture.Instance.Wait(timeout);
        }

        public static IWebElement WaitUntilAjaxLoadingDone(this IWebElement callee)
        {
            callee.Wait(30).Until(x => ScenarioFixture.Instance.AjaxRequestsBusy == 0);
            return callee;
        }

        private static bool NotDisplayedOrAlreadyGoneFromDOMTree(IWebElement el)
        {
            try
            {
                return !el.Displayed;
            }
            catch (StaleElementReferenceException notExistingAnymoreEx)
            {
                return true;
            }
        }
    }
}