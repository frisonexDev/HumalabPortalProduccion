using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDifare.Portal.Humalab.Servicio.Modelos.OrdenGalileo
{
    public class OrdenInfo
    {
        public string Cliente { get; set; }
        public DateTime fechaIngreso { get; set; }
        public string comentario { get; set; }
        public string Identificador { get; set; }
        public string NombrePaciente { get; set; }
        public string ApellidoPaciente { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Genero { get; set; }
        public string CorreoElectronico { get; set; }
        public string nombreCampoAdicional { get; set; }
        public string valor { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Matricula { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string diagnostico { get; set; }
        public string CodigoExamen { get; set; }
        public string NombreExamenPerfil { get; set; }
    }

}
