using MySql.Data.MySqlClient;

namespace ALQUILER_VEHICULOS.Models
{
    public class ModeloMarca
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int TipoVehiculo {get; set;}
        public List<ModeloMarca> TraerTodosMetodasMarcas()
        {
            List<ModeloMarca> MarcasVehiculo = [];
            string ConsultaSQL = "SELECT " +
                "alquiler_vehiculos.marca_vehiculo.Id_MarcaVehiculo, " +
                "alquiler_vehiculos.marca_vehiculo.Nombre_MarcaVehiculo, " +
                "alquiler_vehiculos.marca_vehiculo.TipoVehiculo_MarcaVehiculo " + 
                "FROM alquiler_vehiculos.marca_vehiculo " +
                "ORDER BY alquiler_vehiculos.marca_vehiculo.Id_MarcaVehiculo ASC";
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
                        ModeloMarca MarcaVehiculo = new()
                        {
                            Id = Lector.GetInt32(0),
                            Nombre = Lector.GetString(1),
                            TipoVehiculo = Lector.GetInt32(2)
                        };
                        MarcasVehiculo.Add(MarcaVehiculo);
                    }
                }
            }
            catch (Exception) { }
            finally
            {
                ConexionBD.Close();
            }
            return MarcasVehiculo;
        }
    }
}
