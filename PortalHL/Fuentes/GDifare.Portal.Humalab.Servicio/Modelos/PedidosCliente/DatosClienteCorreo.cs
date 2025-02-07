using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDifare.Portal.Humalab.Servicio.Modelos.PedidosCliente;

public class DatosClienteCorreo
{
	public string Cliente { get; set; }	= string.Empty;
	public string Telefono { get; set; } = string.Empty;
	public string DireccionCliente { get; set; } = string.Empty;
	public string Correo { get; set; } = string.Empty;
	public string TotalMuestras { get; set; } = string.Empty;
    public string numRemision { get; set; } = string.Empty;
}
