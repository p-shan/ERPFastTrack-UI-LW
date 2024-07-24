using ERPFastTrack.DBGround.Context;
using ERPFastTrack.LicenseProcessor.Internals;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ERPFastTrack.LicenseProcessor.Background
{
    public class LicenseBgProcessor : IHostedService, IDisposable
    {
        private readonly ILogger<LicenseBgProcessor> _logger;
        private readonly IServiceScopeFactory _scopeFactory;
        private Timer? _timer = null;

        public LicenseBgProcessor(ILogger<LicenseBgProcessor> logger, IServiceScopeFactory scopeFactory)
        {
            _logger = logger;
            this._scopeFactory = scopeFactory;
        }

        public Task StartAsync(CancellationToken stoppingToken)
        {
            _logger.LogDebug("License BG Service is running.");

            _timer = new Timer(DoWork, null, TimeSpan.Zero,
                TimeSpan.FromSeconds(30));

            return Task.CompletedTask;
        }

        private void DoWork(object? state)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ERPFastTrackUIContext>();
                var licenseAuth = scope.ServiceProvider.GetRequiredService<ILicenseAuth>();

                var existsRes = licenseAuth.LicenseExist();
                if (existsRes.Exists)
                {
                    var license = existsRes.License;
                    var org = existsRes.Organization;

                    if (licenseAuth.Authenticate(license.LicenseCode, org.Name).Result.IsValid)
                    {
                        context.Entry(license).State = EntityState.Modified;
                        license.IsValid = true;
                        license.LastValidation = DateTime.Now;
                        context.SaveChanges();
                    }
                    else
                    {
                        context.Entry(license).State = EntityState.Modified;
                        license.IsValid = false;
                        license.LastValidation = DateTime.Now;
                        context.SaveChanges();
                    }
                }
                _logger.LogDebug("License BG Service is running. Validated On: " + DateTime.Now);
            }
        }

        public Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogDebug("License BG Service is stopping.");

            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
