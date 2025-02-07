using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDifare.Portal.Humalab.Servicio.Modelos.GestionCliente;

public class ClienteDirecHuma
{
	public string Direccion { get; set; } = string.Empty;
	public string Provincia { get; set; } = string.Empty;
	public string Ciudad { get; set; } = string.Empty;
	public string Latitud { get; set; } = string.Empty;
	public string Longitud { get; set; } = string.Empty;
	public string Codigo { get; set; } = string.Empty;
}
