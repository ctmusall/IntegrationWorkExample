using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Sockets;
using System.Text;

namespace ReswareOrderMonitorService.Mirth
{
    internal class MirthServiceClient : IMirthServiceClient
    {
        public bool SendMessageToMirth(string message, int port, string ip)
        {
            try
            {
                var dataToSend = Encoding.ASCII.GetBytes(message);
                var socket = new TcpClient(ip, port);

                var stream = socket.GetStream();
                var headerBytes = BuildMessageHeader(dataToSend, port);

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

        private static byte[] BuildMessageHeader(IReadOnlyCollection<byte> dataToSend, int port)
        {
            var sb = new StringBuilder();
            sb.Append("POST / HTTP/1.1\n");
            sb.Append("Host: webservices.pcnclosings.com:" + port + "\n");
            sb.Append($"Content-Length: {dataToSend.Count}\n");
            sb.Append("Expect: 100-continue\n");
            sb.Append("Connection: Keep-Alive\n\n");
            var header = sb.ToString();
            var headerBytes = Encoding.ASCII.GetBytes(header);
            return headerBytes;
        }
    }
}
