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
                                "ORDER BY alquiler_vehiculos.tipo_combustible.Nombre_TipoCombustible ASC";
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
        public bool ActualizarTipoCombustible(int Id, string TipoCombustible){
            string ConsultaSQL = "UPDATE " + 
            "alquiler_vehiculos.tipo_combustible " + 
            "SET " + 
            "alquiler_vehiculos.tipo_combustible.Nombre_TipoCombustible = '" + 
            TipoCombustible + "' " + 
            "WHERE (alquiler_vehiculos.tipo_combustible.Id_TipoCombustible = " + Id + ")";
            return ModeloConexion.ExecuteNonQuerySentence(ConsultaSQL);
        }
        public bool AgregarTipoCombustible(string TipoCombustible){
            string ConsultaSQL = "INSERT INTO " + 
            "alquiler_vehiculos.tipo_combustible (" + 
            "alquiler_vehiculos.tipo_combustible.Nombre_TipoCombustible) " + 
            "VALUES ('" + 
            TipoCombustible + "') ";
            return ModeloConexion.ExecuteNonQuerySentence(ConsultaSQL);
        }
    }
}