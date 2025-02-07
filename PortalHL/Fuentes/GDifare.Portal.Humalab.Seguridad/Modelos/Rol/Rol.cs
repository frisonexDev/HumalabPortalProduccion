using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GDifare.Portal.Humalab.Seguridad.Modelos
{
    public class Rol
    {
        [Display(Name = "ID")]
        public int RolID { get; set; }

        [Display(Name = "ROL")]
        [Required(ErrorMessage = "La nombre del rol es requerido")]
        [RegularExpression("^[a-zA-ZñÑáéíóúÁÉÍÓÚ ]*[0-9]*$", ErrorMessage = "No se pueden ingresar caracteres especiales")]
        [MaxLength(50, ErrorMessage = "El nombre del rol no puede ser mayor a 50 caracteres")]
        public string RolDescripcion { get; set; }
        public int EstadoID { get; set; }
        public List<RolPerfil> RolPerfiles { get; set; }
    }
}
