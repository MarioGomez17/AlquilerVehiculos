using MySql.Data.MySqlClient;
namespace ALQUILER_VEHICULOS.Models
{
    public class ModeloModelo
    {
        public int Id { get; set; }
        public int Modelo { get; set; }
        public List<ModeloModelo> TraerModelos()
        {
            List<ModeloModelo> Modelos = [];
            string ConsultaSQL = "SELECT " +
                "alquiler_vehiculos.modelo.Id_Modelo, " +
                "alquiler_vehiculos.modelo.Valor_Modelo " +
                "FROM alquiler_vehiculos.modelo " +
                "ORDER BY alquiler_vehiculos.modelo.Valor_Modelo ASC";
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
                        ModeloModelo Modelo = new()
                        {
                            Id = Lector.GetInt32(0),
                            Modelo = Lector.GetInt32(1)
                        };
                        Modelos.Add(Modelo);
                    }
                }
            }
            catch (Exception) { }
            finally
            {
                ConexionBD.Close();
            }
            return Modelos;
        }
        public bool ActualizarModelo(int Id, int Modelo)
        {
            string ConsultaSQL = "UPDATE " +
            "alquiler_vehiculos.modelo " +
            "SET " +
            "alquiler_vehiculos.modelo.Valor_Modelo = " +
            Modelo + " " +
            "WHERE (alquiler_vehiculos.modelo.Id_Modelo = " + Id + ")";
            return ModeloConexion.ExecuteNonQuerySentence(ConsultaSQL);
        }
        public bool AgregarModelo(int Modelo)
        {
            string ConsultaSQL = "INSERT INTO " +
            "alquiler_vehiculos.modelo (" +
            "alquiler_vehiculos.modelo.Valor_Modelo) " +
            "VALUES (" +
            Modelo + ") ";
            return ModeloConexion.ExecuteNonQuerySentence(ConsultaSQL);
        }
    }
}