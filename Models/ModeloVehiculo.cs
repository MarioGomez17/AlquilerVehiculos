using MySql.Data.MySqlClient;

namespace ALQUILER_VEHICULOS.Models
{
    public class ModeloVehiculo
    {
        public int Id { get; set; }
        public string TipoVehiculo { get; set; }
        public string ClasificacionVehiculo { get; set; }
        public string Placa { get; set; }
        public int Modelo { get; set; }
        public int Cilindrada { get; set; }
        public string Color { get; set; }
        public int CantidadPasajeros { get; set; }
        public string NumeroSeguro { get; set; }
        public string NumeroCertificadoCDA { get; set; }
        public float PrecioAlquilerDia { get; set; }
        public string RutaFoto { get; set; }
        public string Ciudad { get; set; }
        public string Marca { get; set; }
        public string Linea { get; set; }
        public string TipoCombustible { get; set; }
        public string Estado { get; set; }
        public int Propietario { get; set; }
        public ModeloUsuario UsuarioPropietario { get; set; }
        public List<ModeloVehiculo> TraerTodosVehiculos()
        {
            List<ModeloVehiculo> ListaVehiculos = [];
            string ConsultaSQL = "SELECT " +
                "alquiler_vehiculos.vehiculo.Id_Vehiculo, " +
                "alquiler_vehiculos.tipo_vehiculo.Nombre_TipoVehiculo, " +
                "alquiler_vehiculos.clasificacion_vehiculo.Nombre_ClasificacionVehiculo, " +
                "alquiler_vehiculos.vehiculo.Placa_Vehiculo, " +
                "alquiler_vehiculos.modelo.Valor_Modelo, " +
                "alquiler_vehiculos.cilindrada.Valor_Cilindrada, " +
                "alquiler_vehiculos.color.Nombre_Color, " +
                "alquiler_vehiculos.cantidad_pasajeros.Valor_CantidadPasajeros, " +
                "alquiler_vehiculos.vehiculo.NumeroSeguro_Vehiculo, " +
                "alquiler_vehiculos.vehiculo.NumeroCertificadoCDA_Vehiculo, " +
                "alquiler_vehiculos.vehiculo.PrecioAlquilerDia_Vehiculo, " +
                "alquiler_vehiculos.vehiculo.Foto_Vehiculo, " +
                "alquiler_vehiculos.ciudad.Nombre_Ciudad, " +
                "alquiler_vehiculos.departamento.Nombre_Departamento, " +
                "alquiler_vehiculos.marca_vehiculo.Nombre_MarcaVehiculo, " +
                "alquiler_vehiculos.linea_vehiculo.Nombre_LineaVehiculo, " +
                "alquiler_vehiculos.tipo_combustible.Nombre_TipoCombustible, " +
                "alquiler_vehiculos.estado_vehiculo.Nombre_EstadoVehiculo, " +
                "alquiler_vehiculos.vehiculo.Propietario_Vehiculo " +
                "FROM alquiler_vehiculos.vehiculo " +
                "INNER JOIN alquiler_vehiculos.clasificacion_vehiculo " +
                "ON alquiler_vehiculos.vehiculo.Clasificacion_Vehiculo = alquiler_vehiculos.clasificacion_vehiculo.Id_ClasificacionVehiculo " +
                "INNER JOIN alquiler_vehiculos.ciudad " +
                "ON alquiler_vehiculos.ciudad.Id_Ciudad = alquiler_vehiculos.vehiculo.Ciudad_Vehiculo " +
                "INNER JOIN alquiler_vehiculos.departamento " +
                "ON alquiler_vehiculos.ciudad.Departamento_Ciudad = alquiler_vehiculos.departamento.Id_Departamento " +
                "INNER JOIN alquiler_vehiculos.tipo_combustible " +
                "ON alquiler_vehiculos.tipo_combustible.Id_TipoCombustible = alquiler_vehiculos.vehiculo.TipoCombustible_Vehiculo " +
                "INNER JOIN alquiler_vehiculos.estado_vehiculo " +
                "ON alquiler_vehiculos.estado_vehiculo.Id_EstadoVehiculo = alquiler_vehiculos.vehiculo.Estado_Vehiculo " +
                "INNER JOIN alquiler_vehiculos.linea_vehiculo " +
                "ON alquiler_vehiculos.linea_vehiculo.Id_LineaVehiculo = alquiler_vehiculos.vehiculo.Linea_Vehiculo " +
                "INNER JOIN alquiler_vehiculos.marca_vehiculo " +
                "ON alquiler_vehiculos.marca_vehiculo.Id_MarcaVehiculo = alquiler_vehiculos.linea_vehiculo.MarcaVehiculo_LineaVehiculo " +
                "INNER JOIN alquiler_vehiculos.tipo_vehiculo " +
                "ON alquiler_vehiculos.tipo_vehiculo.Id_TipoVehiculo = alquiler_vehiculos.marca_vehiculo.TipoVehiculo_MarcaVehiculo " +
                "INNER JOIN alquiler_vehiculos.modelo " + 
                "ON alquiler_vehiculos.modelo.Id_Modelo = alquiler_vehiculos.vehiculo.Modelo_Vehiculo " + 
                "INNER JOIN alquiler_vehiculos.cilindrada " + 
                "ON alquiler_vehiculos.cilindrada.Id_Cilindrada = alquiler_vehiculos.vehiculo.Cilindrada_Vehiculo " + 
                "INNER JOIN alquiler_vehiculos.cantidad_pasajeros " + 
                "ON alquiler_vehiculos.cantidad_pasajeros.Id_CantidadPasajeros = alquiler_vehiculos.vehiculo.CantidadPasajeros_Vehiculo " + 
                "INNER JOIN alquiler_vehiculos.color " + 
                "ON alquiler_vehiculos.color.Id_Color = alquiler_vehiculos.vehiculo.Color_Vehiculo " + 
                "WHERE alquiler_vehiculos.vehiculo.Estado_Vehiculo != 2 " +
                "ORDER BY alquiler_vehiculos.vehiculo.Id_Vehiculo ASC";
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
                        ModeloVehiculo Vehiculo = new()
                        {
                            Id = Lector.GetInt32(0),
                            TipoVehiculo = Lector.GetString(1),
                            ClasificacionVehiculo = Lector.GetString(2),
                            Placa = Lector.GetString(3),
                            Modelo = Lector.GetInt32(4),
                            Cilindrada = Lector.GetInt32(5),
                            Color = Lector.GetString(6),
                            CantidadPasajeros = Lector.GetInt32(7),
                            NumeroSeguro = Lector.GetString(8),
                            NumeroCertificadoCDA = Lector.GetString(9),
                            PrecioAlquilerDia = Lector.GetFloat(10),
                            RutaFoto = Lector.GetString(11),
                            Ciudad = Lector.GetString(12) + " - " + Lector.GetString(13),
                            Marca = Lector.GetString(14),
                            Linea = Lector.GetString(15),
                            TipoCombustible = Lector.GetString(16),
                            Estado = Lector.GetString(17),
                            Propietario = Lector.GetInt32(18),
                        };
                        ModeloUsuario Usuario = new();
                        UsuarioPropietario = Usuario.TraerUsuarioPropietario(Lector.GetInt32(18));
                        ListaVehiculos.Add(Vehiculo);
                    }
                }
            }
            catch (Exception) { }
            finally
            {
                ConexionBD.Close();
            }
            return ListaVehiculos;
        }
        public List<ModeloVehiculo> TraerTodosVehiculosAdministrador()
        {
            List<ModeloVehiculo> ListaVehiculos = [];
            string ConsultaSQL = "SELECT " +
                "alquiler_vehiculos.vehiculo.Id_Vehiculo, " +
                "alquiler_vehiculos.tipo_vehiculo.Nombre_TipoVehiculo, " +
                "alquiler_vehiculos.clasificacion_vehiculo.Nombre_ClasificacionVehiculo, " +
                "alquiler_vehiculos.vehiculo.Placa_Vehiculo, " +
                "alquiler_vehiculos.modelo.Valor_Modelo, " +
                "alquiler_vehiculos.cilindrada.Valor_Cilindrada, " +
                "alquiler_vehiculos.color.Nombre_Color, " +
                "alquiler_vehiculos.cantidad_pasajeros.Valor_CantidadPasajeros, " +
                "alquiler_vehiculos.vehiculo.NumeroSeguro_Vehiculo, " +
                "alquiler_vehiculos.vehiculo.NumeroCertificadoCDA_Vehiculo, " +
                "alquiler_vehiculos.vehiculo.PrecioAlquilerDia_Vehiculo, " +
                "alquiler_vehiculos.vehiculo.Foto_Vehiculo, " +
                "alquiler_vehiculos.ciudad.Nombre_Ciudad, " +
                "alquiler_vehiculos.departamento.Nombre_Departamento, " +
                "alquiler_vehiculos.marca_vehiculo.Nombre_MarcaVehiculo, " +
                "alquiler_vehiculos.linea_vehiculo.Nombre_LineaVehiculo, " +
                "alquiler_vehiculos.tipo_combustible.Nombre_TipoCombustible, " +
                "alquiler_vehiculos.estado_vehiculo.Nombre_EstadoVehiculo, " +
                "alquiler_vehiculos.vehiculo.Propietario_Vehiculo " +
                "FROM alquiler_vehiculos.vehiculo " +
                "INNER JOIN alquiler_vehiculos.clasificacion_vehiculo " +
                "ON alquiler_vehiculos.vehiculo.Clasificacion_Vehiculo = alquiler_vehiculos.clasificacion_vehiculo.Id_ClasificacionVehiculo " +
                "INNER JOIN alquiler_vehiculos.ciudad " +
                "ON alquiler_vehiculos.ciudad.Id_Ciudad = alquiler_vehiculos.vehiculo.Ciudad_Vehiculo " +
                "INNER JOIN alquiler_vehiculos.departamento " +
                "ON alquiler_vehiculos.ciudad.Departamento_Ciudad = alquiler_vehiculos.departamento.Id_Departamento " +
                "INNER JOIN alquiler_vehiculos.tipo_combustible " +
                "ON alquiler_vehiculos.tipo_combustible.Id_TipoCombustible = alquiler_vehiculos.vehiculo.TipoCombustible_Vehiculo " +
                "INNER JOIN alquiler_vehiculos.estado_vehiculo " +
                "ON alquiler_vehiculos.estado_vehiculo.Id_EstadoVehiculo = alquiler_vehiculos.vehiculo.Estado_Vehiculo " +
                "INNER JOIN alquiler_vehiculos.linea_vehiculo " +
                "ON alquiler_vehiculos.linea_vehiculo.Id_LineaVehiculo = alquiler_vehiculos.vehiculo.Linea_Vehiculo " +
                "INNER JOIN alquiler_vehiculos.marca_vehiculo " +
                "ON alquiler_vehiculos.marca_vehiculo.Id_MarcaVehiculo = alquiler_vehiculos.linea_vehiculo.MarcaVehiculo_LineaVehiculo " +
                "INNER JOIN alquiler_vehiculos.tipo_vehiculo " +
                "ON alquiler_vehiculos.tipo_vehiculo.Id_TipoVehiculo = alquiler_vehiculos.marca_vehiculo.TipoVehiculo_MarcaVehiculo " +
                "INNER JOIN alquiler_vehiculos.modelo " + 
                "ON alquiler_vehiculos.modelo.Id_Modelo = alquiler_vehiculos.vehiculo.Modelo_Vehiculo " + 
                "INNER JOIN alquiler_vehiculos.cilindrada " + 
                "ON alquiler_vehiculos.cilindrada.Id_Cilindrada = alquiler_vehiculos.vehiculo.Cilindrada_Vehiculo " + 
                "INNER JOIN alquiler_vehiculos.cantidad_pasajeros " + 
                "ON alquiler_vehiculos.cantidad_pasajeros.Id_CantidadPasajeros = alquiler_vehiculos.vehiculo.CantidadPasajeros_Vehiculo " + 
                "INNER JOIN alquiler_vehiculos.color " + 
                "ON alquiler_vehiculos.color.Id_Color = alquiler_vehiculos.vehiculo.Color_Vehiculo " + 
                "ORDER BY alquiler_vehiculos.vehiculo.Id_Vehiculo ASC";
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
                        ModeloVehiculo Vehiculo = new()
                        {
                            Id = Lector.GetInt32(0),
                            TipoVehiculo = Lector.GetString(1),
                            ClasificacionVehiculo = Lector.GetString(2),
                            Placa = Lector.GetString(3),
                            Modelo = Lector.GetInt32(4),
                            Cilindrada = Lector.GetInt32(5),
                            Color = Lector.GetString(6),
                            CantidadPasajeros = Lector.GetInt32(7),
                            NumeroSeguro = Lector.GetString(8),
                            NumeroCertificadoCDA = Lector.GetString(9),
                            PrecioAlquilerDia = Lector.GetFloat(10),
                            RutaFoto = Lector.GetString(11),
                            Ciudad = Lector.GetString(12) + " - " + Lector.GetString(13),
                            Marca = Lector.GetString(14),
                            Linea = Lector.GetString(15),
                            TipoCombustible = Lector.GetString(16),
                            Estado = Lector.GetString(17),
                            Propietario = Lector.GetInt32(18),
                        };
                        ModeloUsuario Usuario = new();
                        UsuarioPropietario = Usuario.TraerUsuarioPropietario(Lector.GetInt32(18));
                        ListaVehiculos.Add(Vehiculo);
                    }
                }
            }
            catch (Exception) { }
            finally
            {
                ConexionBD.Close();
            }
            return ListaVehiculos;
        }
        public List<ModeloVehiculo> TraerTodosVehiculosTodosFiltros(int Ciudad, int Tipo, int Marca)
        {
            List<ModeloVehiculo> ListaVehiculos = [];
            string ConsultaSQL = "SELECT " +
                "alquiler_vehiculos.vehiculo.Id_Vehiculo, " +
                "alquiler_vehiculos.tipo_vehiculo.Nombre_TipoVehiculo, " +
                "alquiler_vehiculos.clasificacion_vehiculo.Nombre_ClasificacionVehiculo, " +
                "alquiler_vehiculos.vehiculo.Placa_Vehiculo, " +
                "alquiler_vehiculos.modelo.Valor_Modelo, " +
                "alquiler_vehiculos.cilindrada.Valor_Cilindrada, " +
                "alquiler_vehiculos.color.Nombre_Color, " +
                "alquiler_vehiculos.cantidad_pasajeros.Valor_CantidadPasajeros, " +
                "alquiler_vehiculos.vehiculo.NumeroSeguro_Vehiculo, " +
                "alquiler_vehiculos.vehiculo.NumeroCertificadoCDA_Vehiculo, " +
                "alquiler_vehiculos.vehiculo.PrecioAlquilerDia_Vehiculo, " +
                "alquiler_vehiculos.vehiculo.Foto_Vehiculo, " +
                "alquiler_vehiculos.ciudad.Nombre_Ciudad, " +
                "alquiler_vehiculos.departamento.Nombre_Departamento, " +
                "alquiler_vehiculos.marca_vehiculo.Nombre_MarcaVehiculo, " +
                "alquiler_vehiculos.linea_vehiculo.Nombre_LineaVehiculo, " +
                "alquiler_vehiculos.tipo_combustible.Nombre_TipoCombustible, " +
                "alquiler_vehiculos.estado_vehiculo.Nombre_EstadoVehiculo, " +
                "alquiler_vehiculos.vehiculo.Propietario_Vehiculo " +
                "FROM alquiler_vehiculos.Vehiculo " +
                "INNER JOIN alquiler_vehiculos.clasificacion_vehiculo " +
                "ON alquiler_vehiculos.vehiculo.Clasificacion_Vehiculo = alquiler_vehiculos.clasificacion_vehiculo.Id_ClasificacionVehiculo " +
                "INNER JOIN alquiler_vehiculos.ciudad " +
                "ON alquiler_vehiculos.ciudad.Id_Ciudad = alquiler_vehiculos.vehiculo.Ciudad_Vehiculo " +
                "INNER JOIN alquiler_vehiculos.departamento " +
                "ON alquiler_vehiculos.ciudad.Departamento_Ciudad = alquiler_vehiculos.departamento.Id_Departamento " +
                "INNER JOIN alquiler_vehiculos.tipo_combustible " +
                "ON alquiler_vehiculos.tipo_combustible.Id_TipoCombustible = alquiler_vehiculos.vehiculo.TipoCombustible_Vehiculo " +
                "INNER JOIN alquiler_vehiculos.estado_vehiculo " +
                "ON alquiler_vehiculos.estado_vehiculo.Id_EstadoVehiculo = alquiler_vehiculos.vehiculo.Estado_Vehiculo " +
                "INNER JOIN alquiler_vehiculos.linea_vehiculo " +
                "ON alquiler_vehiculos.linea_vehiculo.Id_LineaVehiculo = alquiler_vehiculos.vehiculo.Linea_Vehiculo " +
                "INNER JOIN alquiler_vehiculos.marca_vehiculo " +
                "ON alquiler_vehiculos.marca_vehiculo.Id_MarcaVehiculo = alquiler_vehiculos.linea_vehiculo.MarcaVehiculo_LineaVehiculo " +
                "INNER JOIN alquiler_vehiculos.tipo_vehiculo " +
                "ON alquiler_vehiculos.tipo_vehiculo.Id_TipoVehiculo = alquiler_vehiculos.marca_vehiculo.TipoVehiculo_MarcaVehiculo " +
                "INNER JOIN alquiler_vehiculos.modelo " + 
                "ON alquiler_vehiculos.modelo.Id_Modelo = alquiler_vehiculos.vehiculo.Modelo_Vehiculo " + 
                "INNER JOIN alquiler_vehiculos.cilindrada " + 
                "ON alquiler_vehiculos.cilindrada.Id_Cilindrada = alquiler_vehiculos.vehiculo.Cilindrada_Vehiculo " + 
                "INNER JOIN alquiler_vehiculos.cantidad_pasajeros " + 
                "ON alquiler_vehiculos.cantidad_pasajeros.Id_CantidadPasajeros = alquiler_vehiculos.vehiculo.CantidadPasajeros_Vehiculo " + 
                "INNER JOIN alquiler_vehiculos.color " + 
                "ON alquiler_vehiculos.color.Id_Color = alquiler_vehiculos.vehiculo.Color_Vehiculo " + 
                "WHERE alquiler_vehiculos.vehiculo.Estado_Vehiculo != 2 " +
                "AND alquiler_vehiculos.vehiculo.Ciudad_Vehiculo = " + Ciudad + " " +
                "AND alquiler_vehiculos.tipo_vehiculo.Id_TipoVehiculo = " + Tipo + " " +
                "AND alquiler_vehiculos.marca_vehiculo.Id_MarcaVehiculo = " + Marca + " " +
                "ORDER BY alquiler_vehiculos.vehiculo.Id_Vehiculo ASC";
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
                        ModeloVehiculo Vehiculo = new()
                        {
                            Id = Lector.GetInt32(0),
                            TipoVehiculo = Lector.GetString(1),
                            ClasificacionVehiculo = Lector.GetString(2),
                            Placa = Lector.GetString(3),
                            Modelo = Lector.GetInt32(4),
                            Cilindrada = Lector.GetInt32(5),
                            Color = Lector.GetString(6),
                            CantidadPasajeros = Lector.GetInt32(7),
                            NumeroSeguro = Lector.GetString(8),
                            NumeroCertificadoCDA = Lector.GetString(9),
                            PrecioAlquilerDia = Lector.GetFloat(10),
                            RutaFoto = Lector.GetString(11),
                            Ciudad = Lector.GetString(12) + " - " + Lector.GetString(13),
                            Marca = Lector.GetString(14),
                            Linea = Lector.GetString(15),
                            TipoCombustible = Lector.GetString(16),
                            Estado = Lector.GetString(17),
                            Propietario = Lector.GetInt32(18),
                        };
                        ModeloUsuario Usuario = new();
                        UsuarioPropietario = Usuario.TraerUsuarioPropietario(Lector.GetInt32(18));
                        ListaVehiculos.Add(Vehiculo);
                    }
                }
            }
            catch (Exception) { }
            finally
            {
                ConexionBD.Close();
            }
            return ListaVehiculos;
        }
        public List<ModeloVehiculo> TraerTodosVehiculosFiltroCiudad(int Ciudad)
        {
            List<ModeloVehiculo> ListaVehiculos = [];
            string ConsultaSQL = "SELECT " +
                "alquiler_vehiculos.vehiculo.Id_Vehiculo, " +
                "alquiler_vehiculos.tipo_vehiculo.Nombre_TipoVehiculo, " +
                "alquiler_vehiculos.clasificacion_vehiculo.Nombre_ClasificacionVehiculo, " +
                "alquiler_vehiculos.vehiculo.Placa_Vehiculo, " +
                "alquiler_vehiculos.modelo.Valor_Modelo, " +
                "alquiler_vehiculos.cilindrada.Valor_Cilindrada, " +
                "alquiler_vehiculos.color.Nombre_Color, " +
                "alquiler_vehiculos.cantidad_pasajeros.Valor_CantidadPasajeros, " +
                "alquiler_vehiculos.vehiculo.NumeroSeguro_Vehiculo, " +
                "alquiler_vehiculos.vehiculo.NumeroCertificadoCDA_Vehiculo, " +
                "alquiler_vehiculos.vehiculo.PrecioAlquilerDia_Vehiculo, " +
                "alquiler_vehiculos.vehiculo.Foto_Vehiculo, " +
                "alquiler_vehiculos.ciudad.Nombre_Ciudad, " +
                "alquiler_vehiculos.departamento.Nombre_Departamento, " +
                "alquiler_vehiculos.marca_vehiculo.Nombre_MarcaVehiculo, " +
                "alquiler_vehiculos.linea_vehiculo.Nombre_LineaVehiculo, " +
                "alquiler_vehiculos.tipo_combustible.Nombre_TipoCombustible, " +
                "alquiler_vehiculos.estado_vehiculo.Nombre_EstadoVehiculo, " +
                "alquiler_vehiculos.vehiculo.Propietario_Vehiculo " +
                "FROM alquiler_vehiculos.Vehiculo " +
                "INNER JOIN alquiler_vehiculos.clasificacion_vehiculo " +
                "ON alquiler_vehiculos.vehiculo.Clasificacion_Vehiculo = alquiler_vehiculos.clasificacion_vehiculo.Id_ClasificacionVehiculo " +
                "INNER JOIN alquiler_vehiculos.ciudad " +
                "ON alquiler_vehiculos.ciudad.Id_Ciudad = alquiler_vehiculos.vehiculo.Ciudad_Vehiculo " +
                "INNER JOIN alquiler_vehiculos.departamento " +
                "ON alquiler_vehiculos.ciudad.Departamento_Ciudad = alquiler_vehiculos.departamento.Id_Departamento " +
                "INNER JOIN alquiler_vehiculos.tipo_combustible " +
                "ON alquiler_vehiculos.tipo_combustible.Id_TipoCombustible = alquiler_vehiculos.vehiculo.TipoCombustible_Vehiculo " +
                "INNER JOIN alquiler_vehiculos.estado_vehiculo " +
                "ON alquiler_vehiculos.estado_vehiculo.Id_EstadoVehiculo = alquiler_vehiculos.vehiculo.Estado_Vehiculo " +
                "INNER JOIN alquiler_vehiculos.linea_vehiculo " +
                "ON alquiler_vehiculos.linea_vehiculo.Id_LineaVehiculo = alquiler_vehiculos.vehiculo.Linea_Vehiculo " +
                "INNER JOIN alquiler_vehiculos.marca_vehiculo " +
                "ON alquiler_vehiculos.marca_vehiculo.Id_MarcaVehiculo = alquiler_vehiculos.linea_vehiculo.MarcaVehiculo_LineaVehiculo " +
                "INNER JOIN alquiler_vehiculos.tipo_vehiculo " +
                "ON alquiler_vehiculos.tipo_vehiculo.Id_TipoVehiculo = alquiler_vehiculos.marca_vehiculo.TipoVehiculo_MarcaVehiculo " +
                "INNER JOIN alquiler_vehiculos.modelo " + 
                "ON alquiler_vehiculos.modelo.Id_Modelo = alquiler_vehiculos.vehiculo.Modelo_Vehiculo " + 
                "INNER JOIN alquiler_vehiculos.cilindrada " + 
                "ON alquiler_vehiculos.cilindrada.Id_Cilindrada = alquiler_vehiculos.vehiculo.Cilindrada_Vehiculo " + 
                "INNER JOIN alquiler_vehiculos.cantidad_pasajeros " + 
                "ON alquiler_vehiculos.cantidad_pasajeros.Id_CantidadPasajeros = alquiler_vehiculos.vehiculo.CantidadPasajeros_Vehiculo " + 
                "INNER JOIN alquiler_vehiculos.color " + 
                "ON alquiler_vehiculos.color.Id_Color = alquiler_vehiculos.vehiculo.Color_Vehiculo " + 
                "WHERE alquiler_vehiculos.vehiculo.Estado_Vehiculo != 2 " +
                "AND alquiler_vehiculos.vehiculo.Ciudad_Vehiculo = " + Ciudad + " " +
                "ORDER BY alquiler_vehiculos.vehiculo.Id_Vehiculo ASC";
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
                        ModeloVehiculo Vehiculo = new()
                        {
                            Id = Lector.GetInt32(0),
                            TipoVehiculo = Lector.GetString(1),
                            ClasificacionVehiculo = Lector.GetString(2),
                            Placa = Lector.GetString(3),
                            Modelo = Lector.GetInt32(4),
                            Cilindrada = Lector.GetInt32(5),
                            Color = Lector.GetString(6),
                            CantidadPasajeros = Lector.GetInt32(7),
                            NumeroSeguro = Lector.GetString(8),
                            NumeroCertificadoCDA = Lector.GetString(9),
                            PrecioAlquilerDia = Lector.GetFloat(10),
                            RutaFoto = Lector.GetString(11),
                            Ciudad = Lector.GetString(12) + " - " + Lector.GetString(13),
                            Marca = Lector.GetString(14),
                            Linea = Lector.GetString(15),
                            TipoCombustible = Lector.GetString(16),
                            Estado = Lector.GetString(17),
                            Propietario = Lector.GetInt32(18),
                        };
                        ModeloUsuario Usuario = new();
                        UsuarioPropietario = Usuario.TraerUsuarioPropietario(Lector.GetInt32(18));
                        ListaVehiculos.Add(Vehiculo);
                    }
                }
            }
            catch (Exception) { }
            finally
            {
                ConexionBD.Close();
            }
            return ListaVehiculos;
        }
        public List<ModeloVehiculo> TraerTodosVehiculosFiltroTipo(int Tipo)
        {
            List<ModeloVehiculo> ListaVehiculos = [];
            string ConsultaSQL = "SELECT " +
                "alquiler_vehiculos.vehiculo.Id_Vehiculo, " +
                "alquiler_vehiculos.tipo_vehiculo.Nombre_TipoVehiculo, " +
                "alquiler_vehiculos.clasificacion_vehiculo.Nombre_ClasificacionVehiculo, " +
                "alquiler_vehiculos.vehiculo.Placa_Vehiculo, " +
                "alquiler_vehiculos.modelo.Valor_Modelo, " +
                "alquiler_vehiculos.cilindrada.Valor_Cilindrada, " +
                "alquiler_vehiculos.color.Nombre_Color, " +
                "alquiler_vehiculos.cantidad_pasajeros.Valor_CantidadPasajeros, " +
                "alquiler_vehiculos.vehiculo.NumeroSeguro_Vehiculo, " +
                "alquiler_vehiculos.vehiculo.NumeroCertificadoCDA_Vehiculo, " +
                "alquiler_vehiculos.vehiculo.PrecioAlquilerDia_Vehiculo, " +
                "alquiler_vehiculos.vehiculo.Foto_Vehiculo, " +
                "alquiler_vehiculos.ciudad.Nombre_Ciudad, " +
                "alquiler_vehiculos.departamento.Nombre_Departamento, " +
                "alquiler_vehiculos.marca_vehiculo.Nombre_MarcaVehiculo, " +
                "alquiler_vehiculos.linea_vehiculo.Nombre_LineaVehiculo, " +
                "alquiler_vehiculos.tipo_combustible.Nombre_TipoCombustible, " +
                "alquiler_vehiculos.estado_vehiculo.Nombre_EstadoVehiculo, " +
                "alquiler_vehiculos.vehiculo.Propietario_Vehiculo " +
                "FROM alquiler_vehiculos.Vehiculo " +
                "INNER JOIN alquiler_vehiculos.clasificacion_vehiculo " +
                "ON alquiler_vehiculos.vehiculo.Clasificacion_Vehiculo = alquiler_vehiculos.clasificacion_vehiculo.Id_ClasificacionVehiculo " +
                "INNER JOIN alquiler_vehiculos.ciudad " +
                "ON alquiler_vehiculos.ciudad.Id_Ciudad = alquiler_vehiculos.vehiculo.Ciudad_Vehiculo " +
                "INNER JOIN alquiler_vehiculos.departamento " +
                "ON alquiler_vehiculos.ciudad.Departamento_Ciudad = alquiler_vehiculos.departamento.Id_Departamento " +
                "INNER JOIN alquiler_vehiculos.tipo_combustible " +
                "ON alquiler_vehiculos.tipo_combustible.Id_TipoCombustible = alquiler_vehiculos.vehiculo.TipoCombustible_Vehiculo " +
                "INNER JOIN alquiler_vehiculos.estado_vehiculo " +
                "ON alquiler_vehiculos.estado_vehiculo.Id_EstadoVehiculo = alquiler_vehiculos.vehiculo.Estado_Vehiculo " +
                "INNER JOIN alquiler_vehiculos.linea_vehiculo " +
                "ON alquiler_vehiculos.linea_vehiculo.Id_LineaVehiculo = alquiler_vehiculos.vehiculo.Linea_Vehiculo " +
                "INNER JOIN alquiler_vehiculos.marca_vehiculo " +
                "ON alquiler_vehiculos.marca_vehiculo.Id_MarcaVehiculo = alquiler_vehiculos.linea_vehiculo.MarcaVehiculo_LineaVehiculo " +
                "INNER JOIN alquiler_vehiculos.tipo_vehiculo " +
                "ON alquiler_vehiculos.tipo_vehiculo.Id_TipoVehiculo = alquiler_vehiculos.marca_vehiculo.TipoVehiculo_MarcaVehiculo " +
                "INNER JOIN alquiler_vehiculos.modelo " + 
                "ON alquiler_vehiculos.modelo.Id_Modelo = alquiler_vehiculos.vehiculo.Modelo_Vehiculo " + 
                "INNER JOIN alquiler_vehiculos.cilindrada " + 
                "ON alquiler_vehiculos.cilindrada.Id_Cilindrada = alquiler_vehiculos.vehiculo.Cilindrada_Vehiculo " + 
                "INNER JOIN alquiler_vehiculos.cantidad_pasajeros " + 
                "ON alquiler_vehiculos.cantidad_pasajeros.Id_CantidadPasajeros = alquiler_vehiculos.vehiculo.CantidadPasajeros_Vehiculo " + 
                "INNER JOIN alquiler_vehiculos.color " + 
                "ON alquiler_vehiculos.color.Id_Color = alquiler_vehiculos.vehiculo.Color_Vehiculo " + 
                "WHERE alquiler_vehiculos.vehiculo.Estado_Vehiculo != 2 " +
                "AND alquiler_vehiculos.tipo_vehiculo.Id_TipoVehiculo = " + Tipo + " " +
                "ORDER BY alquiler_vehiculos.vehiculo.Id_Vehiculo ASC";
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
                        ModeloVehiculo Vehiculo = new()
                        {
                            Id = Lector.GetInt32(0),
                            TipoVehiculo = Lector.GetString(1),
                            ClasificacionVehiculo = Lector.GetString(2),
                            Placa = Lector.GetString(3),
                            Modelo = Lector.GetInt32(4),
                            Cilindrada = Lector.GetInt32(5),
                            Color = Lector.GetString(6),
                            CantidadPasajeros = Lector.GetInt32(7),
                            NumeroSeguro = Lector.GetString(8),
                            NumeroCertificadoCDA = Lector.GetString(9),
                            PrecioAlquilerDia = Lector.GetFloat(10),
                            RutaFoto = Lector.GetString(11),
                            Ciudad = Lector.GetString(12) + " - " + Lector.GetString(13),
                            Marca = Lector.GetString(14),
                            Linea = Lector.GetString(15),
                            TipoCombustible = Lector.GetString(16),
                            Estado = Lector.GetString(17),
                            Propietario = Lector.GetInt32(18),
                        };
                        ModeloUsuario Usuario = new();
                        UsuarioPropietario = Usuario.TraerUsuarioPropietario(Lector.GetInt32(18));
                        ListaVehiculos.Add(Vehiculo);
                    }
                }
            }
            catch (Exception) { }
            finally
            {
                ConexionBD.Close();
            }
            return ListaVehiculos;
        }
        public List<ModeloVehiculo> TraerTodosVehiculosFiltroMarca(int Marca)
        {
            List<ModeloVehiculo> ListaVehiculos = [];
            string ConsultaSQL = "SELECT " +
                "alquiler_vehiculos.vehiculo.Id_Vehiculo, " +
                "alquiler_vehiculos.tipo_vehiculo.Nombre_TipoVehiculo, " +
                "alquiler_vehiculos.clasificacion_vehiculo.Nombre_ClasificacionVehiculo, " +
                "alquiler_vehiculos.vehiculo.Placa_Vehiculo, " +
                "alquiler_vehiculos.modelo.Valor_Modelo, " +
                "alquiler_vehiculos.cilindrada.Valor_Cilindrada, " +
                "alquiler_vehiculos.color.Nombre_Color, " +
                "alquiler_vehiculos.cantidad_pasajeros.Valor_CantidadPasajeros, " +
                "alquiler_vehiculos.vehiculo.NumeroSeguro_Vehiculo, " +
                "alquiler_vehiculos.vehiculo.NumeroCertificadoCDA_Vehiculo, " +
                "alquiler_vehiculos.vehiculo.PrecioAlquilerDia_Vehiculo, " +
                "alquiler_vehiculos.vehiculo.Foto_Vehiculo, " +
                "alquiler_vehiculos.ciudad.Nombre_Ciudad, " +
                "alquiler_vehiculos.departamento.Nombre_Departamento, " +
                "alquiler_vehiculos.marca_vehiculo.Nombre_MarcaVehiculo, " +
                "alquiler_vehiculos.linea_vehiculo.Nombre_LineaVehiculo, " +
                "alquiler_vehiculos.tipo_combustible.Nombre_TipoCombustible, " +
                "alquiler_vehiculos.estado_vehiculo.Nombre_EstadoVehiculo, " +
                "alquiler_vehiculos.vehiculo.Propietario_Vehiculo " +
                "FROM alquiler_vehiculos.Vehiculo " +
                "INNER JOIN alquiler_vehiculos.clasificacion_vehiculo " +
                "ON alquiler_vehiculos.vehiculo.Clasificacion_Vehiculo = alquiler_vehiculos.clasificacion_vehiculo.Id_ClasificacionVehiculo " +
                "INNER JOIN alquiler_vehiculos.ciudad " +
                "ON alquiler_vehiculos.ciudad.Id_Ciudad = alquiler_vehiculos.vehiculo.Ciudad_Vehiculo " +
                "INNER JOIN alquiler_vehiculos.departamento " +
                "ON alquiler_vehiculos.ciudad.Departamento_Ciudad = alquiler_vehiculos.departamento.Id_Departamento " +
                "INNER JOIN alquiler_vehiculos.tipo_combustible " +
                "ON alquiler_vehiculos.tipo_combustible.Id_TipoCombustible = alquiler_vehiculos.vehiculo.TipoCombustible_Vehiculo " +
                "INNER JOIN alquiler_vehiculos.estado_vehiculo " +
                "ON alquiler_vehiculos.estado_vehiculo.Id_EstadoVehiculo = alquiler_vehiculos.vehiculo.Estado_Vehiculo " +
                "INNER JOIN alquiler_vehiculos.linea_vehiculo " +
                "ON alquiler_vehiculos.linea_vehiculo.Id_LineaVehiculo = alquiler_vehiculos.vehiculo.Linea_Vehiculo " +
                "INNER JOIN alquiler_vehiculos.marca_vehiculo " +
                "ON alquiler_vehiculos.marca_vehiculo.Id_MarcaVehiculo = alquiler_vehiculos.linea_vehiculo.MarcaVehiculo_LineaVehiculo " +
                "INNER JOIN alquiler_vehiculos.tipo_vehiculo " +
                "ON alquiler_vehiculos.tipo_vehiculo.Id_TipoVehiculo = alquiler_vehiculos.marca_vehiculo.TipoVehiculo_MarcaVehiculo " +
                "INNER JOIN alquiler_vehiculos.modelo " + 
                "ON alquiler_vehiculos.modelo.Id_Modelo = alquiler_vehiculos.vehiculo.Modelo_Vehiculo " + 
                "INNER JOIN alquiler_vehiculos.cilindrada " + 
                "ON alquiler_vehiculos.cilindrada.Id_Cilindrada = alquiler_vehiculos.vehiculo.Cilindrada_Vehiculo " + 
                "INNER JOIN alquiler_vehiculos.cantidad_pasajeros " + 
                "ON alquiler_vehiculos.cantidad_pasajeros.Id_CantidadPasajeros = alquiler_vehiculos.vehiculo.CantidadPasajeros_Vehiculo " + 
                "INNER JOIN alquiler_vehiculos.color " + 
                "ON alquiler_vehiculos.color.Id_Color = alquiler_vehiculos.vehiculo.Color_Vehiculo " + 
                "WHERE alquiler_vehiculos.vehiculo.Estado_Vehiculo != 2 " +
                "AND alquiler_vehiculos.marca_vehiculo.Id_MarcaVehiculo = " + Marca + " " +
                "ORDER BY alquiler_vehiculos.vehiculo.Id_Vehiculo ASC";
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
                        ModeloVehiculo Vehiculo = new()
                        {
                            Id = Lector.GetInt32(0),
                            TipoVehiculo = Lector.GetString(1),
                            ClasificacionVehiculo = Lector.GetString(2),
                            Placa = Lector.GetString(3),
                            Modelo = Lector.GetInt32(4),
                            Cilindrada = Lector.GetInt32(5),
                            Color = Lector.GetString(6),
                            CantidadPasajeros = Lector.GetInt32(7),
                            NumeroSeguro = Lector.GetString(8),
                            NumeroCertificadoCDA = Lector.GetString(9),
                            PrecioAlquilerDia = Lector.GetFloat(10),
                            RutaFoto = Lector.GetString(11),
                            Ciudad = Lector.GetString(12) + " - " + Lector.GetString(13),
                            Marca = Lector.GetString(14),
                            Linea = Lector.GetString(15),
                            TipoCombustible = Lector.GetString(16),
                            Estado = Lector.GetString(17),
                            Propietario = Lector.GetInt32(18),
                        };
                        ModeloUsuario Usuario = new();
                        UsuarioPropietario = Usuario.TraerUsuarioPropietario(Lector.GetInt32(18));
                        ListaVehiculos.Add(Vehiculo);
                    }
                }
            }
            catch (Exception) { }
            finally
            {
                ConexionBD.Close();
            }
            return ListaVehiculos;
        }
        public List<ModeloVehiculo> TraerTodosVehiculosFiltroCiudadTipo(int Ciudad, int Tipo)
        {
            List<ModeloVehiculo> ListaVehiculos = [];
            string ConsultaSQL = "SELECT " +
                "alquiler_vehiculos.vehiculo.Id_Vehiculo, " +
                "alquiler_vehiculos.tipo_vehiculo.Nombre_TipoVehiculo, " +
                "alquiler_vehiculos.clasificacion_vehiculo.Nombre_ClasificacionVehiculo, " +
                "alquiler_vehiculos.vehiculo.Placa_Vehiculo, " +
                "alquiler_vehiculos.modelo.Valor_Modelo, " +
                "alquiler_vehiculos.cilindrada.Valor_Cilindrada, " +
                "alquiler_vehiculos.color.Nombre_Color, " +
                "alquiler_vehiculos.cantidad_pasajeros.Valor_CantidadPasajeros, " +
                "alquiler_vehiculos.vehiculo.NumeroSeguro_Vehiculo, " +
                "alquiler_vehiculos.vehiculo.NumeroCertificadoCDA_Vehiculo, " +
                "alquiler_vehiculos.vehiculo.PrecioAlquilerDia_Vehiculo, " +
                "alquiler_vehiculos.vehiculo.Foto_Vehiculo, " +
                "alquiler_vehiculos.ciudad.Nombre_Ciudad, " +
                "alquiler_vehiculos.departamento.Nombre_Departamento, " +
                "alquiler_vehiculos.marca_vehiculo.Nombre_MarcaVehiculo, " +
                "alquiler_vehiculos.linea_vehiculo.Nombre_LineaVehiculo, " +
                "alquiler_vehiculos.tipo_combustible.Nombre_TipoCombustible, " +
                "alquiler_vehiculos.estado_vehiculo.Nombre_EstadoVehiculo, " +
                "alquiler_vehiculos.vehiculo.Propietario_Vehiculo " +
                "FROM alquiler_vehiculos.Vehiculo " +
                "INNER JOIN alquiler_vehiculos.clasificacion_vehiculo " +
                "ON alquiler_vehiculos.vehiculo.Clasificacion_Vehiculo = alquiler_vehiculos.clasificacion_vehiculo.Id_ClasificacionVehiculo " +
                "INNER JOIN alquiler_vehiculos.ciudad " +
                "ON alquiler_vehiculos.ciudad.Id_Ciudad = alquiler_vehiculos.vehiculo.Ciudad_Vehiculo " +
                "INNER JOIN alquiler_vehiculos.departamento " +
                "ON alquiler_vehiculos.ciudad.Departamento_Ciudad = alquiler_vehiculos.departamento.Id_Departamento " +
                "INNER JOIN alquiler_vehiculos.tipo_combustible " +
                "ON alquiler_vehiculos.tipo_combustible.Id_TipoCombustible = alquiler_vehiculos.vehiculo.TipoCombustible_Vehiculo " +
                "INNER JOIN alquiler_vehiculos.estado_vehiculo " +
                "ON alquiler_vehiculos.estado_vehiculo.Id_EstadoVehiculo = alquiler_vehiculos.vehiculo.Estado_Vehiculo " +
                "INNER JOIN alquiler_vehiculos.linea_vehiculo " +
                "ON alquiler_vehiculos.linea_vehiculo.Id_LineaVehiculo = alquiler_vehiculos.vehiculo.Linea_Vehiculo " +
                "INNER JOIN alquiler_vehiculos.marca_vehiculo " +
                "ON alquiler_vehiculos.marca_vehiculo.Id_MarcaVehiculo = alquiler_vehiculos.linea_vehiculo.MarcaVehiculo_LineaVehiculo " +
                "INNER JOIN alquiler_vehiculos.tipo_vehiculo " +
                "ON alquiler_vehiculos.tipo_vehiculo.Id_TipoVehiculo = alquiler_vehiculos.marca_vehiculo.TipoVehiculo_MarcaVehiculo " +
                "INNER JOIN alquiler_vehiculos.modelo " + 
                "ON alquiler_vehiculos.modelo.Id_Modelo = alquiler_vehiculos.vehiculo.Modelo_Vehiculo " + 
                "INNER JOIN alquiler_vehiculos.cilindrada " + 
                "ON alquiler_vehiculos.cilindrada.Id_Cilindrada = alquiler_vehiculos.vehiculo.Cilindrada_Vehiculo " + 
                "INNER JOIN alquiler_vehiculos.cantidad_pasajeros " + 
                "ON alquiler_vehiculos.cantidad_pasajeros.Id_CantidadPasajeros = alquiler_vehiculos.vehiculo.CantidadPasajeros_Vehiculo " + 
                "INNER JOIN alquiler_vehiculos.color " + 
                "ON alquiler_vehiculos.color.Id_Color = alquiler_vehiculos.vehiculo.Color_Vehiculo " + 
                "WHERE alquiler_vehiculos.vehiculo.Estado_Vehiculo != 2 " +
                "AND alquiler_vehiculos.vehiculo.Ciudad_Vehiculo = " + Ciudad + " " +
                "AND alquiler_vehiculos.tipo_vehiculo.Id_TipoVehiculo = " + Tipo + " " +
                "ORDER BY alquiler_vehiculos.vehiculo.Id_Vehiculo ASC";
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
                        ModeloVehiculo Vehiculo = new()
                        {
                            Id = Lector.GetInt32(0),
                            TipoVehiculo = Lector.GetString(1),
                            ClasificacionVehiculo = Lector.GetString(2),
                            Placa = Lector.GetString(3),
                            Modelo = Lector.GetInt32(4),
                            Cilindrada = Lector.GetInt32(5),
                            Color = Lector.GetString(6),
                            CantidadPasajeros = Lector.GetInt32(7),
                            NumeroSeguro = Lector.GetString(8),
                            NumeroCertificadoCDA = Lector.GetString(9),
                            PrecioAlquilerDia = Lector.GetFloat(10),
                            RutaFoto = Lector.GetString(11),
                            Ciudad = Lector.GetString(12) + " - " + Lector.GetString(13),
                            Marca = Lector.GetString(14),
                            Linea = Lector.GetString(15),
                            TipoCombustible = Lector.GetString(16),
                            Estado = Lector.GetString(17),
                            Propietario = Lector.GetInt32(18),
                        };
                        ModeloUsuario Usuario = new();
                        UsuarioPropietario = Usuario.TraerUsuarioPropietario(Lector.GetInt32(18));
                        ListaVehiculos.Add(Vehiculo);
                    }
                }
            }
            catch (Exception) { }
            finally
            {
                ConexionBD.Close();
            }
            return ListaVehiculos;
        }
        public List<ModeloVehiculo> TraerTodosVehiculosFiltroCiudadMarca(int Ciudad, int Marca)
        {
            List<ModeloVehiculo> ListaVehiculos = [];
            string ConsultaSQL = "SELECT " +
                "alquiler_vehiculos.vehiculo.Id_Vehiculo, " +
                "alquiler_vehiculos.tipo_vehiculo.Nombre_TipoVehiculo, " +
                "alquiler_vehiculos.clasificacion_vehiculo.Nombre_ClasificacionVehiculo, " +
                "alquiler_vehiculos.vehiculo.Placa_Vehiculo, " +
                "alquiler_vehiculos.modelo.Valor_Modelo, " +
                "alquiler_vehiculos.cilindrada.Valor_Cilindrada, " +
                "alquiler_vehiculos.color.Nombre_Color, " +
                "alquiler_vehiculos.cantidad_pasajeros.Valor_CantidadPasajeros, " +
                "alquiler_vehiculos.vehiculo.NumeroSeguro_Vehiculo, " +
                "alquiler_vehiculos.vehiculo.NumeroCertificadoCDA_Vehiculo, " +
                "alquiler_vehiculos.vehiculo.PrecioAlquilerDia_Vehiculo, " +
                "alquiler_vehiculos.vehiculo.Foto_Vehiculo, " +
                "alquiler_vehiculos.ciudad.Nombre_Ciudad, " +
                "alquiler_vehiculos.departamento.Nombre_Departamento, " +
                "alquiler_vehiculos.marca_vehiculo.Nombre_MarcaVehiculo, " +
                "alquiler_vehiculos.linea_vehiculo.Nombre_LineaVehiculo, " +
                "alquiler_vehiculos.tipo_combustible.Nombre_TipoCombustible, " +
                "alquiler_vehiculos.estado_vehiculo.Nombre_EstadoVehiculo, " +
                "alquiler_vehiculos.vehiculo.Propietario_Vehiculo " +
                "FROM alquiler_vehiculos.Vehiculo " +
                "INNER JOIN alquiler_vehiculos.clasificacion_vehiculo " +
                "ON alquiler_vehiculos.vehiculo.Clasificacion_Vehiculo = alquiler_vehiculos.clasificacion_vehiculo.Id_ClasificacionVehiculo " +
                "INNER JOIN alquiler_vehiculos.ciudad " +
                "ON alquiler_vehiculos.ciudad.Id_Ciudad = alquiler_vehiculos.vehiculo.Ciudad_Vehiculo " +
                "INNER JOIN alquiler_vehiculos.departamento " +
                "ON alquiler_vehiculos.ciudad.Departamento_Ciudad = alquiler_vehiculos.departamento.Id_Departamento " +
                "INNER JOIN alquiler_vehiculos.tipo_combustible " +
                "ON alquiler_vehiculos.tipo_combustible.Id_TipoCombustible = alquiler_vehiculos.vehiculo.TipoCombustible_Vehiculo " +
                "INNER JOIN alquiler_vehiculos.estado_vehiculo " +
                "ON alquiler_vehiculos.estado_vehiculo.Id_EstadoVehiculo = alquiler_vehiculos.vehiculo.Estado_Vehiculo " +
                "INNER JOIN alquiler_vehiculos.linea_vehiculo " +
                "ON alquiler_vehiculos.linea_vehiculo.Id_LineaVehiculo = alquiler_vehiculos.vehiculo.Linea_Vehiculo " +
                "INNER JOIN alquiler_vehiculos.marca_vehiculo " +
                "ON alquiler_vehiculos.marca_vehiculo.Id_MarcaVehiculo = alquiler_vehiculos.linea_vehiculo.MarcaVehiculo_LineaVehiculo " +
                "INNER JOIN alquiler_vehiculos.tipo_vehiculo " +
                "ON alquiler_vehiculos.tipo_vehiculo.Id_TipoVehiculo = alquiler_vehiculos.marca_vehiculo.TipoVehiculo_MarcaVehiculo " +
                "INNER JOIN alquiler_vehiculos.modelo " + 
                "ON alquiler_vehiculos.modelo.Id_Modelo = alquiler_vehiculos.vehiculo.Modelo_Vehiculo " + 
                "INNER JOIN alquiler_vehiculos.cilindrada " + 
                "ON alquiler_vehiculos.cilindrada.Id_Cilindrada = alquiler_vehiculos.vehiculo.Cilindrada_Vehiculo " + 
                "INNER JOIN alquiler_vehiculos.cantidad_pasajeros " + 
                "ON alquiler_vehiculos.cantidad_pasajeros.Id_CantidadPasajeros = alquiler_vehiculos.vehiculo.CantidadPasajeros_Vehiculo " + 
                "INNER JOIN alquiler_vehiculos.color " + 
                "ON alquiler_vehiculos.color.Id_Color = alquiler_vehiculos.vehiculo.Color_Vehiculo " + 
                "WHERE alquiler_vehiculos.vehiculo.Estado_Vehiculo != 2 " +
                "AND alquiler_vehiculos.vehiculo.Ciudad_Vehiculo = " + Ciudad + " " +
                "AND alquiler_vehiculos.marca_vehiculo.Id_MarcaVehiculo = " + Marca + " " +
                "ORDER BY alquiler_vehiculos.vehiculo.Id_Vehiculo ASC";
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
                        ModeloVehiculo Vehiculo = new()
                        {
                            Id = Lector.GetInt32(0),
                            TipoVehiculo = Lector.GetString(1),
                            ClasificacionVehiculo = Lector.GetString(2),
                            Placa = Lector.GetString(3),
                            Modelo = Lector.GetInt32(4),
                            Cilindrada = Lector.GetInt32(5),
                            Color = Lector.GetString(6),
                            CantidadPasajeros = Lector.GetInt32(7),
                            NumeroSeguro = Lector.GetString(8),
                            NumeroCertificadoCDA = Lector.GetString(9),
                            PrecioAlquilerDia = Lector.GetFloat(10),
                            RutaFoto = Lector.GetString(11),
                            Ciudad = Lector.GetString(12) + " - " + Lector.GetString(13),
                            Marca = Lector.GetString(14),
                            Linea = Lector.GetString(15),
                            TipoCombustible = Lector.GetString(16),
                            Estado = Lector.GetString(17),
                            Propietario = Lector.GetInt32(18),
                        };
                        ModeloUsuario Usuario = new();
                        UsuarioPropietario = Usuario.TraerUsuarioPropietario(Lector.GetInt32(18));
                        ListaVehiculos.Add(Vehiculo);
                    }
                }
            }
            catch (Exception) { }
            finally
            {
                ConexionBD.Close();
            }
            return ListaVehiculos;
        }
        public List<ModeloVehiculo> TraerTodosVehiculosFiltroTipoMarca(int Tipo, int Marca)
        {
            List<ModeloVehiculo> ListaVehiculos = [];
            string ConsultaSQL = "SELECT " +
                "alquiler_vehiculos.vehiculo.Id_Vehiculo, " +
                "alquiler_vehiculos.tipo_vehiculo.Nombre_TipoVehiculo, " +
                "alquiler_vehiculos.clasificacion_vehiculo.Nombre_ClasificacionVehiculo, " +
                "alquiler_vehiculos.vehiculo.Placa_Vehiculo, " +
                "alquiler_vehiculos.modelo.Valor_Modelo, " +
                "alquiler_vehiculos.cilindrada.Valor_Cilindrada, " +
                "alquiler_vehiculos.color.Nombre_Color, " +
                "alquiler_vehiculos.cantidad_pasajeros.Valor_CantidadPasajeros, " +
                "alquiler_vehiculos.vehiculo.NumeroSeguro_Vehiculo, " +
                "alquiler_vehiculos.vehiculo.NumeroCertificadoCDA_Vehiculo, " +
                "alquiler_vehiculos.vehiculo.PrecioAlquilerDia_Vehiculo, " +
                "alquiler_vehiculos.vehiculo.Foto_Vehiculo, " +
                "alquiler_vehiculos.ciudad.Nombre_Ciudad, " +
                "alquiler_vehiculos.departamento.Nombre_Departamento, " +
                "alquiler_vehiculos.marca_vehiculo.Nombre_MarcaVehiculo, " +
                "alquiler_vehiculos.linea_vehiculo.Nombre_LineaVehiculo, " +
                "alquiler_vehiculos.tipo_combustible.Nombre_TipoCombustible, " +
                "alquiler_vehiculos.estado_vehiculo.Nombre_EstadoVehiculo, " +
                "alquiler_vehiculos.vehiculo.Propietario_Vehiculo " +
                "FROM alquiler_vehiculos.Vehiculo " +
                "INNER JOIN alquiler_vehiculos.clasificacion_vehiculo " +
                "ON alquiler_vehiculos.vehiculo.Clasificacion_Vehiculo = alquiler_vehiculos.clasificacion_vehiculo.Id_ClasificacionVehiculo " +
                "INNER JOIN alquiler_vehiculos.ciudad " +
                "ON alquiler_vehiculos.ciudad.Id_Ciudad = alquiler_vehiculos.vehiculo.Ciudad_Vehiculo " +
                "INNER JOIN alquiler_vehiculos.departamento " +
                "ON alquiler_vehiculos.ciudad.Departamento_Ciudad = alquiler_vehiculos.departamento.Id_Departamento " +
                "INNER JOIN alquiler_vehiculos.tipo_combustible " +
                "ON alquiler_vehiculos.tipo_combustible.Id_TipoCombustible = alquiler_vehiculos.vehiculo.TipoCombustible_Vehiculo " +
                "INNER JOIN alquiler_vehiculos.estado_vehiculo " +
                "ON alquiler_vehiculos.estado_vehiculo.Id_EstadoVehiculo = alquiler_vehiculos.vehiculo.Estado_Vehiculo " +
                "INNER JOIN alquiler_vehiculos.linea_vehiculo " +
                "ON alquiler_vehiculos.linea_vehiculo.Id_LineaVehiculo = alquiler_vehiculos.vehiculo.Linea_Vehiculo " +
                "INNER JOIN alquiler_vehiculos.marca_vehiculo " +
                "ON alquiler_vehiculos.marca_vehiculo.Id_MarcaVehiculo = alquiler_vehiculos.linea_vehiculo.MarcaVehiculo_LineaVehiculo " +
                "INNER JOIN alquiler_vehiculos.tipo_vehiculo " +
                "INNER JOIN alquiler_vehiculos.modelo " + 
                "ON alquiler_vehiculos.modelo.Id_Modelo = alquiler_vehiculos.vehiculo.Modelo_Vehiculo " + 
                "INNER JOIN alquiler_vehiculos.cilindrada " + 
                "ON alquiler_vehiculos.cilindrada.Id_Cilindrada = alquiler_vehiculos.vehiculo.Cilindrada_Vehiculo " + 
                "INNER JOIN alquiler_vehiculos.cantidad_pasajeros " + 
                "ON alquiler_vehiculos.cantidad_pasajeros.Id_CantidadPasajeros = alquiler_vehiculos.vehiculo.CantidadPasajeros_Vehiculo " + 
                "INNER JOIN alquiler_vehiculos.color " + 
                "ON alquiler_vehiculos.color.Id_Color = alquiler_vehiculos.vehiculo.Color_Vehiculo " + 
                "INNER JOIN alquiler_vehiculos.modelo " + 
                "ON alquiler_vehiculos.modelo.Id_Modelo = alquiler_vehiculos.vehiculo.Modelo_Vehiculo " + 
                "INNER JOIN alquiler_vehiculos.cilindrada " + 
                "ON alquiler_vehiculos.cilindrada.Id_Cilindrada = alquiler_vehiculos.vehiculo.Cilindrada_Vehiculo " + 
                "INNER JOIN alquiler_vehiculos.cantidad_pasajeros " + 
                "ON alquiler_vehiculos.cantidad_pasajeros.Id_CantidadPasajeros = alquiler_vehiculos.vehiculo.CantidadPasajeros_Vehiculo " + 
                "INNER JOIN alquiler_vehiculos.color " + 
                "ON alquiler_vehiculos.color.Id_Color = alquiler_vehiculos.vehiculo.Color_Vehiculo " + 
                "ON alquiler_vehiculos.tipo_vehiculo.Id_TipoVehiculo = alquiler_vehiculos.marca_vehiculo.TipoVehiculo_MarcaVehiculo " +
                "INNER JOIN alquiler_vehiculos.modelo " + 
                "ON alquiler_vehiculos.modelo.Id_Modelo = alquiler_vehiculos.vehiculo.Modelo_Vehiculo " + 
                "INNER JOIN alquiler_vehiculos.cilindrada " + 
                "ON alquiler_vehiculos.cilindrada.Id_Cilindrada = alquiler_vehiculos.vehiculo.Cilindrada_Vehiculo " + 
                "INNER JOIN alquiler_vehiculos.cantidad_pasajeros " + 
                "ON alquiler_vehiculos.cantidad_pasajeros.Id_CantidadPasajeros = alquiler_vehiculos.vehiculo.CantidadPasajeros_Vehiculo " + 
                "INNER JOIN alquiler_vehiculos.color " + 
                "ON alquiler_vehiculos.color.Id_Color = alquiler_vehiculos.vehiculo.Color_Vehiculo " + "WHERE alquiler_vehiculos.vehiculo.Estado_Vehiculo != 2 " +
                "AND alquiler_vehiculos.tipo_vehiculo.Id_TipoVehiculo = " + Tipo + " " +
                "AND alquiler_vehiculos.marca_vehiculo.Id_MarcaVehiculo = " + Marca + " " +
                "ORDER BY alquiler_vehiculos.vehiculo.Id_Vehiculo ASC";
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
                        ModeloVehiculo Vehiculo = new()
                        {
                            Id = Lector.GetInt32(0),
                            TipoVehiculo = Lector.GetString(1),
                            ClasificacionVehiculo = Lector.GetString(2),
                            Placa = Lector.GetString(3),
                            Modelo = Lector.GetInt32(4),
                            Cilindrada = Lector.GetInt32(5),
                            Color = Lector.GetString(6),
                            CantidadPasajeros = Lector.GetInt32(7),
                            NumeroSeguro = Lector.GetString(8),
                            NumeroCertificadoCDA = Lector.GetString(9),
                            PrecioAlquilerDia = Lector.GetFloat(10),
                            RutaFoto = Lector.GetString(11),
                            Ciudad = Lector.GetString(12) + " - " + Lector.GetString(13),
                            Marca = Lector.GetString(14),
                            Linea = Lector.GetString(15),
                            TipoCombustible = Lector.GetString(16),
                            Estado = Lector.GetString(17),
                            Propietario = Lector.GetInt32(18),
                        };
                        ModeloUsuario Usuario = new();
                        UsuarioPropietario = Usuario.TraerUsuarioPropietario(Lector.GetInt32(18));
                        ListaVehiculos.Add(Vehiculo);
                    }
                }
            }
            catch (Exception) { }
            finally
            {
                ConexionBD.Close();
            }
            return ListaVehiculos;
        }

        public List<ModeloVehiculo> TraerTodosVehiculosPropietario(int IdUsuario)
        {
            List<ModeloVehiculo> ListaVehiculos = [];
            ModeloPropietario ModeloPropietario = new();
            if (!ModeloPropietario.ValidarPropietario(IdUsuario))
            {
                ModeloPropietario = ModeloPropietario.TraerPropietarioUsuario(IdUsuario);
                string ConsultaSQL = "SELECT " +
                "alquiler_vehiculos.vehiculo.Id_Vehiculo, " +
                "alquiler_vehiculos.tipo_vehiculo.Nombre_TipoVehiculo, " +
                "alquiler_vehiculos.clasificacion_vehiculo.Nombre_ClasificacionVehiculo, " +
                "alquiler_vehiculos.vehiculo.Placa_Vehiculo, " +
                "alquiler_vehiculos.modelo.Valor_Modelo, " +
                "alquiler_vehiculos.cilindrada.Valor_Cilindrada, " +
                "alquiler_vehiculos.color.Nombre_Color, " +
                "alquiler_vehiculos.cantidad_pasajeros.Valor_CantidadPasajeros, " +
                "alquiler_vehiculos.vehiculo.NumeroSeguro_Vehiculo, " +
                "alquiler_vehiculos.vehiculo.NumeroCertificadoCDA_Vehiculo, " +
                "alquiler_vehiculos.vehiculo.PrecioAlquilerDia_Vehiculo, " +
                "alquiler_vehiculos.vehiculo.Foto_Vehiculo, " +
                "alquiler_vehiculos.ciudad.Nombre_Ciudad, " +
                "alquiler_vehiculos.departamento.Nombre_Departamento, " +
                "alquiler_vehiculos.marca_vehiculo.Nombre_MarcaVehiculo, " +
                "alquiler_vehiculos.linea_vehiculo.Nombre_LineaVehiculo, " +
                "alquiler_vehiculos.tipo_combustible.Nombre_TipoCombustible, " +
                "alquiler_vehiculos.estado_vehiculo.Nombre_EstadoVehiculo, " +
                "alquiler_vehiculos.vehiculo.Propietario_Vehiculo " +
                "FROM alquiler_vehiculos.vehiculo " +
                "INNER JOIN alquiler_vehiculos.clasificacion_vehiculo " +
                "ON alquiler_vehiculos.vehiculo.Clasificacion_Vehiculo = alquiler_vehiculos.clasificacion_vehiculo.Id_ClasificacionVehiculo " +
                "INNER JOIN alquiler_vehiculos.ciudad " +
                "ON alquiler_vehiculos.ciudad.Id_Ciudad = alquiler_vehiculos.vehiculo.Ciudad_Vehiculo " +
                "INNER JOIN alquiler_vehiculos.departamento " +
                "ON alquiler_vehiculos.ciudad.Departamento_Ciudad = alquiler_vehiculos.departamento.Id_Departamento " +
                "INNER JOIN alquiler_vehiculos.tipo_combustible " +
                "ON alquiler_vehiculos.tipo_combustible.Id_TipoCombustible = alquiler_vehiculos.vehiculo.TipoCombustible_Vehiculo " +
                "INNER JOIN alquiler_vehiculos.estado_vehiculo " +
                "ON alquiler_vehiculos.estado_vehiculo.Id_EstadoVehiculo = alquiler_vehiculos.vehiculo.Estado_Vehiculo " +
                "INNER JOIN alquiler_vehiculos.linea_vehiculo " +
                "ON alquiler_vehiculos.linea_vehiculo.Id_LineaVehiculo = alquiler_vehiculos.vehiculo.Linea_Vehiculo " +
                "INNER JOIN alquiler_vehiculos.marca_vehiculo " +
                "ON alquiler_vehiculos.marca_vehiculo.Id_MarcaVehiculo = alquiler_vehiculos.linea_vehiculo.MarcaVehiculo_LineaVehiculo " +
                "INNER JOIN alquiler_vehiculos.tipo_vehiculo " +
                "ON alquiler_vehiculos.tipo_vehiculo.Id_TipoVehiculo = alquiler_vehiculos.marca_vehiculo.TipoVehiculo_MarcaVehiculo " +
                "INNER JOIN alquiler_vehiculos.modelo " + 
                "ON alquiler_vehiculos.modelo.Id_Modelo = alquiler_vehiculos.vehiculo.Modelo_Vehiculo " + 
                "INNER JOIN alquiler_vehiculos.cilindrada " + 
                "ON alquiler_vehiculos.cilindrada.Id_Cilindrada = alquiler_vehiculos.vehiculo.Cilindrada_Vehiculo " + 
                "INNER JOIN alquiler_vehiculos.cantidad_pasajeros " + 
                "ON alquiler_vehiculos.cantidad_pasajeros.Id_CantidadPasajeros = alquiler_vehiculos.vehiculo.CantidadPasajeros_Vehiculo " + 
                "INNER JOIN alquiler_vehiculos.color " + 
                "ON alquiler_vehiculos.color.Id_Color = alquiler_vehiculos.vehiculo.Color_Vehiculo " + 
                "WHERE alquiler_vehiculos.vehiculo.Propietario_Vehiculo = " + ModeloPropietario.Id + " " +
                "ORDER BY alquiler_vehiculos.vehiculo.Id_Vehiculo ASC";
                MySqlConnection ConexionDB = ModeloConexion.Conect();
                try
                {
                    ConexionDB.Open();
                    MySqlCommand Comando = new(ConsultaSQL, ConexionDB);
                    MySqlDataReader Lector = Comando.ExecuteReader();
                    if (Lector.HasRows)
                    {
                        while (Lector.Read())
                        {
                            ModeloVehiculo Vehiculo = new()
                            {
                                Id = Lector.GetInt32(0),
                                TipoVehiculo = Lector.GetString(1),
                                ClasificacionVehiculo = Lector.GetString(2),
                                Placa = Lector.GetString(3),
                                Modelo = Lector.GetInt32(4),
                                Cilindrada = Lector.GetInt32(5),
                                Color = Lector.GetString(6),
                                CantidadPasajeros = Lector.GetInt32(7),
                                NumeroSeguro = Lector.GetString(8),
                                NumeroCertificadoCDA = Lector.GetString(9),
                                PrecioAlquilerDia = Lector.GetFloat(10),
                                RutaFoto = Lector.GetString(11),
                                Ciudad = Lector.GetString(12) + " - " + Lector.GetString(13),
                                Marca = Lector.GetString(14),
                                Linea = Lector.GetString(15),
                                TipoCombustible = Lector.GetString(16),
                                Estado = Lector.GetString(17),
                                Propietario = Lector.GetInt32(18),
                            };
                            ModeloUsuario Usuario = new();
                            UsuarioPropietario = Usuario.TraerUsuarioPropietario(Lector.GetInt32(18));
                            ListaVehiculos.Add(Vehiculo);
                        }
                    }
                }
                catch (Exception) { }
                finally
                {
                    ConexionDB.Close();
                }
            }
            return ListaVehiculos;
        }
        public ModeloVehiculo TraerVehiculo(int IdVehiculo)
        {
            ModeloVehiculo Vehiculo = new();
            string ConsultaSQL = "SELECT " +
                "alquiler_vehiculos.vehiculo.Id_Vehiculo, " +
                "alquiler_vehiculos.tipo_vehiculo.Nombre_TipoVehiculo, " +
                "alquiler_vehiculos.clasificacion_vehiculo.Nombre_ClasificacionVehiculo, " +
                "alquiler_vehiculos.vehiculo.Placa_Vehiculo, " +
                "alquiler_vehiculos.modelo.Valor_Modelo, " +
                "alquiler_vehiculos.cilindrada.Valor_Cilindrada, " +
                "alquiler_vehiculos.color.Nombre_Color, " +
                "alquiler_vehiculos.cantidad_pasajeros.Valor_CantidadPasajeros, " +
                "alquiler_vehiculos.vehiculo.NumeroSeguro_Vehiculo, " +
                "alquiler_vehiculos.vehiculo.NumeroCertificadoCDA_Vehiculo, " +
                "alquiler_vehiculos.vehiculo.PrecioAlquilerDia_Vehiculo, " +
                "alquiler_vehiculos.vehiculo.Foto_Vehiculo, " +
                "alquiler_vehiculos.ciudad.Nombre_Ciudad, " +
                "alquiler_vehiculos.departamento.Nombre_Departamento, " +
                "alquiler_vehiculos.marca_vehiculo.Nombre_MarcaVehiculo, " +
                "alquiler_vehiculos.linea_vehiculo.Nombre_LineaVehiculo, " +
                "alquiler_vehiculos.tipo_combustible.Nombre_TipoCombustible, " +
                "alquiler_vehiculos.estado_vehiculo.Nombre_EstadoVehiculo, " +
                "alquiler_vehiculos.vehiculo.Propietario_Vehiculo " +
                "FROM alquiler_vehiculos.vehiculo " +
                "INNER JOIN alquiler_vehiculos.clasificacion_vehiculo " +
                "ON alquiler_vehiculos.vehiculo.Clasificacion_Vehiculo = alquiler_vehiculos.clasificacion_vehiculo.Id_ClasificacionVehiculo " +
                "INNER JOIN alquiler_vehiculos.ciudad " +
                "ON alquiler_vehiculos.ciudad.Id_Ciudad = alquiler_vehiculos.vehiculo.Ciudad_Vehiculo " +
                "INNER JOIN alquiler_vehiculos.departamento " +
                "ON alquiler_vehiculos.ciudad.Departamento_Ciudad = alquiler_vehiculos.departamento.Id_Departamento " +
                "INNER JOIN alquiler_vehiculos.tipo_combustible " +
                "ON alquiler_vehiculos.tipo_combustible.Id_TipoCombustible = alquiler_vehiculos.vehiculo.TipoCombustible_Vehiculo " +
                "INNER JOIN alquiler_vehiculos.estado_vehiculo " +
                "ON alquiler_vehiculos.estado_vehiculo.Id_EstadoVehiculo = alquiler_vehiculos.vehiculo.Estado_Vehiculo " +
                "INNER JOIN alquiler_vehiculos.linea_vehiculo " +
                "ON alquiler_vehiculos.linea_vehiculo.Id_LineaVehiculo = alquiler_vehiculos.vehiculo.Linea_Vehiculo " +
                "INNER JOIN alquiler_vehiculos.marca_vehiculo " +
                "ON alquiler_vehiculos.marca_vehiculo.Id_MarcaVehiculo = alquiler_vehiculos.linea_vehiculo.MarcaVehiculo_LineaVehiculo " +
                "INNER JOIN alquiler_vehiculos.tipo_vehiculo " +
                "ON alquiler_vehiculos.tipo_vehiculo.Id_TipoVehiculo = alquiler_vehiculos.marca_vehiculo.TipoVehiculo_MarcaVehiculo " +
                "INNER JOIN alquiler_vehiculos.modelo " + 
                "ON alquiler_vehiculos.modelo.Id_Modelo = alquiler_vehiculos.vehiculo.Modelo_Vehiculo " + 
                "INNER JOIN alquiler_vehiculos.cilindrada " + 
                "ON alquiler_vehiculos.cilindrada.Id_Cilindrada = alquiler_vehiculos.vehiculo.Cilindrada_Vehiculo " + 
                "INNER JOIN alquiler_vehiculos.cantidad_pasajeros " + 
                "ON alquiler_vehiculos.cantidad_pasajeros.Id_CantidadPasajeros = alquiler_vehiculos.vehiculo.CantidadPasajeros_Vehiculo " + 
                "INNER JOIN alquiler_vehiculos.color " + 
                "ON alquiler_vehiculos.color.Id_Color = alquiler_vehiculos.vehiculo.Color_Vehiculo " + 
                "WHERE alquiler_vehiculos.vehiculo.Id_Vehiculo = " + IdVehiculo + " ";
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
                        Vehiculo = new()
                        {
                            Id = Lector.GetInt32(0),
                            TipoVehiculo = Lector.GetString(1),
                            ClasificacionVehiculo = Lector.GetString(2),
                            Placa = Lector.GetString(3),
                            Modelo = Lector.GetInt32(4),
                            Cilindrada = Lector.GetInt32(5),
                            Color = Lector.GetString(6),
                            CantidadPasajeros = Lector.GetInt32(7),
                            NumeroSeguro = Lector.GetString(8),
                            NumeroCertificadoCDA = Lector.GetString(9),
                            PrecioAlquilerDia = Lector.GetFloat(10),
                            RutaFoto = Lector.GetString(11),
                            Ciudad = Lector.GetString(12) + " - " + Lector.GetString(13),
                            Marca = Lector.GetString(14),
                            Linea = Lector.GetString(15),
                            TipoCombustible = Lector.GetString(16),
                            Estado = Lector.GetString(17),
                            Propietario = Lector.GetInt32(18),
                        };
                        ModeloUsuario Usuario = new();
                        Vehiculo.UsuarioPropietario = UsuarioPropietario = Usuario.TraerUsuarioPropietario(Lector.GetInt32(18));
                    }
                }
            }
            catch (Exception) { }
            finally
            {
                ConexionBD.Close();
            }
            return Vehiculo;
        }

        public bool ValidarVehiculo(string Placa)
        {
            ModeloVehiculo Vehiculo = null;
            string ConsultaSQL = "SELECT " +
                "alquiler_vehiculos.vehiculo.Id_Vehiculo, " +
                "alquiler_vehiculos.tipo_vehiculo.Nombre_TipoVehiculo, " +
                "alquiler_vehiculos.clasificacion_vehiculo.Nombre_ClasificacionVehiculo, " +
                "alquiler_vehiculos.vehiculo.Placa_Vehiculo, " +
                "alquiler_vehiculos.modelo.Valor_Modelo, " +
                "alquiler_vehiculos.cilindrada.Valor_Cilindrada, " +
                "alquiler_vehiculos.color.Nombre_Color, " +
                "alquiler_vehiculos.cantidad_pasajeros.Valor_CantidadPasajeros, " +
                "alquiler_vehiculos.vehiculo.NumeroSeguro_Vehiculo, " +
                "alquiler_vehiculos.vehiculo.NumeroCertificadoCDA_Vehiculo, " +
                "alquiler_vehiculos.vehiculo.PrecioAlquilerDia_Vehiculo, " +
                "alquiler_vehiculos.vehiculo.Foto_Vehiculo, " +
                "alquiler_vehiculos.ciudad.Nombre_Ciudad, " +
                "alquiler_vehiculos.departamento.Nombre_Departamento, " +
                "alquiler_vehiculos.marca_vehiculo.Nombre_MarcaVehiculo, " +
                "alquiler_vehiculos.linea_vehiculo.Nombre_LineaVehiculo, " +
                "alquiler_vehiculos.tipo_combustible.Nombre_TipoCombustible, " +
                "alquiler_vehiculos.estado_vehiculo.Nombre_EstadoVehiculo, " +
                "alquiler_vehiculos.vehiculo.Propietario_Vehiculo " +
                "FROM alquiler_vehiculos.vehiculo " +
                "INNER JOIN alquiler_vehiculos.clasificacion_vehiculo " +
                "ON alquiler_vehiculos.vehiculo.Clasificacion_Vehiculo = alquiler_vehiculos.clasificacion_vehiculo.Id_ClasificacionVehiculo " +
                "INNER JOIN alquiler_vehiculos.ciudad " +
                "ON alquiler_vehiculos.ciudad.Id_Ciudad = alquiler_vehiculos.vehiculo.Ciudad_Vehiculo " +
                "INNER JOIN alquiler_vehiculos.departamento " +
                "ON alquiler_vehiculos.ciudad.Departamento_Ciudad = alquiler_vehiculos.departamento.Id_Departamento " +
                "INNER JOIN alquiler_vehiculos.tipo_combustible " +
                "ON alquiler_vehiculos.tipo_combustible.Id_TipoCombustible = alquiler_vehiculos.vehiculo.TipoCombustible_Vehiculo " +
                "INNER JOIN alquiler_vehiculos.estado_vehiculo " +
                "ON alquiler_vehiculos.estado_vehiculo.Id_EstadoVehiculo = alquiler_vehiculos.vehiculo.Estado_Vehiculo " +
                "INNER JOIN alquiler_vehiculos.linea_vehiculo " +
                "ON alquiler_vehiculos.linea_vehiculo.Id_LineaVehiculo = alquiler_vehiculos.vehiculo.Linea_Vehiculo " +
                "INNER JOIN alquiler_vehiculos.marca_vehiculo " +
                "ON alquiler_vehiculos.marca_vehiculo.Id_MarcaVehiculo = alquiler_vehiculos.linea_vehiculo.MarcaVehiculo_LineaVehiculo " +
                "INNER JOIN alquiler_vehiculos.tipo_vehiculo " +
                "ON alquiler_vehiculos.tipo_vehiculo.Id_TipoVehiculo = alquiler_vehiculos.marca_vehiculo.TipoVehiculo_MarcaVehiculo " +
                "INNER JOIN alquiler_vehiculos.modelo " + 
                "ON alquiler_vehiculos.modelo.Id_Modelo = alquiler_vehiculos.vehiculo.Modelo_Vehiculo " + 
                "INNER JOIN alquiler_vehiculos.cilindrada " + 
                "ON alquiler_vehiculos.cilindrada.Id_Cilindrada = alquiler_vehiculos.vehiculo.Cilindrada_Vehiculo " + 
                "INNER JOIN alquiler_vehiculos.cantidad_pasajeros " + 
                "ON alquiler_vehiculos.cantidad_pasajeros.Id_CantidadPasajeros = alquiler_vehiculos.vehiculo.CantidadPasajeros_Vehiculo " + 
                "INNER JOIN alquiler_vehiculos.color " + 
                "ON alquiler_vehiculos.color.Id_Color = alquiler_vehiculos.vehiculo.Color_Vehiculo " + 
                "WHERE alquiler_vehiculos.vehiculo.Placa_Vehiculo = '" + Placa + "' ";
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
                        Vehiculo = new()
                        {
                            Id = Lector.GetInt32(0),
                            TipoVehiculo = Lector.GetString(1),
                            ClasificacionVehiculo = Lector.GetString(2),
                            Placa = Lector.GetString(3),
                            Modelo = Lector.GetInt32(4),
                            Cilindrada = Lector.GetInt32(5),
                            Color = Lector.GetString(6),
                            CantidadPasajeros = Lector.GetInt32(7),
                            NumeroSeguro = Lector.GetString(8),
                            NumeroCertificadoCDA = Lector.GetString(9),
                            PrecioAlquilerDia = Lector.GetFloat(10),
                            RutaFoto = Lector.GetString(11),
                            Ciudad = Lector.GetString(12) + " - " + Lector.GetString(13),
                            Marca = Lector.GetString(14),
                            Linea = Lector.GetString(15),
                            TipoCombustible = Lector.GetString(16),
                            Estado = Lector.GetString(17),
                            Propietario = Lector.GetInt32(18),
                        };
                        ModeloUsuario Usuario = new();
                        UsuarioPropietario = Usuario.TraerUsuarioPropietario(Lector.GetInt32(18));
                    }
                }
            }
            catch (Exception) { }
            finally
            {
                ConexionBD.Close();
            }
            return Vehiculo == null;
        }

        public bool ActualizarVehiculo(int Id, string Placa, int Cilindrada, int Modelo, float PrecioAlquilerDia, int Color, int CantidadPasajeros, int ClasificacionVehiculo, int Linea, string NumeroSeguro, string NumeroCertificadoCDA, int TipoCombustible, int Ciudad)
        {
            string ConsultaSQL = "UPDATE " +
                                "alquiler_vehiculos.vehiculo " +
                                "SET " +
                                "alquiler_vehiculos.vehiculo.Placa_Vehiculo = '" + Placa + "', " +
                                "alquiler_vehiculos.vehiculo.Modelo_Vehiculo = " + Modelo + ", " +
                                "alquiler_vehiculos.vehiculo.Cilindrada_Vehiculo = " + Cilindrada + ", " +
                                "alquiler_vehiculos.vehiculo.Color_Vehiculo = " + Color + ", " +
                                "alquiler_vehiculos.vehiculo.CantidadPasajeros_Vehiculo = " + CantidadPasajeros + ", " +
                                "alquiler_vehiculos.vehiculo.NumeroSeguro_Vehiculo = '" + NumeroSeguro + "', " +
                                "alquiler_vehiculos.vehiculo.NumeroCertificadoCDA_Vehiculo = '" + NumeroCertificadoCDA + "', " +
                                "alquiler_vehiculos.vehiculo.PrecioAlquilerDia_Vehiculo = " + PrecioAlquilerDia + ", " +
                                "alquiler_vehiculos.vehiculo.Ciudad_Vehiculo = " + Ciudad + ", " +
                                "alquiler_vehiculos.vehiculo.Clasificacion_Vehiculo = " + ClasificacionVehiculo + ", " +
                                "alquiler_vehiculos.vehiculo.Linea_Vehiculo = " + Linea + ", " +
                                "alquiler_vehiculos.vehiculo.TipoCombustible_Vehiculo = " + TipoCombustible + " " +
                                "WHERE (alquiler_vehiculos.vehiculo.Id_Vehiculo = " + Id + " )";
            return ModeloConexion.ExecuteNonQuerySentence(ConsultaSQL);
        }
        public bool ActualizarVehiculoConFoto(int Id, string Placa, int Cilindrada, int Modelo, float PrecioAlquilerDia, int Color, int CantidadPasajeros, int ClasificacionVehiculo, int Linea, string NumeroSeguro, string NumeroCertificadoCDA, int TipoCombustible, int Ciudad, string Foto)
        {
            string ConsultaSQL = "UPDATE " +
                                "alquiler_vehiculos.vehiculo " +
                                "SET " +
                                "alquiler_vehiculos.vehiculo.Placa_Vehiculo = '" + Placa + "', " +
                                "alquiler_vehiculos.vehiculo.Modelo_Vehiculo = " + Modelo + ", " +
                                "alquiler_vehiculos.vehiculo.Cilindrada_Vehiculo = " + Cilindrada + ", " +
                                "alquiler_vehiculos.vehiculo.Color_Vehiculo = " + Color + ", " +
                                "alquiler_vehiculos.vehiculo.CantidadPasajeros_Vehiculo = " + CantidadPasajeros + ", " +
                                "alquiler_vehiculos.vehiculo.NumeroSeguro_Vehiculo = '" + NumeroSeguro + "', " +
                                "alquiler_vehiculos.vehiculo.NumeroCertificadoCDA_Vehiculo = '" + NumeroCertificadoCDA + "', " +
                                "alquiler_vehiculos.vehiculo.PrecioAlquilerDia_Vehiculo = " + PrecioAlquilerDia + ", " +
                                "alquiler_vehiculos.vehiculo.Ciudad_Vehiculo = " + Ciudad + ", " +
                                "alquiler_vehiculos.vehiculo.Clasificacion_Vehiculo = " + ClasificacionVehiculo + ", " +
                                "alquiler_vehiculos.vehiculo.Linea_Vehiculo = " + Linea + ", " +
                                "alquiler_vehiculos.vehiculo.TipoCombustible_Vehiculo = " + TipoCombustible + ", " +
                                "alquiler_vehiculos.vehiculo.Foto_Vehiculo = '" + Foto + "' " +
                                "WHERE (alquiler_vehiculos.vehiculo.Id_Vehiculo = " + Id + " )";
            return ModeloConexion.ExecuteNonQuerySentence(ConsultaSQL);
        }
        public bool EliminarVehiculo(int IdVehiculo)
        {
            string ConsultaSQL = "UPDATE " +
                                "alquiler_vehiculos.vehiculo " +
                                "SET " +
                                "alquiler_vehiculos.vehiculo.Estado_Vehiculo = 2 " +
                                "WHERE " +
                                "(alquiler_vehiculos.vehiculo.Id_Vehiculo = " + IdVehiculo + ")";
            return ModeloConexion.ExecuteNonQuerySentence(ConsultaSQL);
        }
        public bool ActivarVehiculo(int IdVehiculo)
        {
            string ConsultaSQL = "UPDATE " +
                                "alquiler_vehiculos.vehiculo " +
                                "SET " +
                                "alquiler_vehiculos.vehiculo.Estado_Vehiculo = 1 " +
                                "WHERE " +
                                "(alquiler_vehiculos.vehiculo.Id_Vehiculo = " + IdVehiculo + ")";
            return ModeloConexion.ExecuteNonQuerySentence(ConsultaSQL);
        }
        public bool RegistrarVehiculo(string Placa, int Cilindrada, int Modelo, float PrecioAlquilerDia, int Color, int CantidadPasajeros, int ClasificacionVehiculo, int Linea, string NumeroCertificadoCDA, string NumeroSeguro, int TipoCombustible, int Ciudad, string FotoVehiculo, int Propietario)
        {
            string ConsultaSQL = "INSERT INTO  " +
                                "alquiler_vehiculos.vehiculo ( " +
                                "Placa_Vehiculo, " +
                                "Modelo_Vehiculo, " +
                                "Cilindrada_Vehiculo, " +
                                "Color_Vehiculo, " +
                                "CantidadPasajeros_Vehiculo, " +
                                "NumeroSeguro_Vehiculo, " +
                                "NumeroCertificadoCDA_Vehiculo, " +
                                "PrecioAlquilerDia_Vehiculo, " +
                                "Foto_Vehiculo, " +
                                "Propietario_Vehiculo, " +
                                "Ciudad_Vehiculo, " +
                                "Clasificacion_Vehiculo, " +
                                "Linea_Vehiculo, " +
                                "TipoCombustible_Vehiculo) " +
                                "VALUES ('" +
                                Placa + "', " +
                                Modelo + ", " +
                                Cilindrada + ", " +
                                Color + ", " +
                                CantidadPasajeros + ", '" +
                                NumeroSeguro + "', '" +
                                NumeroCertificadoCDA + "', " +
                                PrecioAlquilerDia + ", '" +
                                FotoVehiculo + "', " +
                                Propietario + ", " +
                                Ciudad + ", " +
                                ClasificacionVehiculo + ", " +
                                Linea + ", " +
                                TipoCombustible + ") ";
            return ModeloConexion.ExecuteNonQuerySentence(ConsultaSQL);
        }
    }
}
