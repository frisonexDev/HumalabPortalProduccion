using System.ComponentModel.DataAnnotations;

namespace GDifare.Portal.Humalab.Seguridad.Modelos
{
    public class ModificarEmpresaRequest
    {
        [Display(Name = "ID")]
        public int EmpresaID { get; set; }

        [Display(Name = "EMPRESA")]
        [Required(ErrorMessage ="El nombre de la empresa es requerido")]
        [RegularExpression(@"^[a-zA-Z0-9áéíóúÁÉÍÓÚñÑ ]*$", ErrorMessage = "No se pueden ingresar caracteres especiales")]
        [MaxLength(50, ErrorMessage = "El nombre de la empresa no puede ser mayor a 50 caracteres")]
        public string Empresa { get; set; }

        [Display(Name = "ESTADO")]
        public int Estado { get; set; } = 1;

        [Display(Name = "ESTADO")]
        public bool BEstado 
        {
            get { return Estado == 1; }
        }

        [Display(Name = "RUC")]
        [Required(ErrorMessage = "El ruc de la empresa es requerido")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "No se pueden ingresar caracteres alfabéticos ni especiales")]
        [MaxLength(13, ErrorMessage = "El ruc no puede ser mayor a 13 digitos")]
        public string Ruc { get; set; }
    }
}


