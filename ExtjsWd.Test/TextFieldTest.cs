using ExtjsWd.Elements;
using ExtjsWd.Test.ExtSandBox;
using NUnit.Framework;
using OpenQA.Selenium;

namespace ExtjsWd.Test
{
    [TestFixture]
    public class TextFieldTest : WebDriverTest
    {
        private string _TextBoxName = "TestTextBox";

        [SetUp]
        public void CreateTestWithTextField()
        {
            new ExtjsViewBuilder()
                .AddTextField()
                    .WithName(_TextBoxName)
                    .WithFieldLabel("TestLabel")
                    .WithClassName("wd-test")
                    .WithValue("Dit is een Test")
                    .Build()
                .WriteItemsToTestjs();
        }

        [Test]
        public void TextField_CanClear()
        {
            var textFieldElement = Driver.FindElement(By.Name(_TextBoxName));
            var inputTextField = new InputTextField(textFieldElement, Driver);
            var value = GetValueByName(_TextBoxName);

            Assert.AreEqual("Dit is een Test", value);

            inputTextField.Clear();

            var newValue = GetValueByName(_TextBoxName);

            Assert.AreEqual("", newValue);
        }

        [Test]
        public void TextField_CanFillIn()
        {
            var textFieldElement = Driver.FindElement(By.Name(_TextBoxName));
            var inputTextField = new InputTextField(textFieldElement, Driver);
            var value = GetValueByName(_TextBoxName);

            Assert.AreEqual("Dit is een Test", value);

            inputTextField.FillIn("Dit is de andere test");

            var newValue = GetValueByName(_TextBoxName);

            Assert.AreEqual("Dit is de andere test", newValue);
        }
    }
}