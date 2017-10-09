using System;
using System.Diagnostics;
using System.Net.Sockets;
using System.Text;

namespace PCN_Integration.Services.Services
{
    internal class MirthService
    {
        private readonly string _ip = Properties.Settings.Default.mirthIPAddress;
        private readonly int _port = Properties.Settings.Default.mirthChannel;

        internal bool SendFassMessageToMirth(string serializedMessage)
        {
            var dataToSend = Encoding.ASCII.GetBytes(serializedMessage);
            var socket = new TcpClient(_ip, _port);

            try
            {
                var stream = socket.GetStream();
                var headerBytes = BuildFassMessageHeader(dataToSend);

                stream.Write(headerBytes, 0, headerBytes.Length);
                stream.Write(dataToSend, 0, dataToSend.Length);
                socket.Close();
                
                return true;
            }
            catch (Exception ex)
            {
                EventLog.WriteEntry(ex.Source, ex.Message);
                return false;
            }
        }

        private byte[] BuildFassMessageHeader(byte[] dataToSend)
        {
            var sb = new StringBuilder();
            sb.Append("POST / HTTP/1.1\n");
            sb.Append("Host: webservices.pcnclosings.com:" + _port + "\n");
            sb.Append($"Content-Length: {dataToSend.Length}\n");
            sb.Append("Expect: 100-continue\n");
            sb.Append("Connection: Keep-Alive\n\n");
            var header = sb.ToString();
            var headerBytes = Encoding.ASCII.GetBytes(header);
            return headerBytes;
        }
    }
}