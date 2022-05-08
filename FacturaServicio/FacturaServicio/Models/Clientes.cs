namespace FacturaServicio.Models
{
    public class Clientes
    {
        public int IdClientes { get; set; }
        public string Cedula { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Telefono { get; set; }
        public string  Email { get; set; }
        public string Estado { get; set; }
        public int IdUsuario { get; set; }
    }
}
