using BadHunter.DAL.Models;
using Dapper;
using Npgsql;

namespace BadHunter.DAL
{
    public class AuthDAL : IAuthDAL
    {
        public async Task<UserModel> GetUser(string email)
        {
            using (var connection = new NpgsqlConnection(DbHelper.ConnectionString))
            {
                connection.Open();
                return await connection.QueryFirstOrDefaultAsync<UserModel>($@"
                        select UserId, Email, Password, Salt, Status
                        from AppUser
                        where Email = @email", new { email = email }) ?? new UserModel();
            }
        }
        public async Task<UserModel> GetUser(int id)
        {
            using (var connection = new NpgsqlConnection(DbHelper.ConnectionString))
            {
                connection.Open();
                return await connection.QueryFirstOrDefaultAsync<UserModel>($@"
                        select UserId, Email, Password, Salt, Status
                        from AppUser
                        where UserId = @id", new { id = id }) ?? new UserModel();
            }
        }
        public async Task<int> CreateUser(UserModel model)
        {
            using (var connection = new NpgsqlConnection(DbHelper.ConnectionString))
            {
                connection.Open();
                string sql = @"insert into AppUser(Email, Password, Salt, Status)
                                values(@Email, @Password, @Salt, @Status)";
                return await connection.ExecuteAsync(sql, model);
            }
        }
    }
}