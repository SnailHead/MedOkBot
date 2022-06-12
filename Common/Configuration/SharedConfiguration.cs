
using Microsoft.Extensions.Logging;

namespace Common.Configuration
{
	public class SharedConfiguration
	{
		public static string DbConnectionString { get; private set; }

		public static ILoggerFactory LoggerFactory { get; private set; }

		public static void UpdateSharedConfiguration(string dbConnectionString, ILoggerFactory loggerFactory)
		{
			DbConnectionString = dbConnectionString;
			LoggerFactory = loggerFactory;
		}
	}
}
