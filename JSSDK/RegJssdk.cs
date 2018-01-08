using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
namespace Weixin.JSSDK
{
    public class RegJssdk
    {
        public static string RegisterJssdk(string appid,string accessToken,string url,ShareEnitiy shareentity)
        {
            long timestamp = JSSDKHelper.GetTimestamp();
            string nonceStr = JSSDKHelper.GetNoncestr();
            string jsapiTicket = JSSDKHelper.GetTicket(accessToken);
            string signature = JSSDKHelper.GetSignature(jsapiTicket, nonceStr, timestamp, url);

            StringBuilder sbjsApi = new StringBuilder();
            //sbjsApi.Append("<script>");

            //通过config接口注入权限验证配置
            sbjsApi.Append("wx.config({debug:false,");
            sbjsApi.Append("appId: '" + appid + "',");
            sbjsApi.Append("timestamp: " + timestamp + ",");
            sbjsApi.Append("nonceStr: '" + nonceStr + "',");
            sbjsApi.Append("signature: '" + signature + "',");
            sbjsApi.Append("jsApiList: ['onMenuShareTimeline', 'onMenuShareAppMessage', 'chooseImage', 'uploadImage', 'getNetworkType']});");

            //通过ready接口处理成功验证
            sbjsApi.Append("wx.ready(on_weixin_ready);");
            sbjsApi.Append("function on_weixin_ready() {weixin_share();}");

            //获取“分享给朋友”按钮点击状态及自定义分享内容接口
            sbjsApi.Append("function weixin_share(){");
            sbjsApi.Append("wx.onMenuShareAppMessage({");
            sbjsApi.Append("title:'"+shareentity.Title+"',");  
            sbjsApi.Append("desc:'"+shareentity.Desc+"',");
            sbjsApi.Append("link:'"+shareentity.Link+"',");
            sbjsApi.Append("imgUrl:'"+shareentity.imgUrl+"',");
            sbjsApi.Append("type:'link',");
            sbjsApi.Append("dataUrl:'',");
            sbjsApi.Append("success: function (res) {friendcallback(res);},");
            sbjsApi.Append("cancel:function () {}");
            sbjsApi.Append("});");

            //获取“分享到朋友圈”按钮点击状态及自定义分享内容接口
            sbjsApi.Append("wx.onMenuShareTimeline({");
            sbjsApi.Append("title:'" + shareentity.Title + "',");
            sbjsApi.Append("desc:'" + shareentity.Desc + "',");
            sbjsApi.Append("link:'" + shareentity.Link + "',");
            sbjsApi.Append("imgUrl:'" + shareentity.imgUrl + "',");
            //sbjsApi.Append("success: function (res) { alert('分享成功');},");            
            sbjsApi.Append("success: function (res) { friendCirclecallback(res);},");            
            sbjsApi.Append("cancel:function () {}");
            sbjsApi.Append("});}");

            //sbjsApi.Append("</script>");
            return sbjsApi.ToString();
        }
        public class ShareEnitiy
        {
            public string Title="";
            public string Desc="";
            public string Link="";
            public string imgUrl="";

        }
    }
}