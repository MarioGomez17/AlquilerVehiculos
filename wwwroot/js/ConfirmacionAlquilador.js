function AparecerDivConfirmacionAlquilador() {
    const ConfirmacionEntregaVehiculoAlquilador = document.getElementById("ConfirmacionEntregaVehiculoAlquilador");
    ConfirmacionEntregaVehiculoAlquilador.classList.remove("ConfirmacionEntregaVehiculoOcultoAlquilador");
    ConfirmacionEntregaVehiculoAlquilador.classList.add("ConfirmacionEntregaVehiculoAlquilador");
    const ContenidoBodyInformacionAlquilerAlquilador = document.getElementById("ContenidoBodyInformacionAlquilerAlquilador");
    ContenidoBodyInformacionAlquilerAlquilador.style.opacity = "0";
}

function FuncionCerrarConfirmacion() {
    const ConfirmacionEntregaVehiculoAlquilador = document.getElementById("ConfirmacionEntregaVehiculoAlquilador");
    ConfirmacionEntregaVehiculoAlquilador.classList.add("ConfirmacionEntregaVehiculoOcultoAlquilador");
    ConfirmacionEntregaVehiculoAlquilador.classList.remove("ConfirmacionEntregaVehiculoAlquilador");
    const ContenidoBodyInformacionAlquilerAlquilador = document.getElementById("ContenidoBodyInformacionAlquilerAlquilador");
    ContenidoBodyInformacionAlquilerAlquilador.style.opacity = "1";
}

function VehiculoOptimo(CheckBox) {
    if (!CheckBox.checked) {
        document.getElementById("LinkIniciarAlquiler").addEventListener("click", function (event) {
            event.preventDefault();
        });
    }
}

const BotonConfirmacionAlquilador = document.querySelector('#BotonConfirmacionAlquilador');
if (BotonConfirmacionAlquilador) {
    const BotonConfirmacionAlquilador = document.getElementById("BotonConfirmacionAlquilador");
    BotonConfirmacionAlquilador.addEventListener("click", AparecerDivConfirmacionAlquilador);
    document.addEventListener('DOMContentLoaded', function () {
        const BotonOptimoVehiculoAlquilador = document.getElementById('BotonOptimoVehiculoAlquilador');
        const LinkIniciarAlquiler = document.getElementById("LinkIniciarAlquiler");
        LinkIniciarAlquiler.addEventListener('click', function (event) {
            if (!BotonOptimoVehiculoAlquilador.checked) {
                event.preventDefault();
            }
        });
    });
}

const CerrarConfirmacion = document.querySelector('#CerrarConfirmacion');
if (CerrarConfirmacion) {
    const CerrarConfirmacion = document.getElementById("CerrarConfirmacion");
    CerrarConfirmacion.addEventListener("click", FuncionCerrarConfirmacion);
}