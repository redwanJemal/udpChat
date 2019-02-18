using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdpFinishing.ChatClasses
{
    class Status
    {
        public int StatusCheck(string status)
        {
            int _status = 0;
            switch (status)
            {
                case "Online":
                    _status = 1;
                    break;
                case "Offline":
                    _status = 2;
                    break;
                case "Busy":
                    _status = 3;
                    break;
            }
            Console.WriteLine("User status is " + status + " " + _status);
            return _status;
        }

    }
}
