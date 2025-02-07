using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDifare.Portal.Humalab.Servicio.Modelos.Orden;
public class CabecOrdenResul
{
    public int IdOrden { get; set; }
    public int IdLaboratorio { get; set; }
    public string NombreLaboratorio { get; set; } = string.Empty;
    public int IdSede { get; set; }
    public string NombreSede { get; set; } = string.Empty;
    public int IdDiagnostico { get; set; }
    public string DescripcionDiagnostico { get; set; } = string.Empty;
    public int IdPaciente { get; set; }
    public int IdMedico { get; set; }
    public string PrefijoOrden { get; set; } = string.Empty;
    public string CodigoOrden { get; set; } = string.Empty;
    public int NumeroOrden { get; set; }
    public int IdUsuario { get; set; }
    public int IdEstadoOrden { get; set; }
    public string message { get; set; } = string.Empty;
    public string status { get; set; } = string.Empty;
}