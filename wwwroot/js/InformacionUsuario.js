const Contrasena = document.getElementById('Contraseña');
const ConfirmarContrasena = document.getElementById('ConfirmarContraseña');
const BotonActualizarDatos = document.getElementById('BotonActualizarDatos');

Contrasena.addEventListener('input', ValidarContrasena);
ConfirmarContrasena.addEventListener('input', ValidarContrasena);

function ValidarContrasena() {
    const ContrasenaVariable = Contrasena.value;
    const ConfirmarContrasenaVariable = ConfirmarContrasena.value;
    if ((ContrasenaVariable.length) >= 8 && (ContrasenaVariable === ConfirmarContrasenaVariable)) {
        BotonActualizarDatos.disabled = false;
        BotonActualizarDatos.classList.add("BotonHabilitado");
    } else if ((ContrasenaVariable.length) >= 1 && (ContrasenaVariable.length) < 8) {
        BotonActualizarDatos.disabled = true;
        BotonActualizarDatos.classList.remove("BotonHabilitado");
    } else if ((ContrasenaVariable.length) == 0) {
        BotonActualizarDatos.disabled = false;
        BotonActualizarDatos.classList.add("BotonHabilitado");
    }
}