using APIDataAccessLayer.Application_Types;
using APIDataAccessLayer.People;
using APIDataAccessLayer.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIBusinessLayer.ApplicationTypes
{
    public class ApplicationTypes
    {
        public int  ApplicationTypeID     { get; set; }
        public string  ApplicationTypeTitle { get; set; }
        public decimal ApplicationFees       { get; set; }
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;
        public ApplicationTypesDTO AtDTO
        {
            get { return  new ApplicationTypesDTO(ApplicationTypeID, ApplicationTypeTitle, ApplicationFees); }
        }

        public ApplicationTypes(ApplicationTypesDTO AtDTO,enMode cMode=enMode.AddNew)
        {
            this.ApplicationTypeID = AtDTO.ApplicationTypeID;
            this.ApplicationTypeTitle = AtDTO.ApplicationTypeTitle;
            this.ApplicationFees = AtDTO.ApplicationFees;
            this.Mode = cMode;               
        }

        public static List <ApplicationTypesDTO> GetApplicationTypes()
        {
            return ApplicationTypesData.GetAllApplicationTypes();
        }



        public static bool IsApplicationTypeExists(int AppTyID)
        {
            return ApplicationTypesData.IsApplicationTypeExist(AppTyID);
        }

        public static ApplicationTypes FindByApplicationTypeID(int AppTyID)
        {
            var sDTO = ApplicationTypesData.FindByApplicationTypeID(AppTyID);
            if (sDTO != null)
            {
                return new ApplicationTypes(sDTO, enMode.Update);
            }
            return null;
        }

        private bool _Update()
        {
            return ApplicationTypesData.UpdateApplicationType(AtDTO);
        }
        public bool Save()
        {   
            
            switch (Mode)
            {
                case enMode.AddNew:         
                    return false;
                case enMode.Update:
                    return _Update();
            }

            return false;
        }



    }
}
