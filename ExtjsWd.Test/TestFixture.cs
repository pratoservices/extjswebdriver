using System.IO;
using NUnit.Framework;

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
            return Path.Combine(TestContext.CurrentContext.TestDirectory,  "ExtSandBox/test4.html");
        }
    }
}