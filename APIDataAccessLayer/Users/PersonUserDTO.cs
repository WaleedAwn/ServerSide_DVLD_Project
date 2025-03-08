using APIDataAccessLayer.People;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIDataAccessLayer.Users
{
    public class PersonUserDTO
    {
        public int UserID { get; set; }
        public int PersonID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
        public PersonsDTO Person { get; set; }


        public PersonUserDTO()
        {
            Person = new PersonsDTO();

        }
        
        public PersonUserDTO(int userId,int PersonID, string userName, string password, bool isActive, PersonsDTO personsDTO)
        {
            this.UserID = userId;
            this.PersonID = PersonID;
            this.UserName = userName;
            this.Password = password;
            this.IsActive = isActive;
            this.Person = personsDTO;

        }


    }




}
