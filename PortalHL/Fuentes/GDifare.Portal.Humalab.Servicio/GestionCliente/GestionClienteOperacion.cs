using GDifare.Portal.Humalab.Seguridad.Modelos;
using GDifare.Portal.Humalab.Servicio.Modelos;
using GDifare.Portal.Humalab.Servicio.Modelos.Flexline;
using GDifare.Portal.Humalab.Servicio.Modelos.Galileo;
using GDifare.Portal.Humalab.Servicio.Modelos.GestionCliente;
using GDifare.Portal.Humalab.Servicio.Modelos.PedidosCliente;
using GDifare.Portal.Humalab.Servicio.Resources;
using GDifare.Portales.Comunicaciones;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using System.Web;

namespace GDifare.Portal.Humalab.Servicio.GestionCliente;

public class GestionClienteOperacion
{
    private readonly Communicator CommunicatorGestionCliente; 
	private readonly Communicator CommunicatorFlexline;
	private readonly Communicator CommunicatorGalileo;
    private readonly Communicator CommunicatorCorreo;
    private readonly AppServicioMicros microInterno;
    private readonly AppServicioMicrosExternos microsExterno;
    private readonly Variables variables;

    //Avalab
    private string UserAvalab;
    private string PassAvalab;

    public GestionClienteOperacion(AppServicioMicros settingsMicros, AppServicioMicrosExternos servicioMicrosExternos, Variables variables,
                                   string user, string pass)
    {
        this.microInterno = settingsMicros;
        this.microsExterno = servicioMicrosExternos;
        this.variables = variables;

        CommunicatorGestionCliente = new Communicator(microInterno.ServerGestionCliente, microInterno.PortGestionCliente, microInterno.RouteGestionCliente, microInterno.TokenGestionCliente);
        CommunicatorFlexline = new Communicator(microsExterno.ServerFlexLine, microsExterno.PortFlexLine, microsExterno.PathServicesClientesFlexLine, microsExterno.TokenFlexline);
        CommunicatorGalileo = new Communicator(microsExterno.ServerGalileo, microsExterno.PortGalileo, microsExterno.PathServicesGalileo, microsExterno.TokenGalileo);
        CommunicatorCorreo = new Communicator(microInterno.ServerCorreo, microInterno.PortCorreo, microInterno.RouteCorreo, microInterno.TokenCorreo);

        UserAvalab = user;
        PassAvalab = pass;
    }

