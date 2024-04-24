document.addEventListener("DOMContentLoaded", function () {
    const BotonCalcularPrecioAlquiler = document.getElementById("BotonCalcularPrecioAlquiler");
    BotonCalcularPrecioAlquiler.addEventListener("click", function (event) {
        event.preventDefault();
        CalcularPrecioAlquiler();
    });
});

async function CalcularPrecioAlquiler() {
    try {
        let Respuesta = await fetch('/Alquiler/ObtenerPrecioAlquiler');
        let Datos = await Respuesta.json()
        const PrecioAlquiler = document.getElementById("PrecioAlquiler");
        PrecioAlquiler.value = Datos.PrecioAlquiler;
        console.log(Datos);
    }
    catch (error) {
        console.log("Error" + error);
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