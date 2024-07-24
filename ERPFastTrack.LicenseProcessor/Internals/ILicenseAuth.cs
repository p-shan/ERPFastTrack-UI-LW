using ERPFastTrack.DBGround.DBModels.Custom;

namespace ERPFastTrack.LicenseProcessor.Internals
{
    public interface ILicenseAuth
    {
        string GetInstanceId();
        LicenseExistsResponse LicenseExist();
        Task<ValidateResponse> Authenticate(string licenseCode, string orgName);
    }
}
