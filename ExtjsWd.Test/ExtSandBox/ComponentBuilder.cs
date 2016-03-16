using System.Collections.Generic;

namespace ExtjsWd.Test.ExtSandBox
{
    public class ComponentBuilder
    {
        private readonly ExtjsViewBuilder _Builder;
        private readonly List<string> _Items = new List<string>();

        public ComponentBuilder(ExtjsViewBuilder builder)
        {
            _Builder = builder;
        }

        public ExtjsViewBuilder Build()
        {
            _Builder.AddItem("{" + string.Join(",", _Items) + "}");
            return _Builder;
        }

        public ComponentBuilder WithClassName(string className)
        {
            _Items.Add("cls:'" + className + "'");
            return this;
        }

        public ComponentBuilder WithFieldLabel(string fieldLabel)
        {
            _Items.Add("fieldLabel:'" + fieldLabel + "'");
            return this;
        }

        public ComponentBuilder WithName(string name)
        {
            _Items.Add("name:'" + name + "'");
            return this;
        }

        public ComponentBuilder WithValue(string value)
        {
            _Items.Add("value:'" + value + "'");
            return this;
        }

        public ComponentBuilder WithXType(string xtype)
        {
            _Items.Add("xtype:'" + xtype + "'");
            return this;
        }
    }
}