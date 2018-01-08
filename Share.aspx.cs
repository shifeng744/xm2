using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NavShare
{
    public partial class Share1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string typeStr = Request["type"];
            string url = Request["url"];
            Uri uri = new Uri(url);
            url = "http://" + uri.Host + uri.AbsolutePath;
            if (!string.IsNullOrEmpty(typeStr))
            {
                var pageShare = new Share()
                {
                    Url = url,
                    ShareOpenId = Request["u"] ?? "None",
                    ParentShareOpenId = Request["s"] ?? "None",
                    ShareTo = typeStr
                };
                BLL.InsertShare(pageShare);
            }


        }
    }
}