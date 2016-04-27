using System.IO;

namespace ExtjsWd.Test
{
    public class TestFixture : ScenarioFixture
    {
        public TestFixture()
        {
        }

        public override string ResolveHostAndPort()
        {
            return Directory.GetCurrentDirectory() + "/ExtSandBox/test.html";
        }
    }
}