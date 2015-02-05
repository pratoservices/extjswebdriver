using RestSharp;

namespace ExtjsWd.Exceptions
{
    public class ServiceLogInfoResolver : IExceptionLogInfoResolver
    {
        private readonly string _logServerLocation;

        public ServiceLogInfoResolver(string logServerLocation)
        {
            _logServerLocation = logServerLocation;
        }

        public string ReadLog()
        {
            var restClient = new RestClient(ScenarioFixture.Instance.ResolveHostAndPort());
            var restRequest = new RestRequest(_logServerLocation);
            restRequest.AddHeader("Content-Type", "text/plain");
            restRequest.AddHeader("Accept", "text/plain");
            var response = restClient.Execute(restRequest);
            return response.Content;
        }
    }
}