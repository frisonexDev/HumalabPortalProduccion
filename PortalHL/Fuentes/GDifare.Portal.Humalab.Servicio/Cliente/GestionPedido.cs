using GDifare.Portal.Humalab.Seguridad.Modelos;
using GDifare.Portal.Humalab.Servicio.Modelos;
using GDifare.Portal.Humalab.Servicio.Modelos.Flexline;
using GDifare.Portal.Humalab.Servicio.Modelos.Galileo;
using GDifare.Portal.Humalab.Servicio.Modelos.GestionCliente;
using GDifare.Portal.Humalab.Servicio.Modelos.Orden;
using GDifare.Portal.Humalab.Servicio.Modelos.PedidosCliente;
using GDifare.Portal.Humalab.Servicio.Resources;
using GDifare.Portal.Humalab.Servicio.Utils;
using GDifare.Portales.Comunicaciones;
using HtmlRendererCore.Adapters;
using Newtonsoft.Json;
using PdfSharpCore.Pdf.Content.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace GDifare.Portal.Humalab.Servicio.Cliente
{
    public class GestionPedido
    {

        private readonly Communicator CommunicatorGestionPedido;
        private readonly Communicator CommunicatorCorreo;
		private readonly AppServicioMicrosExternos _servicioMicrosExternos;
		private readonly Variables variables;

        private string ServerClientePedido;
        private int PortClientePedido;
        private string RouteClientePedido;

        private string ServerCorreo;
        private int PortCorreo;
        private string RouteCorreo;

		private string ServerAvalab;
		private int portAvabalab;
		private string RouteAvalab;

        private string UserAvalab;
		private string PassAvalab;

		private string ServerFlex;
		private int PortFlexl;
		private string PathFlex;
		private string TokenFlex;

		public GestionPedido(AppServicioClienteApi configuracion, AppServicioMicros _appServicioMicros, 
							 Variables val, AppServicioMicrosExternos configurationApisExt,
                             string user, string pass)
        {
			this.variables = val;
			this._servicioMicrosExternos = configurationApisExt;

			CommunicatorGestionPedido = new Communicator(configuracion.Server, configuracion.Port, configuracion.RouteCliente, configuracion.Token);
            CommunicatorCorreo = new Communicator(_appServicioMicros.ServerCorreo, _appServicioMicros.PortCorreo, _appServicioMicros.RouteCorreo, _appServicioMicros.TokenCorreo);

			ServerClientePedido = configuracion.Server;
			PortClientePedido = configuracion.Port;
			RouteClientePedido = configuracion.RouteClientePedido;

            ServerCorreo = _appServicioMicros.ServerCorreo;
            PortCorreo = _appServicioMicros.PortCorreo;
            RouteCorreo = _appServicioMicros.RouteCorreo;

			UserAvalab = user;
            PassAvalab = pass;

			ServerAvalab = configurationApisExt.ServerGalileo;
			portAvabalab = configurationApisExt.PortGalileo;
			RouteAvalab = configurationApisExt.PathServicesGalileo;

			ServerFlex = configurationApisExt.ServerFlexLine;
			PortFlexl = configurationApisExt.PortFlexLine;
			PathFlex = configurationApisExt.PathServicesAsesorFlexline;
			TokenFlex = configurationApisExt.TokenFlexline;

		}


        public int ConsultarNPedido(ConsultarPedido Valor)
		{
			try
			{
                object objPedidos;

				var stringBuilder = new StringBuilder();
				stringBuilder.Append("/consultarnumeropedido?");
				stringBuilder.Append("IdUsuario=" + Valor.IdUsuario);
				var queryString = stringBuilder.ToString();

				var url = ServerClientePedido + ":" + PortClientePedido + "/" + RouteClientePedido + queryString;
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

				//object objPedidos = CommunicatorGestionPedido.InvokeOperation<object>(queryString, TipoOperacion.GET);
				int nPedido = JsonConvert.DeserializeObject<int>(objPedidos.ToString()!)!;
				return nPedido + Numeros.Uno;
			}
			catch (Exception)
			{
				return Transaccion.Error;
			}
		}

        public string ListaDePedidos(ConsultarPedido Buscar)
        {
            string lista = string.Empty;
            string FechaIniciall = Buscar.FechaDesde.GetDateTimeFormats('d')[0].Replace("/", "d");
            string FechaFinall = Buscar.FechaHasta.GetDateTimeFormats('d')[0].Replace("/", "d");

            string FechaInicial = Buscar.FechaDesde.ToString("dd'\\d'MM'\\d'yyyy");
            string FechaFinal = Buscar.FechaHasta.ToString("dd'\\d'MM'\\d'yyyy");
			object objPedidos;

            try
            {
                var stringBuilder = new StringBuilder();
                stringBuilder.Append("/listarpedidos?");
                stringBuilder.Append("OpcionBusqueda=" + Buscar.OpcionBusqueda+"&");

				if(Buscar.DatoBusqueda != null && Buscar.DatoBusqueda != "")
					stringBuilder.Append("DatoBusqueda=" + Buscar.DatoBusqueda + "&");

				stringBuilder.Append("IdUsuario=" + Buscar.IdUsuario + "&");
                stringBuilder.Append("FechaDesde=" + FechaInicial + "&");
                stringBuilder.Append("FechaHasta=" + FechaFinal + "&");

				if(Buscar.numRemision != null && Buscar.numRemision != "")
					stringBuilder.Append("numRemision=" + Buscar.numRemision);

				var queryString = stringBuilder.ToString();

                var url = ServerClientePedido + ":" + PortClientePedido + "/" + RouteClientePedido + queryString;
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

                //object objPedidos = CommunicatorGestionPedido.InvokeOperation<object>(queryString, TipoOperacion.GET);
                lista = JsonConvert.SerializeObject(objPedidos);
                return lista;
            }
            catch (Exception)
            {
                return lista;
            }
        }


        public Operador ConsultarOperador(ConsultarPedido Valor)
		{
			Operador operador = new Operador();
            object objPedidos;
			try
			{
				var stringBuilder = new StringBuilder();
				stringBuilder.Append("/consultaroperador?");
				stringBuilder.Append("IdUsuario=" + Valor.IdUsuario);
				var queryString = stringBuilder.ToString();

				var url = ServerClientePedido + ":" + PortClientePedido + "/" + RouteClientePedido + queryString;
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

				//object objPedidos = CommunicatorGestionPedido.InvokeOperation<object>(queryString, TipoOperacion.GET);
				operador = JsonConvert.DeserializeObject<Operador>(objPedidos.ToString()!)!;

				return operador;
			}
			catch (Exception)
			{
				return operador;
			}
		}


		public int CrearPedido(Pedido Valor)
		{

			int result = Transaccion.Error;
			try
			{
				string ruta = "/grabarpedido?";

				var url = ServerClientePedido + ":" + PortClientePedido + "/" + RouteClientePedido + ruta;
				var request = (HttpWebRequest)WebRequest.Create(url)!;
				request.Method = "POST";
				request.ContentType = "application/json";

				var json = JsonConvert.SerializeObject(Valor);
				var data = Encoding.UTF8.GetBytes(json);

				// Escribir los datos en el cuerpo de la solicitud
				using (var stream = request.GetRequestStream())
				{
					stream.Write(data, 0, data.Length);
				}

				using (var response = (HttpWebResponse)request.GetResponse())
				{
					using (var reader = new StreamReader(response.GetResponseStream()))
					{
						var responseText = reader.ReadToEnd();
						result = JsonConvert.DeserializeObject<int>(responseText)!;
					}
				}

				//result = CommunicatorGestionPedido.InvokeOperation<int, Pedido>(ruta, TipoOperacion.POST, Valor);
				return result;
			}
			catch (Exception)
			{
				return result = Transaccion.Error;
			}
			
		}

        public int BorrarPedido(int IdPedido, int UsuarioEliminacion)
        {
            int result = Transaccion.Error;
			
            try
            {
                ListaPedido Npedido = new ListaPedido
                {
                    IdPedido = IdPedido,
                    UsuarioCreacion = UsuarioEliminacion
                };
                
				string ruta = "/eliminarpedido?";

				var url = ServerClientePedido + ":" + PortClientePedido + "/" + RouteClientePedido + ruta;
				var request = (HttpWebRequest)WebRequest.Create(url)!;
				request.Method = "POST";
				request.ContentType = "application/json";

				var json = JsonConvert.SerializeObject(Npedido);
				var data = Encoding.UTF8.GetBytes(json);

				// Escribir los datos en el cuerpo de la solicitud
				using (var stream = request.GetRequestStream())
				{
					stream.Write(data, 0, data.Length);
				}

				using (var response = (HttpWebResponse)request.GetResponse())
				{
					using (var reader = new StreamReader(response.GetResponseStream()))
					{
						var responseText = reader.ReadToEnd();
						result = JsonConvert.DeserializeObject<int>(responseText)!;
					}
				}

				//result = CommunicatorGestionPedido.InvokeOperation<int,ListaPedido>(ruta, TipoOperacion.POST, Npedido);
                return result;
            }
            catch (Exception)
            {
                return result = Transaccion.Error;
            }

        }

		//datos de tipo operador
		public async Task<List<UsuarioGalileoResponse>> BuscarUsuariosGalileoOperador(string idenCliente)
        {
			var metodo = "?";

            metodo += string.Format("empresaId={0}", HttpUtility.UrlEncode(idenCliente.ToString()));
            //if (variables.empresaId >= 0)
                //metodo += string.Format("empresaId={0}", HttpUtility.UrlEncode(variables.empresaId.ToString()));
            
            if (variables.idRolOperador >= 0)
				metodo += string.Format("&rolID={0}", HttpUtility.UrlEncode(variables.idRolOperador.ToString()));

            //descomentar cuando pase a produccion
            //if (!string.IsNullOrWhiteSpace(_servicioMicrosExternos.TokenGalileo))
            //    metodo += string.Format("&token={0}", HttpUtility.UrlEncode(_servicioMicrosExternos.TokenGalileo.ToString()));

            var url = ServerAvalab + ":" + portAvabalab + "/" + RouteAvalab + metodo;
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";

            string username = UserAvalab;
            string password = PassAvalab;

            string credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes(username + ":" + password));
            request.Headers[HttpRequestHeader.Authorization] = "Basic " + credentials;

            using (var response = (HttpWebResponse)request.GetResponse())
            {
                using (var reader = new StreamReader(response.GetResponseStream()))
                {
                    var responseText = await reader.ReadToEndAsync();
                    var respuesta = JsonConvert.DeserializeObject<List<UsuarioGalileoResponse>>(responseText)!;

                    return respuesta;
                }
            }

   //         using (var clienteGa = new HttpClient())
			//{
   //             //clienteGa.DefaultRequestHeaders.Add("Authorization", $"Bearer {_servicioMicrosExternos.TokenGalileo}");

   //             var resultado = await clienteGa.GetAsync("http://" + _servicioMicrosExternos.ServerGalileo + ":" + _servicioMicrosExternos.PortGalileo + _servicioMicrosExternos.PathServicesGalileo + metodo);
   //             //var resultado = await clienteGa.GetAsync("http://" + _servicioMicrosExternos.ServerGalileo + _servicioMicrosExternos.PathServicesGalileo);

   //             if (!resultado.IsSuccessStatusCode)
			//		throw new ArgumentException("Problemas con el servicio de Galileo.");

			//	var resultadoJson = await resultado.Content.ReadAsStringAsync();
			//	var respuesta = JsonConvert.DeserializeObject<List<UsuarioGalileoResponse>>(resultadoJson);

			//	return respuesta!;
			//}
		}


        public async Task<List<UsuarioGalileoResponse>> BuscarUsuariosGalileoMail(string idenCliente)
        {
            var metodo = "?";
            
			metodo += string.Format("empresaId={0}", HttpUtility.UrlEncode(idenCliente.ToString()));
            //if (variables.empresaId > 0)
            //metodo += string.Format("empresaId={0}", HttpUtility.UrlEncode(variables.empresaId.ToString()));                

            if (variables.idRol > 0)
                metodo += string.Format("&rolID={0}", HttpUtility.UrlEncode(variables.idRol.ToString()));

            //descomentar cuando pase a produccion
            //if (!string.IsNullOrWhiteSpace(_servicioMicrosExternos.TokenGalileo))
            //    metodo += string.Format("&token={0}", HttpUtility.UrlEncode(_servicioMicrosExternos.TokenGalileo.ToString()));

            var url = ServerAvalab + ":" + portAvabalab + "/" + RouteAvalab + metodo;
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";

            string username = UserAvalab;
            string password = PassAvalab;

            string credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes(username + ":" + password));
            request.Headers[HttpRequestHeader.Authorization] = "Basic " + credentials;

            using (var response = (HttpWebResponse)request.GetResponse())
            {
                using (var reader = new StreamReader(response.GetResponseStream()))
                {
                    var responseText = await reader.ReadToEndAsync();
                    var respuesta =  JsonConvert.DeserializeObject<List<UsuarioGalileoResponse>>(responseText)!;

					return respuesta;
                }
            }

            //using (var clienteGa = new HttpClient())
            //{
            //    //clienteGa.DefaultRequestHeaders.Add("Authorization", $"Bearer {microsExterno.TokenGalileo}");

            //    //var resultado = await clienteGa.GetAsync("http://" + StringHandler.ServerGalileo + ":" +
            //    //    StringHandler.PortGalileo + Routes.PathServicesGalileo + "/" + metodo);

            //    var resultado = await clienteGa.GetAsync("http://" + _servicioMicrosExternos.ServerGalileo + ":" + _servicioMicrosExternos.PortGalileo + _servicioMicrosExternos.PathServicesGalileo + metodo);
            //    //var resultado = await clienteGa.GetAsync("http://" + microsExterno.ServerGalileo + microsExterno.PathServicesGalileo + metodo); //prueba

            //    if (!resultado.IsSuccessStatusCode)
            //        throw new ArgumentException("Problemas con el servicio de Galileo.");

            //    var resultadoJson = await resultado.Content.ReadAsStringAsync();
            //    var respuesta = JsonConvert.DeserializeObject<List<UsuarioGalileoResponse>>(resultadoJson);

            //    return respuesta!;
            //}
        }


        //Datos del cliente para el envio de correo
        public DatosClienteCorreo datosClienteCorreo(int idOrden)
        {
			DatosClienteCorreo datosCliente = new DatosClienteCorreo();

			try
            {                
                var stringBuilder = new StringBuilder();
				string ruta = "/clienteCorreo";
				object objDatosCliente;

				if (idOrden > 0)
				{
					ruta += string.Format("?idOrden={0}", HttpUtility.UrlEncode(idOrden.ToString()));
				}

				var url = ServerClientePedido + ":" + PortClientePedido + "/" + RouteClientePedido + ruta;
				var request = (HttpWebRequest)WebRequest.Create(url)!;
				request.Method = "GET";

				using (var response = (HttpWebResponse)request.GetResponse())
				{
					using (var reader = new StreamReader(response.GetResponseStream()))
					{
						var responseText = reader.ReadToEnd();
						objDatosCliente = JsonConvert.DeserializeObject<object>(responseText)!;
					}
				}

				//object objDatosCliente = CommunicatorGestionPedido.InvokeOperation<object>(ruta, TipoOperacion.GET);
				datosCliente = JsonConvert.DeserializeObject<DatosClienteCorreo>(objDatosCliente.ToString()!)!;

				return datosCliente;
			}
            catch (Exception ex)
            {
				return datosCliente;
			}
        }


        public int EnviarCorreoPedido(PedidoCorreo pedidoCorreo)
        {
            int result = Transaccion.Error;
			int codigoRespuesta = 0;

			try
            {
                string ruta = "/pedidoCorreo";

				var url = ServerCorreo + ":" + PortCorreo + "/" + RouteCorreo + ruta;
				var request = (HttpWebRequest)WebRequest.Create(url)!;
				request.Method = "POST";
				request.ContentType = "application/json";

				var json = JsonConvert.SerializeObject(pedidoCorreo);
				var data = Encoding.UTF8.GetBytes(json);

				// Escribir los datos en el cuerpo de la solicitud
				using (var stream = request.GetRequestStream())
				{
					stream.Write(data, 0, data.Length);
				}

				using (var response = (HttpWebResponse)request.GetResponse())
				{
					using (var reader = new StreamReader(response.GetResponseStream()))
					{
						var responseText = reader.ReadToEnd();
						codigoRespuesta = JsonConvert.DeserializeObject<int>(responseText)!;
					}
				}

                //codigoRespuesta = CommunicatorCorreo.InvokeOperation<int, PedidoCorreo>(ruta, TipoOperacion.POST, pedidoCorreo);
				return codigoRespuesta;

            }
            catch (Exception ex)
            {
                return result = Transaccion.Error;
            }
        }

		//Datos del cliente cuando se elimina un pedido
		public DatosClienteCorreo datosClienteCorreoElim(int idPedido)
		{
            DatosClienteCorreo datosCliente = new DatosClienteCorreo();

            try
            {
                var stringBuilder = new StringBuilder();
                string ruta = "/clienteCorreoElim";
				object objDatosCliente;

				if (idPedido > 0)
                {
                    ruta += string.Format("?idPedido={0}", HttpUtility.UrlEncode(idPedido.ToString()));
                }

				var url = ServerCorreo + ":" + PortCorreo + "/" + RouteCorreo + ruta;
				var request = (HttpWebRequest)WebRequest.Create(url)!;
				request.Method = "GET";

				using (var response = (HttpWebResponse)request.GetResponse())
				{
					using (var reader = new StreamReader(response.GetResponseStream()))
					{
						var responseText = reader.ReadToEnd();
						objDatosCliente = JsonConvert.DeserializeObject<object>(responseText)!;
					}
				}

				//object objDatosCliente = CommunicatorGestionPedido.InvokeOperation<object>(ruta, TipoOperacion.GET);
                datosCliente = JsonConvert.DeserializeObject<DatosClienteCorreo>(objDatosCliente.ToString()!)!;
                return datosCliente;
            }
            catch (Exception ex)
            {
                return datosCliente;
            }
        }

		public int EnviarCorreoPedidoEli(ElimPedidoCorreo pedidoCorreo)
		{
            int result = Transaccion.Error;
			int codigoRespuesta = 0;

			try
			{
                string ruta = "/elimPedidoCorreo";

				var url = ServerClientePedido + ":" + PortClientePedido + "/" + RouteClientePedido + ruta;
				var request = (HttpWebRequest)WebRequest.Create(url)!;
				request.Method = "POST";
				request.ContentType = "application/json";

				var json = JsonConvert.SerializeObject(pedidoCorreo);
				var data = Encoding.UTF8.GetBytes(json);

				// Escribir los datos en el cuerpo de la solicitud
				using (var stream = request.GetRequestStream())
				{
					stream.Write(data, 0, data.Length);
				}

				using (var response = (HttpWebResponse)request.GetResponse())
				{
					using (var reader = new StreamReader(response.GetResponseStream()))
					{
						var responseText = reader.ReadToEnd();
						codigoRespuesta = JsonConvert.DeserializeObject<int>(responseText)!;
					}
				}

                //codigoRespuesta = CommunicatorCorreo.InvokeOperation<int, ElimPedidoCorreo>(ruta, TipoOperacion.POST, pedidoCorreo);
                return codigoRespuesta;
            }
			catch (Exception ex)
			{
                return result = Transaccion.Error;
            }
        }

		//metodo para correos del operador para el envio de pedidos
		public async Task<List<ClienteFlexlineResponse>> BuscarClienteFlexline(string clienteFlexline)
		{
			var metodo = "clientes";

			if (!string.IsNullOrWhiteSpace(clienteFlexline))
				metodo += string.Format("?ruc={0}", HttpUtility.UrlEncode(clienteFlexline));

			using (var clienteF = new HttpClient())
			{
				clienteF.DefaultRequestHeaders.Add("Authorization", $"Bearer {TokenFlex}");

				var resultado = await clienteF.GetAsync("http://" + ServerFlex + ":" + PortFlexl + PathFlex + metodo);

				if (!resultado.IsSuccessStatusCode)
					throw new ArgumentException("Problemas con el servicio de Flexline.");

				var resultadoJson = await resultado.Content.ReadAsStringAsync();
				var respuesta = JsonConvert.DeserializeObject<List<ClienteFlexlineResponse>>(resultadoJson);

				return respuesta!;
			}
		}

	}
}
