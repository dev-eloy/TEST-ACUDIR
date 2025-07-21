using System.Text.Json;
using Acudir.Test.Domain.Interfaces;
using Acudir.Test.Domain.Models;

namespace Acudir.Test.Infrastructure.Repositories
{
    public class PersonaRepository : IPersonaRepository
    {
        private readonly string _filePath;

        public PersonaRepository()
        {
            _filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "Test.json");
        }

        #region Read Operations
        public async Task<List<Persona>> GetAllAsync()
        {
            if (!File.Exists(_filePath))
            {
                var directory = Path.GetDirectoryName(_filePath);
                if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                await SaveToFileAsync(new List<Persona>());
                return new List<Persona>();
            }

            var json = await File.ReadAllTextAsync(_filePath);
            return JsonSerializer.Deserialize<List<Persona>>(json) ?? new List<Persona>();
        }

        public async Task<Persona?> GetByIdAsync(string nombreCompleto)
        {
            var personas = await GetAllAsync();
            return personas.FirstOrDefault(p => p.NombreCompleto.Equals(nombreCompleto, StringComparison.OrdinalIgnoreCase));
        }
        #endregion

        #region Write Operations
        public async Task<bool> AddAsync(Persona persona)
        {
            var personas = await GetAllAsync();

            if (personas.Any(p => p.NombreCompleto.Equals(persona.NombreCompleto, StringComparison.OrdinalIgnoreCase)))
                return false;

            personas.Add(persona);
            await SaveToFileAsync(personas);
            return true;
        }

        public async Task<bool> UpdateAsync(Persona persona)
        {
            var personas = await GetAllAsync();
            var existingPersona = personas.FirstOrDefault(p => p.NombreCompleto.Equals(persona.NombreCompleto, StringComparison.OrdinalIgnoreCase));

            if (existingPersona == null)
                return false;

            var index = personas.IndexOf(existingPersona);
            personas[index] = persona;
            await SaveToFileAsync(personas);
            return true;
        }

        public async Task<bool> DeleteAsync(string nombreCompleto)
        {
            var personas = await GetAllAsync();
            var personaToDelete = personas.FirstOrDefault(p => p.NombreCompleto.Equals(nombreCompleto, StringComparison.OrdinalIgnoreCase));

            if (personaToDelete == null)
                return false;

            personas.Remove(personaToDelete);
            await SaveToFileAsync(personas);
            return true;
        }
        #endregion

        #region Private Methods
        private async Task SaveToFileAsync(List<Persona> personas)
        {
            var directory = Path.GetDirectoryName(_filePath);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            var json = JsonSerializer.Serialize(personas, new JsonSerializerOptions { WriteIndented = true });
            await File.WriteAllTextAsync(_filePath, json);
        }
        #endregion
    }
}