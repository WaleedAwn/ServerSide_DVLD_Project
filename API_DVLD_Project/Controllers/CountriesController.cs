using API_DVLD_Project.Globals;
using APIBusinessLayer.Countries;
using APIBusinessLayer.People;
using APIDataAccessLayer.Countries;
using APIDataAccessLayer.People;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_DVLD_Project.Controllers
{
    [Route("api/Countries")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        [HttpGet("All", Name = "GetAllCountries")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<CountriesDTO>> GetAllCountries()
        {
            var CountriesList = Countries.GetAllCountries();
            if (CountriesList.Count == 0)
            {
                return NotFound("No Countries Found");
            }
            return Ok(CountriesList);

        }

        [HttpGet("FindByID/{CountryID}", Name = "GetCountryByID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<CountriesDTO> GetCountryByID(int CountryID)
        {
            if (CountryID < 0)
            {
                return BadRequest("bad Request");
            }

            var Country = Countries.FindByID(CountryID);

            if (Country == null)
            {
                return NotFound("No Country found");
            }

            CountriesDTO sDTO = Country.SDTO;


            return Ok(sDTO);
        }

        [HttpGet("FindByName/{CountryName}", Name = "GetCountryByName")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<CountriesDTO> GetCountryByName(string CountryName)
        {
            if (string.IsNullOrWhiteSpace(CountryName))
            {
                return BadRequest("bad Request");
            }

            var Country = Countries.FindCountryByName(CountryName);

            if (Country == null)
            {
                return NotFound("No Country found");
            }

            CountriesDTO sDTO = Country.SDTO;


            return Ok(sDTO);
        }



        [HttpGet("IsExist/{CountryID}", Name = "IsCountryExist")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<bool> IsCountryExist(int CountryID)
        {
            if (CountryID < 0)
            {
                return BadRequest("bad Request");
            }

            bool Country = Countries.IsCountryExists(CountryID);

            if (Country == false)
            {
                return false;
            }


            return Ok(Country);
        }




        [HttpPost("Add", Name = "AddCountry")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<CountriesDTO> AddNewCountry(CountriesDTO newCountryDTO)
        {
            if (newCountryDTO == null || string.IsNullOrEmpty(newCountryDTO.CountryName))
            {
                return BadRequest("Invalid Country data");
            }
           

            Countries Country =  new  APIBusinessLayer.Countries.Countries(
                new CountriesDTO(newCountryDTO.CountryID,newCountryDTO.CountryName) 
                
                );
             
            Country.Save();

            newCountryDTO.CountryID = Country.CountryID;

            return CreatedAtRoute("GetCountryByID", new { CountryID = newCountryDTO.CountryID }, newCountryDTO);


        }




        [HttpPut("Update/{CountryID}", Name = "UpdateCountry")]

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<CountriesDTO> UpdateCountry(int CountryID, CountriesDTO UpdateCountryDTO)
        {
            if (CountryID < 1)
                return BadRequest("Invalid Country ID ");


            if (UpdateCountryDTO == null | string.IsNullOrEmpty(UpdateCountryDTO.CountryName))
            {
                return BadRequest("Invalid Country data");
            }




            var country = Countries.FindByID(CountryID);


            if (country == null)
            {
                return NotFound("No Country found");
            }

            country.CountryID   =CountryID;
            country.CountryName = UpdateCountryDTO.CountryName;

            if (country.Save())
            {
                //return the DTO not the Full Object
                return Ok(country.SDTO);
            }
            else
            {
                return StatusCode(500, new { message = $"{CountryID} ,{country.CountryID},{country.CountryName} Error Updating Country" });
            }




        }


        [HttpDelete("Delete/{CountryID}", Name = "DeleteCountry")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]

        public ActionResult DeleteCountry(int CountryID)
        {
            if (CountryID < 1)
            {
                return BadRequest("bad Request");
            }


            if (!Countries.IsCountryExists(CountryID))
            {
                return NotFound($"Country with ID {CountryID} not exist");
            }


            if (Countries.DeleteCountry(CountryID))
            {
                return Ok($"Country with ID {CountryID} has been deleted");
            }
            else
            {
                return StatusCode(500, new { message = $"Error deleting Country with ID {CountryID}" });
            }

            

        }




    }
}
