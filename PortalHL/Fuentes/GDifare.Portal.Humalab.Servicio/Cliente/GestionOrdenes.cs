using GDifare.Portal.Humalab.Seguridad.Modelos;
using GDifare.Portal.Humalab.Servicio.Modelos;
using GDifare.Portal.Humalab.Servicio.Modelos.GestionCliente;
using GDifare.Portal.Humalab.Servicio.Modelos.Orden;
using GDifare.Portal.Humalab.Servicio.Utils;
using GDifare.Portales.Comunicaciones;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Web;

namespace GDifare.Portal.Humalab.Servicio.Cliente
{
    public class GestionOrdenes
    {       
        private readonly Communicator CommunicatorGestionOrdenes;

        private string ServerCliente;
        private int PortCliente;
        private string RouteCliente;

		public GestionOrdenes(AppServicioClienteApi configuracion)
        {
            CommunicatorGestionOrdenes = new Communicator(configuracion.Server ,configuracion.Port, configuracion.RouteCliente, configuracion.Token);

            ServerCliente = configuracion.Server;
            PortCliente = configuracion.Port;
            RouteCliente = configuracion.RouteCliente;
		}

        public int ConsultarNumeroOrden(ConsultarOrden valor)
        {
            try
            {
                object objPedidos;

				var stringBuilder = new StringBuilder();
                stringBuilder.Append("/consultarnumeroorden?");
                stringBuilder.Append("IdUsuarioGalileo=" + valor.IdUsuarioGalileo+"&");
                stringBuilder.Append("UsuarioCreacion=" + valor.UsuarioCreacion);
                var queryString = stringBuilder.ToString();

				var url = ServerCliente + ":" + PortCliente + "/" + RouteCliente + queryString;
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

				//object objPedidos = CommunicatorGestionOrdenes.InvokeOperation<object>(queryString, TipoOperacion.GET);
                int norden =  JsonConvert.DeserializeObject<int>(objPedidos.ToString()!)!;
                return norden+1;
            }
            catch (Exception)
            {
                return Transaccion.Error;
            }            
        }

        public int NumeroOrden(string CodigoBarra)
        {
            try
            {
                object objOrden;

				var stringBuilder = new StringBuilder();
                stringBuilder.Append("/numeroorden?");
                stringBuilder.Append("CodigoBarra=" + CodigoBarra);
                var queryString = stringBuilder.ToString();

				var url = ServerCliente + ":" + PortCliente + "/" + RouteCliente + queryString;
				var request = (HttpWebRequest)WebRequest.Create(url)!;
				request.Method = "GET";

				using (var response = (HttpWebResponse)request.GetResponse())
				{
					using (var reader = new StreamReader(response.GetResponseStream()))
					{
						var responseText = reader.ReadToEnd();

						objOrden = JsonConvert.DeserializeObject<object>(responseText)!;
					}
				}

				//object objOrden = CommunicatorGestionOrdenes.InvokeOperation<object>(queryString, TipoOperacion.GET);
                int norden = JsonConvert.DeserializeObject<int>(objOrden.ToString()!)!;
                return norden;
            }
            catch (Exception)
            {
                return Transaccion.Error;
            }


        }

        public Orden ConsultarOrden(int IdOrden)
        {
            object objPedidos;

			var stringBuilder = new StringBuilder();
            stringBuilder.Append("/consultarorden?");
            stringBuilder.Append("IdOrden=" + IdOrden);
            var queryString = stringBuilder.ToString();

			var url = ServerCliente + ":" + PortCliente + "/" + RouteCliente + queryString;
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

			//object objPedidos = CommunicatorGestionOrdenes.InvokeOperation<object>(queryString, TipoOperacion.GET);
            Orden norden = JsonConvert.DeserializeObject<Orden>(objPedidos.ToString()!)!;
            return norden;
        }


