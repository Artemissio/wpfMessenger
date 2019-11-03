using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfMessenger.Models;

namespace WpfMessenger.Repositories
{
    public class DevicesRepository
    {
        static DevicesRepository _repository;

        DeviceModel _device;
        List<DeviceModel> _devices;

        DevicesRepository()
        {
            _devices = new List<DeviceModel>();
        }

        public static DevicesRepository GetInstance()
        {
            if (_repository == null)
                _repository = new DevicesRepository();
            return _repository;
        }

        public DeviceModel GetDevice(UserModel user)
        {
            for (int i = 0; i < _devices.Count; i++)
            {
                if (_devices[i].User == user)
                    _device = _devices[i];
            }
            return _device;
        }

        public IEnumerable<DeviceModel> GetDevices()
        {
            return _devices;
        }
    }
}