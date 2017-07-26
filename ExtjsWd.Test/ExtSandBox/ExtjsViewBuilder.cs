using System.Collections.Generic;
using System.IO;
using NUnit.Framework;

namespace ExtjsWd.Test.ExtSandBox
{
    public class ExtjsViewBuilder
    {
        private readonly List<string> _Items = new List<string>();

        public ComponentBuilder AddCheckBox()
        {
            return new ComponentBuilder(this)
                .WithXType("checkbox");
        }

        public void AddItem(string item)
        {
            _Items.Add(item);
        }

        public ComponentBuilder AddTextField()
        {
            return new ComponentBuilder(this)
                .WithXType("textfield");
        }

        public void WriteItemsToTestjs()
        {
            File.WriteAllText(Path.Combine(TestContext.CurrentContext.TestDirectory, @"ExtSandBox\test.js"), Build());
        }

        private string Build()
        {
            return ExtjsViewBoilerPlate.BeforeItems + string.Join(",", _Items) + ExtjsViewBoilerPlate.AfterItems;
        }
    }
}