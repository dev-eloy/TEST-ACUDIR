using Acudir.Test.Domain.Models;

namespace Acudir.Test.Domain.Interfaces
{
    public interface IPersonaRepository
    {
        #region Read Operations
        Task<List<Persona>> GetAllAsync();
        Task<Persona?> GetByIdAsync(string nombreCompleto);
        #endregion

        #region Write Operations
        Task<bool> AddAsync(Persona persona);
        Task<bool> UpdateAsync(Persona persona);
        Task<bool> DeleteAsync(string nombreCompleto);
        #endregion
    }
}