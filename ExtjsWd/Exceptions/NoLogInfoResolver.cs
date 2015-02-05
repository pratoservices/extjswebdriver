namespace ExtjsWd.Exceptions
{
    public class NoLogInfoResolver : IExceptionLogInfoResolver
    {
        public string ReadLog()
        {
            return string.Empty;
        }
    }
}