using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDifare.Portal.Humalab.Servicio.Modelos.Flexline;

public class ClienteFlexlineResponse
{
	public string RUC { get; set; } = string.Empty;
	public string CtaCte { get; set; } = string.Empty;
	public string Nombre { get; set; } = string.Empty;
	public string Email { get; set; } = string.Empty;
	public string Direccion { get; set; } = string.Empty;
	public string Telefono { get; set; } = string.Empty;
	public string Region { get; set; } = string.Empty;
	public string Provincia { get; set; } = string.Empty;
	public string Ciudad { get; set; } = string.Empty;
	public string CodAsesor { get; set; } = string.Empty;
	public string Usuario { get; set;} = string.Empty;
	public int? IdAsesor { get; set; }
}
