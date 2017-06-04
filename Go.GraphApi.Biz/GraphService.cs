using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web.Script.Serialization;


namespace Go.GraphApi.Biz
{
    public class GraphService : IGraphService
    {
        public static string MD5(string s)
        {
            Byte[] originalBytes;
            Byte[] encodedBytes;
            MD5 md5;

            //Instantiate MD5CryptoServiceProvider, get bytes for original password and compute hash (encoded password)
            md5 = new MD5CryptoServiceProvider();
            originalBytes = Encoding.Default.GetBytes(s);
            encodedBytes = md5.ComputeHash(originalBytes);

            //Convert encoded bytes back to a 'readable' string
            return BitConverter.ToString(encodedBytes).ToLower().Replace("-", "");
        }
        /// <summary>
        /// login direct GoVN
        /// </summary>
        /// <param name="username">username người dùng</param>
        /// <param name="password">password người dùng</param>
        /// <param name="message"></param>
        /// <returns></returns>
        public AuthenResult Authenticate(string username, string password, out string message)
        {
            try
            {
                var salt = GetSalt(username);
                var loginResult = Login(username, password, salt);

                //login true
                if (loginResult != null)
                {
                    message = loginResult._message;

                    if (loginResult._code == "1")
                    {
                        
                            var accessToken = GetAccessToken(loginResult._data.Code);
                            if (accessToken != null)
                            {
                                return new AuthenResult()
                                    {
                                        IsAuthen = true,
                                        AccountId = Convert.ToInt64(loginResult._data.UserId),
                                        AccountName = loginResult._data.User,
                                        AccessToken = accessToken.access_token,
                                        PublicName = loginResult._data.User
                                    };
                            }
                       
                    }
                    return new AuthenResult()
                    {
                        IsAuthen = false
                    };
                }
                message = "Tên đăng nhập hoặc mật khẩu không tồn tại";
                return new AuthenResult()
                {
                    IsAuthen = false
                };
            }
            catch (Exception e)
            {
                message = "Có lỗi xảy ra trên hệ thống";
                //Logger.Error(e);
                return new AuthenResult()
                {
                    IsAuthen = false
                };
            }
        }

        public AuthenResult AuthenticateDetail(string username, string password, out string error, out string message)
        {
            try
            {
                var salt = GetSalt(username);
                var loginResult = Login(username, password, salt);

                //login true
                if (loginResult != null)
                {
                    if (loginResult._code == "1")
                    {
                        error = "0";
                        message = "Login Success";
                        return new AuthenResult()
                            {
                                IsAuthen = true,
                                AccountId = Convert.ToInt64(loginResult._data.UserId),
                                AccountName = loginResult._data.User,
                                AccessToken = "",
                                PublicName = loginResult._data.User
                            };

                    }
                    else
                    {
                        switch (loginResult._code)
                        {
                            case "-300":
                                error = "3";
                                message = "This account is not exist";
                                break;
                            case "-301":
                                error = "1";
                                message = "Username or Password is invalid";
                                break;
                            case "-302":
                                error = "2";
                                message = " This account is locked";
                                break;
                                
                            default:
                                error = "-1";
                                message = "Unknow Error";
                                break;
                        }
                        return new AuthenResult()
                        {
                            IsAuthen = false
                        };    
                    }
                }
                message = "Unknow Error";
                error = "-1";
                return new AuthenResult()
                    {
                        IsAuthen = false
                    };
            }
            catch (Exception e)
            {
                message = "The unknown error is happend";
                error = "100";
                //Logger.Error(e);
                return new AuthenResult()
                {
                    IsAuthen = false
                };
            }
        }

