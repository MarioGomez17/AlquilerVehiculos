using MySql.Data.MySqlClient;

namespace ALQUILER_VEHICULOS.Models
{
    public class ModeloTipoCombustible
    {
        public int Id {get; set;}
        public string Nombre {get; set;}
        public List<ModeloTipoCombustible> TraerTodosTiposComustible()
        {
            List<ModeloTipoCombustible> TiposCombustible = [];
            string ConsultaSQL = "SELECT " +
                                "alquiler_vehiculos.tipo_combustible.Id_TipoCombustible, " +
                                "alquiler_vehiculos.tipo_combustible.Nombre_TipoCombustible " +
                                "FROM alquiler_vehiculos.tipo_combustible " +
                                "ORDER BY alquiler_vehiculos.tipo_combustible.Id_TipoCombustible ASC";
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
                        ModeloTipoCombustible TipoCombustible = new()
                        {
                            Id = Lector.GetInt32(0),
                            Nombre = Lector.GetString(1),
                        };
                        TiposCombustible.Add(TipoCombustible);
                    }
                }
            }
            catch (Exception) { }
            finally
            {
                ConexionBD.Close();
            }
            return TiposCombustible;
        }
    }
}