using System.Collections.Generic;

namespace GDifare.Portal.Humalab.Seguridad.Modelos
{
    public class CrearUsuarioRequest
    {
        public int UsuarioID { get; set; }
        public int EmpresaID { get; set; }
        public string Usuario { get; set; }
        public int RolID { get; set; }
        public string Clave { get; set; }
        public string Identificacion { get; set; }
        public string Nombre { get; set; }
        public string PrimerApellido { get; set; }
        public string SegundoApellido { get; set; }
        public string Correo { get; set; }
        public int EstadoID  { get; set; }
        public string Foto { get; set; }
        public string Firma { get; set; }

        // agregado 03/04/2022
        public IList<Rol> Roles { get; set; } = new List<Rol>();
    }
}
