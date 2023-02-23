namespace P620231_API.ModelsDTOs
{
    public class UserDTO
    {

        //UN DTO (Data Transfer Object) sirve para:
        //1. Para que el equipo de desarrollo de los front ends (app en este caso) 
        //no entiendan la estructura real de la tabla a nivel de base de datos.

        //2. Simplificar objetos complejos en estructuras mas simples para que los 
        //Json resultante sean mucho más fáciles de gestionar

        //3. En caso de que se deba regenrar los modelos por medios de
        //scaffold -f los controles seguirán trabajando con normalidad


        //En este caso he decidido escribir los nombres en español solo por ejemplo de uso de DTOs
        public int IDUsuario { get; set; }
        public string Nombre { get; set; } = null!;
        public string Correo { get; set; } = null!;
        public string NumeroTelefono { get; set; } = null!;
        public string Contrasennia { get; set; } = null!;
        public string? Cedula { get; set; }
        public string? Direccion { get; set; }
        public int IdRol { get; set; }
        public int IdEstado { get; set; }
        public string EstadoDescripcion { get; set; } = null!;
        public string RolDescripcion { get; set; } = null!;


    }
}
