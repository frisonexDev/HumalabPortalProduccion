using GDifare.Portal.Humalab.Seguridad.Operation;
using GDifare.Portal.Humalab.Servicio.Modelos.Orden;
using iTextSharp.text;
using iTextSharp.text.pdf;
using PdfSharpCore;
using PdfSharpCore.Pdf;
using System.Text.RegularExpressions;

namespace GDifare.Portal.HumaLab.UI.Models;

public class GenerarPdfResult
{
    PdfPCell BCode;

    //orden
    public string LogoEmpresa;
    public float CurrentY;
    public string emptySection;
    public bool exEveryone;
    public string aArea;
    public string laboratorioDesc;
    public string sedeDesc;
    public string origen;
    public string generoPaciente;
    public string edadPaciente;
    public string estadoOrden;
    public DateTime dateIncoming;
    public string NombreLaboratorio;

    //medico
    public string nombreMedico;
    public string telefonoMedico;
    public string correoMedico;

    public DateTime fnacPaciente;
    public string fregOrden;

    //paciente
    public string telefono;
    public string direccion;
    public string correoPaciente;
    public string idenPac;
    public string NombreCompletoPaciente;
    public string FechaNaci;

    //areas
    public int IdExamen;
    public int OrdenArea;
    public int IdMuestra;

    public bool cabeceraExamen;
    public string eunValidado;
    public string euaValidado;
    public int uaValidador;
    public DateTime faValidacion;
    public string eaValidado;

