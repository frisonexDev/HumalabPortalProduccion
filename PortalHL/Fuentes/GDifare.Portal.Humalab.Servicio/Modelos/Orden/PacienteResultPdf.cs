using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDifare.Portal.Humalab.Servicio.Modelos.Orden;
public class PacienteResultPdf
{
    public string Identificador { get; set; } = string.Empty;
    public string NombrePaciente { get; set; } = string.Empty;
    public string ApellidoPaciente { get; set; } = string.Empty;
    public string FechaNacimiento { get; set; } = string.Empty;
    public string Genero { get; set; } = string.Empty;
    public string CorreoElectronico { get; set; } = string.Empty;
    public string Contrasena { get; set; } = string.Empty;
    public string PublicKey { get; set; } = string.Empty;
    public bool EnvioCorreo { get; set; }
    public bool AccesoPortal { get; set; }
    public bool Activo { get; set; }
    public List<CampoAdicional> CampoAdicional { get; set; }
    public int IdPaciente { get; set; }
    public int IdLaboratorio { get; set; }
    public string code { get; set; } = string.Empty;
    public string mensaje { get; set; } = string.Empty;
}

public class CampoAdicional
{
    public int IdInformacionPaciente { get; set; }
    public int IdPaciente { get; set; }
    public int IdCampoPaciente { get; set; }
    public string NombreCampo { get; set; } = string.Empty;
    public string Valor { get; set; } = string.Empty;
}
