using APIDataAccessLayer.Application_Types;
using APIDataAccessLayer.People;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace APIDataAccessLayer.Applications.DTOs
{
    public class ApplicationDetailsDTO
    {


        public int ApplicationID { get; set; }
        public DateTime ApplicationDate { get; set; }
        public short ApplicationStatus { get; set; }
        public DateTime LastStatusDate { get; set; }
        public decimal PaidFees { get; set; }
        public int CreatedByUserID { get; set; }

        public int ApplicantPersonID { get; set; }
        public PersonsDTO personsDto { get; set; } = new PersonsDTO();


        public int ApplicationTypeID { get; set; }


        public ApplicationTypesDTO applicationTypeDto { get; set; } = new ApplicationTypesDTO();

        public ApplicationDetailsDTO()
        {

        }
        public ApplicationDetailsDTO(int applicationID, DateTime applicationDate, short applicationStatus,
            DateTime lastStatusDate, decimal paidFees, int createdByUserId,
            int applicantPersonId, PersonsDTO _personsDto,
            int applicationTypeId, ApplicationTypesDTO _applicationTypeDto)
        {
            ApplicationID = applicationID;
            ApplicationDate = applicationDate;
            ApplicationStatus = applicationStatus;
            LastStatusDate = lastStatusDate;
            PaidFees = paidFees;
            CreatedByUserID = createdByUserId;

            ApplicantPersonID = applicantPersonId;
            personsDto = _personsDto;

            ApplicationTypeID = applicationTypeId;
            applicationTypeDto = _applicationTypeDto;


        }




    }



}
