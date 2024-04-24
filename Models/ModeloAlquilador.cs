using MySql.Data.MySqlClient;

namespace ALQUILER_VEHICULOS.Models
{
    public class ModeloAlquilador
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public ModeloUsuario Usuario { get; set; }
        public List<ModeloAlquiler> HistorialAlquileres { get; set; }
        public ModeloAlquilador TraerAlquilador(int Id_Usuario)
        {
            ModeloAlquilador ModeloAlquilador = null;
            ModeloUsuario ModeloUsuario = new();
            string ConsultaSQL = "SELECT " +
                    "alquiler_vehiculos.alquilador.Id_Alquilador, " +
                    "alquiler_vehiculos.alquilador.Codigo_Alquilador," +
                    "alquiler_vehiculos.alquilador.Usuario_Alquilador " +
                    "FROM alquiler_vehiculos.alquilador " +
                    "WHERE alquiler_vehiculos.alquilador.Usuario_Alquilador = " + Id_Usuario;
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
                        ModeloAlquilador = new()
                        {
                            Id = Lector.GetInt32(0),
                            Codigo = Lector.GetString(1),
                            Usuario = ModeloUsuario.TraerUsuario(Id_Usuario)
                        };
                    }
                }
            }
            catch (Exception) {}
            finally
            {
                ConexionBD.Close();
            }
            return ModeloAlquilador;
        }
        public bool CrearAlquilador(int Id_Usuario){
            ModeloUsuario ModeloUsuario = new();
            ModeloUsuario = ModeloUsuario.TraerUsuario(Id_Usuario);
            string CodigoAlquilador = "Alquilador" + ModeloUsuario.Nombre + ModeloUsuario.NumeroIdentificacion;
            string ConsultaSQL = "INSERT INTO " +
            "alquiler_vehiculos.alquilador " +
            "(alquiler_vehiculos.alquilador.Codigo_Alquilador, " +
            "alquiler_vehiculos.alquilador.Usuario_Alquilador) " +
            "VALUES " +
            "('" + CodigoAlquilador +  "', " +
            ModeloUsuario.Id + ")";
            return ModeloConexion.ExecuteNonQuerySentence(ConsultaSQL);
        }
        public bool ValidarAlquilador(int Id_Usuario){
            return TraerAlquilador(Id_Usuario) == null;
        }
    }
}
