using APIBusinessLayer.ApplicationTypes;
using APIBusinessLayer.People;
using APIBusinessLayer.Users;
using APIDataAccessLayer.Application_Types;
using APIDataAccessLayer.People;
using APIDataAccessLayer.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_DVLD_Project.Controllers
{
    [Route("api/ApplicationTypes")]
    [ApiController]
    public class ApplicationTypesController : ControllerBase
    {
        [HttpGet("All", Name = "GetAllApplicationTypes")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<ApplicationTypesDTO>> GetAllApplicationTypes()
        {
            var ApplicationTypesList = ApplicationTypes.GetApplicationTypes();
            if (ApplicationTypesList.Count == 0)
            {
                return NotFound("No ApplicationTypes Found");
            }
            return Ok(ApplicationTypesList);

        }

        [HttpGet("IsExist/{ApplicationTypeID}", Name = "IsApplicationTypeExist")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<bool> IsApplicationTypeExist(int ApplicationTypeID)
        {
            if (ApplicationTypeID < 0)
            {
                return BadRequest("bad Request");
            }

            bool applicationType = ApplicationTypes.IsApplicationTypeExists(ApplicationTypeID);

            if (applicationType == false)
            {
                return NotFound(false);


            }


            return Ok(applicationType);
        }



        [HttpGet("FindByID/{ApplicationTypeID}", Name = "GetApplicationTypeByID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<ApplicationTypesDTO> GetApplicationTypeByID(int ApplicationTypeID)
        {
            if (ApplicationTypeID < 0)
            {
                return BadRequest("bad Request");
            }

            var applicationType = ApplicationTypes.FindByApplicationTypeID(ApplicationTypeID);

            if (applicationType == null)
            {
                return NotFound("No Application Type found");
            }

            ApplicationTypesDTO AtDTO = applicationType.AtDTO;


            return Ok(AtDTO);
        }


        [HttpPut("Update/{ApplicationTypeID}", Name = "UpdateApplicationType")]

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<ApplicationTypes> UpdateApplicationType(int ApplicationTypeID, ApplicationTypesDTO UpdateApplicationType)
        {
            if (UpdateApplicationType == null || ApplicationTypeID < 0 ||
                string.IsNullOrEmpty(UpdateApplicationType.ApplicationTypeTitle) ||
                UpdateApplicationType.ApplicationFees < 0
                )
            {
                return BadRequest("Invalid Application Type data");
            }
            if (!ApplicationTypes.IsApplicationTypeExists(ApplicationTypeID))
                return BadRequest("Invalid Application Type NotExist");




            var ApplicationType = ApplicationTypes.FindByApplicationTypeID(ApplicationTypeID);


            if (ApplicationType == null)
            {
                return NotFound("No Application Type  found");
            }

            ApplicationType.ApplicationTypeID = ApplicationTypeID;
            ApplicationType.ApplicationTypeTitle = UpdateApplicationType.ApplicationTypeTitle;
            ApplicationType.ApplicationFees = UpdateApplicationType.ApplicationFees;
           




            if (ApplicationType.Save())
            {
                //return the DTO not the Full Object
                return Ok(ApplicationType.AtDTO);
            }
            else
            {
                return StatusCode(500, new { message = " Error Updating Application Type" });
            }




        }


    }


}
