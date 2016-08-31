using System.IO;

namespace ExtjsWd.Test
{
    public class TestFixture : ScenarioFixture
    {
        public TestFixture()
        {
            Instance = this;
        }

        public override string ResolveHostAndPort()
        {
            return Directory.GetCurrentDirectory() + "/ExtSandBox/test4.html";
        }
    }
}