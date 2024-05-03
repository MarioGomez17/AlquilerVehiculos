using MySql.Data.MySqlClient;

namespace ALQUILER_VEHICULOS.Models
{
    public class ModeloTipoVehiculo
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public List<ModeloTipoVehiculo> TraerTodosTipoVehiculo()
        {
            List<ModeloTipoVehiculo> TiposVehiculo = [];
            string ConsultaSQL = "SELECT " +
                "alquiler_vehiculos.tipo_vehiculo.Id_TipoVehiculo, " +
                "alquiler_vehiculos.tipo_vehiculo.Nombre_TipoVehiculo " +
                "FROM alquiler_vehiculos.tipo_vehiculo " +
                "ORDER BY alquiler_vehiculos.tipo_vehiculo.Id_TipoVehiculo ASC";
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
                        ModeloTipoVehiculo TipoVehiculo = new()
                        {
                            Id = Lector.GetInt32(0),
                            Nombre = Lector.GetString(1),
                        };
                        TiposVehiculo.Add(TipoVehiculo);
                    }
                }
            }
            catch (Exception) { }
            finally
            {
                ConexionBD.Close();
            }
            return TiposVehiculo;
        }
    }
}