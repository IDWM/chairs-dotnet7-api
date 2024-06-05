using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using project_dotnet7_api.Src.Helpers.Validators;

namespace project_dotnet7_api.Src.DTO.User
{
    public class RegisterUserDto
    {
        [Required(ErrorMessage = "El Rut es obligatorio.")]
        [RegularExpression(@"^\d{7,8}-[0-9kK]", ErrorMessage = "El Rut no tiene un formato válido.")]
        [RutValidation(ErrorMessage = "El Rut no es válido.")]
        public string Rut { get; set; } = string.Empty;

        [Required(ErrorMessage = "El Nombre es obligatorio.")]
        [RegularExpression(@"^[a-zA-ZáéíóúÁÉÍÓÚüÜñÑ\s]+$", ErrorMessage = "El Nombre solo puede contener caracteres del abecedario español.")]
        [MinLength(8, ErrorMessage = "El Nombre debe tener al menos 8 caracteres.")]
        [MaxLength(255, ErrorMessage = "El Nombre debe tener a lo más 255 caracteres.")]
        public string Name { get; set; } = string.Empty;

             
        [Required(ErrorMessage = "La Fecha de Nacimiento es obligatoria.")]
        [DataType(DataType.Date)]
        [DateValidation]   
        public string Birthday { get; set; } = string.Empty;

        [Required(ErrorMessage = "El Email es obligatorio.")]
        [EmailAddress(ErrorMessage = "El Email no tiene un formato válido.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "El Género es obligatorio.")]
        [GenderValidation]
        public string GenderId { get; set; } = string.Empty;

        [Required(ErrorMessage = "La Contraseña es obligatoria.")]
        [RegularExpression(@"^(?=.*[0-9])(?=.*[a-zA-Z])[a-zA-Z0-9]+$", ErrorMessage = "La Contraseña debe ser alfanumérica.")]
        [MinLength(8, ErrorMessage = "La Contraseña debe tener al menos 8 caracteres.")]
        [MaxLength(20, ErrorMessage = "La Contraseña debe tener a lo más 20 caracteres.")]
        public string Password { get; set; } = string.Empty;

        [Compare("Password", ErrorMessage = "Las Contraseñas no coinciden.")]
        public string ConfirmPassword { get; set; } = string.Empty;        
    }
}