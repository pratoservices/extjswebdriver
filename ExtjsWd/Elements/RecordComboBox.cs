using OpenQA.Selenium;

namespace ExtjsWd.Elements
{
    public class RecordComboBox : ComboBox<RecordComboBox>
    {
        public RecordComboBox(IWebDriver driver, string cssClass)
            : base(driver, cssClass)
        {
        }

        public override RecordComboBox FillIn(string value)
        {
            Input.Clear();
            Input.SendKeys(value);
            Input.SendKeys(Keys.Tab);

            return this;
        }
    }
}