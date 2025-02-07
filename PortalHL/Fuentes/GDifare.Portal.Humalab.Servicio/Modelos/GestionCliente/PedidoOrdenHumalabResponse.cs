using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDifare.Portal.Humalab.Servicio.Modelos.GestionCliente;

public class PedidoOrdenHumalabResponse
{
	public int totalOrdenes { get; set; }
	public List<DiagnosticoPedido> Diagnostico { get; set; } = new List<DiagnosticoPedido>();
}

public class DiagnosticoPedido
{
	public string Nombre { get; set; } = string.Empty;
}