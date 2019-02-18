using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UdpFinishing.Interface;

namespace UdpFinishing.ChatClasses
{
    class UdpPortM : IChat
    {
        Messages newMessage = new Messages();
        public event EventHandler Datarecived;
        UdpClient sendingClient;//, receivingClient;
        string message, broadcastAddress;
        private Thread receivingThread;
        IPEndPoint endPoint;
        int port1, port2;
        byte[]  _userStatus, _userName;

        Message bytedata = new Message();
        FileManager fileManager;

        public string username { get; set; }

        public bool IsOpen { get; set; }

        public UdpPortM(int port1, int port2)
        {
            this.port1 = port1;
            this.port2 = port2;
        }

        public void ClosePort()
        {
            //receivingClient.Close();
            sendingClient.Close();
            receivingThread.Abort();
        }

        public string getIpAddres()
        {
            return getIpAddres();
        }

        public void OpenPort()
        {
            Common settings = new Common();
            //string reciverip = Properties.Settings.Default.ReciverIp.ToString();
            IPAddress senderip = IPAddress.Parse(settings.GetLocalIPAddress());
            endPoint = new IPEndPoint(senderip, port2);
            //AddressFamily adrress = AddressFamily.
            broadcastAddress = settings.GetLocalIPAddress();
            sendingClient = new UdpClient(port1);//broadcastAddress,port1
            sendingClient.EnableBroadcast = true;

            //receivingClient = new UdpClient(port2);
            IsOpen = true;
        }
        
        public void SendMessage(string message)
        {
            //UdpClient uClient = new UdpClient(port1);
            //Socket uSocket = sendingClient.Client;

            // use the underlying socket to enable broadcast.
            //uSocket.SetSocketOption(SocketOptionLevel.Socket,
            //              SocketOptionName.Broadcast, 1);
            string username = Properties.Settings.Default.UserName;
            string status = Properties.Settings.Default.Status;
            byte[] datatosend = bytedata.EncodeMessage(username, message, status);
            //byte[] data = test.byteMessage("Redwan", "Hello Selam new", "Online");

            //string sentData = newMessage.Encodemessagesend(username, message,status);
            //byte[] sendData = Encoding.Unicode.GetBytes(sentData);

            sendingClient.Send(datatosend, datatosend.Length, endPoint);//,endPoint
        }

        public void TestBroadcasting()
        {
            Console.WriteLine("Testing broadcasting ");
            UdpClient udp = new UdpClient();

            int GroupPort = 15000;



            IPEndPoint groupEP = new IPEndPoint(IPAddress.Parse("255.255.255.255"), GroupPort);



            string str4 = "Is anyone out there?";

            byte[] sendBytes4 = Encoding.ASCII.GetBytes(str4);



            udp.Send(sendBytes4, sendBytes4.Length, groupEP);

            byte[] receiveBytes = udp.Receive(ref groupEP);

            string returnData = Encoding.ASCII.GetString(receiveBytes, 0, receiveBytes.Length);

            Console.WriteLine("Response: " + returnData);
        }

        public void Readmessageboth()
        {
            ThreadStart start = new ThreadStart(Receiverudp);
            receivingThread = new Thread(start);
            receivingThread.IsBackground = true;
            receivingThread.Start();
        }
        public string ReadMessage()
        {
            //IPEndPoint sender = null;
            try
            {
                IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, port2);
                byte[] data = sendingClient.Receive(ref RemoteIpEndPoint);
                //bytedata.DecodeData(data); Text Message Recived
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            
            
            
            message = bytedata.UserMessage;//DecodeMessage(data);
            return message;
        }

        private void Receiverudp()
        {
            string datarecived;
            do
            {
                datarecived = ReadMessage();
                Datarecived(this, new MessageEventargs() { Message = bytedata.UserMessage, Username = bytedata.UserName , Status = bytedata.UserStatus});
            } while (datarecived != null);
        }

        public void SendFile(string filename,int counter)
        {
            FileManager file = new FileManager(filename);
            byte[] result = file.ReadChunk(counter, 5);
            file.AppendData(counter, result);
            /*string userName = Properties.Settings.Default.UserName;
            string userStatus = Properties.Settings.Default.Status;

            _userName = Encoding.Default.GetBytes(userName);
            _userStatus = Encoding.Default.GetBytes(userStatus);
            fileManager = new FileManager(filename); // Creacte new instance of FileManager
            fileManager.PrepareFile();*/

            //fileMessage = new FileMessage(_userName,userName.Length,_userName,userName.Length,)
            // byte[] datatosend = bytedata.EncodeMessage(username, message, status);
        }
    }
}
