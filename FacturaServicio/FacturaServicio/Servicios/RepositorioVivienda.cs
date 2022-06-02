using Dapper;
using FacturaServicio.Models;
using Microsoft.Data.SqlClient;

namespace FacturaServicio.Servicios
{
    public interface IRepositorioVivienda
    {
       Task Crear(Vivienda Vivienda);
        Task Delete(int id);
        Task<IEnumerable<Vivienda>> Obtener(int UsuarioId);
        Task<Vivienda> ObtenerId(int id, int UsuarioId);
        Task Update(Vivienda vivienda);
    }
    public class RepositorioVivienda : IRepositorioVivienda
    {
        private readonly string connectionString;
        public RepositorioVivienda(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");

        }
          public async Task Crear(Vivienda Vivienda)
        {
            using var connection = new SqlConnection(connectionString);
            var id = await connection.QuerySingleAsync($@"INSERT INTO Vivienda (Tipo,Estado,UsuarioId)
                Values (@Tipo,@Estado,@UsuarioId);
                SELECT SCOPE_IDENTITY();", Vivienda);
          
        }


  async Task<IEnumerable<Vivienda>> IRepositorioVivienda.Obtener(int UsuarioId)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryAsync<Vivienda>(@"SELECT id, Tipo, Estado
                                                            From Vivienda
                                                            Where UsuarioId = @UsuarioId", new { UsuarioId });
        }

        public async Task<Vivienda> ObtenerId(int id, int UsuarioId)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryFirstOrDefaultAsync<Vivienda>(
                 @"select * from Vivienda where id= @id and UsuarioId= @UsuarioId",
                 new { id, UsuarioId });
        }
       public async Task Delete( int id)
        {
          using var connection = new SqlConnection(connectionString);
          await connection.ExecuteAsync(@"Delete Vivienda where id= @id", new {id });
        }
        public async Task Update(Vivienda vivienda)
        {
            using var connection = new SqlConnection(connectionString);
            var id = await connection.ExecuteAsync($@"UPDATE vivienda set tipo = @tipo, Estado = @Estado
             where id = @id", vivienda);
            vivienda.id = id;
        }
    }
}