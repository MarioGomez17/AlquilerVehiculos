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

function TraerTodasClasificacionesPorTipoInicial(IdTipoVehiculo) {
    if (IdTipoVehiculo) {
        fetch('/Vehiculo/TraerTodasClasificacionesPorTipo?IdTipoVehiculo=' + IdTipoVehiculo)
            .then(response => response.json())
            .then(Data => {
                var ClasificacionVehiculo = document.getElementById('ClasificacionVehiculo');
                ClasificacionVehiculo.innerHTML = '';
                Data.ClasificacionesVehiculo.forEach(function (Clasificacion) {
                    if (document.getElementById('TextoClasificacion').value == Clasificacion.Nombre) {
                        var OptionSelect = new Option(Clasificacion.Nombre, Clasificacion.Id);
                        OptionSelect.classList.add("OptionSelect");
                        OptionSelect.selected = true;
                        ClasificacionVehiculo.add(OptionSelect);
                    } else {
                        var OptionSelect = new Option(Clasificacion.Nombre, Clasificacion.Id);
                        OptionSelect.classList.add("OptionSelect");
                        ClasificacionVehiculo.add(OptionSelect);
                    }
                });
            })
            .catch(error => console.error('Error:', error));
    } else {
        document.getElementById('ClasificacionVehiculo').innerHTML = '';
    }
}

function TraerTodasMarcasPorTIpoInicial(IdTipoVehiculo) {
    if (IdTipoVehiculo) {
        fetch('/Vehiculo/TraerTodasMarcasPorTIpo?IdTipoVehiculo=' + IdTipoVehiculo)
            .then(response => response.json())
            .then(Data => {
                var MarcaVehiculo = document.getElementById('Marca');
                MarcaVehiculo.innerHTML = '';
                Data.Marcas.forEach(function (Marca) {
                    if (document.getElementById('TextoMarca').value == Marca.Nombre) {
                        var OptionSelect = new Option(Marca.Nombre, Marca.Id);
                        OptionSelect.classList.add("OptionSelect");
                        OptionSelect.selected = true;
                        MarcaVehiculo.add(OptionSelect);
                    } else {
                        var OptionSelect = new Option(Marca.Nombre, Marca.Id);
                        OptionSelect.classList.add("OptionSelect");
                        MarcaVehiculo.add(OptionSelect);
                    }
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

document.getElementById('TipoVehiculo').addEventListener('change', TraerTodasClasificacionesPorTipo);
document.getElementById('TipoVehiculo').addEventListener('change', TraerTodasMarcasPorTIpo);
document.getElementById('Marca').addEventListener('change', TraerTodasLineasPorMarca);
document.getElementById('FotoVehiculo').addEventListener('change', CargarImagen);