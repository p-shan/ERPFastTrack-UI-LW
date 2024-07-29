using ERPFastTrack.DBGround.DBModels.Custom;

namespace ERPFastTrack.LicenseProcessor.Internals
{
    public interface ILicenseAuth
    {
        string GetDownloadableRequest();
        string GetInstanceId();
        LicenseExistsResponse LicenseExist();
        Task<ValidateResponse> Authenticate(string licenseCode, string orgName);
    }
}
