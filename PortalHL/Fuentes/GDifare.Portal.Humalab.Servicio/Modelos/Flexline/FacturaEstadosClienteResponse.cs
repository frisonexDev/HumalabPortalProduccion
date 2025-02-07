using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDifare.Portal.Humalab.Servicio.Modelos.Flexline;

public class FacturaEstadosClienteResponse
{
    [Display(Name = "NumeroDocumento")]
    public string NumeroDocumento { get; set; } = string.Empty;

    [Display(Name = "ValorPorVencer")]
    public string ValorPorVencer { get; set; } = string.Empty;

    [Display(Name = "DiasCredito")]
    public decimal DiasCredito { get; set; }

    [Display(Name = "DiasVencidos")]
    public decimal DiasVencidos { get; set; }

    [Display(Name = "ValorVencido")]
    public decimal ValorVencido { get; set; }
}
