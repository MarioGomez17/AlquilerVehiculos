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
    public class ReporteUsuarios
    {
        public ModeloUsuario Usuario { get; set; }
        public ModeloEmpresa Empresa { get; set; }
        public string ConsultaSQL { get; set; }
        public ReporteUsuarios(int IdUsuario)
        {
            ModeloUsuario ModeloUsuario = new();
            this.Usuario = ModeloUsuario.TraerUsuario(IdUsuario);
            this.Empresa = new();
            this.ConsultaSQL = "SELECT " +
                "alquiler_vehiculos.tipo_identificacion_usuario.Nombre_TipoIdentificacionUsuario, " +
                "alquiler_vehiculos.tipo_identificacion_usuario.Simbolo_TipoIdentificacionUsuario, " +
                "alquiler_vehiculos.usuario.NumeroIdentificacion_Usuario, " +
                "alquiler_vehiculos.usuario.Nombre_Usuario, " +
                "alquiler_vehiculos.usuario.Apellido_Usuario, " +
                "alquiler_vehiculos.usuario.Telefono_Usuario, " +
                "alquiler_vehiculos.usuario.Correo_Usuario, " +
                "alquiler_vehiculos.estado_usuario.Nombre_EstadoUsuario, " +
                "alquiler_vehiculos.rol.Nombre_Rol, " +
                "alquiler_vehiculos.vehiculosusuario.Vehiculos, " +
                "alquiler_vehiculos.alquilereshechosusuario.AlquileresHechos, " +
                "alquiler_vehiculos.alquileresofrecidosusuario.AlquileresOfrecidos, " +
                "alquiler_vehiculos.alquilereshechosusuario.TotalAlquileresHechos, " +
                "alquiler_vehiculos.alquileresofrecidosusuario.TotalAlquileresOfrecidos, " +
                "alquiler_vehiculos.alquilereshechosusuario.GananciasAlquileresHechos, " +
                "alquiler_vehiculos.alquileresofrecidosusuario.GananciasAlquileresOfrecidos " +
                "FROM alquiler_vehiculos.usuario " +
                "INNER JOIN alquiler_vehiculos.tipo_identificacion_usuario " +
                "ON alquiler_vehiculos.tipo_identificacion_usuario.Id_TipoIdentificacionUsuario = alquiler_vehiculos.usuario.TipoIdentificacion_Usuario " +
                "INNER JOIN alquiler_vehiculos.estado_usuario " +
                "ON alquiler_vehiculos.estado_usuario.Id_EstadoUsuario = alquiler_vehiculos.usuario.Estado_Usuario " +
                "INNER JOIN alquiler_vehiculos.rol " +
                "ON alquiler_vehiculos.rol.Id_Rol = alquiler_vehiculos.usuario.Rol_Usuario " +
                "INNER JOIN alquiler_vehiculos.alquilereshechosusuario " +
                "ON alquiler_vehiculos.alquilereshechosusuario.Id_Usuario = alquiler_vehiculos.usuario.Id_Usuario " +
                "INNER JOIN alquiler_vehiculos.alquileresofrecidosusuario " +
                "ON alquiler_vehiculos.alquileresofrecidosusuario.Id_Usuario =  alquiler_vehiculos.alquilereshechosusuario.Id_Usuario " +
                "INNER JOIN alquiler_vehiculos.vehiculosusuario " +
                "ON alquiler_vehiculos.vehiculosusuario.Id_Usuario =  alquiler_vehiculos.alquilereshechosusuario.Id_Usuario " +
                "ORDER BY alquiler_vehiculos.usuario.NumeroIdentificacion_Usuario";
        }
        public void GenerarReporteUsuariosPDF()
        {
            string FilasTabla;
            int Indice = 0;
            int TotalVehiculos = 0;
            int TotalAlquileresRealizados = 0;
            int TotalAlquileresOfrecidos = 0;
            float TotalPrecioAlquileresRealizados = 0;
            float TotalPrecioAlquileresOfrecidos = 0;
            float TotalGanaciasAlquileresRealizados = 0;
            float TotalGanaciasAlquileresOfrecidos = 0;
            string RutaImagen = Directory.GetCurrentDirectory() + "/wwwroot/imagenes/";
            string HTMLString = File.ReadAllText("./Templates/ReporteUsuarios.html").ToString();
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
                                                "<td class='CeldaDatoTabla CeldaImpar'>" + Lector.GetString(0) + "<br>" + Lector.GetString(1) + "</td> " +
                                                "<td class='CeldaDatoTabla CeldaImpar'>" + Lector.GetString(2) + "</td> " +
                                                "<td class='CeldaDatoTabla CeldaImpar'>" + Lector.GetString(3) + "</td> " +
                                                "<td class='CeldaDatoTabla CeldaImpar'>" + Lector.GetString(4) + "</td> " +
                                                "<td class='CeldaDatoTabla CeldaImpar'>" + Lector.GetString(5) + "</td> " +
                                                "<td class='CeldaDatoTabla CeldaImpar'>" + Lector.GetString(6) + "</td> " +
                                                "<td class='CeldaDatoTabla CeldaImpar'>" + Lector.GetString(7) + "</td> " +
                                                "<td class='CeldaDatoTabla CeldaImpar'>" + Lector.GetString(8) + "</td> " +
                                                "<td class='CeldaDatoTabla CeldaImpar'>" + Lector.GetInt32(9).ToString() + "</td> " +
                                                "<td class='CeldaDatoTabla CeldaImpar'>" + Lector.GetInt32(10).ToString() + "</td> " +
                                                "<td class='CeldaDatoTabla CeldaImpar'>" + Lector.GetInt32(11).ToString() + "</td> " +
                                                "<td class='CeldaDatoTabla CeldaImpar'>" + Lector.GetFloat(12).ToString() + "</td> " +
                                                "<td class='CeldaDatoTabla CeldaImpar'>" + Lector.GetFloat(13).ToString() + "</td> " +
                                                "<td class='CeldaDatoTabla CeldaImpar'>" + Lector.GetFloat(14).ToString() + "</td> " +
                                                "<td class='CeldaDatoTabla CeldaImpar'>" + Lector.GetFloat(15).ToString() + "</td> " +
                                            "</tr> ");
                        }
                        else
                        {
                            ListaListas.Last().Add(
                                            "<tr>" +
                                                "<td class='CeldaDatoTabla CeldaPar'>" + Indice.ToString() + "</td> " +
                                                "<td class='CeldaDatoTabla CeldaPar'>" + Lector.GetString(0) + "<br>" + Lector.GetString(1) + "</td> " +
                                                "<td class='CeldaDatoTabla CeldaPar'>" + Lector.GetString(2) + "</td> " +
                                                "<td class='CeldaDatoTabla CeldaPar'>" + Lector.GetString(3) + "</td> " +
                                                "<td class='CeldaDatoTabla CeldaPar'>" + Lector.GetString(4) + "</td> " +
                                                "<td class='CeldaDatoTabla CeldaPar'>" + Lector.GetString(5) + "</td> " +
                                                "<td class='CeldaDatoTabla CeldaPar'>" + Lector.GetString(6) + "</td> " +
                                                "<td class='CeldaDatoTabla CeldaPar'>" + Lector.GetString(7) + "</td> " +
                                                "<td class='CeldaDatoTabla CeldaPar'>" + Lector.GetString(8) + "</td> " +
                                                "<td class='CeldaDatoTabla CeldaPar'>" + Lector.GetInt32(9).ToString() + "</td> " +
                                                "<td class='CeldaDatoTabla CeldaPar'>" + Lector.GetInt32(10).ToString() + "</td> " +
                                                "<td class='CeldaDatoTabla CeldaPar'>" + Lector.GetInt32(11).ToString() + "</td> " +
                                                "<td class='CeldaDatoTabla CeldaPar'>" + Lector.GetFloat(12).ToString() + "</td> " +
                                                "<td class='CeldaDatoTabla CeldaPar'>" + Lector.GetFloat(13).ToString() + "</td> " +
                                                "<td class='CeldaDatoTabla CeldaPar'>" + Lector.GetFloat(14).ToString() + "</td> " +
                                                "<td class='CeldaDatoTabla CeldaPar'>" + Lector.GetFloat(15).ToString() + "</td> " +
                                            "</tr> ");
                        }
                        TotalVehiculos += Lector.GetInt32(9);
                        TotalAlquileresRealizados += Lector.GetInt32(10);
                        TotalAlquileresOfrecidos += Lector.GetInt32(11);
                        TotalPrecioAlquileresRealizados += Lector.GetFloat(12);
                        TotalGanaciasAlquileresOfrecidos += Lector.GetFloat(13);
                        TotalGanaciasAlquileresRealizados += Lector.GetFloat(14);
                        TotalGanaciasAlquileresOfrecidos += Lector.GetFloat(15);
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
                        "<td colspan='9' class='FilaTotal TituloTotal'>Totales</td>" +
                        "<td class='FilaTotal'>" + TotalVehiculos.ToString() + "</td>" +
                        "<td class='FilaTotal'>" + TotalAlquileresRealizados.ToString() + "</td>" +
                        "<td class='FilaTotal'>" + TotalAlquileresOfrecidos.ToString() + "</td>" +
                        "<td class='FilaTotal'>" + TotalPrecioAlquileresRealizados.ToString() + "</td>" +
                        "<td class='FilaTotal'>" + TotalPrecioAlquileresOfrecidos.ToString() + "</td>" +
                        "<td class='FilaTotal'>" + TotalGanaciasAlquileresRealizados.ToString() + "</td>" +
                        "<td class='FilaTotal'>" + TotalGanaciasAlquileresOfrecidos.ToString() + "</td>" +
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
            DocumentoFinal.Save("./wwwroot/Reportes/ReporteUsuarios" + this.Usuario.NumeroIdentificacion + ".pdf");
        }
        public void GenerarReporteUsuariosEXCEL()
        {
            string RutaArchivo = "./wwwroot/Reportes/ReporteUsuarios" + this.Usuario.NumeroIdentificacion + ".xlsx";
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using ExcelPackage PaqueteExcel = new();
            PaqueteExcel.Workbook.Properties.Author = Usuario.Nombre + " " + Usuario.Apellido;
            PaqueteExcel.Workbook.Properties.Title = "Reporte de Alquileres Realizados";
            ExcelWorksheet HojaCalculo = PaqueteExcel.Workbook.Worksheets.Add("Reporte");
            HojaCalculo.Cells[1, 1].Value = "#";
            HojaCalculo.Cells[1, 2].Value = "TIPO DE IDENTIFICACIÓN";
            HojaCalculo.Cells[1, 3].Value = "IDENTIFICACIÓN";
            HojaCalculo.Cells[1, 4].Value = "NOMBRE";
            HojaCalculo.Cells[1, 5].Value = "APELLIDO";
            HojaCalculo.Cells[1, 6].Value = "TELEFONO";
            HojaCalculo.Cells[1, 7].Value = "CORREO";
            HojaCalculo.Cells[1, 8].Value = "ESTADO";
            HojaCalculo.Cells[1, 9].Value = "ROL";
            HojaCalculo.Cells[1, 10].Value = "CANTIDAD DE VEHÍCULOS";
            HojaCalculo.Cells[1, 11].Value = "CANTIDAD DE ALQUILERES REALIZADOS";
            HojaCalculo.Cells[1, 12].Value = "CANTIDAD DE ALQUILERES OFRECIDOS";
            HojaCalculo.Cells[1, 13].Value = "PRECIO ALQUILERES REALIZADOS";
            HojaCalculo.Cells[1, 14].Value = "PRECIO ALQUILERES OFRECIDOS";
            HojaCalculo.Cells[1, 15].Value = "GANANCIAS ALQUILERES REALIZADOS";
            HojaCalculo.Cells[1, 16].Value = "GANANCIAS ALQUILERES OFRECIDOS";
            using (var Encabezados = HojaCalculo.Cells[1, 1, 1, 16])
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
                        HojaCalculo.Cells[Celda, 2].Value = Lector.GetString(0) + " (" + Lector.GetString(1) + ")";
                        HojaCalculo.Cells[Celda, 3].Value = float.Parse(Lector.GetString(2));
                        HojaCalculo.Cells[Celda, 4].Value = Lector.GetString(3);
                        HojaCalculo.Cells[Celda, 5].Value = Lector.GetString(4);
                        HojaCalculo.Cells[Celda, 6].Value = float.Parse(Lector.GetString(5));
                        HojaCalculo.Cells[Celda, 7].Value = Lector.GetString(6);
                        HojaCalculo.Cells[Celda, 8].Value = Lector.GetString(7);
                        HojaCalculo.Cells[Celda, 9].Value = Lector.GetString(8);
                        HojaCalculo.Cells[Celda, 10].Value = Lector.GetInt32(9);
                        HojaCalculo.Cells[Celda, 11].Value = Lector.GetInt32(10);
                        HojaCalculo.Cells[Celda, 12].Value = Lector.GetInt32(11);
                        HojaCalculo.Cells[Celda, 13].Value = Lector.GetFloat(12);
                        HojaCalculo.Cells[Celda, 14].Value = Lector.GetFloat(13);
                        HojaCalculo.Cells[Celda, 15].Value = Lector.GetFloat(14);
                        HojaCalculo.Cells[Celda, 16].Value = Lector.GetFloat(15);
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
        public void EnviarReportesUsuariosPorCorreo(string CorreoReceptor)
        {
            try
            {
                GenerarReporteUsuariosEXCEL();
                GenerarReporteUsuariosPDF();
                string ReporteExcel = "./wwwroot/Reportes/ReporteUsuarios" + this.Usuario.NumeroIdentificacion + ".xlsx";
                string ReportePDF = "./wwwroot/Reportes/ReporteUsuarios" + this.Usuario.NumeroIdentificacion + ".pdf";
                string Asunto = "Reportes Todos los Usuarios";
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