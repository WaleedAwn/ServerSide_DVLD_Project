namespace APIDataAccessLayer.Applications.DTOs
{
    public class ApplicationDTO
    {


        public int ApplicationID { get; set; }
        public DateTime ApplicationDate { get; set; }
        public short ApplicationStatus { get; set; }
        public DateTime LastStatusDate { get; set; }
        public decimal PaidFees { get; set; }
        public int CreatedByUserID { get; set; }
        public int ApplicantPersonID { get; set; }
        public int ApplicationTypeID { get; set; }




        public ApplicationDTO()
        {

        }
        public ApplicationDTO(int applicationID, DateTime applicationDate, short applicationStatus,
            DateTime lastStatusDate, decimal paidFees, int createdByUserId,
            int applicantPersonId,
            int applicationTypeId)
        {
            ApplicationID = applicationID;
            ApplicationDate = applicationDate;
            ApplicationStatus = applicationStatus;
            LastStatusDate = lastStatusDate;
            PaidFees = paidFees;
            CreatedByUserID = createdByUserId;
            ApplicantPersonID = applicantPersonId;
           
            ApplicationTypeID = applicationTypeId;
       
            
        }




    }



}
