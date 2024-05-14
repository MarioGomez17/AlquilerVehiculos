using MySql.Data.MySqlClient;
namespace ALQUILER_VEHICULOS.Models
{
    public class ModeloColor
    {
        public int Id { get; set; }
        public string Color { get; set; }
        public List<ModeloColor> TraerColores()
        {
            List<ModeloColor> Colores = [];
            string ConsultaSQL = "SELECT " +
                "alquiler_vehiculos.color.Id_Color, " +
                "alquiler_vehiculos.color.Nombre_Color " +
                "FROM alquiler_vehiculos.color " +
                "ORDER BY alquiler_vehiculos.color.Nombre_Color ASC";
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
                        ModeloColor Color = new()
                        {
                            Id = Lector.GetInt32(0),
                            Color = Lector.GetString(1)
                        };
                        Colores.Add(Color);
                    }
                }
            }
            catch (Exception) { }
            finally
            {
                ConexionBD.Close();
            }
            return Colores;
        }
        public bool ActualizarColor(int Id, string Color)
        {
            string ConsultaSQL = "UPDATE " +
            "alquiler_vehiculos.color " +
            "SET " +
            "alquiler_vehiculos.color.Nombre_Color = '" +
            Color + "' " +
            "WHERE (alquiler_vehiculos.color.Id_Color = " + Id + ")";
            return ModeloConexion.ExecuteNonQuerySentence(ConsultaSQL);
        }
        public bool AgregarColor(string Color)
        {
            string ConsultaSQL = "INSERT INTO " +
            "alquiler_vehiculos.color (" +
            "alquiler_vehiculos.color.Nombre_Color) " +
            "VALUES ('" +
            Color + "') ";
            return ModeloConexion.ExecuteNonQuerySentence(ConsultaSQL);
        }
    }
}