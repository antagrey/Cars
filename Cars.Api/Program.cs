
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Threading.Tasks;
using Cars.Api.Settings;

namespace Cars.Api
{
    public class Program
    {
        public static Task Main() => CreateWebHost().RunAsync();

        internal static ApplicationSettings ApplicationSettings;

        private const string AppTitle = "CarsApi";

        private static IWebHost CreateWebHost()
        {
            Console.Title = AppTitle;

            SetApplicationSettings();

            var builder = WebHost.CreateDefaultBuilder().UseStartup<Startup>();

            return builder.Build();
        }

        private static void SetApplicationSettings()
        {
            ApplicationSettings = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build()
                .Get<ApplicationSettings>();
        }

    }
}
