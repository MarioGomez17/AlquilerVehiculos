using MySql.Data.MySqlClient;
namespace ALQUILER_VEHICULOS.Models
{
    public class ModeloCiudad
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public List<ModeloCiudad> TraerTodasCiudades()
        {
            List<ModeloCiudad> Ciudades = [];
            string ConsultaSQL = "SELECT " +
                "alquiler_vehiculos.ciudad.Id_Ciudad, " +
                "alquiler_vehiculos.ciudad.Nombre_Ciudad, " +
                "alquiler_vehiculos.departamento.Nombre_Departamento " +
                "FROM alquiler_vehiculos.ciudad " +
                "INNER JOIN alquiler_vehiculos.departamento " +
                "ON alquiler_vehiculos.departamento.Id_Departamento = alquiler_vehiculos.ciudad.Departamento_Ciudad " +
                "ORDER BY alquiler_vehiculos.ciudad.Nombre_Ciudad ASC";
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
                        ModeloCiudad Ciudad = new()
                        {
                            Id = Lector.GetInt32(0),
                            Nombre = Lector.GetString(1) + " - " + Lector.GetString(2)
                        };
                        Ciudades.Add(Ciudad);
                    }
                }
            }
            catch (Exception) { }
            finally
            {
                ConexionBD.Close();
            }
            return Ciudades;
        }
        public List<ModeloCiudad> TraerTodasCiudadesDepartamento(int IdDepartamento)
        {
            List<ModeloCiudad> Ciudades = [];
            string ConsultaSQL = "SELECT " +
                "alquiler_vehiculos.ciudad.Id_Ciudad, " +
                "alquiler_vehiculos.ciudad.Nombre_Ciudad " +
                "FROM alquiler_vehiculos.ciudad " +
                "WHERE alquiler_vehiculos.ciudad.Departamento_Ciudad = " + IdDepartamento + " " +
                "ORDER BY alquiler_vehiculos.ciudad.Nombre_Ciudad ASC";
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
                        ModeloCiudad Ciudad = new()
                        {
                            Id = Lector.GetInt32(0),
                            Nombre = Lector.GetString(1)
                        };
                        Ciudades.Add(Ciudad);
                    }
                }
            }
            catch (Exception e) { Console.WriteLine(e); }
            finally
            {
                ConexionBD.Close();
            }
            return Ciudades;
        }
        public bool ActualizarCiudad(int Id, string Ciudad)
        {
            string ConsultaSQL = "UPDATE " +
            "alquiler_vehiculos.ciudad " +
            "SET " +
            "alquiler_vehiculos.ciudad.Nombre_Ciudad = '" +
            Ciudad + "' " +
            "WHERE (alquiler_vehiculos.ciudad.Id_Ciudad = " + Id + ")";
            return ModeloConexion.ExecuteNonQuerySentence(ConsultaSQL);
        }
        public bool AgregarCiudad(int Departamento, string Ciudad)
        {
            string ConsultaSQL = "INSERT INTO " +
            "alquiler_vehiculos.ciudad (" +
            "alquiler_vehiculos.ciudad.Departamento_Ciudad, " +
            "alquiler_vehiculos.ciudad.Nombre_Ciudad) " +
            "VALUES (" +
            Departamento + ", '" +
            Ciudad + "') ";
            return ModeloConexion.ExecuteNonQuerySentence(ConsultaSQL);
        }
    }
}