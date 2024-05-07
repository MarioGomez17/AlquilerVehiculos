import Swal from 'https://cdn.skypack.dev/sweetalert2';

function ValidarIniciarSesion(event) {
    const Correo = document.getElementById("Correo");
    const Contrasena = document.getElementById("Contrasena");
    if (Correo.value == "" || Contrasena.value == "") {
        event.preventDefault();
        Swal.fire({
            title: 'ERROR',
            text: 'LLENE TODO EL FORMULARIO PARA PODER INICIAR SESIÓN',
            icon: 'error',
            confirmButtonText: 'OK'
        });
    }
}

const BotonIniciarSesion = document.getElementById("BotonIniciarSesion");
BotonIniciarSesion.addEventListener("click", ValidarIniciarSesion);