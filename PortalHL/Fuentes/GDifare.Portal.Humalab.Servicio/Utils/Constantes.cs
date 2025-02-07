using GDifare.Portal.Humalab.Servicio.Modelos.PedidosCliente;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDifare.Portal.Humalab.Servicio.Utils
{
    public struct Notificaciones
    { 
        public const string TituloDanger = "danger";
        public const string TituloSuccess = "success";
        public const string TituloWarning = "warning";
		public const string TituloConfirmarPedido = "Recolectar Pedido";
        public const string TituloRechazaMuestra = "Rechaza Muestra";

        public const string DangerError = "Se ha producido un error en el sistema o no existen pedidos";
		public const string DangerOperador = "No se ha podido obtener el Operador Logístico, póngase en contacto con Soporte Técnico";
		public const string DangerFecha = "Fecha Desde no puede ser mayor a Fecha Hasta";
        public const string DangerRecolecta = "No se ha recolectado ninguna muestra. No se puede recolectar el pedido";
		public const string DangerImprimir = "Cree primero una orden para imprimir las etiquetas de las muestras";
        public const string DangerValidacionEnviaLis = "Existen muestras por receptar. No puede enviar a LIS.";
        public const string DangerEnviaLis = "Se ha producido un error en el envío a Avalab. Póngase en contacto con Soporte Técnico";
        public const string DangerRecibeOrden = "Error al actualizar la orden. Póngase en contacto con Soporte Técnico";
		public const string DangerActualizaEstadoM = "Error al actualizar el estado de la muestra. Póngase en contacto con Soporte Técnico";
        public const string DangerActualizaEstadoP = "Error al actualizar el estado de la prueba. Póngase en contacto con Soporte Técnico";
		public const string DangerConsultaCatalogo = "Error en la consulta del catálogo. Póngase en contacto con Soporte Técnico";
		public const string DangerPdfResultados = "El resultado contiene un formato no válido";
        public const string DangerPdfResult = "Aún esta pendiente de validar los resultados de la orden.";

        public const string Success = "Proceso ejecutado correctamente";
        public const string SuccessOrdenNueva = "Orden creada exitósamente";
		public const string SuccessPedidoNuevo = "Pedido creado exitósamente";
        public const string SuccessPedidoEliminado = "El pedido se ha elimanado";
        public const string SuccessOrdenActualizada = "La orden ha sido actualizada";
        public const string SuccessPedidoRecolectado = "Pedido actualizado correctamente";
        public const string SuccessOrdenGalileoEnviada = "Orden enviada a LIS satisfactoriamente";
        public const string SuccessOrdenRecibida = "Orden actualizada correctamente";

        public const string WarningCampos = "Complete todos los campos";
        public const string WarningObservaciones = "No hay Observaciones para esta orden";
        public const string WarningExamen = "No ha ingresado exámenes para la orden";
        public const string WarningOrden = "La orden ya fue creada";
        public const string WarningPdf = "No hay resultados para visualizar";
		public const string WarningOrdenEstado = "Para generar un Pedido seleccione Órdenes con estado GENERADA";
		public const string WarningConfirmarPedido = "Existen muestras por recolectar. ¿Desea continuar?";
		public const string WarningEliminarPrueba = "Si elimina esta prueba, la orden será cancelada y deberá crear una nueva orden";
		public const string WarningEliminarOrden = "¿Quieres eliminar la orden?";
        public const string WarningEliminarPedido = "¿Quieres eliminar el Pedido?";
		public const string WarningRechazaMuestras = "Se rechazarán todas las muestras de la orden. ¿Desea continuar?";

        public const string CedulaInvalida = "Cédula Inválida";
		public const string CorreoInvalido = "Formato de correo incorrecto";

    }

    public struct CodigoBarra
    {
        public const string SeisDigitos = "000000";
        public const string CincoDigitos = "00000";
        public const string CuatroDigitos = "0000";
        public const string TresDigitos = "000";
        public const string DosDigitos = "00";
        public const string UnDigito = "0";


    }

	public struct Transaccion
	{
		public const int Default = 0;
		public const int Correcta = 201;
		public const int Error = 400;

	}

	public struct Caracteres
	{
		public const string Guion = "-";
		public const string Dolar = "$";
	}


	public struct Numeros
	{
		public const int Cero = 0;
		public const int Uno = 1;
		public const int Dos = 2;
		public const int Tres = 3;
		public const int Cuatro = 4;
		public const int Cinco = 5;
		public const int Seis = 6;
		public const int Siete = 7;
		public const int Ocho = 8;
		public const int Nueve = 9;
        public const int Diez = 10;
        public const int Once = 11;
		public const int Doce = 12;
        public const int Trece = 13;
        public const int CuatroCientosUno = 401;
	}

    public struct  NumDecimales
    {
		public const float Cero = 0.00f;
		public const string CeroString = "0.00";
		public const string DecimalFloat = "0.##";
    }

    public struct BreadCrumbs
	{
		public const string ListaOrden = "/ConsultarOrdenes";
		public const string NuevaOrden = "/ConsultarOrdenes/NuevaOrden";
		public const string ListaPedido = "/Pedidos";
	}

	public struct Estados
	{
		public const string Orden = "EstadoOrden";
		public const string Prueba = "EstadoPrueba";
		public const string Muestra = "EstadoMuestra";
        public const string Pedido = "EstadoPedido";


        public const string Verdadero = "True";
		public const string Falso = "False";

		public const string Cancelado = "Cancelada";
		public const string Validado = "Validado";
		public const string Generado = "Generada";
		public const string PorRecolectar = "Por Recolectar";
		public const string Anulado = "Anulado";

		public const string MuestraPorRecolectar = "Por Recolectar";
		public const string MuestraRecolectada = "Recolectada";
		public const string MuestraRecibida = "Recibida";
		public const string MuestraRechazadaOp = "Rechazada Operador";
        public const string MuestraRechazadaLab = "Rechazada Laboratorista";
		public const string MuestraCancelada = "Cancelada";


        public const string OrdenPorRecolectar = "Por Recolectar";
        public const string OrdenRecolectada = "Recolectado Total";
        public const string OrdenRechazada = "Rechazada";
        public const string OrdenRecolectadaParcial = "Recolectado Parcial";
        public const string OrdenRecibida = "Recibida";
        public const string OrdenRecibidaParcial = "Recibida Parcial";
        public const string OrdenEnAnalisis = "En Analisis";
		public const string OrdenEnviado = "Enviado";
        public const string OrdenEnviadoParcial = "Enviado Parcial";
		public const string RechazadaLabora = "Rechazada";

        public const string PedidoPorRecolectar = "Por Recolectar";
        public const string PedidoRecolectado = "Recolectado";
        public const string PedidoRecolectadoParcial = "Recolectado Parcial";
        public const string PedidoAnulado = "Anulado";


    }

    public struct Tablas
    {
		public const string TOrdenes = "TOrdenes";
		public const string TExamen = "TExamenOrden";
		public const string TItemExamen = "TbItemsExamen";
		public const string TItemOrdenes = "TbFila";

    }

	public struct Combos
	{
		public const string EstadoOrden = "cmbEstadobOrden";
		public const string EstadoPedido = "cmbEstadoPedido";
    }


	public struct PDF
	{
		public const string Etiquetas = "EtiquetasHumaLab.pdf";
		public const string RutaPdf = @"C:\";
	}


	public struct Funcion
	{
        public const string EliminarPedido = "EliminarPedido";
        public const string EliminarOrden = "EliminarConsultaOrden";
		public const string EliminarPrueba = "EliminarPruebaOrden";

		//Administrador ordenes
		public const string EliminarOrdenAdmin = "EliminarConsultaOrdenAdmin";
        public const string EliminarPruebaOrdAdmin = "EliminarPrbOrdenAdmin";
    }
	
	public struct Estandar
	{
		public const string Fecha = "1/1/2001 0:00:00";
	}

    public struct Roles
    {
        public const int RolOperador = 7;
    }
}