        public string ListarPruebas(int IdOrden, int IdUsuario)
        {
            string lista = string.Empty;
            object objPedidos;

			try
            {
                var stringBuilder = new StringBuilder();
                stringBuilder.Append("/consultarprueba?");
                stringBuilder.Append("IdOrden=" + IdOrden + "&");
                stringBuilder.Append("IdUsuarioGalileo=" + IdUsuario);
                var queryString = stringBuilder.ToString();

				var url = ServerCliente + ":" + PortCliente + "/" + RouteCliente + queryString;
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

				//object objPedidos = CommunicatorGestionOrdenes.InvokeOperation<object>(queryString, TipoOperacion.GET);
                lista = JsonConvert.SerializeObject(objPedidos);
                return lista;
            }
            catch (Exception)
            {
                return lista;
            }
        }

        public List<LogObservaciones> Observaciones(string CodigoBarra)
        {
            List<LogObservaciones> lista = new List<LogObservaciones>();
            object obs;

			try
            {
                var stringBuilder = new StringBuilder();
                stringBuilder.Append("/consultarobservaciones?");
                stringBuilder.Append("CodigoBarra=" + CodigoBarra);
                var queryString = stringBuilder.ToString();

				var url = ServerCliente + ":" + PortCliente + "/" + RouteCliente + queryString;
				var request = (HttpWebRequest)WebRequest.Create(url)!;
				request.Method = "GET";

				using (var response = (HttpWebResponse)request.GetResponse())
				{
					using (var reader = new StreamReader(response.GetResponseStream()))
					{
						var responseText = reader.ReadToEnd();

						obs = JsonConvert.DeserializeObject<object>(responseText)!;
					}
				}

				//object obs = CommunicatorGestionOrdenes.InvokeOperation<object>(queryString, TipoOperacion.GET);
                lista = JsonConvert.DeserializeObject<List<LogObservaciones>>(obs.ToString()!)!;
                return lista;
            }
            catch (Exception)
            {
                return lista;
            }
        }

		public string Resultados(string CodigoBarra)
		{
            var error = string.Empty;
            string obs = "";
			try
			{
				var stringBuilder = new StringBuilder();
				stringBuilder.Append("/obtenerresultados?");
				stringBuilder.Append("CodigoBarra=" + CodigoBarra);
				var queryString = stringBuilder.ToString();

				var url = ServerCliente + ":" + PortCliente + "/" + RouteCliente + queryString;
				var request = (HttpWebRequest)WebRequest.Create(url)!;
				request.Method = "GET";

				using (var response = (HttpWebResponse)request.GetResponse())
				{
					using (var reader = new StreamReader(response.GetResponseStream()))
					{
						var responseText = reader.ReadToEnd();

						obs = JsonConvert.DeserializeObject<string>(responseText)!;
					}
				}

				//string obs = CommunicatorGestionOrdenes.InvokeOperation<string>(queryString, TipoOperacion.GET);
				return obs ;
			}
			catch (Exception)
			{
				return error;
			}
		}

        public string ResultadosNuevoPdf(string CodigoBarra)
        {
            var error = string.Empty;
            string obs = "";

			try
            {
                var stringBuilder = new StringBuilder();
                stringBuilder.Append("/resultadoPdfinal?");
                stringBuilder.Append("codBarra=" + CodigoBarra);
                var queryString = stringBuilder.ToString();

				var url = ServerCliente + ":" + PortCliente + "/" + RouteCliente + queryString;
				var request = (HttpWebRequest)WebRequest.Create(url)!;
				request.Method = "GET";

				using (var response = (HttpWebResponse)request.GetResponse())
				{
					using (var reader = new StreamReader(response.GetResponseStream()))
					{
						var responseText = reader.ReadToEnd();

						obs = responseText;
                        //obs = JsonConvert.DeserializeObject<string>(responseText)!;
					}
				}

				//string obs = CommunicatorGestionOrdenes.InvokeOperation<string>(queryString, TipoOperacion.GET);
                return obs;
            }
            catch (Exception)
            {
                return error;
            }
        }

