function ActivarDivCorreo(){
    const DivEnviarCorreo = document.getElementById("DivEnviarCorreo");
    DivEnviarCorreo.style.display = "flex";
    DivEnviarCorreo.style.justifyContent = "center";
    DivEnviarCorreo.style.alignItems = "center";
}

function DesactivarDivCorreo(){
    const DivEnviarCorreo = document.getElementById("DivEnviarCorreo");
    DivEnviarCorreo.style.display = "none";
}

const BotonEnviarCorreo = document.getElementById("BotonEnviarCorreo");
BotonEnviarCorreo.addEventListener("click", ActivarDivCorreo);

const BotonCerrarCorreo = document.getElementById("BotonCerrarCorreo");
BotonCerrarCorreo.addEventListener("click", DesactivarDivCorreo);