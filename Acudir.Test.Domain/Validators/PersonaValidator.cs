using FluentValidation;
using Acudir.Test.Domain.Models;
using Acudir.Test.Domain.Constants;

namespace Acudir.Test.Domain.Validators
{
    public class PersonaValidator : AbstractValidator<Persona>
    {
        public PersonaValidator()
        {
            #region NombreCompleto
            RuleFor(x => x.NombreCompleto)
                .NotEmpty().WithMessage(ValidationMessages.Persona.NombreCompletoObligatorio)
                .MaximumLength(100).WithMessage(ValidationMessages.Persona.NombreCompletoMaxLength)
                .Matches(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$").WithMessage(ValidationMessages.Persona.NombreCompletoSoloLetras);
            #endregion

            #region Edad
            RuleFor(x => x.Edad)
                .GreaterThan(0).WithMessage(ValidationMessages.Persona.EdadMayorACero)
                .LessThanOrEqualTo(100).WithMessage(ValidationMessages.Persona.EdadMaxima)
                .Must(edad => edad >= 1 && edad <= 100).WithMessage(ValidationMessages.Persona.EdadRango);
            #endregion

            #region Domicilio
            RuleFor(x => x.Domicilio)
                .NotEmpty().WithMessage(ValidationMessages.Persona.DomicilioObligatorio)
                .MaximumLength(200).WithMessage(ValidationMessages.Persona.DomicilioMaxLength);
            #endregion

            #region Telefono
            RuleFor(x => x.Telefono)
                .NotEmpty().WithMessage(ValidationMessages.Persona.TelefonoObligatorio)
                .MaximumLength(10).WithMessage(ValidationMessages.Persona.TelefonoMaxLength)
                .Matches(@"^\d+$").WithMessage(ValidationMessages.Persona.TelefonoSoloNumeros)
                .Must(telefono => telefono.Length >= 7 && telefono.Length <= 10)
                .WithMessage(ValidationMessages.Persona.TelefonoRango);
            #endregion

            #region Profesion
            RuleFor(x => x.Profesion)
                .NotEmpty().WithMessage(ValidationMessages.Persona.ProfesionObligatoria)
                .MaximumLength(50).WithMessage(ValidationMessages.Persona.ProfesionMaxLength)
                .Matches(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$").WithMessage(ValidationMessages.Persona.ProfesionSoloLetras);
            #endregion
        }
    }
}