using GDifare.Portal.Humalab.Servicio.CatalogoPrueba;
using GDifare.Portal.Humalab.Servicio.Cliente;
using GDifare.Portal.Humalab.Servicio.GestionCliente;
using GDifare.Portal.Humalab.Servicio.Modelos;
using GDifare.Portal.Humalab.Servicio.Modelos.CatalogoPruebas;
using GDifare.Portal.Humalab.Servicio.Modelos.Cliente;
using GDifare.Portal.Humalab.Servicio.Modelos.Galileo;
using GDifare.Portal.Humalab.Servicio.Modelos.Orden;
using GDifare.Portal.Humalab.Servicio.Modelos.Paciente;
using GDifare.Portal.Humalab.Servicio.Modelos.PedidosCliente;
using GDifare.Portal.Humalab.Servicio.Modelos.PedidosLaboratorista;
using GDifare.Portal.Humalab.Servicio.Modelos.PedidosOpLogistico;
using GDifare.Portal.Humalab.Servicio.PedidosOpLogistico;
using GDifare.Portal.Humalab.Servicio.Utils;
using GDifare.Portal.HumaLab.UI.Models;
using GDifare.Portales.HumaLab.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;
using Newtonsoft.Json;
using OfficeOpenXml;
using PdfSharpCore.Pdf.Content.Objects;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks.Dataflow;
using System.Web.Helpers;

namespace GDifare.Portales.HumaLab.UI.Controllers
{
    [Authorize]
    public class ClienteController : Controller
    {
        private static readonly Newtonsoft.Json.JsonSerializerSettings _options = new() { NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore };

        private readonly CatalogoPruebaOperacion catalogoOperacion;
        private readonly CatalogoDetalleOperacion catalogoDetalleOperacion;
        private readonly PedidosOpLogisticoOperacion gestionPedidosOperations;
        private readonly GestionOrdenes gestionarOrdenes;
		private readonly GestionPedido gestionarPedidos;
		private readonly GestionPaciente gestionarPaciente;
        private readonly GenerarResultPDF gestionarPDF;
        private readonly Variables variables;
        //Instancia de Clase
        DatosResponse result = new DatosResponse();

