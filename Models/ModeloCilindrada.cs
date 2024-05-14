using MySql.Data.MySqlClient;
namespace ALQUILER_VEHICULOS.Models
{
    public class ModeloCilindrada
    {
        public int Id { get; set; }
        public int Cilindrada { get; set; }
        public List<ModeloCilindrada> TraerCilindradas()
        {
            List<ModeloCilindrada> Cilindradas = [];
            string ConsultaSQL = "SELECT " +
                "alquiler_vehiculos.cilindrada.Id_Cilindrada, " +
                "alquiler_vehiculos.cilindrada.Valor_Cilindrada " +
                "FROM alquiler_vehiculos.cilindrada " +
                "ORDER BY alquiler_vehiculos.cilindrada.Valor_Cilindrada ASC";
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
                        ModeloCilindrada Cilindrada = new()
                        {
                            Id = Lector.GetInt32(0),
                            Cilindrada = Lector.GetInt32(1)
                        };
                        Cilindradas.Add(Cilindrada);
                    }
                }
            }
            catch (Exception) { }
            finally
            {
                ConexionBD.Close();
            }
            return Cilindradas;
        }
        public bool ActualizarCilindrada(int Id, int Cilindrada)
        {
            string ConsultaSQL = "UPDATE " +
            "alquiler_vehiculos.cilindrada " +
            "SET " +
            "alquiler_vehiculos.cilindrada.Valor_Cilindrada = " +
            Cilindrada + " " +
            "WHERE (alquiler_vehiculos.cilindrada.Id_Cilindrada = " + Id + ")";
            return ModeloConexion.ExecuteNonQuerySentence(ConsultaSQL);
        }
        public bool AgregarCilindrada(int Cilindrada)
        {
            string ConsultaSQL = "INSERT INTO " +
            "alquiler_vehiculos.cilindrada (" +
            "alquiler_vehiculos.cilindrada.Valor_Cilindrada) " +
            "VALUES (" +
            Cilindrada + ") ";
            return ModeloConexion.ExecuteNonQuerySentence(ConsultaSQL);
        }
    }
}