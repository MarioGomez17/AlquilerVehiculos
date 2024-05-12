using MySql.Data.MySqlClient;

namespace ALQUILER_VEHICULOS.Models
{
    public class ModeloColor
    {
        public int Id { get; set; }
        public string Color { get; set; }
        public List<ModeloColor> TraerColores(){
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
    }
}