using Acudir.Test.Domain.Constants;
using Acudir.Test.Domain.Interfaces;
using Acudir.Test.Domain.Models;
using Acudir.Test.Domain.Models.DTOs;
using Acudir.Test.Domain.Validators;
using FluentValidation;

namespace Acudir.Test.Infrastructure.Services
{
    public class ValidatorService : IValidatorService
    {
        #region Fields
        private readonly PersonaValidator _personaValidator;
        #endregion

        #region Constructor
        public ValidatorService()
        {
            _personaValidator = new PersonaValidator();
        }
        #endregion

        #region Methods
        public async Task<ServiceResult> ValidateAsync<T>(T entity) where T : class
        {
            var result = new ServiceResult();

            if (entity is Persona persona)
            {
                var validationResult = await _personaValidator.ValidateAsync(persona);
                result.Success = validationResult.IsValid;
                result.Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                result.Message = validationResult.IsValid ? ValidationMessages.Service.ValidacionExitosa : ValidationMessages.Service.ErroresValidacion;
            }

            return result;
        }
        #endregion
    }
}