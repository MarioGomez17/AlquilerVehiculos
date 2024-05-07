import Swal from 'https://cdn.skypack.dev/sweetalert2';

function TraerTodasClasificacionesPorTipo() {
    var IdTipoVehiculo = document.getElementById('TipoVehiculo').value;
    if (IdTipoVehiculo) {
        fetch('/Vehiculo/TraerTodasClasificacionesPorTipo?IdTipoVehiculo=' + IdTipoVehiculo)
            .then(response => response.json())
            .then(Data => {
                var ClasificacionVehiculo = document.getElementById('ClasificacionVehiculo');
                ClasificacionVehiculo.innerHTML = '';
                var OptionSelectNull = new Option("Seleccione Una Clasificación", 0);
                OptionSelectNull.selected = true;
                OptionSelectNull.disabled = true;
                OptionSelectNull.classList.add("OptionSelect");
                ClasificacionVehiculo.add(OptionSelectNull);
                Data.ClasificacionesVehiculo.forEach(function (Clasificacion) {
                    var OptionSelect = new Option(Clasificacion.Nombre, Clasificacion.Id);
                    OptionSelect.classList.add("OptionSelect");
                    ClasificacionVehiculo.add(OptionSelect);
                });
            })
            .catch(error => console.error('Error:', error));
    } else {
        document.getElementById('ClasificacionVehiculo').innerHTML = '';
    }
}

function TraerTodasMarcasPorTIpo() {
    var IdTipoVehiculo = document.getElementById('TipoVehiculo').value;
    if (IdTipoVehiculo) {
        fetch('/Vehiculo/TraerTodasMarcasPorTIpo?IdTipoVehiculo=' + IdTipoVehiculo)
            .then(response => response.json())
            .then(Data => {
                var MarcaVehiculo = document.getElementById('Marca');
                MarcaVehiculo.innerHTML = '';
                var OptionSelectNull = new Option("Seleccione una Marca", 0);
                OptionSelectNull.classList.add("OptionSelect");
                OptionSelectNull.selected = true;
                OptionSelectNull.disabled = true;
                MarcaVehiculo.add(OptionSelectNull);
                Data.Marcas.forEach(function (Marca) {
                    var OptionSelect = new Option(Marca.Nombre, Marca.Id);
                    OptionSelect.classList.add("OptionSelect");
                    MarcaVehiculo.add(OptionSelect);
                });
            })
            .catch(error => console.error('Error:', error));
    } else {
        document.getElementById('Marca').innerHTML = '';
    }
    TraerTodasLineasPorMarca();
}

function TraerTodasLineasPorMarca() {
    var IdMarca = document.getElementById('Marca').value;
    if (IdMarca) {
        fetch('/Vehiculo/TraerTodasLineasPorMarca?IdMarca=' + IdMarca)
            .then(response => response.json())
            .then(Data => {
                var LineaVehiculo = document.getElementById('Linea');
                LineaVehiculo.innerHTML = '';
                var OptionSelectNull = new Option("Seleccione una Línea", 0);
                OptionSelectNull.classList.add("OptionSelect");
                OptionSelectNull.selected = true;
                OptionSelectNull.disabled = true;
                LineaVehiculo.add(OptionSelectNull);
                Data.Lineas.forEach(function (Linea) {
                    var OptionSelect = new Option(Linea.Nombre, Linea.Id);
                    OptionSelect.classList.add("OptionSelect");
                    LineaVehiculo.add(OptionSelect);
                });
            })
            .catch(error => console.error('Error:', error));
    } else {
        document.getElementById('Marca').innerHTML = '';
    }
}

function CargarImagen() {
    const EspacioFotoVehiculo = document.getElementById('EspacioFotoVehiculo');
    const FotoVehiculo = document.getElementById('FotoVehiculo').files[0];
    if (FotoVehiculo) {
        const Lector = new FileReader();
        Lector.onload = function (event) {
            const URL = event.target.result;
            EspacioFotoVehiculo.src = URL;
        };
        Lector.readAsDataURL(FotoVehiculo);
    }
}

function ValidarRegistrarVehiculo(event) {
    const Placa = document.getElementById('Placa');
    const Cilindrada = document.getElementById('Cilindrada');
    const Modelo = document.getElementById('Modelo');
    const PrecioAlquilerDia = document.getElementById('PrecioAlquilerDia');
    const Color = document.getElementById('Color');
    const TipoVehiculo = document.getElementById('TipoVehiculo');
    const ClasificacionVehiculo = document.getElementById('ClasificacionVehiculo');
    const Marca = document.getElementById('Marca');
    const Linea = document.getElementById('Linea');
    const NumeroCertificadoCDA = document.getElementById('NumeroCertificadoCDA');
    const NumeroPolizaSeguro = document.getElementById('NumeroPolizaSeguro');
    const TipoCombustible = document.getElementById('TipoCombustible');
    const Ciudad = document.getElementById('Ciudad');
    const FotoVehiculo = document.getElementById('FotoVehiculo');
    if (Placa.value == "" || Cilindrada.value == "" || Modelo.value == "" || PrecioAlquilerDia.value == "" || Color.value == "" || TipoVehiculo.value == 0 || ClasificacionVehiculo.value == 0 || Marca.value == 0 || Linea.value == 0 || NumeroCertificadoCDA.value == "" || NumeroPolizaSeguro.value == "" || TipoCombustible.value == 0 || Ciudad.value == 0 || FotoVehiculo.value == "") {
        event.preventDefault();
        Swal.fire({
            title: 'ERROR',
            text: 'COMPLETE TODO EL FORMULARIO PARA PODER REGISTRAR UN VEHÍCULO',
            icon: 'error',
            confirmButtonText: 'OK'
        });
    }
}

document.getElementById('TipoVehiculo').addEventListener('change', TraerTodasClasificacionesPorTipo);
document.getElementById('TipoVehiculo').addEventListener('change', TraerTodasMarcasPorTIpo);
document.getElementById('Marca').addEventListener('change', TraerTodasLineasPorMarca);
document.getElementById('FotoVehiculo').addEventListener('change', CargarImagen);
document.getElementById('BotonRegistrarVehiculo').addEventListener('click', ValidarRegistrarVehiculo);
