using GDifare.Portal.Humalab.Servicio.GestionCliente;
using GDifare.Portal.Humalab.Servicio.Modelos;
using GDifare.Portal.Humalab.Servicio.Modelos.GestionCliente;
using GDifare.Portales.HumaLab.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GDifare.Portales.HumaLab.UI.Controllers;

[Authorize]
public class GestionClienteController : Controller
{

    private readonly GestionClienteOperacion gestionClienteOperations;
    private readonly AppServicioMicros _appServicioMicros;
    private readonly AppServicioMicrosExternos _servicioMicrosExternos;
    private readonly Variables _variables;

    public GestionClienteController(AppServicioMicros microInterno, AppServicioMicrosExternos microExterno, Variables variables, AppSettings appSettings)
    {
        this._appServicioMicros = microInterno;
        this._servicioMicrosExternos = microExterno;
        this._variables = variables;

        gestionClienteOperations = new GestionClienteOperacion(_appServicioMicros, _servicioMicrosExternos, _variables, appSettings.UserAvalab, appSettings.PassAvalab);
    }

    public IActionResult Index(int idMaestro)
    {
        try
        {
            List<EstadosHumalabClienteResponse> clientesHuma = gestionClienteOperations.EstadosHumalabCliente();

            return View();
        }
        catch (Exception ex)
        {
            return View();
        }
    }

    //Estados de los clientes
    public List<EstadosHumalabClienteResponse> EstadosClienteHuma()
    {
        List<EstadosHumalabClienteResponse> clientesHuma = gestionClienteOperations.EstadosHumalabCliente();
        return clientesHuma;
    }

    //Busca cliente o clientes en específico en base al ruc o nombre
    public ClientesFinal BuscarClienteHumalab(ClienteHumalabRequest clientesHumalab)
    {
        ClientesFinal clienteHumalab = new();

        try
        {
            clienteHumalab = gestionClienteOperations.ConsultarClienteHuma(clientesHumalab);
            if (clienteHumalab.dataClientes.Count > 0)
            {
                clienteHumalab.codigoRespuesta = "00";
            }
            else
            {
                clienteHumalab!.codigoRespuesta = "01";
                clienteHumalab.mensajeRespuesta = "No existen datos.";
            }
        }
        catch (Exception ex)
        {
            clienteHumalab.mensajeRespuesta = "Error";
        }

        return clienteHumalab;
    }

    //Retorna clientes humalab
    public ClientesFinal ClientesHumalab(ClientesRequest clientes)
    {
        ClientesFinal clientesHumalab = new();

        try
        {
            clientesHumalab = gestionClienteOperations.ConsultarClientesTodos(clientes);
            if (clientesHumalab.dataClientes.Count > 0)
            {
                clientesHumalab.codigoRespuesta = "00";
            }
            else
            {
                clientesHumalab!.codigoRespuesta = "01";
                clientesHumalab.mensajeRespuesta = "No existen datos.";
            }
        }
        catch (Exception ex)
        {
            clientesHumalab.mensajeRespuesta = "Error";
        }

        return clientesHumalab;
    }

    //Modifica al cliente humalab
    public ModificarClienteResponse ModificarClienteHumalab(ModificarClienteRequest modificarCliente)
    {   
        ModificarClienteResponse modiCliente = new();

        try
        {
            modiCliente = gestionClienteOperations.ModificarClienteHumalab(modificarCliente);
        }
        catch (Exception ex)
        {
            modiCliente.codigoRespuesta = "01";
            modiCliente.mensajeRespuesta = "Se ha producido un error en el sistema, comuníquese con el área de sistemas";
        }
        return modiCliente;
    }

    //Dar de alta cliente metodo busqueda al flexline
    public async Task<object> BuscarClienteFlexline(string clienteFlexline)
    {   
        try
        {
            var buscarCliente = await gestionClienteOperations.BuscarClienteFlexline(clienteFlexline);
            if (buscarCliente.Count > 0)
            {
                return buscarCliente;
            }
            else
            {   
                return "01";
            }
        }
        catch (Exception ex)
        {
            return "Error";
        }
    }

    //Asesores de flexline
	public async Task<object> BuscarAsesoresFlexline()
	{
		try
		{
			var buscarAsesor = await gestionClienteOperations.BuscarAsesoresFlexline();
			if (buscarAsesor.Count > 0)
			{
				return buscarAsesor;
			}
			else
			{
				return "01";
			}
		}
		catch (Exception ex)
		{
			return "Error";
		}
	}

