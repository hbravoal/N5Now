using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N5.Utilities
{
    /// <summary>
    /// Generic Utilities
    /// </summary>
    public static class WebConfiguration
    {
        /// <summary>
        /// Return the App Name
        /// </summary>
        public static string AppName { get => GetValue("AppName") ?? string.Empty; }

        /// <summary>
        /// Return the Enviroment
        /// </summary>
        public static string AppEnvironment { get => Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? Environment.GetEnvironmentVariable("ENV") ?? string.Empty; }

        /// <summary>
        /// Validated is Prod
        /// </summary>
        public static bool IsProd { get => Environment.GetEnvironmentVariable("ENV")?.ToUpper() == "PROD"; }

        private static IConfiguration? _configuration;

        /// <summary>
        /// Return a instance of IConfiguration for appsettings.json
        /// </summary>
        /// <returns></returns>
        public static IConfiguration GetConfiguration()
        {
            if (_configuration is null)
            {
                var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
                    .AddEnvironmentVariables();

                _configuration = builder.Build();
            }

            return _configuration;
        }

        /// <summary>
        /// Get a value of configuration file by Key
        /// </summary>
        /// <param name="Key"></param>
        /// <returns></returns>
        public static string? GetValue(string Key) => GetConfiguration().GetSection(Key).Value;

        /// <summary>
        /// Get a value of configuration file by Key
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Key"></param>
        /// <returns></returns>
        public static T? GetValue<T>(string Key) => GetConfiguration().GetSection(Key).Get<T>();
    }
}