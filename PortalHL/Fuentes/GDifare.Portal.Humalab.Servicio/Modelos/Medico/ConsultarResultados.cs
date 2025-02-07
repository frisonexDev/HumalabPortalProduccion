using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDifare.Portal.Humalab.Servicio.Modelos.Medico;

public class ConsultarResultados
{
    public DateTime FechaInicio { get; set; }
    public DateTime FechaFin {  get; set; }
    public int OpcionBusqueda { get; set; }
    public string DatoBusqueda { get; set; } = string.Empty;
    public int opcionEstado { get; set; }
    public string CodigoBarra {  get; set; } = string.Empty;
    public int idLaboratorio {  get; set; }
    public string Sedes { get; set; } = string.Empty;
}
