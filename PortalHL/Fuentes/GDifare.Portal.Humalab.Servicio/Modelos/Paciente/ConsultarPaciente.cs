using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDifare.Portal.Humalab.Servicio.Modelos.Paciente
{
    public class ConsultarPaciente
    {
        public string Identificacion { get; set; }
        public int UsuarioCreacion { get; set; }
        public DateTime FechaCreacion { get; set; } = DateTime.Now;


    }
}
