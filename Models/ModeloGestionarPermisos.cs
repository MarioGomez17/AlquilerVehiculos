namespace ALQUILER_VEHICULOS.Models
{
    public class ModeloGestionarPermisos
    { 
        public List<ModeloRol> Roles {get; set;}
        public List<ModeloPermiso> Permisos {get; set;}
        public ModeloGestionarPermisos(){
            ModeloRol Rol = new();
            ModeloPermiso Permiso = new();
            this.Roles = Rol.TraerRoles();
            this.Permisos = Permiso.TraerPermisos();
        }
    }
}