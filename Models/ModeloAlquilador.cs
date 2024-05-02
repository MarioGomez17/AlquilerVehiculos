using MySql.Data.MySqlClient;

namespace ALQUILER_VEHICULOS.Models
{
    public class ModeloAlquilador
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public ModeloUsuario Usuario { get; set; }
        public ModeloAlquilador TraerAlquiladorUsuario(int IdUsuario)
        {
            ModeloAlquilador ModeloAlquilador = null;
            ModeloUsuario ModeloUsuario = new();
            string ConsultaSQL = "SELECT " +
                    "alquiler_vehiculos.alquilador.Id_Alquilador, " +
                    "alquiler_vehiculos.alquilador.Codigo_Alquilador, " +
                    "alquiler_vehiculos.alquilador.Usuario_Alquilador " +
                    "FROM alquiler_vehiculos.alquilador " +
                    "WHERE alquiler_vehiculos.alquilador.Usuario_Alquilador = " + IdUsuario;
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
                            Usuario = ModeloUsuario.TraerUsuario(IdUsuario)
                        };
                    }
                }
            }
            catch (Exception) { }
            finally
            {
                ConexionBD.Close();
            }
            return ModeloAlquilador;
        }
        public ModeloAlquilador TraerAlquilador(int IdAlquilador)
        {
            ModeloAlquilador ModeloAlquilador = null;
            ModeloUsuario ModeloUsuario = new();
            string ConsultaSQL = "SELECT " +
                    "alquiler_vehiculos.alquilador.Id_Alquilador, " +
                    "alquiler_vehiculos.alquilador.Codigo_Alquilador, " +
                    "alquiler_vehiculos.alquilador.Usuario_Alquilador " +
                    "FROM alquiler_vehiculos.alquilador " +
                    "WHERE alquiler_vehiculos.alquilador.Id_Alquilador = " + IdAlquilador;
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
                            Usuario = ModeloUsuario.TraerUsuario(Lector.GetInt32(2))
                        };
                    }
                }
            }
            catch (Exception) { }
            finally
            {
                ConexionBD.Close();
            }
            return ModeloAlquilador;
        }
        public bool CrearAlquilador(int IdUsuario)
        {
            ModeloUsuario ModeloUsuario = new();
            ModeloUsuario = ModeloUsuario.TraerUsuario(IdUsuario);
            string CodigoAlquilador = "Alquilador" + ModeloUsuario.Nombre + ModeloUsuario.NumeroIdentificacion;
            string ConsultaSQL = "INSERT INTO " +
            "alquiler_vehiculos.alquilador " +
            "(alquiler_vehiculos.alquilador.Codigo_Alquilador, " +
            "alquiler_vehiculos.alquilador.Usuario_Alquilador) " +
            "VALUES " +
            "('" + CodigoAlquilador + "', " +
            ModeloUsuario.Id + ")";
            return ModeloConexion.ExecuteNonQuerySentence(ConsultaSQL);
        }
        public bool ValidarAlquilador(int IdUsuario)
        {
            return TraerAlquiladorUsuario(IdUsuario) == null;
        }
        public bool AgregarAlquilerHistorialAlquilador(int IdAlquilador, int IdAlquiler)
        {
            string ConsultaSQL = "INSERT INTO " +
                                "alquiler_vehiculos.historial_alquileres_alquilador ( " +
                                "alquiler_vehiculos.historial_alquileres_alquilador.alquilador_HistorialAlquileresalquilador, " +
                                "alquiler_vehiculos.historial_alquileres_alquilador.Alquiler_HistorialAlquileresalquilador) " +
                                "VALUES ( " +
                                IdAlquilador + ", " +
                                IdAlquiler + ")";
            return ModeloConexion.ExecuteNonQuerySentence(ConsultaSQL);
        }
    }
}