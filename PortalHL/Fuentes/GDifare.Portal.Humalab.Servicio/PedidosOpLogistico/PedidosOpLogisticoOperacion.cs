using GDifare.Portal.Humalab.Servicio.Modelos;
using GDifare.Portal.Humalab.Servicio.Modelos.Facturas;
using GDifare.Portal.Humalab.Servicio.Modelos.Flexline;
using GDifare.Portal.Humalab.Servicio.Modelos.GestionCliente;
using GDifare.Portal.Humalab.Servicio.Modelos.Orden;
using GDifare.Portal.Humalab.Servicio.Modelos.PedidosOpLogistico;
using GDifare.Portal.Humalab.Servicio.Resources;
using GDifare.Portales.Comunicaciones;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using static System.Net.Mime.MediaTypeNames;

namespace GDifare.Portal.Humalab.Servicio.PedidosOpLogistico
{
    public class PedidosOpLogisticoOperacion
    {
        private readonly Communicator CommunicatorGestionPedidos;
        private readonly Communicator CommunicatorGestionPDF;
        private readonly AppServicioMicros microInterno;
        private readonly AppServicioMicrosExternos microsExterno;
        private readonly AppServicioClienteApi microCliente;
        private readonly Variables variables;

        public string SeverPedidos;
        public int PortPedidos;
        public string RoutePedidos;

        public PedidosOpLogisticoOperacion(AppServicioMicros settingsMicros, AppServicioMicrosExternos servicioMicrosExternos, AppServicioClienteApi servicioCliente, Variables variables)
        {
            this.microInterno = settingsMicros;
            this.microsExterno = servicioMicrosExternos;
            this.variables = variables;
            this.microCliente = servicioCliente;

            CommunicatorGestionPedidos = new Communicator(microInterno.ServerGestionPedidos, microInterno.PortGestionPedidos, microInterno.RouteGestionPedidos, microInterno.TokenGestionPedidos);
            CommunicatorGestionPDF = new Communicator(microCliente.ServerPDF, microCliente.PortPDF, Routes.PathServicioPDF, "");

            SeverPedidos = microInterno.ServerGestionPedidos;
            PortPedidos = microInterno.PortGestionPedidos;
            RoutePedidos = microInterno.RouteGestionPedidos;

        }

        //Ver el detalle de Pedidos del Operador Logístico
        public VerDetallePedidoOperadorResponse VerDetallePedidoOperadorLogistico(VerDetallePedidoOperadorRequest request)
        {
            object objMuestras;

            string metodo = "ver";
            var stringBuilder = new StringBuilder();
            stringBuilder.Append("/" + metodo + "?");
            AppendParam(stringBuilder, nameof(request.IdPedido), request.IdPedido.ToString());
            var queryString = stringBuilder.ToString();

            var url = SeverPedidos + ":" + PortPedidos + "/" + RoutePedidos + queryString;
            var requestNew = (HttpWebRequest)WebRequest.Create(url)!;
            requestNew.Method = "GET";

            using (var response = (HttpWebResponse)requestNew.GetResponse())
            {
                using (var reader = new StreamReader(response.GetResponseStream()))
                {
                    var responseText = reader.ReadToEnd();
                    objMuestras = JsonConvert.DeserializeObject<object>(responseText)!;
                }
            }

            //object objMuestras = CommunicatorGestionPedidos.InvokeOperation<object>(queryString, TipoOperacion.GET);
            VerDetallePedidoOperadorResponse muestras = JsonConvert.DeserializeObject<VerDetallePedidoOperadorResponse>(objMuestras.ToString()!)!;
            return muestras;
        }

        //Ver el nuevo detalle de los pedidos para recolectar
        public VerDetallePedidoOperadorResponse VerDetallePedidoOperadorLogisticoNew(VerDetallePedidoOperadorRequest request)
        {
			object objMuestras;

			string metodo = "verPedidos";
			var stringBuilder = new StringBuilder();
			stringBuilder.Append("/" + metodo + "?");
			AppendParam(stringBuilder, nameof(request.IdPedido), request.IdPedido.ToString());
			var queryString = stringBuilder.ToString();

			var url = SeverPedidos + ":" + PortPedidos + "/" + RoutePedidos + queryString;
			var requestNew = (HttpWebRequest)WebRequest.Create(url)!;
			requestNew.Method = "GET";

			using (var response = (HttpWebResponse)requestNew.GetResponse())
			{
				using (var reader = new StreamReader(response.GetResponseStream()))
				{
					var responseText = reader.ReadToEnd();
					objMuestras = JsonConvert.DeserializeObject<object>(responseText)!;
				}
			}

			//object objMuestras = CommunicatorGestionPedidos.InvokeOperation<object>(queryString, TipoOperacion.GET);
			VerDetallePedidoOperadorResponse muestras = JsonConvert.DeserializeObject<VerDetallePedidoOperadorResponse>(objMuestras.ToString()!)!;
			return muestras;
		}


