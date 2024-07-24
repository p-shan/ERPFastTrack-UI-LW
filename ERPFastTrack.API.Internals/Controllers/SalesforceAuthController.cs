using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ERPFastTrack.DBGround.Context;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.IdentityModel.Tokens.Jwt;
using ERPFastTrack.Common.Operations;

namespace ERPFastTrack.API.Internals.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesforceAuthController : ControllerBase
    {
        private readonly ERPFastTrackUIContext _context;

        public SalesforceAuthController(ERPFastTrackUIContext context)
        {
            _context = context;
        }

        [HttpPost("AuthorizeSalesforce")]
        [Authorize]
        public async Task<ActionResult<bool>> AuthorizeSalesforce([FromForm] int id)
        {
            if (Utils.IsLocal())
            {
                return Redirect($"https://login.salesforce.com/services/oauth2/authorize?client_id={Utils.LOCAL_CLIENTID}&redirect_uri=https://localhost:7091/api/salesforceauth/validateauth&response_type=code&state=" + id.ToString());

            }
            else
            {
                return Redirect($"https://login.salesforce.com/services/oauth2/authorize?client_id={Utils.SVR_CLIENTID}&redirect_uri=https://erpfasttrackui-v2.azurewebsites.net/api/salesforceauth/validateauth&response_type=code&state=" + id.ToString());

            }
        }

        [HttpGet("ValidateAuth")]
        [Authorize]
        public async Task<ActionResult<bool>> ValidateAuth(string code, string state)
        {
            var httpClient = new HttpClient();

            var parameters = new Dictionary<string, string>
            {
                { "grant_type", "authorization_code" },
                { "code", code },
                { "scopes", "refresh_token"},
                { "client_id", Utils.IsLocal() ? Utils.LOCAL_CLIENTID : Utils.SVR_CLIENTID },
                { "client_secret", Utils.IsLocal() ? Utils.LOCAL_CLIENTSECRET : Utils.SVR_CLIENTSECRET },
                { "redirect_uri", Utils.IsLocal() ? "https://localhost:7091/api/salesforceauth/validateauth" : "https://erpfasttrackui-v2.azurewebsites.net/api/salesforceauth/validateauth" }
            };

            var content = new FormUrlEncodedContent(parameters);

            var response = await httpClient.PostAsync("https://login.salesforce.com/services/oauth2/token", content);

            if (response.IsSuccessStatusCode)
            {
                using var responseStream = await response.Content.ReadAsStreamAsync();
                using var jsonDocument = await JsonDocument.ParseAsync(responseStream);

                jsonDocument.RootElement.TryGetProperty("access_token", out var accessToken);
                jsonDocument.RootElement.TryGetProperty("id_token", out var idToken);
                jsonDocument.RootElement.TryGetProperty("refresh_token", out var refreshToken);
                jsonDocument.RootElement.TryGetProperty("instance_url", out var instance_url);

                JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
                DateTime tokenExpiry = handler.ReadJwtToken(idToken.GetString()).ValidTo;
                var salesforceConnection = await _context.SalesforceConnections.FindAsync(Convert.ToInt32(state));

                salesforceConnection.Token = accessToken.GetString();
                salesforceConnection.TokenExpiry = tokenExpiry;
                salesforceConnection.Url = instance_url.GetString();
                salesforceConnection.RefreshToken = refreshToken.GetString();

                _context.Entry(salesforceConnection).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            return Redirect("/");
        }
    }
}
