using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UdpFinishing.Interface;

namespace UdpFinishing.ChatClasses
{
    class Message
    {
        public string UserName;
        public int UserStatus;
        public string UserMessage;
        public MessageType MessageType { get; set; }
        IChat reciver = new UdpPortM(Properties.Settings.Default.Ports, Properties.Settings.Default.Portr);

        public byte[] EncodeMessage(string username,string message,string ustatus)
        {
            #region Encoding 
            byte[] preable = Encoding.Default.GetBytes("AB");
            byte[] _userName = Encoding.Default.GetBytes(username);
            byte[] _userMessage = Encoding.Default.GetBytes(message);
            byte[] _userStatus = Encoding.Default.GetBytes(ustatus);
            int checksum = 0;

            int userNameLength = username.Length;
            int messageLength = message.Length;
            int status = StatusCheck(ustatus);

            List<byte> userFrame = new List<byte>();
            foreach (byte pr in preable)
            {
                userFrame.Add(pr);
            }

            userFrame.Add((byte)userNameLength);
            userFrame.Add((byte)messageLength);
            userFrame.Add((byte)MessageType.Text);
            userFrame.Add((byte)status);

            foreach (byte name in _userName)
            {
                userFrame.Add(name);
            }

            foreach(byte m in _userMessage)
            {
                userFrame.Add(m);
            }

            //CheckSum evaluation
            foreach (byte c in userFrame)
            {
                checksum += c;
            }
            userFrame.Add((byte)checksum);
            Console.WriteLine(" checksum => " + checksum);
            
            #endregion

            byte[] returndata = userFrame.ToArray();
            return returndata;
        }

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
            return _status;
        }

