using ALQUILER_VEHICULOS.Models;
using MySql.Data.MySqlClient;
using OfficeOpenXml.Style;
using OfficeOpenXml;
using System.Net.Mail;
using System.Net;
using SelectPdf;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Data;
namespace ALQUILER_VEHICULOS.Reports
{
    public class ReporteVehiculoPropietario
    {
        public ModeloUsuario Usuario { get; set; }
        public ModeloEmpresa Empresa { get; set; }
        public ModeloPropietario Propietario { get; set; }
        public int IdVehiculo { get; set; }
        public string ConsultaSQL { get; set; }
        public ReporteVehiculoPropietario(int IdUsuario, int IdVehiculo)
        {
            ModeloPropietario ModeloPropietario = new();
            ModeloUsuario ModeloUsuario = new();
            this.Usuario = ModeloUsuario.TraerUsuario(IdUsuario);
            this.Empresa = new();
            this.Propietario = ModeloPropietario.TraerPropietarioUsuario(this.Usuario.Id);
            this.IdVehiculo = IdVehiculo;
            this.ConsultaSQL = "SELECT " +
                "alquiler_vehiculos.tipo_vehiculo.Nombre_TipoVehiculo, " +
                "alquiler_vehiculos.marca_vehiculo.Nombre_MarcaVehiculo, " +
                "alquiler_vehiculos.linea_vehiculo.Nombre_LineaVehiculo, " +
                "alquiler_vehiculos.vehiculo.Placa_Vehiculo, " +
                "alquiler_vehiculos.modelo.Valor_Modelo, " +
                "alquiler_vehiculos.cilindrada.Valor_Cilindrada, " +
                "alquiler_vehiculos.color.Nombre_Color, " +
                "alquiler_vehiculos.cantidad_pasajeros.Valor_CantidadPasajeros, " +
                "alquiler_vehiculos.vehiculo.PrecioAlquilerDia_Vehiculo, " +
                "alquiler_vehiculos.vehiculo.NumeroSeguro_Vehiculo, " +
                "alquiler_vehiculos.vehiculo.NumeroCertificadoCDA_Vehiculo, " +
                "alquiler_vehiculos.ciudad.Nombre_Ciudad, " +
                "alquiler_vehiculos.departamento.Nombre_Departamento, " +
                "alquiler_vehiculos.estado_vehiculo.Nombre_EstadoVehiculo, " +
                "COALESCE(COUNT(alquiler_vehiculos.alquiler.Id_Alquiler), 0), " +
                "COALESCE(SUM(alquiler_vehiculos.alquiler.Precio_Alquiler), 0), " +
                "COALESCE(SUM(alquiler_vehiculos.alquiler.Ganancias_Alquiler), 0), " +
                "alquiler_vehiculos.vehiculo.Foto_Vehiculo " +
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
                "INNER JOIN alquiler_vehiculos.color " +
                "ON alquiler_vehiculos.color.Id_Color = alquiler_vehiculos.vehiculo.Color_Vehiculo " +
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
                "WHERE alquiler_vehiculos.vehiculo.Id_Vehiculo = " + this.IdVehiculo + " " +
                "GROUP BY " +
                "alquiler_vehiculos.tipo_vehiculo.Nombre_TipoVehiculo, " +
                "alquiler_vehiculos.marca_vehiculo.Nombre_MarcaVehiculo, " +
                "alquiler_vehiculos.linea_vehiculo.Nombre_LineaVehiculo, " +
                "alquiler_vehiculos.vehiculo.Placa_Vehiculo, " +
                "alquiler_vehiculos.modelo.Valor_Modelo, " +
                "alquiler_vehiculos.cilindrada.Valor_Cilindrada, " +
                "alquiler_vehiculos.cantidad_pasajeros.Valor_CantidadPasajeros, " +
                "alquiler_vehiculos.vehiculo.PrecioAlquilerDia_Vehiculo, " +
                "alquiler_vehiculos.ciudad.Nombre_Ciudad, " +
                "alquiler_vehiculos.departamento.Nombre_Departamento, " +
                "alquiler_vehiculos.estado_vehiculo.Nombre_EstadoVehiculo " +
                "ORDER BY alquiler_vehiculos.vehiculo.Placa_Vehiculo ASC";
        }
        public void GenerarReporteVehiculoPropietarioPDF()
        {
            string RutaImagen = Directory.GetCurrentDirectory() + "/wwwroot/imagenes/";
            string RutaImagenVehiculo = Directory.GetCurrentDirectory() + "/wwwroot/FotoVehiculos/";
            string HTMLString = File.ReadAllText("./Templates/ReporteVehiculoPropietario.html").ToString();
            HtmlToPdf Convertidor = new();
            PdfDocument DocumentoFinal;
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
                        HTMLString = HTMLString.Replace("@VEHICULO", Lector.GetString(0) + " " + Lector.GetString(1) + Lector.GetString(2));
                        HTMLString = HTMLString.Replace("@PLACA", Lector.GetString(3));
                        HTMLString = HTMLString.Replace("@MODELO", Lector.GetInt32(4).ToString());
                        HTMLString = HTMLString.Replace("@CILINDRADA", Lector.GetInt32(5).ToString());
                        HTMLString = HTMLString.Replace("@COLOR", Lector.GetString(6));
                        HTMLString = HTMLString.Replace("@CANTIDADPASAJEROS", Lector.GetInt32(7).ToString());
                        HTMLString = HTMLString.Replace("@PRECIOALQUILER", Lector.GetFloat(8).ToString());
                        HTMLString = HTMLString.Replace("@SEGURO", Lector.GetString(9));
                        HTMLString = HTMLString.Replace("@CDA", Lector.GetString(10));
                        HTMLString = HTMLString.Replace("@CIUDAD", Lector.GetString(11) + ", " + Lector.GetString(12));
                        HTMLString = HTMLString.Replace("@ESTADO", Lector.GetString(13));
                        HTMLString = HTMLString.Replace("@ALQUILERES", Lector.GetInt32(14).ToString());
                        HTMLString = HTMLString.Replace("@PRECIOS", Lector.GetFloat(15).ToString());
                        HTMLString = HTMLString.Replace("@GANANCIAS", (Lector.GetFloat(15) - Lector.GetFloat(16)).ToString());
                        HTMLString = HTMLString.Replace("@FOTOVEHICULO", RutaImagenVehiculo + Lector.GetString(17));
                    }
                }
            }
            catch (Exception e) { Console.WriteLine(e); }
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
            HTMLString = HTMLString.Replace("@FOTO", RutaImagen + this.Empresa.RutaFoto);
            HTMLString = HTMLString.Replace("@PAGINAS", "PAGINA 1 DE 1");
            DocumentoFinal = Convertidor.ConvertHtmlString(HTMLString);
            DocumentoFinal.Save("./wwwroot/Reportes/ReporteVehiculoPropietario" + this.Usuario.NumeroIdentificacion + ".pdf");
        }
        public void EnviarReportesVehiculoPropietarioPorCorreo(string CorreoReceptor)
        {
            try
            {
                GenerarReporteVehiculoPropietarioPDF();
                string ReportePDF = "./wwwroot/Reportes/ReporteVehiculoPropietario" + this.Usuario.NumeroIdentificacion + ".pdf";
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
                Attachment ReporteAdjuntoPDF = new(ReportePDF);
                MensajeCorreo.Attachments.Add(ReporteAdjuntoPDF);
                SMTP.Send(MensajeCorreo);
            }
            catch { }
        }
    }
}