        public AuthenResult FastRegister(string username, string password, out string message)
        {
            try
            {
                var loginResult = Register(username, password);
                //login true
                if (loginResult != null)
                {
                    message = loginResult._message;
                    if (loginResult._code == "1")
                    {
                        var accessToken = GetAccessToken(loginResult._data.Code);

                        if (accessToken != null)
                        {
                            return new AuthenResult()
                                {
                                    IsAuthen = true,
                                    AccountId = Convert.ToInt64(loginResult._data.UserId),
                                    AccountName = loginResult._data.User,
                                    AccessToken = accessToken.access_token,
                                    PublicName = loginResult._data.User
                                };
                        }
                    }
                    return new AuthenResult()
                    {
                        IsAuthen = false,
                    };
                }
                message = "Đăng ký thất bại";
                return new AuthenResult()
                {
                    IsAuthen = false
                };
            }
            catch (Exception e)
            {
                //Logger.Error(e);
                message = "Có lỗi xảy ra trên hệ thống";
                return new AuthenResult()
                {
                    IsAuthen = false
                };
            }
        }

        public BaseApiResult UpdatePassword(string username, string password, string newpass, string accesstoken, out string message)
        {
            try
            {
                var res = Request(GoConstants.GoUrlUpdatePassWord(accesstoken), "POST", new Dictionary<string, string>
                    {
                        { "username", username }, 
                        { "oldPassword", password },
                        { "newPassword", newpass },
                    });
                var data = new JavaScriptSerializer().Deserialize<BaseApiResult>(res);
                message = data._message;
                return data;
            }
            catch (Exception e)
            {
                //Logger.Error(e);
                message = "Có lỗi xảy ra trên hệ thống";
                return new BaseApiResult()
                {
                    _code = "-1",
                    _message = message
                };
            }
        }

        public AuthenResult FastPlay(string deviceid, string email, string cpid, string refcode, string agent, string platform, out string message)
        {
            try
            {
                var loginResult = FastPlayGo(deviceid, email, cpid, refcode, agent, platform);
                //login true
                if (loginResult != null)
                {
                    message = loginResult._message;
                    if (loginResult._code == "1")
                    {
                        var accessToken = GetAccessToken(loginResult._data.Code);

                        if (accessToken != null)
                        {
                            return new AuthenResult()
                            {
                                IsAuthen = true,
                                AccountId = Convert.ToInt64(loginResult._data.UserId),
                                AccountName = loginResult._data.User,
                                AccessToken = accessToken.access_token,
                                PublicName = loginResult._data.User
                            };
                        }
                    }
                    return new AuthenResult()
                    {
                        IsAuthen = false,
                    };
                }
                message = "Đăng nhập thất bại";
                return new AuthenResult()
                {
                    IsAuthen = false
                };
            }
            catch (Exception e)
            {
                //Logger.Error(e);
                message = "Có lỗi xảy ra trên hệ thống";
                return new AuthenResult()
                {
                    IsAuthen = false
                };
            }
        }

        public AuthenResult LoginOverFaceBook(string facebookid, string email, string fullname, string cpid, string refcode, string agent, string platform, out string message)
        {
            try
            {
                var loginResult = LoginOverFace(facebookid, email, fullname, cpid, refcode, agent, platform);
                //login true
                if (loginResult != null)
                {
                    message = loginResult._message;
                    if (loginResult._code == "1")
                    {
                        var accessToken = GetAccessToken(loginResult._data.Code);

                        if (accessToken != null)
                        {
                            return new AuthenResult()
                            {
                                IsAuthen = true,
                                AccountId = Convert.ToInt64(loginResult._data.UserId),
                                AccountName = loginResult._data.User,
                                AccessToken = accessToken.access_token,
                                PublicName = loginResult._data.User
                            };
                        }
                    }
                    return new AuthenResult()
                    {
                        IsAuthen = false,
                    };
                }
                message = "Đăng nhập thất bại";
                return new AuthenResult()
                {
                    IsAuthen = false
                };
            }
            catch (Exception e)
            {
                //Logger.Error(e);
                message = "Có lỗi xảy ra trên hệ thống";
                return new AuthenResult()
                {
                    IsAuthen = false
                };
            }
        }

