using GDifare.Portal.Humalab.Servicio.Modelos.CarteraEstado;
using GDifare.Portal.Humalab.Servicio.Modelos.GestionCliente;
using GDifare.Portal.Humalab.Servicio.Resources;
using GDifare.Portales.Comunicaciones;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace GDifare.Portal.Humalab.Servicio.CarteraCliente;

public class CarteraOperacion
{
    private readonly Communicator CommunicatorHumalab;
    private string serverFlexLine;
    private int portFlexLine;
    private string tokenFlexLine;
    private string _PathServicesClientesFlexLine;

    private string ServerAdministracion;
    private int PortAdministracion;
    private string RouteAdministracion;
    private string RouteFlexCartera;

    public CarteraOperacion(string Server, int Port, string token, string PathServicesCarteraEstado,
                            string ServerFlexLine, int PortFlexLine, string TokenFlexLine, string PathServicesClientesFlexLine,
                            string ServerAdmin, int PortAdmin, string RouteAdmin, string RouteFlexCart)
    {
        CommunicatorHumalab = new Communicator(Server, Port, PathServicesCarteraEstado, token);
        serverFlexLine = ServerFlexLine;
        portFlexLine = PortFlexLine;
        tokenFlexLine = TokenFlexLine;
        _PathServicesClientesFlexLine = PathServicesClientesFlexLine;

        ServerAdministracion = ServerAdmin;
        PortAdministracion = PortAdmin;
        RouteAdministracion = RouteAdmin;

        RouteFlexCartera = RouteFlexCart;
    }

    public string ValidarCartera(int idRol, string ruc)
    {
        string result = string.Empty;
        if (ConsultarRolCliente(ruc))
        {
            string item = ConsultaEstadoHumalab(ruc);
            string item2 = ConsultaEstadoFlexline(ruc);
            (string, string) tuple = (item, item2);
            (string, string) tuple2 = tuple;
            switch (tuple2.Item1)
            {
                case "A":
                    {
                        string item3 = tuple2.Item2;
                        if (!(item3 == "A"))
                        {
                            if (item3 == "S")
                            {
                                ActualizaEstadoHumalab(ruc, "S");
                                result = "S";
                            }
                        }
                        else
                        {
                            result = "A";
                        }

                        break;
                    }
                case "T":
                    {
                        string item3 = tuple2.Item2;
                        if (!(item3 == "A"))
                        {
                            if (item3 == "S")
                            {
                                result = "T";
                            }
                        }
                        else
                        {
                            ActualizaEstadoHumalab(ruc, "A");
                            result = "A";
                        }

                        break;
                    }
                case "S":
                    {
                        string item3 = tuple2.Item2;
                        if (!(item3 == "A"))
                        {
                            if (item3 == "S")
                            {
                                result = "S";
                            }
                        }
                        else
                        {
                            ActualizaEstadoHumalab(ruc, "A");
                            result = "A";
                        }

                        break;
                    }
            }
        }
        else
        {
            result = "A";
        }

        return result;
    }

    public bool ConsultarRolCliente(string ruc)
    {
        try
        {
            string text = "/existeCliente?";
            if (!string.IsNullOrWhiteSpace(ruc))
            {
                text += $"ruc={HttpUtility.UrlEncode(ruc)}";
            }

            var url = ServerAdministracion + ":" + PortAdministracion + "/" + RouteAdministracion + text;
            var request = (HttpWebRequest)WebRequest.Create(url)!;
            request.Method = "GET";

            using (var response = (HttpWebResponse)request.GetResponse())
            {
                using (var reader = new StreamReader(response.GetResponseStream()))
                {
                    var responseText = reader.ReadToEnd();

                    return JsonConvert.DeserializeObject<bool>(responseText)!;
                }
            }
            //return CommunicatorHumalab.InvokeOperation<bool>(text, (TipoOperacion)1);
        }
        catch (Exception)
        {
            return false;
        }
    }

    public string ConsultaEstadoHumalab(string ruc)
    {
        ClienteHumalab clienteHumalab = new();
        try
        {
            string text = "/consultarEstadoHumalab?";
            if (!string.IsNullOrWhiteSpace(ruc))
            {
                text += $"ruc={HttpUtility.UrlEncode(ruc)}";
            }

            var url = ServerAdministracion + ":" + PortAdministracion + "/" + RouteAdministracion + text;
            var request = (HttpWebRequest)WebRequest.Create(url)!;
            request.Method = "GET";

            using (var response = (HttpWebResponse)request.GetResponse())
            {
                using (var reader = new StreamReader(response.GetResponseStream()))
                {
                    var responseText = reader.ReadToEnd();
                    clienteHumalab = JsonConvert.DeserializeObject<ClienteHumalab>(responseText)!;

                    if (clienteHumalab == null || clienteHumalab.Estadocliente == null)
                    {
                        return "S";
                    }

                    return clienteHumalab.Estadocliente;
                }
            }
            //ClienteHumalab clienteHumalab = CommunicatorHumalab.InvokeOperation<ClienteHumalab>(text, (TipoOperacion)1);           
        }
        catch (Exception)
        {
            return "S";
        }
    }

    public int ActualizaEstadoHumalab(string ruc, string estado)
    {
        try
        {
            string text = "/actualizaEstado?";
            if (!string.IsNullOrWhiteSpace(ruc))
            {
                text += $"ruc={HttpUtility.UrlEncode(ruc)}";
            }

            if (!string.IsNullOrWhiteSpace(estado))
            {
                text += $"&estado={HttpUtility.UrlEncode(estado)}";
            }

            var url = ServerAdministracion + ":" + PortAdministracion + "/" + RouteAdministracion + text;
            var request = (HttpWebRequest)WebRequest.Create(url)!;
            request.Method = "GET";

            using (var response = (HttpWebResponse)request.GetResponse())
            {
                using (var reader = new StreamReader(response.GetResponseStream()))
                {
                    var responseText = reader.ReadToEnd();
                    return JsonConvert.DeserializeObject<int>(responseText)!;
                }
            }
        }
        catch (Exception)
        {
            return 0;
        }
    }


    public string ConsultaEstadoFlexline(string ruc)
    {
        try
        {
            Task<ClienteFlexline> task = comunicatorFlexLine(ruc);
            if (task.Result == null)
            {
                return "S";
            }

            return task.Result.EstadoCartera ? "A" : "S";
        }
        catch (Exception)
        {
            return "S";
        }
    }

    private async Task<ClienteFlexline> comunicatorFlexLine(string ruc)
    {
        string metodo = "cartera?";
        if (!string.IsNullOrWhiteSpace(ruc))
        {
            metodo += $"ruc={HttpUtility.UrlEncode(ruc)}";
        }

        using HttpClient client = new HttpClient();
        client.DefaultRequestHeaders.Add("Authorization", "Bearer " + tokenFlexLine);
        HttpResponseMessage result = await client.GetAsync("http://" + serverFlexLine + ":" + portFlexLine + _PathServicesClientesFlexLine + "/" + metodo);
        if (!result.IsSuccessStatusCode)
        {
            throw new ArgumentException("something bad happended");
        }

        return JsonConvert.DeserializeObject<ClienteFlexline>(await result.Content.ReadAsStringAsync())!;
    }
}