	//Usuarios Galileo tipo cliente
	public async Task<object> BuscarUsuariosGalileo(string usuarioRuc)
    {
        try
        {

            //string usuario = Convert.ToString(empresaId);
            var buscarGalileo = await gestionClienteOperations.BuscarUsuariosGalileo(usuarioRuc);

            if (buscarGalileo != null)
            {
                return buscarGalileo;
            }
            else
            {
                return "01";
            }

        }
        catch (Exception ex)
        {
            return "Error";
        }
    }

    //Dar de alta cliente (guardar nuevo registro)
    public DarAltaClienteHumalabResponse DarAltaClienteHuma(DarAltaClienteHumalabRequest darAltaCliente)
    {
        DarAltaClienteHumalabResponse darAltaClienteHuma = new();

        try
        {
            darAltaClienteHuma = gestionClienteOperations.DarAltaClienteHuma(darAltaCliente);
        }
        catch (Exception ex)
        {
            darAltaClienteHuma.codigoRespuesta = "01";
            darAltaClienteHuma.mensajeRespuesta = "Se ha producido un error en el sistema, comuníquese con el área de sistemas";
        }

        return darAltaClienteHuma;
    }

    public DarAltaClienteHumalabResponse ModicarHumalabDarAlta(DarAltaClienteHumalabRequest darAltaCliente)
    {
        DarAltaClienteHumalabResponse darAltaClienteHuma = new();

        try
        {
			darAltaClienteHuma = gestionClienteOperations.ModiClienteHumalabDarAlta(darAltaCliente);
		}
        catch (Exception ex)
        {
			darAltaClienteHuma.codigoRespuesta = "01";
			darAltaClienteHuma.mensajeRespuesta = "Se ha producido un error en el sistema, comuníquese con el área de sistemas";
		}

		return darAltaClienteHuma;
	}


    //Pedidos de humalab
    public List<PedidoHumalabResponse> PedidosHumalab(int idCliente)
    {
        List<PedidoHumalabResponse> pedidoHumalab = gestionClienteOperations.PedidosHumalab(idCliente);
        return pedidoHumalab;
    }

    public PedidoOrdenHumalabResponse PedidosOrdenHumalab(string idPedido, int idCliente)
    {
        PedidoOrdenHumalabResponse pedOrdenHumalabResponses = gestionClienteOperations.PedidosOrdenHumalab(idPedido, idCliente);
        return pedOrdenHumalabResponses;
    }

    public GraficarPedidosHumalabResponse GraficarPedidosHumalab(GraficarPedidosHumalabRequest graficarPedidos)
    {
        GraficarPedidosHumalabResponse graficarPedidosHumalab = gestionClienteOperations.GraficarPedidosHumalab(graficarPedidos);
        return graficarPedidosHumalab;
    }

    public GraficarPedidosHumalabResponse GraficarPedidosTodosHumalab(int idCliente)
    {
		GraficarPedidosHumalabResponse graficarPedidosHumalab = gestionClienteOperations.GraficarPedidosTodosHumalab(idCliente);
		return graficarPedidosHumalab;
    }

    //Muestras de Humalab
    public MuestrasHumalabResponse MuestrasHumalab(int idCliente)
    {
	    MuestrasHumalabResponse muestrasHumalab = gestionClienteOperations.MuestrasHumalab(idCliente);
        return muestrasHumalab;
    }

    //Enviar correo cuando se da de alta a un cliente
    public int EnviarCorreoClienteHuma(EnviarCorreoClienteRequest enviarCorreoCliente)
    {        
        var enviar = gestionClienteOperations.EnviarCorreoCliente(enviarCorreoCliente);
        return enviar;
    }

    public string ObservacionCliente(int idUsuario)
    {
        var observacion = gestionClienteOperations.ClienteObservacion(idUsuario);
        return observacion;
    }

    public ClienteDirecHuma DireccionHumaCliente(string ruc, string accion)
    {
        var direcCliente = new ClienteDirecHuma();

		try
        {
			direcCliente = gestionClienteOperations.DireccionClienteHumalab(ruc, accion);

            if (direcCliente.Codigo == "00")
            {
				return direcCliente;
			}
			else
			{
				direcCliente.Codigo = "01";
			}
		}
        catch (Exception ex)
        {
            direcCliente.Codigo = "Error";
		}        

        return direcCliente;
    }

    //Facturacion valor total cliente
    public async Task<object> FacturacionEstadosFlexline(string Identificacion)
    {
        try
        {
            var facturacionCliente = await gestionClienteOperations.FacturacionEstadosFlexline(Identificacion);
            if (facturacionCliente.Count > 0)
            {
                return facturacionCliente;
            }
            else
            {
                return "01";
            }
        }
        catch (Exception ex)
        {
            return "Error";
        }
    }
}
