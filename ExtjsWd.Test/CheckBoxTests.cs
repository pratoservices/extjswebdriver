using ExtjsWd.Elements;
using ExtjsWd.Test.ExtSandBox;
using NUnit.Framework;
using OpenQA.Selenium;

namespace ExtjsWd.Test
{
    [TestFixture]
    public class CheckBoxTests : WebDriverTest
    {
        private string _CheckboxName = "TestCheckBox";

        [Test]
        public void CheckBox_Can_Set_Checked()
        {
            CreateCheckbox(false);
            var element = Driver.FindElement(By.CssSelector(".wd-test"));
            var checkBox = new CheckBox(element, Driver);
            var checkBoxDomTable = DomElement.ByCssSelector(".wd-test", TestFixture);

            Assert.IsFalse(checkBoxDomTable.HasClass("x-form-cb-checked"));
            Assert.IsFalse(checkBox.Checked);

            checkBox.Checked = true;

            Assert.IsTrue(checkBoxDomTable.HasClass("x-form-cb-checked"));
            Assert.IsTrue(checkBox.Checked);

            checkBox.Checked = false;

            Assert.IsFalse(checkBoxDomTable.HasClass("x-form-cb-checked"));
            Assert.IsFalse(checkBox.Checked);
        }

        [Test]
        public void CheckBox_Checked_Value()
        {
            CreateCheckbox(true);
            var element = Driver.FindElement(By.CssSelector(".wd-test"));
            var checkBox = new CheckBox(element, Driver);
            var checkBoxDomTable = DomElement.ByCssSelector(".wd-test", TestFixture);

            Assert.IsTrue(checkBoxDomTable.HasClass("x-form-cb-checked"));
            Assert.IsTrue(checkBox.Checked);
        }

        [Test]
        public void CheckBox_Click_Clicks_InputElement()
        {
            CreateCheckbox(false);
            var element = Driver.FindElement(By.CssSelector(".wd-test"));
            var checkBox = new CheckBox(element, Driver);
            var checkBoxDomTable = DomElement.ByCssSelector(".wd-test", TestFixture);

            checkBox.Click();
            Assert.IsTrue(checkBoxDomTable.HasClass("x-form-cb-checked"));
            Assert.IsTrue(checkBox.Checked);
        }

        [Test]
        public void CheckBox_SetSame_Checked_DoesNotChangeValue()
        {
            CreateCheckbox(false);
            var element = Driver.FindElement(By.CssSelector(".wd-test"));
            var checkBox = new CheckBox(element, Driver);
            var checkBoxDomTable = DomElement.ByCssSelector(".wd-test", TestFixture);

            Assert.IsFalse(checkBoxDomTable.HasClass("x-form-cb-checked"));
            Assert.IsFalse(checkBox.Checked);

            checkBox.Checked = false;

            Assert.IsFalse(checkBoxDomTable.HasClass("x-form-cb-checked"));
            Assert.IsFalse(checkBox.Checked);

            checkBox.Checked = true;

            Assert.IsTrue(checkBoxDomTable.HasClass("x-form-cb-checked"));
            Assert.IsTrue(checkBox.Checked);

            checkBox.Checked = true;

            Assert.IsTrue(checkBoxDomTable.HasClass("x-form-cb-checked"));
            Assert.IsTrue(checkBox.Checked);
        }

        [Test]
        public void CheckBox_UnChecked_Value()
        {
            CreateCheckbox(false);
            var element = Driver.FindElement(By.CssSelector(".wd-test"));
            var checkBox = new CheckBox(element, Driver);
            var checkBoxDomTable = DomElement.ByCssSelector(".wd-test", TestFixture);

            Assert.IsFalse(checkBoxDomTable.HasClass("x-form-cb-checked"));
            Assert.IsFalse(checkBox.Checked);
        }

        private void CreateCheckbox(bool isChecked)
        {
            new ExtjsViewBuilder()
                .AddCheckBox()
                .WithName(_CheckboxName)
                .WithFieldLabel("TestLabel")
                .WithClassName("wd-test")
                .WithProperty("checked", isChecked ? "true" : "false")
                .Build()
                .WriteItemsToTestjs();
        }
    }
}