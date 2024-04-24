namespace ALQUILER_VEHICULOS.Models
{
    public class ModeloPropietario
    {
        public int Id { get; set; }

        public string Codigo { get; set; }

        public ModeloUsuario Usuario { get; set; }

        public List<ModeloAlquiler> HistorialAlquileres { get; set; }

    }
}
