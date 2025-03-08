using APIBusinessLayer.People;
using APIBusinessLayer.Users;
using APIDataAccessLayer.People;
using APIDataAccessLayer.Users;
using Microsoft.AspNetCore.Mvc;

namespace API_DVLD_Project.Controllers
{
    [Route("api/Users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        [HttpGet("All", Name = "GetAllUsers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]      
        public ActionResult<IEnumerable<UsersDTO>> GetAllUsers()
        {
            var UsersList = Users.GetAllUsers();
            if (UsersList.Count == 0)
            {
                return NotFound("No Users Found");
            }
            return Ok(UsersList);

        }


        [HttpGet("FindByID/{UserID}", Name = "GetUserByID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<PersonsDTO> GetUserByID(int UserID)
        {
            if (UserID < 0)
            {
                return BadRequest("bad Request");
            }

            var User = Users.FindByUSerID(UserID);

            if (User == null)
            {
                return NotFound("No User found");
            }

            UsersDTO sDTO = User.SDTO;


            return Ok(sDTO);
        }


        [HttpGet("FindByName/{UserName}", Name = "GetUserByUserName")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<PersonsDTO> FindUserByUserName(string UserName)
        {
            if (string.IsNullOrWhiteSpace(UserName))
            {
                return BadRequest("bad Request");
            }

            var User = Users.FindByUserName(UserName);

            if (User == null)
            {
                return NotFound("No User found");
            }

            UsersDTO sDTO = User.SDTO;


            return Ok(sDTO);
        }

        [HttpGet("IsExist/{UserID}", Name = "IsUserExist")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<bool> IsUserExist(int UserID)
        {
            if (UserID < 0)
            {
                return BadRequest("bad Request");
            }

            bool USer = Users.IsUserExists(UserID);

            if (USer == false)
            {
                return NotFound(false);
               

            }


            return Ok(USer);
        }



        [HttpPost("Add", Name = "AddUser")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public ActionResult<UsersDTO> AddNewUser(UsersDTO newUserDTO)
        {
            if (newUserDTO == null || 
                string.IsNullOrEmpty(newUserDTO.UserName) ||
                string.IsNullOrEmpty(newUserDTO.Password)
                )
            {
                return BadRequest("Invalid User data");
            }
            if (!People.IsPersonExists(newUserDTO.PersonID))
                return BadRequest("Invalid PersonID NotExist");


            Users user = new APIBusinessLayer.Users.Users(
                new UsersDTO(newUserDTO.UserID, newUserDTO.PersonID,
                newUserDTO.UserName, newUserDTO.Password, newUserDTO.IsActive)

                );

            user.Save();

            newUserDTO.UserID = user.UserID;

            return CreatedAtRoute("GetUserByID", new { UserID = newUserDTO.UserID }, newUserDTO);


        }



        [HttpPut("Update/{UserID}", Name = "UpdateUser")]

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
     
        public ActionResult<UsersDTO> UpdateUser(int UserID, UsersDTO UpdateUserDTO)
        {
            if (UpdateUserDTO == null || UserID < 0||
                string.IsNullOrEmpty(UpdateUserDTO.UserName) ||
                string.IsNullOrEmpty(UpdateUserDTO.Password)
                )
            {
                return BadRequest("Invalid User data");
            }
            if (!People.IsPersonExists(UpdateUserDTO.PersonID))
                return BadRequest("Invalid PersonID NotExist");




            var User = Users.FindByUSerID(UserID);


            if (User == null)
            {
                return NotFound("No Users found");
            }

            User.UserID = UserID;
            User.PersonID = UpdateUserDTO.PersonID;
            User.UserName = UpdateUserDTO.UserName;
            User.Password = UpdateUserDTO.Password;
            User.IsActive = UpdateUserDTO.IsActive;
          



            if (User.Save())
            {
                //return the DTO not the Full Object
                return Ok(User.SDTO);
            }
            else
            {
                return StatusCode(500, new { message = " Error Updating User" });
            }




        }





        [HttpDelete("Delete/{id}", Name = "DeleteUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]

        public ActionResult DeleteUser(int id)
        {
            if (id < 1)
            {
                return BadRequest("bad Request");
            }


            if (!Users.IsUserExists(id))
            {
                return NotFound($"User with ID {id} not exist");
            }

            //if (People.CheckPersonRelations(id))
            //    return Conflict("Can't delete this Person because it has relations in other tables");


            if (Users.DeleteUser(id))
            {
                return Ok($"User with ID {id} has been deleted");
            }
            else
            {
                return StatusCode(500, new { message = $"Error deleting User with ID {id}" });
            }


        }


        [HttpGet("IsUserNameExist/{UserName}", Name = "IsUserNameExist")] 
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<bool> IsUserUserNameExist(string UserName)
        {
            if (string.IsNullOrEmpty(UserName))
            {
                return BadRequest("bad Request");
            }

            bool USer = Users.IsUserNameExists(UserName);

            if (USer == false)
            {
                return NotFound(false);

            }


            return Ok(USer);
        }


        [HttpGet("IsPersonExistAsUser/{PersonID}", Name = "IsPersonExistAsUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<bool> IsPersonAsExist(int PersonID)
        {
            if (PersonID<0)
            {
                return BadRequest("bad Request");
            }

            bool USer = Users.IsPersonExistAsUser(PersonID);

            if (USer == false)
            {
                return NotFound(false);
            }


            return Ok(USer);
        }


    }


}
