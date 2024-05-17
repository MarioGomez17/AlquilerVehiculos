using MySql.Data.MySqlClient;
namespace ALQUILER_VEHICULOS.Models
{
    public class ModeloEmpresa
    {
        public string Nombre { get; set; }
        public string Ciudad { get; set; }
        public string Direccion { get; set; }
        public string Barrio { get; set; }
        public string NIT { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public string RutaFoto { get; set; }
        public ModeloEmpresa()
        {
            string ConsultaSQL = "SELECT " +
                                "alquiler_vehiculos.empresa.Nombre_Empresa, " +
                                "alquiler_vehiculos.ciudad.Nombre_Ciudad, " +
                                "alquiler_vehiculos.departamento.Nombre_Departamento, " +
                                "alquiler_vehiculos.empresa.Direccion_Empresa, " +
                                "alquiler_vehiculos.empresa.Barrio_Empresa, " +
                                "alquiler_vehiculos.empresa.NIT_Empresa, " +
                                "alquiler_vehiculos.empresa.Telefono_Empresa, " +
                                "alquiler_vehiculos.empresa.Correo_Empresa, " +
                                "alquiler_vehiculos.empresa.RutaFoto_Empresa " +
                                "FROM alquiler_vehiculos.empresa " +
                                "INNER JOIN alquiler_vehiculos.ciudad " +
                                "ON alquiler_vehiculos.ciudad.Id_Ciudad = alquiler_vehiculos.empresa.Ciudad_Empresa " +
                                "INNER JOIN alquiler_vehiculos.departamento " +
                                "ON alquiler_vehiculos.departamento.Id_Departamento = alquiler_vehiculos.ciudad.Departamento_Ciudad";
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
                        this.Nombre = Lector.GetString(0);
                        this.Ciudad = Lector.GetString(1) + " - " + Lector.GetString(2);
                        this.Direccion = Lector.GetString(3);
                        this.Barrio = Lector.GetString(4);
                        this.NIT = Lector.GetString(5);
                        this.Telefono = Lector.GetString(6);
                        this.Correo = Lector.GetString(7);
                        this.RutaFoto = Lector.GetString(8);
                    }
                }
            }
            catch (Exception) { }
            finally
            {
                ConexionBD.Close();
            }
        }
    }
}