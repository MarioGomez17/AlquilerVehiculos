using ALQUILER_VEHICULOS.Models;
using MySql.Data.MySqlClient;
using iText.IO.Font.Constants;
using iText.IO.Image;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using OfficeOpenXml.Style;
using OfficeOpenXml;
using System.Net.Mail;
using System.Net;
namespace ALQUILER_VEHICULOS.Reports
{
    public class ReporteAlquileresAlquilador
    {
        public ModeloUsuario Usuario { get; set; }
        public ModeloAlquilador Alquilador { get; set; }
        public string ConsultaSQL { get; set; }
        public ReporteAlquileresAlquilador(int IdUsuario)
        {
            ModeloUsuario ModeloUsuario = new();
            ModeloAlquilador ModeloAlquilador = new();
            this.Usuario = ModeloUsuario.TraerUsuario(IdUsuario);
            this.Alquilador = ModeloAlquilador.TraerAlquiladorUsuario(IdUsuario);
            this.ConsultaSQL = "SELECT " +
                    "alquiler_vehiculos.alquiler.FechaIncio_Alquiler, " +
                    "alquiler_vehiculos.alquiler.FechaFin_Alquiler, " +
                    "alquiler_vehiculos.alquiler.Precio_Alquiler, " +
                    "alquiler_vehiculos.tipo_vehiculo.Nombre_TipoVehiculo, " +
                    "alquiler_vehiculos.marca_vehiculo.Nombre_MarcaVehiculo, " +
                    "alquiler_vehiculos.linea_vehiculo.Nombre_LineaVehiculo, " +
                    "alquiler_vehiculos.lugar_alquiler.Nombre_LugarAlquiler, " +
                    "alquiler_vehiculos.metodo_pago.Nombre_MetodoPago, " +
                    "alquiler_vehiculos.estado_pago_alquiler.Nombre_EstadoPagoAlquiler, " +
                    "alquiler_vehiculos.seguro_alquiler.Nombre_SeguroAlquiler, " +
                    "alquiler_vehiculos.seguro_alquiler.Precio_SeguroAlquiler, " +
                    "alquiler_vehiculos.estado_alquiler.Nombre_EstadoAlquiler " +
                    "FROM alquiler_vehiculos.alquiler " +
                    "INNER JOIN alquiler_vehiculos.estado_alquiler " +
                    "ON alquiler_vehiculos.estado_alquiler.Id_EstadoAlquiler = alquiler_vehiculos.alquiler.Estado_Alquiler " +
                    "INNER JOIN alquiler_vehiculos.estado_pago_alquiler " +
                    "ON alquiler_vehiculos.estado_pago_alquiler.Id_EstadoPagoAlquiler = alquiler_vehiculos.alquiler.EstadoPago_Alquiler " +
                    "INNER JOIN alquiler_vehiculos.seguro_alquiler " +
                    "ON alquiler_vehiculos.seguro_alquiler.Id_SeguroAlquiler = alquiler_vehiculos.alquiler.Seguro_Alquiler " +
                    "INNER JOIN alquiler_vehiculos.metodo_pago " +
                    "ON alquiler_vehiculos.metodo_pago.Id_MetodoPago = alquiler_vehiculos.alquiler.MeotodoPago_Alquiler " +
                    "INNER JOIN alquiler_vehiculos.lugar_alquiler " +
                    "ON alquiler_vehiculos.lugar_alquiler.Id_LugarAlquiler = alquiler_vehiculos.alquiler.Lugar_Alquiler " +
                    "INNER JOIN alquiler_vehiculos.vehiculo " +
                    "ON alquiler_vehiculos.vehiculo.Id_Vehiculo = alquiler_vehiculos.alquiler.Vehiculo_Alquiler " +
                    "INNER JOIN alquiler_vehiculos.linea_vehiculo " +
                    "ON alquiler_vehiculos.linea_vehiculo.Id_LineaVehiculo = alquiler_vehiculos.vehiculo.Linea_Vehiculo " +
                    "INNER JOIN alquiler_vehiculos.marca_vehiculo " +
                    "ON alquiler_vehiculos.marca_vehiculo.Id_MarcaVehiculo = alquiler_vehiculos.linea_vehiculo.MarcaVehiculo_LineaVehiculo " +
                    "INNER JOIN alquiler_vehiculos.tipo_vehiculo " +
                    "ON alquiler_vehiculos.tipo_vehiculo.Id_TipoVehiculo = alquiler_vehiculos.marca_vehiculo.TipoVehiculo_MarcaVehiculo " +
                    "WHERE alquiler_vehiculos.alquiler.Alquilador_Alquiler = " + this.Alquilador.Id + " " +
                    "ORDER BY alquiler_vehiculos.alquiler.FechaIncio_Alquiler DESC";
        }
        public void GenerarReporteAlquileresAlquiladorPDF()
        {
            PdfWriter PdrWriter = new("./wwwroot/Reportes/DocumentosIniciales/ReporteAlquileresAlquilador.pdf");
            PdfDocument PDF = new(PdrWriter);
            Document Documento = new(PDF, new PageSize(1200, 600));
            Documento.SetMargins(60, 20, 55, 20);
            PdfFont FuenteColumnas = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD);
            PdfFont FuenteContenido = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);
            string[] Columnas = ["#", "FECHA INICIO", "FECHA FIN", "PRECIO", "VEHÍCULO", "LUGAR", "MÉTODO DE PAGO", "ESTADO DEL PAGO", "SEGURO", "ESTADO"];
            float[] Tamanos = [1, 4, 4, 3, 8, 5, 5, 5, 7, 3];
            Table Tabla = new(UnitValue.CreatePercentArray(Tamanos));
            Tabla.SetWidth(UnitValue.CreatePercentValue(100));
            Tabla.SetMarginTop(80);
            foreach (string Columna in Columnas)
            {
                Tabla.AddHeaderCell(new Cell().Add(new Paragraph(Columna).SetFont(FuenteColumnas).SetTextAlignment(TextAlignment.CENTER)));
            }
            MySqlConnection ConexionBD = ModeloConexion.Conect();
            try
            {
                ConexionBD.Open();
                MySqlCommand Comando = new(ConsultaSQL, ConexionBD);
                MySqlDataReader Lector = Comando.ExecuteReader();
                if (Lector.HasRows)
                {
                    int Indice = 1;
                    while (Lector.Read())
                    {
                        Tabla.AddCell(new Cell().Add(new Paragraph(Indice.ToString()).SetFont(FuenteContenido).SetTextAlignment(TextAlignment.CENTER)));
                        Tabla.AddCell(new Cell().Add(new Paragraph(new DateOnly(Lector.GetDateTime(0).Year, Lector.GetDateTime(0).Month, Lector.GetDateTime(0).Day).ToString()).SetFont(FuenteContenido).SetTextAlignment(TextAlignment.CENTER)));
                        Tabla.AddCell(new Cell().Add(new Paragraph(new DateOnly(Lector.GetDateTime(1).Year, Lector.GetDateTime(1).Month, Lector.GetDateTime(1).Day).ToString()).SetFont(FuenteContenido).SetTextAlignment(TextAlignment.CENTER)));
                        Tabla.AddCell(new Cell().Add(new Paragraph(Lector.GetFloat(2).ToString()).SetFont(FuenteContenido).SetTextAlignment(TextAlignment.CENTER)));
                        Tabla.AddCell(new Cell().Add(new Paragraph(Lector.GetString(3) + " " + Lector.GetString(4) + " " + Lector.GetString(5)).SetFont(FuenteContenido).SetTextAlignment(TextAlignment.CENTER)));
                        Tabla.AddCell(new Cell().Add(new Paragraph(Lector.GetString(6)).SetFont(FuenteContenido).SetTextAlignment(TextAlignment.CENTER)));
                        Tabla.AddCell(new Cell().Add(new Paragraph(Lector.GetString(7)).SetFont(FuenteContenido).SetTextAlignment(TextAlignment.CENTER)));
                        Tabla.AddCell(new Cell().Add(new Paragraph(Lector.GetString(8)).SetFont(FuenteContenido).SetTextAlignment(TextAlignment.CENTER)));
                        Tabla.AddCell(new Cell().Add(new Paragraph(Lector.GetString(9) + " (" + Lector.GetFloat(10).ToString() + ")").SetFont(FuenteContenido).SetTextAlignment(TextAlignment.CENTER)));
                        Tabla.AddCell(new Cell().Add(new Paragraph(Lector.GetString(11)).SetFont(FuenteContenido).SetTextAlignment(TextAlignment.CENTER)));
                        Indice++;
                    }
                }
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            finally
            {
                ConexionBD.Close();
            }
            Documento.Add(Tabla);
            Documento.Close();
            var RutaArchivo = "D:/UNIVERSIDAD/WEB/ALQUILER_VEHICULOS/wwwroot/imagenes/Logo.png";
            var Logo = new Image(ImageDataFactory.Create(RutaArchivo)).SetWidth(80).SetHeight(80);
            var PLogo = new Paragraph("").Add(Logo);
            var Titulo = new Paragraph("REPORTE ALQUILERES ALQUILADOR");
            Titulo.SetTextAlignment(TextAlignment.CENTER);
            Titulo.SetFontSize(15);
            var Fecha = DateTime.Now.ToString("dd-MM-yyyy");
            var Hora = DateTime.Now.ToString("hh:mm:ss");
            var FechaHora = new Paragraph("FECHA: " + Fecha + " Hora: " + Hora);
            FechaHora.SetFontSize(12);
            PdfDocument PDFDoc = new(new PdfReader("./wwwroot/Reportes/DocumentosIniciales/ReporteAlquileresAlquilador.pdf"), new PdfWriter("./wwwroot/Reportes/ReportesFinalizados/ReportesAlquilerAlquilador/ReporteAlquileresAlquilador.pdf"));
            Document Doc = new(PDFDoc);
            int Numeros = PDFDoc.GetNumberOfPages();
            for (int i = 1; i <= Numeros; i++)
            {
                float Y = PDFDoc.GetPage(i).GetPageSize().GetTop() - 15;
                Doc.ShowTextAligned(PLogo, 80, Y, i, TextAlignment.CENTER, VerticalAlignment.TOP, 0);
                Doc.ShowTextAligned(Titulo, 350, Y - 15, i, TextAlignment.CENTER, VerticalAlignment.TOP, 0);
                Doc.ShowTextAligned(FechaHora, 670, Y - 15, i, TextAlignment.CENTER, VerticalAlignment.TOP, 0);
                Doc.ShowTextAligned(new Paragraph(string.Format("PÁGINA {0} DE {1}", i, Numeros)), PDFDoc.GetPage(i).GetPageSize().GetWidth() / 2, PDFDoc.GetPage(i).GetPageSize().GetBottom() + 30, i, TextAlignment.CENTER, VerticalAlignment.TOP, 0);
            }
            Doc.Close();
        }
        public void GenerarReporteAlquileresAlquiladorEXCEL()
        {
            string RutaArchivo = "./wwwroot/Reportes/ReportesFinalizados/ReportesAlquilerAlquilador/ReporteAlquileresAlquilador.xlsx";
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using ExcelPackage PaqueteExcel = new();
            PaqueteExcel.Workbook.Properties.Author = Usuario.Nombre + " " + Usuario.Apellido;
            PaqueteExcel.Workbook.Properties.Title = "Reporte de Alquileres Realizados";
            ExcelWorksheet HojaCalculo = PaqueteExcel.Workbook.Worksheets.Add("Reporte");
            HojaCalculo.Cells[1, 1].Value = "#";
            HojaCalculo.Cells[1, 2].Value = "FECHA INICIO";
            HojaCalculo.Cells[1, 3].Value = "FECHA FIN";
            HojaCalculo.Cells[1, 4].Value = "PRECIO";
            HojaCalculo.Cells[1, 5].Value = "VEHÍCULO";
            HojaCalculo.Cells[1, 6].Value = "LUGAR";
            HojaCalculo.Cells[1, 7].Value = "MÉTODO DE PAGO";
            HojaCalculo.Cells[1, 8].Value = "ESTADO DEL PAGO";
            HojaCalculo.Cells[1, 9].Value = "SEGURO";
            HojaCalculo.Cells[1, 10].Value = "ESTADO";
            using (var Encabezados = HojaCalculo.Cells[1, 1, 1, 10])
            {
                Encabezados.Style.Font.Bold = true;
                Encabezados.Style.Font.Color.SetColor(System.Drawing.Color.White); ;
                Encabezados.Style.Fill.PatternType = ExcelFillStyle.Solid;
                Encabezados.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Black);
                Encabezados.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            }
            MySqlConnection ConexionBD = ModeloConexion.Conect();
            try
            {
                ConexionBD.Open();
                MySqlCommand Comando = new(ConsultaSQL, ConexionBD);
                MySqlDataReader Lector = Comando.ExecuteReader();
                if (Lector.HasRows)
                {
                    int Celda = 2;
                    int Indice = 1;
                    while (Lector.Read())
                    {
                        HojaCalculo.Cells[Celda, 1].Value = Indice;
                        HojaCalculo.Cells[Celda, 2].Value = new DateOnly(Lector.GetDateTime(0).Year, Lector.GetDateTime(0).Month, Lector.GetDateTime(0).Day).ToString();
                        HojaCalculo.Cells[Celda, 3].Value = new DateOnly(Lector.GetDateTime(1).Year, Lector.GetDateTime(1).Month, Lector.GetDateTime(1).Day).ToString();
                        HojaCalculo.Cells[Celda, 4].Value = Lector.GetFloat(2);
                        HojaCalculo.Cells[Celda, 5].Value = Lector.GetString(3) + " " + Lector.GetString(4) + " " + Lector.GetString(5);
                        HojaCalculo.Cells[Celda, 6].Value = Lector.GetString(6);
                        HojaCalculo.Cells[Celda, 7].Value = Lector.GetString(7);
                        HojaCalculo.Cells[Celda, 8].Value = Lector.GetString(8);
                        HojaCalculo.Cells[Celda, 9].Value = Lector.GetString(9) + " (" + Lector.GetFloat(10).ToString() + ")";
                        HojaCalculo.Cells[Celda, 10].Value = Lector.GetString(11);
                        for (int Columna = 1; Columna <= 10; Columna++)
                        {
                            HojaCalculo.Cells[Celda, Columna].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        }
                        Celda++;
                        Indice++;
                    }
                }
            }
            catch (Exception) { }
            finally
            {
                ConexionBD.Close();
            }
            HojaCalculo.Cells.AutoFitColumns();
            FileInfo ArchivoExcel = new(RutaArchivo);
            PaqueteExcel.SaveAs(ArchivoExcel);
        }
        public void EnviarReportesPorCorreo(string CorreoReceptor)
        {
            GenerarReporteAlquileresAlquiladorEXCEL();
            GenerarReporteAlquileresAlquiladorPDF();
            string ReporteExcel = "./wwwroot/Reportes/ReportesFinalizados/ReportesAlquilerAlquilador/ReporteAlquileresAlquilador.xlsx";
            string ReportePDF = "./wwwroot/Reportes/ReportesFinalizados/ReportesAlquilerAlquilador/ReporteAlquileresAlquilador.pdf";
            string CorreoEmisor = "mariog.101200@hotmail.com";
            string Asunto = "Reportes de Alquileres Realizados por " + this.Usuario.Nombre + " " + this.Usuario.Apellido;
            string Mensaje = "Adjunto encontrarás los reportes en Excel y PDF";
            SmtpClient ClienteAMTP = new("smtp-mail.outlook.com", 587)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential(CorreoEmisor, "")
            };
            MailMessage MensajeCorreo = new (CorreoEmisor, CorreoReceptor, Asunto, Mensaje);
            Attachment ReporteAdjuntoExcel = new (ReporteExcel);
            MensajeCorreo.Attachments.Add(ReporteAdjuntoExcel);
            Attachment ReporteAdjuntoPDF = new (ReportePDF);
            MensajeCorreo.Attachments.Add(ReporteAdjuntoPDF);
            ClienteAMTP.Send(MensajeCorreo);
        }
    }
}