using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDifare.Portal.Humalab.Servicio.Modelos;

public class Variables
{
    public int idRol { get; set; } 
    public int empresaId { get; set; }
    public string EmpresaHumalab { get; set; } = string.Empty;
    public int idRolOperador { get; set; }

    public int DescargaCatalogo { get; set; }
}