        public void DecodeData(byte[] returndata)
        {
            int status = returndata[5];

            int _userNameLength = 0, _messageLength = 0,_status;
            _userNameLength = returndata[2];
            _messageLength = returndata[3];
            _status = returndata[5];

            MessageType = (MessageType)returndata[4];

            //Get User name
            byte[] decodedUsername = new byte[_userNameLength];
            Array.Copy(returndata, 6, decodedUsername, 0, _userNameLength);
            UserName = Encoding.ASCII.GetString(decodedUsername);

            //Get User Message
            byte[] decodedMessage = new byte[_messageLength];
            Array.Copy(returndata, 6 + _userNameLength, decodedMessage, 0, _messageLength);
            UserMessage = Encoding.ASCII.GetString(decodedMessage);

            int checksumaddres = 6 + _userNameLength + _messageLength;
            int checksum = 0;// = returndata[checksumaddres];
            //Get Use Status
            UserStatus = _status;

            //Evaluate Checksum
            //byte checksum = returndata[returndata.Length - 1];
            foreach (byte c in returndata)
            {
                checksum += c;
            }
            Console.WriteLine(MessageType);
        }
    }

    public class FileManager
    {
        public string Filename { get; private set; }
        //public byte[] data { get;  set}
        FileStream stream;
        byte[] FileByte, _userStatus, _userName;
        FileInfo info;
        FileMessage fileMessage;
        IChat uchatt;
        string usreName, userStatus;
        int stop = 0,start = 0;
        string selectedPath;

        public FileManager(string filename)
        {
            Filename = filename;
            FileByte =  File.ReadAllBytes(Filename);
            info = new FileInfo(Filename);
            //byte[] preable = Encoding.Default.GetBytes("AB");
            usreName = Properties.Settings.Default.UserName;
            userStatus = Properties.Settings.Default.Status;

            _userName = Encoding.Default.GetBytes(usreName);
            _userStatus = Encoding.Default.GetBytes(userStatus);
            //PrepareFile();
        }

        public void PrepareFile()
        {
            // TODO:
            fileMessage.Decode(new byte[0]);
        }

        public void AppendData(int counter, byte[] chunkData)
        {
            // TODO:

            /*using (var stream = new FileStream(fileName, FileMode.Append))
            {
                stream.Write(chunkData, 0, chunkData.Length);
            }*/
            //stream.Write();
            fileMessage.Decode(chunkData);
            if (counter == 0)
            {
                FolderBrowserDialog filedialog = new FolderBrowserDialog();
                DialogResult result = filedialog.ShowDialog();
                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(filedialog.SelectedPath))
                {
                    FileStream file;
                    selectedPath = filedialog.SelectedPath + @"\downloaded.txt";
                    //Console.WriteLine("Selecte Path: " + selectedPath);
                    file = new FileStream(selectedPath, FileMode.Create);
                    file.Close();
                    file = new FileStream(selectedPath, FileMode.Append);
                    file.Write(fileMessage.FileChunk, 0, fileMessage.FileChunk.Length);
                    file.Close();
                }

            }
            else{
                FileStream file;
                file = new FileStream(selectedPath, FileMode.Append);
                file.Write(fileMessage.FileChunk, 0, fileMessage.FileChunk.Length);
                file.Close();
            }

            if (stop == 1)
            {
                Console.WriteLine("************** File transfer is completed ***************************");
            }

            Console.WriteLine("Encode file " +Encoding.ASCII.GetString(fileMessage.FileChunk));
        }

        //public byte[] ReadChunk(int counter, int totalBytes)
        public byte[] ReadChunk(int counter, int totalBytes)
        {
            // TODO:
            //stream.Read();
            // Read 5 byte from the data and send it
            long size;
            if(counter == 0)
            {
                size = info.Length > 5 ? totalBytes:info.Length;
            }
            else
            {
                size = info.Length >= totalBytes * (counter + 1) ? totalBytes : info.Length % (counter);
            }
            if( stop == 0)
            {
                byte[] chunkfile = new byte[size];
                //Console.WriteLine(size);
                Array.Copy(FileByte, counter * totalBytes, chunkfile, 0, size);
                fileMessage = new FileMessage(_userName, usreName.Length, _userName, usreName.Length, FrameType.Data, (byte)counter, chunkfile);
                byte[] result = fileMessage.Encode();
                //fileMessage.Decode(result);
                if (!(info.Length >= totalBytes * (counter + 1)))
                {
                    stop = 1;
                }
                return result;
            }
            else
            {
                fileMessage = new FileMessage(_userName, usreName.Length, _userName, usreName.Length, FrameType.Stop, (byte)counter, new byte[0]);
                byte[] result = fileMessage.Encode();
                //fileMessage.Decode(result);
                if (!(info.Length >= totalBytes * (counter + 1)))
                {
                    stop = 1;
                }
                return result;
            }

            
            //Console.WriteLine("Chunk File " + counter);
            //Console.WriteLine(Encoding.ASCII.GetString(chunkfile));
            //Console.WriteLine();
            //return chunkfile;
        }

        public void SaveAndClose()
        {
            // TODO:
            stream.Close();
        }


    }

    public class FileMessage
    {
        //public byte[] Preamble { get; set; }
        public byte[] Sender { get; set; }
        public byte[] Reciever { get; set; }
        public byte Counter { get; set; }
        public FrameType Type { get; set; } // Start=0, Ack=1, Data=2, Stop=3
        public byte[] FileChunk { get; set; }

        public MessageType MessageType { get; set; }

        public int senderLength { get; set; }
        // TODO: Filename
        // TODO: Filesize

        /*public FileMessage()
        {
        }*/

        public FileMessage()
        {

        }

        public FileMessage(byte[] sender,int senderlength, byte[] reciever,int reciverlength, FrameType type, byte counter, byte[] fileChunk)
        {
            Sender = sender;
            Reciever = reciever;
            FileChunk = fileChunk;
            Type = type;
            Counter = counter;
            senderLength = senderlength;
            //Preamble = Encoding.Default.GetBytes("A");
        }

        public byte[] Encode()
        {
            List<byte> frame = new List<byte>();
            //frame.AddRange(Preamble);
            frame.Add((byte)Type);
            frame.Add(Counter);
            frame.Add((byte)FileChunk.Length);
            frame.Add((byte)senderLength);
            frame.Add((byte)senderLength);
            frame.Add((byte)MessageType.File);
            frame.AddRange(Sender);
            frame.AddRange(Reciever);
            frame.AddRange(FileChunk);
            // A 1 2 0 0 5 1 2 3 4 5
            Console.WriteLine("Encoded file ");
            foreach (byte b in FileChunk)
            {
                Console.Write(b);
            }
            Console.WriteLine();
            return frame.ToArray();
        }

        public void Decode(byte[] frame)
        {
            //Preamble = frame[0];
            Type = (FrameType)frame[0];
            MessageType = (MessageType)frame[5];


            if (Type == FrameType.Stop)
            {
                Console.WriteLine("File Transfer Completed");
            }
            if(Type == FrameType.Data)
            {
                Counter = frame[1];
                
                Console.WriteLine("Counter " + Counter);
                byte filelength = frame[2];
                int Senderlength = frame[3];
                int Reciverlength = frame[4];
                Sender = new byte[Senderlength];
                Reciever = new byte[Reciverlength];
                Array.Copy(frame, 6, Sender, 0, Senderlength);
                Array.Copy(frame, 6 + Senderlength, Reciever, 0, Reciverlength);

                FileChunk = new byte[filelength];

                Array.Copy(frame, 6 + Senderlength + Reciverlength, FileChunk, 0, filelength);
                
            }
            
        }
    }

    public enum FrameType
    {
        Start = 0,
        Ack = 1,
        Data = 2,
        Stop = 3
    }

    public enum MessageType
    {
        File = 1,
        Text = 2
    }
}
