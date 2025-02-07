using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDifare.Portal.Humalab.Servicio.Modelos.Orden;
public class MedicoResultPdf
{
    public List<Medico> medico { get; set; }
}

public class Medico
{
    public string code { get; set; } = string.Empty;
    public List<Detalles> details { get; set; }
    public string message { get; set; } = string.Empty;
    public string status { get; set; } = string.Empty;
}

public class Detalles
{
    public int IdMedico { get; set; }
    public int IdLaboratorio { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Apellido { get; set; } = string.Empty;
    public string Matricula { get; set; } = string.Empty;
    public string Telefono { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Usuario { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public bool Activo { get; set; }
    public string PublicKey { get; set; } = string.Empty;

}
