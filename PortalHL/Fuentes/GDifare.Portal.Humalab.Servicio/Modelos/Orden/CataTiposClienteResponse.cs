using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDifare.Portal.Humalab.Servicio.Modelos.Orden;

public class CataTiposClienteResponse
{
    public int IdCatalogo { get; set; }
    public string NombreCatalogo { get; set; } = string.Empty;
    public string ValorCatalogo { get; set; } = string.Empty;
}
