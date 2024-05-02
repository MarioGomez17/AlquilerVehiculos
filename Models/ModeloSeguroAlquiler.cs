using MySql.Data.MySqlClient;

namespace ALQUILER_VEHICULOS.Models
{
    public class ModeloSeguroAlquiler
    {
        public int Id { get; set; }
        public string NombreSeguroAlquiler { get; set; }
        public float PrecioSeguroAlquiler { get; set; }
        public List<ModeloSeguroAlquiler> TraerTodosSegurosAlquiler()
        {
            List<ModeloSeguroAlquiler> SegurosAlquiler = [];
            string ConsultaSQL = "SELECT " +
                "alquiler_vehiculos.seguro_alquiler.Id_SeguroAlquiler, " +
                "alquiler_vehiculos.seguro_alquiler.Nombre_SeguroAlquiler, " +
                "alquiler_vehiculos.seguro_alquiler.Precio_SeguroAlquiler " +
                "FROM alquiler_vehiculos.seguro_alquiler " +
                "ORDER BY alquiler_vehiculos.seguro_alquiler.Id_SeguroAlquiler ASC";
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
                        ModeloSeguroAlquiler SeguroAlquiler = new()
                        {
                            Id = Lector.GetInt32(0),
                            NombreSeguroAlquiler = Lector.GetString(1),
                            PrecioSeguroAlquiler = Lector.GetFloat(2)
                        };
                        SegurosAlquiler.Add(SeguroAlquiler);
                    }
                }
            }
            catch (Exception) { }
            finally
            {
                ConexionBD.Close();
            }
            return SegurosAlquiler;
        }
        public ModeloSeguroAlquiler TraerSeguroAlquiler(int IdSeguro)
        {
            ModeloSeguroAlquiler SeguroAlquiler = null;
            string ConsultaSQL = "SELECT " +
                "alquiler_vehiculos.seguro_alquiler.Id_SeguroAlquiler, " +
                "alquiler_vehiculos.seguro_alquiler.Nombre_SeguroAlquiler, " +
                "alquiler_vehiculos.seguro_alquiler.Precio_SeguroAlquiler " +
                "FROM alquiler_vehiculos.seguro_alquiler " +
                "WHERE alquiler_vehiculos.seguro_alquiler.Id_SeguroAlquiler = " + IdSeguro + " " +
                "ORDER BY alquiler_vehiculos.seguro_alquiler.Id_SeguroAlquiler ASC";
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
                        SeguroAlquiler = new()
                        {
                            Id = Lector.GetInt32(0),
                            NombreSeguroAlquiler = Lector.GetString(1),
                            PrecioSeguroAlquiler = Lector.GetFloat(2)
                        };
                    }
                }
            }
            catch (Exception) { }
            finally
            {
                ConexionBD.Close();
            }
            return SeguroAlquiler;
        }
    }
}
