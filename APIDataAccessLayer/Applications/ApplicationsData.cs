using APIDataAccessLayer.Application_Types;
using APIDataAccessLayer.Applications.DTOs;
using APIDataAccessLayer.Countries;
using APIDataAccessLayer.Globals;
using APIDataAccessLayer.People;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Reflection.PortableExecutable;

namespace APIDataAccessLayer.Applications;

public class ApplicationsData
{
 
    public static bool IsApplicationExist(int ApplicationID)
    {
        using (var connection = new SqlConnection(ConnectionClass.ConnectionString))
        {
            string query = "SELECT 1 FROM Applications WHERE ApplicationID=@ApplicationID";

            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

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

    public static ApplicationDTO FindByApplicationID(int ApplicationID)
    {

        using (SqlConnection conn = new SqlConnection(ConnectionClass.ConnectionString))
        {

            string Query = "select * from Applications WHERE ApplicationID=@ApplicationID ";

            using (SqlCommand cmd = new SqlCommand(Query, conn))
            {
                cmd.Parameters.AddWithValue("@ApplicationID", ApplicationID);

                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {

                        return new ApplicationDTO(

                          reader.GetInt32(reader.GetOrdinal("ApplicationID")),                      
                           reader.GetDateTime(reader.GetOrdinal("ApplicationDate")),
                            reader.GetByte(reader.GetOrdinal("ApplicationStatus")),
                           reader.GetDateTime(reader.GetOrdinal("LastStatusDate")),
                           reader.GetDecimal(reader.GetOrdinal("PaidFees")),
                           reader.GetInt32(reader.GetOrdinal("CreatedByUserID")),
                            reader.GetInt32(reader.GetOrdinal("ApplicantPersonID")),
                           reader.GetInt32(reader.GetOrdinal("ApplicationTypeID"))
                         );


                    }

                    else { return null; }
                }



            }



        }

    }


    public static int AddNewApplication(ApplicationDTO UpdateApplication)
    {
        int countryId = -1;

        string query = @"
        INSERT INTO Applications (
            ApplicationDate, ApplicationStatus, LastStatusDate, PaidFees,
            CreatedByUserID, ApplicantPersonID, ApplicationTypeID
        )
        VALUES (
            @ApplicationDate, @ApplicationStatus, @LastStatusDate, @PaidFees,
            @CreatedByUserID, @ApplicantPersonID, @ApplicationTypeID
        );
        SELECT SCOPE_IDENTITY();";

        using (var connection = new SqlConnection(ConnectionClass.ConnectionString))
        using (var command = new SqlCommand(query, connection))
        {
            command.Parameters.AddWithValue("@ApplicationDate", UpdateApplication.ApplicationDate);
            command.Parameters.AddWithValue("@ApplicationStatus", UpdateApplication.ApplicationStatus);
            command.Parameters.AddWithValue("@LastStatusDate", UpdateApplication.LastStatusDate);
            command.Parameters.AddWithValue("@PaidFees", UpdateApplication.PaidFees);
            command.Parameters.AddWithValue("@CreatedByUserID", UpdateApplication.CreatedByUserID);
            command.Parameters.AddWithValue("@ApplicantPersonID", UpdateApplication.ApplicantPersonID);
            command.Parameters.AddWithValue("@ApplicationTypeID", UpdateApplication.ApplicationTypeID);

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


    public static bool UpdateApplication(ApplicationDTO UpdateApplication)
    {

        int rowsAffected = 0;

        using (var connection = new SqlConnection(ConnectionClass.ConnectionString))
        {
           

            string query = @"
            UPDATE Applications SET
                
                ApplicationDate = @ApplicationDate,
                ApplicationStatus = @ApplicationStatus , 
                LastStatusDate = @LastStatusDate,
                PaidFees = @PaidFees ,
                CreatedByUserID = @CreatedByUserID,
                ApplicantPersonID = @ApplicantPersonID ,
                ApplicationTypeID = @ApplicationTypeID 
            WHERE ApplicationID = @ApplicationID";

            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@ApplicationID", UpdateApplication.ApplicationID);
                command.Parameters.AddWithValue("@ApplicationDate", UpdateApplication.ApplicationDate);
                command.Parameters.AddWithValue("@ApplicationStatus", UpdateApplication.ApplicationStatus);
                command.Parameters.AddWithValue("@LastStatusDate", UpdateApplication.LastStatusDate);
                command.Parameters.AddWithValue("@PaidFees", UpdateApplication.PaidFees);
                command.Parameters.AddWithValue("@CreatedByUserID", UpdateApplication.CreatedByUserID);
                command.Parameters.AddWithValue("@ApplicantPersonID", UpdateApplication.ApplicantPersonID);
                command.Parameters.AddWithValue("@ApplicationTypeID", UpdateApplication.ApplicationTypeID);           
                
                
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

    public static bool DeleteApplication(int ApplicationID)
    {
        int rowsAffected = 0;

        using (var connection = new SqlConnection(ConnectionClass.ConnectionString))
        {
            string query = "DELETE FROM Applications WHERE ApplicationID = @ApplicationID";

            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

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

    public static bool ChangeApplicationStatus(ApplicationDTO UpdateApplication)
    {

        int rowsAffected = 0;

        using (var connection = new SqlConnection(ConnectionClass.ConnectionString))
        {


            string query = @"
            UPDATE Applications SET
                ApplicationStatus = @ApplicationStatus  
                LastStatusDate = @LastStatusDate,
               
            WHERE ApplicationID = @ApplicationID";

            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@ApplicationID", UpdateApplication.ApplicationID);
                command.Parameters.AddWithValue("@ApplicationStatus", UpdateApplication.ApplicationStatus);
                command.Parameters.AddWithValue("@LastStatusDate", UpdateApplication.LastStatusDate);          
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