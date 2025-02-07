using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDifare.Portal.Humalab.Servicio.Modelos.GestionCliente;

public class GraficarPedidosHumalabRequest
{
	//public int idPedido { get; set; }
	public int Ordenes { get; set; }
    public int idCliente { get; set; }
    public string numRemision { get; set; } = string.Empty;
    public string nomPrueba { get; set; } = string.Empty;
}
