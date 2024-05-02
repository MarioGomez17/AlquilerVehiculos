using MySql.Data.MySqlClient;

namespace ALQUILER_VEHICULOS.Models
{
    public class ModeloLinea
    {
        public int Id {get; set;}
        public string Nombre {get; set;}
        public int Marca {get; set;}
        public List<ModeloLinea> TraerTodasLineas()
        {
            List<ModeloLinea> Lineas = [];
            string ConsultaSQL = "SELECT " +
                                "alquiler_vehiculos.linea_vehiculo.Id_LineaVehiculo, " +
                                "alquiler_vehiculos.linea_vehiculo.Nombre_LineaVehiculo, " +
                                "alquiler_vehiculos.linea_vehiculo.MarcaVehiculo_LineaVehiculo " +
                                "FROM alquiler_vehiculos.linea_vehiculo " +
                                "ORDER BY alquiler_vehiculos.linea_vehiculo.Id_LineaVehiculo ASC";
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
                        ModeloLinea Linea = new()
                        {
                            Id = Lector.GetInt32(0),
                            Nombre = Lector.GetString(1),
                            Marca = Lector.GetInt32(2)
                        };
                        Lineas.Add(Linea);
                    }
                }
            }
            catch (Exception) { }
            finally
            {
                ConexionBD.Close();
            }
            return Lineas;
        }
        public List<ModeloLinea> TraerTodasLineasPorMarca(int IdMarca)
        {
            List<ModeloLinea> Lineas = [];
            string ConsultaSQL = "SELECT " +
                                "alquiler_vehiculos.linea_vehiculo.Id_LineaVehiculo, " +
                                "alquiler_vehiculos.linea_vehiculo.Nombre_LineaVehiculo, " +
                                "alquiler_vehiculos.linea_vehiculo.MarcaVehiculo_LineaVehiculo " +
                                "FROM alquiler_vehiculos.linea_vehiculo " +
                                "WHERE alquiler_vehiculos.linea_vehiculo.MarcaVehiculo_LineaVehiculo = " + IdMarca + " " + 
                                "ORDER BY alquiler_vehiculos.linea_vehiculo.Id_LineaVehiculo ASC";
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
                        ModeloLinea Linea = new()
                        {
                            Id = Lector.GetInt32(0),
                            Nombre = Lector.GetString(1),
                            Marca = Lector.GetInt32(2)
                        };
                        Lineas.Add(Linea);
                    }
                }
            }
            catch (Exception) { }
            finally
            {
                ConexionBD.Close();
            }
            return Lineas;
        }
    }
}