
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Newtonsoft.Json;
using NUnit.Framework;
using ProductLake.Authentication;
using ProductLake.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Xunit;
using Assert = NUnit.Framework.Assert;

namespace ProductLakeTest.L0
{
    public class ProductLakeIntegrationTest : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> factory;

        public ProductLakeIntegrationTest(WebApplicationFactory<Program> factory)
        {
            this.factory = factory;
        }

        [Test]
        public async Task GetAllProducts()
        {
            var client = this.factory.CreateClient();
            var token = GenerateJwtToken();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            // Act
            var response = await client.GetAsync("/api/product");

            // Assert
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();
            List<Product> products = JsonConvert.DeserializeObject<List<Product>>(stringResponse);
            Assert.Equals(2, products.Count);
        }

        private string GenerateJwtToken()
        {
            SigningCredentials credentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtSettings.Key)), SecurityAlgorithms.HmacSha256);
            JwtSecurityToken token = new JwtSecurityToken(JwtSettings.Issuer, JwtSettings.Audience, null, expires: DateTime.Now.AddMinutes(120), signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}