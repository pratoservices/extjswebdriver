using OpenQA.Selenium;
using System;

namespace ExtjsWd
{
    public static class WebElementFormFieldExtentions
    {
        public static WebElementFormFieldType GetExtJsFormFieldType(this IWebElement webElement)
        {
            if (webElement.IsCheckedBox())
            {
                return WebElementFormFieldType.CheckBox;
            }
            if (webElement.IsRadioButton())
            {
                return WebElementFormFieldType.RadioButton;
            }
            if (webElement.IsCodeCombobox())
            {
                return WebElementFormFieldType.CodeCombobox;
            }
            if (webElement.IsTextArea())
            {
                return WebElementFormFieldType.TextArea;
            }
            if (webElement.IsDateField())
            {
                return WebElementFormFieldType.DateField;
            }
            if (webElement.IsTimeField())
            {
                return WebElementFormFieldType.TimeField;
            }

            throw new Exception("No Type defined yet for webElement id: " + webElement.GetAttribute("id") + "  - class: " + webElement.GetAttribute("class"));
        }

        public static bool IsCheckedBox(this IWebElement webElement)
        {
            return webElement.GetAttribute("class")
                .Contains("x-form-type-checkbox");
        }

        public static bool IsCodeCombobox(this IWebElement webElement)
        {
            return webElement.GetAttribute("id")
                .Contains("codecombo");
        }

        public static bool IsDateField(this IWebElement webElement)
        {
            return webElement.GetAttribute("id")
                .Contains("datefield");
        }

        public static bool IsRadioButton(this IWebElement webElement)
        {
            return webElement.GetAttribute("class")
                .Contains("x-form-radio");
        }

        public static bool IsTextArea(this IWebElement webElement)
        {
            return webElement.GetAttribute("class")
                .Contains("x-form-textarea");
        }

        public static bool IsTimeField(this IWebElement webElement)
        {
            return webElement.GetAttribute("id")
                .Contains("timefield");
        }
    }
}