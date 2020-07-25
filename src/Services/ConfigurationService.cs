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
        IWebHostEnvironment env) {
            _configuration = configuration;
            _env = env;
        }

        public string GetConnectionString() {
            var dbhost = _configuration.GetValue<string>("CWL_DB_HOST", "127.0.0.1");
            var dbname = _configuration.GetValue<string>("CWL_DB_NAME", "ollsmartdb");
            var dbuser = _configuration.GetValue<string>("CWL_DB_USER", "root");
            var dbpassword = _configuration.GetValue<string>("CWL_DB_PASSWORD", "root@123");
            var dbport = _configuration.GetValue<int>("CWL_DB_PORT",3333);

            if (string.IsNullOrEmpty(dbuser)) {
                return GetDefaultConnectionString();
            }

            if (string.IsNullOrEmpty(dbpassword)) {
                return GetDefaultConnectionString();
            }

            return $"server={dbhost};database={dbname};user={dbuser};password={dbpassword}";
        }

        public string GetDefaultConnectionString() {
            if (_env.IsDevelopment()) {
                return _configuration.GetConnectionString("Local");
            } else if (_env.IsStaging()) {
                return _configuration.GetConnectionString("Staging");
            } else {
                return _configuration.GetConnectionString("Prodcution");
            }
        }
    }
}