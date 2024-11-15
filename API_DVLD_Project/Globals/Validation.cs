namespace API_DVLD_Project.Globals
{
    public class clsValidation
    {
        public static bool CheckGender(short genderNumber)
        {
            return (genderNumber == 0 || genderNumber == 1);
        }

        
        //public static bool CheckCountryExist (int genderNumber) {
        //    return true;
        //}
}
}
