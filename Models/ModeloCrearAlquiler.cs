namespace ALQUILER_VEHICULOS.Models
{
    public class ModeloCrearAlquiler
    {
        public ModeloVehiculo Vehiculo {  get; set; }

        public List<ModeloLugarAlquiler> Lugares { get; set; }

        public List<ModeloSeguroAlquiler> Seguros { get; set;}

        public List<ModeloMetodoPagoAlquiler> MetodosPago { get; set; }

        public ModeloCrearAlquiler(int Id_Vehicle) 
        { 
            ModeloVehiculo VehicleModel = new ();
            ModeloLugarAlquiler RentalPlaceModel = new();
            ModeloSeguroAlquiler RentalInsuranceModel = new();
            ModeloMetodoPagoAlquiler RentalPaymentMethodModel = new();
            this.Vehiculo = VehicleModel.TraerVehiculo(Id_Vehicle);
            this.Lugares = RentalPlaceModel.TraerTodosLugaresAlquiler();
            this.Seguros = RentalInsuranceModel.TraerTodosSegurosAlquiler();
            this.MetodosPago = RentalPaymentMethodModel.TraerTodosMetodosPagoAlquiler();
        }
    }
}
