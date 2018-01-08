using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Text;
using System.Net;
using Senparc.Weixin.MP;
using Senparc.Weixin.MP.AdvancedAPIs;
using Senparc.Weixin.MP.Helpers;
using Senparc.Weixin.MP.AdvancedAPIs.Media3;

namespace Weixin
{
    public class Media2
    {
        public static UploadResultJson Upload(string accessToken, UploadMediaFileType type, HttpPostedFile postfile)
        {
            //微信公众号上传媒体文件接口地址
            var urlFormat = "http://file.api.weixin.qq.com/cgi-bin/media/upload?access_token={0}&type={1}";
            return Upload<UploadResultJson>(accessToken, urlFormat, postfile, type.ToString());
        }

        public static UploadForeverMediaResult UploadForeverMedia(string accessToken, HttpPostedFile postfile)
        {
            var url = "https://api.weixin.qq.com/cgi-bin/material/add_material?access_token={0}";
            return Upload<UploadForeverMediaResult>(accessToken, url, postfile);
        }
        public static UploadImgResult UploadImg(string accessToken, HttpPostedFile postfile)
        {
            var url = "https://api.weixin.qq.com/cgi-bin/media/uploadimg?access_token={0}";
            return Upload<UploadImgResult>(accessToken, url, postfile);
        }


        public static T Upload<T>(string accessToken, string urlFormat, System.Web.HttpPostedFile postfile, params string[] querys)
        {
            var url =GetApiUrl(urlFormat, accessToken, querys);
            
            string returnText = Upload(url, postfile);
            var result =ApiHelper.GetResult<T>(returnText);
            return result;
        }
        private static string GetApiUrl(string urlFormat, string accessToken, string[] querys)
        {
            string[] args = new string[] { accessToken };
            if (querys.Length > 0)
                args = args.Concat(querys).ToArray();
            var url = string.Format(urlFormat, args);
            return url;
        }
        public static string Upload(string url, HttpPostedFile postfile, Encoding encoding = null)
        {
            //创建HTTP请求
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            int n = postfile.ContentLength;
            Stream postStream = new MemoryStream();
            #region 处理Form表单文件上传
            string boundary = "----" + DateTime.Now.Ticks.ToString("x");
            string formdataTemplate = "\r\n--" + boundary + "\r\nContent-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\nContent-Type: application/octet-stream\r\n\r\n";
            var formdata = string.Format(formdataTemplate, "media", postfile.FileName /*Path.GetFileName(fileName)*/);
            var formdataBytes = Encoding.ASCII.GetBytes(postStream.Length == 0 ? formdata.Substring(2, formdata.Length - 2) : formdata);//第一行不需要换行
            postStream.Write(formdataBytes, 0, formdataBytes.Length);
            byte[] bytes = new byte[n];
            postfile.InputStream.Read(bytes, 0, n);
            postStream.Write(bytes, 0, n);
            var footer = Encoding.ASCII.GetBytes("\r\n--" + boundary + "--\r\n");
            postStream.Write(footer, 0, footer.Length);
            request.ContentType = string.Format("multipart/form-data; boundary={0}", boundary);

            #endregion

            request.ContentLength = postStream != null ? postStream.Length : 0;
            request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
            request.KeepAlive = true;
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/31.0.1650.57 Safari/537.36";

            #region 输入二进制流
            if (postStream != null)
            {
                postStream.Position = 0;

                //直接写入流
                Stream requestStream = request.GetRequestStream();

                byte[] buffer = new byte[1024];
                int bytesRead = 0;
                while ((bytesRead = postStream.Read(buffer, 0, buffer.Length)) != 0)
                {
                    requestStream.Write(buffer, 0, bytesRead);
                }

                postStream.Close();//关闭文件访问
            }
            #endregion

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            using (Stream responseStream = response.GetResponseStream())
            {
                using (StreamReader myStreamReader = new StreamReader(responseStream, encoding ?? Encoding.GetEncoding("utf-8")))
                {
                    string retString = myStreamReader.ReadToEnd();
                    return retString;
                }
            }
        }

    }

}