using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDifare.Portal.Humalab.Servicio.Modelos.OrdenGalileo
{
    public class PacienteCampoAdicional
    {
        public string nombreCampo { get; set; }
        public string valor { get; set; }
    }

    public class Paciente
    {
        public string Identificador { get; set; }
        public string NombrePaciente { get; set; }
        public string ApellidoPaciente { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Genero { get; set; }
        public string CorreoElectronico { get; set; }
        public List<PacienteCampoAdicional> CamposAdicional { get; set; }
    }

    public class Medico
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Matricula { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
    }

    public class DetalleOrden
    {
        public string NombreExamenPerfil { get; set; }
        public string CodigoExamen { get; set; }
    }

    public class InformacionOrden
    {
        public string nombreCampo { get; set; }
        public string valor { get; set; }
    }

    public class TomaMuestra
    {
        public bool tomaRemota { get; set; }
        public string direccionToma { get; set; }
        public bool activo { get; set; }
    }

    public class InfoOrden
    {
        public int idLaboratorio { get; set; }
        public int idSede { get; set; }
        public string Laboratorio { get; set; }
        public string Cliente { get; set; }
        public string CieDiagnostico { get; set; }
        public string IdSistemaExterno { get; set; }
        public Paciente Paciente { get; set; }
        public Medico Medico { get; set; }
        public string prioridadOrden { get; set; }
        public DateTime fechaIngreso { get; set; }
        public string comentario { get; set; }
        public string codigoExterno { get; set; }
        public string usuario { get; set; }
        public List<DetalleOrden> detalleOrden { get; set; }
        public List<InformacionOrden> informacionOrden { get; set; }
        public TomaMuestra tomaMuestra { get; set; }
        public bool activo { get; set; }
    }

}
