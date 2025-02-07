using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDifare.Portal.Humalab.Servicio.Modelos.GestionCliente;

public class ClienteHumalabRequest
{
    public string RucNombre { get; set; } = string.Empty;
    public int idEstado { get; set; }
    public int Offset { get; set; }
    public int Limit { get; set; }
}
