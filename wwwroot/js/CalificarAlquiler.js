import Swal from 'https://cdn.skypack.dev/sweetalert2';

function ValidarCalificacion(event) {
    const InputCalificacion = document.getElementById("InputCalificacion");
    if (InputCalificacion.value == "") {
        event.preventDefault();
        Swal.fire({
            title: 'ERROR',
            text: 'DEBE DILIGENCIAR AL MENOS UNA CALIFICACIÓN',
            icon: 'error',
            confirmButtonText: 'OK'
        });
    }
}

const BotonEnviarCalificacion = document.getElementById("BotonEnviarCalificacion");
BotonEnviarCalificacion.addEventListener("click", ValidarCalificacion);