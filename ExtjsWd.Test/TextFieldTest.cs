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
            var domInputTextField = DomElement.ByName(_TextBoxName, TestFixture);

            Assert.AreEqual("Dit is een Test", domInputTextField.Value);

            inputTextField.Clear();

            Assert.AreEqual("", domInputTextField.Value);
        }

        [Test]
        public void TextField_CanFillIn()
        {
            var textFieldElement = Driver.FindElement(By.Name(_TextBoxName));
            var inputTextField = new InputTextField(textFieldElement, Driver);

            var domInputTextField = DomElement.ByName(_TextBoxName, TestFixture);

            Assert.AreEqual("Dit is een Test", domInputTextField.Value);

            inputTextField.FillIn("Dit is de andere test");

            Assert.AreEqual("Dit is de andere test", domInputTextField.Value);
        }
    }
}