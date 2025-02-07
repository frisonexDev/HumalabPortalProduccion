using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDifare.Portal.Humalab.Servicio.Modelos;

public class AppServicioMicros
{
    public string ServerGestionCliente { get; set; } = string.Empty;
    public int PortGestionCliente { get; set; }
    public string RouteGestionCliente { get; set; } = string.Empty;
    public string TokenGestionCliente { get; set; } = string.Empty;
    public string ServerCorreo { get; set; } = string.Empty;
    public int PortCorreo { get; set; }
    public string RouteCorreo { get; set; } = string.Empty;
    public string TokenCorreo { get; set; } = string.Empty;

    public string ServerGestionPedidos { get; set; } = string.Empty;
    public int PortGestionPedidos { get; set; }
    public string RouteGestionPedidos { get; set; } = string.Empty;
    public string TokenGestionPedidos { get; set; } = string.Empty;

    public string ServerGestionPDF { get; set; } = string.Empty;
    public int PortGestionPDF { get; set; }
    public string RouteGestionPDF { get; set; } = string.Empty;
    public string TokenGestionPDF { get; set; } = string.Empty;

    public string ServerAdministracion { get; set; } = string.Empty;
    public int PortAdministracion { get; set; }
    public string RouteAdministracion { get; set; } = string.Empty;

    public string RouteFlexCartera { get; set; } = string.Empty;
}
