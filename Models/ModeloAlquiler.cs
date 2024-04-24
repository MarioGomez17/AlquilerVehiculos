namespace ALQUILER_VEHICULOS.Models
{
    public class ModeloAlquiler
    {
        public int Id { get; set; }

        public DateTime FechaIncio { get; set; }

        public DateTime FechaFin { get; set; }

        public float Precio { get; set; }

        public bool Lavada { get; set; }

        public ModeloAlquilador Alquilador { get; set; }

        public ModeloVehiculo Vehiculo { get; set; }

        public string Lugar { get; set; }

        public string MetodoPago { get; set; }

        public string EstadoPago { get; set; }

        public string Seguro { get; set; }

        public float PrecioSeguro { get; set; }

        public float Calificacion { get; set; }

        public string ComentarioCalificacion { get; set; }

        public string Estado { get; set; }
    }
}
