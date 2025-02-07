using System.ComponentModel.DataAnnotations;

namespace GDifare.Portal.Humalab.Seguridad.Modelos
{
    public class CredencialesRequest
    {
        [Display(Name = "USUARIO")]
        [Required(ErrorMessage = "Usuario es requerido")]
        public string Usuario { get; set; }

        [Display(Name = "CONTRASEÑA")]
        [Required(ErrorMessage = "Contraseña es requerida")]
        public string Clave { get; set; }

        // =======================================
        // agregado 22/05/2022 CAPTCHA
        // =======================================

        //[Display(Name = "CAPTCHA")]
        //public string Captcha { get; set; }

        //[Display(Name = "INGRESE CAPTCHA")]
        //[Required(ErrorMessage = "Captcha es requerida")]
        //public string CaptchaTextBox { get; set; }

        // =======================================

    }
}