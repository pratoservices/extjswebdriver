using ExtjsWd.Elements;
using ExtjsWd.Test.ExtSandBox;
using NUnit.Framework;
using OpenQA.Selenium;

namespace ExtjsWd.Test
{
    [TestFixture]
    public class JSCommandsTests : WebDriverTest
    {
        private string _TextFieldName = "JSCommandsTestsTextField";
        [Test]
        public void Value_ReturnsValueOfInput()
        {
            CreateATextfield();

            var textFieldElement = Driver.FindElement(By.Name(_TextFieldName));
            var inputTextField = new InputTextField(textFieldElement, Driver);
            Assert.AreEqual("Test1", inputTextField.Value);
        }

        private void CreateATextfield()
        {
            new ExtjsViewBuilder()
                .AddTextField()
                .WithName(_TextFieldName)
                .WithFieldLabel("TestLabel")
                .WithClassName("wd-test")
                .WithValue("Test1")
                .Build()
                .WriteItemsToTestjs();
        }
    }
}