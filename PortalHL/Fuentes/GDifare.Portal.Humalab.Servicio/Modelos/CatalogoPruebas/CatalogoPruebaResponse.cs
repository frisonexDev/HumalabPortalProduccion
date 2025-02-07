using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDifare.Portal.Humalab.Servicio.Modelos.CatalogoPruebas
{
    public class CatalogoPruebaResponse
    {
        public string Id { get; set; } = string.Empty;
        public string Prueba { get; set; } = string.Empty;
        public string Metodología { get; set; } = string.Empty;
        public string Muestra { get; set; } = string.Empty;
        public string Muestraalterna  { get; set; } = string.Empty;
        public string Estabilidadmuestra  { get; set; } = string.Empty;
        public string Precio { get; set; } = string.Empty;
        public string Entregaresultado  { get; set; } = string.Empty;
        public string Diasdeproceso { get; set; } = string.Empty;
        public string Fichatecnica  { get; set; } = string.Empty;
    }
}
