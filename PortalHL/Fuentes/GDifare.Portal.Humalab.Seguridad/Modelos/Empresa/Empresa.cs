using System.ComponentModel.DataAnnotations;

namespace GDifare.Portal.Humalab.Seguridad.Modelos
{
    public class Empresa
    {
        [Display(Name = "ID")]
        public int EmpresaID { get; set;  }

        [Display(Name = "NOMBRE")]
        public string Nombre { get; set; }

        [Display(Name = "ESTADO")]
        public int EstadoID { get; set; }
        public int EmpresaSegID { get; set; }

        [Display(Name = "RUC")]
        public string Ruc { get; set; }

    }
}


