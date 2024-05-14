using MySql.Data.MySqlClient;
namespace ALQUILER_VEHICULOS.Models
{
    public class ModeloClasificacionVehículo
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int TipoVehiculo { get; set; }
        public List<ModeloClasificacionVehículo> TraerTodasClasificaciones()
        {
            List<ModeloClasificacionVehículo> ClasificacionesVehículo = [];
            string ConsultaSQL = "SELECT " +
                                "alquiler_vehiculos.clasificacion_vehiculo.Id_ClasificacionVehiculo, " +
                                "alquiler_vehiculos.clasificacion_vehiculo.Nombre_ClasificacionVehiculo, " +
                                "alquiler_vehiculos.clasificacion_vehiculo.TipoVehiculo_ClasificacionVehiculo " +
                                "FROM alquiler_vehiculos.clasificacion_vehiculo " +
                                "ORDER BY alquiler_vehiculos.clasificacion_vehiculo.Nombre_ClasificacionVehiculo ASC";
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
                        ModeloClasificacionVehículo ClasificacionVehículo = new()
                        {
                            Id = Lector.GetInt32(0),
                            Nombre = Lector.GetString(1),
                            TipoVehiculo = Lector.GetInt32(2)
                        };
                        ClasificacionesVehículo.Add(ClasificacionVehículo);
                    }
                }
            }
            catch (Exception) { }
            finally
            {
                ConexionBD.Close();
            }
            return ClasificacionesVehículo;
        }
        public List<ModeloClasificacionVehículo> TraerTodasClasificacionesPorTipo(int IdTipoVehiculo)
        {
            List<ModeloClasificacionVehículo> ClasificacionesVehículo = [];
            string ConsultaSQL = "SELECT " +
                                "alquiler_vehiculos.clasificacion_vehiculo.Id_ClasificacionVehiculo, " +
                                "alquiler_vehiculos.clasificacion_vehiculo.Nombre_ClasificacionVehiculo, " +
                                "alquiler_vehiculos.clasificacion_vehiculo.TipoVehiculo_ClasificacionVehiculo " +
                                "FROM alquiler_vehiculos.clasificacion_vehiculo " +
                                "WHERE alquiler_vehiculos.clasificacion_vehiculo.TipoVehiculo_ClasificacionVehiculo = " + IdTipoVehiculo + " " +
                                "ORDER BY alquiler_vehiculos.clasificacion_vehiculo.Nombre_ClasificacionVehiculo ASC";
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
                        ModeloClasificacionVehículo ClasificacionVehículo = new()
                        {
                            Id = Lector.GetInt32(0),
                            Nombre = Lector.GetString(1),
                            TipoVehiculo = Lector.GetInt32(2)
                        };
                        ClasificacionesVehículo.Add(ClasificacionVehículo);
                    }
                }
            }
            catch (Exception) { }
            finally
            {
                ConexionBD.Close();
            }
            return ClasificacionesVehículo;
        }
        public bool ActualizarClasificacion(int Id, string Clasificacion)
        {
            string ConsultaSQL = "UPDATE " +
            "alquiler_vehiculos.clasificacion_vehiculo " +
            "SET " +
            "alquiler_vehiculos.clasificacion_vehiculo.Nombre_ClasificacionVehiculo = '" +
            Clasificacion + "' " +
            "WHERE (alquiler_vehiculos.clasificacion_vehiculo.Id_ClasificacionVehiculo = " + Id + ")";
            return ModeloConexion.ExecuteNonQuerySentence(ConsultaSQL);
        }
        public bool AgregarClasificacion(int Tipo, string Clasificacion)
        {
            string ConsultaSQL = "INSERT INTO " +
            "alquiler_vehiculos.clasificacion_vehiculo (" +
            "alquiler_vehiculos.clasificacion_vehiculo.TipoVehiculo_ClasificacionVehiculo, " +
            "alquiler_vehiculos.clasificacion_vehiculo.Nombre_ClasificacionVehiculo) " +
            "VALUES (" +
            Tipo + ", '" +
            Clasificacion + "') ";
            return ModeloConexion.ExecuteNonQuerySentence(ConsultaSQL);
        }
    }
}