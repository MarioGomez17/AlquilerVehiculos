using MySql.Data.MySqlClient;

namespace ALQUILER_VEHICULOS.Models
{
    public class ModeloDepartamento
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public List<ModeloCiudad> Ciudades { get; set; }
        public List<ModeloDepartamento> TraerDepartamentos()
        {
            List<ModeloDepartamento> Departamentos = [];
            string ConsultaSQL = "SELECT " +
                "alquiler_vehiculos.departamento.Id_Departamento, " +
                "alquiler_vehiculos.departamento.Nombre_Departamento " +
                "FROM alquiler_vehiculos.departamento " +
                "ORDER BY alquiler_vehiculos.departamento.Id_Departamento ASC";
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
                        ModeloCiudad ModeloCiudad = new();
                        ModeloDepartamento Departamento = new()
                        {
                            Id = Lector.GetInt32(0),
                            Nombre = Lector.GetString(1),
                            Ciudades = ModeloCiudad.TraerTodasCiudadesDepartamento(Lector.GetInt32(0))
                        };
                        Departamentos.Add(Departamento);
                    }
                }
            }
            catch (Exception) { }
            finally
            {
                ConexionBD.Close();
            }
            return Departamentos;
        }
    }
}