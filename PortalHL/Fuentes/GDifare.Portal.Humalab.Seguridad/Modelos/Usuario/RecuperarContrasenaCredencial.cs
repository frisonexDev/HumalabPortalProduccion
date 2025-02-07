using System.ComponentModel.DataAnnotations;

namespace GDifare.Portal.Humalab.Seguridad.Modelos
{
    public class RecuperarContrasenaCredencial
    {
        [Display(Name = "CORREO ELECTRÓNICO")]
        [Required(ErrorMessage = "Correo electrónico es requerido")]
        [RegularExpression(@"^[\d\w\._\-]+@([\d\w\._\-]+\.)+[\w]+$", ErrorMessage = "Correo electrónico no es válido")]
        public string Usuario { get; set; }

        public string Clave { get; set; }
    }
}