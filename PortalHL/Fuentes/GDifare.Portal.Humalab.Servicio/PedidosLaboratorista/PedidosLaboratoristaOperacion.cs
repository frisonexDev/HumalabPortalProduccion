using GDifare.Portal.Humalab.Servicio.Modelos;
using GDifare.Portal.Humalab.Servicio.Modelos.Galileo;
using GDifare.Portal.Humalab.Servicio.Modelos.GestionCliente;
using GDifare.Portal.Humalab.Servicio.Modelos.OrdenGalileo;
using GDifare.Portal.Humalab.Servicio.Modelos.PedidosLaboratorista;
using GDifare.Portal.Humalab.Servicio.Modelos.PedidosOpLogistico;
using GDifare.Portal.Humalab.Servicio.Resources;
using GDifare.Portales.Comunicaciones;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using static System.Net.Mime.MediaTypeNames;
using Medico = GDifare.Portal.Humalab.Servicio.Modelos.OrdenGalileo.Medico;
using Paciente = GDifare.Portal.Humalab.Servicio.Modelos.OrdenGalileo.Paciente;

namespace GDifare.Portal.Humalab.Servicio.PedidosOpLogistico
{
    public class PedidosLaboratoristaOperacion
    {
        private readonly Communicator CommunicatorGestionPedidos;
        private readonly AppServicioMicros microInterno;
        private readonly AppServicioMicrosExternos microsExterno;
        private readonly Variables variables;
        private readonly Communicator CommunicatorGalileo;

        private string UserAvalab;
        private string PassAvalab;

        private string SeverPedidosLab;
        private int PortPedidosLab;
        private string RoutePedidosLab;

        public PedidosLaboratoristaOperacion(AppServicioMicros settingsMicros, AppServicioMicrosExternos servicioMicrosExternos, 
                                             Variables variables, string user, string pass)
        {
            //CommunicatorGestionCliente = new Communicator("localhost", 5000, "gdifare/api/humalab/pedidos/v1", token);
            this.microInterno = settingsMicros;
            this.microsExterno = servicioMicrosExternos;
            this.variables = variables;

            CommunicatorGestionPedidos = new Communicator(microInterno.ServerGestionPedidos, microInterno.PortGestionPedidos, microInterno.RouteGestionPedidos, microInterno.TokenGestionPedidos);
            CommunicatorGalileo = new Communicator(microsExterno.ServerGalileo, microsExterno.PortGalileo, microsExterno.PathServicesGalileo, microsExterno.TokenGalileo);

            UserAvalab = user;
            PassAvalab = pass;

            SeverPedidosLab = microInterno.ServerGestionPedidos;
            PortPedidosLab = microInterno.PortGestionPedidos;
            RoutePedidosLab = microInterno.RouteGestionPedidos;

        }


        //Consulta de ordenes para Laboratorista
        public BuscarPedidoLaboratoristaResponse ConsultarOrdenesLaboratorista(BuscarPedidoLaboratoristaRequest request)
        {
            try
            {
                object objMuestras;

                string metodo = "consultarordenes";
                var stringBuilder = new StringBuilder();
                stringBuilder.Append("/" + metodo + "?");
                AppendParam(stringBuilder, nameof(request.FechaDesde), request.FechaDesde.ToString("yyyy-MM-dd"));
                AppendParam(stringBuilder, nameof(request.FechaHasta), request.FechaHasta.ToString("yyyy-MM-dd"));
                AppendParam(stringBuilder, nameof(request.Operador), request.Operador!);
                AppendParam(stringBuilder, nameof(request.BuscarPorRuc), request.BuscarPorRuc == null ? null : request.BuscarPorRuc.ToString());
                AppendParam(stringBuilder, nameof(request.Cliente), request.Cliente!);
                AppendParam(stringBuilder, nameof(request.Estado), request.Estado!);
                AppendParam(stringBuilder, nameof(request.IdPedido), request.IdPedido == null ? null : request.IdPedido.ToString());
                AppendParam(stringBuilder, nameof(request.IdAsesor), request.IdAsesor == null ? null : request.IdAsesor.ToString());
                var queryString = stringBuilder.ToString();

                var url = SeverPedidosLab + ":" + PortPedidosLab + "/" + RoutePedidosLab + queryString;
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
                BuscarPedidoLaboratoristaResponse pedidos = JsonConvert.DeserializeObject<BuscarPedidoLaboratoristaResponse>(objMuestras.ToString()!)!;
                return pedidos;
            }
            catch (Exception ex)
            {
                BuscarPedidoLaboratoristaResponse ped = new BuscarPedidoLaboratoristaResponse();
                return ped;
            }
        }

