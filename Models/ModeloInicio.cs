namespace ALQUILER_VEHICULOS.Models
{
    public class ModeloInicio
    {
        public List<ModeloVehiculo> Vehiculos {get; set;}
        public List<ModeloMarca> Marcas {get; set;}
        public List<ModeloTipoVehiculo> TiposVehiculos {get; set;}
        public List<ModeloCiudad> Ciudades {get; set;}
        public ModeloInicio()
        {
            ModeloMarca Marcas = new();
            ModeloTipoVehiculo TiposVehiculos = new();
            ModeloCiudad Ciudades = new();
            ModeloVehiculo Vehiculos = new();
            this.Marcas = Marcas.TraerTodosMetodasMarcas();
            this.TiposVehiculos = TiposVehiculos.TraerTodosTipoVehiculo();
            this.Ciudades = Ciudades.TraerTodasCiudades();
            this.Vehiculos = Vehiculos.TraerTodosVehiculos();
        }
        public ModeloInicio(List<ModeloVehiculo> Vehiculos)
        {
            ModeloMarca Marcas = new();
            ModeloTipoVehiculo TiposVehiculos = new();
            ModeloCiudad Ciudades = new();
            this.Marcas = Marcas.TraerTodosMetodasMarcas();
            this.TiposVehiculos = TiposVehiculos.TraerTodosTipoVehiculo();
            this.Ciudades = Ciudades.TraerTodasCiudades();
            this.Vehiculos = Vehiculos;
        }
    }
}