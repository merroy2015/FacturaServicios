namespace FacturaServicio.Servicios
{
    public interface IServiciosUsuarios
    {
        int ObtenerUsuarioid();
    }

    public class ServiciosUsuarios : IServiciosUsuarios 
    {
        public int ObtenerUsuarioid()
        {
            return 1;   
        }

    }
}
