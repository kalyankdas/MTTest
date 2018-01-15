using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Common
{
    public class BusSettings
    {
        public string Uri { get; set; }
        public string IncomingQueue { get; set; }
        public string OutgoingQueue { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public ushort HeartBeatInSeconds { get; set; }
        public static BusSettings LoadBusSettings()
        {
            return new BusSettings
            {
                Uri = ConfigurationManager.AppSettings["BusSettings.Uri"],
                IncomingQueue = ConfigurationManager.AppSettings["BusSettings.IncomingQueue"],
                OutgoingQueue = ConfigurationManager.AppSettings["BusSettings.OutgoingQueue"],
                Username = ConfigurationManager.AppSettings["BusSettings.Username"],
                Password = ConfigurationManager.AppSettings["BusSettings.Password"],
                HeartBeatInSeconds = Convert.ToUInt16(ConfigurationManager.AppSettings["BusSettings.HeartBeatInSeconds"])
            };
        }
    }
}
