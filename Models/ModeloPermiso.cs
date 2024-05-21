using MySql.Data.MySqlClient;
namespace ALQUILER_VEHICULOS.Models
{
    public class ModeloPermiso
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Controlador { get; set; }
        public string Accion { get; set; }
        public List<ModeloPermiso> TraerPermisosUsuario(string Rol)
        {
            List<ModeloPermiso> Permisos = [];
            string ConsultaSQL = "SELECT " +
                                "alquiler_vehiculos.permiso.Id_Permiso, " +
                                "alquiler_vehiculos.permiso.NombrePermiso, " +
                                "alquiler_vehiculos.permiso.Controlador_Permiso, " +
                                "alquiler_vehiculos.permiso.LinkPermiso " +
                                "FROM alquiler_vehiculos.rol_permiso " +
                                "INNER JOIN alquiler_vehiculos.rol " +
                                "ON alquiler_vehiculos.rol_permiso.Rol_RolPermiso = alquiler_vehiculos.rol.Id_Rol " +
                                "INNER JOIN alquiler_vehiculos.permiso " +
                                "ON alquiler_vehiculos.permiso.Id_Permiso = alquiler_vehiculos.rol_permiso.Permiso_RolPermiso " +
                                "WHERE alquiler_vehiculos.rol.Nombre_Rol = '" + Rol + "' " +
                                "ORDER BY alquiler_vehiculos.permiso.Id_Permiso ASC";
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
                        ModeloPermiso Permiso = new()
                        {
                            Id = Lector.GetInt32(0),
                            Nombre = Lector.GetString(1),
                            Controlador = Lector.GetString(2),
                            Accion = Lector.GetString(3)
                        };
                        Permisos.Add(Permiso);
                    }
                }
            }
            catch (Exception) { }
            finally
            {
                ConexionBD.Close();
            }
            return Permisos;
        }
        public List<ModeloPermiso> TraerPermisosRol(int Rol)
        {
            List<ModeloPermiso> Permisos = [];
            string ConsultaSQL = "SELECT " +
                                "alquiler_vehiculos.permiso.Id_Permiso, " +
                                "alquiler_vehiculos.permiso.NombrePermiso, " +
                                "alquiler_vehiculos.permiso.Controlador_Permiso, " +
                                "alquiler_vehiculos.permiso.LinkPermiso " +
                                "FROM alquiler_vehiculos.rol_permiso " +
                                "INNER JOIN alquiler_vehiculos.rol " +
                                "ON alquiler_vehiculos.rol_permiso.Rol_RolPermiso = alquiler_vehiculos.rol.Id_Rol " +
                                "INNER JOIN alquiler_vehiculos.permiso " +
                                "ON alquiler_vehiculos.permiso.Id_Permiso = alquiler_vehiculos.rol_permiso.Permiso_RolPermiso " +
                                "WHERE alquiler_vehiculos.rol.Id_Rol = " + Rol + " " +
                                "ORDER BY alquiler_vehiculos.permiso.Id_Permiso ASC";
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
                        ModeloPermiso Permiso = new()
                        {
                            Id = Lector.GetInt32(0),
                            Nombre = Lector.GetString(1),
                            Controlador = Lector.GetString(2),
                            Accion = Lector.GetString(3)
                        };
                        Permisos.Add(Permiso);
                    }
                }
            }
            catch (Exception) { }
            finally
            {
                ConexionBD.Close();
            }
            return Permisos;
        }
        public List<ModeloPermiso> TraerPermisos()
        {
            List<ModeloPermiso> Permisos = [];
            string ConsultaSQL = "SELECT " +
                                "alquiler_vehiculos.permiso.Id_Permiso, " +
                                "alquiler_vehiculos.permiso.NombrePermiso, " +
                                "alquiler_vehiculos.permiso.Controlador_Permiso, " +
                                "alquiler_vehiculos.permiso.LinkPermiso " +
                                "FROM alquiler_vehiculos.permiso " +
                                "ORDER BY alquiler_vehiculos.permiso.Id_Permiso ASC";
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
                        ModeloPermiso Permiso = new()
                        {
                            Id = Lector.GetInt32(0),
                            Nombre = Lector.GetString(1),
                            Controlador = Lector.GetString(2),
                            Accion = Lector.GetString(3)
                        };
                        Permisos.Add(Permiso);
                    }
                }
            }
            catch (Exception) { }
            finally
            {
                ConexionBD.Close();
            }
            return Permisos;
        }
        public bool EliminarPermiso(int IdRol, int IdPermiso)
        {
            string ConsultaSQL = "DELETE  " +
            "FROM alquiler_vehiculos.rol_permiso " +
            "WHERE (" +
            "alquiler_vehiculos.rol_permiso.Rol_RolPermiso = " + IdRol + " " +
            "AND " +
            "alquiler_vehiculos.rol_permiso.Permiso_RolPermiso = " + IdPermiso + ")";
            return ModeloConexion.ExecuteNonQuerySentence(ConsultaSQL);
        }
        public bool AgregarPermiso(int IdRol, int IdPermiso)
        {
            string ConsultaSQL = "INSERT INTO  " +
            "alquiler_vehiculos.rol_permiso (" +
            "alquiler_vehiculos.rol_permiso.Rol_RolPermiso, " +
            "alquiler_vehiculos.rol_permiso.Permiso_RolPermiso)" +
            "VALUES (" +
            IdRol + ", " +
            IdPermiso + ")";
            return ModeloConexion.ExecuteNonQuerySentence(ConsultaSQL);
        }
    }
}