using APIDataAccessLayer.Countries;
using APIDataAccessLayer.People;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace APIBusinessLayer.Countries
{
    public class Countries
    {
        public int CountryID { get; set; }
        public string CountryName { get; set; }
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;
        public CountriesDTO SDTO 
        {
            get{ 
                return new CountriesDTO(CountryID, CountryName);
            
            }
        }

        public Countries(CountriesDTO sDTO, enMode cMode = enMode.AddNew)
        {
            this.CountryID= sDTO.CountryID;
            this.CountryName= sDTO.CountryName;
            this.Mode = cMode;
            
        }

        public static List<CountriesDTO> GetAllCountries() { 
        
            return CountriesData.GetAllCountries();
        }

        public static Countries FindByID(int CountryID)
        {
            CountriesDTO sDTO = CountriesData.FindByID(CountryID);
            if (sDTO != null)
            {
                return new Countries(sDTO, enMode.Update);
            }
            return null;
        }

        public static Countries FindCountryByName(string CountryName)
        {
            CountriesDTO sDTO = CountriesData.FindCountryByName(CountryName);
            if (sDTO != null)
            {
                return new Countries(sDTO, enMode.Update);
            }
            return null;
        }

        private bool _AddNewCountry()
        {
            CountryID = CountriesData.AddNewCountry(SDTO);
            return CountryID != -1;
        }
        private bool _UpdateCountry()
        {
            return CountriesData.UpdateCountry(SDTO); // Fixed: UpdatePerson instead of UpdateStudent
        }
        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewCountry())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    return false;
                   
                case enMode.Update:
                    return _UpdateCountry();
            }
            return false;
        }


        public static bool DeleteCountry(int CountryId)
        {
            return CountriesData.DeleteCountry(CountryId);
        }


        public static bool IsCountryExists(int CountryId)
        {
            return CountriesData.IsCountryExist(CountryId);
        }




    }
}
