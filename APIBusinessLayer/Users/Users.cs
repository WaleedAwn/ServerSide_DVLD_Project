using APIDataAccessLayer.Countries;
using APIDataAccessLayer.People;
using APIDataAccessLayer.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;


namespace APIBusinessLayer.Users
{
    public class Users
    {
        public int UserID { get; set; }
        public int PersonID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
  

        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;


        public UsersDTO SDTO
        {
            get
            {
                return new UsersDTO(UserID, PersonID, UserName, Password, IsActive);
            }
        }

      
        public Users(UsersDTO sDTO, enMode cMode = enMode.AddNew)
        {
            this.UserID = sDTO.UserID;
            this.PersonID = sDTO.PersonID;
            this.UserName = sDTO.UserName;
            this.Password=sDTO.Password;
            this.IsActive=sDTO.IsActive;
            this.Mode = cMode;


        }

      

        public static List<PersonUserDTO> GetAllUsers()
        {

            return UserData.GetAllUsers();
        }

        public static Users FindByUSerID(int UserID)
        {
            UsersDTO sDTO = UserData.FindByUserID(UserID);
            if (sDTO != null)
            {
                return new Users(sDTO, enMode.Update);
            }
            return null;
        }
        public static Users FindByUserName(string UserName)
        {
            UsersDTO sDTO = UserData.FindByUserName(UserName);
            if (sDTO != null)
            {
                return new Users(sDTO, enMode.Update);
            }
            return null;
        }


        private bool _AddNewUser()
        {
            UserID = UserData.AddNewUser(SDTO);
            return UserID != -1;
        }
        private bool _UpdateUser()
        {
            return UserData.UpdateUser(SDTO);
        }

        //public static bool UpdateUserPassword(int UserID, string Password)
        //{
        //    return UserData.UpdateUserPassword(UserID,Password);
        //}
       
        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewUser())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    return false;
                case enMode.Update:
                    return _UpdateUser();
            }

            return false;
        }


        public static bool DeleteUser(int UserID)
        {
            return UserData.DeleteUser(UserID);
        }


        public static bool IsUserExists(int UserID)
        { 
            return UserData.IsUserExist(UserID);
        }

        public static bool IsUserNameExists(string UserName)
        {
            return UserData.IsUserUserNameExist(UserName);
        }

        public static bool IsPersonExistAsUser(int PersonID)
        {
            return UserData.IsPersonExistAsUser(PersonID);
        }

    }
}
