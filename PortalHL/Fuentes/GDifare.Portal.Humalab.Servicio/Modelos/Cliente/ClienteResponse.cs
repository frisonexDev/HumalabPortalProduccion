using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDifare.Portal.Humalab.Servicio.Modelos.Cliente
{
    public class ClienteResponse
    {
      
        public int idUsuario { get; set; }

     
        public int idGalileo { get; set; }

      
        public string Usuario { get; set; } = string.Empty;

       
        public int IdRol { get; set; }


       
        public string Estado { get; set; } = string.Empty;


      
        public string Identificacion { get; set; } = string.Empty;
    }
}
