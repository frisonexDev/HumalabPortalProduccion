using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
namespace GDifare.Portal.HumaLab.UI.Models.Pedidos
{
    public class BuscarPedidosOpLogistico
    {
        [Display(Name = "Cliente")]
        public string? Cliente { get; set; }

        public bool BuscarPorIdentificacion { get; set; }


        [DataType(DataType.Date)]
        [Required(ErrorMessage = "La Fecha Desde es requerida")]
        [Display(Name = "Fecha Desde")]
        public DateTime FechaDesde { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "La Fecha Hasta es requerida")]
        [Display(Name = "Fecha Hasta")]
        public DateTime FechaHasta { get; set; }

        public string? Estado { get; set; }

        public int? IdOperadorLogistico { get; set; }

    }
}