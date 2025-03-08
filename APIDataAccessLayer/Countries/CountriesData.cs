using APIDataAccessLayer.People;
using Microsoft.Data.SqlClient;
using System.Data;

namespace APIDataAccessLayer.Countries
{

   
    public class CountriesData
    {
        public static List<CountriesDTO> GetAllCountries()
        {
            var CountryList = new List<CountriesDTO>();

            using (SqlConnection conn = new SqlConnection(ConnectionClass.ConnectionString))
            {
                string Query = "select * from Countries";
                using (SqlCommand cmd = new SqlCommand(Query, conn))
                {
                    cmd.CommandType = CommandType.Text;

                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            CountryList.Add(new CountriesDTO
                            (
                                reader.GetInt32(reader.GetOrdinal("CountryID")),
                                reader.GetString(reader.GetOrdinal("CountryName"))
                           
                            ));
                        }
                    }
                }
                conn.Close();
            }

            return CountryList;
        }

        public static CountriesDTO FindByID(int CountryID)
        {

            using (SqlConnection conn = new SqlConnection(ConnectionClass.ConnectionString))
            {

                string Query = "Select * from Countries where CountryID=@countryId";
                using (SqlCommand cmd = new SqlCommand(Query, conn))
                {
                    cmd.Parameters.AddWithValue("@countryId", CountryID);

                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                           
                            return new CountriesDTO
                             (   reader.GetInt32(reader.GetOrdinal("CountryID")),
                                reader.GetString(reader.GetOrdinal("CountryName"))
                               
                             );
                        }

                        else { return null; }
                    }
                }



            }

        }
        public static CountriesDTO FindCountryByName(string CountryName)
        {

            using (SqlConnection conn = new SqlConnection(ConnectionClass.ConnectionString))
            {

                string Query = "Select * from Countries where CountryName=@CountryName";
                using (SqlCommand cmd = new SqlCommand(Query, conn))
                {
                    cmd.Parameters.AddWithValue("@CountryName", CountryName);

                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {

                            return new CountriesDTO
                             (reader.GetInt32(reader.GetOrdinal("CountryID")),
                                reader.GetString(reader.GetOrdinal("CountryName"))

                             );
                        }

                        else { return null; }
                    }
                }



            }

        }


        public static int AddNewCountry(CountriesDTO newCountryDTOInfo)
        {
            int countryId = -1;

            string query = @"
        INSERT INTO Countries (CountryName )
        VALUES (@CountryName);
        SELECT SCOPE_IDENTITY();";

            using (var connection = new SqlConnection(ConnectionClass.ConnectionString))
            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@CountryName", newCountryDTOInfo.CountryName);
               
                try
                {
                    connection.Open();
                    var result = command.ExecuteScalar();
                    if (result != null && int.TryParse(result.ToString(), out int insertedID))
                    {
                        countryId = insertedID;
                    }
                }
                catch (Exception)
                {
                    // Log the exception as needed
                    countryId = -1;
                }
            }

            return countryId;
        }

        public static bool UpdateCountry(CountriesDTO UpdatedCountryInfo)
        {
            int rowsAffected = 0;

            using (var connection = new SqlConnection(ConnectionClass.ConnectionString))
            {
                string query = @"
                 UPDATE Countries SET
                    CountryName = @CountryName              
                WHERE CountryID = @CountryID";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CountryName", UpdatedCountryInfo.CountryName);
                    command.Parameters.AddWithValue("@CountryID", UpdatedCountryInfo.CountryID);
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

        public static bool DeleteCountry(int CountryId)
        {
            int rowsAffected = 0;

            using (var connection = new SqlConnection(ConnectionClass.ConnectionString))
            {
                string query = "DELETE FROM Countries WHERE CountryID = @CountryID";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CountryID", CountryId);

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
        public static bool IsCountryExist(int CountryId)
        {
            using (var connection = new SqlConnection(ConnectionClass.ConnectionString))
            {
                string query = "SELECT 1 FROM Countries WHERE CountryID = @CountryID";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CountryID", CountryId);

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
