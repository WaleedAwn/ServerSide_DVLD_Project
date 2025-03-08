using APIDataAccessLayer.People;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Diagnostics.Metrics;
using System.Net;
using System.Reflection;
using System.Xml.Linq;

namespace APIBusinessLayer.People
{
    public class People
    {
        public int PersonID { get; set; }
        public string NationalNo { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string ThirdName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public short Gendor { get; set; }
        public string Address { get; set; }

        public string Phone { get; set; }
        public string Email { get; set; }
        public int NationalityCountryID { get; set; }
        public string ImagePath { get; set; }
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;
        public PersonsDTO SDTO
        {
            get { return new PersonsDTO(PersonID, NationalNo, FirstName, SecondName, ThirdName, LastName,
                DateOfBirth,Gendor,Address,Phone,Email,NationalityCountryID,ImagePath); }
        }

        public People(PersonsDTO sDTO, enMode cMode = enMode.AddNew)
        {
            this.PersonID =sDTO.PersonID;
            this.NationalNo = sDTO.NationalNo;
            this.FirstName = sDTO.FirstName;
            this.SecondName = sDTO.SecondName;
            this.ThirdName = sDTO.ThirdName;
            this.LastName = sDTO.LastName;
            this.DateOfBirth = sDTO.DateOfBirth;
            this.Gendor = sDTO.Gendor;
            this.Address = sDTO.Address;
            this.Phone = sDTO.Phone;
            this.Email = sDTO.Email;
            this.NationalityCountryID = sDTO.NationalityCountryID;
            this.ImagePath = sDTO.ImagePath;
            this.Mode = cMode;

            
        }
       
        public static List<PersonsDTO> GetAllPersons()
        {
            return PersonsData.GetAllPersons();
        }

        public static People FindByNationalNo(string   NationalNo)
        {
            PersonsDTO sDTO = PersonsData.FindByNationalNo(NationalNo);
            if (sDTO != null)
            {
                return new People(sDTO, enMode.Update);
            }
            return null;
        }

        public static People FindByPersonID(int PersonID)
        {
            PersonsDTO sDTO = PersonsData.FindByPersonID(PersonID);
            if (sDTO != null)
            {
                return new People(sDTO, enMode.Update);
            }
            return null;
        }

        private bool _AddNewPerson()
        {
            PersonID = PersonsData.AddNewPerson(SDTO);
            return PersonID != -1;
        }
        private bool _UpdatePerson()
        {
            return PersonsData.UpdatePerson(SDTO); 
        }
        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewPerson())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    return false;
                case enMode.Update:
                    return _UpdatePerson();
            }
            
            return false;
        }


        public static bool DeletePerson(int  PersonID)
        {
            return PersonsData.DeletePerson(PersonID);
        }


        public static bool IsPersonExists(int PersonID)
        {
            return PersonsData.IsPersonExist(PersonID);
        }







    }
}
