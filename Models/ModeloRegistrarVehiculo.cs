namespace ALQUILER_VEHICULOS.Models
{
    public class ModeloRegistrarVehiculo
    { 
        public List<ModeloTipoVehiculo> TipoVehiculos {get; set;}
        public List<ModeloClasificacionVehículo> ClasificacionesVehículo {get; set;}
        public List<ModeloMarca> Marcas {get; set;}
        public List<ModeloLinea> Lineas {get; set;}
        public List<ModeloTipoCombustible> Combustibles {get; set;}
        public List<ModeloCiudad> Ciudades {get; set;}
        public ModeloRegistrarVehiculo(){
            ModeloTipoVehiculo ModeloTipoVehiculo = new();
            ModeloClasificacionVehículo ModeloClasificacionVehículo = new();
            ModeloMarca ModeloMarca = new();
            ModeloLinea ModeloLinea = new();
            ModeloTipoCombustible ModeloTipoCombustible = new();
            ModeloCiudad ModeloCiudad = new();
            this.TipoVehiculos = ModeloTipoVehiculo.TraerTodosTipoVehiculo();
            this.ClasificacionesVehículo = ModeloClasificacionVehículo.TraerTodasClasificaciones();
            this.Marcas = ModeloMarca.TraerTodosMetodasMarcas();
            this.Lineas = ModeloLinea.TraerTodasLineas();
            this.Combustibles = ModeloTipoCombustible.TraerTodosTiposComustible();
            this.Ciudades = ModeloCiudad.TraerTodasCiudades();
        }
    }
}