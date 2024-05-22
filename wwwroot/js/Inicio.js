import Swal from 'https://cdn.skypack.dev/sweetalert2';

function TraerTodasMarcasPorTIpo() {
    var IdTipoVehiculo = document.getElementById('FiltroTipoVehiculo').value;
    if (IdTipoVehiculo) {
        fetch('/Vehiculo/TraerTodasMarcasPorTIpo?IdTipoVehiculo=' + IdTipoVehiculo)
            .then(response => response.json())
            .then(Data => {
                var MarcaVehiculo = document.getElementById('FiltroMarca');
                MarcaVehiculo.innerHTML = '';
                var OptionSelectNull = new Option("Seleccione una Marca", 0);
                OptionSelectNull.classList.add("OptionSelect");
                OptionSelectNull.selected = true;
                OptionSelectNull.disabled = true;
                MarcaVehiculo.add(OptionSelectNull);
                Data.Marcas.forEach(function (Marca) {
                    var OptionSelect = new Option(Marca.Nombre, Marca.Id);
                    OptionSelect.classList.add("OptionSelectFormulario");
                    MarcaVehiculo.add(OptionSelect);
                });
            })
            .catch(error => console.error('Error:', error));
    } else {
        document.getElementById('Marca').innerHTML = '';
    }
}

document.getElementById('FiltroTipoVehiculo').addEventListener('change', TraerTodasMarcasPorTIpo);

document.addEventListener("DOMContentLoaded", function () {
    const FiltroFechaInicio = document.getElementById('FiltroFechaInicio');
    const FiltroFechaFin = document.getElementById('FiltroFechaFin');
    const BotonFiltros = document.getElementById('BotonFiltros');
    BotonFiltros.addEventListener('click', function (event) {
        if (FiltroFechaInicio.value == '' || FiltroFechaFin.value == '') {
            event.preventDefault();
            Swal.fire({
                title: 'ERROR',
                text: 'COMPLETE AL MENOS LOS FILTROS DE FECHA DE INICIO Y FIN DEL ALQUILER',
                icon: 'error',
                confirmButtonText: 'OK'
            });
        }
    });
    function FormatearFecha(date) {
        return date.toISOString().split('T')[0];
    }
    let FechaMinimoInicio = new Date();
    FechaMinimoInicio.setDate(FechaMinimoInicio.getDate() + 2);
    FiltroFechaInicio.min = FormatearFecha(FechaMinimoInicio);
    FiltroFechaInicio.addEventListener('change', function () {
        let FechaSeleccionadaInicio = new Date(this.value);
        let MinimoFechaFin = new Date(FechaSeleccionadaInicio);
        MinimoFechaFin.setDate(MinimoFechaFin.getDate() + 1);
        FiltroFechaFin.min = FormatearFecha(MinimoFechaFin);
        FiltroFechaFin.value = FiltroFechaFin.min;
        FiltroFechaFin.disabled = false;
    });
});