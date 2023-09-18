using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using NUnit.Framework;
using ProductLake.Authentication;
using ProductLake.Controllers;
using ProductLake.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using Assert = NUnit.Framework.Assert;

namespace ProductLake.Test.Intergation
{
    public class ProductsApiIntegrationTest
    {
        private WebApplicationFactory<Program> applicationFactory;

        [SetUp]
        public void Startup()
        {
            this.applicationFactory = new WebApplicationFactory<Program>();
        }

        [Test]
        public async Task  ShouldReturnListOfProductsWhenRequestIsAuthorized()
        {
            // Arrange.
            HttpClient httpClient = this.applicationFactory.CreateClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GenerateJwtToken());
            
            // Act.
            HttpResponseMessage response = await httpClient.GetAsync("/api/product");

            // Assert.
            IEnumerable<Product> products = JsonConvert.DeserializeObject<IEnumerable<Product>>
            (await response.Content.ReadAsStringAsync());

            // Either test the service as it is or
            // follow the mock method used ShouldGetProductListWhenRequestIsAuthorized to perform more granular assertion. 
            Assert.AreEqual(2, products.Count());
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        // Add other test
        // - Unauthorized
        // - test filter method

        private string GenerateJwtToken()
        {
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtSettings.Key));
            SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            JwtSecurityToken token = new JwtSecurityToken(JwtSettings.Issuer, JwtSettings.Audience,null, expires: DateTime.Now.AddMinutes(30), signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}