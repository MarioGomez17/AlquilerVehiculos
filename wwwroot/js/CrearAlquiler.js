document.addEventListener("DOMContentLoaded", function () {
    const BotonCalcularPrecioAlquiler = document.getElementById("BotonCalcularPrecioAlquiler");
    BotonCalcularPrecioAlquiler.addEventListener("click", function (event) {
        event.preventDefault();
        CalcularPrecioAlquiler();
    });
});

async function CalcularPrecioAlquiler() {
    var PrecioSeguro = 0;
    var PrecioAlquilerDiaVehiculo = 0;
    var IdVehiculo = document.getElementById("Vehiculo").value;
    var IdSeguro = document.getElementById("Seguro").value;
    var Lavada = document.getElementById("PrecioLavadaVehiculo").value;
    var FechaInicio = new Date(document.getElementById("FechaInicio").value);
    var FechaFin = new Date(document.getElementById("FechaFin").value);
    if (IdVehiculo != 0 && IdSeguro != 0 && FechaInicio != "" && FechaFin != "") {
        try {
            let Respuesta = await fetch('/Alquiler/ObtenerPrecioAlquiler?IdVehiculo=' + IdVehiculo + '&IdSeguro=' + IdSeguro);
            let Datos = await Respuesta.json();
            PrecioSeguro = Datos.PrecioSeguro;
            PrecioAlquilerDiaVehiculo = Datos.PrecioAlquilerDiaVehiculo;
        }
        catch (error) {
            console.log("Error" + error);
        }
        var DiferenciaFechas = Math.abs(FechaFin - FechaInicio);
        var DiasAlquiler = Math.ceil(DiferenciaFechas / (1000 * 60 * 60 * 24));
        var ValorPrecioAlquiler = ((Number(PrecioAlquilerDiaVehiculo)*Number(DiasAlquiler)) + Number(PrecioSeguro) + Number(Lavada));
        const PrecioAlquiler = document.getElementById("PrecioAlquiler");
        PrecioAlquiler.value = ValorPrecioAlquiler;
    } else {
        alert("COMPLETE TODOS LOS CAMPOS DEL FORMULARIO PARA CONSULTAR EL PRECIO DEL ALQUILER");
    }
}

document.addEventListener("DOMContentLoaded", function () {
    var BotonLavadaVehiculo = document.getElementById("BotonLavadaVehiculo");
    BotonLavadaVehiculo.addEventListener('change', function () {
        const PrecioLavadaVehiculo = document.getElementById("PrecioLavadaVehiculo");
        if (this.checked) {
            PrecioLavadaVehiculo.value = "15000";
        } else {
            PrecioLavadaVehiculo.value = "";
        }
    });
});