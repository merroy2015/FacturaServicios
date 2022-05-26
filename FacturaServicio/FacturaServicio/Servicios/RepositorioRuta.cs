using Dapper;
using FacturaServicio.Models;
using Microsoft.Data.SqlClient;

namespace FacturaServicio.Servicios
{
    public interface IRepositorioRuta
    {
        Task Crear(Ruta ruta);
        Task Delete(int id);
        Task<IEnumerable<Ruta>> Obtener(int UsuarioId);
        Task<Ruta> ObtenerId(int IdRutas, int UsuarioId);
        Task Update(Ruta Rutas);
    }
    public class RepositorioRuta : IRepositorioRuta
    {
        private readonly string connectionString;
        public RepositorioRuta(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<IEnumerable<Ruta>> Obtener(int UsuarioId)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryAsync<Ruta>(@"Select id, Nombre, LongitudIni
                                                        From Rutas
                                                        Where UsuarioId= @UsuarioId", new { UsuarioId });
        }
        public async Task<Ruta> ObtenerId(int id, int UsuarioId)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryFirstOrDefaultAsync<Ruta>(
                 @"Select * from  Rutas where id= @id and UsuarioId= @UsuarioId",
                 new { id, UsuarioId });
        }
        public async Task Delete(int id)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync(@"Delete rutas where id= @id", new { id });
        }

        public async Task Update(Ruta Rutas)
        {
            using var connection = new SqlConnection(connectionString);
            var id = await connection.ExecuteAsync($@"UPDATE Rutas set Nombre = @Nombre, LongitudIni = @LongitudIni
             where id= @id", Rutas);
            Rutas.id = id;
        }
        public async Task Crear(Ruta ruta)
        {
            using var connection = new SqlConnection(connectionString);
            var id = await connection.QuerySingleAsync($@"INSERT INTO Rutas (Nombre, LongitudIni, LongitudFin, LatitudIni, LatitudFin, Estado, UsuarioId)
                Values (@Nombre, @LongitudIni, @LongitudFin, @LatitudIni, @LatitudFin, @Estado, @UsuarioId);
                SELECT SCOPE_IDENTITY();", ruta);
            
        }


    }

}
       
        

