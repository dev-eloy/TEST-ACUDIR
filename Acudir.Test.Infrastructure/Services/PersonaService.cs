using Acudir.Test.Domain.Interfaces;
using Acudir.Test.Domain.Models;
using Acudir.Test.Domain.Models.DTOs;
using Acudir.Test.Domain.Constants;

namespace Acudir.Test.Infrastructure.Services
{
    public class PersonaService : IPersonaService
    {
        #region Fields
        private readonly IPersonaRepository _repository;
        private readonly IValidatorService _validatorService;
        #endregion

        #region Constructor
        public PersonaService(IPersonaRepository repository, IValidatorService validatorService)
        {
            _repository = repository;
            _validatorService = validatorService;
        }
        #endregion

        #region Query Operations
        public async Task<List<Persona>> GetAllAsync(string? nombre = null, int? edad = null,
            string? domicilio = null, string? telefono = null, string? profesion = null)
        {
            var personas = await _repository.GetAllAsync();

            return personas.Where(p =>
                (nombre == null || p.NombreCompleto.Contains(nombre, StringComparison.OrdinalIgnoreCase)) &&
                (edad == null || p.Edad == edad) &&
                (domicilio == null || p.Domicilio.Contains(domicilio, StringComparison.OrdinalIgnoreCase)) &&
                (telefono == null || p.Telefono.Contains(telefono, StringComparison.OrdinalIgnoreCase)) &&
                (profesion == null || p.Profesion.Contains(profesion, StringComparison.OrdinalIgnoreCase))
            ).ToList();
        }
        #endregion

        #region Command Operations
        public async Task<ServiceResult> AddAsync(Persona persona)
        {
            var result = new ServiceResult();

            var validationResult = await _validatorService.ValidateAsync(persona);
            if (!validationResult.Success)
            {
                result.Success = false;
                result.Errors = validationResult.Errors;
                result.Message = ValidationMessages.Service.DatosInvalidos;
                return result;
            }

            var repositoryResult = await _repository.AddAsync(persona);
            if (!repositoryResult)
            {
                result.Success = false;
                result.Errors.Add(ValidationMessages.Service.Duplicado);
                result.Message = ValidationMessages.Service.NoSePudoAgregar;
                return result;
            }

            result.Success = true;
            result.Message = ValidationMessages.Service.AgregadaExitosamente;
            return result;
        }

        public async Task<ServiceResult> UpdateAsync(Persona persona)
        {
            var result = new ServiceResult();

            var validationResult = await _validatorService.ValidateAsync(persona);
            if (!validationResult.Success)
            {
                result.Success = false;
                result.Errors = validationResult.Errors;
                result.Message = ValidationMessages.Service.DatosInvalidos;
                return result;
            }

            var repositoryResult = await _repository.UpdateAsync(persona);
            if (!repositoryResult)
            {
                result.Success = false;
                result.Errors.Add(ValidationMessages.Service.NoEncontrada);
                result.Message = ValidationMessages.Service.NoSePudoActualizar;
                return result;
            }

            result.Success = true;
            result.Message = ValidationMessages.Service.ActualizadaExitosamente;
            return result;
        }
        #endregion
    }
}