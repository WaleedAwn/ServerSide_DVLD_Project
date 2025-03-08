using APIDataAccessLayer.People;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIDataAccessLayer.Users
{
    public class UsersDTO
    {
        public int UserID { get; set; }

        public int  PersonID { get; set; }
       public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }

        public UsersDTO()
        {

        }
        public UsersDTO(int userId, int PersonId, string userName, string password, bool isActive)
        {
            this.UserID = userId;
            this.PersonID = PersonId;
            this.UserName = userName;
            this.Password = password;
            this.IsActive = isActive;
        }



    }


}
