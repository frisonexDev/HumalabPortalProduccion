using GDifare.Portal.Humalab.Seguridad.Modelos;
using GDifare.Portal.Humalab.Servicio.Cliente;
using GDifare.Portal.Humalab.Servicio.GestionCliente;
using GDifare.Portal.Humalab.Servicio.Modelos;
using GDifare.Portal.Humalab.Servicio.Modelos.Orden;
using GDifare.Portal.Humalab.Servicio.Modelos.PedidosCliente;
using GDifare.Portal.Humalab.Servicio.Modelos.PedidosLaboratorista;
using GDifare.Portal.Humalab.Servicio.Modelos.PedidosOpLogistico;
using GDifare.Portal.Humalab.Servicio.PedidosOpLogistico;
using GDifare.Portal.HumaLab.UI.Models.Pedidos;
using GDifare.Portales.HumaLab.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OfficeOpenXml;
using System.Data;
using System.Reflection;

namespace GDifare.Portales.HumaLab.UI.Controllers
{
    [Authorize]
    public class LaboratoristaController : Controller
    {
        private readonly PedidosLaboratoristaOperacion gestionPedidosOperations;
        private readonly AppServicioMicros _appServicioMicros;
        private readonly AppServicioMicrosExternos _servicioMicrosExternos;
        private readonly Variables _variables;
        public LaboratoristaController(AppServicioMicros microInterno, AppServicioMicrosExternos microExterno, Variables variables, AppSettings appSettings)
        {
            this._appServicioMicros = microInterno;
            this._servicioMicrosExternos = microExterno;
            this._variables = variables;
            gestionPedidosOperations = new PedidosLaboratoristaOperacion(_appServicioMicros, _servicioMicrosExternos, _variables, appSettings.UserAvalab, appSettings.PassAvalab);

        }
        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                PedidosLaboratorista model = new PedidosLaboratorista();
                model.ConsultaPedidosLab = new List<ConsultaPedidosLaboratorista>();
                model.BuscarPedidosLab = new BuscarPedidosLaboratorista();
                model.BuscarPedidosLab.FechaDesde = model.BuscarPedidosLab.FechaHasta = DateTime.Now;
                model.BuscarPedidosLab.FechaDesde = model.BuscarPedidosLab.FechaHasta = DateTime.Now;
                ViewBag.DatosConsulta = model.BuscarPedidosLab;
                return View(GetPedidosLaboratorista(model.BuscarPedidosLab));
            }
            catch (Exception ex)
            {
                // En caso de error, puedes registrar el error (por ejemplo, en un archivo de registro) y devolver una respuesta de error.
                // Puedes personalizar el mensaje de error según tus necesidades.
                return BadRequest("Se produjo un error al cargar los datos: " + ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Index(BuscarPedidosLaboratorista model)
        {
            ViewBag.DatosConsulta = model;
            ViewBag.Estado = model.EstadoPedido;
            return View(GetPedidosLaboratorista(model));
        }

        [HttpGet]
        public IActionResult PedidosLab(int id, string datos, string estado)
        {
            BuscarPedidosLaboratorista input = new BuscarPedidosLaboratorista { IdOrden = id };
            BuscarPedidosLaboratorista d = JsonConvert.DeserializeObject<BuscarPedidosLaboratorista>(datos)!;
            ViewBag.DatosConsulta = d;
            ViewBag.Estado = estado;
            return View(VerOrdenLaboratorista(input));
        }
        [HttpGet]
        public IActionResult ActualizarMuestras(int idPrueba)
        {
            return PartialView("~/Views/Componentes/TablaMuestrasLab.cshtml", idPrueba);
        }

        [HttpPost]
        public IActionResult regresarBusqueda(string modelJson)
        {
            PedidosLaboratorista m = new PedidosLaboratorista();
            BuscarPedidosLaboratorista model = JsonConvert.DeserializeObject<BuscarPedidosLaboratorista>(modelJson)!;
            m.ConsultaPedidosLab = new List<ConsultaPedidosLaboratorista>();
            m.BuscarPedidosLab = new BuscarPedidosLaboratorista();
            m.BuscarPedidosLab.FechaDesde = model.FechaDesde;
            m.BuscarPedidosLab.FechaHasta = model.FechaHasta;
            m.BuscarPedidosLab.BuscarPorRuc = model.BuscarPorRuc;
            m.BuscarPedidosLab.IdPedido = model.IdPedido;
            m.BuscarPedidosLab.EstadoPedido = model.EstadoPedido;
            m.BuscarPedidosLab.UsuarioOperador = model.UsuarioOperador;
            m.BuscarPedidosLab.Cliente = model.Cliente;
            m.BuscarPedidosLab.IdOrden = model.IdOrden;
            //return View(GetPedidosLaboratorista(model.BuscarPedidosLab));
            ViewBag.CargaDatos = true;
            return View("Index", m);
            //return PartialView("Index",GetPedidosOpLogistico(model));
        }

        public PedidosLaboratorista GetPedidosLaboratorista(BuscarPedidosLaboratorista model)
        {
            try
            {
                var pedidos = gestionPedidosOperations.ConsultarOrdenesLaboratorista(new Portal.Humalab.Servicio.Modelos.PedidosLaboratorista.BuscarPedidoLaboratoristaRequest
                {
                    FechaDesde = model.FechaDesde,
                    FechaHasta = model.FechaHasta,
                    Operador = model.UsuarioOperador,
                    BuscarPorRuc = model.BuscarPorRuc,
                    Cliente = model.Cliente,
                    Estado = model.EstadoPedido,
                    IdPedido = model.IdPedido,
                    IdAsesor = model.IdAsesor,
                });
                PedidosLaboratorista pedidosLab = new PedidosLaboratorista();
                pedidosLab.BuscarPedidosLab = model;
                //Asigna valores de resumen
                pedidosLab.ResumenPedidosLab = pedidos.ResumenMuestra != null ? (new ResumenPedidosLaboratorista
                {
                    TotalMuestrasEntregadas = pedidos.ResumenMuestra.TotalMuestrasEntregadas,
                    TotalMuestrasRechazadas = pedidos.ResumenMuestra.TotalMuestrasRechazadas,
                    TotalMuestrasRecibidas = pedidos.ResumenMuestra.TotalMuestrasRecibidas,
                    TotalSinMuestra = pedidos.ResumenMuestra.TotalSinMuestra
                }) : new ResumenPedidosLaboratorista();

                //Asigna resultados de órdenes
                pedidosLab.ConsultaPedidosLab = pedidos.Ordenes != null ? (from i in pedidos.Ordenes
                                                                           select new ConsultaPedidosLaboratorista
                                                                           {
                                                                               IdPedido = i.IdPedido,
                                                                               IdOrden = i.IdOrden,
                                                                               CodigoBarraOrden = i.CodigoBarra,
                                                                               FechaCreacion = i.FechaCreacion,
                                                                               UsuarioOperador = i.UsuarioOperador,
                                                                               EstadoPedido = i.EstadoPedido,
                                                                               ObservacionMuestras = i.ObservacionMuestras != null ? i.ObservacionMuestras.Replace("¬", "\n").TrimStart() : null,
                                                                               Resultados = !string.IsNullOrEmpty(i.Resultados) ? i.Resultados : "-",
                                                                               IdentificacionPac = i.IdentificacionPac,
                                                                               NombresPac = i.NombresPac,
                                                                               ClienteNombre = i.ClienteNombre                                                                               
                                                                           }).ToList() : new List<ConsultaPedidosLaboratorista>();

                return pedidosLab;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public VerOrdenLaboratoristaResponse VerOrdenLaboratorista(BuscarPedidosLaboratorista model)
        {
            BuscarPedidoLaboratoristaRequest input = new BuscarPedidoLaboratoristaRequest();
            input.IdOrden = model.IdOrden;
            //var ordenesConsulta = gestionPedidosOperations.VerOrdenLaboratorista(new Portal.Humalab.Servicio.Modelos.PedidosLaboratorista.BuscarPedidoLaboratoristaRequest

            var ordenesConsulta = gestionPedidosOperations.VerOrdenLaboratorista(input);
            VerOrdenLaboratoristaResponse ordenesLab = new VerOrdenLaboratoristaResponse();
            //Asigna datos de la orden
            ordenesLab.Orden = ordenesConsulta.Orden != null ? (new GDifare.Portal.Humalab.Servicio.Modelos.PedidosLaboratorista.OrdenCab
            {
                Diagnostico = ordenesConsulta.Orden.Diagnostico,
                Medicamento = ordenesConsulta.Orden.Medicamento,
                Edad = ordenesConsulta.Orden.Edad,
                FechaNacimiento = ordenesConsulta.Orden.FechaNacimiento,
                Genero = ordenesConsulta.Orden.Genero,
                ObservacionCliente = ordenesConsulta.Orden.ObservacionCliente,
                ObservacionOpLogistico = ordenesConsulta.Orden.ObservacionOpLogistico,
                ObservacionOrden = ordenesConsulta.Orden.ObservacionOrden,
                FechaEnvio = ordenesConsulta.Orden.FechaEnvio,
                NombresPaciente = ordenesConsulta.Orden.NombresPaciente,
                Identificacion = ordenesConsulta.Orden.Identificacion                
            }) : new GDifare.Portal.Humalab.Servicio.Modelos.PedidosLaboratorista.OrdenCab();

            //Asigna resultados de órdenes
            ordenesLab.PruebasMuestras = ordenesConsulta.PruebasMuestras != null ? (from i in ordenesConsulta.PruebasMuestras
                                                                       select new GDifare.Portal.Humalab.Servicio.Modelos.PedidosLaboratorista.OrdenDet
                                                                       {
                                                                           IdOrden = i.IdOrden,
                                                                           Codigo = i.Codigo,
                                                                           CodigoBarra = i.CodigoBarra,
                                                                           IdMuestra = i.IdMuestra,
                                                                           IdPrueba = i.IdPrueba,
                                                                           Muestra = i.Muestra,
                                                                           PruebaPerfil = i.PruebaPerfil,
                                                                           PruebaRechazada = i.PruebaRechazada,
                                                                           ObservacionPrueba = i.ObservacionPrueba,
                                                                           ObservacionMuestra = i.ObservacionMuestra,
                                                                           EstadoMuestra = i.EstadoMuestra,
                                                                           Recibido = i.Recibido,
                                                                           Rechazado =  i.Rechazado
                                                                       }).ToList() : new List<GDifare.Portal.Humalab.Servicio.Modelos.PedidosLaboratorista.OrdenDet>();

            return ordenesLab;
        }

        //Envia orden a Galileo
        public async Task<object> EnviaOrdenGalileo(int id)
        {
            try
            {
                string json = gestionPedidosOperations.ConsultaOrdenGalileo(id);

                //Solo para pruebas despues cambiar en el microservicio
				JObject data = JObject.Parse(json);
				data["idLaboratorio"] = _variables.empresaId;
				string jsonNew = JsonConvert.SerializeObject(data, Formatting.Indented);

				var buscarGalileo = await gestionPedidosOperations.EnviarOrdenGalileo(jsonNew);                

                return buscarGalileo;
            }
            catch (Exception ex)
            {
                return "Error";
            }
        }

        //Envia orden a Galileo
        public string RecibirOrden(int id, string codExterno)
        {
            try
            {
                string json = gestionPedidosOperations.RecibirOrden(id, codExterno);
                return json;

            }
            catch (Exception ex)
            {
                return "Error";
            }
        }

        public string CambiaEstadoPrueba(int idPrueba, int idMuestra, bool rechaza, string observacion)
        {
            ObjUsuario usuario = JsonConvert.DeserializeObject<ObjUsuario>(HttpContext.Session.GetString("usuario")!)!;
            CambiarPruebaRequest cambioPrueba = new CambiarPruebaRequest()
            {
                IdMuestra = idMuestra,
                IdPrueba = idPrueba,
                Rechaza = rechaza,
                Usuario = usuario.UsuarioID,
                Observacion = observacion == null ? String.Empty : observacion
            };
            CambiarPruebaResponse respuesta = gestionPedidosOperations.CambiarPruebaLaboratorista(cambioPrueba);
            return JsonConvert.SerializeObject(respuesta);

        }

        public List<PruebasResponse> ConsultaPruebasLaboratorista(int idMuestra)
        {
            try
            {
                List<PruebasResponse> pruebas = gestionPedidosOperations.ConsultaPruebas(idMuestra);
                return pruebas;
            }
            catch (Exception ex)
            {
                return new List<PruebasResponse>();
            }
        }
        
        public string ExportalLab([FromBody] List<ExportarDatosRequest> exportarDatos)
        {            
            List<ExportarDatosResponse> lstDatosLab = new List<ExportarDatosResponse>();            

            foreach (var datosLab in exportarDatos)
            {
                BuscarPedidoLaboratoristaRequest input = new BuscarPedidoLaboratoristaRequest
                {
                    IdOrden = datosLab.IdOrden
                };

                ExportarDatosResponse datosResponse = new ExportarDatosResponse
                {
                    CodigoBarraOrden = datosLab.CodigoBarraOrden,
                    NombresPac = datosLab.NombresPac,
                    IdentificacionPac = datosLab.IdentificacionPac,   
                    RucLab = datosLab.RucLab,
                    Operador = datosLab.Operador,
                    ClienteNombre = datosLab.ClienteNombre,
                    CodLaboratorio = datosLab.CodLaboratorio,
                    TipoPaciente = datosLab.TipoPaciente,
					muestras = new List<ListaMuestras>() // Inicializa la lista de muestras para esta respuesta
                };

                //consulta de ordenes y muestras
                var ordenesConsulta = gestionPedidosOperations.VerOrdenLaboratorista(input);

                string clienteNombre = ordenesConsulta.Orden.NombreCliente;
                datosResponse.NombreCliente = clienteNombre;

                string ciudadCliente = ordenesConsulta.Orden.CiudadCliente;
                datosResponse.CiudadCliente = ciudadCliente;

                string rucLab = ordenesConsulta.Orden.RucLab;
                datosResponse.RucLab = rucLab;

				string operador = ordenesConsulta.Orden.Operador;
				datosResponse.Operador = operador;

				string nombreC = ordenesConsulta.Orden.ClienteNombre;
                datosResponse.ClienteNombre = nombreC;

                string codLaboratorio = ordenesConsulta.Orden.CodLaboratorio;
                datosResponse.CodLaboratorio = codLaboratorio;

                string tipPaciente = ordenesConsulta.Orden.TipoPaciente;
                datosResponse.TipoPaciente = tipPaciente;


                //recorre el nombre de las muestras de la orden
                foreach (var muestra in ordenesConsulta.PruebasMuestras)
                {
                    ListaMuestras nuevaMuestra = new ListaMuestras
                    {
                        NombreMuestra = muestra.Muestra,
                        NombreExamen = muestra.PruebaPerfil,
                        CodExamen = muestra.CodigoExamen,
                        OrdenEstado = muestra.EstadoOrden,
                        FechaCreacion = muestra.FechaCreacion,
                        codLis = muestra.CodLis,
                        estadoPrueba = muestra.EstadoPrueba
                    };

                    datosResponse.muestras.Add(nuevaMuestra);                    
                }

                lstDatosLab.Add(datosResponse);
            }

            //Generar el excel
            string excelBase64 = ExcelLaboratorista(lstDatosLab);
            return excelBase64;
        }

        public string ExcelLaboratorista(List<ExportarDatosResponse> lstDatosLab)
        {
            string base64Pdf = "";
            DateTime now = DateTime.Now;
            string fechaExport = now.ToString("dd/MM/yyyy HH:mm:ss");

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            try
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (ExcelPackage package = new ExcelPackage(memoryStream))
                    {
                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Sheet1");

                        worksheet.Cells["A2"].Value = "POR RETIRAR";
                        worksheet.Cells["A3"].Value = "Generado " + fechaExport;                        

                        int rowStart = 5;

                        worksheet.Cells[rowStart, 1].Value = "Codigo Orden";
                        worksheet.Cells[rowStart, 2].Value = "Recipiente";
                        worksheet.Cells[rowStart, 3].Value = "Asunto";
                        worksheet.Cells[rowStart, 4].Value = "Código Examen";
                        worksheet.Cells[rowStart, 5].Value = "Nombre Examen";
                        worksheet.Cells[rowStart, 6].Value = "Laboratorio";
                        worksheet.Cells[rowStart, 7].Value = "Ciudad";
                        worksheet.Cells[rowStart, 8].Value = "Estado Orden";

                        worksheet.Cells[rowStart, 9].Value = "Ruc Laboratorio";
						worksheet.Cells[rowStart, 10].Value = "Asesor";
						worksheet.Cells[rowStart, 11].Value = "Cliente";
						worksheet.Cells[rowStart, 12].Value = "Codigo Laboratorio";
						worksheet.Cells[rowStart, 13].Value = "Tipo Paciente";
						worksheet.Cells[rowStart, 14].Value = "Fecha Creación";
						worksheet.Cells[rowStart, 15].Value = "Orden Lis";
                        worksheet.Cells[rowStart, 16].Value = "Estado Prueba";

						int row = rowStart + 1;

                        foreach(var datos in lstDatosLab)
                        {
                            //worksheet.Cells[row, 1].Value = datos.CodigoBarraOrden;
                            //worksheet.Cells[row, 3].Value = datos.NombresPac + "|" + datos.IdentificacionPac;
                            //worksheet.Cells[row, 5].Value = datos.NombreCliente;

                            if (datos.muestras != null && datos.muestras.Any())
                            {
                                foreach(var muestra in datos.muestras)
                                {
                                    worksheet.Cells[row, 1].Value = datos.CodigoBarraOrden;
                                    worksheet.Cells[row, 2].Value = muestra.NombreMuestra;
                                    worksheet.Cells[row, 3].Value = datos.NombresPac + "|" + datos.IdentificacionPac;
                                    worksheet.Cells[row, 4].Value = muestra.CodExamen;
                                    worksheet.Cells[row, 5].Value = muestra.NombreExamen;
                                    worksheet.Cells[row, 6].Value = datos.NombreCliente;
                                    worksheet.Cells[row, 7].Value = datos.CiudadCliente;
                                    worksheet.Cells[row, 8].Value = muestra.OrdenEstado;

                                    worksheet.Cells[row, 9].Value = datos.RucLab;
                                    worksheet.Cells[row, 10].Value = datos.Operador;
									worksheet.Cells[row, 11].Value = datos.ClienteNombre;
									worksheet.Cells[row, 12].Value = datos.CodLaboratorio;
                                    worksheet.Cells[row, 13].Value = datos.TipoPaciente;
                                    worksheet.Cells[row, 14].Value = muestra.FechaCreacion;
                                    worksheet.Cells[row, 15].Value = muestra.codLis;
                                    worksheet.Cells[row, 16].Value = muestra.estadoPrueba;

                                    row++;
                                }
                            }
                            else
                            {
                                worksheet.Cells[row, 1].Value = string.Empty;
                                worksheet.Cells[row, 2].Value = string.Empty;
                                worksheet.Cells[row, 3].Value = string.Empty;
                                worksheet.Cells[row, 4].Value = string.Empty;
                                worksheet.Cells[row, 5].Value = string.Empty;
                                worksheet.Cells[row, 6].Value = string.Empty;
                                worksheet.Cells[row, 7].Value = string.Empty;
                                worksheet.Cells[row, 8].Value = string.Empty;
								worksheet.Cells[row, 9].Value = string.Empty;
								worksheet.Cells[row, 10].Value = string.Empty;
								worksheet.Cells[row, 11].Value = string.Empty;
								worksheet.Cells[row, 12].Value = string.Empty;
								worksheet.Cells[row, 13].Value = string.Empty;
								worksheet.Cells[row, 14].Value = string.Empty;
								worksheet.Cells[row, 15].Value = string.Empty;
                                worksheet.Cells[row, 16].Value = string.Empty;

								row++;
                            }
                        }

                        package.Save();
                    }

                    byte[] excelBytes = memoryStream.ToArray();
                    base64Pdf = Convert.ToBase64String(excelBytes);
                }
            }
            catch(Exception ex)
            {
                return "01";
            }

            return base64Pdf;
        }
    }
}
