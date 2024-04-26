using MySql.Data.MySqlClient;

namespace ALQUILER_VEHICULOS.Models
{
    public class ModeloMetodoPagoAlquiler
    {
        public int Id { get; set; }
        public string Nombre { get; set; }

        public List<ModeloMetodoPagoAlquiler> TraerTodosMetodosPagoAlquiler()
        {
            List<ModeloMetodoPagoAlquiler> MetodosPagoAlquiler = [];
            string ConsultaSQL = "SELECT " +
                "alquiler_vehiculos.metodo_pago.Id_MetodoPago, " +
                "alquiler_vehiculos.metodo_pago.Nombre_MetodoPago " +
                "FROM alquiler_vehiculos.metodo_pago " +
                "ORDER BY alquiler_vehiculos.metodo_pago.Id_MetodoPago ASC";
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
                        ModeloMetodoPagoAlquiler MetodoPagoAlquiler = new()
                        {
                            Id = Lector.GetInt32(0),
                            Nombre = Lector.GetString(1),
                        };
                        MetodosPagoAlquiler.Add(MetodoPagoAlquiler);
                    }
                }
            }
            catch (Exception) { }
            finally
            {
                ConexionBD.Close();
            }
            return MetodosPagoAlquiler;
        }
    }
}
