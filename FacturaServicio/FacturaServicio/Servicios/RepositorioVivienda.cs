using Dapper;
using FacturaServicio.Models;
using Microsoft.Data.SqlClient;

namespace FacturaServicio.Servicios
{
    public interface IRepositorioVivienda
    {
        void Crear(Vivienda Vivienda);
        Task<IEnumerable<Vivienda>> Obtener(int UsuarioId);
        Task<Vivienda> ObtenerId(int id, int UsuarioId);
    }
    public class RepositorioVivienda : IRepositorioVivienda
    {
        private readonly string connectionString;
        public RepositorioVivienda(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");

        }
        void IRepositorioVivienda.Crear(Vivienda Vivienda)
        {
            using var connection = new SqlConnection(connectionString);
            var id = connection.QuerySingle<int>($@"INSERT INTO Vivienda (Tipo,Estado)
                Values (@Tipo,@Estado);
                SELECT SCOPE_IDENTITY();", Vivienda);
            Vivienda.id = id;
        }


  async Task<IEnumerable<Vivienda>> IRepositorioVivienda.Obtener(int UsuarioId)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryAsync<Vivienda>(@"SELECT IdVivienda, Tipo, Estado
                                                            From Vivienda
                                                            Where UsuarioId = @UsuarioId", new { UsuarioId });
        }

        public async Task<Vivienda> ObtenerId(int id, int UsuarioId)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryFirstOrDefaultAsync<Vivienda>(
                 @"select * from Vivienda where IdVivienda= @id and UsuarioId= @UsuarioId",
                 new { id, UsuarioId });
        }
    }
}