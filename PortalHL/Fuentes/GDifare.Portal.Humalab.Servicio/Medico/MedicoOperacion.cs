using GDifare.Portal.Humalab.Servicio.Modelos;
using GDifare.Portal.Humalab.Servicio.Modelos.Medico;
using GDifare.Portal.Humalab.Servicio.Modelos.Orden;
using GDifare.Portal.Humalab.Servicio.Utils;
using GDifare.Portales.Comunicaciones;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDifare.Portal.Humalab.Servicio.Medico;

public class MedicoOperacion
{
    private readonly AppServicioMicrosExternos _microsExterno;
    private readonly Variables _variables;
    private readonly Communicator CommunicatorGestionOrdenes;

    public MedicoOperacion(AppServicioMicrosExternos servicioMicrosExternos, 
                           Variables variables, AppServicioClienteApi configuracion)
    {
        CommunicatorGestionOrdenes = new Communicator(configuracion.Server, configuracion.Port, configuracion.RouteCliente, configuracion.Token);
        _variables = variables;
        _microsExterno = servicioMicrosExternos;
    }

    public List<ResultadosMedico> MedicoResultadosLab(ConsultarResultados consultar)
    {
        string FechaIniciall = consultar.FechaInicio.GetDateTimeFormats('d')[0].Replace("/", "d");
        string FechaFinall = consultar.FechaFin.GetDateTimeFormats('d')[0].Replace("/", "d");

        string FechaInicial = consultar.FechaInicio.ToString("dd'\\d'MM'\\d'yyyy");
        string FechaFinal = consultar.FechaFin.ToString("dd'\\d'MM'\\d'yyyy");

        List<ResultadosMedico> resultMedico = new List<ResultadosMedico>();
        try
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append("/listarResultadosLab?");
            stringBuilder.Append("OpcionBusqueda=" + consultar.OpcionBusqueda + "&");
            stringBuilder.Append("opcionEstado=" + consultar.opcionEstado + "&");
            stringBuilder.Append("DatoBusqueda=" + consultar.DatoBusqueda + "&");            
            stringBuilder.Append("CodigoBarra=" + consultar.CodigoBarra + "&");
            stringBuilder.Append("idLaboratorio=" + consultar.idLaboratorio + "&");
            stringBuilder.Append("FechaInicio=" + FechaInicial + "&");
            stringBuilder.Append("FechaFin=" + FechaFinal + "&");
            stringBuilder.Append("Sedes=" + consultar.Sedes);
            var queryString = stringBuilder.ToString();

            object objResultados = CommunicatorGestionOrdenes.InvokeOperation<object>(queryString, TipoOperacion.GET);
            resultMedico = JsonConvert.DeserializeObject<List<ResultadosMedico>>(objResultados.ToString()!)!;

            return resultMedico;
        }
        catch (Exception ex)
        {
            return resultMedico;
        }
    }

    public string ResultadosLabMedico(string codBarra, int IdResultado)
    {
        var error = string.Empty;

        try
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append("/resultadoPdfinalMedico?");
            stringBuilder.Append("codBarra=" + codBarra + "&");
            stringBuilder.Append("idResult=" + IdResultado);
            var queryString = stringBuilder.ToString();

            string obs = CommunicatorGestionOrdenes.InvokeOperation<string>(queryString, TipoOperacion.GET);
            return obs;
        }
        catch (Exception ex)
        {
            return error;
        }
    }
}
