/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc
    
    文件名：JSSDKHelper.cs
    文件功能描述：JSSDK生成签名的方法等
    
    
    创建标识：Senparc - 20150313
    
    修改标识：Senparc - 20150313
    修改描述：整理接口
----------------------------------------------------------------*/

using System;
using System.Collections;
using System.Text;
using System.Net;
using System.Runtime.Serialization.Json;//需添加System.Runtime.Serialization引用
using System.IO;
namespace Weixin.JSSDK
{
    public class JSSDKHelper
    {
        /// <summary>
        /// 获取随机字符串
        /// </summary>
        /// <returns></returns>
        public static string GetNoncestr()
        {
            Random random = new Random();
            return MD5UtilHelper.GetMD5(random.Next(1000).ToString(), "GBK");
        }

        /// <summary>
        /// 获取时间戳
        /// <remarks>
        /// 2016-05-03：修改返回类型，方便GetSignature调用，避免再次类型转换
        /// </remarks>
        /// </summary>
        /// <returns></returns>
        public static long GetTimestamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds);
        }

        /// <summary>
        /// 获取JS-SDK权限验证的签名Signature
        /// </summary>
        /// <param name="jsapi_ticket">jsapi_ticket</param>
        /// <param name="noncestr">随机字符串(必须与wx.config中的nonceStr相同)</param>
        /// <param name="timestamp">时间戳(必须与wx.config中的timestamp相同)</param>
        /// <param name="url">当前网页的URL，不包含#及其后面部分(必须是调用JS接口页面的完整URL)</param>
        /// <returns></returns>
        public static string GetSignature(string jsapi_ticket, string noncestr, long timestamp, string url)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("jsapi_ticket=").Append(jsapi_ticket).Append("&")
             .Append("noncestr=").Append(noncestr).Append("&")
             .Append("timestamp=").Append(timestamp).Append("&")
             .Append("url=").Append(url.IndexOf("#") >= 0 ? url.Substring(0, url.IndexOf("#")) : url);
            return SHA1UtilHelper.GetSha1(sb.ToString()).ToLower();
        }

        /// <summary>
        /// 获取微信jsapi_ticket
        /// </summary>
        /// <param name="token">access_token</param>
        /// <returns>jsapi_ticket</returns>
        public static string GetTicket(string token)
        {
            string ticketUrl = "https://api.weixin.qq.com/cgi-bin/ticket/getticket?access_token=" + token + "&type=jsapi";
            string jsonresult = HttpGet(ticketUrl, "UTF-8");
            WX_Ticket wxTicket = JsonDeserialize<WX_Ticket>(jsonresult);
            return wxTicket.ticket;
        }
                /// <summary>
        /// JSON反序列化
        /// </summary>
        /// <typeparam name="T">实体类</typeparam>
        /// <param name="jsonString">JSON</param>
        /// <returns>实体类</returns>
        private static T JsonDeserialize<T>(string jsonString)
        {
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));
            T obj = (T)ser.ReadObject(ms);
            return obj;
        }

        /// <summary>
        /// HttpGET请求
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="encode">编码方式：GB2312/UTF-8</param>
        /// <returns>字符串</returns>
        private static string HttpGet(string url, string encode)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "text/html;charset=" + encode;
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding(encode));
            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();
            return retString;
        }


        /// <summary>
       /// 通过微信API获取access_token得到的JSON反序列化后的实体
       /// </summary>
       public class WX_Token
       {
          public string access_token { get; set; }
          public string expires_in { get; set; }
       }

       /// <summary>
      /// 通过微信API获取jsapi_ticket得到的JSON反序列化后的实体
       /// </summary>
      public class WX_Ticket
      {
          public string errcode { get; set; }
          public string errmsg { get; set; }
          public string ticket { get; set; }
          public string expires_in { get; set; }
      }
    }
}
