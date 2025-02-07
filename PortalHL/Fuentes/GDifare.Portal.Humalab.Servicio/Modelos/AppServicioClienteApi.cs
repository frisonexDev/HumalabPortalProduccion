using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDifare.Portal.Humalab.Servicio.Modelos
{
    public class AppServicioClienteApi
    {
        public string Server { get; set; } = string.Empty;
        public int Port { get; set; }
        public string RouteCliente { get; set; } = string.Empty;
        public string RoutePaciente { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;

        public string ServerFactura { get; set; } = string.Empty;
        public int PortFactura { get; set; }
        public string ServerPDF { get; set; } = string.Empty;
        public int PortPDF { get; set; }

        public string RouteClientePedido { get; set; } = string.Empty;

        //GeneradorPdf
        public string ServerGenPdf { get; set; } = string.Empty;
        public int PortGenPdf { get; set;}

	}
}
