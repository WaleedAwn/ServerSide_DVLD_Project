using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Numerics;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks; 
using Microsoft.Data.SqlClient;

namespace APIDataAccessLayer.People
{
  
    public class PersonsData
    {
        public static List<PersonsDTO> GetAllPersons()
        {
            var PersonsList = new List<PersonsDTO>();

            using (SqlConnection conn = new SqlConnection(ConnectionClass.ConnectionString))
            {
                string Query = "Select * from people";
                using (SqlCommand cmd = new SqlCommand(Query, conn))
                {
                    cmd.CommandType = CommandType.Text;

                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Use GetOrdinal to get the index for the ImagePath column
                            int ThirdNamePathIndex = reader.GetOrdinal("ThirdName");
                            string thirdName = reader.IsDBNull(ThirdNamePathIndex) ? "" : reader.GetString(ThirdNamePathIndex);

                            int emailPathIndex = reader.GetOrdinal("Email");
                            string Email = reader.IsDBNull(emailPathIndex) ? "" : reader.GetString(emailPathIndex);

                            int imagePathIndex = reader.GetOrdinal("ImagePath");
                            string imagepath = reader.IsDBNull(imagePathIndex) ? "" : reader.GetString(imagePathIndex);


                            PersonsList.Add(new PersonsDTO
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
                            ));
                        }
                    }
                }
            }

            return PersonsList;
        }
      
        public static PersonsDTO FindByNationalNo(string nationalNo)
        {

            using (SqlConnection conn = new SqlConnection(ConnectionClass.ConnectionString))
            {

                string Query = "Select * from people where NationalNo=@NationalNo";
                using (SqlCommand cmd = new SqlCommand(Query, conn))
                {
                    cmd.Parameters.AddWithValue("@NationalNo", nationalNo);

                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {

                            int ThirdNamePathIndex = reader.GetOrdinal("ThirdName");
                            string thirdName = reader.IsDBNull(ThirdNamePathIndex) ? "" : reader.GetString(ThirdNamePathIndex);

                            int emailPathIndex = reader.GetOrdinal("Email");
                            string Email = reader.IsDBNull(emailPathIndex) ? "" : reader.GetString(emailPathIndex);

                            int imagePathIndex = reader.GetOrdinal("ImagePath");
                            string imagepath = reader.IsDBNull(imagePathIndex) ? "" : reader.GetString(imagePathIndex);

                            // Use GetOrdinal to get the index for the ImagePath column
                            //int imagePathIndex = reader.GetOrdinal("ImagePath");
                            //string imagepath = reader.IsDBNull(imagePathIndex) ? "NULL" : reader.GetString(imagePathIndex);

                            return new PersonsDTO
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
                        }

                        else { return null; }
                    }
                }



            }

        }
        
        public static PersonsDTO FindByPersonID(int PersonID)
        {

            using (SqlConnection conn = new SqlConnection(ConnectionClass.ConnectionString))
            {

                string Query = "Select * from people where PersonID=@PersonID";
                using (SqlCommand cmd = new SqlCommand(Query, conn))
                {
                    cmd.Parameters.AddWithValue("@PersonID", PersonID);

                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int ThirdNamePathIndex = reader.GetOrdinal("ThirdName");
                            string thirdName = reader.IsDBNull(ThirdNamePathIndex) ? "" : reader.GetString(ThirdNamePathIndex);

                            int emailPathIndex = reader.GetOrdinal("Email");
                            string Email = reader.IsDBNull(emailPathIndex) ? "" : reader.GetString(emailPathIndex);

                            int imagePathIndex = reader.GetOrdinal("ImagePath");
                            string imagepath = reader.IsDBNull(imagePathIndex) ? "" : reader.GetString(imagePathIndex);

                            return new PersonsDTO
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
                        }

                        else { return null; }
                    }
                }



            }

        }

       
        public static int AddNewPerson(PersonsDTO newPersonDTOInfo)
        {
            int personID = -1;

            string query = @"
        INSERT INTO People (
            NationalNo, FirstName, SecondName, ThirdName,
            LastName, DateOfBirth, Gendor, Address,
            Phone, Email, NationalityCountryID, ImagePath
        )
        VALUES (
            @NationalNo, @FirstName, @SecondName, @ThirdName,
            @LastName, @DateOfBirth, @Gendor, @Address,
            @Phone, @Email, @NationalityCountryID, @ImagePath
        );
        SELECT SCOPE_IDENTITY();";

            using (var connection = new SqlConnection(ConnectionClass.ConnectionString))
            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@NationalNo", newPersonDTOInfo.NationalNo);
                command.Parameters.AddWithValue("@FirstName", newPersonDTOInfo.FirstName);
                command.Parameters.AddWithValue("@SecondName", newPersonDTOInfo.SecondName);
                command.Parameters.AddWithValue("@ThirdName", string.IsNullOrEmpty(newPersonDTOInfo.ThirdName)
                        ? DBNull.Value
                        : (object)newPersonDTOInfo.ThirdName);
                command.Parameters.AddWithValue("@LastName", newPersonDTOInfo.LastName);
                command.Parameters.AddWithValue("@DateOfBirth", newPersonDTOInfo.DateOfBirth.Date);
                command.Parameters.AddWithValue("@Gendor", newPersonDTOInfo.Gendor);
                command.Parameters.AddWithValue("@Address", newPersonDTOInfo.Address);
                command.Parameters.AddWithValue("@Phone", newPersonDTOInfo.Phone);
                command.Parameters.AddWithValue("@Email", string.IsNullOrEmpty(newPersonDTOInfo.Email)
                      ? DBNull.Value
                      : (object)newPersonDTOInfo.Email);
                //command.Parameters.AddWithValue("@Email", string.IsNullOrEmpty(newPersonDTOInfo.Email)
                //        ? DBNull.Value
                //        : (object)newPersonDTOInfo.Email);
                command.Parameters.AddWithValue("@NationalityCountryID", newPersonDTOInfo.NationalityCountryID);
                command.Parameters.AddWithValue("@ImagePath",
                    string.IsNullOrEmpty(newPersonDTOInfo.ImagePath)
                        ? DBNull.Value
                        : (object)newPersonDTOInfo.ImagePath);

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

        public static bool UpdatePerson(PersonsDTO UpdatedPersonInfo)
        {
            int rowsAffected = 0;

            using (var connection = new SqlConnection(ConnectionClass.ConnectionString))
            {
                string query = @"
            UPDATE People SET
                NationalNo = @NationalNo,
                FirstName = @FirstName,
                SecondName = @SecondName,
                ThirdName = @ThirdName,
                LastName = @LastName,
                DateOfBirth = @DateOfBirth,
                Gendor = @Gendor,
                Address = @Address,
                Phone = @Phone,
                Email = @Email,
                NationalityCountryID = @NationalityCountryID,
                ImagePath = @ImagePath
            WHERE PersonID = @PersonID";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@PersonID", UpdatedPersonInfo.PersonID);
                    command.Parameters.AddWithValue("@NationalNo", UpdatedPersonInfo.NationalNo);
                    command.Parameters.AddWithValue("@FirstName", UpdatedPersonInfo.FirstName);
                    command.Parameters.AddWithValue("@SecondName", UpdatedPersonInfo.SecondName);
                    //command.Parameters.AddWithValue("@ThirdName", UpdatedPersonInfo.ThirdName);
                    command.Parameters.AddWithValue("@ThirdName",
                        string.IsNullOrEmpty(UpdatedPersonInfo.ThirdName) ? DBNull.Value : (object)UpdatedPersonInfo.ThirdName);


                    command.Parameters.AddWithValue("@LastName", UpdatedPersonInfo.LastName);
                    command.Parameters.AddWithValue("@DateOfBirth", UpdatedPersonInfo.DateOfBirth.Date);
                    command.Parameters.AddWithValue("@Gendor", UpdatedPersonInfo.Gendor);
                    command.Parameters.AddWithValue("@Address", UpdatedPersonInfo.Address);
                    command.Parameters.AddWithValue("@Phone", UpdatedPersonInfo.Phone);
                    //command.Parameters.AddWithValue("@Email", UpdatedPersonInfo.Email);
                    command.Parameters.AddWithValue("@Email", 
                        string.IsNullOrEmpty(UpdatedPersonInfo.Email) ? DBNull.Value : (object)UpdatedPersonInfo.Email);

                    command.Parameters.AddWithValue("@NationalityCountryID", UpdatedPersonInfo.NationalityCountryID);
                    command.Parameters.AddWithValue("@ImagePath",
                        string.IsNullOrEmpty(UpdatedPersonInfo.ImagePath) ? DBNull.Value : (object)UpdatedPersonInfo.ImagePath);

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
       
        
        
        public static bool DeletePerson(int personID)
        {
            int rowsAffected = 0;

            using (var connection = new SqlConnection(ConnectionClass.ConnectionString))
            {
                string query = "DELETE FROM People WHERE PersonID = @PersonID";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@PersonID", personID);

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
        public static bool IsPersonExist(int personID)
        {
            using (var connection = new SqlConnection(ConnectionClass.ConnectionString))
            {
                string query = "SELECT 1 FROM People WHERE PersonID = @PersonID";

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