    public string GenerarDocumentoResultados(GenPdfResultados resultados, bool todosVal, bool impresion)
    {
        string base64Pdf = "";

        //Excel
        if (impresion == true)
        {

        }
        //PDF
        else
        {
            //Datos orden
            laboratorioDesc = resultados.cabecera.NombreLaboratorio;
            sedeDesc = resultados.cabecera.NombreSede;
            origen = resultados.cabecera.NombreSede;
            //edadPaciente = resultados.cabecera.EdadCompleta; //cambiar
            estadoOrden = "";
            emptySection = " ";
            dateIncoming = resultados.cabecera.FechaIngreso;

            switch (resultados.cabecera.IdEstadoOrden)
            {
                case 1:
                    estadoOrden = "Abierta";
                    break;
                case 2:
                    estadoOrden = "Flebotomía";
                    break;
                case 3:
                    estadoOrden = "Resultados Pendientes";
                    break;
                case 4:
                    estadoOrden = "Validación Pendiente";
                    break;
                case 5:
                    estadoOrden = "Validada";
                    break;
                case 6:
                    estadoOrden = "Resultados Enviados";
                    break;
                case 7:
                    estadoOrden = "Impreso";
                    break;
                case 8:
                    estadoOrden = "Cancelada";
                    break;
            }

            fregOrden = resultados.cabecera.FechaIngreso.ToString();
            NombreLaboratorio = resultados.cabecera.NombreLaboratorio;

            if (resultados.medico != null)
            {
                //medico
                foreach (var medico in resultados.medico)
                {
                    nombreMedico = medico.NomMedico;
                    correoMedico = medico.EmailMedico;
                    telefonoMedico = medico.TeleMedico;
                }
            }

            if (resultados.paciente != null)
            {
                //paciente
                foreach (var paciente in resultados.paciente)
                {
                    correoPaciente = paciente.MailPac;
                    NombreCompletoPaciente = paciente.NombrePac + " " + paciente.ApelliPac;
                    idenPac = paciente.Identificador;
                    fnacPaciente = paciente.FechaNacimiento;
                    generoPaciente = paciente.GenPac.ToString();
                }
            }

            if (resultados.pruebas == null)
            {
                return "01";
            }

            //if (resultados.pruebas.Count <= 0)
            //{
            //    return "01";
            //}

            foreach (var pruebas in resultados.pruebas)
            {
                IdExamen = pruebas.idExamen;
                OrdenArea = pruebas.OrdenArea;
                IdMuestra = pruebas.idMuestra;
                //ResultadoLiteral = pruebas.resultadoLiteral;
            }

            try
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    string fileName = "Resultados-" + NombreCompletoPaciente + " " + resultados.cabecera.CodigoOrden.ToString();

                    using (var pdfDocument = new iTextSharp.text.pdf.PdfDocument())
                    {
                        iTextSharp.text.Document document = new iTextSharp.text.Document(iTextSharp.text.PageSize.A4, 30, 30, 30, 30);
                        PdfWriter writer = PdfWriter.GetInstance(document, ms);

                        //hacer una consulta al api para sacar el logo
                        LogoEmpresa = "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";

                        // Agregar el evento para la marca de agua
                        writer.PageEvent = new Watermark(LogoEmpresa);

                        document.Open();
                        document.NewPage();

                        //Código de barras de la orden
                        BCode = barCodeImage(writer, resultados.cabecera.CodigoOrden);

                        //header table
                        AddHeaderTable(document, resultados.cabecera.CodigoOrden, BCode);

                        //subheader table
                        AddSubHeaderTable(document, resultados.cabecera.FechaIngreso.ToString(),
                                          resultados.cabecera.CodigoOrden, estadoOrden);

                        //order data table
                        AddOrderDataTable(document, NombreCompletoPaciente,
                                          idenPac, resultados.cabecera.EdadCompleta,
                                          Convert.ToDateTime(fnacPaciente),
                                          Convert.ToChar(generoPaciente), dateIncoming,
                                          nombreMedico, correoMedico, resultados.cabecera.NombreSede);

                        AddSpacerTable(document, emptySection);

                        //order detalles table
                        AddOrderDetail(document, writer,
                                       resultados.pruebas, estadoOrden,
                                       idenPac, resultados.cabecera.EdadCompleta,
                                       Convert.ToChar(generoPaciente), Convert.ToDateTime(fnacPaciente),
                                       nombreMedico, correoMedico,
                                       telefonoMedico, resultados.cabecera.NombreSede, todosVal);

                        //espacios
                        AddSpacerTable(document, emptySection);
                        AddSpacerTable(document, emptySection);
                        AddSpacerTable(document, emptySection);
                        AddSpacerTable(document, emptySection);
                        AddSpacerTable(document, emptySection);

                        document.Close();
                        writer.Close();
                    }

                    byte[] pdfBytesPDF = ms.ToArray();
                    base64Pdf = Convert.ToBase64String(pdfBytesPDF);
                }
            }
            catch (Exception ex)
            {
                return "01";
            }
        }

        return base64Pdf;
    }

    #region barCodeImage
    private static PdfPCell barCodeImage(PdfWriter writer, string data)
    {
        PdfContentByte cb = writer.DirectContent;
        iTextSharp.text.pdf.Barcode128 bc = new Barcode128();
        bc.TextAlignment = Element.ALIGN_CENTER;
        bc.Code = data;
        bc.StartStopText = false;
        bc.CodeType = iTextSharp.text.pdf.Barcode128.CODE128;
        bc.X = 1f;
        bc.Font = null;
        bc.Extended = true;
        iTextSharp.text.Image PatImage1 = bc.CreateImageWithBarcode(cb, iTextSharp.text.BaseColor.BLACK, iTextSharp.text.BaseColor.BLACK);
        PatImage1.ScaleAbsolute(90f, 40f);
        PdfPCell palletBarcodeCell = new PdfPCell(PatImage1);
        palletBarcodeCell.Border = Rectangle.BOTTOM_BORDER;
        palletBarcodeCell.FixedHeight = 40f;
        palletBarcodeCell.HorizontalAlignment = 2;
        palletBarcodeCell.VerticalAlignment = 1;
        palletBarcodeCell.Rowspan = 4;
        return palletBarcodeCell;
    }

    #endregion barCodeImage

    #region AddHeaderTable
    public void AddHeaderTable(iTextSharp.text.Document doc, string NumOrden, iTextSharp.text.pdf.PdfPCell barCode)
    {
        // Crear una tabla para el header con 3 columnas y 4 filas
        PdfPTable headerTable = new PdfPTable(3);
        headerTable.WidthPercentage = 100;
        float[] columnWidths = { 15f, 70f, 15f };
        headerTable.SetWidths(columnWidths);

        iTextSharp.text.Image logo;

        Byte[] iByteLE = Convert.FromBase64String(LogoEmpresa);
        logo = iTextSharp.text.Image.GetInstance(iByteLE);
        logo.ScaleAbsolute(74f, 55f); // Ajustar el tamaño del logotipo

        PdfPCell logoCell = new PdfPCell(logo);
        logoCell.HorizontalAlignment = Element.ALIGN_LEFT;
        logoCell.VerticalAlignment = Element.ALIGN_MIDDLE;
        logoCell.Rowspan = 4; // Establecer rowspan para ocupar 4 filas en la primera columna
        logoCell.Border = Rectangle.BOTTOM_BORDER;

        headerTable.AddCell(logoCell); //Fila 1I

        headerTable.AddCell(GetHeaderCell("MINISTERIO DE SALUD PUBLICA", 1, 1, 0)); //Fila 1C
        headerTable.AddCell(barCode); //Fila 1D
        headerTable.AddCell(GetHeaderCell(NombreLaboratorio, 1, 1, 0)); //Fila 2C

        headerTable.AddCell(GetHeaderCell("INFORME DE RESULTADOS", 1, 1, 0)); //Fila 3C                
        headerTable.AddCell(GetHeaderCell("", 1, 1, 2)); //Fila 4C
        doc.Add(headerTable);
        CurrentY = CurrentY + headerTable.TotalHeight;
    }

    //GetHeaderCell
    private static PdfPCell GetHeaderCell(string text, int rowspan, int colspan, int borders)
    {
        BaseFont baseFont = BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
        //Font font = new Font(baseFont, fontSize, fontStyle, BaseColor.BLACK);
        iTextSharp.text.Font fontHeader = FontFactory.GetFont("Arial", 10, 1, BaseColor.BLACK);
        PdfPCell cell = new PdfPCell(new Phrase(text, fontHeader))
        {
            HorizontalAlignment = Element.ALIGN_CENTER,
            VerticalAlignment = Element.ALIGN_MIDDLE,
            Padding = 3,
            BackgroundColor = new BaseColor(0, 0, 0, 0),
            Rowspan = rowspan,
            Colspan = colspan,
        };
        return borderCells(cell, borders);
    }

    #endregion AddHeaderTable

    #region AddSubHeaderTable

    public void AddSubHeaderTable(iTextSharp.text.Document doc, string dateIncoming,
                                  string numOrder, string statePacient)
    {
        // Crear una tabla para el subheader con 3 columnas y 1 fila
        PdfPTable subheaderTable = new PdfPTable(3);
        subheaderTable.WidthPercentage = 100;
        // Definir la distribución de las columnas en la fila
        float[] columnWidths = { 30f, 40f, 30f };
        subheaderTable.SetWidths(columnWidths);

        subheaderTable.AddCell(GetSubHeaderCell("N° ORDEN: " + numOrder, 1, 1, 6, 0));
        subheaderTable.AddCell(GetSubHeaderCell("FECHA REGISTRO: " + dateIncoming, 1, 1, 6, 1));
        subheaderTable.AddCell(GetSubHeaderCell("ESTADO: " + statePacient, 1, 1, 6, 2));
        doc.Add(subheaderTable);
        CurrentY = CurrentY + subheaderTable.TotalHeight;
    }

    //GetSubHeaderCell
    private static PdfPCell GetSubHeaderCell(string text, int rowspan, int colspan, int borders, int align)
    {
        BaseFont baseFont = BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
        //Font font = new Font(baseFont, fontSize, fontStyle, BaseColor.BLACK);
        iTextSharp.text.Font fontSubHeader = FontFactory.GetFont("Arial", 8, 1, BaseColor.DARK_GRAY);
        PdfPCell cell = new PdfPCell(new Phrase(text, fontSubHeader))
        {
            HorizontalAlignment = align,
            VerticalAlignment = Element.ALIGN_MIDDLE,
            Padding = 5,
            BackgroundColor = new BaseColor(0, 0, 200, 0),
            Rowspan = rowspan,
            Colspan = colspan,
        };
        return borderCells(cell, borders);
    }

    #endregion AddSubHeaderTable

    #region AddOrderDataTable
    public void AddOrderDataTable(iTextSharp.text.Document doc, string NombresPaciente,
            string IdentificacionPaciente, string EdadPaciente,
            DateTime FechaNacimiento, char SexoPaciente,
            DateTime FechaRecepcion, string NombresDoctor,
            string CorreoDoctor, string OrigenOrden)
    {
        // Crear una tabla para los datos de la orden con 2 columnas y 5 fila
        PdfPTable orderDataTable = new PdfPTable(2);
        orderDataTable.WidthPercentage = 100;
        // Definir la distribución de las columnas en la fila
        float[] columnWidths = { 60f, 40f };
        orderDataTable.SetWidths(columnWidths);

        orderDataTable.AddCell(GetOrderDataCell("hdrOrder", "PACIENTE", 1, 1, 1, 0, 5));
        orderDataTable.AddCell(GetOrderDataCell("hdrOrder", "SOLICITANTE ", 1, 1, 1, 0, 5));

        orderDataTable.AddCell(GetOrderDataCell("shdrOrder", NombresPaciente.ToUpper(), 1, 1, 0, 0, 2));
        orderDataTable.AddCell(GetOrderDataCell("shdrOrder", "MEDICO: " + NombresDoctor.ToUpper(), 2, 1, 0, 0, 2));

        orderDataTable.AddCell(GetOrderDataCell("shdrOrder", "IDENTIFICACION: " + IdentificacionPaciente, 1, 1, 0, 0, 2));
        orderDataTable.AddCell(GetOrderDataCell("shdrOrder", "EDAD: " + EdadPaciente + ", FECHA DE NACIMIENTO: " + FechaNacimiento.ToString("dd/MM/yyyy"), 1, 1, 0, 0, 2));
        orderDataTable.AddCell(GetOrderDataCell("shdrOrder", "EMAIL: " + CorreoDoctor, 1, 1, 0, 0, 2));

        string SexoPacienteC = string.Empty;

        switch (SexoPaciente.ToString())
        {
            case "M":
                SexoPacienteC = "MASCULINO";
                break;
            case "F":
                SexoPacienteC = "FEMENINO";
                break;
            default:
                break;
        }

        orderDataTable.AddCell(GetOrderDataCell("shdrOrder", "SEXO: " + SexoPacienteC, 1, 1, 0, 0, 2));
        orderDataTable.AddCell(GetOrderDataCell("shdrOrder", "ORIGEN: " + OrigenOrden.ToUpper(), 2, 1, 2, 0, 2));
        orderDataTable.AddCell(GetOrderDataCell("shdrOrder", "FECHA/HORA RECEPCION MUESTRA: " + dateIncoming, 1, 1, 2, 0, 2));
        doc.Add(orderDataTable);
        CurrentY = CurrentY + orderDataTable.TotalHeight;
    }

    //GetOrderDataCell
    private static PdfPCell GetOrderDataCell(string textclass, string text,
                                             int rowspan, int colspan,
                                             int borders, int aligntext, int paddingtext)
    {
        BaseFont baseFont = BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);

        if (textclass == "hdrOrder")
        {
            iTextSharp.text.Font fontOrderData = FontFactory.GetFont("Times", 7, 1, BaseColor.BLACK);
            PdfPCell cell = new PdfPCell(new Phrase(text, fontOrderData))
            {
                HorizontalAlignment = aligntext,
                //VerticalAlignment = Element.ALIGN_MIDDLE,
                PaddingBottom = paddingtext,
                PaddingTop = paddingtext,
                BackgroundColor = new BaseColor(0, 0, 0, 0),
                Rowspan = rowspan,
                Colspan = colspan,
            };

            return borderCells(cell, borders);
        }
        else
        {
            iTextSharp.text.Font fontOrderData = FontFactory.GetFont("Arial", 7, 0, BaseColor.BLACK);
            PdfPCell cell = new PdfPCell(new Phrase(text, fontOrderData))
            {
                HorizontalAlignment = aligntext,
                PaddingBottom = paddingtext,
                PaddingTop = paddingtext,
                PaddingLeft = 10,
                BackgroundColor = new BaseColor(0, 0, 0, 0),
                Rowspan = rowspan,
                Colspan = colspan,
            };
            return borderCells(cell, borders);
        }
    }

    #endregion AddOrderDataTable

    #region AddOrderDetail

    public void AddOrderDetail(iTextSharp.text.Document doc,
            PdfWriter writer,
            List<PruebasPdf> orderResults,
            string estadoOrden,
            string IdentificadorPaciente,
            string EdadPaciente,
            char GeneroPaciente,
            DateTime FNPaciente,
            string nombreMedico,
            string correoMedico,
            string telefonoMedico,
            string origenOrden,
            bool todosVal)
    {
        // Detalle de los resultados
        string sepSection = "true";
        string ValidatorSign = "true";
        List<(string Perfil, string segmento)> ListaPerfil = new List<(string Perfil, string segmento)>();
        string NombreCompleto = string.Empty;
        string PerfilExamen = "";
        string DivisionExamen = "";
        string nArea = String.Empty;
        string eNombreExamen = String.Empty;
        string eResultadoT = String.Empty;
        char eTipoResultado;
        string eUnidades = String.Empty;
        string eReferencia = String.Empty;
        string eRangoMin = String.Empty;
        string eRangoMax = String.Empty;
        string afirma = string.Empty;
        string nfirma = string.Empty;
        int uValidador;
        DateTime fValidacion;

        // LISTA ÚNICA DE USUARIOS VALIDADORES
        List<int> userVal = new List<int>();
        userVal.Add(0);

        foreach (var data in orderResults)
        {
            //datos de cabecera para enviar entre hojas
            NombreCompleto = NombreCompletoPaciente;
            nArea = data.nombreArea.ToString();
            eNombreExamen = data.nombreExamen.ToString();
            eResultadoT = data.resultadoActual ?? "?";
            eTipoResultado = Convert.ToChar(data.tipoResultado);
            eUnidades = (string)(data.siglasUnidad ?? "");

            // Desplegar comentario fijo en vez de referencias, con referencias minimas y maximas = 0
            if (eTipoResultado.Equals("N") && data.refMaxima != 0)
            {
                eReferencia = "[" + data.refMinima.ToString() + " - " + data.refMinima.ToString() + "]";
            }
            else if (eTipoResultado.Equals("N") && data.refMaxima == 0)
            {
                eReferencia = data.ComentarioFijo?.ToString() ?? ""; //[0-0]
            }
            else if (eTipoResultado.Equals("T") && data.refMaxima == 0)
            {
                eReferencia = data.ComentarioFijo?.ToString() ?? ""; //[0-0]
            }
            else if (eTipoResultado.Equals("S"))
            {
                string refLiteral = "";
                refLiteral = data.resultadoLiteral[0].Texto.ToString();
                if (eResultadoT != refLiteral && data.resultado)
                {
                    eReferencia = "[" + refLiteral + "]";
                    eReferencia = "[]";
                    eReferencia = data.ComentarioFijo?.ToString();
                }
                else if (eResultadoT == refLiteral && data.resultado && (refLiteral.Contains("<") || refLiteral.Contains(">")))
                {
                    eReferencia = "[" + refLiteral + "]";
                }
                else
                {
                    eReferencia = data.ComentarioFijo?.ToString();
                }
            }
            else
            {
                eReferencia = "[]";
            }

            eRangoMin = data.refMinima.ToString();
            eRangoMax = data.refMaxima.ToString();
            uValidador = data.usuarioValidacion;
            fValidacion = data.fechaValidacion;
            bool rValidado = data.validado;
            string enValidado;
            DateTime fnValidacion;
            string eComentario;
            string valMessage;

            exEveryone = !todosVal;
            if (exEveryone == true)
                rValidado = false;

            if (!exEveryone == rValidado && eResultadoT != "?" && eResultadoT != "NI")
            {
                if (data.usuarioValidacion != 0)
                {
                    //if (!userVal.Contains(data.usuarioValidacion))
                    //{
                    //    userVal.Add(data.usuarioValidacion);
                    //    var usuario = usuarioOperations.ConsultarUsuario(Convert.ToInt32(data.UsuarioValidacion), 0).FirstOrDefault();
                    //    if (usuario != null)
                    //    {
                    //        eunValidado = usuario.NombreCompleto;
                    //        nfirma = usuario.Firma ??;
                    //    }
                    //}
                }
                else
                {
                    eunValidado = "";
                    nfirma = "";
                }
            }

            enValidado = data.validado.ToString();
            //eunValidado = data;
            fnValidacion = fValidacion;
            eComentario = (string)(data.Comentario ?? "");

            if (sepSection == "true")
            {
                PerfilExamen = "";
                DivisionExamen = "";
                string[] PerfilSubDivision = data.ComentarioImpresion == null ? "|".Split("|") : data.ComentarioImpresion.ToString().Split("|");
                if (PerfilSubDivision.Length == 2 && PerfilSubDivision[0] != "" && PerfilSubDivision[1] != "")
                {
                    if (!ListaPerfil.Exists(lp => lp.Perfil.Contains(PerfilSubDivision[0])))
                    {
                        PerfilExamen = PerfilSubDivision[0];
                    }
                    if (!ListaPerfil.Exists(lp => lp.segmento.Contains(PerfilSubDivision[0])))
                    {
                        DivisionExamen = PerfilSubDivision[1];
                    }
                    ListaPerfil.Add((PerfilExamen, DivisionExamen));
                    eNombreExamen = "   -" + eNombreExamen;
                }
                else if (PerfilSubDivision.Length == 1)
                {
                    eNombreExamen = "   -" + eNombreExamen;
                }
            }

            if (aArea == nArea)
            {
                sizeSheetSA(doc, writer, CurrentY, nArea, data.codigoOrden, data.fechaCreacion,
                            estadoOrden, NombreCompleto, IdentificadorPaciente, EdadPaciente,
                            GeneroPaciente, FNPaciente, nombreMedico, correoMedico, telefonoMedico,
                            origenOrden);

                // Agregar perfil y categoria
                if (DivisionExamen != "")
                {
                    AddProfileTable(doc, PerfilExamen);
                    AddCategoryTable(doc, DivisionExamen);
                }

                if (cabeceraExamen)
                {
                    // Agregar los examenes asociados
                    if (!Regex.IsMatch(eResultadoT, "^[0-9]+([.][0-9]+)?$"))
                    {
                        AddDetailResultsTable(doc, eNombreExamen, eResultadoT, eUnidades, eReferencia, "");
                    }
                    else
                    {
                        if (float.Parse(eResultadoT) < float.Parse(eRangoMin) || float.Parse(eResultadoT) > float.Parse(eRangoMax) && (eTipoResultado.Equals("N") && data.ComentarioFijo == null))
                        {
                            AddDetailResultsTable(doc, eNombreExamen, "* " + eResultadoT, eUnidades, eReferencia, "");
                        }
                        else if (float.Parse(eResultadoT) < float.Parse(eRangoMin) || float.Parse(eResultadoT) > float.Parse(eRangoMax) && (eTipoResultado.Equals("N") && data.ComentarioFijo != null) && (eReferencia != data.ComentarioFijo.ToString()))
                        {
                            AddDetailResultsTable(doc, eNombreExamen, "* " + eResultadoT, eUnidades, eReferencia + "\n" + data.ComentarioFijo.ToString(), "");
                        }
                        else
                        {
                            AddDetailResultsTable(doc, eNombreExamen, eResultadoT, eUnidades, eReferencia, "");
                        }
                    }

                    System.Diagnostics.Debug.WriteLine(eNombreExamen);
                    //COMENTARIOS DE EXAMENES / REFERENCIAS TEXTUALES
                    if (eComentario != null && eComentario != "")
                    {
                        AddCommentTestTable(doc, eComentario, true);
                        System.Diagnostics.Debug.WriteLine("COMENTARIO..." + eComentario);
                    }
                }
            }
            else
            {
                if (eunValidado == "")
                {
                    valMessage = "VALIDADO POR: ";
                }
                else
                {
                    valMessage = "SIN VALIDAR";
                }
                if (nArea != aArea && aArea != "" && enValidado == "True")
                {
                    sizeSheetDA(doc, writer, CurrentY, nArea, aArea, data.codigoOrden, eunValidado, valMessage, faValidacion, estadoOrden, NombreCompleto, IdentificadorPaciente, EdadPaciente, GeneroPaciente, FNPaciente, nombreMedico, correoMedico, telefonoMedico, origenOrden, afirma);
                }
                else if (nArea != aArea && aArea != "" && enValidado == "False")
                {
                    sizeSheetDA(doc, writer, CurrentY, nArea, aArea, data.codigoOrden, eunValidado, valMessage, faValidacion, estadoOrden, NombreCompleto, IdentificadorPaciente, EdadPaciente, GeneroPaciente, FNPaciente, nombreMedico, correoMedico, telefonoMedico, origenOrden, afirma);
                }

                cabeceraExamen = true;
                aArea = nArea;
                eaValidado = enValidado;
                euaValidado = eunValidado;
                uaValidador = uValidador;
                faValidacion = fnValidacion;
                afirma = nfirma;

                if (cabeceraExamen)
                {
                    Console.WriteLine(eNombreExamen + "|" + eResultadoT + "|" + eUnidades + "|" + eReferencia);

                    // Agregar perfil de exámenes
                    if (DivisionExamen != "")
                    {
                        AddProfileTable(doc, PerfilExamen);
                        AddCategoryTable(doc, DivisionExamen);
                    }

                    if (!Regex.IsMatch(eResultadoT, "^[0-9]+([.][0-9]+)?$"))
                    {
                        AddDetailResultsTable(doc, eNombreExamen, eResultadoT, eUnidades, eReferencia, "");
                    }
                    else
                    {
                        if (float.Parse(eResultadoT) < float.Parse(eRangoMin) || float.Parse(eResultadoT) > float.Parse(eRangoMax) && (eTipoResultado.Equals("N") && data.ComentarioFijo == null))
                        {
                            AddDetailResultsTable(doc, eNombreExamen, "* " + eResultadoT, eUnidades, eReferencia, "");
                        }
                        else if (float.Parse(eResultadoT) < float.Parse(eRangoMin) || float.Parse(eResultadoT) > float.Parse(eRangoMax) && (eTipoResultado.Equals("N") && data.ComentarioFijo != null) && (eReferencia != data.ComentarioFijo.ToString()))
                        {
                            AddDetailResultsTable(doc, eNombreExamen, "* " + eResultadoT, eUnidades, eReferencia + "\n" + data.ComentarioFijo.ToString(), "");
                        }
                        else
                        {
                            AddDetailResultsTable(doc, eNombreExamen, eResultadoT, eUnidades, eReferencia, "");
                        }
                    }

                    // Comentarios de examenes / referencias textuales
                    if (eComentario != null && eComentario != "")
                    {
                        AddCommentTestTable(doc, eComentario, true);
                        Console.WriteLine("COMENTARIO..." + eComentario);
                    }
                }
            }

        }

        if (eaValidado == "True")
        {
            AddValidatorTable(doc, euaValidado, "VALIDADO POR:_ " + euaValidado + " " + faValidacion.ToString("dd/MM/yyyy HH:mm"), ValidatorSign, afirma);
        }
        else
        {
            AddValidatorTable(doc, euaValidado, "SIN VALIDAR", ValidatorSign, afirma);
        }
    }
    #endregion AddOrderDetail

    #region sizeSheetSA

    public void sizeSheetSA(Document doc,
            PdfWriter writer,
            float y,
            string newArea,
            string numOrder,
            DateTime dateIncoming,
            string estadoOrden,
            string namePacient,
            string idPacient,
            string agePacient,
            char genPacient,
            DateTime dateBirthPacient,
            string nameDoctor,
            string mailDoctor,
            string phoneDoctor,
            string origenOrden)
    {
        if (y >= 650)
        {
            CurrentY = 0;
            doc.NewPage();
            AddHeaderTable(doc, numOrder, barCodeImage(writer, numOrder));
            AddSubHeaderTable(doc, dateIncoming.ToString(), numOrder, estadoOrden);
            AddOrderDataTable(doc, namePacient, idPacient, agePacient, Convert.ToDateTime(dateBirthPacient), genPacient, dateIncoming, nameDoctor, mailDoctor, origenOrden);
            AddSpacerTable(doc, " ");
            System.Diagnostics.Debug.WriteLine(newArea);
            AddAreaTable(doc, newArea.ToUpper());
            sizeSheetArea(doc, writer, CurrentY, newArea, numOrder, namePacient, idPacient, agePacient, dateBirthPacient.ToString(), genPacient, nameDoctor, mailDoctor, origenOrden, estadoOrden);
            AddTitleDetailTable(doc);
        }
    }

    #endregion sizeSheetSA

    #region AddSpacerTable

    public void AddSpacerTable(Document doc, string emptyText)
    {
        BaseFont baseFont = BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
        Font font = new Font(baseFont, 7, Font.NORMAL, BaseColor.WHITE);
        Phrase emptyPhrase = new Phrase(emptyText, font);

        PdfPCell emptyCell = new PdfPCell(emptyPhrase);
        emptyCell.HorizontalAlignment = Element.ALIGN_CENTER;
        emptyCell.VerticalAlignment = Element.ALIGN_MIDDLE;
        emptyCell.Border = Rectangle.BOTTOM_BORDER;
        emptyCell.BorderColor = new BaseColor(51, 51, 51, 2);
        emptyCell.BackgroundColor = new BaseColor(0, 0, 0, 0);

        // Crear una tabla para el header y agregar la celda
        PdfPTable emptyTable = new PdfPTable(1);
        emptyTable.DefaultCell.FixedHeight = 10;
        emptyTable.WidthPercentage = 100;
        emptyTable.AddCell(emptyCell);
        doc.Add(emptyTable);
        CurrentY = CurrentY + emptyTable.TotalHeight;
    }

    #endregion AddSpacerTable

    #region AddAreaTable

    public void AddAreaTable(Document doc, string area)
    {
        // Crear una tabla para un título de 1 columna y 1 fila
        PdfPTable areaTable = new PdfPTable(1);
        areaTable.WidthPercentage = 100;
        // Definir la distribución de las columnas en la fila
        float[] columnWidths = { 100f };
        areaTable.SetWidths(columnWidths);
        areaTable.AddCell(GetAreaCell(area, 1, 6, 1));
        doc.Add(areaTable);
        CurrentY = CurrentY + areaTable.TotalHeight;
    }

    // Método para crear una celda para el area de estudio con formato personalizado
    private static PdfPCell GetAreaCell(string text, int rowspan, int borders, int align)
    {
        BaseFont baseFont = BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
        iTextSharp.text.Font fontArea = FontFactory.GetFont("Arial", 10, 1, BaseColor.BLACK);
        string[] backgroundArea = "101,158,200,255".Split(",");
        PdfPCell cell = new PdfPCell(new Phrase(text, fontArea))
        {
            HorizontalAlignment = align,
            VerticalAlignment = Element.ALIGN_MIDDLE,
            Padding = 4,
            BackgroundColor = new BaseColor(
                Convert.ToInt32(backgroundArea[0]),
                Convert.ToInt32(backgroundArea[1]),
                Convert.ToInt32(backgroundArea[2]),
                Convert.ToInt32(backgroundArea[3])
                ),
            Rowspan = rowspan
        };
        return borderCells(cell, borders);
    }
    #endregion AddAreaTable

    #region sizeSheetArea

    public void sizeSheetArea(
            Document doc,
            PdfWriter writer,
            float y,
            string newArea,
            string numOrder,
            string namePacient,
            string idPacient,
            string agePacient,
            string dateBirthPacient,
            char genPacient,
            string nameDoctor,
            string mailDoctor,
            string origenOrder,
            string estadoOrden
            )
    {
        if (y >= 650)
        {
            CurrentY = 0;
            doc.NewPage();
            AddHeaderTable(doc, numOrder, barCodeImage(writer, numOrder));
            AddSubHeaderTable(doc, dateIncoming.ToString(), numOrder, estadoOrden);
            AddOrderDataTable(doc, namePacient, idPacient, agePacient, Convert.ToDateTime(dateBirthPacient), genPacient, dateIncoming, nameDoctor, mailDoctor, origenOrder);
            AddSpacerTable(doc, "");
            System.Diagnostics.Debug.WriteLine(newArea);
            AddAreaTable(doc, newArea.ToUpper());
            //AddTitleDetailTable(doc);
        }
    }

    #endregion sizeSheetArea

    #region AddTitleDetailTable

    public void AddTitleDetailTable(
            Document doc
            )
    {
        // Crear una tabla para los titulo del detalle de resultados con 5 columnas y 1 fila
        PdfPTable headerResultsTable = new PdfPTable(5);
        headerResultsTable.WidthPercentage = 100;
        float[] columnWidths = { 40, 15, 15, 30, 0 };
        headerResultsTable.SetWidths(columnWidths);
        headerResultsTable.AddCell(GetTitleDetailCell("EXAMEN", 1, 1, 0, 0));
        headerResultsTable.AddCell(GetTitleDetailCell("RESULTADO", 1, 1, 0, 1));
        headerResultsTable.AddCell(GetTitleDetailCell("UNIDADES", 1, 1, 0, 1));
        headerResultsTable.AddCell(GetTitleDetailCell("REFERENCIA", 1, 1, 0, 0));
        headerResultsTable.AddCell(GetTitleDetailCell("", 1, 1, 0, 0));
        doc.Add(headerResultsTable);
        CurrentY = CurrentY + headerResultsTable.TotalHeight;
    }

    private static PdfPCell GetTitleDetailCell(string text, int rowspan, int colspan, int borders, int aligntext)
    {
        BaseFont baseFont = BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
        //Font font = new Font(baseFont, fontSize, fontStyle, BaseColor.BLACK);
        iTextSharp.text.Font fontHeader = FontFactory.GetFont("Arial", 7, 1, BaseColor.WHITE);
        string[] backgroundTitleDetail = "0,85,139,255".Split(",");
        PdfPCell cell = new PdfPCell(new Phrase(text, fontHeader))
        {
            HorizontalAlignment = aligntext,
            VerticalAlignment = Element.ALIGN_MIDDLE,
            Padding = 3,
            BackgroundColor = new BaseColor(
                        Convert.ToInt32(backgroundTitleDetail[0]),
                        Convert.ToInt32(backgroundTitleDetail[1]),
                        Convert.ToInt32(backgroundTitleDetail[2]),
                        Convert.ToInt32(backgroundTitleDetail[3])
                        ),
            Rowspan = rowspan,
            Colspan = colspan,
        };
        return borderCells(cell, borders);
    }

    #endregion AddTitleDetailTable

    #region AddProfileTable

    public void AddProfileTable(Document doc, string profile)
    {
        // Crear una tabla para un el perfil de 1 columna y 1 fila
        PdfPTable profileTable = new PdfPTable(1);
        profileTable.WidthPercentage = 100;
        // Definir la distribución de las columnas en la fila
        float[] columnWidths = { 100f };
        profileTable.SetWidths(columnWidths);
        profileTable.AddCell(GetProfileCell(profile, 1, 0, 0));
        doc.Add(profileTable);
        CurrentY = CurrentY + profileTable.TotalHeight;
    }

    // Método para crear una celda del pefil del examen con formato personalizado
    private static PdfPCell GetProfileCell(string text, int rowspan, int borders, int align)
    {
        BaseFont baseFont = BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
        iTextSharp.text.Font fontProfile = FontFactory.GetFont("Arial", 8, 1, BaseColor.BLACK);
        string[] backgroundProfile = "128,158,128,255".Split(",");
        PdfPCell cell = new PdfPCell(new Phrase(text, fontProfile))
        {
            HorizontalAlignment = align,
            VerticalAlignment = Element.ALIGN_MIDDLE,
            Padding = 2,
            BackgroundColor = new BaseColor(
                Convert.ToInt32(backgroundProfile[0]),
                Convert.ToInt32(backgroundProfile[1]),
                Convert.ToInt32(backgroundProfile[2]),
                Convert.ToInt32(backgroundProfile[3])
                ),
            Rowspan = rowspan
        };
        return borderCells(cell, borders);
    }

    #endregion AddProfileTable

    #region AddCategoryTable

    public void AddCategoryTable(Document doc, string profile)
    {
        // Crear una tabla para la categoria de 1 columna y 1 fila
        PdfPTable categoryTable = new PdfPTable(1);
        categoryTable.WidthPercentage = 100;
        // Definir la distribución de las columnas en la fila
        float[] columnWidths = { 100f };
        categoryTable.SetWidths(columnWidths);
        categoryTable.AddCell(GetCategoryCell(profile, 1, 0, 0));
        doc.Add(categoryTable);
        CurrentY = CurrentY + categoryTable.TotalHeight;
    }

    // Método para crear una celda para la categoria del examen con formato personalizado
    private static PdfPCell GetCategoryCell(string text, int rowspan, int borders, int aligntext)
    {
        BaseFont baseFont = BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
        iTextSharp.text.Font fontCategory = FontFactory.GetFont("Arial", 8, 3, BaseColor.DARK_GRAY);
        PdfPCell cell = new PdfPCell(new Phrase(text, fontCategory))
        {
            HorizontalAlignment = aligntext,
            VerticalAlignment = Element.ALIGN_MIDDLE,
            Padding = 2,
            //BackgroundColor = new BaseColor(128, 158, 128),
            Rowspan = rowspan
        };
        return borderCells(cell, borders);
    }


    #endregion AddCategoryTable

    #region AddDetailResultsTable

    public void AddDetailResultsTable(
            Document doc,
            string test,
            string result,
            string units,
            string reference,
            string other
            )
    {
        PdfPTable detailResultsTable = new PdfPTable(5);
        detailResultsTable.WidthPercentage = 100;
        float[] columnWidths = { 40, 15, 15, 30, 0 };
        detailResultsTable.SetWidths(columnWidths);
        detailResultsTable.AddCell(GetDetailResultsCell(test, 1, 1, 2, 0));
        detailResultsTable.AddCell(GetDetailResultsCell(result, 1, 1, 2, 1));
        detailResultsTable.AddCell(GetDetailResultsCell(units, 1, 1, 2, 1));
        detailResultsTable.AddCell(GetDetailResultsCell(reference, 1, 1, 2, 0));
        detailResultsTable.AddCell(GetDetailResultsCell(other, 1, 1, 2, 0));
        doc.Add(detailResultsTable);
        CurrentY = CurrentY + detailResultsTable.TotalHeight;
    }

    // Método para crear las celdas de las cabeceras de los resultados de la orden
    private static PdfPCell GetDetailResultsCell(string text, int rowspan, int colspan, int borders, int aligntext)
    {
        BaseFont baseFont = BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
        iTextSharp.text.Font fontResultsDetail = FontFactory.GetFont("Webdings", 8, 0, BaseColor.BLACK);
        PdfPCell cell = new PdfPCell(new Phrase(text, fontResultsDetail))
        {
            HorizontalAlignment = aligntext,
            VerticalAlignment = Element.ALIGN_MIDDLE,
            Padding = 2,
            BackgroundColor = new BaseColor(0, 0, 0, 0),
            Rowspan = rowspan,
            Colspan = colspan,
            BorderWidthBottom = 1,
            BorderColorBottom = new BaseColor(200, 200, 200, 75),
        };
        return borderCells(cell, borders);
    }

    #endregion AddDetailResultsTable

    #region sizeSheetDA

    public void sizeSheetDA(
    Document doc,
    PdfWriter writer,
    float y,
    string newArea,
    string oldArea,
    string numOrder,
    string uaValidador,
    string validationlabel,
    DateTime faValidacion,
    string estadoOrden,
    string namePacient,
    string idPacient,
    string agePacient,
    char genPacient,
    DateTime dateBirthPacient,
    string nameDoctor,
    string mailDoctor,
    string phoneDoctor,
    string origenOrden,
    string firma
    )
    {
        string ValidatorSign = "true";
        if (y >= 715)
        {
            if (newArea != oldArea && oldArea != null)
            {
                AddValidatorTable(doc, uaValidador, validationlabel + euaValidado + " " + faValidacion.ToString("dd/MM/yyyy HH:mm"), ValidatorSign, firma);
                AddSpacerTable(doc, " ");
            }
            CurrentY = 0;
            doc.NewPage();
            AddHeaderTable(doc, numOrder, barCodeImage(writer, numOrder));
            AddSubHeaderTable(doc, dateIncoming.ToString(), numOrder, estadoOrden);
            AddOrderDataTable(doc, namePacient, idPacient, agePacient, Convert.ToDateTime(dateBirthPacient), genPacient, dateIncoming, nameDoctor, mailDoctor, origenOrden);
            AddSpacerTable(doc, " ");
            System.Diagnostics.Debug.WriteLine(newArea);
            AddAreaTable(doc, newArea.ToUpper());
            sizeSheetArea(doc, writer, CurrentY, newArea, numOrder, NombreCompletoPaciente, idPacient, agePacient, dateBirthPacient.ToString(), genPacient, nameDoctor, mailDoctor, origenOrden, estadoOrden);
            AddTitleDetailTable(doc);
        }
        else
        {
            if (newArea != oldArea && oldArea != null)
            {
                AddValidatorTable(doc, uaValidador, validationlabel + euaValidado + " " + faValidacion.ToString("dd/MM/yyyy HH:mm"), ValidatorSign, firma);
                AddSpacerTable(doc, " ");
                AddAreaTable(doc, newArea.ToUpper());
                sizeSheetArea(doc, writer, CurrentY, newArea, numOrder, NombreCompletoPaciente, idPacient, agePacient, dateBirthPacient.ToString(), genPacient, nameDoctor, mailDoctor, origenOrden, estadoOrden);
                AddTitleDetailTable(doc);
            }
            else
            {
                Console.WriteLine(newArea);
                AddAreaTable(doc, newArea.ToUpper());
                sizeSheetArea(doc, writer, CurrentY, newArea, numOrder, NombreCompletoPaciente, idPacient, agePacient, dateBirthPacient.ToString(), genPacient, nameDoctor, mailDoctor, origenOrden, estadoOrden);
                AddTitleDetailTable(doc);
            }
        }
    }

    #endregion sizeSheetDA

    #region AddValidatorTable

    public void AddValidatorTable(
    Document doc,
    string userValidatorSign,
    string userNameSign,
    string yesno,
    string firma
    )
    {
        // Crear una tabla para el header con 1 columnas y 2 filas
        PdfPTable validatorTable = new PdfPTable(2);
        validatorTable.WidthPercentage = 100;
        float[] columnWidths = { 80f, 20f };
        validatorTable.SetWidths(columnWidths);
        validatorTable.AddCell(GetValidatorCell(userNameSign, 1, 1, 0)); //Fila 1I
        if (yesno == "true")
        {
            //iTextSharp.text.Image firma = iTextSharp.text.Image.GetInstance(Environment.CurrentDirectory + "\\img\\" + uaValidador + ".png");


            Byte[] iByte = Convert.FromBase64String(firma);
            //iTextSharp.text.Image sign = iTextSharp.text.Image.GetInstance(iByte);
            //sign.ScaleAbsolute(75f, 34f); // Ajustar el tamaño de la firma
            //PdfPCell signCell = new PdfPCell(sign);
            //signCell.HorizontalAlignment = Element.ALIGN_LEFT;
            //signCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            //signCell.Border = Rectangle.NO_BORDER;
            //signCell.BackgroundColor = new BaseColor(255, 255, 255, 0);
            //validatorTable.AddCell(signCell); //Fila 1D
        }
        else
        {
            validatorTable.AddCell(GetValidatorCell("", 1, 1, 0)); //Fila 1D
        }

        doc.Add(validatorTable);
        CurrentY = CurrentY + validatorTable.TotalHeight;
        Console.WriteLine(validatorTable.TotalHeight);
    }

    // GetValidatorCell
    private static PdfPCell GetValidatorCell(string text, int rowspan, int colspan, int borders)
    {
        BaseFont baseFont = BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
        //Font font = new Font(baseFont, fontSize, fontStyle, BaseColor.BLACK);
        iTextSharp.text.Font fontValidator = FontFactory.GetFont("Arial", 7, 2, BaseColor.BLACK);
        PdfPCell cell = new PdfPCell(new Phrase(text, fontValidator))
        {
            HorizontalAlignment = Element.ALIGN_RIGHT,
            VerticalAlignment = Element.ALIGN_MIDDLE,
            Padding = 3,
            BackgroundColor = new BaseColor(0, 0, 0, 0),
            Rowspan = rowspan,
            Colspan = colspan,
        };
        return borderCells(cell, borders);
    }

    #endregion AddValidatorTable

    #region AddCommentTestTable

    public void AddCommentTestTable(Document doc, string comment, bool show)
    {
        if (show)
        {
            // Crear una tabla para un título de 1 columna y 1 fila
            PdfPTable commentTestTable = new PdfPTable(1);
            commentTestTable.WidthPercentage = 100;
            // Definir la distribución de las columnas en la fila
            float[] columnWidths = { 100f };
            commentTestTable.SetWidths(columnWidths);
            commentTestTable.AddCell(GetCommentTestCell("     " + comment, 1, 2, 0));
            CurrentY = CurrentY + commentTestTable.TotalHeight;
            doc.Add(commentTestTable);
        }
    }

    private static PdfPCell GetCommentTestCell(string text, int rowspan, int borders, int align)
    {
        BaseFont baseFont = BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
        iTextSharp.text.Font commentTestArea = FontFactory.GetFont("Arial", 8, 2, BaseColor.BLACK);
        PdfPCell cell = new PdfPCell(new Phrase(text, commentTestArea))
        {
            HorizontalAlignment = align,
            VerticalAlignment = Element.ALIGN_MIDDLE,
            Padding = 1,
            BackgroundColor = new BaseColor(255, 255, 255, 0),
            Rowspan = rowspan,
            BorderWidthBottom = 1,
            BorderColorBottom = new BaseColor(200, 200, 200, 75),
        };
        return borderCells(cell, borders);
    }

    #endregion AddCommentTestTable

    #region borderCells
    private static PdfPCell borderCells(PdfPCell c, int option)
    {
        switch (option)
        {
            case 0:
                c.Border = Rectangle.NO_BORDER;
                break;
            case 1:
                c.Border = Rectangle.TOP_BORDER;
                break;
            case 2:
                c.Border = Rectangle.BOTTOM_BORDER;
                break;
            case 3:
                c.Border = Rectangle.LEFT_BORDER;
                break;
            case 4:
                c.Border = Rectangle.RIGHT_BORDER;
                break;
            case 5:
                break;
            case 6:
                c.Border = Rectangle.TOP_BORDER | Rectangle.BOTTOM_BORDER;
                break;
        }
        return c;
    }
    #endregion borderCells
}
