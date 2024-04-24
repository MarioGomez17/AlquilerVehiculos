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
        public bool CrearAquiler(DateTime FechaInicio, DateTime FechaFin, float Precio, int Lavada, int Alquilador, int Vehiculo, int Lugar, int MetodoPago, int Seguro){
            string ConsultaSQL = "INSERT INTO " + 
                "alquiler_vehiculos.alquiler (" + 
                "FechaIncio_Alquiler, " + 
                "FechaFin_Alquiler, " + 
                "Precio_Alquiler, " + 
                "LavadaVehiculo_Alquiler, " + 
                "Alquilador_Alquiler, " + 
                "Vehiculo_Alquiler, " + 
                "Lugar_Alquiler, " + 
                "MeotodoPago_Alquiler, " + 
                "Seguro_Alquiler) " + 
                "VALUES (" + 
                FechaInicio + ", " + 
                FechaFin + ", " + 
                Precio + ", " + 
                Lavada + ", " + 
                Alquilador + ", " + 
                Vehiculo + ", " + 
                Lugar + ", " + 
                MetodoPago + ", " + 
                Seguro + ")" ;
            return ModeloConexion.ExecuteNonQuerySentence(ConsultaSQL);
        }
    }
}
