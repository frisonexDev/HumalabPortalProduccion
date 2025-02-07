using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDifare.Portal.Humalab.Servicio.Modelos.PedidosCliente
{
    public class ListaPedido
    {
        public int IdPedido { get; set; }
        public string NumeroRemision { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int TotalOrdenes { get; set; }
        public int TotalMuestras { get; set; }
        public int TotalRetiradas { get; set; }
        public DateTime FechaRetiro { get; set; }
        public string EstadoPedido { get; set; }
        public int UsuarioCreacion { get; set; } 



    }
}
