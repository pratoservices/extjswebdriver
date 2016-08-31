using System.IO;
using OpenQA.Selenium;

namespace ExtjsWd.js
{
    public static class JSCommands
    {
        public static void CloseAllMessageBoxes()
        {
            const string closeMsgBoxesViaExtjs = @"Ext.Msg.close();";
            ScenarioFixture.Instance.EvalJS(closeMsgBoxesViaExtjs);
        }

   
        public static void CloseAllTooltips()
        {
            EvalJS("[].forEach.call(document.querySelectorAll('.x-tip'), function(el) { el.remove(); })");
            EvalJS("[].forEach.call(document.querySelectorAll('.x-css-shadow'), function(el) { el.remove(); })");
        }

        public static void CloseAllWindows()
        {
            const string killWindowsViaExtjs = @"Ext.WindowMgr.each(function(itemToDestroy){if(itemToDestroy.isComponent && itemToDestroy.isVisible()) {itemToDestroy.destroy();}});";
            EvalJS(killWindowsViaExtjs);
        }

        public static void CloseAnyOpenGarbage()
        {
            CloseAllMessageBoxes();
            CloseAllWindows();
            CloseAllTooltips();
        }

        public static void EvalJSFile(string path)
        {
            EvalJS(ReadJSFile(path));
        }

        //Returns the contents of the property of the domelemnt attached to the webelement
        public static string GetPropertyOfWebElement(IWebElement webElement, string propertyName)
        {
            var id = webElement.GetAttribute("id");
            var result =  ScenarioFixture.Instance.EvalJS("return document.getElementById('" + id + "')." +propertyName);
            if (result == null)
            {
                return string.Empty;
            }
            return result.ToString();
        }

        public static void SetAttribute(IWebElement webElement, string attributeName, string value)
        {
            var id = webElement.GetAttribute("id");
           ScenarioFixture.Instance.EvalJS("document.getElementById('" + id + "').setAttribute('" + attributeName + "', '" + value + "')");
        }

        private static void EvalJS(string js)
        {
            ScenarioFixture.Instance.EvalJS(js);
        }

        private static string ReadJSFile(string relativePath)
        {
            var assembly = typeof(JSCommands).Assembly;
            return new StreamReader(
                    assembly.GetManifestResourceStream(assembly.GetName().Name + "." + relativePath.Replace("/", ".")))
                    .ReadToEnd();
        }

        public static void ClickUsingJavascript(int x, int y)
        {
            EvalJS("document.elementFromPoint(" + x + "," + y + ").click();");
        }
    }
}