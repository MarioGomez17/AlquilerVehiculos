using System.Net;
using System.Net.Mail;
using MySql.Data.MySqlClient;
namespace ALQUILER_VEHICULOS.Models
{
    public class ModeloAlquiler
    {
        public int Id { get; set; }
        public DateOnly FechaIncio { get; set; }
        public DateOnly FechaFin { get; set; }
        public float Precio { get; set; }
        public float Ganancia { get; set; }
        public bool Lavada { get; set; }
        public ModeloAlquilador Alquilador { get; set; }
        public ModeloVehiculo Vehiculo { get; set; }
        public string Lugar { get; set; }
        public string MetodoPago { get; set; }
        public string EstadoPago { get; set; }
        public string Seguro { get; set; }
        public float PrecioSeguro { get; set; }
        public float Calificacion { get; set; }
        public string ComentarioCalificacion { get; set; }
        public string Estado { get; set; }
        public bool CrearAquiler(DateOnly FechaInicio, DateOnly FechaFin, float Precio, int Lavada, int Alquilador, int Vehiculo, int Lugar, int MetodoPago, int Seguro, float Ganancia)
        {
            string ConsultaSQL = "INSERT INTO " +
                "alquiler_vehiculos.alquiler (" +
                "FechaIncio_Alquiler, " +
                "FechaFin_Alquiler, " +
                "Precio_Alquiler, " +
                "Ganancias_Alquiler, " +
                "LavadaVehiculo_Alquiler, " +
                "Alquilador_Alquiler, " +
                "Vehiculo_Alquiler, " +
                "Lugar_Alquiler, " +
                "MeotodoPago_Alquiler, " +
                "Seguro_Alquiler, " +
                " Estado_Alquiler) " +
                "VALUES ('" +
                FechaInicio.ToString("yyyy-MM-dd") + "', " +
                "'" + FechaFin.ToString("yyyy-MM-dd") + "', " +
                Precio + ", " +
                Ganancia + ", " +
                Lavada + ", " +
                Alquilador + ", " +
                Vehiculo + ", " +
                Lugar + ", " +
                MetodoPago + ", " +
                Seguro + ", " +
                3 + ")";
            ModeloConexion.ExecuteNonQuerySentence(ConsultaSQL);
            int IdAlquiler = TraerIdUltimoAlquilerPorVehiculo(Vehiculo);
            ModeloAlquilador ModeloAlquilador = new();
            ModeloPropietario ModeloPropietario = new();
            ModeloVehiculo ModeloVehiculo = new();
            ModeloVehiculo = ModeloVehiculo.TraerVehiculo(Vehiculo);
            ModeloVehiculo.UsuarioPropietario.AumentarAlquileresPendientes(ModeloVehiculo.UsuarioPropietario.Id);
            ModeloAlquilador.AgregarAlquilerHistorialAlquilador(Alquilador, IdAlquiler);
            ModeloAlquilador = ModeloAlquilador.TraerAlquilador(Alquilador);
            ReportarNuevoAlquilerPorCorreo(ModeloVehiculo, ModeloAlquilador.Usuario, FechaInicio.ToString("dddd dd-MM-yyyy"), FechaFin.ToString("dddd dd-MM-yyyy"), Precio);
            return ModeloPropietario.AgregarAlquilerHistorialPropietario(ModeloVehiculo.Propietario, IdAlquiler);
        }
        public ModeloAlquiler TraerAlquiler(int IdAlquiler)
        {
            ModeloAlquiler ModeloALquiler = new();
            ModeloAlquilador ModeloAlquilador = new();
            ModeloVehiculo ModeloVehiculo = new();
            string ConsultaSQL = "SELECT " +
                                "alquiler_vehiculos.alquiler.Id_Alquiler, " +
                                "alquiler_vehiculos.alquiler.FechaIncio_Alquiler, " +
                                "alquiler_vehiculos.alquiler.FechaFin_Alquiler, " +
                                "alquiler_vehiculos.alquiler.Precio_Alquiler, " +
                                "alquiler_vehiculos.alquiler.Ganancias_Alquiler, " +
                                "alquiler_vehiculos.alquiler.LavadaVehiculo_Alquiler, " +
                                "alquiler_vehiculos.alquiler.Alquilador_Alquiler, " +
                                "alquiler_vehiculos.alquiler.Vehiculo_Alquiler, " +
                                "alquiler_vehiculos.lugar_alquiler.Nombre_LugarAlquiler, " +
                                "alquiler_vehiculos.metodo_pago.Nombre_MetodoPago, " +
                                "alquiler_vehiculos.estado_pago_alquiler.Nombre_EstadoPagoAlquiler, " +
                                "alquiler_vehiculos.seguro_alquiler.Nombre_SeguroAlquiler, " +
                                "alquiler_vehiculos.seguro_alquiler.Precio_SeguroAlquiler, " +
                                "alquiler_vehiculos.alquiler.Calificacion_Alquiler, " +
                                "alquiler_vehiculos.alquiler.ComentarioCalificacion_Alquiler, " +
                                "alquiler_vehiculos.estado_alquiler.Nombre_EstadoAlquiler " +
                                "FROM alquiler_vehiculos.alquiler " +
                                "INNER JOIN alquiler_vehiculos.lugar_alquiler " +
                                "ON alquiler_vehiculos.lugar_alquiler.Id_LugarAlquiler = alquiler_vehiculos.alquiler.Lugar_Alquiler " +
                                "INNER JOIN alquiler_vehiculos.metodo_pago " +
                                "ON alquiler_vehiculos.metodo_pago.Id_MetodoPago = alquiler_vehiculos.alquiler.MeotodoPago_Alquiler " +
                                "INNER JOIN alquiler_vehiculos.estado_pago_alquiler " +
                                "ON alquiler_vehiculos.estado_pago_alquiler.Id_EstadoPagoAlquiler = alquiler_vehiculos.alquiler.EstadoPago_Alquiler " +
                                "INNER JOIN alquiler_vehiculos.seguro_alquiler " +
                                "ON alquiler_vehiculos.seguro_alquiler.Id_SeguroAlquiler = alquiler_vehiculos.alquiler.Seguro_Alquiler " +
                                "INNER JOIN alquiler_vehiculos.estado_alquiler " +
                                "ON alquiler_vehiculos.estado_alquiler.Id_EstadoAlquiler = alquiler_vehiculos.alquiler.Estado_Alquiler " +
                                "WHERE alquiler_vehiculos.alquiler.Id_Alquiler = " + IdAlquiler;
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
                        ModeloALquiler = new()
                        {
                            Id = Lector.GetInt32(0),
                            FechaIncio = new DateOnly(Lector.GetDateTime(1).Year, Lector.GetDateTime(1).Month, Lector.GetDateTime(1).Day),
                            FechaFin = new DateOnly(Lector.GetDateTime(2).Year, Lector.GetDateTime(2).Month, Lector.GetDateTime(2).Day),
                            Precio = Lector.GetFloat(3),
                            Ganancia = Lector.GetFloat(4),
                            Lavada = Lector.GetInt32(5) == 1,
                            Alquilador = ModeloAlquilador.TraerAlquilador(Lector.GetInt32(6)),
                            Vehiculo = ModeloVehiculo.TraerVehiculo(Lector.GetInt32(7)),
                            Lugar = Lector.GetString(8),
                            MetodoPago = Lector.GetString(9),
                            EstadoPago = Lector.GetString(10),
                            Seguro = Lector.GetString(11),
                            PrecioSeguro = Lector.GetFloat(12),
                            Calificacion = Lector.GetFloat(13),
                            ComentarioCalificacion = Lector.GetString(14),
                            Estado = Lector.GetString(15),
                        };
                    }
                }
            }
            catch (Exception) { }
            finally
            {
                ConexionBD.Close();
            }
            return ModeloALquiler;
        }
        public int TraerIdUltimoAlquilerPorVehiculo(int IdVehiculo)
        {
            int IdAlquiler = 0;
            string ConsultaSQL = "SELECT " +
                                "alquiler_vehiculos.alquiler.Id_Alquiler " +
                                "FROM alquiler_vehiculos.alquiler " +
                                "WHERE alquiler_vehiculos.alquiler.Vehiculo_Alquiler = " + IdVehiculo + " " +
                                "ORDER BY alquiler_vehiculos.alquiler.Id_Alquiler DESC LIMIT 1";
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
                        IdAlquiler = Lector.GetInt32(0);
                    }
                }
            }
            catch (Exception) { }
            finally
            {
                ConexionBD.Close();
            }
            return IdAlquiler;
        }
        public List<ModeloAlquiler> TraerAlquileres()
        {
            List<ModeloAlquiler> Alquileres = [];
            ModeloAlquilador ModeloAlquilador = new();
            ModeloVehiculo ModeloVehiculo = new();
            string ConsultaSQL = "SELECT " +
                                "alquiler_vehiculos.alquiler.Id_Alquiler, " +
                                "alquiler_vehiculos.alquiler.FechaIncio_Alquiler, " +
                                "alquiler_vehiculos.alquiler.FechaFin_Alquiler, " +
                                "alquiler_vehiculos.alquiler.Precio_Alquiler, " +
                                "alquiler_vehiculos.alquiler.Ganancias_Alquiler, " +
                                "alquiler_vehiculos.alquiler.LavadaVehiculo_Alquiler, " +
                                "alquiler_vehiculos.alquiler.Alquilador_Alquiler, " +
                                "alquiler_vehiculos.alquiler.Vehiculo_Alquiler, " +
                                "alquiler_vehiculos.lugar_alquiler.Nombre_LugarAlquiler, " +
                                "alquiler_vehiculos.metodo_pago.Nombre_MetodoPago, " +
                                "alquiler_vehiculos.estado_pago_alquiler.Nombre_EstadoPagoAlquiler, " +
                                "alquiler_vehiculos.seguro_alquiler.Nombre_SeguroAlquiler, " +
                                "alquiler_vehiculos.seguro_alquiler.Precio_SeguroAlquiler, " +
                                "alquiler_vehiculos.alquiler.Calificacion_Alquiler, " +
                                "alquiler_vehiculos.alquiler.ComentarioCalificacion_Alquiler, " +
                                "alquiler_vehiculos.estado_alquiler.Nombre_EstadoAlquiler " +
                                "FROM alquiler_vehiculos.alquiler " +
                                "INNER JOIN alquiler_vehiculos.lugar_alquiler " +
                                "ON alquiler_vehiculos.lugar_alquiler.Id_LugarAlquiler = alquiler_vehiculos.alquiler.Lugar_Alquiler " +
                                "INNER JOIN alquiler_vehiculos.metodo_pago " +
                                "ON alquiler_vehiculos.metodo_pago.Id_MetodoPago = alquiler_vehiculos.alquiler.MeotodoPago_Alquiler " +
                                "INNER JOIN alquiler_vehiculos.estado_pago_alquiler " +
                                "ON alquiler_vehiculos.estado_pago_alquiler.Id_EstadoPagoAlquiler = alquiler_vehiculos.alquiler.EstadoPago_Alquiler " +
                                "INNER JOIN alquiler_vehiculos.seguro_alquiler " +
                                "ON alquiler_vehiculos.seguro_alquiler.Id_SeguroAlquiler = alquiler_vehiculos.alquiler.Seguro_Alquiler " +
                                "INNER JOIN alquiler_vehiculos.estado_alquiler " +
                                "ON alquiler_vehiculos.estado_alquiler.Id_EstadoAlquiler = alquiler_vehiculos.alquiler.Estado_Alquiler " +
                                "ORDER BY alquiler_vehiculos.alquiler.Id_Alquiler ASC";
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
                        ModeloAlquiler Alquiler = new()
                        {
                            Id = Lector.GetInt32(0),
                            FechaIncio = new DateOnly(Lector.GetDateTime(1).Year, Lector.GetDateTime(1).Month, Lector.GetDateTime(1).Day),
                            FechaFin = new DateOnly(Lector.GetDateTime(2).Year, Lector.GetDateTime(2).Month, Lector.GetDateTime(2).Day),
                            Precio = Lector.GetFloat(3),
                            Ganancia = Lector.GetFloat(4),
                            Lavada = Lector.GetInt32(5) == 1,
                            Alquilador = ModeloAlquilador.TraerAlquilador(Lector.GetInt32(6)),
                            Vehiculo = ModeloVehiculo.TraerVehiculo(Lector.GetInt32(7)),
                            Lugar = Lector.GetString(8),
                            MetodoPago = Lector.GetString(9),
                            EstadoPago = Lector.GetString(10),
                            Seguro = Lector.GetString(11),
                            PrecioSeguro = Lector.GetFloat(12),
                            Calificacion = Lector.GetFloat(13),
                            ComentarioCalificacion = Lector.GetString(14),
                            Estado = Lector.GetString(15),
                        };
                        Alquileres.Add(Alquiler);
                    }
                }
            }
            catch (Exception) { }
            finally
            {
                ConexionBD.Close();
            }
            return Alquileres;
        }
        public bool IniciarAlquiler(int IdAlquiler)
        {
            string ConsultaSQL = "UPDATE " +
            "alquiler_vehiculos.alquiler " +
            "SET " +
            "alquiler_vehiculos.alquiler.Estado_Alquiler = 1 " +
            "WHERE " +
            "(alquiler_vehiculos.alquiler.Id_Alquiler = (" + IdAlquiler + "))";
            return ModeloConexion.ExecuteNonQuerySentence(ConsultaSQL);
        }
        public bool FinalizarAlquiler(int IdAlquiler)
        {
            string ConsultaSQL = "UPDATE " +
            "alquiler_vehiculos.alquiler " +
            "SET " +
            "alquiler_vehiculos.alquiler.Estado_Alquiler = 2 " +
            "WHERE " +
            "(alquiler_vehiculos.alquiler.Id_Alquiler = (" + IdAlquiler + "))";
            return ModeloConexion.ExecuteNonQuerySentence(ConsultaSQL);
        }
        public bool CancelarAlquiler(int IdAlquiler)
        {
            string ConsultaSQL = "UPDATE " +
            "alquiler_vehiculos.alquiler " +
            "SET " +
            "alquiler_vehiculos.alquiler.Estado_Alquiler = 4 " +
            "WHERE " +
            "(alquiler_vehiculos.alquiler.Id_Alquiler = (" + IdAlquiler + "))";
            return ModeloConexion.ExecuteNonQuerySentence(ConsultaSQL);
        }
        public bool PagarAlquiler(int IdAlquiler)
        {
            string ConsultaSQL = "UPDATE " +
            "alquiler_vehiculos.alquiler " +
            "SET " +
            "alquiler_vehiculos.alquiler.EstadoPago_Alquiler = 1 " +
            "WHERE " +
            "(alquiler_vehiculos.alquiler.Id_Alquiler = (" + IdAlquiler + "))";
            return ModeloConexion.ExecuteNonQuerySentence(ConsultaSQL);
        }
        public bool CalificarAlquiler(int IdAlquiler, string Calificacion, string Comentario)
        {
            if (Comentario == null)
            {
                Comentario = "Sin Comentarios";
            }
            string ConsultaSQL = "UPDATE " +
            "alquiler_vehiculos.alquiler " +
            "SET " +
            "alquiler_vehiculos.alquiler.Calificacion_Alquiler = " + Calificacion + ", " +
            "alquiler_vehiculos.alquiler.ComentarioCalificacion_Alquiler = '" + Comentario + "' " +
            "WHERE " +
            "(alquiler_vehiculos.alquiler.Id_Alquiler = (" + IdAlquiler + "))";
            return ModeloConexion.ExecuteNonQuerySentence(ConsultaSQL);
        }
        public void ReportarNuevoAlquilerPorCorreo(ModeloVehiculo Vehiculo, ModeloUsuario Usuario, string FechaInicio, string FechaFin, float Precio)
        {
            string Asunto = "Nuevo Alquiler de " + Vehiculo.TipoVehiculo + " " + Vehiculo.Marca + " " + Vehiculo.Linea;
            string Mensaje = "Hola Sr(a) " + Vehiculo.UsuarioPropietario.Nombre + " " + Vehiculo.UsuarioPropietario.Apellido + ".\n" +
            "Le informamos que su vehículo " + Vehiculo.TipoVehiculo + " " + Vehiculo.Marca + " " + Vehiculo.Linea + " " +
            "identificado con la placa " + Vehiculo.Placa + " fue solicitado en alquiler por " + Usuario.Nombre + " " + Usuario.Apellido + " " +
            "desde la fecha de " + FechaInicio + " hasta la fecha de " + FechaFin + " por un costo de " + Precio + ".\n" +
            "Por favor ponerse en contacto con el Sr(a). " + Usuario.Nombre + " " + Usuario.Apellido + ". Sus datos de contacto son:\n" +
            "Teléfono: " + Usuario.Telefono + "\nCorreo: " + Usuario.Correo + "\nGracias por usar nuestra plataforma.";
            SmtpClient ClienteAMTP = new("smtp-mail.outlook.com", 587)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential("mariog.101200@hotmail.com", "M@rio112358")
            };
            MailMessage MensajeCorreo = new("mariog.101200@hotmail.com", Vehiculo.UsuarioPropietario.Correo, Asunto, Mensaje.ToUpper());
            ClienteAMTP.Send(MensajeCorreo);
        }
        public bool RecalcularPrecioAlquiler(int IdAlquiler, int DiasRetraso)
        {
            ModeloAlquiler Alquiler = new();
            Alquiler = Alquiler.TraerAlquiler(IdAlquiler);
            float PrecioAntiguo = Alquiler.Precio;
            float PrecioAlquilerVehiculo = Alquiler.Vehiculo.PrecioAlquilerDia;
            float AumentoPrecio = PrecioAlquilerVehiculo * DiasRetraso;
            float NuevoPrecio = PrecioAntiguo + AumentoPrecio;
            float NuevaGanacia = (float)(NuevoPrecio * 0.15);
            string ConsultaSQL = "UPDATE " +
            "alquiler_vehiculos.alquiler " +
            "SET " +
            "alquiler_vehiculos.alquiler.Precio_Alquiler = " + NuevoPrecio + ", " +
            "alquiler_vehiculos.alquiler.Ganancias_Alquiler = " + NuevaGanacia + " " +
            "WHERE " +
            "(alquiler_vehiculos.alquiler.Id_Alquiler = (" + IdAlquiler + "))";
            return ModeloConexion.ExecuteNonQuerySentence(ConsultaSQL); ;
        }
    }
}