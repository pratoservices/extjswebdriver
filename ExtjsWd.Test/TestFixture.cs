using System.IO;

namespace ExtjsWd.Test
{
    public class TestFixture : ScenarioFixture
    {
        public override string ResolveHostAndPort()
        {
            return Directory.GetCurrentDirectory() + "/ExtSandBox/test.html";
        }
    }
}