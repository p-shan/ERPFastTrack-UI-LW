using ERPFastTrack.DBGround.Context;
using ERPFastTrack.Abstraction.AbstractClass;
using Microsoft.EntityFrameworkCore;
using ERPFastTrack.DBGround.DBModels.Custom;
using ERPFastTrack.Common.Operations;
using ERPFastTrack.APIModels.OperationsModels.Response;
using ERPFastTrack.APIModels.OperationsModels.Request;
using Microsoft.Data.SqlClient;

namespace ERPFastTrack.API.Internals.Controllers.InternalBase.Operations
{
    
    
    public class UtilityBase
    {
        private readonly OrgRoleManagerAbstract _roleManager;

        public UtilityBase(OrgRoleManagerAbstract roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<TestConnectionResponse> TestConnection(TestConnectionRequest request)
        {
            TestConnectionResponse response = new();
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(request.ConnectionString);
            builder["TrustServerCertificate"] = true;
            try
            {
                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    try
                    {
                        connection.Open();
                        if (connection.State == System.Data.ConnectionState.Open)
                        {
                            connection.Close();
                        }
                    }
                    catch
                    {
                        if (connection.State == System.Data.ConnectionState.Open)
                        {
                            connection.Close();
                        }
                        throw;
                    }

                    response.Status = true;
                }
            }
            catch (Exception ex)
            {
                response.ErrorMessage = ex.Message;
                response.Status = false;
            }
            return response;
        }
    }
}
