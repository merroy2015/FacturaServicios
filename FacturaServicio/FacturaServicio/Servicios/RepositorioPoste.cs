using Dapper;
using FacturaServicio.Models;
using Microsoft.Data.SqlClient;

namespace FacturaServicio.Servicios
{
    public interface IRepositorioPoste
    {
        Task Crear(PosteCreacionViewModel poste);
        Task Delete(int id);
        Task<IEnumerable<Poste>> Listado(int UsuarioId);
        Task<IEnumerable<Ruta>> Obtener(int UsuarioId);
        Task<Poste> ObtenerId(int id, int UsuarioId);
        Task Update(Poste Poste);
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
            return await connection.QueryAsync<Ruta>(@"Select id, Nombre
                                                        From Rutas
                                                        Where UsuarioId= @UsuarioId", new { UsuarioId });
        }

        public async Task<IEnumerable<Poste>> Listado(int UsuarioId)
        {
            using var connection = new SqlConnection(connectionStirng);
            return await connection.QueryAsync<Poste>(@"Select id, Nombre, Longitud,latitud
                                                        From Poste
                                                        Where UsuarioId= @UsuarioId", new { UsuarioId });
        }
        public async Task Delete(int id)
        {
            using var connection = new SqlConnection(connectionStirng);
            await connection.ExecuteAsync(@"Delete Poste where id= @id", new { id });
        }
        public async Task<Poste> ObtenerId(int id, int UsuarioId)
        {
            using var connection = new SqlConnection(connectionStirng);
            return await connection.QueryFirstOrDefaultAsync<Poste>(
                 @"Select * from  Poste where id= @id and UsuarioId= @UsuarioId",
                 new { id, UsuarioId });
        }
        public async Task Update(Poste Poste)
        {
            using var connection = new SqlConnection(connectionStirng);
            var id = await connection.ExecuteAsync($@"UPDATE Poste set Nombre = @Nombre, Longitud = @Longitud, latitud = @Longitud
             where id= @id", Poste);
            Poste.id = id;
        }
        public async Task Crear(PosteCreacionViewModel poste)
        {
            using var connection = new SqlConnection(connectionStirng);
            var id = await connection.QuerySingleAsync($@"INSERT INTO Poste (IdRutas, Nombre, Longitud, latitud, UsuarioId)
                Values (@IdRutas, @Nombre, @Longitud, @UsuarioId);
                SELECT SCOPE_IDENTITY();", poste);
               
        }



    }
}