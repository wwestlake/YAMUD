using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LagDaemon.YAMUD.Model.Config
{
    public class HttpClientConfiguration
    {
        public Guid UserId { get; set; }
        public string HostName { get; set; }
        public int Port { get; set; }
        public string JwtToken { get; set; }
    }
}
