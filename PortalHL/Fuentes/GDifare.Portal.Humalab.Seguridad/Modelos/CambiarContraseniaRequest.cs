
using System.ComponentModel.DataAnnotations;

namespace GDifare.Portal.Humalab.Seguridad.Modelos
{
    public class CambiarContraseniaRequest
    {
        [Display(Name = "USUARIO")]
        [Required(ErrorMessage = "Usuario es requerido")]
        public string Usuario { get; set; }

        [Display(Name = "CONTRASEÑA")]
        [Required(ErrorMessage = "Contraseña es requerida")]
        public string Clave { get; set; }

        [Display(Name = "NUEVA CONTRASEÑA")]
        [Required(ErrorMessage = "Contraseña es requerida")]
        public string NuevaClave { get; set; }

        [Display(Name = "REPETIR CONTRASEÑA")]
        [Required(ErrorMessage = "Contraseña es requerida")]
        public string ConfirmaNuevaClave { get; set; }
    }
}