using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIDataAccessLayer.Application_Types
{
    public class ApplicationTypesDTO
    {

   
        public int  ApplicationTypeID {  get; set; }   

        public string ApplicationTypeTitle { get; set; }
        public decimal ApplicationFees { get; set; }
        public ApplicationTypesDTO()
        {
            
        }

        public ApplicationTypesDTO( int id , string title,decimal fees)
        {
            this.ApplicationTypeID = id;
            this.ApplicationTypeTitle = title;
            this.ApplicationFees = fees;
        }

    }
}
