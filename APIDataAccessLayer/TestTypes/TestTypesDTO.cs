using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIDataAccessLayer.TestTypes
{
    public class TestTypesDTO
    {

        public int TestTypeID { get; set; }
        public string TestTypeTitle { get; set; }
        public string TestTypeDescription { get; set; }
        public decimal TestTypeFees { get; set; }


        public TestTypesDTO()
        {
            
        }

        public TestTypesDTO(int id,  string titel, string description, decimal fees)
        {
            this.TestTypeID = id;
            this.TestTypeTitle = titel;
            this.TestTypeDescription = description;
            this.TestTypeFees = fees;
        }


    }



}
