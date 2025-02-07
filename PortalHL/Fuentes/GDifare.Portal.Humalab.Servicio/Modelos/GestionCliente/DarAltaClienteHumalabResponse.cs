using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDifare.Portal.Humalab.Servicio.Modelos.GestionCliente;

public class DarAltaClienteHumalabResponse
{
	public bool procesoExitoso { get; set; }
	public string codigoRespuesta { get; set; } = string.Empty;
	public string mensajeRespuesta { get; set; } = string.Empty;
}
