using APIDataAccessLayer.Application_Types;
using APIDataAccessLayer.Applications;
using APIDataAccessLayer.Applications.DTOs;
using APIDataAccessLayer.Countries;

namespace APIBusinessLayer.Applications;

public class Applications
{
    public int ApplicationID { get; set; }
    public DateTime ApplicationDate { get; set; }
    public short ApplicationStatus { get; set; }
    public DateTime LastStatusDate { get; set; }
    public decimal PaidFees { get; set; }
    public int CreatedByUserID { get; set; }
    public int ApplicantPersonID { get; set; }
    public int ApplicationTypeID { get; set; }

    public enum enMode { AddNew = 0, Update = 1 };
    public enMode Mode = enMode.AddNew;
    public ApplicationDTO ADTO
    {
        get { return new ApplicationDTO(ApplicationID, ApplicationDate,ApplicationStatus, LastStatusDate, 
            PaidFees,CreatedByUserID,ApplicantPersonID,ApplicationTypeID); }
    }

    public Applications(ApplicationDTO ADTO, enMode cMode = enMode.AddNew)
    {
        this.ApplicationID = ADTO.ApplicationID;
        this.ApplicationDate = ADTO.ApplicationDate;
        this.ApplicationStatus = ADTO.ApplicationStatus;
        this.LastStatusDate = ADTO.LastStatusDate;
        this.PaidFees = ADTO.PaidFees;
        this.CreatedByUserID = ADTO.CreatedByUserID;
        this.ApplicantPersonID = ADTO.ApplicantPersonID;
        this.ApplicationTypeID = ADTO.ApplicationTypeID;
        this.Mode = cMode;
    }

    public static bool IsApplicationExists(int AppTyID)
    {
        return ApplicationsData.IsApplicationExist(AppTyID);
    }

    public static Applications FindByApplicationID(int AppID)
    {
        var aDTO = ApplicationsData.FindByApplicationID(AppID);
        if (aDTO != null)
        {
            return new Applications(aDTO, enMode.Update);
        }
        return null;
    }
    private bool _AddNewApplication()
    {
        ApplicationID = ApplicationsData.AddNewApplication(ADTO);
        return ApplicationID != -1;
    }

    private bool _Update()
    {
        return ApplicationsData.UpdateApplication(ADTO);
    }
    public bool Save()
    {

        switch (Mode)
        {
            case enMode.AddNew:
                if (_AddNewApplication())
                {
                    Mode = enMode.Update;
                    return true;
                }
                return false;
                
            case enMode.Update:
                return _Update();
        }

        return false;
    }

    public static bool DeleteApplication(int appId)
    {
        return ApplicationsData.DeleteApplication(appId);
    }



}