        public BillingResult ActiveTelCo(string accesstoken, string accountName, string cardCode, string cardSerial, int cardType, int transactionId, out string message)
        {
            try
            {
                NLogLogger.Info(accesstoken);

                var dic = new Dictionary<string, string>
                    {
                        {"AccountName", accountName},
                        {"CardCode", cardCode},
                        {"CardSerial", cardSerial},
                        {"CardType", cardType.ToString()},
                        {"ClientId", GoConstants.ClientId},
                        {"ClientSecret", GoConstants.SecretKey},
                        {"TransactionId", transactionId.ToString()},
                    };


                NLogLogger.Info(Newtonsoft.Json.JsonConvert.SerializeObject(dic));
                var res = Request(GoConstants.UrlTopUpByCardDirect(accesstoken), "POST", dic);
                NLogLogger.Info(res);

                var data = new JavaScriptSerializer().Deserialize<BillingResult>(res);
                message = data._data.Description;
                return data;
            }
            catch (Exception ex)
            {
                NLogLogger.DebugMessage(ex);
                NLogLogger.PublishException(ex);
                message = "Có lỗi xảy ra trên hệ thống " ;
               // Logger.Error(ex);
                return null;
            }
        }

        public DataBalanceResult GetBalance(string userid, string token)
        {
            try
            {
                var getBalanceUrl = GoConstants.get_balance_url(token);

                
                    var res = Request(getBalanceUrl, "GET", null);

                    var data = new JavaScriptSerializer().Deserialize<GetBalanceReult>(res);
                    var x = data._data;
                    x.ResponseStatus = data._code;
                    return x;
                
            }
            catch (Exception ex)
            {
               // Logger.Error(ex);
                return null;
            }
        }

        public DataBuyItemResult BuyItem(string username, string token, long transId, long itemcode, long itemprice, string accountIP, string description, out string mesage)
        {
            try
            {
                var transdate = DateTime.Now.ToString("yyyyMMddhhmmss");
                var getBalanceUrl = GoConstants.get_buyitem_url(token);
                Dictionary<string, string> post;
                
                    post = new Dictionary<string, string>
                        {
                            {"itemcode", itemcode.ToString()},
                            {"itemprice", itemprice.ToString()},
                            {"accountIP", accountIP},
                            {"description", description},
                            {"serviceAppId", GoConstants.GoserviceAppId},
                            {"serviceKey", GoConstants.GoserviceKey},
                            {"transdate", transdate},
                            {"transId", transId.ToString()}
                        };
                

                var res = Request(getBalanceUrl, "POST", post);
                var data = new JavaScriptSerializer().Deserialize<GetBuyItemReult>(res);
                mesage = data._message;
                return data._data;
            }
            catch (Exception ex)
            {
                mesage = "Có lỗi xảy ra trên hệ thống";
               // Logger.Error(ex);
                return null;
            }
        }

        public bool ConfirmTopup(long gopc_billingtransid, bool gopc_transconfirm, string gopc_servicekey, string gopc_serviceappid, string gopc_securehash, string gopc_transid)
        {
            try
            {
                var dic = new Dictionary<string, string>
                    {
                        {"gopc_billingtransid", gopc_billingtransid.ToString()},
                        {"gopc_transconfirm", gopc_transconfirm.ToString()},
                        {"gopc_servicekey", gopc_servicekey},
                        {"gopc_serviceappid", gopc_serviceappid},
                        {"gopc_securehash", gopc_securehash},
                        {"gopc_transid",gopc_transid}
                    };
                Request(GoConstants.go_confirm_top_url, "POST", dic);
                //Logger.Info(GoConstants.go_confirm_top_url);
                //var data = new JavaScriptSerializer().Deserialize<ConfirmTopupResult>(res);
                //if (data.gopc_responsecode > 1) return true;
                return true;
            }
            catch (Exception ex)
            {
                //Logger.Error(ex);
                return false;
            }
        }

        public UserData GetInfo(string token)
        {
            try
            {
                var text = Request(GoConstants.get_userinfo(token));
                var data = new JavaScriptSerializer().Deserialize<UserData>(text);
                return data;
            }
            catch (Exception ex)
            {
                //Logger.Error(ex);
                return null;
            }
        }