		public List<Muestras> ListarMuestras(int IdOrden)
        {
            List<Muestras> lista = new List<Muestras>();
            object objMuestras;

			try
            {
                var stringBuilder = new StringBuilder();
                stringBuilder.Append("/listarmuestras?");
                stringBuilder.Append("IdOrden=" + IdOrden);
                var queryString = stringBuilder.ToString();

				var url = ServerCliente + ":" + PortCliente + "/" + RouteCliente + queryString;
				var request = (HttpWebRequest)WebRequest.Create(url)!;
				request.Method = "GET";

				using (var response = (HttpWebResponse)request.GetResponse())
				{
					using (var reader = new StreamReader(response.GetResponseStream()))
					{
						var responseText = reader.ReadToEnd();

						objMuestras = JsonConvert.DeserializeObject<object>(responseText)!;
					}
				}

				//object objMuestras = CommunicatorGestionOrdenes.InvokeOperation<object>(queryString, TipoOperacion.GET);
                lista = JsonConvert.DeserializeObject<List<Muestras>>(objMuestras.ToString()!)!;
                return lista;
            }
            catch (Exception)
            {
                return lista;
            }
        }


        public List<ListarOrden> ListarOrden(ConsultarOrden valor)
        {
            string FechaIniciall = valor.FechaInicio.GetDateTimeFormats('d')[0].Replace("/","d");
            string FechaFinall = valor.FechaFin.GetDateTimeFormats('d')[0].Replace("/","d");

            string FechaInicial = valor.FechaInicio.ToString("dd'\\d'MM'\\d'yyyy");
            string FechaFinal = valor.FechaFin.ToString("dd'\\d'MM'\\d'yyyy");

            List<ListarOrden> lista = new List<ListarOrden>();
            object objPedidos;

			try
            {
                var stringBuilder = new StringBuilder();
                stringBuilder.Append("/listarordenes?");
                stringBuilder.Append("OpcionBusqueda=" + valor.OpcionBusqueda  + "&");

				if(valor.opcionEstado != null && valor.opcionEstado != "")
					stringBuilder.Append("opcionEstado=" + (valor.opcionEstado ?? string.Empty) + "&");

				if(valor.DatoBusqueda != null && valor.DatoBusqueda != "")
					stringBuilder.Append("DatoBusqueda=" + (valor.DatoBusqueda ?? string.Empty) + "&");

				stringBuilder.Append("IdOrden=" + valor.IdOrden + "&");

				if(valor.CodigoBarra != null && valor.CodigoBarra != "")
					stringBuilder.Append("CodigoBarra=" + (valor.CodigoBarra ?? string.Empty) + "&");

				stringBuilder.Append("IdUsuarioGalileo=" + valor.IdUsuarioGalileo + "&");
                stringBuilder.Append("FechaInicio=" + FechaInicial + "&");
                stringBuilder.Append("FechaFin=" + FechaFinal);
                var queryString = stringBuilder.ToString();

				var url = ServerCliente + ":" + PortCliente + "/" + RouteCliente + queryString;
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

				//object objPedidos = CommunicatorGestionOrdenes.InvokeOperation<object>(queryString, TipoOperacion.GET);
                lista = JsonConvert.DeserializeObject<List<ListarOrden>>(objPedidos.ToString()!)!;                              
                return lista;
            }
            catch (Exception ex)
            {
                return lista;
            }
        }

		//Datos para el excel del cliente sus ordenes que realiza
		public List<ListOrdClienteExcel> ListarOrdenCliente(ConsultarOrden valor)
		{            
            string FechaInicial = valor.FechaInicio.ToString("dd'\\d'MM'\\d'yyyy");
            string FechaFinal = valor.FechaFin.ToString("dd'\\d'MM'\\d'yyyy");

            List<ListOrdClienteExcel> lista = new List<ListOrdClienteExcel>();
            object objOrdCliente;

            try
            {
                var stringBuilder = new StringBuilder();
                stringBuilder.Append("/listarOrdClienteExcel?");                               
                stringBuilder.Append("IdUsuarioGalileo=" + valor.IdUsuarioGalileo + "&");
                stringBuilder.Append("FechaInicio=" + FechaInicial + "&");
                stringBuilder.Append("FechaFin=" + FechaFinal);
                var queryString = stringBuilder.ToString();

                var url = ServerCliente + ":" + PortCliente + "/" + RouteCliente + queryString;
                var request = (HttpWebRequest)WebRequest.Create(url)!;
                request.Method = "GET";

                using (var response = (HttpWebResponse)request.GetResponse())
                {
                    using (var reader = new StreamReader(response.GetResponseStream()))
                    {
                        var responseText = reader.ReadToEnd();

                        objOrdCliente = JsonConvert.DeserializeObject<object>(responseText)!;
                    }
                }
                
                lista = JsonConvert.DeserializeObject<List<ListOrdClienteExcel>>(objOrdCliente.ToString()!)!;
                return lista;
            }
            catch (Exception ex)
            {
                return lista;
            }
        }


