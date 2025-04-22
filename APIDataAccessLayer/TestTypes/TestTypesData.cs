using Microsoft.Data.SqlClient;
using System.Data;

namespace APIDataAccessLayer.TestTypes
{
    // TestTypeID
    //TestTypeTitle
    // TestTypeDescription
    //TestTypeFees 

    public class TestTypesData
    {
        public static List<TestTypesDTO> GetAllTestTypes()
        {
            var testTypesList = new List<TestTypesDTO>();

            using (SqlConnection conn = new SqlConnection(ConnectionClass.ConnectionString))
            {
                string Query = "select * from TestTypes ";
                using (SqlCommand cmd = new SqlCommand(Query, conn))
                {
                    cmd.CommandType = CommandType.Text;
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {  
                         testTypesList.Add(
                             new TestTypesDTO(

                                     reader.GetInt32(reader.GetOrdinal("TestTypeID")),
                              
                                     reader.GetString(reader.GetOrdinal("TestTypeTitle")),
                                     reader.GetString(reader.GetOrdinal("TestTypeDescription")),
                                     reader.GetDecimal(reader.GetOrdinal("TestTypeFees"))
                             )
                         );
                        }
                    }
                }
            }

            return testTypesList;
        }

        public static bool IsTestTypeExist(int testTypeID)
        {


            using (var connection = new SqlConnection(ConnectionClass.ConnectionString))
            {
                string query = "SELECT 1 FROM TestTypes WHERE TestTypeID=@TestTypeID";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@TestTypeID", testTypeID);

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

        public static TestTypesDTO FindByTestTypeID(int testTypeID)
        {

            using (SqlConnection conn = new SqlConnection(ConnectionClass.ConnectionString))
            {

                string Query = "select * from TestTypes WHERE TestTypeID=@TestTypeID ";

                using (SqlCommand cmd = new SqlCommand(Query, conn))
                {
                    cmd.Parameters.AddWithValue("@TestTypeID", testTypeID);

                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {

                            return new TestTypesDTO(

                                  reader.GetInt32(reader.GetOrdinal("TestTypeID")),
                                 reader.GetString(reader.GetOrdinal("TestTypeTitle")),
                                 reader.GetString(reader.GetOrdinal("TestTypeDescription")),
                                 reader.GetDecimal(reader.GetOrdinal("TestTypeFees"))

                             );
                        }

                        else { return null; }
                    }
                }



            }

        }

        public static bool UpdateTestType(TestTypesDTO UpdateTestType)
        {
            // TestTypeID
            //TestTypeTitle
            // TestTypeDescription
            //TestTypeFees

            int rowsAffected = 0;

            using (var connection = new SqlConnection(ConnectionClass.ConnectionString))
            {
                string query = @"
            UPDATE TestTypes SET
                
                TestTypeTitle = @TestTypeTitle,
                TestTypeDescription = @TestTypeDescription   ,   
             TestTypeFees = @TestTypeFees   
  
                 WHERE TestTypeID = @TestTypeID";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@TestTypeID", UpdateTestType.TestTypeID);
                    command.Parameters.AddWithValue("@TestTypeTitle", UpdateTestType.TestTypeTitle);
                    command.Parameters.AddWithValue("@TestTypeDescription", UpdateTestType.TestTypeDescription);
                    command.Parameters.AddWithValue("@TestTypeFees", UpdateTestType.TestTypeFees);


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


    }





}
