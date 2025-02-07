using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDifare.Portal.Humalab.Servicio.Modelos.PedidosCliente;

public class PedidoCorreo
{    
    public string operador { get; set; } = string.Empty;
    public string clienteNombre { get; set; } = string.Empty;
	public string TotalMuestras { get; set; } = string.Empty;
	public string direccion { get; set; } = string.Empty;
    public string telefonoCliente { get; set; } = string.Empty;
    public string correoCliente { get; set; } = string.Empty;
    public string numRemision { get; set; } = string.Empty;
    public string correoOperador { get; set; } = string.Empty;
}
