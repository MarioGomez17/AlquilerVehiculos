namespace ALQUILER_VEHICULOS.Models
{
    public class ModeloGestionarEmpresa
    { 
        public ModeloEmpresa Empresa {get; set;}
        public List<ModeloCiudad> Ciudades {get; set;}
        public ModeloGestionarEmpresa(){
            ModeloCiudad Ciudad = new();
            this.Empresa = new();
            this.Ciudades = Ciudad.TraerTodasCiudades();
        }
    }
}