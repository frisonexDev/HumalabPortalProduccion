using System.ComponentModel.DataAnnotations;


namespace GDifare.Portal.Humalab.Seguridad.Modelos
{
    public class ObjUsuario
    {
        [Display(Name = "ID")]
        public int UsuarioID { get; set; }

        [Display(Name = "EMPRESA")]
        public int EmpresaID { get; set; }

        [Display(Name = "ROL")]
        public int RolID { get; set; }

        [Display(Name = "USUARIO")]
        public string Usuario{ get; set; }

        [Display(Name = "CLAVE")]
        public string Clave { get; set; }

        [Display(Name = "IDENTIFICACIÓN")]
        public string Identificacion { get; set; }

        [Display(Name = "NOMBRE")]
        public string Nombre { get; set; }

        [Display(Name = " APELLIDO")]
        public string PrimerApellido { get; set; }

        [Display(Name = "")]
        public string SegundoApellido { get; set; }

        [Display(Name = "CORREO")]
        public string Correo { get; set; }

        public int EstadoID { get; set; }

        [Display(Name = "NOMBRE COMPLETO")]
        public string NombreCompleto { get { return Nombre + " " + PrimerApellido; } }

        public string Firma { get; set; }

        public string Foto { get; set; }

        public string EstadoCartera { get; set; }
        public string EstadoColor { get; set; }
        public string Color { get; set; }
        public string BloqueoCartera { get; set; }

        public string perfil { get; set; }
        public string origen { get; set; }



	}
}
