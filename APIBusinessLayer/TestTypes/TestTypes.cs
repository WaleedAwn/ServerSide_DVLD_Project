using APIBusinessLayer.ApplicationTypes;
using APIDataAccessLayer.Application_Types;
using APIDataAccessLayer.TestTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIBusinessLayer.TestTypes
{
    public class TestTypes
    {
        public int TestTypeID { get; set; }
        public string TestTypeTitle { get; set; }
        public string TestTypeDescription { get; set; }
        public decimal TestTypeFees { get; set; }

        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;
        public TestTypesDTO TeDTO
        {
            get { return new TestTypesDTO(TestTypeID, TestTypeTitle, TestTypeDescription, TestTypeFees); }
        }

        public TestTypes(TestTypesDTO TDTO, enMode cMode = enMode.AddNew)
        {
            this.TestTypeID = TDTO.TestTypeID;
            this.TestTypeTitle = TDTO.TestTypeTitle;
            this.TestTypeDescription = TDTO.TestTypeDescription;
            this.TestTypeFees = TDTO.TestTypeFees;
            
            this.Mode = cMode;
        }


        public static List<TestTypesDTO> GetTestTypes()
        {
            return TestTypesData.GetAllTestTypes();
        }



        public static bool IsTestTypeExists(int testTypeID)
        {
            return TestTypesData.IsTestTypeExist(testTypeID);
        }

        public static TestTypes FindTestTypeByID(int testTypeID)
        {
            var tDTO = TestTypesData.FindByTestTypeID(testTypeID);
            if (tDTO != null)
            {
                return new TestTypes(tDTO, enMode.Update);
            }
            return null;
        }

        private bool _Update()
        {
            return TestTypesData.UpdateTestType(TeDTO);
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
