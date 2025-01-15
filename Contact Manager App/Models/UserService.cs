using Contact_Manager_App.Models;
using MySql.Data.MySqlClient;

namespace Contact_Manager_App.Services;

public class UserService
{
    private readonly string _connectionString;

    public UserService(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<List<User>> GetAllUsersAsync()
    {
        var users = new List<User>();

        using var connection = new MySqlConnection(_connectionString);
        await connection.OpenAsync();

        string query = "SELECT * FROM Users";
        using var command = new MySqlCommand(query, connection);
        using var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            users.Add(new User
            {
                Id = reader.GetInt32(0),
                Name = reader.GetString(1),
                DateOfBirth = reader.GetDateTime(2),
                IsMarried = reader.GetBoolean(3),
                Phone = reader.GetString(4),
                Salary = reader.GetDecimal(5)
            });
        }

        return users;
    }

    public async Task AddUserAsync(User user)
    {
        using var connection = new MySqlConnection(_connectionString);
        await connection.OpenAsync();

        string query = "INSERT INTO Users (Name, DateOfBirth, IsMarried, Phone, Salary) VALUES (@Name, @DateOfBirth, @IsMarried, @Phone, @Salary)";
        using var command = new MySqlCommand(query, connection);

        command.Parameters.AddWithValue("@Name", user.Name);
        command.Parameters.AddWithValue("@DateOfBirth", user.DateOfBirth);
        command.Parameters.AddWithValue("@IsMarried", user.IsMarried);
        command.Parameters.AddWithValue("@Phone", user.Phone);
        command.Parameters.AddWithValue("@Salary", user.Salary);

        await command.ExecuteNonQueryAsync();
    }
}
