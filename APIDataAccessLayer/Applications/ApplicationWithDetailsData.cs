using APIDataAccessLayer.Application_Types;
using APIDataAccessLayer.Applications.DTOs;
using APIDataAccessLayer.Globals;
using APIDataAccessLayer.People;
using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace APIDataAccessLayer.Applications;

public class ApplicationWithDetailsData
{
    public static List<ApplicationDetailsDTO> GetAllApplication()
    {
        var ApplicationList = new List<ApplicationDetailsDTO>();

        using (SqlConnection conn = new SqlConnection(ConnectionClass.ConnectionString))
        {
            string Query = "select *" +
                " from Applications a" +
                " JOIN ApplicationTypes at " +
                "ON  a.ApplicationTypeID =at.ApplicationTypeID " +
                "JOIN people p " +
                "ON a.ApplicantPersonID=p.PersonID";
            using (SqlCommand cmd = new SqlCommand(Query, conn))
            {
                cmd.CommandType = CommandType.Text;
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        var app = new ApplicationDetailsDTO(

                          reader.GetInt32(reader.GetOrdinal("ApplicationID")),

                           reader.GetDateTime(reader.GetOrdinal("ApplicationDate")),
                           reader.GetByte(reader.GetOrdinal("ApplicationStatus")),
                           reader.GetDateTime(reader.GetOrdinal("LastStatusDate")),
                           reader.GetDecimal(reader.GetOrdinal("PaidFees")),
                           reader.GetInt32(reader.GetOrdinal("CreatedByUserID")),

                           reader.GetInt32(reader.GetOrdinal("ApplicantPersonID")),

                         new PersonsDTO
                        (
                            reader.GetInt32(reader.GetOrdinal("PersonID")),
                            reader.GetString(reader.GetOrdinal("NationalNo")),
                            reader.GetString(reader.GetOrdinal("FirstName")),
                            reader.GetString(reader.GetOrdinal("SecondName")),
                            //reader.GetString(reader.GetOrdinal("ThirdName")),
                            GlobalParameter.GetParameterValue("ThirdName", reader),
                            reader.GetString(reader.GetOrdinal("LastName")),
                            reader.GetDateTime(reader.GetOrdinal("DateOfBirth")),
                            reader.GetByte(reader.GetOrdinal("Gendor")),
                            reader.GetString(reader.GetOrdinal("Address")),
                            reader.GetString(reader.GetOrdinal("Phone")),
                        //reader.GetString(reader.GetOrdinal("Email")),
                          GlobalParameter.GetParameterValue("Email", reader),

                          reader.GetInt32(reader.GetOrdinal("NationalityCountryID")),
                          GlobalParameter.GetParameterValue("ImagePath", reader)

                        ),



                           reader.GetInt32(reader.GetOrdinal("ApplicationTypeID")),

                           new ApplicationTypesDTO(

                             reader.GetInt32(reader.GetOrdinal("ApplicationTypeID")),
                             reader.GetString(reader.GetOrdinal("ApplicationTypeTitle")),
                             reader.GetDecimal(reader.GetOrdinal("ApplicationFees"))
                           )

                          );


                        ApplicationList.Add(app);
                    }
                }
            }
        }

        return ApplicationList;
    }


    public static ApplicationDetailsDTO FindApplicationDetailsByID(int applicationId)
    {
        var ApplicationList = new ApplicationDetailsDTO();

        using (SqlConnection conn = new SqlConnection(ConnectionClass.ConnectionString))
        {
            string Query = "select *" +
                " from Applications a" +
                " JOIN ApplicationTypes at " +
                "ON  a.ApplicationTypeID =at.ApplicationTypeID " +
                "JOIN people p " +
                "ON a.ApplicantPersonID=p.PersonID " +
                "WHERE ApplicationID=@ApplicationID";

            using (SqlCommand cmd = new SqlCommand(Query, conn))
            {
                cmd.Parameters.AddWithValue("@ApplicationID", applicationId);             
                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {

                        return new ApplicationDetailsDTO(

                          reader.GetInt32(reader.GetOrdinal("ApplicationID")),

                           reader.GetDateTime(reader.GetOrdinal("ApplicationDate")),
                           reader.GetByte(reader.GetOrdinal("ApplicationStatus")),
                           reader.GetDateTime(reader.GetOrdinal("LastStatusDate")),
                           reader.GetDecimal(reader.GetOrdinal("PaidFees")),
                           reader.GetInt32(reader.GetOrdinal("CreatedByUserID")),

                           reader.GetInt32(reader.GetOrdinal("ApplicantPersonID")),

                         new PersonsDTO
                        (
                            reader.GetInt32(reader.GetOrdinal("PersonID")),
                            reader.GetString(reader.GetOrdinal("NationalNo")),
                            reader.GetString(reader.GetOrdinal("FirstName")),
                            reader.GetString(reader.GetOrdinal("SecondName")),
                            //reader.GetString(reader.GetOrdinal("ThirdName")),
                            GlobalParameter.GetParameterValue("ThirdName", reader),
                            reader.GetString(reader.GetOrdinal("LastName")),
                            reader.GetDateTime(reader.GetOrdinal("DateOfBirth")),
                            reader.GetByte(reader.GetOrdinal("Gendor")),
                            reader.GetString(reader.GetOrdinal("Address")),
                            reader.GetString(reader.GetOrdinal("Phone")),
                        //reader.GetString(reader.GetOrdinal("Email")),
                          GlobalParameter.GetParameterValue("Email", reader),

                          reader.GetInt32(reader.GetOrdinal("NationalityCountryID")),
                          GlobalParameter.GetParameterValue("ImagePath", reader)

                        ),



                           reader.GetInt32(reader.GetOrdinal("ApplicationTypeID")),

                           new ApplicationTypesDTO(

                             reader.GetInt32(reader.GetOrdinal("ApplicationTypeID")),
                             reader.GetString(reader.GetOrdinal("ApplicationTypeTitle")),
                             reader.GetDecimal(reader.GetOrdinal("ApplicationFees"))
                           )

                          );


                    }

                    else { return null; }
                }



            }
        }

       
    }


}
