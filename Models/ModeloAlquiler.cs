using MySql.Data.MySqlClient;

namespace ALQUILER_VEHICULOS.Models
{
    public class ModeloAlquiler
    {
        public int Id { get; set; }
        public DateTime FechaIncio { get; set; }
        public DateTime FechaFin { get; set; }
        public float Precio { get; set; }
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
        public bool CrearAquiler(DateTime FechaInicio, DateTime FechaFin, float Precio, int Lavada, int Alquilador, int Vehiculo, int Lugar, int MetodoPago, int Seguro)
        {
            string ConsultaSQL = "INSERT INTO " +
                "alquiler_vehiculos.alquiler (" +
                "FechaIncio_Alquiler, " +
                "FechaFin_Alquiler, " +
                "Precio_Alquiler, " +
                "LavadaVehiculo_Alquiler, " +
                "Alquilador_Alquiler, " +
                "Vehiculo_Alquiler, " +
                "Lugar_Alquiler, " +
                "MeotodoPago_Alquiler, " +
                "Seguro_Alquiler, " +
                " Estado_Alquiler) " +
                "VALUES ('" +
                FechaInicio.ToString("yyyy-MM-dd HH:mm:ss") + "', " +
                "'" + FechaFin.ToString("yyyy-MM-dd HH:mm:ss") + "', " +
                Precio + ", " +
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
            ModeloAlquilador.AgregarAlquilerHistorialAlquilador(Alquilador, IdAlquiler);
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
                            FechaIncio = Lector.GetDateTime(1),
                            FechaFin = Lector.GetDateTime(2),
                            Precio = Lector.GetFloat(3),
                            Lavada = Lector.GetInt32(4) == 1,
                            Alquilador = ModeloAlquilador.TraerAlquilador(Lector.GetInt32(5)),
                            Vehiculo = ModeloVehiculo.TraerVehiculo(Lector.GetInt32(6)),
                            Lugar = Lector.GetString(7),
                            MetodoPago = Lector.GetString(8),
                            EstadoPago = Lector.GetString(9),
                            Seguro = Lector.GetString(10),
                            PrecioSeguro = Lector.GetFloat(11),
                            Calificacion = Lector.GetFloat(12),
                            ComentarioCalificacion = Lector.GetString(13),
                            Estado = Lector.GetString(14),
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
                            FechaIncio = Lector.GetDateTime(1),
                            FechaFin = Lector.GetDateTime(2),
                            Precio = Lector.GetFloat(3),
                            Lavada = Lector.GetInt32(4) == 1,
                            Alquilador = ModeloAlquilador.TraerAlquilador(Lector.GetInt32(5)),
                            Vehiculo = ModeloVehiculo.TraerVehiculo(Lector.GetInt32(6)),
                            Lugar = Lector.GetString(7),
                            MetodoPago = Lector.GetString(8),
                            EstadoPago = Lector.GetString(9),
                            Seguro = Lector.GetString(10),
                            PrecioSeguro = Lector.GetFloat(11),
                            Calificacion = Lector.GetFloat(12),
                            ComentarioCalificacion = Lector.GetString(13),
                            Estado = Lector.GetString(14),
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
    }
}