using ALQUILER_VEHICULOS.Models;
using MySql.Data.MySqlClient;
using OfficeOpenXml.Style;
using OfficeOpenXml;
using System.Net.Mail;
using System.Net;
using SelectPdf;

namespace ALQUILER_VEHICULOS.Reports
{
    public class ReporteAlquileresPropietario
    {
        public ModeloUsuario Usuario { get; set; }
        public ModeloPropietario Propietario { get; set; }
        public ModeloEmpresa Empresa { get; set; }
        public string ConsultaSQL { get; set; }
        public ReporteAlquileresPropietario(int IdUsuario)
        {
            ModeloUsuario ModeloUsuario = new();
            ModeloPropietario ModeloPropietario = new();
            this.Usuario = ModeloUsuario.TraerUsuario(IdUsuario);
            this.Propietario = ModeloPropietario.TraerPropietarioUsuario(IdUsuario);
            this.Empresa = new();
            this.ConsultaSQL = "SELECT " +
                                "alquiler_vehiculos.alquiler.FechaIncio_Alquiler, " +
                                "alquiler_vehiculos.alquiler.FechaFin_Alquiler, " +
                                "alquiler_vehiculos.tipo_vehiculo.Nombre_TipoVehiculo, " +
                                "alquiler_vehiculos.marca_vehiculo.Nombre_MarcaVehiculo, " +
                                "alquiler_vehiculos.linea_vehiculo.Nombre_LineaVehiculo, " +
                                "alquiler_vehiculos.ciudad.Nombre_Ciudad, " +
                                "alquiler_vehiculos.departamento.Nombre_Departamento, " +
                                "alquiler_vehiculos.usuario.Nombre_Usuario, " +
                                "alquiler_vehiculos.usuario.Apellido_Usuario, " +
                                "alquiler_vehiculos.estado_alquiler.Nombre_EstadoAlquiler, " +
                                "alquiler_vehiculos.estado_pago_alquiler.Nombre_EstadoPagoAlquiler, " +
                                "alquiler_vehiculos.alquiler.Precio_Alquiler, " +
                                "alquiler_vehiculos.alquiler.Ganancias_Alquiler, " +
                                "alquiler_vehiculos.alquiler.Calificacion_Alquiler " +
                                "FROM alquiler_vehiculos.alquiler " +
                                "INNER JOIN alquiler_vehiculos.estado_alquiler " +
                                "ON alquiler_vehiculos.estado_alquiler.Id_EstadoAlquiler = alquiler_vehiculos.alquiler.Estado_Alquiler " +
                                "INNER JOIN alquiler_vehiculos.estado_pago_alquiler " +
                                "ON alquiler_vehiculos.estado_pago_alquiler.Id_EstadoPagoAlquiler = alquiler_vehiculos.alquiler.EstadoPago_Alquiler " +
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
                                "INNER JOIN alquiler_vehiculos.alquilador " +
                                "ON alquiler_vehiculos.alquilador.Id_Alquilador = alquiler_vehiculos.alquiler.Alquilador_Alquiler " +
                                "INNER JOIN alquiler_vehiculos.usuario " +
                                "ON alquiler_vehiculos.usuario.Id_Usuario = alquiler_vehiculos.alquilador.Usuario_Alquilador " +
                                "INNER JOIN alquiler_vehiculos.propietario " +
                                "ON alquiler_vehiculos.propietario.Id_Propietario = alquiler_vehiculos.vehiculo.Propietario_Vehiculo " +
                                "WHERE alquiler_vehiculos.propietario.Id_Propietario = " + this.Propietario.Id + " " +
                                "ORDER BY alquiler_vehiculos.alquiler.FechaIncio_Alquiler DESC ";
        }
        public void GenerarReporteAlquileresPropietarioPDF()
        {
            string FilasTabla;
            int Indice = 0;
            float PrecioTotal = 0;
            float ComisionesPlataforma = 0;
            int ContadorRegistros = 0;
            float Calificaciones = 0;
            string RutaImagen = Directory.GetCurrentDirectory() + "/wwwroot/imagenes/";
            string HTMLString = File.ReadAllText("./Templates/ReporteAlquileresPropietario.html").ToString();
            HtmlToPdf Convertidor = new();
            PdfDocument DocumentoFinal = new();
            Convertidor.Options.PdfPageOrientation = PdfPageOrientation.Landscape;
            Convertidor.Options.MarginLeft = 0;
            Convertidor.Options.MarginRight = 0;
            Convertidor.Options.MarginTop = 0;
            Convertidor.Options.MarginBottom = 0;
            List<List<string>> ListaListas = [];
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
                        ContadorRegistros++;
                        if (Indice % 7 == 0)
                        {
                            ListaListas.Add([]);
                        }
                        Indice++;
                        if (Indice % 2 != 0)
                        {
                            ListaListas.Last().Add(
                                            "<tr>" +
                                                "<td class='CeldaDatoTabla CeldaImpar'>" + Indice.ToString() + "</td> " +
                                                "<td class='CeldaDatoTabla CeldaImpar'>" + new DateOnly(Lector.GetDateTime(0).Year, Lector.GetDateTime(0).Month, Lector.GetDateTime(0).Day).ToString() + "</td> " +
                                                "<td class='CeldaDatoTabla CeldaImpar'>" + new DateOnly(Lector.GetDateTime(1).Year, Lector.GetDateTime(1).Month, Lector.GetDateTime(1).Day).ToString() + "</td> " +
                                                "<td class='CeldaDatoTabla CeldaImpar'>" + Lector.GetString(2) + " " + Lector.GetString(3) + "<br>" + Lector.GetString(4) + "</td> " +
                                                "<td class='CeldaDatoTabla CeldaImpar'>" + Lector.GetString(5) + "<br>(" + Lector.GetString(6) + ")</td> " +
                                                "<td class='CeldaDatoTabla CeldaImpar'>" + Lector.GetString(7) + "<br>" + Lector.GetString(8) + "</td> " +
                                                "<td class='CeldaDatoTabla CeldaImpar'>" + Lector.GetString(9) + "</td> " +
                                                "<td class='CeldaDatoTabla CeldaImpar'>" + Lector.GetString(10) + "</td> " +
                                                "<td class='CeldaDatoTabla CeldaImpar'>" + Lector.GetFloat(11).ToString() + "</td> " +
                                                "<td class='CeldaDatoTabla CeldaImpar'>" + Lector.GetFloat(12).ToString() + "</td>" +
                                                "<td class='CeldaDatoTabla CeldaImpar'>" + Lector.GetFloat(12).ToString() + "</td>" +
                                                "<td class='CeldaDatoTabla CeldaImpar'>" + Lector.GetFloat(13).ToString() + "</td>" +
                                            "</tr> ");
                        }
                        else
                        {
                            ListaListas.Last().Add(
                                            "<tr>" +
                                                "<td class='CeldaDatoTabla CeldaPar'>" + Indice.ToString() + "</td> " +
                                                "<td class='CeldaDatoTabla CeldaPar'>" + new DateOnly(Lector.GetDateTime(0).Year, Lector.GetDateTime(0).Month, Lector.GetDateTime(0).Day).ToString() + "</td> " +
                                                "<td class='CeldaDatoTabla CeldaPar'>" + new DateOnly(Lector.GetDateTime(1).Year, Lector.GetDateTime(1).Month, Lector.GetDateTime(1).Day).ToString() + "</td> " +
                                                "<td class='CeldaDatoTabla CeldaPar'>" + Lector.GetString(2) + " " + Lector.GetString(3) + "<br>" + Lector.GetString(4) + "</td> " +
                                                "<td class='CeldaDatoTabla CeldaPar'>" + Lector.GetString(5) + "<br>(" + Lector.GetString(6) + ")</td> " +
                                                "<td class='CeldaDatoTabla CeldaPar'>" + Lector.GetString(7) + "<br>" + Lector.GetString(8) + "</td> " +
                                                "<td class='CeldaDatoTabla CeldaPar'>" + Lector.GetString(9) + "</td> " +
                                                "<td class='CeldaDatoTabla CeldaPar'>" + Lector.GetString(10) + "</td> " +
                                                "<td class='CeldaDatoTabla CeldaPar'>" + Lector.GetFloat(11).ToString() + "</td> " +
                                                "<td class='CeldaDatoTabla CeldaPar'>" + Lector.GetFloat(12).ToString() + "</td>" +
                                                "<td class='CeldaDatoTabla CeldaPar'>" + (Lector.GetFloat(11) - Lector.GetFloat(12)).ToString() + "</td>" +
                                                "<td class='CeldaDatoTabla CeldaPar'>" + Math.Round(Lector.GetFloat(13), 1).ToString() + "</td>" +
                                            "</tr> ");
                        }
                        PrecioTotal += Lector.GetFloat(11);
                        ComisionesPlataforma += Lector.GetFloat(12);
                        Calificaciones += Lector.GetFloat(13);
                    }
                }
            }
            catch (Exception) { }
            finally
            {
                ConexionBD.Close();
            }
            int IndicePagina = 0;
            foreach (var Lista in ListaListas)
            {
                IndicePagina++;
                FilasTabla = "";
                foreach (var ListaAux in Lista)
                {
                    FilasTabla += ListaAux;
                }
                string CopiaHTMLString = HTMLString;
                if (Lista == ListaListas.Last())
                {
                    CopiaHTMLString = CopiaHTMLString.Replace("@FILATOTAL", "<tr>" +
                        "<td colspan='8' class='FilaTotal TituloTotal'>Totales y Promedio de Calificación</td>" +
                        "<td class='FilaTotal'>" + PrecioTotal.ToString() + "</td>" +
                        "<td class='FilaTotal'>" + ComisionesPlataforma.ToString() + "</td>" +
                        "<td class='FilaTotal'>" + (PrecioTotal - ComisionesPlataforma).ToString() + "</td>" +
                        "<td class='FilaTotal'>" + Math.Round(Calificaciones / ContadorRegistros, 1).ToString() + "</td>" +
                        "</tr>");
                }
                else
                {
                    CopiaHTMLString = CopiaHTMLString.Replace("@FILATOTAL", "");
                }
                CopiaHTMLString = CopiaHTMLString.Replace("@USUARIO", this.Usuario.Nombre + " " + this.Usuario.Apellido);
                CopiaHTMLString = CopiaHTMLString.Replace("@IDENTIFICACION", this.Usuario.SimboloTipoIdentificacion + " " + this.Usuario.NumeroIdentificacion);
                CopiaHTMLString = CopiaHTMLString.Replace("@FECHAINFORME", DateTime.Now.ToString("dddd dd-MMMM-yyyy hh:mm:ss tt"));
                CopiaHTMLString = CopiaHTMLString.Replace("@RAZONSOCIAL", this.Empresa.Nombre);
                CopiaHTMLString = CopiaHTMLString.Replace("@CIUDAD", this.Empresa.Ciudad);
                CopiaHTMLString = CopiaHTMLString.Replace("@DIRECCION", this.Empresa.Direccion);
                CopiaHTMLString = CopiaHTMLString.Replace("@BARRIO", this.Empresa.Barrio);
                CopiaHTMLString = CopiaHTMLString.Replace("@NIT", this.Empresa.NIT);
                CopiaHTMLString = CopiaHTMLString.Replace("@TELEFONO", this.Empresa.Telefono);
                CopiaHTMLString = CopiaHTMLString.Replace("@CORREO", this.Empresa.Correo);
                CopiaHTMLString = CopiaHTMLString.Replace("@FOTO", RutaImagen + this.Empresa.RutaFoto);
                CopiaHTMLString = CopiaHTMLString.Replace("@TABLAINFORME", FilasTabla);
                CopiaHTMLString = CopiaHTMLString.Replace("@PAGINAS", "PAGINA " + IndicePagina + " DE " + ListaListas.Count);
                PdfDocument DocumentoPDFAux = Convertidor.ConvertHtmlString(CopiaHTMLString);
                DocumentoFinal.Append(DocumentoPDFAux);
            }
            DocumentoFinal.Save("./wwwroot/Reportes/ReporteAlquileresPropietario" + this.Usuario.NumeroIdentificacion + ".pdf");
        }
        public void GenerarReporteAlquileresPropietarioEXCEL()
        {
            string RutaArchivo = "./wwwroot/Reportes/ReporteAlquileresPropietario" + this.Usuario.NumeroIdentificacion + ".xlsx";
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
            HojaCalculo.Cells[1, 6].Value = "ALQUILADOR";
            HojaCalculo.Cells[1, 7].Value = "ESTADO ALQUILER";
            HojaCalculo.Cells[1, 8].Value = "ESTADO DEL PAGO";
            HojaCalculo.Cells[1, 9].Value = "VALOR";
            HojaCalculo.Cells[1, 10].Value = "COMISIÓN PLATAFORMA";
            HojaCalculo.Cells[1, 11].Value = "GANANCIAS";
            HojaCalculo.Cells[1, 12].Value = "CALIFICACIÓN";
            using (var Encabezados = HojaCalculo.Cells[1, 1, 1, 12])
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
                        HojaCalculo.Cells[Celda, 6].Value = Lector.GetString(7) + " " + Lector.GetString(8);
                        HojaCalculo.Cells[Celda, 7].Value = Lector.GetString(9);
                        HojaCalculo.Cells[Celda, 8].Value = Lector.GetString(10);
                        HojaCalculo.Cells[Celda, 9].Value = Lector.GetFloat(11);
                        HojaCalculo.Cells[Celda, 10].Value = Lector.GetFloat(12);
                        HojaCalculo.Cells[Celda, 11].Value = Lector.GetFloat(11) - Lector.GetFloat(12);
                        HojaCalculo.Cells[Celda, 12].Value = Math.Round(Lector.GetFloat(13), 1);
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
        public void EnviarReportesAlquileresPropietarioPorCorreo(string CorreoReceptor)
        {
            GenerarReporteAlquileresPropietarioEXCEL();
            GenerarReporteAlquileresPropietarioPDF();
            string ReporteExcel = "./wwwroot/Reportes/ReporteAlquileresPropietario" + this.Usuario.NumeroIdentificacion + ".xlsx";
            string ReportePDF = "./wwwroot/Reportes/ReporteAlquileresPropietario" + this.Usuario.NumeroIdentificacion + ".pdf";
            string CorreoEmisor = "mariog.101200@hotmail.com";
            string Asunto = "Reportes de Alquileres Ofrecidos por " + this.Usuario.Nombre + " " + this.Usuario.Apellido;
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