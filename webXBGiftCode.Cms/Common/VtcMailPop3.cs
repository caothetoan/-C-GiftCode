using System;
using System.Net.Sockets;
using System.Text;
using Go.App.Utils;

namespace webXBGiftCode.Cms.Common
{
    public class VtcMailPop3 : TcpClient
    {
        public string CheckAuth(string username, string password)
        {
            Connect("smtp.mail.vtc.vn", 110);

            var response = Response();
            if (response.Substring(0, 3) != "+OK")
            {
                Logger.WriteLog(Logger.LogType.Trace, string.Format("[{0}-{1}]: {2}", username, password, response));
                return response;
            }

            var message = "USER " + username + "\r\n";
            Write(message);
            response = Response();

            if (response.Substring(0, 3) != "+OK")
            {
                Logger.WriteLog(Logger.LogType.Trace, string.Format("[{0}-{1}]: {2}", username, password, response));
                return response;
            }

            message = "PASS " + password + "\r\n";
            Write(message);
            response = Response();

            if (response.Substring(0, 3) != "+OK")
            {
                Logger.WriteLog(Logger.LogType.Trace, string.Format("[{0}-{1}]: {2}", username, password, response));
                return response;
            }

            message = "QUIT\r\n";
            Write(message);
            return "OK";
        }

        private string Response()
        {
            var encoding = new ASCIIEncoding();
            var buffer = new byte[1024];
            var stream = GetStream();
            var count = 0;
            while (true)
            {
                var data = new byte[2];
                var bytes = stream.Read(data, 0, 1);
                if (bytes != 1) break;
                buffer[count] = data[0];
                count++;
                if (data[0] == '\n') break;
            }
            var result = encoding.GetString(buffer, 0, count);
            return result;
        }

        private void Write(string message)
        {
            var encoding = new ASCIIEncoding();
            var buffer = encoding.GetBytes(message);
            var stream = GetStream();
            stream.Write(buffer, 0, buffer.Length);
        }
    }
}