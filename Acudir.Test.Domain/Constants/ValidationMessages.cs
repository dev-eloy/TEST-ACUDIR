namespace Acudir.Test.Domain.Constants
{
    public static class ValidationMessages
    {
        public static class Persona
        {
            public static string NombreCompletoObligatorio => "El nombre completo es obligatorio";
            public static string NombreCompletoMaxLength => "El nombre completo no puede exceder 100 caracteres";
            public static string NombreCompletoSoloLetras => "El nombre completo solo puede contener letras y espacios";

            public static string EdadMayorACero => "La edad debe ser mayor a 0";
            public static string EdadMaxima => "La edad no puede ser mayor a 100 años";
            public static string EdadRango => "La edad debe estar entre 1 y 100 años";

            public static string DomicilioObligatorio => "El domicilio es obligatorio";
            public static string DomicilioMaxLength => "El domicilio no puede exceder 200 caracteres";

            public static string TelefonoObligatorio => "El teléfono es obligatorio";
            public static string TelefonoMaxLength => "El teléfono no puede exceder 10 caracteres";
            public static string TelefonoSoloNumeros => "El teléfono solo puede contener números";
            public static string TelefonoRango => "El teléfono debe tener entre 7 y 10 dígitos";

            public static string ProfesionObligatoria => "La profesión es obligatoria";
            public static string ProfesionMaxLength => "La profesión no puede exceder 50 caracteres";
            public static string ProfesionSoloLetras => "La profesión solo puede contener letras y espacios";
        }

        public static class Service
        {
            public static string DatosInvalidos => "Datos de entrada inválidos";
            public static string AgregadaExitosamente => "Persona agregada exitosamente";
            public static string ActualizadaExitosamente => "Persona actualizada exitosamente";
            public static string NoSePudoAgregar => "No se pudo agregar la persona";
            public static string NoSePudoActualizar => "No se pudo actualizar la persona";
            public static string Duplicado => "Ya existe una persona con ese nombre completo";
            public static string NoEncontrada => "No se encontró la persona para actualizar";
            public static string ValidacionExitosa => "Validación exitosa";
            public static string ErroresValidacion => "Errores de validación";
        }
    }
}