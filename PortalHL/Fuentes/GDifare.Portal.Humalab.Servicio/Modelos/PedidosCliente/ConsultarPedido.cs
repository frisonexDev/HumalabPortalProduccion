using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDifare.Portal.Humalab.Servicio.Modelos.PedidosCliente
{
	public class ConsultarPedido
	{
		public int OpcionBusqueda { get; set; }
		public string DatoBusqueda { get; set; }=string.Empty;
		public int IdUsuario { get; set;}
		public string numRemision {  get; set; } = string.Empty;
		public DateTime FechaDesde { get;set; }
		public DateTime FechaHasta { get; set; }
	}
}
