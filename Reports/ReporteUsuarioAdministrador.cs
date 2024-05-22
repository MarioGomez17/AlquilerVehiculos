using ALQUILER_VEHICULOS.Models;
using MySql.Data.MySqlClient;
using OfficeOpenXml.Style;
using OfficeOpenXml;
using System.Net.Mail;
using System.Net;
using SelectPdf;

namespace ALQUILER_VEHICULOS.Reports
{
    public class ReporteUsuarioAdministrador
    {
        public ModeloUsuario Usuario { get; set; }
        public ModeloEmpresa Empresa { get; set; }
        public string ConsultaSQL { get; set; }
        public ReporteUsuarioAdministrador(int IdUsuarioAdministrador, int IdUsuarioReporte)
        {
            ModeloUsuario ModeloUsuario = new();
            this.Usuario = ModeloUsuario.TraerUsuario(IdUsuarioAdministrador);
            this.Empresa = new();
            this.ConsultaSQL = "SELECT " +
                "alquiler_vehiculos.usuario.Nombre_Usuario, " +
                "alquiler_vehiculos.usuario.Apellido_Usuario, " +
                "alquiler_vehiculos.tipo_identificacion_usuario.Nombre_TipoIdentificacionUsuario, " +
                "alquiler_vehiculos.tipo_identificacion_usuario.Simbolo_TipoIdentificacionUsuario, " +
                "alquiler_vehiculos.usuario.NumeroIdentificacion_Usuario, " +
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
                "WHERE  alquiler_vehiculos.usuario.Id_Usuario = " + IdUsuarioReporte;
        }
        public void GenerarReporteUsuarioAdministradorPDF()
        {
            string RutaImagen = Directory.GetCurrentDirectory() + "/wwwroot/imagenes/";
            string HTMLString = File.ReadAllText("./Templates/ReporteUsuarioAdministrador.html").ToString();
            HtmlToPdf Convertidor = new();
            PdfDocument DocumentoFinal = new();
            Convertidor.Options.PdfPageOrientation = PdfPageOrientation.Landscape;
            Convertidor.Options.MarginLeft = 0;
            Convertidor.Options.MarginRight = 0;
            Convertidor.Options.MarginTop = 0;
            Convertidor.Options.MarginBottom = 0;
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
                        HTMLString = HTMLString.Replace("@USUARIOREPORTE", Lector.GetString(0) + " " + Lector.GetString(1));
                        HTMLString = HTMLString.Replace("@TIPOIDENTIFICACION", Lector.GetString(2) + " (" + Lector.GetString(3) + ")");
                        HTMLString = HTMLString.Replace("@NUMEROIDENTIFICACION", Lector.GetString(4));
                        HTMLString = HTMLString.Replace("@TELEFONO", Lector.GetString(5));
                        HTMLString = HTMLString.Replace("@CORREO", Lector.GetString(6));
                        HTMLString = HTMLString.Replace("@ESTADO", Lector.GetString(7));
                        HTMLString = HTMLString.Replace("@ROL", Lector.GetString(8));
                        HTMLString = HTMLString.Replace("@CANTIDADVEHICULOS", Lector.GetInt32(9).ToString());
                        HTMLString = HTMLString.Replace("@CANTIDADALQUILERESREALIZADOS", Lector.GetInt32(10).ToString());
                        HTMLString = HTMLString.Replace("@CANTIDADALQUILERESOFRECIDOS", Lector.GetInt32(11).ToString());
                        HTMLString = HTMLString.Replace("@TOTALALQUILERESREALIZADOS", Lector.GetFloat(12).ToString());
                        HTMLString = HTMLString.Replace("@TOTALQUILERESOFRECIDOS", Lector.GetFloat(13).ToString());
                        HTMLString = HTMLString.Replace("@GANANCIASALQUILERESREALIZADOS", Lector.GetFloat(14).ToString());
                        HTMLString = HTMLString.Replace("@GANANCIASALQUILERESOFRECIDOS", Lector.GetFloat(15).ToString());
                    }
                }
            }
            catch (Exception) { }
            finally
            {
                ConexionBD.Close();
            }
            HTMLString = HTMLString.Replace("@USUARIO", this.Usuario.Nombre + " " + this.Usuario.Apellido);
            HTMLString = HTMLString.Replace("@IDENTIFICACION", this.Usuario.SimboloTipoIdentificacion + " " + this.Usuario.NumeroIdentificacion);
            HTMLString = HTMLString.Replace("@FECHAINFORME", DateTime.Now.ToString("dddd dd-MMMM-yyyy hh:mm:ss tt"));
            HTMLString = HTMLString.Replace("@RAZONSOCIAL", this.Empresa.Nombre);
            HTMLString = HTMLString.Replace("@CIUDAD", this.Empresa.Ciudad);
            HTMLString = HTMLString.Replace("@DIRECCION", this.Empresa.Direccion);
            HTMLString = HTMLString.Replace("@BARRIO", this.Empresa.Barrio);
            HTMLString = HTMLString.Replace("@NIT", this.Empresa.NIT);
            HTMLString = HTMLString.Replace("@TELEFONO", this.Empresa.Telefono);
            HTMLString = HTMLString.Replace("@CORREO", this.Empresa.Correo);
            HTMLString = HTMLString.Replace("@FOTO", RutaImagen + this.Empresa.RutaFoto);
            HTMLString = HTMLString.Replace("@PAGINAS", "PAGINA 1 de 1");
            DocumentoFinal = Convertidor.ConvertHtmlString(HTMLString);
            DocumentoFinal.Save("./wwwroot/Reportes/ReporteUsuarioAdministrador" + this.Usuario.NumeroIdentificacion + ".pdf");
        }
        public void EnviarReportesUsuarioAdministradorPorCorreo(string CorreoReceptor)
        {
            GenerarReporteUsuarioAdministradorPDF();
            string ReportePDF = "./wwwroot/Reportes/ReporteUsuarioAdministrador" + this.Usuario.NumeroIdentificacion + ".pdf";
            string CorreoEmisor = "mariog.101200@hotmail.com";
            string Asunto = "Reporte de usuario como Administrador";
            string Mensaje = "Adjunto encontrarás el reporte en PDF";
            SmtpClient ClienteAMTP = new("smtp-mail.outlook.com", 587)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential(CorreoEmisor, "M@rio112358")
            };
            MailMessage MensajeCorreo = new(CorreoEmisor, CorreoReceptor, Asunto, Mensaje);
            Attachment ReporteAdjuntoPDF = new(ReportePDF);
            MensajeCorreo.Attachments.Add(ReporteAdjuntoPDF);
            ClienteAMTP.Send(MensajeCorreo);
        }
    }
}