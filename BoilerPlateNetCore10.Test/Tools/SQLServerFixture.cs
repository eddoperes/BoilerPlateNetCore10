using System.ComponentModel;
using System.Diagnostics;
using Testcontainers.MsSql;

namespace BoilerPlateNetCore10.Test.Tools
{
    public class SQLServerFixture : IAsyncLifetime
    {
      
        public MsSqlContainer Container { get; }

        public string ConnectionString => Container.GetConnectionString();

        [Obsolete]
        public SQLServerFixture()
        {
            Container = new MsSqlBuilder()
                .WithPassword("3Ddopere$")
                .Build();
        }

        public async Task InitializeAsync()
        {
            try
            {
                await Container.StartAsync();
            }
            catch 
            {
                Debug.WriteLine(await Container.GetLogsAsync());
                throw;
            }

            // do migrations here
        }

        public async Task DisposeAsync()
        {
            await Container.DisposeAsync();
        }

    }
}
