using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GDifare.Portal.Humalab.Servicio.Modelos.GestionCliente;

public class EstadosHumalabClienteResponse
{
    [Display(Name = "IdCatalogoDetalle")]
    public int IdCatalogoDetalle { get; set; }

    [Display(Name = "Nombre")]
    public string Nombre { get; set; } = string.Empty;

    [Display(Name = "Valor")]
    public string Valor { get; set; } = string.Empty;
}