    //Retorna estados del cliente
    public List<EstadosHumalabClienteResponse> EstadosHumalabCliente()
	{
		List<EstadosHumalabClienteResponse> listaEstadosHumaCliente = new List<EstadosHumalabClienteResponse>();
		try
		{
            var metodo = "/estadosCliente";

			var url = microInterno.ServerGestionCliente + ":" + microInterno.PortGestionCliente + "/" + microInterno.RouteGestionCliente + metodo;
			var request = (HttpWebRequest)WebRequest.Create(url)!;
			request.Method = "GET";

			using (var response = (HttpWebResponse)request.GetResponse())
			{
				using (var reader = new StreamReader(response.GetResponseStream()))
				{
					var responseText = reader.ReadToEnd();

					listaEstadosHumaCliente = JsonConvert.DeserializeObject<List<EstadosHumalabClienteResponse>>(responseText)!;
				}
			}
            
            //listaEstadosHumaCliente = CommunicatorGestionCliente.InvokeOperation<List<EstadosHumalabClienteResponse>>(metodo, TipoOperacion.GET);            
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
        return listaEstadosHumaCliente;
    }

    //Retorna un solo cliente o varios clientes en base al ruc o nombre
    public ClientesFinal ConsultarClienteHuma(ClienteHumalabRequest clientesHumalab)
    {
        var metodo = "/clienteHumalab";
        
        ClienteHumalabResponse clientesHuma = new ClienteHumalabResponse();
        ClientesFinal clientesFinal = new ClientesFinal();
        object objCliente;
		List<DataClientes> dataClientesnew = new List<DataClientes>();

		if (!string.IsNullOrWhiteSpace(clientesHumalab.RucNombre))
        {
            metodo += string.Format("?RucNombre={0}", HttpUtility.UrlEncode(clientesHumalab.RucNombre!.ToString()));
        }

        if (clientesHumalab.idEstado >= 0)
        {
            if (string.IsNullOrWhiteSpace(clientesHumalab.RucNombre))
            {
                metodo += string.Format("?idEstado={0}", HttpUtility.UrlEncode(clientesHumalab.idEstado.ToString()));
            }
            else
            {
                metodo += string.Format("&idEstado={0}", HttpUtility.UrlEncode(clientesHumalab.idEstado.ToString()));
            }            
        }

        //if (clientesHumalab.Offset > 0)
        //{
        //    metodo += string.Format("&Offset={0}", HttpUtility.UrlEncode(clientesHumalab.Offset.ToString()));
        //}

        //if (clientesHumalab.Limit > 0)
        //{
        //    metodo += string.Format("&Limit={0}", HttpUtility.UrlEncode(clientesHumalab.Limit.ToString()));
        //}

		var url = microInterno.ServerGestionCliente + ":" + microInterno.PortGestionCliente + "/" + microInterno.RouteGestionCliente + metodo;
		var request = (HttpWebRequest)WebRequest.Create(url)!;
		request.Method = "GET";

		using (var response = (HttpWebResponse)request.GetResponse())
		{
			using (var reader = new StreamReader(response.GetResponseStream()))
			{
				var responseText = reader.ReadToEnd();

				objCliente = JsonConvert.DeserializeObject<object>(responseText)!;
			}
		}

		//object objCliente = CommunicatorGestionCliente.InvokeOperation<object>(metodo, TipoOperacion.GET);
        if (objCliente != null)
        {
			//clientesHuma = JsonConvert.DeserializeObject<ClienteHumalabResponse>(objCliente.ToString()!)!;
			dataClientesnew = JsonConvert.DeserializeObject<List<DataClientes>>(objCliente.ToString()!)!;

			//clientesFinal.totalRegistros = clientesHuma.totalRegistros;
			//clientesFinal.totalPaginas = clientesHuma.totalPaginas;

			foreach (var dataCliente in (dynamic)dataClientesnew)
			{
				clientesFinal.dataClientes.Add(new DataClientes
				{
					IdCliente = dataCliente.IdCliente,
					Cliente = dataCliente.Cliente,
					Ruc = dataCliente.Ruc,
					Usuario = dataCliente.Usuario,
					FechaRegistro = dataCliente.FechaRegistro,
					OperadorLogistico = dataCliente.OperadorLogistico,
					EstadoCodigo = dataCliente.EstadoCodigo,
					Estado = dataCliente.Estado,
					FechaTemporal = dataCliente.FechaTemporal
				});
			}

			//foreach (var dataCliente in (dynamic)clientesHuma.data)
			//{
			//	clientesFinal.dataClientes.Add(new DataClientes
			//	{
			//		IdCliente = dataCliente.IdCliente,
			//		Cliente = dataCliente.Cliente,
			//		Ruc = dataCliente.Ruc,
			//		Usuario = dataCliente.Usuario,
			//		FechaRegistro = dataCliente.FechaRegistro,
			//		OperadorLogistico = dataCliente.OperadorLogistico,
			//		EstadoCodigo = dataCliente.EstadoCodigo,
			//		Estado = dataCliente.Estado,
			//                 FechaTemporal = dataCliente.FechaTemporal
			//	});
			//}
		}       

        return clientesFinal;
    }

    //Retorna todos los clientes
    public ClientesFinal ConsultarClientesTodos(ClientesRequest clientes)
    {
        var metodo = "/clientesHumalab";

        //ClienteHumalabResponse clientesHuma = new ClienteHumalabResponse();
		List<ClienteHumalabResponse>  clientesHuma = new List<ClienteHumalabResponse>();
		ClientesFinal clientesFinal = new ClientesFinal();
        object objClientes;
		List<DataClientes> dataClientesnew = new List<DataClientes>();

		//if (clientes.Offset > 0)
		//      {
		//          metodo += string.Format("?Offset={0}", HttpUtility.UrlEncode(clientes.Offset.ToString()));
		//      }

		//      if (clientes.Limit > 0)
		//      {
		//          metodo += string.Format("&Limit={0}", HttpUtility.UrlEncode(clientes.Limit.ToString()));
		//      }

		var url = microInterno.ServerGestionCliente + ":" + microInterno.PortGestionCliente + "/" + microInterno.RouteGestionCliente + metodo;
		var request = (HttpWebRequest)WebRequest.Create(url)!;
		request.Method = "GET";

		using (var response = (HttpWebResponse)request.GetResponse())
		{
			using (var reader = new StreamReader(response.GetResponseStream()))
			{
				var responseText = reader.ReadToEnd();

				objClientes = JsonConvert.DeserializeObject<object>(responseText)!;
			}
		}
		//object objClientes = CommunicatorGestionCliente.InvokeOperation<object>(metodo, TipoOperacion.GET);        
        if (objClientes != null)
        {
			//clientesHuma = JsonConvert.DeserializeObject<ClienteHumalabResponse>(objClientes.ToString()!)!;
			dataClientesnew = JsonConvert.DeserializeObject<List<DataClientes>>(objClientes.ToString()!)!;

			//clientesFinal.totalRegistros = clientesHuma.;
			//clientesFinal.totalPaginas = clientesHuma.totalPaginas;
			foreach (var data in (dynamic)dataClientesnew)
			{
				clientesFinal.dataClientes.Add(new DataClientes
				{
					IdCliente = data.IdCliente,
					Cliente = data.Cliente,
					Ruc = data.Ruc,
					Usuario = data.Usuario,
					FechaRegistro = data.FechaRegistro,
					OperadorLogistico = data.OperadorLogistico,
					EstadoCodigo = data.EstadoCodigo,
					Estado = data.Estado,
					FechaTemporal = data.FechaTemporal
				});
			}
			//foreach (var data in (dynamic)clientesHuma.data)
			//{
			//	clientesFinal.dataClientes.Add(new DataClientes
			//	{
			//		IdCliente = data.IdCliente,
			//		Cliente = data.Cliente,
			//		Ruc = data.Ruc,
			//		Usuario = data.Usuario,
			//		FechaRegistro = data.FechaRegistro,
			//		OperadorLogistico = data.OperadorLogistico,
			//		EstadoCodigo = data.EstadoCodigo,
			//		Estado = data.Estado,
   //                 FechaTemporal = data.FechaTemporal
			//	});
			//}
        }
        
        return clientesFinal;
    }

    //Modifica el estado de un cliente
    public ModificarClienteResponse ModificarClienteHumalab(ModificarClienteRequest modificarCliente)
    {
        var metodo = "/modificarClienteHuma";
		var modiCliente = new ModificarClienteResponse();

		var peticionPut = new ModificarClienteRequest
        {
            idCliente = modificarCliente.idCliente,
            Cliente = modificarCliente.Cliente ?? string.Empty,
            FechaVigencia = modificarCliente.FechaVigencia ?? string.Empty,
            IdEstado = modificarCliente.IdEstado,
            Observacion = modificarCliente.Observacion ?? string.Empty,
            UsuarioModificacion = modificarCliente.UsuarioModificacion
        };

		var url = microInterno.ServerGestionCliente + ":" + microInterno.PortGestionCliente + "/" + microInterno.RouteGestionCliente + metodo;
		var requestNew = (HttpWebRequest)WebRequest.Create(url);
		requestNew.Method = "PUT";
		requestNew.ContentType = "application/json";

		var json = JsonConvert.SerializeObject(peticionPut);
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
				modiCliente = JsonConvert.DeserializeObject<ModificarClienteResponse>(responseText)!;
			}
		}

