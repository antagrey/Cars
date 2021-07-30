using Cars.Application.Settings;

namespace Cars.Api.Settings
{
    public class ApplicationSettings
    {
        public ConnectionStringSettings ConnectionStringSettings { get; set; }
        public EntityFrameworkSettings EntityFrameworkSettings { get; set; }
        public CarUrlSettings CarUrlSettings { get; set; }
    }
}
