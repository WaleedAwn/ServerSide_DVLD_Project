using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIDataAccessLayer.Countries
{
    public class CountriesDTO
    {
        public int CountryID { get; set; }
        public string CountryName { get; set; }
        public CountriesDTO()
        {
            
        }

        public CountriesDTO(int _CountryID, string _CountryName)
        {
            this.CountryID = _CountryID;
            this.CountryName = _CountryName;          
        }



    }


}
