using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Web;

namespace webXBGiftCode.Cms.Common
{

    public enum EmailResultEnum
    {
        // ReSharper disable InconsistentNaming
        MAIL_SEVER_ERROR,
        INVALID_LOGIN_INFO,
        INVALID_ACCOUNT,
        LOGIN_SUCCESS
        // ReSharper restore InconsistentNaming
    }

    public class VtcMailPop3Client
    {
        private readonly string _mailserver = string.Empty;
        private readonly string _password = string.Empty;
        private readonly string _username = string.Empty;
        private StreamReader _inStream;
        private Stream _pop3Stream;
        private string _strDataIn = string.Empty;
        private TcpClient _tcp;

        public VtcMailPop3Client(string username, string password, string mailserver)
        {
            _username = username;
            _password = password;
            _mailserver = mailserver;
        }

        public bool CheckLogin(ref EmailResultEnum emailResult)
        {
            try
            {
                _tcp = new TcpClient();
                _tcp.Connect(_mailserver, 110);
                _pop3Stream = _tcp.GetStream();
                _inStream = new StreamReader(_pop3Stream);
                if (WaitFor("+OK"))
                {
                    SendData("USER " + _username);
                    if (!WaitFor("+OK")) return false;

                    SendData("PASS " + _password);
                    if (WaitFor("+OK")) return true;

                    emailResult = EmailResultEnum.INVALID_LOGIN_INFO;
                }
                return false;
            }
            catch
            {
                emailResult = EmailResultEnum.MAIL_SEVER_ERROR;
            }
            finally
            {
                CloseConnection();
            }
            return false;
        }

        private void CloseConnection()
        {
            try
            {
                SendData("QUIT");
                _inStream.Close();
                _pop3Stream.Close();
            }
            catch
            {
            }
        }

        private static byte[] ConvertStringToByteArray(string stringToConvert)
        {
            return new ASCIIEncoding().GetBytes(stringToConvert);
        }

        private void SendData(string strCommand)
        {
            var buffer = ConvertStringToByteArray(strCommand + Environment.NewLine);
            _pop3Stream.Write(buffer, 0, strCommand.Length + 2);
        }

        private bool WaitFor(string strTarget)
        {
            _strDataIn = _inStream.ReadLine();
            return _strDataIn != null && (_strDataIn.IndexOf(strTarget, StringComparison.CurrentCultureIgnoreCase) == 0);
        }
    }
}