using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDifare.Portal.Humalab.Servicio.Modelos.GestionCliente;

public class ModificarClienteRequest
{
    public int idCliente { get; set; }
	public string Cliente { get; set; } = string.Empty;
    public int IdEstado { get; set; } = 0;
    public string FechaVigencia { get; set; } = string.Empty;
    public string Observacion { get; set; } = string.Empty;
    public int UsuarioModificacion { get; set; } = 0;
}