        //Ver el detalle de la orden
        public VerOrdenLaboratoristaResponse VerOrdenLaboratorista(BuscarPedidoLaboratoristaRequest request)
        {
            object objMuestras;

            string metodo = "verorden";
            var stringBuilder = new StringBuilder();
            stringBuilder.Append("/" + metodo + "?");
            AppendParam(stringBuilder, nameof(request.FechaDesde), null!);
            AppendParam(stringBuilder, nameof(request.FechaHasta), null!);
            AppendParam(stringBuilder, nameof(request.Operador), null!);
            AppendParam(stringBuilder, nameof(request.BuscarPorRuc), null!);
            AppendParam(stringBuilder, nameof(request.Cliente), null!);
            AppendParam(stringBuilder, nameof(request.Estado), null!);
            AppendParam(stringBuilder, nameof(request.IdPedido), null!);
            AppendParam(stringBuilder, nameof(request.IdOrden), request.IdOrden == null ? null : request.IdOrden.ToString());
            var queryString = stringBuilder.ToString();

            var url = SeverPedidosLab + ":" + PortPedidosLab + "/" + RoutePedidosLab + queryString;
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
            VerOrdenLaboratoristaResponse ordenes = JsonConvert.DeserializeObject<VerOrdenLaboratoristaResponse>(objMuestras.ToString()!)!;
            return ordenes;
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

        //Enviar orden a galileo
        public async Task<OrdenGalileoResponse_> EnviarOrdenGalileo(string jsonRequest)
        {

            var content = new StringContent(jsonRequest, System.Text.Encoding.UTF8, "application/json");

            var metodo = "";
            
            //if (!string.IsNullOrWhiteSpace(microsExterno.TokenGalileo))
            //    metodo += string.Format("?token={0}", HttpUtility.UrlEncode(microsExterno.TokenGalileo.ToString()));

            HttpClient galileo = new HttpClient();
            string url = this.microsExterno.ServerGalileoCatalogoPruebas + ":" + this.microsExterno.PortGalileoCatalogoPruebas + Routes.PathServicioOrdenGalileo + metodo;

			string username = UserAvalab;
			string password = PassAvalab;

			var byteArray = Encoding.ASCII.GetBytes($"{username}:{password}");
			galileo.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

            //Envio peticion
			var response = await galileo.PostAsync(url, content);

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var importacionOrdenResponse = JsonConvert.DeserializeObject<OrdenGalileoResponse_>(jsonResponse);
            return importacionOrdenResponse!;

        }

        public string ConsultaOrdenGalileo(int idOrden)
        {
            try
            {
                object obj;

                string metodo = "ordenGalileo";
                string json = "";
                var stringBuilder = new StringBuilder();
                stringBuilder.Append("/" + metodo + "?");
                AppendParam(stringBuilder, "idOrden", idOrden.ToString());
                var queryString = stringBuilder.ToString();

                var url = SeverPedidosLab + ":" + PortPedidosLab + "/" + RoutePedidosLab + queryString;
                var requestNew = (HttpWebRequest)WebRequest.Create(url)!;
                requestNew.Method = "GET";

                using (var response = (HttpWebResponse)requestNew.GetResponse())
                {
                    using (var reader = new StreamReader(response.GetResponseStream()))
                    {
                        var responseText = reader.ReadToEnd();
                        obj = JsonConvert.DeserializeObject<object>(responseText)!;
                    }
                }

                //var obj = CommunicatorGestionPedidos.InvokeOperation<object>(queryString, TipoOperacion.GET);
                json = obj.ToString()!;
                return json;
            }
            catch {
                return null!;
            }
        }

        public string RecibirOrden(int idOrden, string codExterno)
        {
            try
            {
                object obj;

                string metodo = "recibirOrden";
                string json = "";
                var stringBuilder = new StringBuilder();
                stringBuilder.Append("/" + metodo + "?");
                AppendParam(stringBuilder, "idOrden", idOrden.ToString());
                AppendParam(stringBuilder, "codExterno", codExterno.ToString());
                var queryString = stringBuilder.ToString();

                var url = SeverPedidosLab + ":" + PortPedidosLab + "/" + RoutePedidosLab + queryString;
                var requestNew = (HttpWebRequest)WebRequest.Create(url)!;
                requestNew.Method = "PUT";
                requestNew.ContentType = "application/x-www-form-urlencoded";

                var data = Encoding.UTF8.GetBytes(string.Empty);

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
                        obj = JsonConvert.DeserializeObject<object>(responseText)!;
                    }
                }

                //var obj = CommunicatorGestionPedidos.InvokeOperation<object>(queryString, TipoOperacion.PUT);
                json = obj.ToString()!;

                return json;
            }
            catch
            {
                return null!;
            }
        }

