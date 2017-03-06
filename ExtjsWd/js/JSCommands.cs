using System;
using System.IO;
using OpenQA.Selenium;

namespace ExtjsWd.js
{
    public static class JSCommands
    {
        public static void ClickUsingJavascript(int x, int y)
        {
            EvalJS("document.elementFromPoint(" + x + "," + y + ").click();");
        }

        public static void CloseAllMessageBoxes()
        {
            const string closeMsgBoxesViaExtjs = @"try { 
Ext.Msg.close(); 
} catch { }";
            ScenarioFixture.Instance.EvalJS(closeMsgBoxesViaExtjs);
        }

        public static void CloseAllNotifications()
        {
            EvalJS("Ext.ux.desktop.MessageFactory.hideAll();");
        }

        public static void CloseAllWindows()
        {
            const string killWindowsViaExtjs = @"Object.values(Ext.ComponentMgr.all).filter(function(comp){return !comp.ownerCt && !comp.parent && comp.xtype == 'Ext_ux_desktop_Window';}).forEach(function(comp) { comp.destroy(); });";
            EvalJS(killWindowsViaExtjs);
        }

        public static void CloseAnyOpenGarbage()
        {
            CloseAllMessageBoxes();
            CloseAllWindows();
            CloseAllNotifications();
        }

        public static void EvalJSFile(string path)
        {
            EvalJS(ReadJSFile(path));
        }

        //Returns the contents of the property of the domelemnt attached to the webelement
        public static string GetPropertyOfWebElement(IWebElement webElement, string propertyName)
        {
            var id = webElement.GetAttribute("id");
            var result = ScenarioFixture.Instance.EvalJS("return document.getElementById('" + id + "')." + propertyName);
            if (result == null)
            {
                return string.Empty;
            }
            return result.ToString();
        }

        public static void MouseDownByCssSelector(string cssSelector)
        {
            EvalJS("var foundElement = document.querySelector('" + cssSelector + "') ;  var clickEvent = document.createEvent('MouseEvents'); clickEvent.initEvent('mousedown', true, true); foundElement.dispatchEvent(clickEvent);");
        }

        public static void ScrollIntoView(IWebElement webElement)
        {
            var id = webElement.GetAttribute("id");
            ScenarioFixture.Instance.EvalJS("document.getElementById('" + id + "').scrollIntoView(true);");
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
    }
}
