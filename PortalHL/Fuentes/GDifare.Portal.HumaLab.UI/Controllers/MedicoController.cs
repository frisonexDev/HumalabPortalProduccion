using GDifare.Portal.Humalab.Servicio.CatalogoPrueba;
using GDifare.Portal.Humalab.Servicio.Cliente;
using GDifare.Portal.Humalab.Servicio.Medico;
using GDifare.Portal.Humalab.Servicio.Modelos;
using GDifare.Portal.Humalab.Servicio.Modelos.Cliente;
using GDifare.Portal.Humalab.Servicio.Modelos.Medico;
using GDifare.Portal.Humalab.Servicio.Modelos.Orden;
using GDifare.Portal.Humalab.Servicio.PedidosOpLogistico;
using GDifare.Portal.Humalab.Servicio.Utils;
using GDifare.Portal.HumaLab.UI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GDifare.Portal.HumaLab.UI.Controllers
{
    [Authorize]
    public class MedicoController : Controller
    {

        private readonly MedicoOperacion gestionMedico;
        private readonly Variables _variables;
        private readonly GenerarResultPDF gestionarPDF;

        public MedicoController(AppServicioClienteApi configuracionCliente, AppServicioMicros _appServicioMicros, AppServicioMicrosExternos configurationApisExt, Variables variables)
        {
            _variables = variables;                                            
            gestionMedico = new MedicoOperacion(configurationApisExt, variables, configuracionCliente);            
            gestionarPDF = new GenerarResultPDF(configuracionCliente);            

        }

        // GET: MedicoController
        public ActionResult Index()
        {
            return View();
        }

        public IActionResult ConsultarResultadosMedico()
        {
            return View();
        }

        //GET: Listar resultados lab
        public string ListarResultadosLab(ConsultarResultados Valor)
        {
            List<ResultadosMedico> lista = gestionMedico.MedicoResultadosLab(Valor);
            string result = System.Text.Json.JsonSerializer.Serialize(lista);

            return result;
        }

        //PDF resultados medico base 64
        public string PDFResultadosLabMed(string CodigoBarra, int IdResultado)
        {
            string pdf = gestionMedico.ResultadosLabMedico(CodigoBarra, IdResultado);

            if (pdf != "" || pdf != null)
            {
                return pdf;
            }
            else
            {
                return "01";
            }
        }
    }
}
