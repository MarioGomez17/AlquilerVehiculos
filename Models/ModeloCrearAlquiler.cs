﻿namespace ALQUILER_VEHICULOS.Models
{
    public class ModeloCrearAlquiler
    {
        public ModeloVehiculo Vehiculo { get; set; }
        public List<ModeloLugarAlquiler> Lugares { get; set; }
        public List<ModeloSeguroAlquiler> Seguros { get; set; }
        public List<ModeloMetodoPagoAlquiler> MetodosPago { get; set; }
        public ModeloCrearAlquiler(int IdVehiculo)
        {
            ModeloVehiculo ModeloVehiculo = new();
            ModeloLugarAlquiler ModeloLugarAlquiler = new();
            ModeloSeguroAlquiler ModeloSeguroAlquiler = new();
            ModeloMetodoPagoAlquiler ModeloMetodoPagoAlquiler = new();
            this.Vehiculo = ModeloVehiculo.TraerVehiculo(IdVehiculo);
            this.Lugares = ModeloLugarAlquiler.TraerTodosLugaresAlquiler();
            this.Seguros = ModeloSeguroAlquiler.TraerTodosSegurosAlquiler();
            this.MetodosPago = ModeloMetodoPagoAlquiler.TraerTodosMetodosPagoAlquiler();
        }
    }
}
