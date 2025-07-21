namespace Acudir.Test.Domain.Models
{
    public class Persona
    {
        #region Properties
        public string NombreCompleto { get; set; } = string.Empty;
        public int Edad { get; set; }
        public string Domicilio { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string Profesion { get; set; } = string.Empty;
        #endregion
    }
}