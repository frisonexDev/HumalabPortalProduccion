using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDifare.Portal.Humalab.Servicio.Modelos.PedidosCliente
{
	public class Pedido
	{
		public string NumeroRemision { get; set; }
		public int IdPedido { get; set; }
		public int IdOperador { get; set; }
		public string UsuarioOperador { get; set; }
		public List<OrdenP> DatosOrden { get; set; }
		public string Observacion { get; set; } = string.Empty;
		public int UsuarioCreacion { get; set; }
		public DateTime FechaCreacion { get; set; } = DateTime.Now;
		public int IdClientePedido { get; set; }

	}


	public class OrdenP
	{
		public int IdOrden { get; set; }

	}
}
