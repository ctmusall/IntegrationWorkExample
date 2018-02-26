﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Sockets;
using System.Text;

namespace ReswareOrderMonitorService.Mirth
{
    internal class MirthServiceClient
    {
        private readonly int _port;
        private readonly string _ip;
        internal MirthServiceClient(int port, string ip)
        {
            _port = port;
            _ip = ip;
        }

        internal bool SendMessageToMirth(string message)
        {
            var dataToSend = Encoding.ASCII.GetBytes(message);
            var socket = new TcpClient(_ip, _port);

            try
            {
                var stream = socket.GetStream();
                var headerBytes = BuildMessageHeader(dataToSend);

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

        private byte[] BuildMessageHeader(IReadOnlyCollection<byte> dataToSend)
        {
            var sb = new StringBuilder();
            sb.Append("POST / HTTP/1.1\n");
            sb.Append("Host: webservices.pcnclosings.com:" + _port + "\n");
            sb.Append($"Content-Length: {dataToSend.Count}\n");
            sb.Append("Expect: 100-continue\n");
            sb.Append("Connection: Keep-Alive\n\n");
            var header = sb.ToString();
            var headerBytes = Encoding.ASCII.GetBytes(header);
            return headerBytes;
        }
    }
}
