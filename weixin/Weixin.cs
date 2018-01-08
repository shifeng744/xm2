using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using Senparc.Weixin.MP.CommonAPIs;
namespace Weixin
{
    public class Wx
    {
        public static string appId = ConfigurationManager.AppSettings["appId"];
        public static string appSecret = ConfigurationManager.AppSettings["appSecret"];
        public static string accessToken 
        {get { return AccessTokenContainer.TryGetToken(appId, appSecret);}}
    }
}