        public int RegistrarOrden(Orden orden)
        {
            int result = Transaccion.Error;
            try
            {
                string ruta = "/grabarorden?";

				var url = ServerCliente + ":" + PortCliente + "/" + RouteCliente + ruta;
				var request = (HttpWebRequest)WebRequest.Create(url)!;
				request.Method = "POST";
				request.ContentType = "application/json";

				var json = JsonConvert.SerializeObject(orden);				
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

				//result =CommunicatorGestionOrdenes.InvokeOperation<int, Orden>(ruta, TipoOperacion.POST, orden);
                return result;
            }
            catch (Exception ex)
            {
                return result= Transaccion.Error;
            }
        }


        public int ActualizarOrden(Orden orden)
        {
            int result = Transaccion.Error;
            try
            {
                string ruta = "/actualizarorden?";

				var url = ServerCliente + ":" + PortCliente + "/" + RouteCliente + ruta;
				var request = (HttpWebRequest)WebRequest.Create(url)!;
				request.Method = "POST";
				request.ContentType = "application/json";

				var json = JsonConvert.SerializeObject(orden);
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

				//result = CommunicatorGestionOrdenes.InvokeOperation<int, Orden>(ruta, TipoOperacion.POST, orden);
                return result;

            }
            catch (Exception)
            {
                return result;
            }
        }

        public int EliminarOrden(Orden orden)
        {
            int result = Transaccion.Error;
            try
            {
                string ruta = "/eliminarorden?";

				var url = ServerCliente + ":" + PortCliente + "/" + RouteCliente + ruta;
				var request = (HttpWebRequest)WebRequest.Create(url)!;
				request.Method = "POST";
				request.ContentType = "application/json";

				var json = JsonConvert.SerializeObject(orden);
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

				//result = CommunicatorGestionOrdenes.InvokeOperation<int, Orden>(ruta, TipoOperacion.POST, orden);
                return result;
            }
            catch (Exception)
            {
                return result;
            }
        }

        public int EliminarPruebas(Pruebas prueba)
        {
            int result = Transaccion.Error;
            try
            {
                string ruta = "/eliminarpruebas?";

				var url = ServerCliente + ":" + PortCliente + "/" + RouteCliente + ruta;
				var request = (HttpWebRequest)WebRequest.Create(url)!;
				request.Method = "POST";
				request.ContentType = "application/json";

				var json = JsonConvert.SerializeObject(prueba);
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

				//result = CommunicatorGestionOrdenes.InvokeOperation<int, Pruebas>(ruta, TipoOperacion.POST, prueba);
                return result;
            }
            catch (Exception)
            {
                return result;
            }
        }

        public string NombreEstadoOrden(int idOrdenEstado)
        {
            string nombreEstado = "";
            
            try
            {
                string ruta = "/nombreEstadoOrden";
                
                if (idOrdenEstado > 0)
                {
                    ruta += string.Format("?idOrdenEstado={0}", HttpUtility.UrlEncode(idOrdenEstado.ToString()));
                }

				var url = ServerCliente + ":" + PortCliente + "/" + RouteCliente + ruta;
				var request = (HttpWebRequest)WebRequest.Create(url)!;
				request.Method = "GET";				

				using (var response = (HttpWebResponse)request.GetResponse())
				{
					using (var reader = new StreamReader(response.GetResponseStream()))
					{
						var responseText = reader.ReadToEnd();
						//nombreEstado = JsonConvert.DeserializeObject<string>(responseText)!;
						nombreEstado = responseText;
                    }
				}

				//nombreEstado = CommunicatorGestionOrdenes.InvokeOperation<string>(ruta, TipoOperacion.GET);
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }

            return nombreEstado;
        }

