using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdpFinishing.Interface
{
    public interface IChat
    {
        void SendMessage(string message);

        void SendFile(string file,int counter);

        //string ReadMessage();

        bool IsOpen { get; set; }

        void Readmessageboth();

        void OpenPort();

        void ClosePort();

        event EventHandler Datarecived;

        string getIpAddres();

    }

    public class MessageEventargs : EventArgs
    {
        public Object Message { get; set; }

        public Object Username { get; set; }

        public Object Status { get; set; }
    }
}
