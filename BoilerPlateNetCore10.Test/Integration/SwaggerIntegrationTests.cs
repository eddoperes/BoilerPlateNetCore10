
using BoilerPlateNetCore10.Test.Tools;
using Microsoft.AspNetCore.Mvc.Testing;

namespace BoilerPlateNetCore10.Test.Integration
{
    public class SwaggerIntegrationTests: IClassFixture<SQLServerFixture>
    {
        private readonly HttpClient _httpClient;
      

        public SwaggerIntegrationTests(SQLServerFixture sqlServerFixture)
        {
            var factory = new CustomWebApplicationFactory<BoilerPlateNetCore10.API.Program>(sqlServerFixture.ConnectionString);
            _httpClient = factory.CreateClient(new WebApplicationFactoryClientOptions { 
                BaseAddress = new Uri("http://localhost")
            });
        }

        [Fact]  
        public async Task Swagger_UI_Endpoint_Returns_Success()
        {
            // Arrange and Act
            var response = await _httpClient.GetAsync("/swagger/index.html");
            // 
            //var response = await _httpClient.SendAsync(request);
            // Assert
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            Assert.Contains("Swagger UI", content);
        }



    }
}