        public ClienteController(AppServicioClienteApi configuracionCliente,AppServicioMicros _appServicioMicros,
                                 AppServicioMicrosExternos configurationApisExt, Variables variables, AppSettings appSettings)
        {
            this.variables = variables;
            //Microservicio humalab
            catalogoOperacion = new CatalogoPruebaOperacion(configurationApisExt, appSettings.UserAvalab, appSettings.PassAvalab);
            catalogoDetalleOperacion = new CatalogoDetalleOperacion(configuracionCliente);
            gestionarOrdenes = new GestionOrdenes(configuracionCliente);
            gestionarPedidos = new GestionPedido(configuracionCliente, _appServicioMicros, variables, configurationApisExt, 
                                                 appSettings.UserAvalab, appSettings.PassAvalab);
            gestionarPaciente = new GestionPaciente(configuracionCliente);
            gestionarPDF = new GenerarResultPDF(configuracionCliente);
            gestionPedidosOperations = new PedidosOpLogisticoOperacion(_appServicioMicros, configurationApisExt, configuracionCliente, variables);

        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ConsultarOrdenes()
        {
            return View();
        }

        public IActionResult EditarOrden(int IdOrden)
        {
            Orden ordenresponse = new Orden();
            ordenresponse = gestionarOrdenes.ConsultarOrden(IdOrden);
            return View(ordenresponse);
        }

        public string ListarPruebas(int IdOrden, int IdUsuario)
        {
            string lista = gestionarOrdenes.ListarPruebas(IdOrden, IdUsuario);

            return lista;
        }


        public IActionResult NuevaOrden()
        {           
            return View();
        }

        public IActionResult Pedido() { 
            
            return View();  
        }

        public IActionResult Catalogo()
        {

            return View();
        }


        public string VisualizarPDF(string CodigoBarra)
        {
            PdfHumalab pdfHumalab = new PdfHumalab();

            int idOrden = Numeros.Cero;
            do
            {
                idOrden = gestionarOrdenes.NumeroOrden(CodigoBarra);
            } while (idOrden == Numeros.Cero);
            
            List<Muestras> lista = gestionarOrdenes.ListarMuestras(idOrden);

            List<CodigoBarrasPdf> codigoBarras = new List<CodigoBarrasPdf>();
            codigoBarras = gestionarPDF.MuestrasEtiquetas(lista);
			string etiquetas = pdfHumalab.GenerarDocumEti(codigoBarras);
			//string memoryStream = pdfHumalab.EtiquetaNew(codigoBarras);
            //string etiquetas = gestionarPDF.MuestrasEtiquetas(lista);

            return etiquetas;
        }

		public string PDFResultados(string CodigoBarra)
		{
			string pdf = gestionarOrdenes.Resultados(CodigoBarra);

            if (pdf != "" || pdf != null)
            {
                return pdf;
            }
            else
            {
                return "01";
            }
        }

        public string PDFResultadosNuevo(string CodigoBarra)
        {
            string pdf = gestionarOrdenes.ResultadosNuevoPdf(CodigoBarra);

            if (pdf != "" || pdf != null)
            {
                return pdf;
            }
            else
            {
                return "01";
            }
        }

        //obtiene cabezara orden PDF
        public async Task<CabecOrdenResul> PDFResultadosLisCabe(string CodigoBarra)
        {
            var pdfCabeLis = await catalogoOperacion.LisCabeceraResultado(CodigoBarra, variables.empresaId);
            return pdfCabeLis;
        }

        //obtiene datos paciente PDF
        public async Task<List<PacienteResultPdf>> PDFResultadosPacLis(int PacienteId)
        {
            var pdfPacLis = await catalogoOperacion.LisPacienteResultado(PacienteId, variables.empresaId);
            return pdfPacLis;
        }

        //obtiene datos medico PDF
        public async Task<MedicoResultPdf> PDFResultadosMedLis(int MedicoId)
        {
            var pdfMedLis = await catalogoOperacion.LisMedicoResultado(MedicoId, variables.empresaId);
            return pdfMedLis;
        }

        //obtiene pruebas de la orden PDF
        public async Task<List<PruebasResultPdf>> PDFResultadosPruebLis(string CodBarra)
        {
            var pdfPrueLis = await catalogoOperacion.LisPruebasResultado(CodBarra, variables.empresaId);
            return pdfPrueLis;
        }

        //actualizar el estado de la orden para el PDF
        public string ActuaEstadoLisOrdResult(ActualizaPdfOrden actualizaPdfOrden)
        {
            var resultado = gestionarOrdenes.ActuaLisOrdResultPdf(actualizaPdfOrden);
            return resultado;
        }

        //genera el PDF de los resultados        
        public string GenerarPdfResultados(bool todosVal, bool impresion, string pdfCabeOrdenPdf, 
                                           string ListaPdfPaciente, string ListaPdfMedico, string ListaPdfPruebas)
        {
            string base64Pdf = "";

            GenerarPdfResult resultados = new GenerarPdfResult();

            CabeceraOrden cabecera = JsonConvert.DeserializeObject<CabeceraOrden>(pdfCabeOrdenPdf)!;
            List<PacientesPdf> paciente = JsonConvert.DeserializeObject<List<PacientesPdf>>(ListaPdfPaciente)!;
            List<MedicoPdf> medico = JsonConvert.DeserializeObject<List<MedicoPdf>>(ListaPdfMedico)!;
            List<PruebasPdf> pruebas = JsonConvert.DeserializeObject<List<PruebasPdf>>(ListaPdfPruebas)!;

            GenPdfResultados genPdfResultados = new GenPdfResultados
            {
                cabecera = cabecera,
                paciente = paciente,
                medico = medico,
                pruebas = pruebas
            };

            if (impresion == true)
            {
                base64Pdf = resultados.GenerarDocumentoResultados(genPdfResultados, todosVal, impresion);
            }
            else
            {
                base64Pdf = resultados.GenerarDocumentoResultados(genPdfResultados, todosVal, impresion);
            }
            
            return base64Pdf;
        }

        public string ObtenerPDF(int IdOrden)
        {
            PdfHumalab pdfHumalab = new PdfHumalab();

            List<Muestras> lista = gestionarOrdenes.ListarMuestras(IdOrden);

            List<CodigoBarrasPdf> codigoBarras = new List<CodigoBarrasPdf>();
            codigoBarras = gestionarPDF.MuestrasEtiquetas(lista);
            string etiquetas = pdfHumalab.GenerarDocumEti(codigoBarras);

            //string etiquetas = gestionarPDF.MuestrasEtiquetas(lista);

            return etiquetas;
        }

        public string RecolectarPDF(int id, bool impresion)
        {
            string base64Pdf = "";
            VerDetallePedidoOperadorResponse verMuestras = gestionPedidosOperations.VerDetallePedidoOperadorLogistico(new VerDetallePedidoOperadorRequest
            {
                IdPedido = id
            });

            PdfHumalab pdfHumalab = new PdfHumalab();

            //excel
            if (impresion == true)
            {
                base64Pdf = pdfHumalab.GenerarDocumPedidoExcel(verMuestras);
            }
            //pdf
            else
            {
                base64Pdf = pdfHumalab.GenerarDocumPedido(verMuestras);
            }            

            //string base64Pdf = gestionPedidosOperations.GenerarPdfPedido(verMuestras);
            return base64Pdf;
        }

        public Task<string> CatalogoPruebas()
        {
            Task<string> ?catalogo = null;
            Task<string> ?menuJson = null;
            TimeSpan diferencia = TimeSpan.FromHours(0);
            TimeSpan ts = TimeSpan.FromHours(variables.DescargaCatalogo);
            string path = "catalogoMuestrasFinal.json";
            string sCurrentDirectory = Directory.GetCurrentDirectory();

			if (System.IO.File.Exists(Path.Combine(sCurrentDirectory,path)))
            {
                DateTime infoFile = System.IO.File.GetLastWriteTime(path);
                DateTime localDate = DateTime.Now;
                diferencia = localDate - infoFile;             
            }

            menuJson = System.IO.File.ReadAllTextAsync(path, Encoding.UTF8);            
            catalogo = menuJson;

            //System.IO.File.Delete(path);
            //catalogo = catalogoOperacion.ListaPruebas();
            //if (catalogo.Result != "01")
            //{                
            //    Task asyncTask = WriteFileAsync("", path, catalogo.Result);                
            //}

            //if (diferencia >= ts || diferencia == TimeSpan.FromHours(0))
            //{
            //    System.IO.File.Delete(path);
            //    catalogo = catalogoOperacion.ListaPruebas();
            //    if (catalogo.Result != "01")
            //    {
            //        Task asyncTask = WriteFileAsync("", path, catalogo.Result);
            //    }                
            //}
            //else
            //{
            //    menuJson = System.IO.File.ReadAllTextAsync(path, Encoding.UTF8);
            //    catalogo = menuJson;
            //}

            return catalogo;
        }

        public Task<string> CatalogoPruebasAvalab()
        {
            Task<string>? catalogo = null;
            Task<string>? menuJson = null;
            TimeSpan diferencia = TimeSpan.FromHours(0);
            TimeSpan ts = TimeSpan.FromHours(variables.DescargaCatalogo);
            string path = "catalogoMuestras.json";
            string sCurrentDirectory = Directory.GetCurrentDirectory();

            System.IO.File.Delete(path);
            catalogo = catalogoOperacion.ListaPruebas();
            if (catalogo.Result != "01")
            {
                Task asyncTask = WriteFileAsync("", path, catalogo.Result);
            }

            return catalogo;
        }

        public string CatalogoPruebasExcel()
        {
            string filePath = @"InfoPruebaPortal.xlsx";

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;            
            var excelData = new List<Dictionary<string, object>>();
            string jsonResult = "";

            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {
                var worksheet = package.Workbook.Worksheets[0];
                
                // Lee los encabezados
                var headerRow = new List<string>();
                for (int col = 1; col <= worksheet.Dimension.End.Column; col++)
                {
                    headerRow.Add(worksheet.Cells[1, col].Text);
                }

                // Lee las filas y columnas, omitiendo la primera fila (encabezados)                
                for (int row = 2; row <= worksheet.Dimension.End.Row; row++)
                {
                    var rowData = new Dictionary<string, object>();
                    for (int col = 1; col <= worksheet.Dimension.End.Column; col++)
                    {
                        var cellValue = worksheet.Cells[row, col].Text;
                        rowData[headerRow[col - 1]] = cellValue; // Asigna el valor de la celda al encabezado correspondiente
                    }
                    excelData.Add(rowData); // Agrega la fila al listado
                }

                jsonResult = JsonConvert.SerializeObject(excelData, Formatting.Indented);
            }

            return jsonResult;
        }

        public Task<string> CatalogoPruebasNew()
        {
            string path = "catalogoMuestrasFinal.json";
            Task<string>? catalogo = null;
            Task<string>? menuJson = null;

			if (System.IO.File.Exists(Path.Combine(path)))
			{
				menuJson = System.IO.File.ReadAllTextAsync(path, Encoding.UTF8);
				catalogo = menuJson;
			}
            else
            {
                catalogo = CatalogoPruebas();
			}
			//menuJson = System.IO.File.ReadAllTextAsync(path, Encoding.UTF8);
            //catalogo = menuJson;
            return catalogo;
        }


        public static async Task WriteFileAsync(string dir, string file, string content)
        {           
            using (StreamWriter outputFile = new StreamWriter(Path.Combine(dir, file)))
            {
                await outputFile.WriteAsync(content);
            }          
        }
        

        
        #region Acciones para la Orden
        public int ObtenerNOrden(ConsultarOrden Valor)
        {
            int nOrden = gestionarOrdenes.ConsultarNumeroOrden(Valor);

            return nOrden;
        }


        public string ListarOrdenes(ConsultarOrden Valor)
        {
            List<ListarOrden> lista = gestionarOrdenes.ListarOrden(Valor);

            string result = System.Text.Json.JsonSerializer.Serialize(lista); 

            return result;
        }

        public string ListarObservaciones(string CodigoBarra)
        {
            List<LogObservaciones> lista = gestionarOrdenes.Observaciones(CodigoBarra);

            string result = System.Text.Json.JsonSerializer.Serialize(lista);

            return result;
        }


        public string ListarPedidos(ConsultarPedido Buscar)
         {
            string lista = string.Empty;

            try
            {
                lista = gestionarPedidos.ListaDePedidos(Buscar);

                return lista;
            }
            catch (Exception)
            {

                return lista;
            }
        }


        public string ListarEstados(string NombreEstado)
        {
            List<CatalogoDetalle> lista = catalogoDetalleOperacion.ListarEstados(NombreEstado);

            string result = System.Text.Json.JsonSerializer.Serialize(lista);

            return result;
        }



        public int RegistrarOrden(Orden orden)
        {
            int result = Transaccion.Error;
            try
            {
                orden.EmpresaId = variables.empresaId;
                result = gestionarOrdenes.RegistrarOrden(orden);

                return result;
            }
            catch (Exception)
            {

                return result;
            }

        }

        public int ActualizarOrden(Orden orden)
        {
            int result = Transaccion.Error;
            try
            {

                result = gestionarOrdenes.ActualizarOrden(orden);

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

                result = gestionarOrdenes.EliminarOrden(orden);

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

                result = gestionarOrdenes.EliminarPruebas(prueba);

                return result;
            }
            catch (Exception)
            {

                return result;
            }
        }

        #endregion


        public Pacientes ObtenerPaciente(ConsultarPaciente datos)
        {
            Pacientes valor = gestionarPaciente.ConsultarPaciente(datos);

            return valor;

        }


		#region Acciones para el Pedido

		public int ObtenerNPedido(ConsultarPedido Valor)
        {
			int nPedido = gestionarPedidos.ConsultarNPedido(Valor);

			return nPedido;
		}


       
        public Operador ObtenerOperador(ConsultarPedido Valor)
        {
            var operador = new Operador();
			operador = gestionarPedidos.ConsultarOperador(Valor);

			return operador;
		}

        public int CrearPedido(Pedido Valor)
        {

			int result = Transaccion.Error;
            try
            {

                result = gestionarPedidos.CrearPedido(Valor);

				return result;
			}
			catch (Exception)
			{

				return result;
			}

		}

		public int EliminarPedido(int IdPedido, int UsuarioEliminacion)
        {
            int result = Transaccion.Error;
            try
            {

                result = gestionarPedidos.BorrarPedido(IdPedido, UsuarioEliminacion);

                return result;
            }
            catch (Exception)
            {

                return result;
            }
        }

		public async Task<int> EnviarCorreoPedido(Pedido Valor, string idenCliente)
        {
            int result = Transaccion.Error;
            try
            {
                PedidoCorreo pedidoCorreo = new();
                DatosClienteCorreo datosCliente = new DatosClienteCorreo();

				pedidoCorreo.operador = Valor.UsuarioOperador;
                pedidoCorreo.numRemision = Valor.NumeroRemision;

                //consulta para obtener el correo del operador
                List<UsuarioGalileoResponse> listaOperadores = new List<UsuarioGalileoResponse>();
                //List<UsuarioGalileoResponse> listaCliente = new List<UsuarioGalileoResponse>();
                listaOperadores = await gestionarPedidos.BuscarUsuariosGalileoOperador(idenCliente);
                //listaCliente = await gestionarPedidos.BuscarUsuariosGalileoMail(idenCliente);

				//consulta a flexline para obtener el ruc del operador
				var buscarCliente = await gestionarPedidos.BuscarClienteFlexline(idenCliente);

				foreach (var operador in listaOperadores)
                {
                    foreach(var operadorFlex in buscarCliente)
                    {
                        if(operador.identificacion == operadorFlex.RUC)
                        {
                            if(operadorFlex.Email.Contains(";"))
                            {
                                string[] correosSeparados = operadorFlex.Email.Split(';');
                                pedidoCorreo.correoOperador = correosSeparados[0];
                            }
                            else
                            {
                                pedidoCorreo.correoOperador = operadorFlex.Email;
                            }                            
                        }
                    }
                }

                foreach (var cliente in listaOperadores)
                {
                    if (idenCliente == cliente.identificacion)
                    {
                        pedidoCorreo.correoCliente = cliente.correo!;
                    }
                }

                //consulta a la BD para obtener nombre cliente, telefono, direcccion y total muestras
                foreach (var ordenId in Valor.DatosOrden)
                {
					datosCliente = gestionarPedidos.datosClienteCorreo(ordenId.IdOrden);
				}

				pedidoCorreo.direccion = datosCliente.DireccionCliente;
                pedidoCorreo.telefonoCliente = datosCliente.Telefono;                
                pedidoCorreo.clienteNombre = datosCliente.Cliente;                
                pedidoCorreo.TotalMuestras = datosCliente.TotalMuestras;

                result = gestionarPedidos.EnviarCorreoPedido(pedidoCorreo);

                return result;
            }
            catch (Exception ex)
            {
                return result;
            }
        }


        public async Task<int> EnviarCorreoElimPedido(int idPedido, int idOPerador, 
                                                      string nomOperador, string observacion,
                                                      string idenCliente)
        {
            int result = Transaccion.Error;
            try
            {

                ElimPedidoCorreo elimPedido = new();
                DatosClienteCorreo datosCliente = new DatosClienteCorreo();

                //consulta para obtener el correo del operador
                List<UsuarioGalileoResponse> listaOperadores = new List<UsuarioGalileoResponse>();
                //List<UsuarioGalileoResponse> listaCliente = new List<UsuarioGalileoResponse>();
				listaOperadores = await gestionarPedidos.BuscarUsuariosGalileoOperador(idenCliente);
                //listaCliente = await gestionarPedidos.BuscarUsuariosGalileoMail(idenCliente);

				//consulta a flexline para obtener el ruc del operador
				var buscarCliente = await gestionarPedidos.BuscarClienteFlexline(idenCliente);

				foreach (var operador in listaOperadores)
				{
					foreach (var operadorFlex in buscarCliente)
					{
						if (operador.identificacion == operadorFlex.RUC)
						{
                            if (operadorFlex.Email.Contains(";"))
                            {
                                string[] correosSeparados = operadorFlex.Email.Split(';');
                                elimPedido.correoOperador = correosSeparados[0];
                            }
                            else
                            {
                                elimPedido.correoOperador = operadorFlex.Email;
                            }                            
						}
					}
				}

				foreach (var cliente in listaOperadores)
				{
					if (idenCliente == cliente.identificacion)
					{
						elimPedido.correoCliente = cliente.correo!;
					}
				}

                //consulta a la BD para obtener nombre cliente, telefono, direcccion y total muestras
                // por el id del pedido
                datosCliente = gestionarPedidos.datosClienteCorreoElim(idPedido);

                elimPedido.clienteNombre = datosCliente.Cliente;
                //elimPedido.correoCliente = datosCliente.Correo;
                elimPedido.telefonoCliente = datosCliente.Telefono;
                elimPedido.numRemision = datosCliente.numRemision;
                elimPedido.direccion = datosCliente.DireccionCliente;
                elimPedido.operador = nomOperador;
                elimPedido.observacion = observacion;

                result = gestionarPedidos.EnviarCorreoPedidoEli(elimPedido);

                return result;

            }
            catch (Exception ex)
            {
                return result;
            }
        }

        public string NombreEstadoOrden(int IdOrdenEstado)
        {
            string nombreEstado = "";

            try
            {
                nombreEstado = gestionarOrdenes.NombreEstadoOrden(IdOrdenEstado);
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
            return nombreEstado;
        }

        public List<CataTiposClienteResponse> TiposClientesHumalab()
        {
            var tipos = new List<CataTiposClienteResponse>();
            try
            {
                tipos = gestionarOrdenes.TiposClientesHum();
            }
            catch
            {
                return tipos;
            }

            return tipos;
        }


        public int ObtenerIdCliente(int idGalileo)
        {
            int id = gestionarOrdenes.ClienteId(idGalileo);

            return id;
        }

        public int ObtenerIdClientePed(int idGalileo)
        {
			int id = gestionarOrdenes.ClienteIdPedido(idGalileo);

			return id;
		}

		//Generar excel de pruebas que se estan utilizando
		public string ExportExcelPruebas(string datosPrueba)
        {
            List<ListaPruebasHumalab> lstPruebas = new List<ListaPruebasHumalab>();
            lstPruebas = JsonConvert.DeserializeObject<List<ListaPruebasHumalab>>(datosPrueba)!;
            string base64Pdf = "";

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            try 
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (ExcelPackage package = new ExcelPackage(memoryStream))
                    {
                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Sheet1");

                        worksheet.Cells["A2"].Value = "CATÁLOGO DE PRUEBAS HUMALAB";                        
                        int rowStart = 4;

                        worksheet.Cells[rowStart, 1].Value = "Abreviatura";
                        worksheet.Cells[rowStart, 2].Value = "CodigoExamen";
                        worksheet.Cells[rowStart, 3].Value = "Metodologia";
                        worksheet.Cells[rowStart, 4].Value = "Muestra";
                        worksheet.Cells[rowStart, 5].Value = "Muestra Alterna";
                        worksheet.Cells[rowStart, 6].Value = "Nombre Muestra";
                        worksheet.Cells[rowStart, 7].Value = "Observaciones";
                        worksheet.Cells[rowStart, 8].Value = "Precio";
                        worksheet.Cells[rowStart, 9].Value = "Recipiente";

                        int row = rowStart + 1;
                        foreach (var pruebas in lstPruebas)
                        {
                            worksheet.Cells[row, 1].Value = pruebas.Abreviatura;
                            worksheet.Cells[row, 2].Value = pruebas.CodigoExamen;
                            worksheet.Cells[row, 3].Value = pruebas.Metodologia;
                            worksheet.Cells[row, 4].Value = pruebas.Muestra;
                            worksheet.Cells[row, 5].Value = pruebas.MuestraAlterna;
                            worksheet.Cells[row, 6].Value = pruebas.Nombre;
                            worksheet.Cells[row, 7].Value = pruebas.Observaciones;
                            worksheet.Cells[row, 8].Value = pruebas.Precio;
                            worksheet.Cells[row, 9].Value = pruebas.Recipiente;
                            row++;
                        }

                        package.Save();
                    }

                    byte[] excelBytes = memoryStream.ToArray();
                    base64Pdf = Convert.ToBase64String(excelBytes);
                }
            }
            catch (Exception ex)
            {
                return "01";
            }

            return base64Pdf;
        }

        #endregion

    }
}