		//Buscar los Pedidos del Operador Logístico
		public List<BuscarPedidoOperadorResponse> ConsultarPedidosOperadorLogistico(BuscarPedidoOperadorRequest request)
        {
            object objPedidos;

            string metodo = "buscar";
            var stringBuilder = new StringBuilder();
            stringBuilder.Append("/"+ metodo + "?");
            AppendParam(stringBuilder, nameof(request.FechaDesde), request.FechaDesde.ToString("yyyy-MM-dd"));
            AppendParam(stringBuilder, nameof(request.FechaHasta), request.FechaHasta.ToString("yyyy-MM-dd"));
            AppendParam(stringBuilder, nameof(request.Cliente), request.Cliente);
            AppendParam(stringBuilder, nameof(request.BuscarPorIdentificacion), request.BuscarPorIdentificacion == null?null:request.BuscarPorIdentificacion.ToString());
            AppendParam(stringBuilder, nameof(request.IdOperadorLogistico), request.IdOperadorLogistico==null?null: request.IdOperadorLogistico.ToString());
            AppendParam(stringBuilder, nameof(request.Estado), request.Estado);
            var queryString = stringBuilder.ToString();

            var url = SeverPedidos + ":" + PortPedidos + "/" + RoutePedidos + queryString;
            var requestNew = (HttpWebRequest)WebRequest.Create(url)!;
            requestNew.Method = "GET";

            using (var response = (HttpWebResponse)requestNew.GetResponse())
            {
                using (var reader = new StreamReader(response.GetResponseStream()))
                {
                    var responseText = reader.ReadToEnd();

                    objPedidos = JsonConvert.DeserializeObject<object>(responseText)!;
                }
            }

            //object objPedidos = CommunicatorGestionPedidos.InvokeOperation<object>(queryString, TipoOperacion.GET);
            List<BuscarPedidoOperadorResponse> pedidos = JsonConvert.DeserializeObject<List<BuscarPedidoOperadorResponse>>(objPedidos.ToString()!)!;
            return pedidos;
        }

        //Buscar pedidos cuando se recolectan el pedido pasa a recolectado total o parcial
        public string ConsultarPedidosActualizados(int idPedido, int idOperador)
        {
            object objPedidos;

            string metodo = "buscarPedidosActua";
            var stringBuilder = new StringBuilder();
            stringBuilder.Append("/" + metodo + "?");
            AppendParam(stringBuilder, nameof(idPedido), idPedido == null ? null : idPedido.ToString());
            AppendParam(stringBuilder, nameof(idOperador), idOperador == null ? null : idOperador.ToString());
            //AppendParam(stringBuilder, nameof(request.Cliente), request.Cliente);
            //AppendParam(stringBuilder, nameof(request.BuscarPorIdentificacion), request.BuscarPorIdentificacion == null ? null : request.BuscarPorIdentificacion.ToString());
            //AppendParam(stringBuilder, nameof(request.IdOperadorLogistico), request.IdOperadorLogistico == null ? null : request.IdOperadorLogistico.ToString());
            //AppendParam(stringBuilder, nameof(request.Estado), request.Estado);
            var queryString = stringBuilder.ToString();

            var url = SeverPedidos + ":" + PortPedidos + "/" + RoutePedidos + queryString;
            var request = (HttpWebRequest)WebRequest.Create(url)!;
            request.Method = "GET";

            using (var response = (HttpWebResponse)request.GetResponse())
            {
                using (var reader = new StreamReader(response.GetResponseStream()))
                {
                    var responseText = reader.ReadToEnd();
                    objPedidos = JsonConvert.DeserializeObject<object>(responseText)!;
                }
            }

            //object objPedidos = CommunicatorGestionPedidos.InvokeOperation<object>(queryString, TipoOperacion.GET);
            string pedidos = JsonConvert.DeserializeObject<string>(objPedidos.ToString()!)!;
            return pedidos;
        }

