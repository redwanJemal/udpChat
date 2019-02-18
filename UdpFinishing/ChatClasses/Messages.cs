using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Old Protocol
namespace UdpFinishing.ChatClasses
{
    class Messages
    {
        public string userName { get; set; }

        public string userMessage { get; set; }

        public string Status { get; set; }

        public string Encodemessagesend(string username, string message, string status)
        {
            string messageSend = username + ";" + message + ";" + status;

            return messageSend;
        }
        public void DecodeMessageString(string sentMessage)
        {
            string[] recivedmessages = sentMessage.Split(';');
            userName = recivedmessages[0];
            userMessage = recivedmessages[1];
            Status = recivedmessages[2];
        }
    }
}
#endregion

#region Testing
/*namespace UdpFinishing.ChatClasses
{
    class Messages
    {
        public string userName { get; set; }

        public string userMessage { get; set; }

        public string Status { get; set; }

        public string Encodemessagesend(string username, string message, string status)
        {
            byte[] preable = Encoding.ASCII.GetBytes("AA");
            byte[] _userName = Encoding.ASCII.GetBytes(username);
            byte[] _userMessage = Encoding.ASCII.GetBytes(message);
            byte[] _userStatus = Encoding.ASCII.GetBytes(status);

            List<byte> userFrame = new List<byte>();

            foreach(byte pr in preable)
            {
                userFrame.Add(pr);
            }
            Console.WriteLine(userFrame);
            string messageSend = username + ";" + message + ';' + status;

            return messageSend;
        }
        public void DecodeMessageString(string sentMessage)
        {
            string[] recivedmessages = sentMessage.Split(';');
            userName = recivedmessages[0];
            userMessage = recivedmessages[1];
            Status = recivedmessages[2];
        }
    }

    class StatusMessages
    {
        public string userName { get; set; }
        public string Status { get; set; }

        public byte[] Encodemessagesend()
        {
            string messageSend = userName + ";" + Status;

            return messageSend;
        }
        public void DecodeMessageString(byte[] sentMessage)
        {
            string[] recivedmessages = sentMessage.Split(';');
            userName = recivedmessages[0];
            Status = recivedmessages[2];
        }
    }
}*/
#endregion