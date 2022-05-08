namespace FacturaServicio.Models
{
    public class Direccion
    {
        public int IdDireccion { get; set; }
        public int IdCliente { get; set; }
        public string Tipo { get; set; }
        public string No_Principal { get; set; }
        public string Sulfijo { get; set; }
        public string No_Secundario { get; set; }
        public string No_Complementario{ get; set; }
        public string CasaPropia { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Today;
        public string Estado     { get; set; }

        public int IdRutas { get; set; }
        public int IdVivienda { get; set; }
        public int IdUsuario { get; set; }
    }
}
