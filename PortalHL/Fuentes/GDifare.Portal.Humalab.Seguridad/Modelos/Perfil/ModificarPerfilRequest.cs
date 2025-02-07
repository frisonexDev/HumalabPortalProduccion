using System.ComponentModel.DataAnnotations;

namespace GDifare.Portal.Humalab.Seguridad.Modelos
{
    public class ModificarPerfilRequest
    {
        [Display(Name = "ID")]
        public int PerfilID { get; set; }

        [Display(Name = "PERFIL")]
        [Required(ErrorMessage = "La nombre del perfil es requerido")]
        [RegularExpression("^[a-zA-ZáéíóúÁÉÍÓÚñÑ ]*[0-9]*$", ErrorMessage = "No se pueden ingresar caracteres especiales")]
        [MaxLength(100, ErrorMessage = "El nombre del perfil no puede ser mayor a 100 caracteres")]
        public string Perfil { get; set; }

        public int Usuario { get; set; }
    }
}
