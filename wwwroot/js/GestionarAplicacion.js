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

const InputFotoEmpresa = document.querySelector('#FotoEmpresa');
if (InputFotoEmpresa) {
    document.getElementById('FotoEmpresa').addEventListener('change', CargarImagen);
}

function EnviarRolPermisoEliminar(IdRol, IdPermiso) {
    fetch('/Administrador/EliminarPermiso?IdRol=' + IdRol + "&IdPermiso=" + IdPermiso)
        .catch(error => console.error('Error:', error));

}

function EnviarRolPermisoAgregar(IdRol, IdPermiso) {
    fetch('/Administrador/AgregarPermiso?IdRol=' + IdRol + "&IdPermiso=" + IdPermiso)
        .catch(error => console.error('Error:', error));
}

function TraerPermisos() {
    const IdRol = document.getElementById("IdRol").value;
    const TotalPermisos = document.querySelectorAll('.InputTextoFormularioPermisos');
    const ArregloPermisos = [];
    for (var i = 0; i < TotalPermisos.length; i++) {
        ArregloPermisos.push(TotalPermisos[i].value);
    }
    const BotonesFormulario = document.querySelectorAll(".BotonFormularioEliminarPermiso, .BotonFormularioAgregarPermiso");
    let Indices = [];
    if (IdRol != 0) {
        fetch('/Administrador/TraerPermisosRol?IdRol=' + IdRol)
            .then(response => response.json())
            .then(Data => {
                Data.Permisos.forEach(function (PermisoRol) {
                    if (ArregloPermisos.includes(PermisoRol.Nombre)) {
                        const Indice = ArregloPermisos.indexOf(PermisoRol.Nombre);
                        Indices.push((Indice - 1));
                    }
                });
                for (var k = 0; k < BotonesFormulario.length; k++) {
                    BotonesFormulario[k].classList.add("BotonFormularioEliminarPermiso");
                    BotonesFormulario[k].classList.add("BotonFormularioAgregarPermiso");
                    BotonesFormulario[k].textContent = 'Agregar Permiso';
                }
                for (var j = 0; j < Indices.length; j++) {
                    BotonesFormulario[Indices[j]].classList.remove("BotonFormularioAgregarPermiso");
                    BotonesFormulario[Indices[j]].textContent = 'Eliminar Permiso';
                }
                AgregarAccionesBoton();
            })
            .catch(error => console.error('Error:', error));
    }
}

function AgregarAccionesBoton() {
    const IdRol = document.getElementById("IdRol").value;
    const BotonesFormulario = document.querySelectorAll(".BotonFormularioEliminarPermiso, .BotonFormularioAgregarPermiso");
    const IdPermisos = document.querySelectorAll('.IdPermiso');
    if (IdRol != 0) {
        for (var i = 0; i < BotonesFormulario.length; i++) {
            (function (IdPermiso) {
                if (BotonesFormulario[i].textContent == "Agregar Permiso") {
                    BotonesFormulario[i].setAttribute('href', 'AgregarPermiso?IdRol=' + IdRol + '&IdPermiso=' + IdPermiso);
                } else {
                    BotonesFormulario[i].setAttribute('href', 'EliminarPermiso?IdRol=' + IdRol + '&IdPermiso=' + IdPermiso);
                }
            })(IdPermisos[i].value);
        }
    }
}

const InputRol = document.querySelector('#IdRol');
if (InputRol) {
    document.getElementById('IdRol').addEventListener('change', TraerPermisos);
}
