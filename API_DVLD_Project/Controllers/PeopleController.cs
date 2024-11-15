using APIBusinessLayer.People;
using APIDataAccessLayer.People;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using API_DVLD_Project.Globals;
namespace API_DVLD_Project.Controllers
{
    
    [Route("api/People")]
    [ApiController]
    public class PeopleController : ControllerBase
    {


        [HttpGet("All", Name = "GetAllPersons")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<PersonsDTO>> GetAllPersons()
        {
            var PersonList = People.GetAllPersons();
            if (PersonList.Count == 0)
            {
                return NotFound("No Persons Found");
            }
            return Ok(PersonList);

        }

        [HttpGet("FindByID/{PersonID}", Name = "GetPersonByPersonID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<PersonsDTO> GetPersonByID(int  PersonID)
        {
            if ((int)PersonID<0)
            {
                return BadRequest("bad Request");
            }

            People person = People.FindByPersonID((int)PersonID);

            if (person == null)
            {
                return NotFound("No Person found");
            }

            PersonsDTO sDTO = person.SDTO;


            return Ok(sDTO);
        }



        [HttpGet("IsExist/{PersonID}", Name = "IsPersonExist")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<bool> IsPersonExist(int PersonID)
        {
            if (PersonID<0)
            {
                return BadRequest("bad Request");
            }

            bool person = People.IsPersonExists(PersonID);

            if (person == false)
            {
                return false;
            }


            return Ok(person);
        }




        [HttpPost("Add", Name = "AddPerson")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public ActionResult<PersonsDTO> AddNewPerson(PersonsDTO newPersonDTO)
        {
            if (newPersonDTO == null || string.IsNullOrEmpty(newPersonDTO.NationalNo)
               
                || string.IsNullOrEmpty(newPersonDTO.FirstName)|| string.IsNullOrEmpty(newPersonDTO.SecondName)
                || string.IsNullOrEmpty(newPersonDTO.ThirdName) || string.IsNullOrEmpty(newPersonDTO.LastName)
                || string.IsNullOrEmpty(newPersonDTO.DateOfBirth.ToString())
               || string.IsNullOrEmpty(newPersonDTO.Address) )
            {
                return BadRequest("Invalid Person data");
            }
            if (!clsValidation.CheckGender(newPersonDTO.Gendor))
            {
                return BadRequest("Invalid Person data");
            }

            People person = new APIBusinessLayer.People.People(
                new PersonsDTO(newPersonDTO.PersonID,
                newPersonDTO.NationalNo, newPersonDTO.FirstName, newPersonDTO.SecondName,
                newPersonDTO.ThirdName, newPersonDTO.LastName, newPersonDTO.DateOfBirth, 
                newPersonDTO.Gendor, newPersonDTO.Address, newPersonDTO.Phone, newPersonDTO.Email,
                newPersonDTO.NationalityCountryID, newPersonDTO.ImagePath)
                
                );

            person.Save();

            newPersonDTO.PersonID = person.PersonID;
            
            return CreatedAtRoute("GetPersonByPersonID", new { PersonID = newPersonDTO.PersonID }, newPersonDTO);


        }

       

        [HttpPut("Update/{PersonID}", Name = "UpdatePerson")]

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<PersonsDTO> UpdatePerson(int PersonID, PersonsDTO UpdatePersonDTO)
        {
            if (UpdatePersonDTO == null || PersonID<0 || string.IsNullOrEmpty(UpdatePersonDTO.NationalNo)

                || string.IsNullOrEmpty(UpdatePersonDTO.FirstName) || string.IsNullOrEmpty(UpdatePersonDTO.SecondName)
                || string.IsNullOrEmpty(UpdatePersonDTO.ThirdName) || string.IsNullOrEmpty(UpdatePersonDTO.LastName)
                || string.IsNullOrEmpty(UpdatePersonDTO.DateOfBirth.ToString())
               || string.IsNullOrEmpty(UpdatePersonDTO.Address))
            {
                return BadRequest("Invalid Person data");
            }
            if (!clsValidation.CheckGender(UpdatePersonDTO.Gendor))
            {
                return BadRequest("Invalid Person data");
            }



            var persons = People.FindByPersonID(PersonID);


            if (persons == null)
            {
                return NotFound("No Persons found");
            }

            persons.PersonID = PersonID;
            persons.NationalNo = UpdatePersonDTO.NationalNo;
            persons.FirstName = UpdatePersonDTO.FirstName;
            persons.SecondName = UpdatePersonDTO.SecondName;
            persons.ThirdName = UpdatePersonDTO.ThirdName;
            persons.LastName = UpdatePersonDTO.LastName;

            persons.DateOfBirth = UpdatePersonDTO.DateOfBirth;
            persons.Gendor = UpdatePersonDTO.Gendor;
            persons.Address = UpdatePersonDTO.Address;

            persons.Phone = UpdatePersonDTO.Phone;
            persons.Email = UpdatePersonDTO.Email;
            persons.NationalityCountryID = UpdatePersonDTO.NationalityCountryID;
            persons.ImagePath = UpdatePersonDTO.ImagePath;



            if (persons.Save())
            {
                //return the DTO not the Full Object
                return Ok(persons.SDTO);
            }
            else
            {
                return StatusCode(500, new { message = " Error Updating Person" });
            }




        }

        [HttpGet("Find/{NationalNo}", Name = "GetPersonByNationalNo")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<PersonsDTO> GetPersonByNationalNO(string NationalNo)
        {
            if (NationalNo.IsNullOrEmpty())
            {
                return BadRequest("bad Request");
            }

            People person = People.FindByNationalNo(NationalNo);

            if (person == null)
            {
                return NotFound("No Person found");
            }

            PersonsDTO sDTO = person.SDTO;


            return Ok(sDTO);
        }


        [HttpDelete("Delete/{id}", Name = "DeletePerson")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]

        public ActionResult DeletePerson(int id)
        {
            if (id < 1)
            {
                return BadRequest("bad Request");
            }


            if (!People.IsPersonExists(id))
            {
                return NotFound($"Person with ID {id} not exist");
            }

            //if (People.CheckPersonRelations(id))
            //    return Conflict("Can't delete this Person because it has relations in other tables");


            if (People.DeletePerson(id))
            {
                return Ok($"Person with ID {id} has been deleted");
            }
            else
            {
                return StatusCode(500, new { message = $"Error deleting person with ID {id}" });
            }

            //waleed alhakimi

        }




    }




}

//"personID": 0,
//  "nationalNo": "AWA",
//  "firstName": "sdf",
//  "secondName": "fdg",
//  "thirdName": "fgh",
//  "lastName": "fgh",
//  "dateOfBirth": "2024-11-12",
//  "gendor": 1,
//  "address": "fdghfgh",
//  "phone": "fhgg",
//  "email": "WA@gmail.com",
//  "nationalityCountryID": 90,
//  "imagePath": ""