		//var modiCliente = CommunicatorGestionCliente.InvokeOperation<ModificarClienteResponse, ModificarClienteRequest>(metodo, TipoOperacion.PUT, peticionPut);
		return modiCliente;
    }

    //Cliente consulta a flexline
    public async Task<List<ClienteFlexlineResponse>> BuscarClienteFlexline(string clienteFlexline)
    {
        var metodo = "";

        if(!string.IsNullOrWhiteSpace(clienteFlexline))
			metodo += string.Format("?ruc={0}", HttpUtility.UrlEncode(clienteFlexline));

        using (var clienteF = new HttpClient())
        {
            clienteF.DefaultRequestHeaders.Add("Authorization", $"Bearer {microsExterno.TokenFlexline}");

            var resultado = await clienteF.GetAsync("http://"+microsExterno.ServerFlexLine+":"+microsExterno.PortFlexLine+microsExterno.PathServicesClientesFlexLine+metodo);

            if (!resultado.IsSuccessStatusCode)
                throw new ArgumentException("Problemas con el servicio de Flexline.");

			var resultadoJson = await resultado.Content.ReadAsStringAsync();
            var respuesta = JsonConvert.DeserializeObject<List<ClienteFlexlineResponse>>(resultadoJson);

            return respuesta!;
		}
    }

    //Asesores flexline
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


	//Consulta usuarios galileo
	public async Task<List<UsuarioGalileoResponse>> BuscarUsuariosGalileo(string ruc)
    {
        //      var metodo = "listar?";

        //if (variables.empresaId >= 0)
        //	metodo += string.Format("empresaId={0}", HttpUtility.UrlEncode(variables.empresaId.ToString()));

        //      if(variables.idRol > 0)
        //	metodo += string.Format("&rolID={0}", HttpUtility.UrlEncode(variables.idRol.ToString()));

        //      //descomentar cuando pase a produccion
        //      if (!string.IsNullOrWhiteSpace(microsExterno.TokenGalileo))
        //          metodo += string.Format("&token={0}", HttpUtility.UrlEncode(microsExterno.TokenGalileo.ToString()));

        //      using (var clienteGa = new HttpClient())
        //      {
        //          //clienteGa.DefaultRequestHeaders.Add("Authorization", $"Bearer {microsExterno.TokenGalileo}");

        //          //var resultado = await clienteGa.GetAsync("http://" + StringHandler.ServerGalileo + ":" +
        //          //    StringHandler.PortGalileo + Routes.PathServicesGalileo + "/" + metodo);

        //          var resultado = await clienteGa.GetAsync("http://" + microsExterno.ServerGalileo + ":" + microsExterno.PortGalileo + microsExterno.PathServicesGalileo + metodo);
        //          //var resultado = await clienteGa.GetAsync("http://" + microsExterno.ServerGalileo + microsExterno.PathServicesGalileo + metodo); //prueba

        //          if (!resultado.IsSuccessStatusCode) 
        //		throw new ArgumentException("Problemas con el servicio de Galileo.");

        //	var resultadoJson = await resultado.Content.ReadAsStringAsync();
        //	var respuesta = JsonConvert.DeserializeObject<List<UsuarioGalileoResponse>>(resultadoJson);

        //	return respuesta!;
        //}		

        //Avalab
        var metodo = "?";
        var respuesta = new List<UsuarioGalileoResponse>();

        if (ruc != null)
            metodo += string.Format("empresaId={0}", HttpUtility.UrlEncode(ruc.ToString()));

        if (variables.idRol > 0)
            metodo += string.Format("&rolID={0}", HttpUtility.UrlEncode(variables.idRol.ToString()));

        var url = microsExterno.ServerGalileo + ":" + microsExterno.PortGalileo + "/" + microsExterno.PathServicesGalileo + metodo;
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
                var responseText =  reader.ReadToEnd();

                respuesta = JsonConvert.DeserializeObject<List<UsuarioGalileoResponse>>(responseText);                
            }
        }

        return respuesta!;
    }

    //Dar de alta a un nuevo cliente registro
    public DarAltaClienteHumalabResponse DarAltaClienteHuma(DarAltaClienteHumalabRequest darAltaCliente)
    {
		var metodo = "/darAltaClienteHumalab";
		var darAlta = new DarAltaClienteHumalabResponse();

		var peticionPost = new DarAltaClienteHumalabRequestService
		{
            idGalileo = darAltaCliente.idGalileo,
            Email = darAltaCliente.Email ?? string.Empty,
            Identificacion = darAltaCliente.Identificacion ?? string.Empty,
            IdRol = variables.idRol,
            Usuario = darAltaCliente.Usuario ?? string.Empty,
            UsuarioCreacion = darAltaCliente.UsuarioCreacion,
            idAsesorFlex = darAltaCliente.idAsesorFlex ?? string.Empty,
            nombreAsesor = darAltaCliente.nombreAsesor ?? string.Empty,
            nombreGalileo = darAltaCliente.nombreGalileo ?? string.Empty,
            codigoCliente = darAltaCliente.codigoCliente ?? string.Empty,
            DireccionCliente = darAltaCliente.DireccionCliente ?? string.Empty,
            ProvinciaCliente = darAltaCliente.ProvinciaCliente ?? string.Empty,
            CiudadCliente = darAltaCliente.CiudadCliente ?? string.Empty,
            LatitudCliente = darAltaCliente.LatitudCliente ?? string.Empty,
            LongitudCliente = darAltaCliente.LongitudCliente ?? string.Empty,
            IdAsesorLis = darAltaCliente.IdAsesorLis ?? string.Empty,
            Telefono = darAltaCliente.Telefono ?? string.Empty,
			LabComercial = darAltaCliente.LabComercial ?? string.Empty
		};

		var url = microInterno.ServerGestionCliente + ":" + microInterno.PortGestionCliente + "/" + microInterno.RouteGestionCliente + metodo;
		var requestNew = (HttpWebRequest)WebRequest.Create(url);
		requestNew.Method = "POST";
		requestNew.ContentType = "application/json";

		var json = JsonConvert.SerializeObject(peticionPost);
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
				darAlta = JsonConvert.DeserializeObject<DarAltaClienteHumalabResponse>(responseText)!;
			}
		}

		//var darAlta = CommunicatorGestionCliente.InvokeOperation<DarAltaClienteHumalabResponse, DarAltaClienteHumalabRequestService>(metodo, TipoOperacion.POST, peticionPost);
		return darAlta;
	}

    public DarAltaClienteHumalabResponse ModiClienteHumalabDarAlta(DarAltaClienteHumalabRequest darAltaCliente)
    {
		var metodo = "/darAltaModificarHumalab";
		var darAlta = new DarAltaClienteHumalabResponse();

		var peticionPost = new DarAltaClienteHumalabRequestService
		{
			idGalileo = darAltaCliente.idGalileo,
			Email = darAltaCliente.Email ?? string.Empty,
			Identificacion = darAltaCliente.Identificacion ?? string.Empty,
			IdRol = variables.idRol,
			Usuario = darAltaCliente.Usuario ?? string.Empty,
			UsuarioCreacion = darAltaCliente.UsuarioCreacion,
			idAsesorFlex = darAltaCliente.idAsesorFlex ?? string.Empty,
			nombreAsesor = darAltaCliente.nombreAsesor ?? string.Empty,
			nombreGalileo = darAltaCliente.nombreGalileo ?? string.Empty,
			codigoCliente = darAltaCliente.codigoCliente ?? string.Empty,
			DireccionCliente = darAltaCliente.DireccionCliente ?? string.Empty,
			ProvinciaCliente = darAltaCliente.ProvinciaCliente ?? string.Empty,
			CiudadCliente = darAltaCliente.CiudadCliente ?? string.Empty,
			LatitudCliente = darAltaCliente.LatitudCliente ?? string.Empty,
			LongitudCliente = darAltaCliente.LongitudCliente ?? string.Empty,
			IdAsesorLis = darAltaCliente.IdAsesorLis ?? string.Empty,
			Telefono = darAltaCliente.Telefono ?? string.Empty,
			LabComercial = darAltaCliente.LabComercial ?? string.Empty
		};

		var url = microInterno.ServerGestionCliente + ":" + microInterno.PortGestionCliente + "/" + microInterno.RouteGestionCliente + metodo;
		var requestNew = (HttpWebRequest)WebRequest.Create(url);
		requestNew.Method = "PUT";
		requestNew.ContentType = "application/json";

		var json = JsonConvert.SerializeObject(peticionPost);
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
				darAlta = JsonConvert.DeserializeObject<DarAltaClienteHumalabResponse>(responseText)!;
			}
		}

		//var darAlta = CommunicatorGestionCliente.InvokeOperation<DarAltaClienteHumalabResponse, DarAltaClienteHumalabRequestService>(metodo, TipoOperacion.PUT, peticionPost);
		return darAlta;
	}


	//Retornar todos los pedidos
	public List<PedidoHumalabResponse> PedidosHumalab(int idCliente)
    {
		var metodo = "/pedidosHumalab";
		List<PedidoHumalabResponse> pedidoHumalab =  new();
        object objPedidos;

		if (idCliente > 0)
			metodo += string.Format("?idCliente={0}", HttpUtility.UrlEncode(idCliente.ToString()));

		var url = microInterno.ServerGestionCliente + ":" + microInterno.PortGestionCliente + "/" + microInterno.RouteGestionCliente + metodo;
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

		//object objPedidos = CommunicatorGestionCliente.InvokeOperation<object>(metodo, TipoOperacion.GET);

        if (objPedidos != null)
        {
			pedidoHumalab = JsonConvert.DeserializeObject<List<PedidoHumalabResponse>>(objPedidos.ToString()!)!;
		}

        return pedidoHumalab;
	}


    //retorno total de ordenes y pruebas del pedido
    public PedidoOrdenHumalabResponse PedidosOrdenHumalab(string idPedido, int idCliente)
    {
        var metodo = "/pedidosOrdenHumalab";
        PedidoOrdenHumalabResponse pedidoOrdenHumalab = new();
        object objPedidosOrden;

		if (!string.IsNullOrWhiteSpace(idPedido))
			metodo += string.Format("?numRemision={0}", HttpUtility.UrlEncode(idPedido.ToString()));

        if(idCliente > 0)
            metodo += string.Format("&idCliente={0}", HttpUtility.UrlEncode(idCliente.ToString()));

		var url = microInterno.ServerGestionCliente + ":" + microInterno.PortGestionCliente + "/" + microInterno.RouteGestionCliente + metodo;
		var request = (HttpWebRequest)WebRequest.Create(url)!;
		request.Method = "GET";

		using (var response = (HttpWebResponse)request.GetResponse())
		{
			using (var reader = new StreamReader(response.GetResponseStream()))
			{
				var responseText = reader.ReadToEnd();
				objPedidosOrden = JsonConvert.DeserializeObject<object>(responseText)!;
			}
		}

		//object objPedidosOrden = CommunicatorGestionCliente.InvokeOperation<object>(metodo, TipoOperacion.GET);

        if (objPedidosOrden != null)
        {
			pedidoOrdenHumalab = JsonConvert.DeserializeObject<PedidoOrdenHumalabResponse>(objPedidosOrden.ToString()!)!;
		}

        return pedidoOrdenHumalab;
	}

    //Retorna los pedidos en base a los estados
    public GraficarPedidosHumalabResponse GraficarPedidosHumalab(GraficarPedidosHumalabRequest graficar)
    {
        var metodo = "/pedidoEstadosHumalab";
        GraficarPedidosHumalabResponse graficarPedidos = new();
        object objPedidosEstados;

		if (!string.IsNullOrEmpty(graficar.numRemision))
            metodo += string.Format("?numRemision={0}", HttpUtility.UrlEncode(graficar.numRemision.ToString()));

   //     if (graficar.idPedido > 0)
			//metodo += string.Format("?idPedido={0}", HttpUtility.UrlEncode(graficar.idPedido.ToString()));

        if(graficar.idCliente > 0)
            metodo += string.Format("&idCliente={0}", HttpUtility.UrlEncode(graficar.idCliente.ToString()));

        if (!string.IsNullOrEmpty(graficar.nomPrueba))
            metodo += string.Format("&nomPrueba={0}", HttpUtility.UrlDecode(graficar.nomPrueba.ToString()));

		var url = microInterno.ServerGestionCliente + ":" + microInterno.PortGestionCliente + "/" + microInterno.RouteGestionCliente + metodo;
		var request = (HttpWebRequest)WebRequest.Create(url)!;
		request.Method = "GET";

		using (var response = (HttpWebResponse)request.GetResponse())
		{
			using (var reader = new StreamReader(response.GetResponseStream()))
			{
				var responseText = reader.ReadToEnd();
				objPedidosEstados = JsonConvert.DeserializeObject<object>(responseText)!;
			}
		}

		//object objPedidosEstados = CommunicatorGestionCliente.InvokeOperation<object>(metodo, TipoOperacion.GET);

        if (objPedidosEstados != null)
        {
			graficarPedidos = JsonConvert.DeserializeObject<GraficarPedidosHumalabResponse>(objPedidosEstados.ToString()!)!;
		}
        
		return graficarPedidos;
	}

    //Retorna todos los pedidos para graficar
    public GraficarPedidosHumalabResponse GraficarPedidosTodosHumalab(int idCliente)
    {
		var metodo = "/pedidoTodosHumalab";
		GraficarPedidosHumalabResponse graficarPedidos = new();
        object objPedidosEstados;

		if (idCliente > 0)
            metodo += string.Format("?idCliente={0}", HttpUtility.UrlEncode(idCliente.ToString()));

		var url = microInterno.ServerGestionCliente + ":" + microInterno.PortGestionCliente + "/" + microInterno.RouteGestionCliente + metodo;
		var request = (HttpWebRequest)WebRequest.Create(url)!;
		request.Method = "GET";

		using (var response = (HttpWebResponse)request.GetResponse())
		{
			using (var reader = new StreamReader(response.GetResponseStream()))
			{
				var responseText = reader.ReadToEnd();
				objPedidosEstados = JsonConvert.DeserializeObject<object>(responseText)!;
			}
		}

		//object objPedidosEstados = CommunicatorGestionCliente.InvokeOperation<object>(metodo, TipoOperacion.GET);

        if (objPedidosEstados != null)
        {
			graficarPedidos = JsonConvert.DeserializeObject<GraficarPedidosHumalabResponse>(objPedidosEstados.ToString()!)!;
		}

		return graficarPedidos;
	}


	//Retorna las muestras mes actual o mes anterior
	public MuestrasHumalabResponse MuestrasHumalab(int idCliente)
    {
		var metodo = "/muestrasHumalab";
		MuestrasHumalabResponse muestrasHumalab = new();
        object objMuestras;

		if (idCliente > 0)
            metodo += string.Format("?idCliente={0}", HttpUtility.UrlEncode(idCliente.ToString()));

		var url = microInterno.ServerGestionCliente + ":" + microInterno.PortGestionCliente + "/" + microInterno.RouteGestionCliente + metodo;
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

		//object objMuestras = CommunicatorGestionCliente.InvokeOperation<object>(metodo, TipoOperacion.GET);

        if (objMuestras != null)
        {
			muestrasHumalab = JsonConvert.DeserializeObject<MuestrasHumalabResponse>(objMuestras.ToString()!)!;
		}

        return muestrasHumalab;
    }

    //Enviar correo al cliente
    public int EnviarCorreoCliente(EnviarCorreoClienteRequest enviarCorreo)
    {
        var metodo = "/clientenuevo";
		int codigoRespuesta = 0;

		var url = microInterno.ServerCorreo + ":" + microInterno.PortCorreo + "/" + microInterno.RouteCorreo + metodo;
		var requestNew = (HttpWebRequest)WebRequest.Create(url);
		requestNew.Method = "POST";
		requestNew.ContentType = "application/json";

		var json = JsonConvert.SerializeObject(enviarCorreo);
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
				codigoRespuesta = JsonConvert.DeserializeObject<int>(responseText)!;
			}
		}

		//int codigoRespuesta = 0;
        //codigoRespuesta = CommunicatorCorreo.InvokeOperation<int, EnviarCorreoClienteRequest>(metodo, TipoOperacion.POST, enviarCorreo);
		return codigoRespuesta;
	}

    public string ClienteObservacion(int idUsuario)
    {
        var metodo = "/observacionCliente";
        string observacion = "";

		if (idUsuario > 0)
            metodo += string.Format("?idUsuario={0}", HttpUtility.UrlEncode(idUsuario.ToString()));

		var url = microInterno.ServerGestionCliente + ":" + microInterno.PortGestionCliente + "/" + microInterno.RouteGestionCliente + metodo;
		var request = (HttpWebRequest)WebRequest.Create(url)!;
		request.Method = "GET";

		using (var response = (HttpWebResponse)request.GetResponse())
		{
			using (var reader = new StreamReader(response.GetResponseStream()))
			{
				var responseText = reader.ReadToEnd();
				//observacion = JsonConvert.DeserializeObject<string>(responseText)!;
				observacion = responseText;
			}
		}

		//string observacion = CommunicatorGestionCliente.InvokeOperation<string>(metodo, TipoOperacion.GET);
        return observacion;
    }

    public ClienteDirecHuma DireccionClienteHumalab(string ruc, string accion)
    {
        var metodo = "/direccionCliente";
        var direccion = new ClienteDirecHuma();

		if (!string.IsNullOrEmpty(ruc))
            metodo += string.Format("?ruc={0}", HttpUtility.UrlEncode(ruc.ToString()));

        if(!string.IsNullOrEmpty(Convert.ToString(accion)))
			metodo += string.Format("&accion={0}", HttpUtility.UrlEncode(accion.ToString()));

		var url = microInterno.ServerGestionCliente + ":" + microInterno.PortGestionCliente + "/" + microInterno.RouteGestionCliente + metodo;
		var request = (HttpWebRequest)WebRequest.Create(url)!;
		request.Method = "GET";

		using (var response = (HttpWebResponse)request.GetResponse())
		{
			using (var reader = new StreamReader(response.GetResponseStream()))
			{
				var responseText = reader.ReadToEnd();
				direccion = JsonConvert.DeserializeObject<ClienteDirecHuma>(responseText)!;
			}
		}

		//var direccion = CommunicatorGestionCliente.InvokeOperation<ClienteDirecHuma>(metodo, TipoOperacion.GET);
        return direccion;
	}


    //Factura de un cliente
    public async Task<List<FacturaEstadosClienteResponse>> FacturacionEstadosFlexline(string Identificacion)
    {
        var metodo = "cartera-vencida";       

        if (!string.IsNullOrWhiteSpace(Identificacion))
            metodo += string.Format("?ruc={0}", HttpUtility.UrlEncode(Identificacion));

        if(!string.IsNullOrWhiteSpace(variables.EmpresaHumalab))
            metodo += string.Format("&empresa={0}", HttpUtility.UrlEncode(variables.EmpresaHumalab));

        using (var facturacionF = new HttpClient())
        {
            facturacionF.DefaultRequestHeaders.Add("Authorization", $"Bearer {microsExterno.TokenFlexline}");

            var resultado = await facturacionF.GetAsync("http://" + microsExterno.ServerFlexLine + ":" + microsExterno.PortFlexLine + microsExterno.PathServicesAsesorFlexline + metodo);

            if (!resultado.IsSuccessStatusCode)
                throw new ArgumentException("Problemas con el servicio de Flexline.");

            var resultadoJson = await resultado.Content.ReadAsStringAsync();
            var respuesta = JsonConvert.DeserializeObject<List<FacturaEstadosClienteResponse>>(resultadoJson);

            return respuesta!;
        }
    }
}
