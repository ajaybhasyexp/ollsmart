using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace OllsMart.Services
{
    public class ConfigurationService
    {
        private IConfiguration _configuration = null;
        private IWebHostEnvironment _env = null;
        public ConfigurationService(IConfiguration configuration,
        IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;
        }

        public string GetConnectionString()
        {
            return GetDefaultConnectionString();
        }

        public string GetDefaultConnectionString()
        {
            if (_env.IsDevelopment())
            {
                return _configuration.GetConnectionString("Local");
            }
            else if (_env.IsStaging())
            {
                return _configuration.GetConnectionString("Staging");
            }
            else
            {
                return _configuration.GetConnectionString("Prodcution");
            }
        }
    }
}