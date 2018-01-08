using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Senparc.Weixin.MP.CommonAPIs;
using Senparc.Weixin.MP.AdvancedAPIs;
using Senparc.Weixin.MP.Entities.Menu;
using Weixin;

namespace NavShare
{
    public partial class menuManage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ButtonGroup bg = new ButtonGroup();
            bg.button.Add(new SingleClickButton { name = "关于类", key = "wxOpenID", type = "click"  });
            bg.button.Add(new SingleViewButton { name = "新闻类", url = "http://weixingongzhonghao-1.apphb.com/sendnews.aspx" });
            var bg2 = new SubButton() { name = "更多类" };//二级菜单
            bg2.sub_button.Add(new SingleViewButton { name = "打开NavShareIndex", url = "http://weixingongzhonghao-1.apphb.com/NavShareIndex.aspx?s=system" });
            bg2.sub_button.Add(new SingleViewButton { name = "点击分享", url = "http://weixingongzhonghao-1.apphb.com/ViewShare.aspx" });
            bg.button.Add(bg2);
            var r = Meun.CreateMenu(Wx.accessToken, bg);
            if (r.errcode == 0)
                Response.Write("创建菜单成功");
            else
                Response.Write("创建菜单失败");
        }
    }
}