        public CambiarPruebaResponse CambiarPruebaLaboratorista(CambiarPruebaRequest request)
        {
            object objResponse;

            string metodo = "cambiarprueba";
            var stringBuilder = new StringBuilder();
            stringBuilder.Append("/" + metodo + "?");
            AppendParam(stringBuilder, nameof(request.IdPrueba), request.IdPrueba.ToString());
            AppendParam(stringBuilder, nameof(request.IdMuestra), request.IdMuestra.ToString());
            AppendParam(stringBuilder, nameof(request.Rechaza), request.Rechaza.ToString());
            AppendParam(stringBuilder, nameof(request.Usuario), request.Usuario.ToString());
            AppendParam(stringBuilder, nameof(request.Observacion), request.Observacion);
            var queryString = stringBuilder.ToString();

            var url = SeverPedidosLab + ":" + PortPedidosLab + "/" + RoutePedidosLab + metodo;
            var requestNew = (HttpWebRequest)WebRequest.Create(url)!;
            requestNew.Method = "PUT";
            requestNew.ContentType = "application/json";

            var jsonNew = JsonConvert.SerializeObject(queryString);
            var data = Encoding.UTF8.GetBytes(jsonNew);

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
                    objResponse = JsonConvert.DeserializeObject<object>(responseText)!;
                }
            }

            //object objResponse = CommunicatorGestionPedidos.InvokeOperation<object>(queryString, TipoOperacion.PUT);
            CambiarPruebaResponse respuesta = JsonConvert.DeserializeObject<CambiarPruebaResponse>(objResponse.ToString()!)!;

            return respuesta;
        }

        public List<PruebasResponse> ConsultaPruebas(int idMuestra)
        {
            object objPruebas;

            string metodo = "consultapruebas";
            var stringBuilder = new StringBuilder();
            stringBuilder.Append("/" + metodo + "?");
            AppendParam(stringBuilder, "idMuestra", idMuestra.ToString());

            var queryString = stringBuilder.ToString();

            var url = SeverPedidosLab + ":" + PortPedidosLab + "/" + RoutePedidosLab + queryString;
            var requestNew = (HttpWebRequest)WebRequest.Create(url)!;
            requestNew.Method = "GET";

            using (var response = (HttpWebResponse)requestNew.GetResponse())
            {
                using (var reader = new StreamReader(response.GetResponseStream()))
                {
                    var responseText = reader.ReadToEnd();
                    objPruebas = JsonConvert.DeserializeObject<object>(responseText)!;
                }
            }

            //object objPruebas = CommunicatorGestionPedidos.InvokeOperation<object>(queryString, TipoOperacion.GET);
            List<PruebasResponse> pruebas = JsonConvert.DeserializeObject<List<PruebasResponse>>(objPruebas.ToString()!)!;
            return pruebas;
        }
    }
}
