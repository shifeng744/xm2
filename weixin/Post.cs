/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc

    文件名：Post.cs
    文件功能描述：Post


    创建标识：Senparc - 20150211

    修改标识：Senparc - 20150303
    修改描述：整理接口

    修改标识：Senparc - 20150312
    修改描述：开放代理请求超时时间

    修改标识：zhanghao-kooboo - 20150316
    修改描述：增加

    修改标识：Senparc - 20150407
    修改描述：发起Post请求方法修改，为了上传永久视频素材
 
    修改标识：Senparc - 20160720
    修改描述：增加了PostFileGetJsonAsync的异步方法（与之前的方法多一个参数）
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using Senparc.Weixin.MP.Entities;
using Senparc.Weixin.MP.Exceptions;

namespace Senparc.Weixin.MP.HttpUtility
{
    public static class Post
    {
        /// <summary>
        /// 使用Post方法上传数据并下载文件或结果
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="stream"></param>
        public static void Download(string url, string data, Stream stream)
        {
            WebClient wc = new WebClient();
            var file = wc.UploadData(url, "POST", Encoding.UTF8.GetBytes(string.IsNullOrEmpty(data) ? "" : data));
            foreach (var b in file)
            {
                stream.WriteByte(b);
            }
        }               
    }
}
