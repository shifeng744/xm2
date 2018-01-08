using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Senparc.Weixin.MP.AdvancedAPIs;
using Senparc.Weixin.MP.CommonAPIs;
using Weixin;

namespace NavShare
{
    public class Data
    {
        public static string GetNavFromType()
        {
            string fromStr =HttpContext.Current.Request["f"];
            if (fromStr != null)
                return fromStr;
            else
                return "other";
        }
        public static string GetNavOpenId()
        {
            string code = HttpContext.Current.Request["code"];
            string from = HttpContext.Current.Request["from"];
            if (code != null && from == null)
            {
                var accessToken = OAuth.GetAccessToken(Wx.appId, Wx.appSecret, code);
                return accessToken.openid;
            }
            else
            {
                string url = "http://" + HttpContext.Current.Request.Url.Host +
                    HttpContext.Current.Request.FilePath;
                url += "?s=" + HttpContext.Current.Request["s"];
                if (from != null) url += "&f=" + from;
                url = OAuth.GetAuthorizeUrl(Wx.appId, url, "a", OAuthScope.snsapi_base);
                HttpContext.Current.Response.Redirect(url, true);
                return null;
            }

        }


    }
}