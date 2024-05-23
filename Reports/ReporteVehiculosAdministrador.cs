using ALQUILER_VEHICULOS.Models;
using MySql.Data.MySqlClient;
using OfficeOpenXml.Style;
using OfficeOpenXml;
using System.Net.Mail;
using System.Net;
using SelectPdf;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
namespace ALQUILER_VEHICULOS.Reports
{
    public class ReporteVehiculosAdministrador
    {
        public ModeloUsuario Usuario { get; set; }
        public ModeloEmpresa Empresa { get; set; }
        public string ConsultaSQL { get; set; }
        public ReporteVehiculosAdministrador(int IdUsuario)
        {
            ModeloUsuario ModeloUsuario = new();
            this.Usuario = ModeloUsuario.TraerUsuario(IdUsuario);
            this.Empresa = new();
            this.ConsultaSQL = "SELECT " +
                "alquiler_vehiculos.vehiculo.Placa_Vehiculo, " +
                "alquiler_vehiculos.tipo_vehiculo.Nombre_TipoVehiculo, " +
                "alquiler_vehiculos.marca_vehiculo.Nombre_MarcaVehiculo, " +
                "alquiler_vehiculos.linea_vehiculo.Nombre_LineaVehiculo, " +
                "alquiler_vehiculos.modelo.Valor_Modelo, " +
                "alquiler_vehiculos.cilindrada.Valor_Cilindrada, " +
                "alquiler_vehiculos.cantidad_pasajeros.Valor_CantidadPasajeros, " +
                "alquiler_vehiculos.vehiculo.PrecioAlquilerDia_Vehiculo, " +
                "alquiler_vehiculos.usuario.Nombre_Usuario, " +
                "alquiler_vehiculos.usuario.Apellido_Usuario, " +
                "alquiler_vehiculos.ciudad.Nombre_Ciudad, " +
                "alquiler_vehiculos.departamento.Nombre_Departamento, " +
                "alquiler_vehiculos.estado_vehiculo.Nombre_EstadoVehiculo, " +
                "COALESCE(COUNT(alquiler_vehiculos.alquiler.Id_Alquiler), 0), " +
                "COALESCE(SUM(alquiler_vehiculos.alquiler.Precio_Alquiler), 0), " +
                "COALESCE(SUM(alquiler_vehiculos.alquiler.Ganancias_Alquiler), 0) " +
                "FROM alquiler_vehiculos.vehiculo " +
                "LEFT JOIN alquiler_vehiculos.alquiler " +
                "ON alquiler_vehiculos.alquiler.Vehiculo_Alquiler = alquiler_vehiculos.vehiculo.Id_Vehiculo " +
                "INNER JOIN alquiler_vehiculos.estado_vehiculo " +
                "ON alquiler_vehiculos.estado_vehiculo.Id_EstadoVehiculo = alquiler_vehiculos.vehiculo.Estado_Vehiculo " +
                "INNER JOIN alquiler_vehiculos.cilindrada " +
                "ON alquiler_vehiculos.cilindrada.Id_Cilindrada = alquiler_vehiculos.vehiculo.Cilindrada_Vehiculo " +
                "INNER JOIN alquiler_vehiculos.cantidad_pasajeros " +
                "ON alquiler_vehiculos.cantidad_pasajeros.Id_CantidadPasajeros = alquiler_vehiculos.vehiculo.CantidadPasajeros_Vehiculo " +
                "INNER JOIN alquiler_vehiculos.modelo " +
                "ON alquiler_vehiculos.modelo.Id_Modelo = alquiler_vehiculos.vehiculo.Modelo_Vehiculo " +
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
                "INNER JOIN alquiler_vehiculos.propietario " +
                "ON alquiler_vehiculos.propietario.Id_Propietario = alquiler_vehiculos.vehiculo.Propietario_Vehiculo " +
                "INNER JOIN alquiler_vehiculos.usuario " +
                "ON alquiler_vehiculos.usuario.Id_Usuario = alquiler_vehiculos.propietario.Id_Propietario " +
                "GROUP BY " +
                "alquiler_vehiculos.tipo_vehiculo.Nombre_TipoVehiculo, " +
                "alquiler_vehiculos.marca_vehiculo.Nombre_MarcaVehiculo, " +
                "alquiler_vehiculos.linea_vehiculo.Nombre_LineaVehiculo, " +
                "alquiler_vehiculos.vehiculo.Placa_Vehiculo, " +
                "alquiler_vehiculos.modelo.Valor_Modelo, " +
                "alquiler_vehiculos.cilindrada.Valor_Cilindrada, " +
                "alquiler_vehiculos.cantidad_pasajeros.Valor_CantidadPasajeros, " +
                "alquiler_vehiculos.vehiculo.PrecioAlquilerDia_Vehiculo, " +
                "alquiler_vehiculos.usuario.Nombre_Usuario, " +
                "alquiler_vehiculos.usuario.Apellido_Usuario, " +
                "alquiler_vehiculos.ciudad.Nombre_Ciudad, " +
                "alquiler_vehiculos.departamento.Nombre_Departamento, " +
                "alquiler_vehiculos.estado_vehiculo.Nombre_EstadoVehiculo " +
                "ORDER BY alquiler_vehiculos.vehiculo.Placa_Vehiculo ASC";
        }
        public void GenerarReporteVehiculosAdministradorPDF()
        {
            string FilasTabla;
            int Indice = 0;
            int Alquileres = 0;
            float Precios = 0;
            float Ganancias = 0;
            string RutaImagen = Directory.GetCurrentDirectory() + "/wwwroot/imagenes/";
            string HTMLString = File.ReadAllText("./Templates/ReporteVehiculosAdministrador.html").ToString();
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
                        if (Indice % 5 == 0)
                        {
                            ListaListas.Add([]);
                        }
                        Indice++;
                        if (Indice % 2 != 0)
                        {
                            ListaListas.Last().Add(
                                            "<tr>" +
                                                "<td class='CeldaDatoTabla CeldaImpar'>" + Indice.ToString() + "</td> " +
                                                "<td class='CeldaDatoTabla CeldaImpar'>" + Lector.GetString(0) + "</td> " +
                                                "<td class='CeldaDatoTabla CeldaImpar'>" + Lector.GetString(1) + "<br>" + Lector.GetString(2) + "<br>" + Lector.GetString(3) + "</td> " +
                                                "<td class='CeldaDatoTabla CeldaImpar'>" + Lector.GetInt32(4).ToString() + "</td> " +
                                                "<td class='CeldaDatoTabla CeldaImpar'>" + Lector.GetInt32(5).ToString() + "</td> " +
                                                "<td class='CeldaDatoTabla CeldaImpar'>" + Lector.GetInt32(6).ToString() + "</td> " +
                                                "<td class='CeldaDatoTabla CeldaImpar'>" + Lector.GetFloat(7).ToString() + "</td> " +
                                                "<td class='CeldaDatoTabla CeldaImpar'>" + Lector.GetString(8) + "<br>" + Lector.GetString(9) + "</td> " +
                                                "<td class='CeldaDatoTabla CeldaImpar'>" + Lector.GetString(10) + "<br>(" + Lector.GetString(11) + ")</td> " +
                                                "<td class='CeldaDatoTabla CeldaImpar'>" + Lector.GetString(12) + "</td>" +
                                                "<td class='CeldaDatoTabla CeldaImpar'>" + Lector.GetInt32(13).ToString() + "</td>" +
                                                "<td class='CeldaDatoTabla CeldaImpar'>" + Lector.GetFloat(14).ToString() + "</td>" +
                                                "<td class='CeldaDatoTabla CeldaImpar'>" + Lector.GetFloat(15).ToString() + "</td>" +
                                            "</tr>");
                        }
                        else
                        {
                            ListaListas.Last().Add(
                                            "<tr>" +
                                                "<td class='CeldaDatoTabla CeldaPar'>" + Indice.ToString() + "</td> " +
                                                "<td class='CeldaDatoTabla CeldaPar'>" + Lector.GetString(0) + "</td> " +
                                                "<td class='CeldaDatoTabla CeldaPar'>" + Lector.GetString(1) + "<br>" + Lector.GetString(2) + "<br>" + Lector.GetString(3) + "</td> " +
                                                "<td class='CeldaDatoTabla CeldaPar'>" + Lector.GetInt32(4).ToString() + "</td> " +
                                                "<td class='CeldaDatoTabla CeldaPar'>" + Lector.GetInt32(5).ToString() + "</td> " +
                                                "<td class='CeldaDatoTabla CeldaPar'>" + Lector.GetInt32(6).ToString() + "</td> " +
                                                "<td class='CeldaDatoTabla CeldaPar'>" + Lector.GetFloat(7).ToString() + "</td> " +
                                                "<td class='CeldaDatoTabla CeldaPar'>" + Lector.GetString(8) + "<br>" + Lector.GetString(9) + "</td> " +
                                                "<td class='CeldaDatoTabla CeldaPar'>" + Lector.GetString(10) + "<br>(" + Lector.GetString(11) + ")</td> " +
                                                "<td class='CeldaDatoTabla CeldaPar'>" + Lector.GetString(12) + "</td>" +
                                                "<td class='CeldaDatoTabla CeldaPar'>" + Lector.GetInt32(13).ToString() + "</td>" +
                                                "<td class='CeldaDatoTabla CeldaPar'>" + Lector.GetFloat(14).ToString() + "</td>" +
                                                "<td class='CeldaDatoTabla CeldaPar'>" + Lector.GetFloat(15).ToString() + "</td>" +
                                            "</tr>");
                        }
                        Alquileres += Lector.GetInt32(13);
                        Precios += Lector.GetFloat(14);
                        Ganancias += Lector.GetFloat(15);
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
                        "<td colspan='10' class='FilaTotal TituloTotal'>Totales</td>" +
                        "<td class='FilaTotal'>" + Alquileres.ToString() + "</td>" +
                        "<td class='FilaTotal'>" + Precios.ToString() + "</td>" +
                        "<td class='FilaTotal'>" + Ganancias.ToString() + "</td>" +
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
            DocumentoFinal.Save("./wwwroot/Reportes/ReporteVehiculosAdministrador" + this.Usuario.NumeroIdentificacion + ".pdf");
        }
        public void GenerarReporteVehiculosAdministradorEXCEL()
        {
            string RutaArchivo = "./wwwroot/Reportes/ReporteVehiculosAdministrador" + this.Usuario.NumeroIdentificacion + ".xlsx";
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using ExcelPackage PaqueteExcel = new();
            PaqueteExcel.Workbook.Properties.Author = Usuario.Nombre + " " + Usuario.Apellido;
            PaqueteExcel.Workbook.Properties.Title = "Reporte de Alquileres Realizados";
            ExcelWorksheet HojaCalculo = PaqueteExcel.Workbook.Worksheets.Add("Reporte");
            HojaCalculo.Cells[1, 1].Value = "#";
            HojaCalculo.Cells[1, 2].Value = "PLACA";
            HojaCalculo.Cells[1, 3].Value = "VEHÍCULO";
            HojaCalculo.Cells[1, 4].Value = "MODELO";
            HojaCalculo.Cells[1, 5].Value = "CILINDRDA";
            HojaCalculo.Cells[1, 6].Value = "CANTIDAD DE PASAJEROS";
            HojaCalculo.Cells[1, 7].Value = "PRECIO DE ALQUILER";
            HojaCalculo.Cells[1, 8].Value = "PROPIETARIO";
            HojaCalculo.Cells[1, 9].Value = "CIUDAD";
            HojaCalculo.Cells[1, 10].Value = "ESTADO";
            HojaCalculo.Cells[1, 11].Value = "ALQUILERES REALIZADOS";
            HojaCalculo.Cells[1, 12].Value = "PRECIO ALQUILERES";
            HojaCalculo.Cells[1, 13].Value = "GANANCIAS NETAS";
            using (var Encabezados = HojaCalculo.Cells[1, 1, 1, 13])
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
                        HojaCalculo.Cells[Celda, 2].Value = Lector.GetString(0);
                        HojaCalculo.Cells[Celda, 3].Value = Lector.GetString(1) + " " + Lector.GetString(2) + " " + Lector.GetString(3);
                        HojaCalculo.Cells[Celda, 4].Value = Lector.GetInt32(4);
                        HojaCalculo.Cells[Celda, 5].Value = Lector.GetInt32(5);
                        HojaCalculo.Cells[Celda, 6].Value = Lector.GetInt32(6);
                        HojaCalculo.Cells[Celda, 7].Value = Lector.GetFloat(7);
                        HojaCalculo.Cells[Celda, 8].Value = Lector.GetString(8) + " " + Lector.GetString(9);
                        HojaCalculo.Cells[Celda, 9].Value = Lector.GetString(10) + "(" + Lector.GetString(11) + ")";
                        HojaCalculo.Cells[Celda, 10].Value = Lector.GetString(12);
                        HojaCalculo.Cells[Celda, 11].Value = Lector.GetInt32(13);
                        HojaCalculo.Cells[Celda, 12].Value = Lector.GetFloat(14);
                        HojaCalculo.Cells[Celda, 13].Value = Lector.GetFloat(15);
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
        public void EnviarReportesVehiculosAdministradorPorCorreo(string CorreoReceptor)
        {
            try
            {
                GenerarReporteVehiculosAdministradorEXCEL();
                GenerarReporteVehiculosAdministradorPDF();
                string ReporteExcel = "./wwwroot/Reportes/ReporteVehiculosAdministrador" + this.Usuario.NumeroIdentificacion + ".xlsx";
                string ReportePDF = "./wwwroot/Reportes/ReporteVehiculosAdministrador" + this.Usuario.NumeroIdentificacion + ".pdf";
                string Asunto = "Reportes de todos los vehículos";
                string Mensaje = "Adjunto encontrarás los reportes en Excel y PDF";
                string CorreoEmisor = "mj.rentalseasy@gmail.com";
                string Contrasena = "wlli eqfn quyb opyb";
                SmtpClient SMTP = new("SMTP.gmail.com", 587)
                {
                    EnableSsl = true
                };
                MailMessage MensajeCorreo = new(CorreoEmisor, CorreoReceptor, Asunto.ToUpper(), Mensaje.ToUpper());
                SMTP.Credentials = new NetworkCredential(CorreoEmisor, Contrasena);
                ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors) { return true; };
                Attachment ReporteAdjuntoExcel = new(ReporteExcel);
                MensajeCorreo.Attachments.Add(ReporteAdjuntoExcel);
                Attachment ReporteAdjuntoPDF = new(ReportePDF);
                MensajeCorreo.Attachments.Add(ReporteAdjuntoPDF);
                SMTP.Send(MensajeCorreo);
            }
            catch { }
        }
    }
}