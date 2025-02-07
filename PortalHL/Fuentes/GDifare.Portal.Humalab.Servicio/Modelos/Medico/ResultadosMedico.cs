using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDifare.Portal.Humalab.Servicio.Modelos.Medico;

public class ResultadosMedico
{
    public int idResultados { get; set; }
    public string Identificacion { get; set; } = string.Empty;
    public string NumeroOrden {  get; set; } = string.Empty;
    public string Estado { get; set; } = string.Empty;
    public string Genero {  get; set; } = string.Empty;
    public string FechaIngreso {  get; set; } = string.Empty;
    public string NombrePaciente { get; set; } = string.Empty;
    public string IdSede { get; set; } = string.Empty;
    public string NombreSede { get; set; } = string.Empty;
}
