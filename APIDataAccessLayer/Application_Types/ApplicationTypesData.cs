using APIDataAccessLayer.Users;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIDataAccessLayer.Application_Types
{

    public class ApplicationTypesData
    {
        public static List<ApplicationTypesDTO> GetAllApplicationTypes()
        {
            var ApplicationTypesList = new List<ApplicationTypesDTO>();

            using (SqlConnection conn = new SqlConnection(ConnectionClass.ConnectionString))
            {
                string Query = "select * from ApplicationTypes; ";
                using (SqlCommand cmd = new SqlCommand(Query, conn))
                {
                    cmd.CommandType = CommandType.Text;
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ApplicationTypesList.Add(new ApplicationTypesDTO(

                                 reader.GetInt32(reader.GetOrdinal("ApplicationTypeID")),
                                 reader.GetString(reader.GetOrdinal("ApplicationTypeTitle")),
                                 reader.GetDecimal(reader.GetOrdinal("ApplicationFees"))
                               )
                             );
                        }
                    }
                }
            }

            return ApplicationTypesList;
        }

        public static bool IsApplicationTypeExist(int ApplicationTypeID)
        {
            using (var connection = new SqlConnection(ConnectionClass.ConnectionString))
            {
                string query = "SELECT 1 FROM ApplicationTypes WHERE ApplicationTypeID=@ApplicationTypeID";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);

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

        public static ApplicationTypesDTO FindByApplicationTypeID(int ApplicationTypeID)
        {

            using (SqlConnection conn = new SqlConnection(ConnectionClass.ConnectionString))
            {

                string Query = "select * from ApplicationTypes WHERE ApplicationTypeID=@ApplicationTypeID ";

                using (SqlCommand cmd = new SqlCommand(Query, conn))
                {
                    cmd.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);

                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {

                            return new ApplicationTypesDTO(

                               reader.GetInt32(reader.GetOrdinal("ApplicationTypeID")),
                                 reader.GetString(reader.GetOrdinal("ApplicationTypeTitle")),
                                 reader.GetDecimal(reader.GetOrdinal("ApplicationFees"))

                             );
                        }

                        else { return null; }
                    }
                }



            }

        }

        public static bool UpdateApplicationType(ApplicationTypesDTO UpdateApplicationType)
        {

            int rowsAffected = 0;

            using (var connection = new SqlConnection(ConnectionClass.ConnectionString))
            {
                string query = @"
            UPDATE ApplicationTypes SET
                
                ApplicationTypeTitle = @ApplicationTypeTitle,
                ApplicationFees      = @ApplicationFees     
            WHERE ApplicationTypeID = @ApplicationTypeID";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ApplicationTypeID", UpdateApplicationType.ApplicationTypeID);
                    command.Parameters.AddWithValue("@ApplicationTypeTitle", UpdateApplicationType.ApplicationTypeTitle);
                    command.Parameters.AddWithValue("@ApplicationFees", UpdateApplicationType.ApplicationFees);
                 

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
