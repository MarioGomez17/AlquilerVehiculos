using ALQUILER_VEHICULOS.Models;
using MySql.Data.MySqlClient;
namespace ALQUILER_VEHICULOS.Reports
{
    public class ReporteAlquileresAlquilador
    {
        public void GenerarReporteAlquileresAlquilador(int IdUsuario)
        {
            ModeloAlquilador ModeloAlquilador = new();
            if (!ModeloAlquilador.ValidarAlquilador(IdUsuario))
            {
                ModeloAlquilador = ModeloAlquilador.TraerAlquiladorUsuario(IdUsuario);
                string ConsultaSQL = "SELECT " +
                    "alquiler_vehiculos.alquiler.FechaIncio_Alquiler, " +
                    "alquiler_vehiculos.alquiler.FechaFin_Alquiler, " +
                    "alquiler_vehiculos.alquiler.Precio_Alquiler, " +
                    "alquiler_vehiculos.tipo_vehiculo.Nombre_TipoVehiculo, " +
                    "alquiler_vehiculos.marca_vehiculo.Nombre_MarcaVehiculo, " +
                    "alquiler_vehiculos.linea_vehiculo.Nombre_LineaVehiculo, " +
                    "alquiler_vehiculos.lugar_alquiler.Nombre_LugarAlquiler, " +
                    "alquiler_vehiculos.metodo_pago.Nombre_MetodoPago, " +
                    "alquiler_vehiculos.estado_pago_alquiler.Nombre_EstadoPagoAlquiler, " +
                    "alquiler_vehiculos.seguro_alquiler.Nombre_SeguroAlquiler, " +
                    "alquiler_vehiculos.seguro_alquiler.Precio_SeguroAlquiler, " +
                    "alquiler_vehiculos.estado_alquiler.Nombre_EstadoAlquiler, " +
                    "alquiler_vehiculos.alquiler.Calificacion_Alquiler " +
                    "FROM alquiler_vehiculos.alquiler " +
                    "INNER JOIN alquiler_vehiculos.estado_alquiler " +
                    "ON alquiler_vehiculos.estado_alquiler.Id_EstadoAlquiler = alquiler_vehiculos.alquiler.Estado_Alquiler " +
                    "INNER JOIN alquiler_vehiculos.estado_pago_alquiler " +
                    "ON alquiler_vehiculos.estado_pago_alquiler.Id_EstadoPagoAlquiler = alquiler_vehiculos.alquiler.EstadoPago_Alquiler " +
                    "INNER JOIN alquiler_vehiculos.seguro_alquiler " +
                    "ON alquiler_vehiculos.seguro_alquiler.Id_SeguroAlquiler = alquiler_vehiculos.alquiler.Seguro_Alquiler " +
                    "INNER JOIN alquiler_vehiculos.metodo_pago " +
                    "ON alquiler_vehiculos.metodo_pago.Id_MetodoPago = alquiler_vehiculos.alquiler.MeotodoPago_Alquiler " +
                    "INNER JOIN alquiler_vehiculos.lugar_alquiler " +
                    "ON alquiler_vehiculos.lugar_alquiler.Id_LugarAlquiler = alquiler_vehiculos.alquiler.Lugar_Alquiler " +
                    "INNER JOIN alquiler_vehiculos.vehiculo " +
                    "ON alquiler_vehiculos.vehiculo.Id_Vehiculo = alquiler_vehiculos.alquiler.Vehiculo_Alquiler " +
                    "INNER JOIN alquiler_vehiculos.linea_vehiculo " +
                    "ON alquiler_vehiculos.linea_vehiculo.Id_LineaVehiculo = alquiler_vehiculos.vehiculo.Linea_Vehiculo " +
                    "INNER JOIN alquiler_vehiculos.marca_vehiculo " +
                    "ON alquiler_vehiculos.marca_vehiculo.Id_MarcaVehiculo = alquiler_vehiculos.linea_vehiculo.MarcaVehiculo_LineaVehiculo " +
                    "INNER JOIN alquiler_vehiculos.tipo_vehiculo " +
                    "ON alquiler_vehiculos.tipo_vehiculo.Id_TipoVehiculo = alquiler_vehiculos.marca_vehiculo.TipoVehiculo_MarcaVehiculo " +
                    "WHERE alquiler_vehiculos.alquiler.Alquilador_Alquiler = " + ModeloAlquilador.Id + " " +
                    "ORDER BY alquiler_vehiculos.alquiler.FechaIncio_Alquiler ASC";
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
}