        public GetAccessTokenResult GetAccessToken(string code)
        {
            try
            {
                var getTokenUrl = GoConstants.UrlGetAccessToken(GoConstants.redirect_uri, code);
                var res = Request(getTokenUrl);
                var data = new JavaScriptSerializer().Deserialize<GetAccessTokenResult>(res);

                return data;

            }
            catch (Exception ex)
            {
                //Logger.Error(ex);
                return null;
            }
        }

        LoginResult Login(string username, string password, string salt)
        {
            try
            {
                // MD5 encrypt username, password and salt
                var pass = MD5(username + password + salt);
                var res = "";
                
                    res = Request(GoConstants.GoUrlLogin, "POST", new Dictionary<string, string> { { "username", username }, { "password", pass } });
                
                var data = new JavaScriptSerializer().Deserialize<LoginResult>(res);

                return data;

            }
            catch (Exception ex)
            {
                //Logger.Error(ex);
                return null;
            }
        }

        RegisterResult Register(string username, string password)
        {
            try
            {
                var pass = password;
                var res = Request(GoConstants.GoUrlRegister(), "POST", new Dictionary<string, string> { { "username", username }, { "password", pass } });
                var data = new JavaScriptSerializer().Deserialize<RegisterResult>(res);

                return data;

            }
            catch (Exception ex)
            {
                //Logger.Error(ex);
                return null;
            }
        }

        FastPlayResult FastPlayGo(string deviceid, string email, string cpid, string refcode, string agent, string platform)
        {
            try
            {
                var res = Request(GoConstants.GoUrlFastPlay, "POST", new Dictionary<string, string>
                    {
                        { "DeviceId", deviceid }, 
                        { "Email", email },
                        { "CpId", cpid },
                        { "RefCode", refcode },
                        { "Agent", agent },
                        { "Platform", platform }
                    });
                var data = new JavaScriptSerializer().Deserialize<FastPlayResult>(res);

                return data;

            }
            catch (Exception ex)
            {
                //Logger.Error(ex);
                return null;
            }
        }

        FastPlayResult LoginOverFace(string facebookid, string email, string fullname, string cpid, string refcode, string agent, string platform)
        {
            try
            {
                var res = Request(GoConstants.GoUrlLoginOverFaceBook, "POST", new Dictionary<string, string>
                    {
                        { "DeviceId", facebookid }, 
                        { "Email", email },
                        { "FullName", fullname },
                        { "CpId", cpid },
                        { "RefCode", refcode },
                        { "Agent", agent },
                        { "Platform", platform }
                    });
                var data = new JavaScriptSerializer().Deserialize<FastPlayResult>(res);

                return data;

            }
            catch (Exception ex)
            {
                //Logger.Error(ex);
                return null;
            }
        }

        string GetSalt(string username)
        {
            try
            {
                string res;
               
                    res = Request(GoConstants.GoUrlSalt, "POST", new Dictionary<string, string> { { "username", username } });
                
                var data = new JavaScriptSerializer().Deserialize<GetSaltResult>(res);
                return data._data;

            }
            catch (Exception ex)
            {
                //Logger.Error(ex);
                return string.Empty;
            }
        }

        public static string Request(string url, string method = "GET", Dictionary<string, string> formBody = null)
        {
            try
            {
                var request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = method;
                var encoding = new UTF8Encoding();
                if (!"GET".Equals(method) && formBody != null)
                {
                    // Post 
                    string formData = formBody.Aggregate(string.Empty, (current, keyValuePair) => current + string.Format("&{0}={1}", keyValuePair.Key, keyValuePair.Value));
                    formData = formData.TrimStart('&');
                    byte[] data = encoding.GetBytes(formData);
                    request.ContentType = "application/x-www-form-urlencoded";
                    request.ContentLength = data.Length;
                    Stream writer = request.GetRequestStream();
                    writer.Write(data, 0, data.Length);
                    writer.Close();
                }

                var response = (HttpWebResponse)request.GetResponse();
                using (var reader = new StreamReader(response.GetResponseStream()))
                {
                    string stringData = reader.ReadToEnd();

                    //Logger.Info(string.Format("[IGraphService] Url: {0}, Respons: {1}", url, stringData));

                    return stringData;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
