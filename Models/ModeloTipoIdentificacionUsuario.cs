using MySql.Data.MySqlClient;

namespace ALQUILER_VEHICULOS.Models
{
    public class ModeloTipoIdentificacionUsuario
    {
        public int Id { get; set; } 
        public string NombreTipoIdentificacionUsuario { get; set; }
        public string SimboloTipoIdentificacionUsuario { get; set; }

        public List<ModeloTipoIdentificacionUsuario> TraerTodosTiposdeIdentificacion()
        {
            List<ModeloTipoIdentificacionUsuario> TiposdeIdentificacion = [];
            string ConsultaSQL = "SELECT " +
                "alquiler_vehiculos.tipo_identificacion_usuario.Id_TipoIdentificacionUsuario, " +
                "alquiler_vehiculos.tipo_identificacion_usuario.Nombre_TipoIdentificacionUsuario, " +
                "alquiler_vehiculos.tipo_identificacion_usuario.Simbolo_TipoIdentificacionUsuario " +
                "FROM alquiler_vehiculos.tipo_identificacion_usuario " +
                "ORDER BY alquiler_vehiculos.tipo_identificacion_usuario.Id_TipoIdentificacionUsuario ASC";
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
                        ModeloTipoIdentificacionUsuario TipoIdentificacionUsuario = new()
                        {
                            Id = Lector.GetInt32(0),
                            NombreTipoIdentificacionUsuario = Lector.GetString(1),
                            SimboloTipoIdentificacionUsuario = Lector.GetString(2),
                        };
                        TiposdeIdentificacion.Add(TipoIdentificacionUsuario);
                    }
                }
            }
            catch (Exception) { }
            finally
            {
                ConexionBD.Close();
            }
            return TiposdeIdentificacion;
        }
    }
}
