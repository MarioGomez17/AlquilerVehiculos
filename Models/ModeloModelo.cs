using MySql.Data.MySqlClient;

namespace ALQUILER_VEHICULOS.Models
{
    public class ModeloModelo
    {
        public int Id { get; set; }
        public int Modelo { get; set; }
        public List<ModeloModelo> TraerModelos(){
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
    }
}