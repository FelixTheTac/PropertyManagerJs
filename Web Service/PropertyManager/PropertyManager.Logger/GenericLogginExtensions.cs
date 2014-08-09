
namespace PropertyManager.Logger
{
    public static class GenericLoggingExtensions
    {
        public static ILogger Log<T>(this T thing)
        {
            var log = LogManager.GetLogger<T>();
            return log;
        }
    }
}
