using MySql.Data.MySqlClient;

namespace ALQUILER_VEHICULOS.Models
{
    public class ModeloPropietario
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public ModeloUsuario Usuario { get; set; }
        public ModeloPropietario TraerPropietario(int Id_Usuario)
        {
            ModeloPropietario ModeloPropietario = null;
            ModeloUsuario ModeloUsuario = new();
            string ConsultaSQL = "SELECT " +
                    "alquiler_vehiculos.propietario.Id_Propietario, " +
                    "alquiler_vehiculos.propietario.Codigo_Propietario," +
                    "alquiler_vehiculos.propietario.Usuario_Propietario " +
                    "FROM alquiler_vehiculos.propietario " +
                    "WHERE alquiler_vehiculos.propietario.Usuario_Propietario = " + Id_Usuario;
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
                        ModeloPropietario = new()
                        {
                            Id = Lector.GetInt32(0),
                            Codigo = Lector.GetString(1),
                            Usuario = ModeloUsuario.TraerUsuario(Id_Usuario)
                        };
                    }
                }
            }
            catch (Exception) { }
            finally
            {
                ConexionBD.Close();
            }
            return ModeloPropietario;
        }
        public bool CrearPropietario(int Id_Usuario)
        {
            ModeloUsuario ModeloUsuario = new();
            ModeloUsuario = ModeloUsuario.TraerUsuario(Id_Usuario);
            string CodigoPropietario = "Propietario" + ModeloUsuario.Nombre + ModeloUsuario.NumeroIdentificacion;
            string ConsultaSQL = "INSERT INTO " +
            "alquiler_vehiculos.propietario " +
            "(alquiler_vehiculos.propietario.Codigo_Propietario, " +
            "alquiler_vehiculos.propietario.Usuario_Propietario) " +
            "VALUES " +
            "('" + CodigoPropietario + "', " +
            ModeloUsuario.Id + ")";
            return ModeloConexion.ExecuteNonQuerySentence(ConsultaSQL);
        }
        public bool ValidarPropietario(int Id_Usuario)
        {
            return TraerPropietario(Id_Usuario) == null;
        }
        public bool AgregarAlquilerHistorialPropietario(int IdPropietario, int IdAlquiler)
        {
            string ConsultaSQL = "INSERT INTO " +
                                "alquiler_vehiculos.historial_alquileres_propietario ( " +
                                "alquiler_vehiculos.historial_alquileres_propietario.Propietario_HistorialAlquileresPropietario, " +
                                "alquiler_vehiculos.historial_alquileres_propietario.Alquiler_HistorialAlquileresPropietario) " +
                                "VALUES ( " +
                                IdPropietario + ", " +
                                IdAlquiler + ")";
            return ModeloConexion.ExecuteNonQuerySentence(ConsultaSQL);
        }
    }
}