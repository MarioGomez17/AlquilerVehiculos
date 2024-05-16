function ActivarDivCorreo(){
    const DivEnviarCorreo = document.getElementById("DivEnviarCorreo");
    DivEnviarCorreo.style.display = "flex";
    DivEnviarCorreo.style.justifyContent = "center";
    DivEnviarCorreo.style.alignItems = "center";
    const TablaReporte = document.getElementById("TablaReporte");
    TablaReporte.style.opacity = 0;
    const DivLinksExportar = document.getElementById("DivLinksExportar");
    DivLinksExportar.style.opacity = 0;
}

function DesactivarDivCorreo(){
    const DivEnviarCorreo = document.getElementById("DivEnviarCorreo");
    DivEnviarCorreo.style.display = "none";
    const TablaReporte = document.getElementById("TablaReporte");
    TablaReporte.style.opacity = 1;
    const DivLinksExportar = document.getElementById("DivLinksExportar");
    DivLinksExportar.style.opacity = 1;
}

const BotonEnviarCorreo = document.getElementById("BotonEnviarCorreo");
BotonEnviarCorreo.addEventListener("click", ActivarDivCorreo);

const BotonCerrarCorreo = document.getElementById("BotonCerrarCorreo");
BotonCerrarCorreo.addEventListener("click", DesactivarDivCorreo);