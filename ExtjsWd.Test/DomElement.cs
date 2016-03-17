namespace ExtjsWd.Test
{
    public class DomElement
    {
        private readonly string _ElementFetchFunction;
        private readonly ScenarioFixture _Fixture;

        private DomElement(string elementFetchFunction, ScenarioFixture fixture)
        {
            _ElementFetchFunction = elementFetchFunction;
            _Fixture = fixture;
        }

        public object Value
        {
            get
            {
                return _Fixture.EvalJS(_ElementFetchFunction + ".value;");
            }
        }

        public static DomElement ByCssSelector(string cssSelector, ScenarioFixture fixture)
        {
            return new DomElement(" return document.querySelector('" + cssSelector + "')", fixture);
        }

        public static DomElement ByName(string name, ScenarioFixture fixture)
        {
            return new DomElement(" return document.getElementsByName('" + name + "')[0]", fixture);
        }

        public bool HasClass(string className)
        {
            var hasClass = _Fixture.EvalJS(_ElementFetchFunction + ".classList.contains('" + className + "');");
            if (hasClass is bool)
            {
                return (bool)hasClass;
            }
            return false;
        }
    }
}