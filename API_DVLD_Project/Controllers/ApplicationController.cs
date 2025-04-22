using API_DVLD_Project.Globals;
using APIBusinessLayer.Applications;
using APIBusinessLayer.ApplicationTypes;
using APIBusinessLayer.People;
using APIBusinessLayer.Users;
using APIDataAccessLayer.Application_Types;
using APIDataAccessLayer.Applications.DTOs;
using APIDataAccessLayer.People;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_DVLD_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationController : ControllerBase
    {

        [HttpGet("All", Name = "GetAllApplications")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<ApplicationDetailsDTO>> GetAllApplications()
        {
            var ApplicationsList = ApplicationWithDetails.GetApplications();
            if (ApplicationsList.Count == 0)
            {
                return NotFound("No Applications Found");
            }
            return Ok(ApplicationsList);

        }


        [HttpGet("FindDetailsByID/{ApplicationID}", Name = "FindApplicationDetailsByID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<ApplicationDetailsDTO> FindApplicationDetailsByID(int ApplicationID)
        {
            if (ApplicationID < 0)
            {
                return BadRequest("bad Request");
            }

            var application = ApplicationWithDetails.FindApplicationDetailsByID(ApplicationID);

            if (application == null)
            {
                return NotFound("No Application  found");
            }


            return Ok(application);
        }



        // Application Without Any Extra Details

        [HttpGet("IsExist/{ApplicationID}", Name = "IsApplicationExist")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<bool> IsApplicationExist(int ApplicationID)
        {
            if (ApplicationID < 0)
            {
                return BadRequest("bad Request");
            }

            bool application = Applications.IsApplicationExists(ApplicationID);

            if (application == false)
            {
                return NotFound(false);


            }


            return Ok(application);
        }


        [HttpGet("FindByID/{ApplicationID}", Name = "GetApplicationByID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<ApplicationDTO> GetApplicationByID(int ApplicationID)
        {
            if (ApplicationID < 0)
            {
                return BadRequest("bad Request");
            }

            var application = Applications.FindByApplicationID(ApplicationID);

            if (application == null)
            {
                return NotFound("No Application  found");
            }

            //ApplicationDTO ADTO = application.ADTO;


            return Ok(application.ADTO);
        }


        [HttpPut("Update/{ApplicationID}", Name = "UpdateApplication")]

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<ApplicationDTO> UpdateApplication(int ApplicationID, ApplicationDTO UpdateApplication)
        {
            if (UpdateApplication == null || ApplicationID < 0 ||
                UpdateApplication.ApplicationTypeID < 0 ||
                UpdateApplication.ApplicantPersonID < 0 ||
                UpdateApplication.PaidFees < 0
                )
            {
                return BadRequest("Invalid Application  data");
            }
            if (!Applications.IsApplicationExists(ApplicationID))
                return BadRequest("Invalid Application  NotExist");




            var Application = Applications.FindByApplicationID(ApplicationID);


            if (Application == null)
            {
                return NotFound("No Application   found");
            }

            Application.ApplicationID = ApplicationID;
            Application.ApplicationDate = UpdateApplication.ApplicationDate;
            Application.ApplicationStatus = UpdateApplication.ApplicationStatus;
            Application.LastStatusDate = UpdateApplication.LastStatusDate;

            Application.PaidFees = UpdateApplication.PaidFees;
            Application.CreatedByUserID = UpdateApplication.CreatedByUserID;
            Application.ApplicantPersonID = UpdateApplication.ApplicantPersonID;
            Application.ApplicationTypeID = UpdateApplication.ApplicationTypeID;




            if (Application.Save())
            {
                //return the DTO not the Full Object
                return Ok(Application.ADTO);
            }
            else
            {
                return StatusCode(500, new { message = " Error Updating Application " });
            }


        }

        [HttpPost("Add", Name = "AddApplication")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public ActionResult<ApplicationDTO> AddNewApplication(ApplicationDTO newApplicationDTO)
        {
            if (newApplicationDTO == null
                || newApplicationDTO.ApplicantPersonID < 0
                || newApplicationDTO.ApplicationTypeID < 0
                || newApplicationDTO.CreatedByUserID < 0
                || string.IsNullOrEmpty(newApplicationDTO.ApplicationDate.ToString())
                || string.IsNullOrEmpty(newApplicationDTO.LastStatusDate.ToString())
               )
            {
                return BadRequest("Invalid Application data");
            }
            if (!ApplicationTypes.IsApplicationTypeExists(newApplicationDTO.ApplicationTypeID) 
                || !People.IsPersonExists(newApplicationDTO.ApplicantPersonID)
                || !Users.IsUserExists(newApplicationDTO.CreatedByUserID))
            {
                return BadRequest("Invalid Application data");
            }


            Applications applicatioon = new APIBusinessLayer.Applications.Applications(
                new ApplicationDTO(newApplicationDTO.ApplicationID,
                newApplicationDTO.ApplicationDate, newApplicationDTO.ApplicationStatus, newApplicationDTO.LastStatusDate,
                newApplicationDTO.PaidFees, newApplicationDTO.CreatedByUserID, newApplicationDTO.ApplicantPersonID,
                newApplicationDTO.ApplicationTypeID)

                );

            applicatioon.Save();

            newApplicationDTO.ApplicationID = applicatioon.ApplicationID;

            return CreatedAtRoute("GetApplicationByID", new { ApplicationID = newApplicationDTO.ApplicationID }, newApplicationDTO);


        }


    }
}
