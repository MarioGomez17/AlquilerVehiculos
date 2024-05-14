using MySql.Data.MySqlClient;
namespace ALQUILER_VEHICULOS.Models
{
    public class ModeloCantidadPasajeros
    {
        public int Id { get; set; }
        public int CantidadPasajeros { get; set; }
        public List<ModeloCantidadPasajeros> TraerCantidadesPasajeros()
        {
            List<ModeloCantidadPasajeros> CantidadesPasajeros = [];
            string ConsultaSQL = "SELECT " +
                "alquiler_vehiculos.cantidad_pasajeros.Id_CantidadPasajeros, " +
                "alquiler_vehiculos.cantidad_pasajeros.Valor_CantidadPasajeros " +
                "FROM alquiler_vehiculos.cantidad_pasajeros " +
                "ORDER BY alquiler_vehiculos.cantidad_pasajeros.Valor_CantidadPasajeros ASC";
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
                        ModeloCantidadPasajeros CantidadPasajeros = new()
                        {
                            Id = Lector.GetInt32(0),
                            CantidadPasajeros = Lector.GetInt32(1)
                        };
                        CantidadesPasajeros.Add(CantidadPasajeros);
                    }
                }
            }
            catch (Exception) { }
            finally
            {
                ConexionBD.Close();
            }
            return CantidadesPasajeros;
        }
        public bool ActualizarCantidadPasajeros(int Id, int CantidadPasajeros)
        {
            string ConsultaSQL = "UPDATE " +
            "alquiler_vehiculos.cantidad_pasajeros " +
            "SET " +
            "alquiler_vehiculos.cantidad_pasajeros.Valor_CantidadPasajeros = " +
            CantidadPasajeros + " " +
            "WHERE (alquiler_vehiculos.cantidad_pasajeros.Id_CantidadPasajeros = " + Id + ")";
            return ModeloConexion.ExecuteNonQuerySentence(ConsultaSQL);
        }
        public bool AgregarCantidadPasajeros(int CantidadPasajeros)
        {
            string ConsultaSQL = "INSERT INTO " +
            "alquiler_vehiculos.cantidad_pasajeros (" +
            "alquiler_vehiculos.cantidad_pasajeros.Valor_CantidadPasajeros) " +
            "VALUES (" +
            CantidadPasajeros + ") ";
            return ModeloConexion.ExecuteNonQuerySentence(ConsultaSQL);
        }
    }
}