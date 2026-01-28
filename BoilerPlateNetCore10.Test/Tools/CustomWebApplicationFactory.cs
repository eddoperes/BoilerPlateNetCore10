using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace BoilerPlateNetCore10.Test.Tools
{
    public class CustomWebApplicationFactory<TProgram>
                 :WebApplicationFactory<TProgram> where TProgram : class
    {

        private readonly string _connectionString;

        public CustomWebApplicationFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureAppConfiguration((context, configBuilder) =>
            {
                var inMemorySettings = new Dictionary<string, string>
                {
                    {"ConnectionStrings:DefaultConnection", _connectionString}
                };
                configBuilder.AddInMemoryCollection(inMemorySettings!);
            });
        }

    }
}
