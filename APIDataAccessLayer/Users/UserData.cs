using APIDataAccessLayer.People;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIDataAccessLayer.Users
{
 
    public class UserData
    {
      
        public static List<PersonUserDTO> GetAllUsers()
        {
            var UsersList = new List<PersonUserDTO>();

            using (SqlConnection conn = new SqlConnection(ConnectionClass.ConnectionString))
            {
                // SQL query to join Users and People tables
                string Query = @"
            SELECT 
                u.UserID,
                u.PersonID,
                u.UserName,
                u.Password,
                u.IsActive,
                p.PersonID,
                p.NationalNo,
                p.FirstName,
                p.SecondName,
                p.ThirdName,
                p.LastName,
                p.DateOfBirth,
                p.Gendor,
                p.Address,
                p.Phone,
                p.Email,
                p.NationalityCountryID,
                p.ImagePath
            FROM 
                Users u
            INNER JOIN 
                People p ON u.PersonID = p.PersonID";

                using (SqlCommand cmd = new SqlCommand(Query, conn))
                {
                    cmd.CommandType = CommandType.Text;

                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            int ThirdNamePathIndex = reader.GetOrdinal("ThirdName");
                            string thirdName = reader.IsDBNull(ThirdNamePathIndex) ? "" : reader.GetString(ThirdNamePathIndex);

                            int emailPathIndex = reader.GetOrdinal("Email");
                            string Email = reader.IsDBNull(emailPathIndex) ? "" : reader.GetString(emailPathIndex);

                            int imagePathIndex = reader.GetOrdinal("ImagePath");
                            string imagepath = reader.IsDBNull(imagePathIndex) ? "" : reader.GetString(imagePathIndex);

                            var person = new PersonsDTO
                             (
                                 reader.GetInt32(reader.GetOrdinal("PersonID")),
                                 reader.GetString(reader.GetOrdinal("NationalNo")),
                                 reader.GetString(reader.GetOrdinal("FirstName")),
                                 reader.GetString(reader.GetOrdinal("SecondName")),
                                 //reader.GetString(reader.GetOrdinal("ThirdName")),
                                 thirdName,
                                 reader.GetString(reader.GetOrdinal("LastName")),
                                 reader.GetDateTime(reader.GetOrdinal("DateOfBirth")),
                                 reader.GetByte(reader.GetOrdinal("Gendor")),
                                 reader.GetString(reader.GetOrdinal("Address")),
                                 reader.GetString(reader.GetOrdinal("Phone")),
                                  //reader.GetString(reader.GetOrdinal("Email")),
                                  Email,
                             reader.GetInt32(reader.GetOrdinal("NationalityCountryID")),
                                 imagepath // Use the processed imagepath variable
                             );
                            // Create UsersDTO object and include the PersonsDTO object
                            UsersList.Add(new PersonUserDTO(
                                reader.GetInt32(reader.GetOrdinal("UserID")),
                                reader.GetInt32(reader.GetOrdinal("PersonID")),
                                reader.GetString(reader.GetOrdinal("UserName")),
                                reader.GetString(reader.GetOrdinal("Password")),
                                reader.GetBoolean(reader.GetOrdinal("IsActive")),
                                person
                            ));
                        }
                    }
                }
            }

            return UsersList;
        }

        public static UsersDTO FindByUserID(int UserID)
        {

            using (SqlConnection conn = new SqlConnection(ConnectionClass.ConnectionString))
            {

                string Query = "select * from Users WHERE UserID=@UserID ";

                using (SqlCommand cmd = new SqlCommand(Query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", UserID);

                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {

                            return new UsersDTO(

                                reader.GetInt32(reader.GetOrdinal("UserID")),
                                reader.GetInt32(reader.GetOrdinal("PersonID")),
                                reader.GetString(reader.GetOrdinal("UserName")),
                                reader.GetString(reader.GetOrdinal("Password")),
                                 reader.GetBoolean(reader.GetOrdinal("IsActive"))
                           
                             );
                        }

                        else { return null; }
                    }
                }



            }

        }

        public static UsersDTO FindByUserName(string UserName)
        {

            using (SqlConnection conn = new SqlConnection(ConnectionClass.ConnectionString))
            {

                string Query = "select * from Users WHERE UserName=@UserName ";

                using (SqlCommand cmd = new SqlCommand(Query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserName", UserName);

                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {

                            return new UsersDTO(

                                reader.GetInt32(reader.GetOrdinal("UserID")),
                                reader.GetInt32(reader.GetOrdinal("PersonID")),
                                reader.GetString(reader.GetOrdinal("UserName")),
                                reader.GetString(reader.GetOrdinal("Password")),
                                 reader.GetBoolean(reader.GetOrdinal("IsActive"))

                             );
                        }

                        else { return null; }
                    }
                }



            }

        }

        public static int AddNewUser(UsersDTO newUserDTO)
        {
            int personID = -1;
           
            string query = @"
        INSERT INTO Users ( PersonID, UserName, Password,IsActive)
        VALUES (@PersonID, @UserName, @Password, @IsActive);
        SELECT SCOPE_IDENTITY();";

            using (var connection = new SqlConnection(ConnectionClass.ConnectionString))
            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@PersonID", newUserDTO.PersonID);
                command.Parameters.AddWithValue("@UserName", newUserDTO.UserName);
                command.Parameters.AddWithValue("@Password", newUserDTO.Password);
                command.Parameters.AddWithValue("@IsActive", newUserDTO.IsActive);
           
               
                try
                {
                    connection.Open();
                    var result = command.ExecuteScalar();
                    if (result != null && int.TryParse(result.ToString(), out int insertedID))
                    {
                        personID = insertedID;
                    }
                }
                catch (Exception)
                {
                    // Log the exception as needed
                    personID = -1;
                }
            }

            return personID;
        }

        public static bool UpdateUser(UsersDTO newUserDTO)
        {
          
            int rowsAffected = 0;

            using (var connection = new SqlConnection(ConnectionClass.ConnectionString))
            {
                string query = @"
            UPDATE Users SET
                PersonID = @PersonID,
                UserName = @UserName,
                Password = @Password,
                IsActive = @IsActive
            WHERE UserID = @UserID";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserID", newUserDTO.UserID);
                    command.Parameters.AddWithValue("@PersonID", newUserDTO.PersonID);
                    command.Parameters.AddWithValue("@UserName", newUserDTO.UserName);
                    command.Parameters.AddWithValue("@Password", newUserDTO.Password);
                    command.Parameters.AddWithValue("@IsActive", newUserDTO.IsActive);

                    try
                    {
                        connection.Open();
                        rowsAffected = command.ExecuteNonQuery();
                    }
                    catch (Exception)
                    {
                        // Log exception if needed
                        return false;
                    }
                }
            }

            return rowsAffected > 0;
        }
    
        public static bool DeleteUser(int UserID)
        {
            int rowsAffected = 0;

            using (var connection = new SqlConnection(ConnectionClass.ConnectionString))
            {
                string query = "DELETE FROM Users WHERE UserID=@UserID";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserID", UserID);

                    try
                    {
                        connection.Open();
                        rowsAffected = command.ExecuteNonQuery();
                    }
                    catch (Exception)
                    {
                        // Log the exception if needed
                        return false;
                    }
                }
            }

            return rowsAffected > 0;
        }
        public static bool IsUserExist(int UserID)
        {
            using (var connection = new SqlConnection(ConnectionClass.ConnectionString))
            {
                string query = "SELECT 1 FROM Users WHERE UserID=@UserID";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserID", UserID);

                    try
                    {
                        connection.Open();
                        var result = command.ExecuteScalar();

                        // If result is not null, the person exists
                        return result != null;
                    }
                    catch (Exception)
                    {
                        // Log the exception if necessary
                        return false;
                    }
                }
            }
        }

        public static bool IsUserUserNameExist(string username)
        {
            using (var connection = new SqlConnection(ConnectionClass.ConnectionString))
            {
                string query = "SELECT 1 FROM Users WHERE UserName=@UserName";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserName", username);

                    try
                    {
                        connection.Open();
                        var result = command.ExecuteScalar();

                        // If result is not null, the person exists
                        return result != null;
                    }
                    catch (Exception)
                    {
                        // Log the exception if necessary
                        return false;
                    }
                }
            }
        }
        public static bool IsPersonExistAsUser(int personID)
        {
            using (var connection = new SqlConnection(ConnectionClass.ConnectionString))
            {
                string query = "SELECT 1 FROM Users WHERE PersonID=@PersonID";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@PersonID", personID);

                    try
                    {
                        connection.Open();
                        var result = command.ExecuteScalar();

                        // If result is not null, the person exists
                        return result != null;
                    }
                    catch (Exception)
                    {
                        // Log the exception if necessary
                        return false;
                    }
                }
            }
        }



    }

   
 

}
