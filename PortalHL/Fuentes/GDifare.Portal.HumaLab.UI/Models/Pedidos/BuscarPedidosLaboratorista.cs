using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
namespace GDifare.Portal.HumaLab.UI.Models.Pedidos
{
    public class BuscarPedidosLaboratorista
    {

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "La Fecha Desde es requerida")]
        [Display(Name = "Fecha Desde")]
        public DateTime FechaDesde { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "La Fecha Hasta es requerida")]
        [Display(Name = "Fecha Hasta")]
        public DateTime FechaHasta { get; set; }

        [Display(Name = "Operador")]
        public string? UsuarioOperador { get; set; }
        public bool BuscarPorRuc { get; set; }

        public string? Cliente { get; set; }

        public string? EstadoPedido { get; set; }

        public int? IdPedido { get; set; }

        public int? IdOrden { get; set; }
        public int? IdAsesor {  get; set; }

    }
}