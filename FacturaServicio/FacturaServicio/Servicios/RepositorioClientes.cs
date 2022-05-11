using Dapper;
using FacturaServicio.Models;
using Microsoft.Data.SqlClient;

namespace FacturaServicio.Servicios
{
    public interface IRepositorioClientes
    {
        Task Actualizar(DireccionViewModel direccion);
        Task<DireccionViewModel> IdDireccion(int idClientes, int UsuarioId);
        Task<IEnumerable<DireccionViewModel>> Obtener(int UsuarioId);
        Task<Clientes> ObtenerId(int idClientes, int UsuarioId);
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
    }
  

}
