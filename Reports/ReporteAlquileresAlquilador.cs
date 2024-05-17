using ALQUILER_VEHICULOS.Models;
using MySql.Data.MySqlClient;
using OfficeOpenXml.Style;
using OfficeOpenXml;
using System.Net.Mail;
using System.Net;
using SelectPdf;

namespace ALQUILER_VEHICULOS.Reports
{
    public class ReporteAlquileresAlquilador
    {
        public ModeloUsuario Usuario { get; set; }
        public ModeloAlquilador Alquilador { get; set; }
        public ModeloEmpresa Empresa { get; set; }
        public string ConsultaSQL { get; set; }
        public ReporteAlquileresAlquilador(int IdUsuario)
        {
            ModeloUsuario ModeloUsuario = new();
            ModeloAlquilador ModeloAlquilador = new();
            this.Usuario = ModeloUsuario.TraerUsuario(IdUsuario);
            this.Alquilador = ModeloAlquilador.TraerAlquiladorUsuario(IdUsuario);
            this.Empresa = new();
            this.ConsultaSQL = "SELECT " +
                                "alquiler_vehiculos.alquiler.FechaIncio_Alquiler, " +
                                "alquiler_vehiculos.alquiler.FechaFin_Alquiler, " +
                                "alquiler_vehiculos.tipo_vehiculo.Nombre_TipoVehiculo, " +
                                "alquiler_vehiculos.marca_vehiculo.Nombre_MarcaVehiculo, " +
                                "alquiler_vehiculos.linea_vehiculo.Nombre_LineaVehiculo, " +
                                "alquiler_vehiculos.ciudad.Nombre_Ciudad, " +
                                "alquiler_vehiculos.departamento.Nombre_Departamento, " +
                                "alquiler_vehiculos.lugar_alquiler.Nombre_LugarAlquiler, " +
                                "alquiler_vehiculos.estado_pago_alquiler.Nombre_EstadoPagoAlquiler, " +
                                "alquiler_vehiculos.estado_alquiler.Nombre_EstadoAlquiler, " +
                                "alquiler_vehiculos.seguro_alquiler.Nombre_SeguroAlquiler, " +
                                "alquiler_vehiculos.seguro_alquiler.Precio_SeguroAlquiler, " +
                                "alquiler_vehiculos.alquiler.Precio_Alquiler " +
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
                                "INNER JOIN alquiler_vehiculos.ciudad " +
                                "ON alquiler_vehiculos.ciudad.Id_Ciudad = alquiler_vehiculos.vehiculo.Ciudad_Vehiculo " +
                                "INNER JOIN alquiler_vehiculos.departamento " +
                                "ON alquiler_vehiculos.departamento.Id_Departamento = alquiler_vehiculos.ciudad.Departamento_Ciudad " +
                                "WHERE alquiler_vehiculos.alquiler.Alquilador_Alquiler = " + this.Alquilador.Id + " " +
                                "ORDER BY alquiler_vehiculos.alquiler.FechaIncio_Alquiler DESC ";
        }
        public void GenerarReporteAlquileresAlquiladorPDF()
        {
            HtmlToPdf Convertidor = new();
            PdfDocument DocumentoPDF;
            Convertidor.Options.PdfPageOrientation = PdfPageOrientation.Landscape;
            Convertidor.Options.MarginLeft = 0;
            Convertidor.Options.MarginRight = 0;
            Convertidor.Options.MarginTop = 0;
            Convertidor.Options.MarginBottom = 0;
            string HTMLString = File.ReadAllText("./Templates/ReporteAlquileresAlquilador.html").ToString();
            string FilasTabla = "";
            int Indice = 1;
            float PrecioTotal = 0;
            string RutaImagen = Directory.GetCurrentDirectory();
            RutaImagen = RutaImagen.Replace(@"\", "/");
            RutaImagen += "/wwwroot/imagenes/";
            MySqlConnection ConexionBD = ModeloConexion.Conect();

            try
            {
                ConexionBD.Open();
                MySqlCommand Comando = new(ConsultaSQL, ConexionBD);
                MySqlDataReader Lector = Comando.ExecuteReader();
                if (Lector.HasRows)
                {
                    while (Lector.Read())
                    {
                        if (Indice % 2 == 0)
                        {
                            FilasTabla += "<tr>" +
                                                "<td class='CeldaDatoTabla CeldaImpar'>" + Indice.ToString() + "</td> " +
                                                "<td class='CeldaDatoTabla CeldaImpar'>" + new DateOnly(Lector.GetDateTime(0).Year, Lector.GetDateTime(0).Month, Lector.GetDateTime(0).Day).ToString() + "</td> " +
                                                "<td class='CeldaDatoTabla CeldaImpar'>" + new DateOnly(Lector.GetDateTime(1).Year, Lector.GetDateTime(1).Month, Lector.GetDateTime(1).Day).ToString() + "</td> " +
                                                "<td class='CeldaDatoTabla CeldaImpar'>" + Lector.GetString(2) + " " + Lector.GetString(3) + "<br>" + Lector.GetString(4) + "</td> " +
                                                "<td class='CeldaDatoTabla CeldaImpar'>" + Lector.GetString(5) + "<br>(" + Lector.GetString(6) + ")</td> " +
                                                "<td class='CeldaDatoTabla CeldaImpar'>" + Lector.GetString(7) + "</td> " +
                                                "<td class='CeldaDatoTabla CeldaImpar'>" + Lector.GetString(8) + "</td> " +
                                                "<td class='CeldaDatoTabla CeldaImpar'>" + Lector.GetString(9) + "</td> " +
                                                "<td class='CeldaDatoTabla CeldaImpar'>" + Lector.GetString(10) + "<br>(" + Lector.GetFloat(11).ToString() + ")</td> " +
                                                "<td class='CeldaDatoTabla CeldaImpar'>" + Lector.GetFloat(12).ToString() + "</td>" +
                                            "</tr> ";
                        }
                        else
                        {
                            FilasTabla += "<tr>" +
                                                "<td class='CeldaDatoTabla CeldaPar'>" + Indice.ToString() + "</td> " +
                                                "<td class='CeldaDatoTabla CeldaPar'>" + new DateOnly(Lector.GetDateTime(0).Year, Lector.GetDateTime(0).Month, Lector.GetDateTime(0).Day).ToString() + "</td> " +
                                                "<td class='CeldaDatoTabla CeldaPar'>" + new DateOnly(Lector.GetDateTime(1).Year, Lector.GetDateTime(1).Month, Lector.GetDateTime(1).Day).ToString() + "</td> " +
                                                "<td class='CeldaDatoTabla CeldaPar'>" + Lector.GetString(2) + " " + Lector.GetString(3) + "<br>" + Lector.GetString(4) + "</td> " +
                                                "<td class='CeldaDatoTabla CeldaPar'>" + Lector.GetString(5) + "<br>(" + Lector.GetString(6) + ")</td> " +
                                                "<td class='CeldaDatoTabla CeldaPar'>" + Lector.GetString(7) + "</td> " +
                                                "<td class='CeldaDatoTabla CeldaPar'>" + Lector.GetString(8) + "</td> " +
                                                "<td class='CeldaDatoTabla CeldaPar'>" + Lector.GetString(9) + "</td> " +
                                                "<td class='CeldaDatoTabla CeldaPar'>" + Lector.GetString(10) + "<br>(" + Lector.GetFloat(11).ToString() + ")</td> " +
                                                "<td class='CeldaDatoTabla CeldaPar'>" + Lector.GetFloat(12).ToString() + "</td>" +
                                            "</tr> ";
                        }
                        Indice++;
                        PrecioTotal += Lector.GetFloat(12);
                    }
                }
            }
            catch (Exception) { }
            finally
            {
                ConexionBD.Close();
            }
            HTMLString = HTMLString.Replace("@RAZONSOCIAL", this.Empresa.Nombre);
            HTMLString = HTMLString.Replace("@CIUDAD", this.Empresa.Ciudad);
            HTMLString = HTMLString.Replace("@DIRECCION", this.Empresa.Direccion);
            HTMLString = HTMLString.Replace("@BARRIO", this.Empresa.Barrio);
            HTMLString = HTMLString.Replace("@NIT", this.Empresa.NIT);
            HTMLString = HTMLString.Replace("@TELEFONO", this.Empresa.Telefono);
            HTMLString = HTMLString.Replace("@CORREO", this.Empresa.Correo);
            HTMLString = HTMLString.Replace("@FOTO", RutaImagen + this.Empresa.RutaFoto);
            HTMLString = HTMLString.Replace("@TABLAINFORME", FilasTabla);
            HTMLString = HTMLString.Replace("@TOTAL", PrecioTotal.ToString());
            HTMLString = HTMLString.Replace("@USUARIO", this.Usuario.Nombre + " " + this.Usuario.Apellido);
            HTMLString = HTMLString.Replace("@IDENTIFICACION", this.Usuario.SimboloTipoIdentificacion + " " + this.Usuario.NumeroIdentificacion);
            HTMLString = HTMLString.Replace("@FECHAINFORME", DateTime.Now.ToString("dddd dd-MMMM-yyyy hh:mm:ss tt"));
            DocumentoPDF = Convertidor.ConvertHtmlString(HTMLString);
            DocumentoPDF.Save("./wwwroot/Reportes/ReporteAlquileresAlquilador" + this.Usuario.NumeroIdentificacion + ".pdf");
            DocumentoPDF.Close();
        }
        public void GenerarReporteAlquileresAlquiladorEXCEL()
        {
            string RutaArchivo = "./wwwroot/Reportes/ReporteAlquileresAlquilador" + this.Usuario.NumeroIdentificacion + ".xlsx";
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using ExcelPackage PaqueteExcel = new();
            PaqueteExcel.Workbook.Properties.Author = Usuario.Nombre + " " + Usuario.Apellido;
            PaqueteExcel.Workbook.Properties.Title = "Reporte de Alquileres Realizados";
            ExcelWorksheet HojaCalculo = PaqueteExcel.Workbook.Worksheets.Add("Reporte");
            HojaCalculo.Cells[1, 1].Value = "#";
            HojaCalculo.Cells[1, 2].Value = "FECHA INICIO";
            HojaCalculo.Cells[1, 3].Value = "FECHA FIN";
            HojaCalculo.Cells[1, 4].Value = "VEHÍCULO";
            HojaCalculo.Cells[1, 5].Value = "CIUDAD";
            HojaCalculo.Cells[1, 6].Value = "LUGAR";
            HojaCalculo.Cells[1, 7].Value = "ESTADO DEL PAGO";
            HojaCalculo.Cells[1, 8].Value = "ESTADO ALQUILER";
            HojaCalculo.Cells[1, 9].Value = "SEGURO";
            HojaCalculo.Cells[1, 10].Value = "PRECIO";
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
                        HojaCalculo.Cells[Celda, 4].Value = Lector.GetString(2) + " " + Lector.GetString(3) + " " + Lector.GetString(4);
                        HojaCalculo.Cells[Celda, 5].Value = Lector.GetString(5) + ", " + Lector.GetString(6);
                        HojaCalculo.Cells[Celda, 6].Value = Lector.GetString(7);
                        HojaCalculo.Cells[Celda, 7].Value = Lector.GetString(8);
                        HojaCalculo.Cells[Celda, 8].Value = Lector.GetString(9);
                        HojaCalculo.Cells[Celda, 9].Value = Lector.GetString(10) + " (" + Lector.GetFloat(11).ToString() + ")";
                        HojaCalculo.Cells[Celda, 10].Value = Lector.GetFloat(12);
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
            string ReporteExcel = "./wwwroot/Reportes/ReporteAlquileresAlquilador" + this.Usuario.NumeroIdentificacion + ".xlsx";
            string ReportePDF = "./wwwroot/Reportes/ReporteAlquileresAlquilador" + this.Usuario.NumeroIdentificacion + ".pdf";
            string CorreoEmisor = "mariog.101200@hotmail.com";
            string Asunto = "Reportes de Alquileres Realizados por " + this.Usuario.Nombre + " " + this.Usuario.Apellido;
            string Mensaje = "Adjunto encontrarás los reportes en Excel y PDF";
            SmtpClient ClienteAMTP = new("smtp-mail.outlook.com", 587)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential(CorreoEmisor, "M@rio112358")
            };
            MailMessage MensajeCorreo = new(CorreoEmisor, CorreoReceptor, Asunto, Mensaje);
            Attachment ReporteAdjuntoExcel = new(ReporteExcel);
            MensajeCorreo.Attachments.Add(ReporteAdjuntoExcel);
            Attachment ReporteAdjuntoPDF = new(ReportePDF);
            MensajeCorreo.Attachments.Add(ReporteAdjuntoPDF);
            ClienteAMTP.Send(MensajeCorreo);
        }
    }
}
/*

*/