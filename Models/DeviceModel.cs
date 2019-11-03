using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WpfMessenger.Models
{
    public class DeviceModel
    {
        IPAddress ipAddress;
        public DateTime LastSeen { get; set; }
        public UserModel User { get; }
        public DeviceModel(IPAddress iP, UserModel user) 
        {
            ipAddress = iP;
            User = user;
            user.AddDevice(this);
        }
    }
}