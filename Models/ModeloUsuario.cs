using MySql.Data.MySqlClient;

namespace ALQUILER_VEHICULOS.Models
{
    public class ModeloUsuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string TipoIdentificacion { get; set; }
        public string SimboloTipoIdentificacion { get; set; }
        public string NumeroIdentificacion { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public string Contrasena { get; set; }
        public string Estado { get; set; }

        public ModeloUsuario TraerUsuario(string Correo, string Contrasena)
        {
            ModeloUsuario Usuario = new();
            string ConsultaSQL = "SELECT " +
                "alquiler_vehiculos.usuario.Id_Usuario, " +
                "alquiler_vehiculos.usuario.Nombre_Usuario, " +
                "alquiler_vehiculos.usuario.Apellido_Usuario, " +
                "alquiler_vehiculos.tipo_identificacion_usuario.Nombre_TipoIdentificacionUsuario, " +
                "alquiler_vehiculos.tipo_identificacion_usuario.Simbolo_TipoIdentificacionUsuario, " +
                "alquiler_vehiculos.usuario.NumeroIdentificacion_Usuario, " + 
                "alquiler_vehiculos.usuario.Telefono_Usuario, " +
                "alquiler_vehiculos.usuario.Correo_Usuario, " +
                "alquiler_vehiculos.estado_usuario.Nombre_EstadoUsuario " +
                "FROM alquiler_vehiculos.usuario " +
                "INNER JOIN alquiler_vehiculos.tipo_identificacion_usuario " +
                "ON alquiler_vehiculos.usuario.TipoIdentificacion_Usuario = alquiler_vehiculos.tipo_identificacion_usuario.Id_TipoIdentificacionUsuario " +
                "INNER JOIN alquiler_vehiculos.estado_usuario " +
                "ON alquiler_vehiculos.usuario.Estado_Usuario = alquiler_vehiculos.estado_usuario.Id_EstadoUsuario " +
                "WHERE alquiler_vehiculos.usuario.Correo_Usuario = '" + Correo + "' " +
                "AND alquiler_vehiculos.usuario.Contrasena_Usuario = SHA('" + Contrasena + "') ";
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
                        Usuario = new()
                        {
                            Id = Lector.GetInt32(0),
                            Nombre = Lector.GetString(1),
                            Apellido = Lector.GetString(2),
                            TipoIdentificacion = Lector.GetString(3),
                            SimboloTipoIdentificacion = Lector.GetString(4),
                            NumeroIdentificacion = Lector.GetString(5),
                            Telefono = Lector.GetString(6),
                            Correo = Lector.GetString(7),
                            Contrasena = "",
                            Estado = Lector.GetString(8),
                        };
                    }
                }
            }
            catch (Exception) {}
            finally
            {
                ConexionBD.Close();
            }
            return Usuario;
        }

        public ModeloUsuario TraerUsuario(int Id_usuario)
        {
            ModeloUsuario Usuario = new();
            string ConsultaSQL = "SELECT " +
                "alquiler_vehiculos.usuario.Id_Usuario, " +
                "alquiler_vehiculos.usuario.Nombre_Usuario, " +
                "alquiler_vehiculos.usuario.Apellido_Usuario, " +
                "alquiler_vehiculos.tipo_identificacion_usuario.Nombre_TipoIdentificacionUsuario, " +
                "alquiler_vehiculos.tipo_identificacion_usuario.Simbolo_TipoIdentificacionUsuario, " +
                "alquiler_vehiculos.usuario.NumeroIdentificacion_Usuario, " + 
                "alquiler_vehiculos.usuario.Telefono_Usuario, " +
                "alquiler_vehiculos.usuario.Correo_Usuario, " +
                "alquiler_vehiculos.estado_usuario.Nombre_EstadoUsuario " +
                "FROM alquiler_vehiculos.usuario " +
                "INNER JOIN alquiler_vehiculos.tipo_identificacion_usuario " +
                "ON alquiler_vehiculos.usuario.TipoIdentificacion_Usuario = alquiler_vehiculos.tipo_identificacion_usuario.Id_TipoIdentificacionUsuario " +
                "INNER JOIN alquiler_vehiculos.estado_usuario " +
                "ON alquiler_vehiculos.usuario.Estado_Usuario = alquiler_vehiculos.estado_usuario.Id_EstadoUsuario " +
                "WHERE alquiler_vehiculos.usuario.Id_Usuario = " + Id_usuario;
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
                        Usuario = new()
                        {
                            Id = Lector.GetInt32(0),
                            Nombre = Lector.GetString(1),
                            Apellido = Lector.GetString(2),
                            TipoIdentificacion = Lector.GetString(3),
                            SimboloTipoIdentificacion = Lector.GetString(4),
                            NumeroIdentificacion = Lector.GetString(5),
                            Telefono = Lector.GetString(6),
                            Correo = Lector.GetString(7),
                            Contrasena = "",
                            Estado = Lector.GetString(8),
                        };
                    }
                }
            }
            catch (Exception) { }
            finally
            {
                ConexionBD.Close();
            }
            return Usuario;
        }

        public bool ValidarUsuario(string NumeroIdentificacion)
        {
            ModeloUsuario Usuario = null;
            string ConsultaSQL = "SELECT " +
                "alquiler_vehiculos.usuario.Id_Usuario, " +
                "alquiler_vehiculos.usuario.Nombre_Usuario, " +
                "alquiler_vehiculos.usuario.Apellido_Usuario, " +
                "alquiler_vehiculos.tipo_identificacion_usuario.Nombre_TipoIdentificacionUsuario, " +
                "alquiler_vehiculos.tipo_identificacion_usuario.Simbolo_TipoIdentificacionUsuario, " +
                "alquiler_vehiculos.usuario.NumeroIdentificacion_Usuario, " +
                "alquiler_vehiculos.usuario.Telefono_Usuario, " +
                "alquiler_vehiculos.usuario.Correo_Usuario, " +
                "alquiler_vehiculos.estado_usuario.Nombre_EstadoUsuario " +
                "FROM alquiler_vehiculos.usuario " +
                "INNER JOIN alquiler_vehiculos.tipo_identificacion_usuario " +
                "ON alquiler_vehiculos.usuario.TipoIdentificacion_Usuario = alquiler_vehiculos.tipo_identificacion_usuario.Id_TipoIdentificacionUsuario " +
                "INNER JOIN alquiler_vehiculos.estado_usuario " +
                "ON alquiler_vehiculos.usuario.Estado_Usuario = alquiler_vehiculos.estado_usuario.Id_EstadoUsuario " +
                "WHERE alquiler_vehiculos.usuario.NumeroIdentificacion_Usuario = '" + NumeroIdentificacion + "' ";
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
                        Usuario = new()
                        {
                            Id = Lector.GetInt32(0),
                            Nombre = Lector.GetString(1),
                            Apellido = Lector.GetString(2),
                            TipoIdentificacion = Lector.GetString(3),
                            SimboloTipoIdentificacion = Lector.GetString(4),
                            NumeroIdentificacion = Lector.GetString(5),
                            Telefono = Lector.GetString(6),
                            Correo = Lector.GetString(7),
                            Contrasena = "",
                            Estado = Lector.GetString(8),
                        };
                    }
                }
            }
            catch (Exception) { }
            finally
            {
                ConexionBD.Close();
            }
            return Usuario == null;
        }

        public bool RegistrarUsuario(string Nombre, string Apellido, int TipoIdentificacion, string NumeroIdentificacion, string Telefono, string Correo, string Contrasena)
        {
            string ConsultaSQL = "INSERT INTO alquiler_vehiculos.usuario " +
                "(alquiler_vehiculos.usuario.Nombre_Usuario, " +
                "alquiler_vehiculos.usuario.Apellido_Usuario, " +
                "alquiler_vehiculos.usuario.TipoIdentificacion_Usuario, " +
                "alquiler_vehiculos.usuario.NumeroIdentificacion_Usuario, " +
                "alquiler_vehiculos.usuario.Telefono_Usuario, " +
                "alquiler_vehiculos.usuario.Correo_Usuario, " +
                "alquiler_vehiculos.usuario.Contrasena_Usuario) " +
                "VALUES (" +
                "'" + Nombre + "', " +
                "'" + Apellido + "', " +
                "'" + TipoIdentificacion + "', " +
                "'" + NumeroIdentificacion + "', " +
                "'" + Telefono + "', " +
                "'" + Correo + "', " +
                "SHA ('" + Contrasena + "'))";
            return ModeloConexion.ExecuteNonQuerySentence(ConsultaSQL);
        }
    }
}
