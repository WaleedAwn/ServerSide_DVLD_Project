using APIDataAccessLayer.Application_Types;
using APIDataAccessLayer.Applications;
using APIDataAccessLayer.Applications.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIBusinessLayer.Applications
{
    public class ApplicationWithDetails
    {
        
        public static List<ApplicationDetailsDTO> GetApplications()
        {
            return ApplicationWithDetailsData.GetAllApplication();
        }
        public static ApplicationDetailsDTO FindApplicationDetailsByID(int AppID)
        {
            return ApplicationWithDetailsData.FindApplicationDetailsByID(AppID);
        }



    }




}
