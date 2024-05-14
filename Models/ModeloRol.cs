using MySql.Data.MySqlClient;
namespace ALQUILER_VEHICULOS.Models
{
    public class ModeloRol
    {
        public int Id { get; set; }
        public string NombreRol { get; set; }
        public List<ModeloRol> TraerRoles()
        {
            List<ModeloRol> Roles = [];
            string ConsultaSQL = "SELECT " +
                "alquiler_vehiculos.rol.Id_Rol, " +
                "alquiler_vehiculos.rol.Nombre_Rol " +
                "FROM alquiler_vehiculos.rol " +
                "ORDER BY alquiler_vehiculos.rol.Nombre_Rol ASC";
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
                        ModeloRol Rol = new()
                        {
                            Id = Lector.GetInt32(0),
                            NombreRol = Lector.GetString(1)
                        };
                        Roles.Add(Rol);
                    }
                }
            }
            catch (Exception) { }
            finally
            {
                ConexionBD.Close();
            }
            return Roles;
        }
    }
}