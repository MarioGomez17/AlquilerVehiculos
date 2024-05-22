import Swal from 'https://cdn.skypack.dev/sweetalert2';

document.addEventListener("DOMContentLoaded", function () {
    const BotonCalcularPrecioAlquiler = document.getElementById("BotonCalcularPrecioAlquiler");
    BotonCalcularPrecioAlquiler.addEventListener("click", function (event) {
        event.preventDefault();
        CalcularPrecioAlquiler();
    });
    var BotonLavadaVehiculo = document.getElementById("BotonLavadaVehiculo");
    BotonLavadaVehiculo.addEventListener('change', function () {
        const PrecioLavadaVehiculo = document.getElementById("PrecioLavadaVehiculo");
        const PrecioAlquiler = document.getElementById("PrecioAlquiler");
        if (this.checked) {
            if (PrecioAlquiler.value != "") {
                var ValorAlquiler = (Number(PrecioAlquiler.value) + 20000);
                PrecioAlquiler.value = ValorAlquiler;
            }
            PrecioLavadaVehiculo.value = "20000";
        } else {
            if (PrecioAlquiler.value != "") {
                var ValorAlquiler = (Number(PrecioAlquiler.value) - 20000);
                PrecioAlquiler.value = ValorAlquiler;
            }
            PrecioLavadaVehiculo.value = "0";
        }
    });
    const BotonEnviarFormularioCrearAlquiler = document.getElementById("BotonEnviarFormularioCrearAlquiler");
    BotonEnviarFormularioCrearAlquiler.addEventListener('click', function (event) {
        const FechaInicio = document.getElementById("FechaInicio").value;
        const FechaFin = document.getElementById("FechaFin").value;
        const Seguro = document.getElementById("Seguro").value;
        const PrecioAlquiler = document.getElementById("PrecioAlquiler").value;
        const NombreVehiculo = document.getElementById("NombreVehiculo").value;
        const Ciudad = document.getElementById("Ciudad").value;
        const Lugar = document.getElementById("Lugar").value;
        const MetodoPago = document.getElementById("MetodoPago").value;
        if (FechaInicio == "" || FechaFin == "" || Seguro == 0 || PrecioAlquiler == "" || NombreVehiculo == "" || Ciudad == 0 || Lugar == 0 || MetodoPago == 0) {
            event.preventDefault();
            Swal.fire({
                title: 'ERROR',
                text: 'COMPLETE TODO EL FORMULARIO PARA PODER REGISTRAR EL ALQUILER',
                icon: 'error',
                confirmButtonText: 'OK'
            });
        }
    });
});

function ConvertirFecha(StringFecha) {
    let [Dia, Mes, Anio] = StringFecha.split('-').map(Number);
    Mes -= 1;
    return new Date(Anio, Mes, Dia);
}

async function CalcularPrecioAlquiler() {
    var PrecioSeguro = 0;
    var PrecioAlquilerDiaVehiculo = 0;
    var IdVehiculo = document.getElementById("Vehiculo").value;
    var IdSeguro = document.getElementById("Seguro").value;
    var Lavada = document.getElementById("PrecioLavadaVehiculo").value;
    var FechaInicio = ConvertirFecha(document.getElementById("FechaInicio").value);
    var FechaFin = ConvertirFecha(document.getElementById("FechaFin").value);
    if (IdVehiculo != 0 && IdSeguro != 0 && FechaInicio != "" && FechaFin != "") {
        try {
            let Respuesta = await fetch('/Alquiler/ObtenerPrecioAlquiler?IdVehiculo=' + IdVehiculo + '&IdSeguro=' + IdSeguro);
            let Datos = await Respuesta.json();
            PrecioSeguro = Datos.PrecioSeguro;
            PrecioAlquilerDiaVehiculo = Datos.PrecioAlquilerDiaVehiculo;
        }
        catch (error) {
        }
        var DiferenciaFechas = Math.abs(FechaFin - FechaInicio);
        var DiasAlquiler = Math.ceil(DiferenciaFechas / (1000 * 60 * 60 * 24));
        var ValorPrecioAlquiler = ((Number(PrecioAlquilerDiaVehiculo) * Number(DiasAlquiler)) + Number(PrecioSeguro) + Number(Lavada));
        const PrecioAlquiler = document.getElementById("PrecioAlquiler");
        PrecioAlquiler.value = ValorPrecioAlquiler;
    } else {
        Swal.fire({
            title: 'ERROR',
            text: 'DEBE SELECCIONAR AL MENOS UN SEGURO PARA PODER CALCULAR EL PRECIO DEL ALQUILER',
            icon: 'error',
            confirmButtonText: 'OK'
        });
    }
}