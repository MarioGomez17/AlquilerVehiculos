using MySql.Data.MySqlClient;

namespace ALQUILER_VEHICULOS.Models
{
    public class ModeloAlquileresUsuario
    {
        public ModeloUsuario Usuario { get; set; }
        public List<ModeloAlquiler> HistorialAlquileresAlquilador { get; set; }
        public List<ModeloAlquiler> HistorialAlquileresPropietario { get; set; }
        public ModeloAlquileresUsuario(int IdUsuario)
        {
            ModeloUsuario ModeloUsuario = new();
            this.Usuario = ModeloUsuario.TraerUsuario(IdUsuario);
            this.HistorialAlquileresAlquilador = TraerHistorialAlquileresAlquilador(IdUsuario);
            this.HistorialAlquileresPropietario = TraerHistorialAlquileresPropietario(IdUsuario);
        }
        public List<ModeloAlquiler> TraerHistorialAlquileresAlquilador(int IdUsuario)
        {
            List<ModeloAlquiler> AlquileresAlquilador = [];
            ModeloAlquilador ModeloAlquilador = new();
            if (!ModeloAlquilador.ValidarAlquilador(IdUsuario))
            {
                ModeloAlquilador = ModeloAlquilador.TraerAlquiladorUsuario(IdUsuario);
                string ConsultaSQL = "SELECT " +
                                    "alquiler_vehiculos.historial_alquileres_alquilador.Alquiler_HistorialAlquileresAlquilador " +
                                    "FROM alquiler_vehiculos.historial_alquileres_alquilador " +
                                    "WHERE " +
                                    "alquiler_vehiculos.historial_alquileres_alquilador.Alquilador_HistorialAlquileresAlquilador = " + ModeloAlquilador.Id + " " +
                                    "ORDER BY alquiler_vehiculos.historial_alquileres_alquilador.Id_HistorialAlquileresAlquilador DESC";
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
                            ModeloAlquiler Alquiler = new();
                            Alquiler = Alquiler.TraerAlquiler(Lector.GetInt32(0));
                            AlquileresAlquilador.Add(Alquiler);
                        }
                    }
                }
                catch (Exception) { }
                finally
                {
                    ConexionBD.Close();
                }
            }
            return AlquileresAlquilador;
        }
        public List<ModeloAlquiler> TraerHistorialAlquileresPropietario(int IdUsuario)
        {
            List<ModeloAlquiler> AlquileresPropietario = [];
            ModeloPropietario ModeloPropietario = new();
            if (!ModeloPropietario.ValidarPropietario(IdUsuario))
            {
                ModeloPropietario = ModeloPropietario.TraerPropietario(IdUsuario);
                string ConsultaSQL = "SELECT " +
                                    "alquiler_vehiculos.historial_alquileres_propietario.Alquiler_HistorialAlquileresPropietario " +
                                    "FROM alquiler_vehiculos.historial_alquileres_propietario " +
                                    "WHERE " +
                                    "alquiler_vehiculos.historial_alquileres_propietario.propietario_HistorialAlquileresPropietario = " + ModeloPropietario.Id + " " +
                                    "ORDER BY alquiler_vehiculos.historial_alquileres_propietario.Id_HistorialAlquileresPropietario DESC";
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
                            ModeloAlquiler Alquiler = new();
                            Alquiler = Alquiler.TraerAlquiler(Lector.GetInt32(0));
                            AlquileresPropietario.Add(Alquiler);
                        }
                    }
                }
                catch (Exception) { }
                finally
                {
                    ConexionBD.Close();
                }
            }
            return AlquileresPropietario;
        }
    }
}