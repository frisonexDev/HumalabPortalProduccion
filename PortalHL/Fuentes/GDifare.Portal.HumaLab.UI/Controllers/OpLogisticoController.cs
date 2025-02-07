using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using GDifare.Portales.HumaLab.Models;
using GDifare.Portal.Humalab.Servicio.PedidosOpLogistico;
using GDifare.Portal.HumaLab.UI.Models.Pedidos;
using System.Reflection;
using GDifare.Portal.Humalab.Servicio.Modelos.PedidosOpLogistico;
using Newtonsoft.Json;
using GDifare.Portal.Humalab.Seguridad.Modelos;
using Microsoft.AspNetCore;
using GDifare.Portal.Humalab.Servicio.Modelos;
using GDifare.Portal.Humalab.Servicio.GestionCliente;
using GDifare.Portal.Humalab.Servicio.Modelos.GestionCliente;
using System.Buffers.Text;
using System.Text;
using GDifare.Portal.Humalab.Servicio.Utils;
using GDifare.Portal.HumaLab.UI.Models;

namespace GDifare.Portales.HumaLab.UI.Controllers
{
    [Authorize]
    public class OpLogisticoController : Controller
    {
        private readonly PedidosOpLogisticoOperacion gestionPedidosOperations;
        private readonly AppServicioMicros _appServicioMicros;
        private readonly AppServicioMicrosExternos _servicioMicrosExternos;
        private readonly AppServicioClienteApi _microCliente;
        private readonly Variables _variables;        

        public OpLogisticoController(AppServicioMicros microInterno, AppServicioMicrosExternos microExterno, AppServicioClienteApi servicioCliente, Variables variables)
        {
            this._appServicioMicros = microInterno;
            this._servicioMicrosExternos = microExterno;
            this._microCliente = servicioCliente;
            this._variables = variables;
            gestionPedidosOperations = new PedidosOpLogisticoOperacion(_appServicioMicros, _servicioMicrosExternos, _microCliente, _variables);

        }        

