using ExtjsWd.Elements;
using ExtjsWd.js;
using ExtjsWd.Test.ExtSandBox;
using NUnit.Framework;
using OpenQA.Selenium;

namespace ExtjsWd.Test
{
    [TestFixture]
    public class TextFieldTest : WebDriverTest
    {
        private string _TextFieldName = "TestTextField";

        [SetUp]
        public void CreateTestWithTextField()
        {
            new ExtjsViewBuilder()
                .AddTextField()
                    .WithName(_TextFieldName)
                    .WithFieldLabel("TestLabel")
                    .WithClassName("wd-test")
                    .WithValue("Dit is een Test")
                    .Build()
                .WriteItemsToTestjs();
        }

        [Test]
        public void TextField_CanClear()
        {
            var textFieldElement = Driver.FindElement(By.Name(_TextFieldName));
            var inputTextField = new InputTextField(textFieldElement, Driver);
            var domInputTextField = DomElement.ByName(_TextFieldName, TestFixture);

            Assert.AreEqual("Dit is een Test", domInputTextField.Value);

            inputTextField.Clear();

            Assert.AreEqual("", domInputTextField.Value);
        }

        [Test]
        public void TextField_CanFillIn()
        {
            var textFieldElement = Driver.FindElement(By.Name(_TextFieldName));
            var inputTextField = new InputTextField(textFieldElement, Driver);

            var domInputTextField = DomElement.ByName(_TextFieldName, TestFixture);

            Assert.AreEqual("Dit is een Test", domInputTextField.Value);

            inputTextField.FillIn("Dit is de andere test");

            Assert.AreEqual("Dit is de andere test", domInputTextField.Value);
        }
    }
}