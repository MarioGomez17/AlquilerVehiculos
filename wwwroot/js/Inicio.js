const InputFechaInicio = document.getElementById('FiltroFechaInicio');
const FechaActual = new Date();
const FehcaLimiteFechaInicial = new Date(FechaActual);
FehcaLimiteFechaInicial.setDate(FehcaLimiteFechaInicial.getDate() + 2);
const FormatoFehcaLimiteFechaInicial = FehcaLimiteFechaInicial.toISOString().slice(0, 16);
InputFechaInicio.min = FormatoFehcaLimiteFechaInicial;

InputFechaInicio.addEventListener('change', function () {
    const InputFechaFin = document.getElementById("FiltroFechaFin");
    InputFechaFin.disabled = false;
    if(InputFechaFin.value == ""){
        const FechaInputFehcaInicio = new Date(InputFechaInicio.value);
        const FechaLimiteFechaFinal = new Date(FechaInputFehcaInicio);
        FechaLimiteFechaFinal.setDate(FechaLimiteFechaFinal.getDate() + 1);
        const FormatoFechaLimiteFechaFinal = FechaLimiteFechaFinal.toISOString().slice(0, 16);
        InputFechaFin.min = FormatoFechaLimiteFechaFinal;
    }else{
        const FechaInputFehcaInicio = new Date(InputFechaInicio.value);
        const FechaNuevaFechaFinal = new Date(FechaInputFehcaInicio);
        FechaNuevaFechaFinal.setDate(FechaNuevaFechaFinal.getDate() + 1);
        InputFechaFin.value = FechaNuevaFechaFinal.toISOString().slice(0, 16);
    }
    
});