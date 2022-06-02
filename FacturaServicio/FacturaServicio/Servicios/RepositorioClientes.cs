using Dapper;
using FacturaServicio.Models;
using Microsoft.Data.SqlClient;

namespace FacturaServicio.Servicios
{
    public interface IRepositorioClientes
    {
        Task Actualizar(DireccionViewModel direccion);
        Task Crear(Clientes clientes);
        Task Delete(int id);
        Task<DireccionViewModel> IdDireccion(int idClientes, int UsuarioId);
        Task<IEnumerable<DireccionViewModel>> Obtener(int UsuarioId);
        Task<Clientes> ObtenerId(int idClientes, int UsuarioId);
        Task Update(Clientes Clientes);
    }
    public class RepositorioClientes: IRepositorioClientes
    {
        private readonly string connectionString;
        public RepositorioClientes(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        async Task<IEnumerable<DireccionViewModel>> IRepositorioClientes.Obtener(int UsuarioId)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryAsync<DireccionViewModel>(@"select IdClientes,Cedula, Nombres, Apellidos, Telefono, Email
                                                          from Clientes
                                                            Where UsuarioId = @UsuarioId", new { UsuarioId });
        }
        public async Task <Clientes> ObtenerId (int idClientes, int UsuarioId)
        {
           using  var connection = new SqlConnection(connectionString);   
           return await connection.QueryFirstOrDefaultAsync<Clientes>(
                @"select * from Clientes where IdClientes= @idClientes and UsuarioId= @UsuarioId",
                new { idClientes, UsuarioId });
        }  
        public async Task Actualizar(DireccionViewModel direccion)
        {
            using var connection = new SqlConnection(connectionString);
           var id = await connection.ExecuteAsync($@"INSERT INTO Direccion (IdClientes, Tipo, No_Principal, Sufijo, No_Secundario,
              No_Complementario, CasaPropia, Fecha, Estado, IdRutas, IdVivienda, IdUsuario) values(@IdClientes, @Tipo, @No_Principal, @Sufijo, @No_Secundario,
              @No_Complementario, @CasaPropia, @Fecha, @Estado, @IdRutas, @IdVivienda, @IdUsuario);
              SELECT SCOPE_IDENTITY();", direccion);
            direccion.IdClientes= id;
        }

        public async Task<DireccionViewModel> IdDireccion (int idClientes, int UsuarioId)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryFirstOrDefaultAsync<DireccionViewModel>(
                 @"select * from Clientes where IdClientes= @idClientes and UsuarioId= @UsuarioId",
                 new { idClientes, UsuarioId });
        }

        public async Task Crear(Clientes clientes)
        {
            using var connection = new SqlConnection(connectionString);
            var id = await connection.ExecuteAsync($@"INSERT INTO Clientes (IdClientes, Cedula, Nombres, Apellidos, Telefono,
              Email, Estado, UsuarioId) values (@IdClientes, @Cedula, @Nombres, @Apellidos, @Telefono, @Email, @Estado, @IdUsuario);
              SELECT SCOPE_IDENTITY();", clientes);
            
        }
        public async Task Update(Clientes Clientes)
        {
            using var connection = new SqlConnection(connectionString);
            var id = await connection.ExecuteAsync($@"UPDATE Clientes set  Cedula = @Cedula, Nombres = @Nombres, Apellidos = @Apellidos, Telefono = @Telefono,
            Email = @Email, Estado = @Estado
             where IdClientes = @IdClientes", Clientes);
            Clientes.IdClientes = id;
        }

        public async Task Delete(int id)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync(@"Delete Clientes where IdClientes = @id", new { id });
        }

    }


}