        public string RecogerMuestraOperadorLogistico(RecogerMuestraRequest request)
        {
            object objPedidos;

            string metodo = "recogermuestra";
            var stringBuilder = new StringBuilder();
            stringBuilder.Append("/" + metodo + "?");
            AppendParam(stringBuilder, nameof(request.IdMuestra), request.IdMuestra.ToString());
            AppendParam(stringBuilder, nameof(request.Recolectar), request.Recolectar.ToString());
            AppendParam(stringBuilder, nameof(request.Rechazar), request.Rechazar.ToString());
            AppendParam(stringBuilder, nameof(request.EsOperador), request.EsOperador.ToString());
            AppendParam(stringBuilder, nameof(request.IdOperador), request.IdOperador.ToString());
            AppendParam(stringBuilder, nameof(request.NombreUsuario), request.NombreUsuario);
            AppendParam(stringBuilder, nameof(request.Observacion), request.Observacion);
            var queryString = stringBuilder.ToString();

            var url = SeverPedidos + ":" + PortPedidos + "/" + RoutePedidos + queryString;
            var requestNew = (HttpWebRequest)WebRequest.Create(url)!;
            requestNew.Method = "PUT";
            requestNew.ContentType = "application/json";

            var json = JsonConvert.SerializeObject(request);
            var data = Encoding.UTF8.GetBytes(json);

            // Escribir los datos en el cuerpo de la solicitud
            using (var stream = requestNew.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            using (var response = (HttpWebResponse)requestNew.GetResponse())
            {
                using (var reader = new StreamReader(response.GetResponseStream()))
                {
                    var responseText = reader.ReadToEnd();
                    objPedidos = JsonConvert.DeserializeObject<object>(responseText)!;
                }
            }

            //object objPedidos = CommunicatorGestionPedidos.InvokeOperation<object>(queryString, TipoOperacion.PUT);
            //RecogerMuestraResponse r = JsonConvert.DeserializeObject<RecogerMuestraResponse>(objPedidos.ToString()!)!;
            return objPedidos.ToString()!;
        }

        public string RecogerPedido(RecogerPedidoRequest request)
        {
            object objPedidos;

            string metodo = "recogerpedido";
            var stringBuilder = new StringBuilder();
            stringBuilder.Append("/" + metodo + "?");
            AppendParam(stringBuilder, nameof(request.IdPedido), request.IdPedido.ToString());
            AppendParam(stringBuilder, nameof(request.IdOperador), request.IdOperador.ToString());
            AppendParam(stringBuilder, nameof(request.ObservacionOperador), request.ObservacionOperador);
            var queryString = stringBuilder.ToString();

            var url = SeverPedidos + ":" + PortPedidos + "/" + RoutePedidos + queryString;
            var requestNew = (HttpWebRequest)WebRequest.Create(url)!;
            requestNew.Method = "PUT";
            requestNew.ContentType = "application/json";

            var json = JsonConvert.SerializeObject(request);
            var data = Encoding.UTF8.GetBytes(json);

            // Escribir los datos en el cuerpo de la solicitud
            using (var stream = requestNew.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            using (var response = (HttpWebResponse)requestNew.GetResponse())
            {
                using (var reader = new StreamReader(response.GetResponseStream()))
                {
                    var responseText = reader.ReadToEnd();
                    objPedidos = JsonConvert.DeserializeObject<object>(responseText)!;
                }
            }

            //object objPedidos = CommunicatorGestionPedidos.InvokeOperation<object>(queryString, TipoOperacion.PUT);
            return objPedidos.ToString()!;
        }

        public string EntregarPedido(EntregarPedidoRequest request)
        {
            object objPedidos;

            string metodo = "entregarpedido";
            var stringBuilder = new StringBuilder();
            stringBuilder.Append("/" + metodo + "?");
            AppendParam(stringBuilder, nameof(request.Pedidos), request.Pedidos);
            AppendParam(stringBuilder, nameof(request.IdOperador), request.IdOperador.ToString());
            AppendParam(stringBuilder, nameof(request.ObservacionOperador), request.ObservacionOperador);
            var queryString = stringBuilder.ToString();

            var url = SeverPedidos + ":" + PortPedidos + "/" + RoutePedidos + queryString;
            var requestNew = (HttpWebRequest)WebRequest.Create(url)!;
            requestNew.Method = "PUT";
            requestNew.ContentType = "application/json";

            var json = JsonConvert.SerializeObject(request);
            var data = Encoding.UTF8.GetBytes(json);

            // Escribir los datos en el cuerpo de la solicitud
            using (var stream = requestNew.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            using (var response = (HttpWebResponse)requestNew.GetResponse())
            {
                using (var reader = new StreamReader(response.GetResponseStream()))
                {
                    var responseText = reader.ReadToEnd();
                    objPedidos = JsonConvert.DeserializeObject<object>(responseText)!;
                }
            }

            //object objPedidos = CommunicatorGestionPedidos.InvokeOperation<object>(queryString, TipoOperacion.PUT);
            return objPedidos.ToString()!;
        }

        //Retorna estados 
        public List<EstadosCatalogoResponse> EstadosCatalogo(string nombreMaestro)
        {
            List<EstadosCatalogoResponse> listaEstados = new List<EstadosCatalogoResponse>();
            try
            {
                var metodo = "/estadosCatalogo";

                metodo += string.Format("?nombreMaestro={0}", HttpUtility.UrlEncode(nombreMaestro));

                var url = SeverPedidos + ":" + PortPedidos + "/" + RoutePedidos + metodo;
                var request = (HttpWebRequest)WebRequest.Create(url)!;
                request.Method = "GET";

                using (var response = (HttpWebResponse)request.GetResponse())
                {
                    using (var reader = new StreamReader(response.GetResponseStream()))
                    {
                        var responseText = reader.ReadToEnd();

                        listaEstados = JsonConvert.DeserializeObject<List<EstadosCatalogoResponse>>(responseText)!;
                    }
                }

                //listaEstados = CommunicatorGestionPedidos.InvokeOperation<List<EstadosCatalogoResponse>>(metodo, TipoOperacion.GET);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return listaEstados;
        }

        //Genera PDF
        public string GenerarPdfPedido(VerDetallePedidoOperadorResponse verMuestras)
        {
            string base64string = String.Empty;
            try
            {
                string metodo = "pdfRecolectarPedido";

                var url = SeverPedidos + ":" + PortPedidos + "/" + RoutePedidos + metodo;
                var requestNew = (HttpWebRequest)WebRequest.Create(url)!;
                requestNew.Method = "PUT";
                requestNew.ContentType = "application/json";

                var json = JsonConvert.SerializeObject(verMuestras);
                var data = Encoding.UTF8.GetBytes(json);

                // Escribir los datos en el cuerpo de la solicitud
                using (var stream = requestNew.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }

                using (var response = (HttpWebResponse)requestNew.GetResponse())
                {
                    using (var reader = new StreamReader(response.GetResponseStream()))
                    {
                        var responseText = reader.ReadToEnd();
                        base64string = JsonConvert.DeserializeObject<string>(responseText)!;
                    }
                }

                //base64string = CommunicatorGestionPDF.InvokeOperation<string, VerDetallePedidoOperadorResponse>(metodo, TipoOperacion.POST, verMuestras);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return base64string;
        }

        private static void AppendParam(StringBuilder stringBuilder, string key, string value)
        {
            if (value == null)
                return;
            if (stringBuilder.Length > stringBuilder.ToString().IndexOf('?') + 1)
                stringBuilder.Append("&");

            stringBuilder.Append(key);
            stringBuilder.Append("=");
            stringBuilder.Append(Uri.EscapeDataString(value));
        }

        public string VerificarActPed(int idPedido)
        {
            string actualizado = "";
            var metodo = "/pedidoAct";

            metodo += string.Format("?idPedido={0}", HttpUtility.UrlEncode(idPedido.ToString()));

            var url = SeverPedidos + ":" + PortPedidos + "/" + RoutePedidos + metodo;
            var request = (HttpWebRequest)WebRequest.Create(url)!;
            request.Method = "GET";

            using (var response = (HttpWebResponse)request.GetResponse())
            {
                using (var reader = new StreamReader(response.GetResponseStream()))
                {
                    var responseText = reader.ReadToEnd();
                    //actualizado = JsonConvert.DeserializeObject<string>(responseText)!;
                    actualizado = responseText;
                }
            }

            //actualizado = CommunicatorGestionPedidos.InvokeOperation<string>(metodo, TipoOperacion.GET);
            return actualizado;
        }

        public async Task<List<AsesorFlexlineResponse>> BuscarAsesoresFlexline()
        {
            var metodo = "asesores";

            using (var clienteF = new HttpClient())
            {
                clienteF.DefaultRequestHeaders.Add("Authorization", $"Bearer {microsExterno.TokenFlexline}");

                var resultado = await clienteF.GetAsync("http://" + microsExterno.ServerFlexLine + ":" +
                    microsExterno.PortFlexLine + microsExterno.PathServicesAsesorFlexline + "/" + metodo);

                if (!resultado.IsSuccessStatusCode)
                    throw new ArgumentException("Problemas con el servicio de Flexline.");

                var resultadoJson = await resultado.Content.ReadAsStringAsync();
                var respuesta = JsonConvert.DeserializeObject<List<AsesorFlexlineResponse>>(resultadoJson);

                return respuesta!;
            }
        }
    }
}