        [HttpGet]
        public IActionResult Index()
        {
            try
            {

                ObjUsuario usuario = JsonConvert.DeserializeObject<ObjUsuario>(HttpContext.Session.GetString("usuario")!)!;
                int idOperador = 0;
                int.TryParse(usuario.Identificacion, out idOperador);

                PedidosOpLogistico model = new PedidosOpLogistico();
                model.ConsultaPedidos = new List<ConsultaPedidosOpLogistico>();
                model.BuscarPedidos = new BuscarPedidosOpLogistico();
                model.BuscarPedidos.Cliente = null;
                model.BuscarPedidos.BuscarPorIdentificacion = false;
                model.BuscarPedidos.FechaDesde = model.BuscarPedidos.FechaHasta = DateTime.Now;
                //model.BuscarPedidos.Estado = (usuario.RolID == Roles.RolOperador) ? "PREC" : null;
                model.BuscarPedidos.Estado = (usuario.RolID == Roles.RolOperador) ? "-1" : null;
                model.BuscarPedidos.IdOperadorLogistico = (usuario.RolID == Roles.RolOperador) ? idOperador : usuario.UsuarioID;
                //ViewBag.Estado = (usuario.RolID == Roles.RolOperador) ? "PREC" : "";
                ViewBag.Estado = (usuario.RolID == Roles.RolOperador) ? "-1" : "";
                ViewBag.DatosConsulta = model.BuscarPedidos;
                return PartialView(GetPedidosOpLogistico(model.BuscarPedidos));
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }



        [HttpPost]
        public IActionResult Index(BuscarPedidosOpLogistico model)
        {
            ViewBag.Estado = model.Estado;
            ViewBag.DatosConsulta = model;
            return PartialView(GetPedidosOpLogistico(model));
        }
        [HttpPost]
        public IActionResult regresarBusqueda(string modelJson, int idPedido)
        {
            ObjUsuario usuario = JsonConvert.DeserializeObject<ObjUsuario>(HttpContext.Session.GetString("usuario")!)!;
            int idOperador = 0;
            int.TryParse(usuario.Identificacion, out idOperador);
            BuscarPedidosOpLogistico model = JsonConvert.DeserializeObject<BuscarPedidosOpLogistico>(modelJson)!;
            PedidosOpLogistico m = new PedidosOpLogistico();
            m.ConsultaPedidos = new List<ConsultaPedidosOpLogistico>();
            m.BuscarPedidos = new BuscarPedidosOpLogistico();
            m.BuscarPedidos.Cliente = model.Cliente;
            m.BuscarPedidos.BuscarPorIdentificacion = model.BuscarPorIdentificacion;
            m.BuscarPedidos.FechaDesde = model.FechaDesde;
            m.BuscarPedidos.FechaHasta = model.FechaHasta;
            m.BuscarPedidos.Estado = model.Estado;
            m.BuscarPedidos.IdOperadorLogistico = (usuario.RolID == Roles.RolOperador) ? idOperador : usuario.UsuarioID;
            
            int idOperadorNew = (int)m.BuscarPedidos.IdOperadorLogistico;

            //hacer una consulta a la BD para actualizar el pedido sea recoletctato total
            //o parcial y si no queda en prec
            var pedidosActua = gestionPedidosOperations.ConsultarPedidosActualizados(idPedido, idOperadorNew);           

            ViewBag.Estado = "";
            ViewBag.CargaDatos = true;
            return View("Index", m);            
        }


        public IActionResult Recolectar(int id, string datos)
        {
            VerDetallePedidoOperadorResponse verMuestras = gestionPedidosOperations.VerDetallePedidoOperadorLogisticoNew(new VerDetallePedidoOperadorRequest
            {
				IdPedido = id
			});

			BuscarPedidosOpLogistico d = JsonConvert.DeserializeObject<BuscarPedidosOpLogistico>(datos)!;
			ViewBag.DatoConsulta = d;
			ViewBag.IdPedido = id;
			return View(verMuestras);

			//VerDetallePedidoOperadorResponse verMuestras = gestionPedidosOperations.VerDetallePedidoOperadorLogistico(new VerDetallePedidoOperadorRequest
			//{
			//    IdPedido = id
			//});

			//BuscarPedidosOpLogistico d = JsonConvert.DeserializeObject<BuscarPedidosOpLogistico>(datos)!;
			//ViewBag.DatoConsulta = d;
			//ViewBag.IdPedido = id;
			//return View(verMuestras);
		}

        
        public string RecolectarPDF(int id, bool impresion)
        {
            string base64Pdf = "";
            VerDetallePedidoOperadorResponse verMuestras = gestionPedidosOperations.VerDetallePedidoOperadorLogistico(new VerDetallePedidoOperadorRequest
            {
                IdPedido = id
            });

            PdfHumalab pdfHumalab = new PdfHumalab();

            if (impresion == true)
            {
                base64Pdf = pdfHumalab.GenerarDocumPedidoExcel(verMuestras);
            }
            else
            {
                base64Pdf = pdfHumalab.GenerarDocumPedido(verMuestras);
            }

            //string base64Pdf = gestionPedidosOperations.GenerarPdfPedido(verMuestras);
            
            byte[] pdfBytes = Convert.FromBase64String(base64Pdf);
            return base64Pdf;
            //return File(pdfBytes, "application/pdf");
        }

        [HttpPost]
        public IActionResult ActualizarEstadoMuestra(int idPruebaMuestra, bool recoger, bool rechazar, bool esOperador, string comentario)
        {
            ObjUsuario usuario = JsonConvert.DeserializeObject<ObjUsuario>(HttpContext.Session.GetString("usuario")!)!;
            int idOperador = 0;
            int.TryParse(usuario.Identificacion, out idOperador);

            RecogerMuestraRequest recogerMuestraRequest = new RecogerMuestraRequest()
            {
                IdMuestra = idPruebaMuestra,
                IdOperador = (usuario.RolID == Roles.RolOperador) ? idOperador : usuario.UsuarioID,
                NombreUsuario = usuario.Usuario,
                Observacion = comentario,
                Recolectar = recoger,
                Rechazar = rechazar,
                EsOperador = esOperador,
            };
            string recogerMuestra = gestionPedidosOperations.RecogerMuestraOperadorLogistico(recogerMuestraRequest);
            return Json(recogerMuestra);

        }
        // OPERACIONES
        //Búsqueda de pedidos
        private PedidosOpLogistico GetPedidosOpLogistico(BuscarPedidosOpLogistico model)
        {
            if (model.Estado == null)
            {
                model.Estado = "-1";
            }

            var pedidos = gestionPedidosOperations.ConsultarPedidosOperadorLogistico(new Portal.Humalab.Servicio.Modelos.PedidosOpLogistico.BuscarPedidoOperadorRequest
            {
                Cliente = model.Cliente,
                BuscarPorIdentificacion = model.BuscarPorIdentificacion,
                FechaDesde = model.FechaDesde,
                FechaHasta = model.FechaHasta,
                Estado = model.Estado,
                IdOperadorLogistico = model.IdOperadorLogistico,
            });
            PedidosOpLogistico pedidosOpLogistico = new PedidosOpLogistico();
            pedidosOpLogistico.BuscarPedidos = model;
            pedidosOpLogistico.ConsultaPedidos = (from i in pedidos
                                                  select new ConsultaPedidosOpLogistico
                                                  {
                                                      Cliente = i.Cliente,
                                                      EstadoPedido = i.EstadoPedido,
                                                      FechaPedido = i.FechaCreacion,
                                                      IdPedido = i.IdPedido,
                                                      FechaRetiro = i.FechaRetiro,
                                                      NumeroRemision = i.NumeroRemision,
                                                      SeleccionPedido = false,
                                                      TotalMuestras = i.TotalMuestras,
                                                      TotalOrdenes = i.TotalOrdenes,
                                                      TotalRetiradas = i.TotalRetiradas,
                                                      Paciente = i.Paciente,
                                                      NomLab = i.NomLaboratorio
                                                  }).ToList();

            return pedidosOpLogistico;
        }

        //[HttpPost]
        //public IActionResult RecolectarPedido(RecogerPedidoRequest request)
        //{
        //    try
        //    {
        //        ObjUsuario usuario = JsonConvert.DeserializeObject<ObjUsuario>(HttpContext.Session.GetString("usuario"));
        //        int idOperador = 0;
        //        int.TryParse(usuario.Identificacion, out idOperador);

        //        request.IdOperador = (usuario.RolID == Roles.RolOperador) ? idOperador : usuario.UsuarioID;

        //        string recogerPedido = gestionPedidosOperations.RecogerPedido(request);
        //        RecogerPedidoResponse response = JsonConvert.DeserializeObject<RecogerPedidoResponse>(recogerPedido);

        //        return Json(response);
        //    }
        //    catch (Exception ex) { return Json(ex.Message); }


        //}

        [HttpPost]
        public IActionResult RecolectarPedido(RecogerPedidosRequest request)
        {
            try
            {
                RecogerPedidoRequest recoger = new();

                ObjUsuario usuario = JsonConvert.DeserializeObject<ObjUsuario>(HttpContext.Session.GetString("usuario")!)!;
                int idOperador = 0;
                int.TryParse(usuario.Identificacion, out idOperador);

                request.IdOperador = (usuario.RolID == Roles.RolOperador) ? idOperador : usuario.UsuarioID;

                string[] valorPedido = request.IdPedido.Split(',');
                string recogerPedido = "";

                foreach (var idpedido in valorPedido)
                {
                    recoger.IdPedido = Convert.ToInt32(idpedido);
                    recoger.ObservacionOperador = request.ObservacionOperador;
                    recoger.IdOperador = request.IdOperador;

                    recogerPedido = gestionPedidosOperations.RecogerPedido(recoger);
                }
                
                RecogerPedidoResponse response = JsonConvert.DeserializeObject<RecogerPedidoResponse>(recogerPedido)!;

                return Json(response);
            }
            catch (Exception ex) { return Json(ex.Message); }


        }

        [HttpPost]
        public IActionResult EntregarPedido(string pedidos) 
        {
            ObjUsuario usuario = JsonConvert.DeserializeObject<ObjUsuario>(HttpContext.Session.GetString("usuario")!)!;
            int idOperador = 0;
            int.TryParse(usuario.Identificacion, out idOperador);

            EntregarPedidoRequest req = new EntregarPedidoRequest();
            req.Pedidos = pedidos;
            req.IdOperador = (usuario.RolID == Roles.RolOperador) ? idOperador : usuario.UsuarioID;
            req.ObservacionOperador = "Pedidos entregados";
            string entregarPedido = gestionPedidosOperations.EntregarPedido(req);
            EntregarPedidoResponse response = JsonConvert.DeserializeObject<EntregarPedidoResponse>(entregarPedido)!;
            
            return Json(response.Entregado);
        }

        //Estados del catalogo
        public List<EstadosCatalogoResponse> EstadosPedido(string nombreMaestro)
        {
            List<EstadosCatalogoResponse> estados = gestionPedidosOperations.EstadosCatalogo(nombreMaestro);
            return estados;
        }

        //Verifica si se actualizo un pedido
        public string VerificarPedidoAct(int idPedido)
        {
            string actualizado = gestionPedidosOperations.VerificarActPed(idPedido);

            return actualizado;
        }

        public async Task<object> OperadoresHl()
        {
            try
            {
                var buscarAsesor = await gestionPedidosOperations.BuscarAsesoresFlexline();
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
    }
}
