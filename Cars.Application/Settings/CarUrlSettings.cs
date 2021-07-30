using Cars.Application.Providers;

namespace Cars.Application.Settings
{
    public class CarUrlSettings : IUrlsProvider
    {
        public string DataMuseUrl { get; set; }
    }
}
