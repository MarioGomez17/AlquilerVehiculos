namespace ALQUILER_VEHICULOS.Models
{
    public class ModeloEditarUsuario
    {
        public ModeloUsuario Usuario { get; set; }
        public List<ModeloTipoIdentificacionUsuario> TiposIdentificacion { get; set; }
        public List<ModeloRol> Roles{get; set;}
    }
}
