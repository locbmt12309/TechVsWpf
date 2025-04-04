using System;
using System.Configuration;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Data.SqlClient;
using BCrypt.Net;
using Serilog;

namespace WpfApp1.Models
{
    public class UserModel
    {
        private string connectionString;

        public UserModel()
        {

            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"]?.ConnectionString;

            if (string.IsNullOrEmpty(connectionString))
            {
                Log.Error("Connection string không tồn tại hoặc bị lỗi.");
                throw new Exception("Connection string không tồn tại hoặc bị lỗi trong app.config.");
            }
        }
        private string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
        public bool UserExists(string username)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var command = new SqlCommand("SELECT COUNT(*) FROM Users WHERE Username = @Username", connection);
                command.Parameters.AddWithValue("@Username", username);
                var result = (int)command.ExecuteScalar();
                return result > 0;
            }
        }

        public bool RegisterNewUser(string fullName, string username, string password)
        {
            try
            {
                string hashedPassword = HashPassword(password);

                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    var command = new SqlCommand("INSERT INTO Users (Name, Username, Password) VALUES (@FullName, @Username, @Password)", connection);
                    command.Parameters.AddWithValue("@FullName", fullName);
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@Password", hashedPassword);
                    int result = command.ExecuteNonQuery();
                    Log.Information("Đăng ký người dùng thành công: {Username}", username);
                    return result > 0;
                }

            }
            catch (Exception ex)
            {
                Log.Error(ex, "Đã xảy ra lỗi khi đăng ký người dùng.");
                return false;
            }
        }
        public bool ValidateUser(string username, string password)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    var command = new SqlCommand("SELECT Password FROM Users WHERE Username = @Username", connection);
                    command.Parameters.AddWithValue("@Username", username ?? string.Empty);

                    var result = command.ExecuteScalar();
                    if (result != null)
                    {
                        string storedHashedPassword = result.ToString();
                        Log.Information("Xác thực người dùng thành công: {Username}", username);
                        return BCrypt.Net.BCrypt.Verify(password, storedHashedPassword);
                    }

                    return false;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Đã xảy ra lỗi khi xác thực người dùng.");
                return false;
            }
        }


        public bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            try
            {
                if (!ValidateOldPassword(username, oldPassword))
                {
                    return false;
                }

                string hashedNewPassword = HashPassword(newPassword);

                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    var updateCommand = new SqlCommand("UPDATE Users SET Password = @NewPassword WHERE Username = @Username", connection);
                    updateCommand.Parameters.AddWithValue("@Username", username);
                    updateCommand.Parameters.AddWithValue("@NewPassword", hashedNewPassword);
                    updateCommand.ExecuteNonQuery();
                    Log.Information("Người dùng đổi mật khẩu thành công: {Username}", username);
                    return true;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Đã xảy ra lỗi khi đổi mật khẩu");
                return false;
            }
        }

        public bool ValidateOldPassword(string username, string oldPassword)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    var command = new SqlCommand("SELECT Password FROM Users WHERE Username = @Username", connection);
                    command.Parameters.AddWithValue("@Username", username ?? string.Empty);

                    var result = command.ExecuteScalar();
                    if (result != null)
                    {
                        string storedHashedPassword = result.ToString();
                        return BCrypt.Net.BCrypt.Verify(oldPassword, storedHashedPassword);
                    }
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
 

    public bool RegisterUser(string name, string username, string password)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    var checkUserCommand = new SqlCommand("SELECT COUNT(*) FROM Users WHERE Username = @Username", connection);
                    checkUserCommand.Parameters.AddWithValue("@Username", username);

                    var result = (int)checkUserCommand.ExecuteScalar();
                    if (result > 0) return false;

                    var insertCommand = new SqlCommand("INSERT INTO Users (Name, Username, Password) VALUES (@Name, @Username, @Password)", connection);
                    insertCommand.Parameters.AddWithValue("@Name", name);
                    insertCommand.Parameters.AddWithValue("@Username", username);
                    insertCommand.Parameters.AddWithValue("@Password", password);

                    insertCommand.ExecuteNonQuery();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
