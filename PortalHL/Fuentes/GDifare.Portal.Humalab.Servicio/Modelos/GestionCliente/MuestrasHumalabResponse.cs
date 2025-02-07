using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDifare.Portal.Humalab.Servicio.Modelos.GestionCliente;

public class MuestrasHumalabResponse
{
	public List<MuestrasMesActual> mesActual { get; set; } = new List<MuestrasMesActual>();
	public List<MuestrasMesAnterior> mesAnterior { get; set; } = new List<MuestrasMesAnterior>();
}

public class MuestrasMesActual
{
	public int dia_semana { get; set; }
	public int total_muestras { get; set; }
}

public class MuestrasMesAnterior
{
	public int dia_semana { get; set; }
	public int total_muestras { get; set; }
}
