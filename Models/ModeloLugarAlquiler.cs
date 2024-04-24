using MySql.Data.MySqlClient;

namespace ALQUILER_VEHICULOS.Models
{
    public class ModeloLugarAlquiler
    {
        public int Id { get; set; }
        public string Nombre { get; set; }

        public List<ModeloLugarAlquiler> TraerTodosLugaresAlquiler()
        {
            List<ModeloLugarAlquiler> LugaresAlquiler = [];
            string SQLQuery = "SELECT " +
                "alquiler_vehiculos.lugar_alquiler.Id_LugarAlquiler, " +
                "alquiler_vehiculos.lugar_alquiler.Nombre_LugarAlquiler " +
                "FROM alquiler_vehiculos.lugar_alquiler " +
                "ORDER BY alquiler_vehiculos.lugar_alquiler.Id_LugarAlquiler ASC";
            MySqlConnection ConexionBD = ModeloConexion.Conect();
            try
            {
                ConexionBD.Open();
                MySqlCommand Comando = new(SQLQuery, ConexionBD);
                MySqlDataReader Lector = Comando.ExecuteReader();
                if (Lector.HasRows)
                {
                    while (Lector.Read())
                    {
                        ModeloLugarAlquiler LugarAlquiler = new()
                        {
                            Id = Lector.GetInt32(0),
                            Nombre = Lector.GetString(1),

                        };
                        LugaresAlquiler.Add(LugarAlquiler);
                    }
                }
            }
            catch (Exception) { }
            finally
            {
                ConexionBD.Close();
            }
            return LugaresAlquiler;
        }
    }
}
