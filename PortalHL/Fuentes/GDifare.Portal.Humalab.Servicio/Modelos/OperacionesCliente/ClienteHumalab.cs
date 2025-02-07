using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDifare.Portal.Humalab.Servicio.Modelos.OperacionesCliente;

public class ClienteHumalab
{
    public string? Estadocliente { get; set; }
    public string? Fechasuspension { get; set; }
    public string Fecharegistro { get; set; } = string.Empty;
    public string Ruc { get; set; } = string.Empty;
}
