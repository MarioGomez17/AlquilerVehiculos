function AparecerDivConfirmacionPropietario() {
    const ConfirmacionEntregaVehiculoPropietario = document.getElementById("ConfirmacionEntregaVehiculoPropietario");
    ConfirmacionEntregaVehiculoPropietario.classList.remove("ConfirmacionEntregaVehiculoOcultoPropietario");
    ConfirmacionEntregaVehiculoPropietario.classList.add("ConfirmacionEntregaVehiculoPropietario");
    const ContenidoBodyInformacionAlquilerPropietario = document.getElementById("ContenidoBodyInformacionAlquilerPropietario");
    ContenidoBodyInformacionAlquilerPropietario.style.opacity = "0";
}

function FuncionCerrarConfirmacion() {
    const ConfirmacionEntregaVehiculoPropietario = document.getElementById("ConfirmacionEntregaVehiculoPropietario");
    ConfirmacionEntregaVehiculoPropietario.classList.add("ConfirmacionEntregaVehiculoOcultoPropietario");
    ConfirmacionEntregaVehiculoPropietario.classList.remove("ConfirmacionEntregaVehiculoPropietario");
    const ContenidoBodyInformacionAlquilerPropietario = document.getElementById("ContenidoBodyInformacionAlquilerPropietario");
    ContenidoBodyInformacionAlquilerPropietario.style.opacity = "1";
}

function CancelarDiasRetraso() {
    const DiasRetraso = document.getElementById("DiasRetraso");
    if (DiasRetraso.disabled) {
        DiasRetraso.disabled = false;
    } else {
        DiasRetraso.disabled = true;
        DiasRetraso.value = 0;
    }
}

function VehiculoOptimo(CheckBox) {
    const BotonFinalizarAlquiler = document.getElementById("BotonFinalizarAlquiler");
    if (CheckBox.checked) {
        BotonFinalizarAlquiler.disabled = false;
    } else {
        BotonFinalizarAlquiler.disabled = true;
    }
}

function VehiculoOptimoAlquilador(CheckBox, event) {
    const LinkIniciarAlquiler = document.getElementById("LinkIniciarAlquiler");
    if (CheckBox.checked) {
        LinkIniciarAlquiler.event.preventDefault();
    }
}

const BotonConfirmacionPropietario = document.querySelector('#BotonConfirmacionPropietario');
if (BotonConfirmacionPropietario) {
    const BotonConfirmacionPropietario = document.getElementById("BotonConfirmacionPropietario");
    BotonConfirmacionPropietario.addEventListener("click", AparecerDivConfirmacionPropietario);
}

const CerrarConfirmacion = document.querySelector('#CerrarConfirmacion');
if (CerrarConfirmacion) {
    const CerrarConfirmacion = document.getElementById("CerrarConfirmacion");
    CerrarConfirmacion.addEventListener("click", FuncionCerrarConfirmacion);
}

const BotonTiempoVehiculo = document.querySelector('#BotonTiempoVehiculo');
if (BotonTiempoVehiculo) {
    const CerrarConfirmacion = document.getElementById("BotonTiempoVehiculo");
    CerrarConfirmacion.addEventListener("change", CancelarDiasRetraso);
}

const BotonOptimoVehiculo = document.querySelector('#BotonOptimoVehiculo');
if (BotonOptimoVehiculo) {
    const BotonOptimoVehiculo = document.getElementById("BotonOptimoVehiculo");
    BotonOptimoVehiculo.addEventListener('change', function () {
        VehiculoOptimo(BotonOptimoVehiculo);
    });
}

const BotonOptimoVehiculoAlquilador = document.querySelector('#BotonOptimoVehiculoAlquilador');
if (BotonOptimoVehiculoAlquilador) {
    const BotonOptimoVehiculoAlquilador = document.getElementById("BotonOptimoVehiculoAlquilador");
    BotonOptimoVehiculoAlquilador.addEventListener('change', function () {
        VehiculoOptimo(BotonOptimoVehiculoAlquilador, this);
    });
}


