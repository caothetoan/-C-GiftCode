using System;
using System.IO;
using System.Net;
using System.Web.Script.Serialization;
using log4net.Repository.Hierarchy;

namespace Go.GraphApi.Biz
{
    public interface IServerMobileService
    {
        DataBalanceResult GetBalance(string accessToken);

        AuthenResult GetAccessToken(string code);

        DataBuyItemResult BuyItem(string token, long transactionId, long itemCode, long itemPrice, string accountIp,
                                  string description);
    }

    public class ServerMobileService : IServerMobileService
    {

        string DomainServer
        {
            get
            {
                return "http://localhost:8899";
            }
        }

        private string GetWebRequest(string requestUrl)
        {
            var request = (HttpWebRequest)WebRequest.Create(requestUrl);
            request.Timeout = 10000;
            request.Method = "GET";
            //request.UserAgent = _userAgent;
            request.AllowAutoRedirect = true;

            var response = request.GetResponse();

            var sr = new StreamReader(response.GetResponseStream());

            var jsonResponse = sr.ReadToEnd();

            //Logger.Info(string.Format("[GraphAPI] url: {0}, response: {1}", requestUrl, jsonResponse));

            return jsonResponse;
        }
        private string PostWebRequest(string requestUrl)
        {
            var uri = new Uri(requestUrl);
            var queryString = uri.Query.Replace("?", "");
            requestUrl = requestUrl.Replace(uri.Query, "");

            var request = (HttpWebRequest)WebRequest.Create(requestUrl);
            request.Timeout = 10000;
            request.Method = "POST";
            request.ContentLength = queryString.Length;
            request.ContentType = "application/x-www-form-urlencoded";
            

            var streamWriter = new StreamWriter(request.GetRequestStream());
            streamWriter.Write(queryString);
            streamWriter.Close();

            var response = request.GetResponse();

            var sr = new StreamReader(response.GetResponseStream());

            var jsonResponse = sr.ReadToEnd();

            //Logger.Info(string.Format("[GraphAPI] url: {0}, response: {1}", requestUrl, jsonResponse));

            return jsonResponse;
        }
        

        public DataBalanceResult GetBalance(string accessToken)
        {
            string requestUrl = string.Format("{0}/Billing.ashx?token={1}&method=balance", DomainServer, accessToken);

            var jsonResponse = GetWebRequest(requestUrl);

            var result = new JavaScriptSerializer().Deserialize<DataBalanceResult>(jsonResponse);

            return result;
        }

        public AuthenResult GetAccessToken(string code)
        {
            string requestUrl = string.Format("{0}/AccessToken.ashx{1}", DomainServer, code);

            var loginResult = PostWebRequest(requestUrl);

            var authenResult = new JavaScriptSerializer().Deserialize<AuthenResult>(loginResult);

            return authenResult;
        }

        public DataBuyItemResult BuyItem(string token, long transactionId, long itemCode, long itemPrice, string accountIp, string description)
        {
            string requestUrl = string.Format("{0}/Billing.ashx?token={1}&method=buyitem&transid={2}&itemcode={3}&itemprice={4}&accountip={5}&des={6}", DomainServer, token, transactionId, itemCode, itemPrice, accountIp, description);

            var jsonResponse = GetWebRequest(requestUrl);

            var result = new JavaScriptSerializer().Deserialize<DataBuyItemResult>(jsonResponse);

            return result;
        }
    }
}