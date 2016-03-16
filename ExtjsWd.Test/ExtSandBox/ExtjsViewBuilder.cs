using System.Collections.Generic;
using System.IO;

namespace ExtjsWd.Test.ExtSandBox
{
    public class ExtjsViewBuilder
    {
        private readonly List<string> _Items = new List<string>();

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
            File.WriteAllText(@"ExtSandBox\test.js", Build());
        }

        private string Build()
        {
            return ExtjsViewBoilerPlate.BeforeItems + string.Join(",", _Items) + ExtjsViewBoilerPlate.AfterItems;
        }
    }
}