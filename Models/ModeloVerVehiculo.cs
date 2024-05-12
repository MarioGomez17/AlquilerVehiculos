namespace ALQUILER_VEHICULOS.Models
{
    public class ModeloVerVehiculo
    {
        public List<ModeloTipoVehiculo> TipoVehiculos { get; set; }
        public List<ModeloClasificacionVehículo> ClasificacionesVehículo { get; set; }
        public List<ModeloMarca> Marcas { get; set; }
        public List<ModeloLinea> Lineas { get; set; }
        public List<ModeloTipoCombustible> Combustibles { get; set; }
        public List<ModeloCiudad> Ciudades { get; set; }
        public ModeloVehiculo Vehiculo { get; set; }
        public List<ModeloCilindrada> Cilindradas { get; set; }
        public List<ModeloModelo> Modelos { get; set; }
        public List<ModeloColor> Colores { get; set; }
        public List<ModeloCantidadPasajeros> CantidadesPasajeros { get; set; }
        public ModeloVerVehiculo(int IdVehiculo)
        {
            ModeloTipoVehiculo ModeloTipoVehiculo = new();
            ModeloClasificacionVehículo ModeloClasificacionVehículo = new();
            ModeloMarca ModeloMarca = new();
            ModeloLinea ModeloLinea = new();
            ModeloTipoCombustible ModeloTipoCombustible = new();
            ModeloCiudad ModeloCiudad = new();
            ModeloVehiculo ModeloVehiculo = new();
            ModeloCilindrada ModeloCilindrada = new();
            ModeloModelo ModeloModelo = new();
            ModeloColor ModeloColor = new();
            ModeloCantidadPasajeros ModeloCantidadPasajeros = new();
            this.TipoVehiculos = ModeloTipoVehiculo.TraerTodosTipoVehiculo();
            this.ClasificacionesVehículo = ModeloClasificacionVehículo.TraerTodasClasificaciones();
            this.Marcas = ModeloMarca.TraerTodosMetodasMarcas();
            this.Lineas = ModeloLinea.TraerTodasLineas();
            this.Combustibles = ModeloTipoCombustible.TraerTodosTiposComustible();
            this.Ciudades = ModeloCiudad.TraerTodasCiudades();
            this.Vehiculo = ModeloVehiculo.TraerVehiculo(IdVehiculo);
            this.Cilindradas = ModeloCilindrada.TraerCilindradas();
            this.Modelos = ModeloModelo.TraerModelos();
            this.Colores = ModeloColor.TraerColores();
            this.CantidadesPasajeros = ModeloCantidadPasajeros.TraerCantidadesPasajeros();
        }
    }
}