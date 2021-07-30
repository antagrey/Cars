using Cars.Application.Providers;

namespace Cars.Application.Settings
{
    public class ConnectionStringSettings : IConnectionStringsProvider
    {
        public string Cars { get; set; }
    }
}