        public string ActuaLisOrdResultPdf(ActualizaPdfOrden actualizaPdfOrden)
        {
            string resultado = "";
            string ruta = "/actualizaEstadoOrdenPdf";

            try
            {
				var url = ServerCliente + ":" + PortCliente + "/" + RouteCliente + ruta;
				var request = (HttpWebRequest)WebRequest.Create(url)!;
				request.Method = "POST";
				request.ContentType = "application/json";

				var json = JsonConvert.SerializeObject(actualizaPdfOrden);
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
						resultado = JsonConvert.DeserializeObject<string>(responseText)!;
					}
				}

				//resultado = CommunicatorGestionOrdenes.InvokeOperation<string, ActualizaPdfOrden>(ruta, TipoOperacion.POST, actualizaPdfOrden);
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }

            return resultado;
        }

        public List<CataTiposClienteResponse> TiposClientesHum()
        {
            string ruta = "/listarTiposClientes";
            var tipos = new List<CataTiposClienteResponse>();

            try
            {
				var url = ServerCliente + ":" + PortCliente + "/" + RouteCliente + ruta;
				var request = (HttpWebRequest)WebRequest.Create(url)!;
				request.Method = "GET";

				using (var response = (HttpWebResponse)request.GetResponse())
				{
					using (var reader = new StreamReader(response.GetResponseStream()))
					{
						var responseText = reader.ReadToEnd();
						tipos = JsonConvert.DeserializeObject<List<CataTiposClienteResponse>>(responseText)!;
					}
				}

				//tipos = CommunicatorGestionOrdenes.InvokeOperation<List<CataTiposClienteResponse>>(ruta, TipoOperacion.GET);

                if (tipos.Count > 0)
                {
                    return tipos; 
                }
            }
            catch
            {
                return tipos;
            }

            return tipos;
        }

        public int ClienteId(int idGalileo)
        {
            string ruta = "/idClienteOrden";
            int id = 0;

            try
            {
                if (idGalileo > 0)
                {
                    ruta += string.Format("?idGalileo={0}", HttpUtility.UrlEncode(idGalileo.ToString()));
                }

				var url = ServerCliente + ":" + PortCliente + "/" + RouteCliente + ruta;
				var request = (HttpWebRequest)WebRequest.Create(url)!;
				request.Method = "GET";

				using (var response = (HttpWebResponse)request.GetResponse())
				{
					using (var reader = new StreamReader(response.GetResponseStream()))
					{
						var responseText = reader.ReadToEnd();
						id = JsonConvert.DeserializeObject<int>(responseText)!;
					}
				}

				//id = CommunicatorGestionOrdenes.InvokeOperation<int>(ruta, TipoOperacion.GET);

                if (id > 0)
                {
                    return id;
                }
            }
            catch
            {
                return id;
            }

            return id;
        }

		public int ClienteIdPedido(int idGalileo)
		{
			string ruta = "/idClientePedido";
			int id = 0;

			try
			{
				if (idGalileo > 0)
				{
					ruta += string.Format("?idGalileo={0}", HttpUtility.UrlEncode(idGalileo.ToString()));
				}

				var url = ServerCliente + ":" + PortCliente + "/" + RouteCliente + ruta;
				var request = (HttpWebRequest)WebRequest.Create(url)!;
				request.Method = "GET";

				using (var response = (HttpWebResponse)request.GetResponse())
				{
					using (var reader = new StreamReader(response.GetResponseStream()))
					{
						var responseText = reader.ReadToEnd();
						id = JsonConvert.DeserializeObject<int>(responseText)!;
					}
				}

				if (id > 0)
				{
					return id;
				}
			}
			catch
			{
				return id;
			}

			return id;
		}
	}
}
