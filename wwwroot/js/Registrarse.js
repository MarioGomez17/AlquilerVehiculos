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