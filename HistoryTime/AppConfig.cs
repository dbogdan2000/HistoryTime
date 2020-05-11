using Microsoft.Extensions.Configuration;

namespace HistoryTime
{
    public class AppConfig 
    {
        public AppConfig(IConfiguration config)
        {
            config.Bind(this);
        }
        
        public string ConnectionString { get; set; }
    }
}