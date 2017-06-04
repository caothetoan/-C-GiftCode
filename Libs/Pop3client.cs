using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Sockets;
using System.IO;
using Libs;

namespace Minxtu.Biz.Utils
{
    /// <summary>
    /// Summary description for Pop3client
    /// </summary>
    public class Pop3client
    {
        private TcpClient tpc;
        private Stream POP3Stream;
        private StreamReader inStream;
        private string strDataIn = String.Empty;

        private string _username = String.Empty;
        private string _password = String.Empty;
        private string _mailserver = String.Empty;

        public Pop3client(string username, string password, string mailserver)
        {
            _username = username;
            _password = password;
            _mailserver = mailserver;

            if (_username.ToLower().IndexOf(Config.MailDomain) >= 0)
            {
                _username = _username.Substring(0, _username.ToLower().IndexOf(Config.MailDomain));
            }
        }

        public bool CheckLogin(ref Exception ex)
        {
            bool flag = false;
            try
            {
                tpc = new TcpClient();
                tpc.Connect(_mailserver, 110);

                POP3Stream = tpc.GetStream();
                inStream = new StreamReader(POP3Stream);

                if (WaitFor("+OK"))
                {
                    SendData("USER " + _username);
                    if (WaitFor("+OK"))
                    {
                        SendData("PASS " + _password);
                        if (WaitFor("+OK"))
                        {
                            flag = true;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                ex = e;
            }
            finally
            {
                CloseConnection();
            }
            return flag;
        }

        private bool WaitFor(string strTarget)
        {
            strDataIn = inStream.ReadLine();
            if (strDataIn.IndexOf(strTarget) == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void SendData(string strCommand)
        {
            byte[] outBuff;
            outBuff = ConvertStringToByteArray(strCommand + Environment.NewLine);
            POP3Stream.Write(outBuff, 0, strCommand.Length + 2);
        }

        private byte[] ConvertStringToByteArray(string stringToConvert)
        {
            return (new System.Text.ASCIIEncoding()).GetBytes(stringToConvert);
        }

        private void CloseConnection()
        {
            try
            {
                SendData("QUIT");
                inStream.Close();
                POP3Stream.Close();
            }
            catch
            {

            }
        }
    }
}