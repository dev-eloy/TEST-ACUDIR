using Acudir.Test.Domain.Models.DTOs;

namespace Acudir.Test.Domain.Interfaces
{
    public interface IValidatorService
    {
        Task<ServiceResult> ValidateAsync<T>(T entity) where T : class;
    }
}