using GDifare.Portal.Humalab.Servicio.Modelos;
using GDifare.Portal.Humalab.Servicio.Modelos.CatalogoPruebas;
using GDifare.Portal.Humalab.Servicio.Modelos.Orden;
using GDifare.Portales.Comunicaciones;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GDifare.Portal.Humalab.Servicio.CatalogoPrueba
{
    public class CatalogoDetalleOperacion
    {
        private readonly Communicator CommunicatorGestionOrdenes;

        private string ServerOrden;
        private int PortOrden;
        private string RouteCliente;

        public CatalogoDetalleOperacion(AppServicioClienteApi configuracion)
        {
            CommunicatorGestionOrdenes = new Communicator(configuracion.Server, configuracion.Port, configuracion.RouteCliente, configuracion.Token);
			ServerOrden = configuracion.Server;
			PortOrden = configuracion.Port;
			RouteCliente = configuracion.RouteCliente;
		}


        public List<CatalogoDetalle> ListarEstados(string NombreEstado)
        {
            List<CatalogoDetalle> lista = new List<CatalogoDetalle> ();
            object objCatalogo;

			try
            {
                var stringBuilder = new StringBuilder();
                stringBuilder.Append("/listarestados?");
                stringBuilder.Append("NombreEstado=" + NombreEstado);
                var queryString = stringBuilder.ToString();

				var url = ServerOrden + ":" + PortOrden + "/" + RouteCliente + queryString;
				var request = (HttpWebRequest)WebRequest.Create(url)!;
				request.Method = "GET";

				using (var response = (HttpWebResponse)request.GetResponse())
				{
					using (var reader = new StreamReader(response.GetResponseStream()))
					{
						var responseText = reader.ReadToEnd();
						objCatalogo = JsonConvert.DeserializeObject<object>(responseText)!;
					}
				}

				//object objCatalogo = CommunicatorGestionOrdenes.InvokeOperation<object>(queryString, TipoOperacion.GET);
                lista = JsonConvert.DeserializeObject<List<CatalogoDetalle>>(objCatalogo.ToString()!)!;
                return lista;
            }
            catch (Exception)
            {
                return lista;
            }
        }
    }
}
