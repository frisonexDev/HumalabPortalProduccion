using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDifare.Portal.Humalab.Servicio.Modelos.PedidosLaboratorista;

public class OrdenGalileoResponse_
{
    public int idOrden { get; set; }
    public int idLaboratorio { get; set; }
    public string nombreLaboratorio { get; set; }
    public string nombrePaciente { get; set; }
    public DateTime fechaOrden { get; set; }
    public string identificaforPaciente { get; set; }
    public bool procesoExitoso { get; set; }
    public string code { get; set; }
    public string codigoOrden { get; set; }
    public List<Muestra> muestraOrden { get; set; }
    public string mensaje { get; set; }

    public string codigo { get; set; }
    public string descripcion { get; set;}
    public string error { get; set;}
}

public class Muestra
{
    public string codigoMuestra { get; set; }
    public string numeroEtiquetas { get; set; }
    public string nombreRecipiente { get; set; }
}