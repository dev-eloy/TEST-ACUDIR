using Xunit;
using Moq;
using Acudir.Test.Domain.Interfaces;
using Acudir.Test.Domain.Models;
using Acudir.Test.Domain.Models.DTOs;
using Acudir.Test.Infrastructure.Services;

namespace Acudir.Test.Tests.UnitTests
{
    public class PersonaServiceTests
    {
        #region Fields
        private readonly Mock<IPersonaRepository> _mockRepository;
        private readonly Mock<IValidatorService> _mockValidatorService;
        private readonly PersonaService _service;
        #endregion

        #region Constructor
        public PersonaServiceTests()
        {
            _mockRepository = new Mock<IPersonaRepository>();
            _mockValidatorService = new Mock<IValidatorService>();
            _service = new PersonaService(_mockRepository.Object, _mockValidatorService.Object);
        }
        #endregion

        #region Tests
        [Fact]
        public async Task GetAllAsync_WithNoFilters_ReturnsAllPersonas()
        {
            var expectedPersonas = new List<Persona>
            {
                new Persona { NombreCompleto = "Juan Perez", Edad = 30 },
                new Persona { NombreCompleto = "Maria Garcia", Edad = 25 }
            };
            _mockRepository.Setup(r => r.GetAllAsync()).ReturnsAsync(expectedPersonas);

            var result = await _service.GetAllAsync();

            Assert.Equal(expectedPersonas.Count, result.Count);
            Assert.Equal(expectedPersonas, result);
        }

        [Fact]
        public async Task AddAsync_ValidPersona_ReturnsSuccess()
        {
            var persona = new Persona { NombreCompleto = "Nuevo Usuario", Edad = 25 };
            var validationResult = new ServiceResult { Success = true, Errors = new List<string>() };

            _mockValidatorService.Setup(v => v.ValidateAsync(persona))
                .ReturnsAsync(validationResult);
            _mockRepository.Setup(r => r.AddAsync(persona)).ReturnsAsync(true);

            var result = await _service.AddAsync(persona);

            Assert.True(result.Success);
            Assert.Empty(result.Errors);
            _mockRepository.Verify(r => r.AddAsync(persona), Times.Once);
        }

        [Fact]
        public async Task AddAsync_InvalidPersona_ReturnsValidationErrors()
        {
            var persona = new Persona { NombreCompleto = "", Edad = 150 }; // Datos inválidos
            var validationResult = new ServiceResult
            {
                Success = false,
                Errors = new List<string> { "El nombre completo es obligatorio", "La edad no puede ser mayor a 100 años" }
            };

            _mockValidatorService.Setup(v => v.ValidateAsync(persona))
                .ReturnsAsync(validationResult);

            var result = await _service.AddAsync(persona);

            Assert.False(result.Success);
            Assert.NotEmpty(result.Errors);
            Assert.Equal(2, result.Errors.Count);
            _mockRepository.Verify(r => r.AddAsync(persona), Times.Never);
        }

        [Fact]
        public async Task UpdateAsync_ValidPersona_ReturnsSuccess()
        {
            var persona = new Persona { NombreCompleto = "Usuario Existente", Edad = 30 };
            var validationResult = new ServiceResult { Success = true, Errors = new List<string>() };

            _mockValidatorService.Setup(v => v.ValidateAsync(persona))
                .ReturnsAsync(validationResult);
            _mockRepository.Setup(r => r.UpdateAsync(persona)).ReturnsAsync(true);

            var result = await _service.UpdateAsync(persona);

            Assert.True(result.Success);
            Assert.Empty(result.Errors);
            _mockRepository.Verify(r => r.UpdateAsync(persona), Times.Once);
        }
        #endregion
    }
}