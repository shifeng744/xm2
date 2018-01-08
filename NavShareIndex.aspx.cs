using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Runtime.Serialization.Json;//需添加System.Runtime.Serialization引用
using Weixin.JSSDK;
using Weixin;
namespace NavShare
{
    public partial class NavShareIndex : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string shareOpenId = Request.QueryString["s"];
            string navOpenId = Data.GetNavOpenId();
            AddNav(navOpenId, shareOpenId);
            ViewState["s"] = shareOpenId;
            ViewState["u"] = navOpenId;
            ClientScript.RegisterStartupScript(GetType(), "2", RegShare(), true);
        }
        void AddNav(string navOpenId, string shareOpenId)
        {            
            var pageNav = new Nav()
            {
                NavFrom = Data.GetNavFromType(),
                NavOpenId = navOpenId ?? "noknow",
                ShareOpenId = shareOpenId ?? "None",
                Url = "http://" + Request.Url.Host + Request.FilePath
            };
            BLL.InsertNav(pageNav);
        }

        string RegShare()
        {
            string url = Request.Url.AbsoluteUri.Replace(":" + Request.Url.Port, "");
            string link = "http://" + Request.Url.Host + Request.FilePath;
            link += "?s=" + ViewState["u"];
            RegJssdk.ShareEnitiy shareentity = new RegJssdk.ShareEnitiy()
            {           
                imgUrl = "http://mmbiz.qpic.cn/mmbiz_jpg/iajv1mr1yia0VbVbibDfmGdYh2fuMbN55cYFbW8ASm88OrJK1u7xcfopiaMLWTic7Rdac9roFjys9ibvUJRUqN4Oj7Bg/0",
                Title = "啊啊啊",
                Desc = "点点我",
                Link = link 
            };
            string jscode = RegJssdk.RegisterJssdk(Wx.appId, Wx.accessToken, url, shareentity);
            return jscode; 
        }        
    }
}