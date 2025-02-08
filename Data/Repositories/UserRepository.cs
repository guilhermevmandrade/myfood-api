using Dapper;
using MyFood.Models;
using MyFood.Data.Repositories.Interfaces;
using MyFood.DTOs.Responses;
using MyFood.DTOs.Requests;

namespace MyFood.Data.Repositories
{
    /// <summary>
    /// Implementação do repositório de usuários.
    /// </summary>
    public class UserRepository : IUserRepository
    {
        private readonly DbSession _dbSession;

        /// <summary>
        /// Construtor que recebe as dependências necessárias para gerenciar a conexão com o banco de dados.
        /// </summary>
        /// <param name="dbSession"></param>
        public UserRepository(DbSession dbSession)
        {
            _dbSession = dbSession;
        }

        /// <summary>
        /// Registra um novo usuário na tabela <c>user</c>
        /// </summary>
        /// <param name="user">Informações do usuário a serem registradas.</param>
        public async Task CreateAsync(User user)
        {
            string query = @"INSERT INTO ""user"" (
                                name, email, password_hash, created_at, height, weight, activity_level
                            ) 
                             VALUES (
                                @Name, @Email, @PasswordHash, @CreatedAt, @Height, @Weight, @ActivityLevel
                            )";

            await _dbSession.Connection.ExecuteAsync(query, user, _dbSession.Transaction);
        }

        /// <summary>
        /// Obtém o usuário a partir do seu identificador.
        /// </summary>
        /// <param name="id">Identificador do usuário.</param>
        /// <returns>Informações do usuário correspondente.</returns>
        public async Task<GetUserResponse?> GetByIdAsync(int id)
        {
            string query = @"SELECT 
                                name AS Name,
                                email AS Email,
                                height AS Height,
                                weight AS Weight,
                                activity_level AS ActivityLevel
                            FROM ""user""
                            WHERE id = @Id";

            return await _dbSession.Connection.QueryFirstOrDefaultAsync<GetUserResponse>(query, new { Id = id });
        }

        /// <summary>
        /// Atualiza os dados do usuário.
        /// </summary>
        /// <param name="user">Dados do usuário a serem atualizados.</param>
        /// <param name="id">Identificador do usuário a atualizar</param>
        public async Task UpdateAsync(UpdateUserRequest user, int id)
        {
            string query = @"UPDATE ""user"" SET 
                                 name = @Name, 
                                 height = @Height,
                                 weight = @Weight,
                                 activity_level = @ActivityLevel
                             WHERE id = @Id";

            await _dbSession.Connection.ExecuteAsync(query, new { user.Name, user.Height, user.Weight, user.ActivityLevel, Id = id }, _dbSession.Transaction);
        }

        /// <summary>
        /// Deleta o usuário a partir do seu identificador.
        /// </summary>
        /// <param name="id">Identificador do usuário.</param>
        public async Task DeleteAsync(int id)
        {
            string query = "DELETE FROM \"user\" WHERE id = @Id";

            await _dbSession.Connection.ExecuteAsync(query, new { Id = id }, _dbSession.Transaction);
        }

        /// <summary>
        /// Obtém o usuário a partir do email.
        /// </summary>
        /// <param name="email">Email do usuário.</param>
        /// <returns>Usuário correspondente.</returns>
        public async Task<User?> GetByEmailAsync(string email)
        {
            string query = @"SELECT 
                                id AS Id,
                                name AS Name,
                                email AS Email,
                                password_hash AS PasswordHash
                            FROM ""user""
                            WHERE email = @Email";

            return await _dbSession.Connection.QueryFirstOrDefaultAsync<User>(query, new { Email = email });
        }
    }
}
