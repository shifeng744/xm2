using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Senparc.Weixin.MP.AdvancedAPIs;
using Senparc.Weixin.MP.Helpers;
using Senparc.Weixin.MP.Entities;
namespace Weixin
{
    public class Group2
    {
        public static WxJsonResult Delete(string accessToken, int GroupId)
        {
            var urlFormat = "https://api.weixin.qq.com/cgi-bin/groups/delete?access_token={0}";
            var data = new
            {
                group = new
                {
                    id = GroupId
                }
            };
            return ApiHelper.Post<WxJsonResult>(accessToken, urlFormat, data);            
        }

    }
}