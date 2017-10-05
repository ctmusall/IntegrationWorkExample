using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Net.Sockets;
using System.Reflection;
using System.Threading;

namespace PCN_Integration.Services
{
  public class Result
  {

    protected Result(bool success, string message = null)
    {
      Messages = new List<string> { message };
      Success = success;
    }

    public bool Success { get; set; }

    public string Message
    {
      get { return Messages.First(); }
    }

    public IList<string> Messages { get; protected set; }

    public static Result CreateSuccess()
    {
      return new Result(true);
    }

    public static Result CreateSuccess(string message)
    {
      return new Result(true, message);
    }

    public static Result CreateFailure(string message, string field = null)
    {
      return new Result(false, message);
    }

    public Result()
    {
    }
  }

  public class Result<T> : Result
  {
    private readonly T m_Payload;

    private Result(T payload)
      : base(true)
    {
      m_Payload = payload;
    }

    private Result(T payload, string message)
      : base(true, message)
    {
      m_Payload = payload;
    }

    protected Result(bool success, string message)
      : base(success, message)
    {
    }

    public virtual T Payload
    {
      get
      {
        if (Success == false)
        {
          throw new InvalidOperationException("Payload may not be accessed when the operation fails.");
        }
        return m_Payload;
      }
    }

    public static Result<T> CreateSuccess(T payload)
    {
      return new Result<T>(payload);
    }

    public static Result<T> CreateSuccess(T payload, string message)
    {
      return new Result<T>(payload, message);
    }

    public static Result<T> CreateFailure(string message)
    {
      return new Result<T>(false, message);
    }
  }

  public class MirthService
  {
    public string Ip { get; set; }
    public int Port { get; set; }
    public Result SendFassMessageToMirth(string serializedMessage)
    {
      Byte[] dataToSend = Encoding.ASCII.GetBytes(serializedMessage);

      using (var sock = new TcpClient(this.Ip, this.Port))
      {
        try
        {
          using (var stream = sock.GetStream())
          {
            int port = this.Port;
            var sb = new StringBuilder();
            sb.Append("POST / HTTP/1.1\n");
            sb.Append("Host: webservices.pcnclosings.com:" + port + "\n");
            sb.Append(string.Format("Content-Length: {0}\n", dataToSend.Length));
            sb.Append("Expect: 100-continue\n");
            sb.Append("Connection: Keep-Alive\n\n");
            var header = sb.ToString();
            var headerBytes = Encoding.ASCII.GetBytes(header);

            stream.Write(headerBytes, 0, headerBytes.Length);
            stream.Write(dataToSend, 0, dataToSend.Length);
            sock.Close();
          }
          return Result.CreateSuccess();
        }
        catch (Exception ex)
        {
          sock.Close();

          //Log.Trace(String.Format("Failed to send xml to mirth: {0}", e));
          //TODO add logging and file handling.
          return Result.CreateFailure("Failed to send XML to Mirth");
        }
      }
    }
  }
}
