function ActivarDivCorreoReportesAlquileresAlquilador(){
    const DivEnviarCorreo = document.getElementById("EnviarReportesAlquileresAlquiladorPorCorreo");
    DivEnviarCorreo.style.display = "flex";
    DivEnviarCorreo.style.justifyContent = "center";
    DivEnviarCorreo.style.alignItems = "center";
}

function DesactivarDivCorreoAlquileresAlquilador(){
    const DivEnviarCorreo = document.getElementById("EnviarReportesAlquileresAlquiladorPorCorreo");
    DivEnviarCorreo.style.display = "none";
}

const BotonEnviarCorreoAlquileresAlquilador = document.getElementById("BotonEnviarReportesAlquileresAlquiladorPorCorreo");
BotonEnviarCorreoAlquileresAlquilador.addEventListener("click", ActivarDivCorreoReportesAlquileresAlquilador);

const BotonCerrarCorreoAlquileresAlquilador = document.getElementById("BotonCerrarReportesAlquileresAlquiladorPorCorreo");
BotonCerrarCorreoAlquileresAlquilador.addEventListener("click", DesactivarDivCorreoAlquileresAlquilador);


function ActivarDivCorreoReportesAlquileresPropietario(){
    const DivEnviarCorreo = document.getElementById("EnviarReportesAlquileresPropietarioPorCorreo");
    DivEnviarCorreo.style.display = "flex";
    DivEnviarCorreo.style.justifyContent = "center";
    DivEnviarCorreo.style.alignItems = "center";
}

function DesactivarDivCorreoReportesAlquileresPropietario(){
    const DivEnviarCorreo = document.getElementById("EnviarReportesAlquileresPropietarioPorCorreo");
    DivEnviarCorreo.style.display = "none";
}

const BotonEnviarCorreoAlquileresPropietario = document.getElementById("BotonEnviarReportesAlquileresPropietarioPorCorreo");
BotonEnviarCorreoAlquileresPropietario.addEventListener("click", ActivarDivCorreoReportesAlquileresPropietario);

const BotonCerrarCorreoAlquileresPropietario = document.getElementById("BotonCerrarReportesAlquileresPropietarioPorCorreo");
BotonCerrarCorreoAlquileresPropietario.addEventListener("click", DesactivarDivCorreoReportesAlquileresPropietario);