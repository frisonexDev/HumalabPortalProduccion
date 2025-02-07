using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDifare.Portal.Humalab.Servicio.Modelos.OperacionesCliente;

public class ClienteFlexline
{
    public string? Ruc { get; set; }
    public string? Nombre { get; set; }
    public string? Email { get; set; }
    public string? Direccion { get; set; }
    public string? Telefono { get; set; }
    public string? Region { get; set; }
    public string Provincia { get; set; } = string.Empty;
    public string Ciudad { get; set; } = string.Empty;
    public bool EstadoCartera { get; set; } = false;
}
