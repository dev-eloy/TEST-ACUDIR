using Acudir.Test.Domain.Models;
using Acudir.Test.Domain.Models.DTOs;

namespace Acudir.Test.Domain.Interfaces
{
    public interface IPersonaService
    {
        #region Query Operations
        Task<List<Persona>> GetAllAsync(string? nombre = null, int? edad = null,
            string? domicilio = null, string? telefono = null, string? profesion = null);
        #endregion

        #region Command Operations
        Task<ServiceResult> AddAsync(Persona persona);
        Task<ServiceResult> UpdateAsync(Persona persona);
        #endregion
    }
}