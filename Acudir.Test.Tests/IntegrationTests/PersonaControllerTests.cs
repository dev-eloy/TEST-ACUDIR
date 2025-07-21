using Acudir.Test.Domain.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Text;
using System.Text.Json;
using Xunit;

namespace Acudir.Test.Tests.IntegrationTests
{
    public class PersonaControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public PersonaControllerTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GetAll_ReturnsSuccessStatusCode()
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync("/Persona/GetAll");

            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task Post_ValidPersona_ReturnsCreated()
        {
            var client = _factory.CreateClient();
            var persona = new Persona
            {
                NombreCompleto = "Test User",
                Edad = 25,
                Domicilio = "Test Address",
                Telefono = "123456789",
                Profesion = "Tester"
            };
            var json = JsonSerializer.Serialize(persona);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("/Persona", content);

            Assert.Equal(System.Net.HttpStatusCode.Created, response.StatusCode);
        }
    }
}