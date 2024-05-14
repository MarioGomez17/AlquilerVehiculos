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
        public string Rol { get; set; }
        public List<ModeloPermiso> Permisos { get; set; }
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
                "alquiler_vehiculos.estado_usuario.Nombre_EstadoUsuario, " +
                "alquiler_vehiculos.rol.Nombre_Rol " +
                "FROM alquiler_vehiculos.usuario " +
                "INNER JOIN alquiler_vehiculos.tipo_identificacion_usuario " +
                "ON alquiler_vehiculos.usuario.TipoIdentificacion_Usuario = alquiler_vehiculos.tipo_identificacion_usuario.Id_TipoIdentificacionUsuario " +
                "INNER JOIN alquiler_vehiculos.estado_usuario " +
                "ON alquiler_vehiculos.usuario.Estado_Usuario = alquiler_vehiculos.estado_usuario.Id_EstadoUsuario " +
                "INNER JOIN alquiler_vehiculos.rol " +
                "ON alquiler_vehiculos.usuario.Rol_Usuario = alquiler_vehiculos.rol.Id_Rol " +
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
                            Rol = Lector.GetString(9)
                        };
                        ModeloPermiso Permiso = new();
                        Usuario.Permisos = Permiso.TraerPermisosUsuario(Usuario.Rol);
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
        public ModeloUsuario TraerUsuario(int IdUsuario)
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
                "alquiler_vehiculos.estado_usuario.Nombre_EstadoUsuario, " +
                "alquiler_vehiculos.rol.Nombre_Rol " +
                "FROM alquiler_vehiculos.usuario " +
                "INNER JOIN alquiler_vehiculos.tipo_identificacion_usuario " +
                "ON alquiler_vehiculos.usuario.TipoIdentificacion_Usuario = alquiler_vehiculos.tipo_identificacion_usuario.Id_TipoIdentificacionUsuario " +
                "INNER JOIN alquiler_vehiculos.estado_usuario " +
                "ON alquiler_vehiculos.usuario.Estado_Usuario = alquiler_vehiculos.estado_usuario.Id_EstadoUsuario " +
                "INNER JOIN alquiler_vehiculos.rol " +
                "ON alquiler_vehiculos.usuario.Rol_Usuario = alquiler_vehiculos.rol.Id_Rol " +
                "WHERE alquiler_vehiculos.usuario.Id_Usuario = " + IdUsuario;
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
                            Rol = Lector.GetString(9)
                        };
                        ModeloPermiso Permiso = new();
                        Usuario.Permisos = Permiso.TraerPermisosUsuario(Usuario.Rol);
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
        public List<ModeloUsuario> TraerUsuariosAdministrador(int IdUsuarioAdministrador)
        {
            List<ModeloUsuario> Usuarios = [];
            string ConsultaSQL = "SELECT " +
                "alquiler_vehiculos.usuario.Id_Usuario, " +
                "alquiler_vehiculos.usuario.Nombre_Usuario, " +
                "alquiler_vehiculos.usuario.Apellido_Usuario, " +
                "alquiler_vehiculos.tipo_identificacion_usuario.Nombre_TipoIdentificacionUsuario, " +
                "alquiler_vehiculos.tipo_identificacion_usuario.Simbolo_TipoIdentificacionUsuario, " +
                "alquiler_vehiculos.usuario.NumeroIdentificacion_Usuario, " +
                "alquiler_vehiculos.usuario.Telefono_Usuario, " +
                "alquiler_vehiculos.usuario.Correo_Usuario, " +
                "alquiler_vehiculos.estado_usuario.Nombre_EstadoUsuario, " +
                "alquiler_vehiculos.rol.Nombre_Rol " +
                "FROM alquiler_vehiculos.usuario " +
                "INNER JOIN alquiler_vehiculos.tipo_identificacion_usuario " +
                "ON alquiler_vehiculos.usuario.TipoIdentificacion_Usuario = alquiler_vehiculos.tipo_identificacion_usuario.Id_TipoIdentificacionUsuario " +
                "INNER JOIN alquiler_vehiculos.estado_usuario " +
                "ON alquiler_vehiculos.usuario.Estado_Usuario = alquiler_vehiculos.estado_usuario.Id_EstadoUsuario " +
                "INNER JOIN alquiler_vehiculos.rol " +
                "ON alquiler_vehiculos.usuario.Rol_Usuario = alquiler_vehiculos.rol.Id_Rol " +
                "WHERE alquiler_vehiculos.usuario.Id_Usuario != " + IdUsuarioAdministrador;
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
                        ModeloUsuario Usuario = new()
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
                            Rol = Lector.GetString(9)
                        };
                        ModeloPermiso Permiso = new();
                        Usuario.Permisos = Permiso.TraerPermisosUsuario(Usuario.Rol);
                        Usuarios.Add(Usuario);
                    }
                }
            }
            catch (Exception) { }
            finally
            {
                ConexionBD.Close();
            }
            return Usuarios;
        }
        public ModeloUsuario TraerUsuarioPropietario(int IdPropietario)
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
                "alquiler_vehiculos.estado_usuario.Nombre_EstadoUsuario, " +
                "alquiler_vehiculos.rol.Nombre_Rol " +
                "FROM alquiler_vehiculos.usuario " +
                "INNER JOIN alquiler_vehiculos.tipo_identificacion_usuario " +
                "ON alquiler_vehiculos.usuario.TipoIdentificacion_Usuario = alquiler_vehiculos.tipo_identificacion_usuario.Id_TipoIdentificacionUsuario " +
                "INNER JOIN alquiler_vehiculos.estado_usuario " +
                "ON alquiler_vehiculos.usuario.Estado_Usuario = alquiler_vehiculos.estado_usuario.Id_EstadoUsuario " +
                "INNER JOIN alquiler_vehiculos.rol " +
                "ON alquiler_vehiculos.usuario.Rol_Usuario = alquiler_vehiculos.rol.Id_Rol " +
                "INNER JOIN alquiler_vehiculos.propietario " +
                "ON alquiler_vehiculos.propietario.Usuario_Propietario = alquiler_vehiculos.usuario.Id_Usuario " +
                "WHERE alquiler_vehiculos.propietario.Id_Propietario = " + IdPropietario;
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
                            Rol = Lector.GetString(9)
                        };
                        ModeloPermiso Permiso = new();
                        Usuario.Permisos = Permiso.TraerPermisosUsuario(Usuario.Rol);
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
                "alquiler_vehiculos.estado_usuario.Nombre_EstadoUsuario, " +
                "alquiler_vehiculos.rol.Nombre_Rol " +
                "FROM alquiler_vehiculos.usuario " +
                "INNER JOIN alquiler_vehiculos.tipo_identificacion_usuario " +
                "ON alquiler_vehiculos.usuario.TipoIdentificacion_Usuario = alquiler_vehiculos.tipo_identificacion_usuario.Id_TipoIdentificacionUsuario " +
                "INNER JOIN alquiler_vehiculos.estado_usuario " +
                "ON alquiler_vehiculos.usuario.Estado_Usuario = alquiler_vehiculos.estado_usuario.Id_EstadoUsuario " +
                "INNER JOIN alquiler_vehiculos.rol " +
                "ON alquiler_vehiculos.usuario.Rol_Usuario = alquiler_vehiculos.rol.Id_Rol " +
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
                            Rol = Lector.GetString(9)
                        };
                        ModeloPermiso Permiso = new();
                        Usuario.Permisos = Permiso.TraerPermisosUsuario(Usuario.Rol);
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
        public bool ActualizarUsuario(int Id, string Nombre, string Apellido, int TipoIdentificacion, string NumeroIdentificacion, string Telefono, string Correo, string Contrasena)
        {
            string ConsultaSQL = "UPDATE alquiler_vehiculos.usuario " +
            "SET " +
            "Nombre_Usuario = '" + Nombre + "', " +
            "Apellido_Usuario = '" + Apellido + "', " +
            "TipoIdentificacion_Usuario = " + TipoIdentificacion + ", " +
            "NumeroIdentificacion_Usuario = '" + NumeroIdentificacion + "', " +
            "Telefono_Usuario = '" + Telefono + "', " +
            "Correo_Usuario = '" + Correo + "', " +
            "Contrasena_Usuario = SHA('" + Contrasena + "')" +
            " WHERE (Id_Usuario = " + Id + ")";
            return ModeloConexion.ExecuteNonQuerySentence(ConsultaSQL);
        }
        public bool ActualizarUsuario(int Id, string Nombre, string Apellido, int TipoIdentificacion, string NumeroIdentificacion, string Telefono, string Correo)
        {
            string ConsultaSQL = "UPDATE alquiler_vehiculos.usuario " +
            "SET " +
            "Nombre_Usuario = '" + Nombre + "', " +
            "Apellido_Usuario = '" + Apellido + "', " +
            "TipoIdentificacion_Usuario = " + TipoIdentificacion + ", " +
            "NumeroIdentificacion_Usuario = '" + NumeroIdentificacion + "', " +
            "Telefono_Usuario = '" + Telefono + "', " +
            "Correo_Usuario = '" + Correo + "' " +
            " WHERE (Id_Usuario = " + Id + ")";
            return ModeloConexion.ExecuteNonQuerySentence(ConsultaSQL);
        }
        public bool EliminarUsuario(int IdUsuario)
        {
            string ConsultaSQL = "UPDATE " +
                                "alquiler_vehiculos.usuario " +
                                "SET " +
                                "alquiler_vehiculos.usuario.Estado_Usuario = 2 " +
                                "WHERE " +
                                "(alquiler_vehiculos.usuario.Id_Usuario = " + IdUsuario + ")";
            return ModeloConexion.ExecuteNonQuerySentence(ConsultaSQL);
        }
        public bool AccionActualizarUsuarioAdministrador(int Id, string Nombre, string Apellido, int TipoIdentificacion, string NumeroIdentificacion, string Telefono, string Correo, string Contrasena, int Rol)
        {
            string ConsultaSQL = "UPDATE alquiler_vehiculos.usuario " +
            "SET " +
            "Nombre_Usuario = '" + Nombre + "', " +
            "Apellido_Usuario = '" + Apellido + "', " +
            "TipoIdentificacion_Usuario = " + TipoIdentificacion + ", " +
            "NumeroIdentificacion_Usuario = '" + NumeroIdentificacion + "', " +
            "Telefono_Usuario = '" + Telefono + "', " +
            "Correo_Usuario = '" + Correo + "', " +
            "Contrasena_Usuario = SHA('" + Contrasena + "'), " +
            "Rol_Usuario = " + Rol + " " +
            " WHERE (Id_Usuario = " + Id + ")";
            return ModeloConexion.ExecuteNonQuerySentence(ConsultaSQL);
        }
        public bool AccionActualizarUsuarioAdministrador(int Id, string Nombre, string Apellido, int TipoIdentificacion, string NumeroIdentificacion, string Telefono, string Correo, int Rol)
        {
            string ConsultaSQL = "UPDATE alquiler_vehiculos.usuario " +
            "SET " +
            "Nombre_Usuario = '" + Nombre + "', " +
            "Apellido_Usuario = '" + Apellido + "', " +
            "TipoIdentificacion_Usuario = " + TipoIdentificacion + ", " +
            "NumeroIdentificacion_Usuario = '" + NumeroIdentificacion + "', " +
            "Telefono_Usuario = '" + Telefono + "', " +
            "Correo_Usuario = '" + Correo + "', " +
            "Rol_Usuario = " + Rol + " " +
            " WHERE (Id_Usuario = " + Id + ")";
            return ModeloConexion.ExecuteNonQuerySentence(ConsultaSQL);
        }
    }
}