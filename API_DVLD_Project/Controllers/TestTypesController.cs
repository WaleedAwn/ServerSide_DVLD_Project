using APIBusinessLayer.ApplicationTypes;
using APIBusinessLayer.TestTypes;
using APIDataAccessLayer.Application_Types;
using APIDataAccessLayer.TestTypes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_DVLD_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestTypesController : ControllerBase
    {

        [HttpGet("All", Name = "GetAllTestTypes")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<TestTypesDTO>> GetAllTestTypes()
        {
            var testTypesList = TestTypes.GetTestTypes();
            if (testTypesList.Count == 0)
            {
                return NotFound("No Test Types Found");
            }
            return Ok(testTypesList);

        }

        [HttpGet("IsExist/{TestTypeID}", Name = "IsTestTypeExist")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<bool> IsTestTypeExist(int TestTypeID)
        {
            if (TestTypeID < 0)
            {
                return BadRequest("bad Request");
            }

            bool testType = TestTypes.IsTestTypeExists(TestTypeID);

            if (testType == false)
            {
                return NotFound(false);


            }


            return Ok(testType);
        }



        [HttpGet("FindByID/{TestTypeID}", Name = "GetTestTypeByID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<TestTypesDTO> GetTestTypeByID(int TestTypeID)
        {
            if (TestTypeID < 0)
            {
                return BadRequest("bad Request");
            }

            var testType = TestTypes.FindTestTypeByID(TestTypeID);

            if (testType == null)
            {
                return NotFound("No Test Type found");
            }

            TestTypesDTO tDTO = testType.TeDTO;


            return Ok(tDTO);
        }


        [HttpPut("Update/{TestTypeID}", Name = "UpdateTestType")]

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<TestTypesDTO> UpdateTestType(int TestTypeID, TestTypesDTO UpdateTestType)
        {
            if (UpdateTestType == null || TestTypeID < 0 ||
                string.IsNullOrEmpty(UpdateTestType.TestTypeTitle) ||
                string.IsNullOrEmpty(UpdateTestType.TestTypeDescription) ||
                UpdateTestType.TestTypeFees < 0
                )
            {
                return BadRequest("Invalid Test Type data");
            }
            if (!TestTypesData.IsTestTypeExist(TestTypeID))
                return BadRequest("Invalid Test Type NotExist");




            var TestType = TestTypes.FindTestTypeByID(TestTypeID);


            if (TestType == null)
            {
                return NotFound("No Test Type  found");
            }

            TestType.TestTypeID = TestTypeID;
            TestType.TestTypeTitle = UpdateTestType.TestTypeTitle;
            TestType.TestTypeDescription = UpdateTestType.TestTypeDescription;
            TestType.TestTypeFees = UpdateTestType.TestTypeFees;


            if (TestType.Save())
            {
                //return the DTO not the Full Object
                return Ok(TestType.TeDTO);
            }
            else
            {
                return StatusCode(500, new { message = " Error Updating Test Type" });
            }




        }



    }
}
