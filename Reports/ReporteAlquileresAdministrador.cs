using ALQUILER_VEHICULOS.Models;
using MySql.Data.MySqlClient;
using OfficeOpenXml.Style;
using OfficeOpenXml;
using System.Net.Mail;
using System.Net;
using SelectPdf;

namespace ALQUILER_VEHICULOS.Reports
{
    public class ReporteAlquileresAdministrador
    {
        public ModeloUsuario Usuario { get; set; }
        public ModeloEmpresa Empresa { get; set; }
        public string ConsultaSQL { get; set; }
        public ReporteAlquileresAdministrador(int IdUsuario)
        {
            ModeloUsuario ModeloUsuario = new();
            this.Usuario = ModeloUsuario.TraerUsuario(IdUsuario);
            this.Empresa = new();
            this.ConsultaSQL = "SELECT " +
                                "alquiler_vehiculos.alquiler.FechaIncio_Alquiler, " +
                                "alquiler_vehiculos.alquiler.FechaFin_Alquiler, " +
                                "alquiler_vehiculos.upropietario.Nombre_Usuario, " +
                                "alquiler_vehiculos.upropietario.Apellido_Usuario, " +
                                "alquiler_vehiculos.tipo_vehiculo.Nombre_TipoVehiculo, " +
                                "alquiler_vehiculos.marca_vehiculo.Nombre_MarcaVehiculo, " +
                                "alquiler_vehiculos.linea_vehiculo.Nombre_LineaVehiculo, " +
                                "alquiler_vehiculos.ciudad.Nombre_Ciudad, " +
                                "alquiler_vehiculos.departamento.Nombre_Departamento, " +
                                "alquiler_vehiculos.lugar_alquiler.Nombre_LugarAlquiler, " +
                                "alquiler_vehiculos.ualquilador.Nombre_Usuario, " +
                                "alquiler_vehiculos.ualquilador.Apellido_Usuario, " +
                                "alquiler_vehiculos.alquiler.Precio_Alquiler, " +
                                "alquiler_vehiculos.alquiler.Ganancias_Alquiler " +
                                "FROM alquiler_vehiculos.alquiler " +
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
                                "INNER JOIN alquiler_vehiculos.lugar_alquiler " +
                                "ON alquiler_vehiculos.lugar_alquiler.Id_LugarAlquiler = alquiler_vehiculos.alquiler.Lugar_Alquiler " +
                                "INNER JOIN alquiler_vehiculos.alquilador " +
                                "ON alquiler_vehiculos.alquilador.Id_Alquilador = alquiler_vehiculos.alquiler.Alquilador_Alquiler " +
                                "INNER JOIN alquiler_vehiculos.usuario AS ualquilador " +
                                "ON alquiler_vehiculos.ualquilador.Id_Usuario = alquiler_vehiculos.alquilador.Usuario_Alquilador " +
                                "INNER JOIN alquiler_vehiculos.propietario " +
                                "ON alquiler_vehiculos.propietario.Id_Propietario = alquiler_vehiculos.vehiculo.Propietario_Vehiculo " +
                                "INNER JOIN alquiler_vehiculos.usuario as upropietario " +
                                "ON alquiler_vehiculos.upropietario.Id_Usuario = alquiler_vehiculos.propietario.Usuario_Propietario " +
                                "ORDER BY alquiler_vehiculos.alquiler.FechaIncio_Alquiler DESC";
        }
        public void GenerarReporteAlquileresAdministradorPDF()
        {
            string FilasTabla;
            int Indice = 0;
            float PrecioTotal = 0;
            float ComisionesPlataforma = 0;
            string RutaImagen = Directory.GetCurrentDirectory() + "/wwwroot/imagenes/";
            string HTMLString = File.ReadAllText("./Templates/ReporteAlquileresAdministrador.html").ToString();
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
                        if (Indice % 8 == 0)
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
                                                "<td class='CeldaDatoTabla CeldaImpar'>" + Lector.GetString(2) + "<br>" + Lector.GetString(3) + "</td> " +
                                                "<td class='CeldaDatoTabla CeldaImpar'>" + Lector.GetString(4) + " " + Lector.GetString(5) + "<br>" + Lector.GetString(6) + "</td> " +
                                                "<td class='CeldaDatoTabla CeldaImpar'>" + Lector.GetString(7) + "<br>(" + Lector.GetString(8) + ")</td> " +
                                                "<td class='CeldaDatoTabla CeldaImpar'>" + Lector.GetString(9) +
                                                "<td class='CeldaDatoTabla CeldaImpar'>" + Lector.GetString(10) + "<br>" + Lector.GetString(11) + "</td> " +
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
                                                "<td class='CeldaDatoTabla CeldaPar'>" + Lector.GetString(2) + "<br>" + Lector.GetString(3) + "</td> " +
                                                "<td class='CeldaDatoTabla CeldaPar'>" + Lector.GetString(4) + " " + Lector.GetString(5) + "<br>" + Lector.GetString(6) + "</td> " +
                                                "<td class='CeldaDatoTabla CeldaPar'>" + Lector.GetString(7) + "<br>(" + Lector.GetString(8) + ")</td> " +
                                                "<td class='CeldaDatoTabla CeldaPar'>" + Lector.GetString(9) +
                                                "<td class='CeldaDatoTabla CeldaPar'>" + Lector.GetString(10) + "<br>" + Lector.GetString(11) + "</td> " +
                                                "<td class='CeldaDatoTabla CeldaPar'>" + Lector.GetFloat(12).ToString() + "</td>" +
                                                "<td class='CeldaDatoTabla CeldaPar'>" + Lector.GetFloat(13).ToString() + "</td>" +
                                            "</tr> ");
                        }
                        PrecioTotal += Lector.GetFloat(12);
                        ComisionesPlataforma += Lector.GetFloat(13);
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
                        "<td colspan='8' class='FilaTotal TituloTotal'>Totales</td>" +
                        "<td class='FilaTotal'>" + PrecioTotal.ToString() + "</td>" +
                        "<td class='FilaTotal'>" + ComisionesPlataforma.ToString() + "</td>" +
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
            DocumentoFinal.Save("./wwwroot/Reportes/ReporteAlquileresAdministrador" + this.Usuario.NumeroIdentificacion + ".pdf");
        }
        public void GenerarReporteAlquileresAdministradorEXCEL()
        {
            string RutaArchivo = "./wwwroot/Reportes/ReporteAlquileresAdministrador" + this.Usuario.NumeroIdentificacion + ".xlsx";
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using ExcelPackage PaqueteExcel = new();
            PaqueteExcel.Workbook.Properties.Author = Usuario.Nombre + " " + Usuario.Apellido;
            PaqueteExcel.Workbook.Properties.Title = "Reporte de Alquileres Realizados";
            ExcelWorksheet HojaCalculo = PaqueteExcel.Workbook.Worksheets.Add("Reporte");
            HojaCalculo.Cells[1, 1].Value = "#";
            HojaCalculo.Cells[1, 2].Value = "FECHA INICIO";
            HojaCalculo.Cells[1, 3].Value = "FECHA FIN";
            HojaCalculo.Cells[1, 4].Value = "PROPIETARIO";
            HojaCalculo.Cells[1, 5].Value = "VEHÍCULO";
            HojaCalculo.Cells[1, 6].Value = "CIUDAD";
            HojaCalculo.Cells[1, 7].Value = "LUGAR";
            HojaCalculo.Cells[1, 8].Value = "ALQUILADOR";
            HojaCalculo.Cells[1, 9].Value = "PRECIO";
            HojaCalculo.Cells[1, 10].Value = "GANANCIAS";
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
                        HojaCalculo.Cells[Celda, 4].Value = Lector.GetString(2) + " " + Lector.GetString(3);
                        HojaCalculo.Cells[Celda, 5].Value = Lector.GetString(4) + " " + Lector.GetString(5) + " " + Lector.GetString(6);
                        HojaCalculo.Cells[Celda, 6].Value = Lector.GetString(7) + " (" + Lector.GetString(8) + ")";
                        HojaCalculo.Cells[Celda, 7].Value = Lector.GetString(9);
                        HojaCalculo.Cells[Celda, 8].Value = Lector.GetString(10) + " " + Lector.GetString(11);
                        HojaCalculo.Cells[Celda, 9].Value = Lector.GetFloat(12);
                        HojaCalculo.Cells[Celda, 10].Value = Lector.GetFloat(13);
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
        public void EnviarReportesAlquileresAdministradorPorCorreo(string CorreoReceptor)
        {
            GenerarReporteAlquileresAdministradorEXCEL();
            GenerarReporteAlquileresAdministradorPDF();
            string ReporteExcel = "./wwwroot/Reportes/ReporteAlquileresAdministrador" + this.Usuario.NumeroIdentificacion + ".xlsx";
            string ReportePDF = "./wwwroot/Reportes/ReporteAlquileresAdministrador" + this.Usuario.NumeroIdentificacion + ".pdf";
            string CorreoEmisor = "mariog.101200@hotmail.com";
            string Asunto = "Reportes Todos los Alquileres";
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