import Swal from 'https://cdn.skypack.dev/sweetalert2';

const Contrasena = document.getElementById('Contrasena');
const ConfirmarContrasena = document.getElementById('ConfirmarContrasena');
const BotonRegistrar = document.getElementById('BotonRegistrar');

Contrasena.addEventListener('input', ValidarContrasena);
ConfirmarContrasena.addEventListener('input', ValidarContrasena);

function ValidarContrasena() {
    const ContrasenaVariable = Contrasena.value;
    const ConfirmarContrasenaVariable = ConfirmarContrasena.value;

    if ((ContrasenaVariable.length) >= 8 && (ContrasenaVariable === ConfirmarContrasenaVariable)) {
        BotonRegistrar.disabled = false;
        BotonRegistrar.classList.add("BotonRegistrarHabilitado");

    } else {
        BotonRegistrar.disabled = true;
        BotonRegistrar.classList.remove("BotonRegistrarHabilitado");
    }
}

function ValidarRegistro(event){
    const Nombre = document.getElementById('Nombre');
    const Apellido = document.getElementById('Apellido');
    const TipoIdentificacion = document.getElementById('TipoIdentificacion');
    const NumeroIdentificacion = document.getElementById('NumeroIdentificacion');
    const Telefono = document.getElementById('Telefono');
    const Correo = document.getElementById('Correo');
    if(Nombre.value == "" || Apellido.value == "" || TipoIdentificacion.value == 0 || NumeroIdentificacion.value == "" || Telefono.value == "" || Correo.value == "" || Contrasena.value == ""){
        event.preventDefault();
        Swal.fire({
            title: 'ERROR',
            text: 'LLENE TODO EL FORMULARIO PARA PODER REGISTRARSE',
            icon: 'error',
            confirmButtonText: 'OK'
        });
    }
}

BotonRegistrar.addEventListener("click", ValidarRegistro)