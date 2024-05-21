function TraerTodosTIposInicial() {
    fetch('/Vehiculo/TraerTodosTIpo')
        .then(response => response.json())
        .then(Data => {
            var Tipo = document.getElementById('Tipo');
            Tipo.innerHTML = '';
            var OptionSelect = new Option("Seleccione un tipo de Vehículo", 0);
            OptionSelect.classList.add("OptionSelectFormularioAgregar");
            OptionSelect.selected = true;
            OptionSelect.disabled = true;
            Tipo.add(OptionSelect);
            Data.TiposVehiculo.forEach(function (TipoVehiuclo) {
                OptionSelect = new Option(TipoVehiuclo.Nombre, TipoVehiuclo.Id);
                OptionSelect.classList.add("OptionSelectFormularioAgregar");
                Tipo.add(OptionSelect);
            });
        })
        .catch(error => console.error('Error:', error));
}

function TraerTodasMarcasPorTIpo() {
    var IdTipoVehiculo = document.getElementById('Tipo').value;
    if (IdTipoVehiculo) {
        fetch('/Vehiculo/TraerTodasMarcasPorTIpo?IdTipoVehiculo=' + IdTipoVehiculo)
            .then(response => response.json())
            .then(Data => {
                var MarcaVehiculo = document.getElementById('Marca');
                MarcaVehiculo.innerHTML = '';
                var OptionSelectNull = new Option("Seleccione una Marca", 0);
                OptionSelectNull.classList.add("OptionSelectFormularioAgregar");
                OptionSelectNull.selected = true;
                OptionSelectNull.disabled = true;
                MarcaVehiculo.add(OptionSelectNull);
                Data.Marcas.forEach(function (Marca) {
                    var OptionSelect = new Option(Marca.Nombre, Marca.Id);
                    OptionSelect.classList.add("OptionSelectFormularioAgregar");
                    MarcaVehiculo.add(OptionSelect);
                });
            })
            .catch(error => console.error('Error:', error));
    } else {
        document.getElementById('Marca').innerHTML = '';
    }
    TraerTodasLineasPorMarca();
}

const InputTipos = document.querySelector('#Tipo');
if (InputTipos) {
    document.getElementById('Tipo').addEventListener('change', TraerTodasMarcasPorTIpo);
    document.addEventListener('DOMContentLoaded', function () {
        TraerTodosTIposInicial();
    });
}

function CargarImagen() {
    const EspacioFotoEmpresa = document.getElementById('EspacioFotoEmpresa');
    const FotoEmpresa = document.getElementById('FotoEmpresa').files[0];
    if (FotoEmpresa) {
        const Lector = new FileReader();
        Lector.onload = function (event) {
            const URL = event.target.result;
            EspacioFotoEmpresa.src = URL;
        };
        Lector.readAsDataURL(FotoEmpresa);
    }
}
document.getElementById('FotoEmpresa').addEventListener('change', CargarImagen);
