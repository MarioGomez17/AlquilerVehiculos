using ALQUILER_VEHICULOS.Models;
using MySql.Data.MySqlClient;
using OfficeOpenXml.Style;
using OfficeOpenXml;
using System.Net.Mail;
using System.Net;
using SelectPdf;

namespace ALQUILER_VEHICULOS.Reports
{
    public class ReporteAlquilerPropietario
    {
        public ModeloUsuario Usuario { get; set; }
        public ModeloEmpresa Empresa { get; set; }
        public string ConsultaSQL { get; set; }
        public ReporteAlquilerPropietario(int IdUsuario, int IdAlquiler)
        {
            ModeloUsuario ModeloUsuario = new();
            this.Usuario = ModeloUsuario.TraerUsuario(IdUsuario);
            this.Empresa = new();
            this.ConsultaSQL = "SELECT " +
                                "alquiler_vehiculos.tipo_vehiculo.Nombre_TipoVehiculo, " +
                                "alquiler_vehiculos.marca_vehiculo.Nombre_MarcaVehiculo, " +
                                "alquiler_vehiculos.linea_vehiculo.Nombre_LineaVehiculo, " +
                                "alquiler_vehiculos.vehiculo.Placa_Vehiculo, " +
                                "alquiler_vehiculos.usuario.Nombre_Usuario, " +
                                "alquiler_vehiculos.usuario.Apellido_Usuario, " +
                                "alquiler_vehiculos.tipo_identificacion_usuario.Simbolo_TipoIdentificacionUsuario, " +
                                "alquiler_vehiculos.usuario.NumeroIdentificacion_Usuario, " +
                                "alquiler_vehiculos.ciudad.Nombre_Ciudad, " +
                                "alquiler_vehiculos.departamento.Nombre_Departamento, " +
                                "alquiler_vehiculos.alquiler.FechaIncio_Alquiler, " +
                                "alquiler_vehiculos.alquiler.FechaFin_Alquiler, " +
                                "alquiler_vehiculos.alquiler.Precio_Alquiler, " +
                                "alquiler_vehiculos.alquiler.Ganancias_Alquiler, " +
                                "alquiler_vehiculos.vehiculo.Foto_Vehiculo, " +
                                "alquiler_vehiculos.modelo.Valor_Modelo, " +
                                "alquiler_vehiculos.cilindrada.Valor_Cilindrada, " +
                                "alquiler_vehiculos.color.Nombre_Color, " +
                                "alquiler_vehiculos.cantidad_pasajeros.Valor_CantidadPasajeros, " +
                                "alquiler_vehiculos.vehiculo.NumeroSeguro_Vehiculo, " +
                                "alquiler_vehiculos.vehiculo.NumeroCertificadoCDA_Vehiculo, " +
                                "alquiler_vehiculos.lugar_alquiler.Nombre_LugarAlquiler " +
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
                                "INNER JOIN alquiler_vehiculos.alquilador " +
                                "ON alquiler_vehiculos.alquilador.Id_Alquilador = alquiler_vehiculos.alquiler.Alquilador_Alquiler " +
                                "INNER JOIN alquiler_vehiculos.usuario " +
                                "ON alquiler_vehiculos.usuario.Id_Usuario = alquiler_vehiculos.alquilador.Usuario_Alquilador " +
                                "INNER JOIN alquiler_vehiculos.tipo_identificacion_usuario " +
                                "ON alquiler_vehiculos.tipo_identificacion_usuario.Id_TipoIdentificacionUsuario = alquiler_vehiculos.usuario.TipoIdentificacion_Usuario " +
                                "INNER JOIN alquiler_vehiculos.modelo " +
                                "ON alquiler_vehiculos.modelo.Id_Modelo =  alquiler_vehiculos.vehiculo.Modelo_Vehiculo " +
                                "INNER JOIN alquiler_vehiculos.cilindrada " +
                                "ON alquiler_vehiculos.cilindrada.Id_Cilindrada =  alquiler_vehiculos.vehiculo.Cilindrada_Vehiculo " +
                                "INNER JOIN alquiler_vehiculos.color " +
                                "ON alquiler_vehiculos.color.Id_Color =  alquiler_vehiculos.vehiculo.Color_Vehiculo " +
                                "INNER JOIN alquiler_vehiculos.cantidad_pasajeros " +
                                "ON alquiler_vehiculos.cantidad_pasajeros.Id_CantidadPasajeros =  alquiler_vehiculos.vehiculo.CantidadPasajeros_Vehiculo " +
                                "INNER JOIN alquiler_vehiculos.lugar_alquiler " +
                                "ON alquiler_vehiculos.lugar_alquiler.Id_LugarAlquiler = alquiler_vehiculos.alquiler.Lugar_Alquiler " +
                                "WHERE alquiler_vehiculos.alquiler.Id_Alquiler = " + IdAlquiler;
        }
        public void GenerarReporteAlquilerPropietarioPDF()
        {
            string RutaImagenEmpresa = Directory.GetCurrentDirectory() + "/wwwroot/imagenes/";
            string RutaImagenVehiculo = Directory.GetCurrentDirectory() + "/wwwroot/FotoVehiculos/";
            string HTMLString = File.ReadAllText("./Templates/ReporteAlquilerPropietario.html").ToString();
            HtmlToPdf Convertidor = new();
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
                        HTMLString = HTMLString.Replace("@VEHICULO", Lector.GetString(0) + " " + Lector.GetString(1) + " " + Lector.GetString(2));
                        HTMLString = HTMLString.Replace("@PLACA", Lector.GetString(3));
                        HTMLString = HTMLString.Replace("@ALQUILADOR", Lector.GetString(4) + " " + Lector.GetString(5));
                        HTMLString = HTMLString.Replace("@SIMBOLO", Lector.GetString(6));
                        HTMLString = HTMLString.Replace("@IDENTIFICACIONALQUILADOR", Lector.GetString(7));
                        HTMLString = HTMLString.Replace("@CIUDADALQUILER", Lector.GetString(8) + ", " + Lector.GetString(9));
                        HTMLString = HTMLString.Replace("@FECHAINICIO", new DateOnly(Lector.GetDateTime(10).Year, Lector.GetDateTime(10).Month, Lector.GetDateTime(10).Day).ToString());
                        HTMLString = HTMLString.Replace("@FECHAFIN", new DateOnly(Lector.GetDateTime(11).Year, Lector.GetDateTime(11).Month, Lector.GetDateTime(11).Day).ToString());
                        HTMLString = HTMLString.Replace("@PRECIO", Lector.GetFloat(12).ToString());
                        HTMLString = HTMLString.Replace("@COMISION", Lector.GetFloat(13).ToString());
                        HTMLString = HTMLString.Replace("@GANANCIAS", (Lector.GetFloat(12) - Lector.GetFloat(13)).ToString());
                        HTMLString = HTMLString.Replace("@FOTOVEHICULO", RutaImagenVehiculo + Lector.GetString(14));
                        HTMLString = HTMLString.Replace("@MODELO", Lector.GetInt32(15).ToString());
                        HTMLString = HTMLString.Replace("@CILINDRADA", Lector.GetInt32(16).ToString());
                        HTMLString = HTMLString.Replace("@COLOR", Lector.GetString(17));
                        HTMLString = HTMLString.Replace("@CANTIDADPASAJEROS", Lector.GetInt32(18).ToString());
                        HTMLString = HTMLString.Replace("@SEGURO", Lector.GetString(19));
                        HTMLString = HTMLString.Replace("@CDA", Lector.GetString(20));
                        HTMLString = HTMLString.Replace("@LUGAR", Lector.GetString(21));
                    }
                }
            }
            catch (Exception) {}
            finally
            {
                ConexionBD.Close();
            }
            HTMLString = HTMLString.Replace("@PROPIETARIO", this.Usuario.Nombre + " " + this.Usuario.Apellido);
            HTMLString = HTMLString.Replace("@IDENTIFICACION", this.Usuario.SimboloTipoIdentificacion + " " + this.Usuario.NumeroIdentificacion);
            HTMLString = HTMLString.Replace("@FECHAINFORME", DateTime.Now.ToString("dddd dd-MMMM-yyyy hh:mm:ss tt"));
            HTMLString = HTMLString.Replace("@RAZONSOCIAL", this.Empresa.Nombre);
            HTMLString = HTMLString.Replace("@CIUDAD", this.Empresa.Ciudad);
            HTMLString = HTMLString.Replace("@DIRECCION", this.Empresa.Direccion);
            HTMLString = HTMLString.Replace("@BARRIO", this.Empresa.Barrio);
            HTMLString = HTMLString.Replace("@NIT", this.Empresa.NIT);
            HTMLString = HTMLString.Replace("@TELEFONO", this.Empresa.Telefono);
            HTMLString = HTMLString.Replace("@CORREO", this.Empresa.Correo);
            HTMLString = HTMLString.Replace("@FOTO", RutaImagenEmpresa + this.Empresa.RutaFoto);
            HTMLString = HTMLString.Replace("@PAGINAS", "PAGINA 1 DE 1");
            PdfDocument InformePDF = Convertidor.ConvertHtmlString(HTMLString);
            InformePDF.Save("./wwwroot/Reportes/ReporteAlquilerPropietario" + this.Usuario.NumeroIdentificacion + ".pdf");
        }

        public void EnviarReportesAlquilerPropietarioPorCorreo(string CorreoReceptor)
        {
            GenerarReporteAlquilerPropietarioPDF();
            string ReportePDF = "./wwwroot/Reportes/ReporteAlquilerPropietario" + this.Usuario.NumeroIdentificacion + ".pdf";
            string CorreoEmisor = "mariog.101200@hotmail.com";
            string Asunto = "Reportes de Alquiler Ofrecidos por " + this.Usuario.Nombre + " " + this.Usuario.Apellido;
            string Mensaje = "Documento del alquiler solicitado";
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