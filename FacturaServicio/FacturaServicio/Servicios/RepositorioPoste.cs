using Dapper;
using FacturaServicio.Models;
using Microsoft.Data.SqlClient;

namespace FacturaServicio.Servicios
{
    public interface IRepositorioPoste
    {
        Task<IEnumerable<Ruta>> Obtener(int UsuarioId);
    }

    public class RepositorioPoste : IRepositorioPoste

    {
        private readonly string connectionStirng;
        public RepositorioPoste(IConfiguration configuration)
        {
            connectionStirng = configuration.GetConnectionString("DefaultConnection");
        }
        public async Task<IEnumerable<Ruta>> Obtener(int UsuarioId)
        {
            using var connection = new SqlConnection(connectionStirng);
            return await connection.QueryAsync<Ruta>(@"Select IdRutas, Nombre
                                                        From Rutas
                                                        Where IdUsuario= @UsuarioId", new { UsuarioId });
        }
    }
} 