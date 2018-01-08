using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Text;
using Senparc.Weixin.MP.Context;
using Senparc.Weixin.MP.Entities;
using Senparc.Weixin.MP.MessageHandlers;
using System.Text.RegularExpressions;
using Weixin;
using Senparc.Weixin.MP.AdvancedAPIs;

namespace Sample_3
{
/// <summary>
    /// 自定义MessageHandler
    /// 把MessageHandler作为基类，重写对应请求的处理方法
    /// </summary>
    public partial class CustomMessageHandler : MessageHandler<MessageContext>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="inputStream"></param>
        /// <param name="maxRecordCount"></param>
        public CustomMessageHandler(Stream inputStream, int maxRecordCount = 0)
            : base(inputStream, maxRecordCount)
        {
            //这里设置仅用于测试，实际开发可以在外部更全局的地方设置，
            //比如MessageHandler<MessageContext>.GlobalWeixinContext.ExpireMinutes = 3。
            WeixinContext.ExpireMinutes = 3;
        }
        public override void OnExecuting()
        {
            //测试MessageContext.StorageData
            if (CurrentMessageContext.StorageData == null)
            {
                CurrentMessageContext.StorageData = 0;
            }
            base.OnExecuting();
        }
        public override void OnExecuted()
        {
            base.OnExecuted();
            CurrentMessageContext.StorageData = ((int)CurrentMessageContext.StorageData) + 1;
        }
        protected override IResponseMessageBase OnTextRequest(RequestMessageText requestMessage)
        {
            string ms1 = "viewnav";
            string ms2 = ms1.ToUpper();
            string ms3 = "viewshare";
            string ms4 = ms3.ToUpper();
            var responseMessage = CreateResponseMessage<ResponseMessageNews>();
            if (requestMessage.Content == ms1||requestMessage.Content==ms2)
            {
                Article a = new Article()
                {
                    PicUrl = "http://weixingongzhonghao-1.apphb.com/img/1.jpg",
                    Description = "点我跳转到ViewNav页面",
                    Url = "http://weixingongzhonghao-1.apphb.com/ViewNav.aspx",
                    Title = "我是访问记录"
                };
                responseMessage.Articles.Add(a);
                return responseMessage;
            }
            else if (requestMessage.Content == ms3||requestMessage.Content==ms4)
            {
                Article a= new Article()
                    {
                        PicUrl = "http://weixingongzhonghao-1.apphb.com/img/2.jpg",
                        Description = "点我跳转到ViewShare页面",
                        Url = "http://weixingongzhonghao-1.apphb.com/ViewShare.aspx",
                        Title = "点我试试"
                    };
                  responseMessage.Articles.Add(a);
                  return responseMessage;
              }
            else
            {
                Article a = new Article()
                {
                    PicUrl = "http://weixingongzhonghao-1.apphb.com/img/3.jpg",
                    Description = "点我跳转到NavshareIndex.aspx?=system页面",
                    Url = "http://weixingongzhonghao-1.apphb.com/NavShareIndex.aspx?s=system",
                    Title = "不错哟"
                };
                responseMessage.Articles.Add(a);
                return responseMessage;
            }
        }
        protected override IResponseMessageBase OnEvent_ScanRequest(RequestMessageEvent_Scan requestMessage)
        {
            var eventResponseMessage = base.OnEventRequest(requestMessage);
            return eventResponseMessage;
        }
        protected override IResponseMessageBase DefaultResponseMessage(IRequestMessageBase requestMessage)
        {     
            //所有没有被处理的消息会默认返回这里的结果
            return null;
        }
        protected override IResponseMessageBase OnEvent_ClickRequest(RequestMessageEvent_Click requestMessage)
        {
            IResponseMessageBase rm = null;
            switch (requestMessage.EventKey)
            {
                case "wxOpenID":
                    {
                        var msg = CreateResponseMessage<ResponseMessageNews>();
                        var r = User.Info(Wx.accessToken, requestMessage.FromUserName);
                        msg.Articles.Add(new Article { Description = string.Format("{0},作者姓名：施锋  性别：男  班级：2班  学号：20150301208  爱好：乒乓球。啊啊啊", r.nickname) });
                        msg.Articles.Add(new Article
                        {
                            Title = string.Format("你的微信号是{0}", r.openid),
                            PicUrl = r.headimgurl
                        });
                        rm = msg;
                    }
                    break;
            }
            return rm;
        }
        protected override IResponseMessageBase OnEvent_SubscribeRequest(RequestMessageEvent_Subscribe requestMessage)
        {
            var responseMessage = CreateResponseMessage<ResponseMessageText>();
            responseMessage.Content = @"作者姓名：施锋  性别：男  班级：2班  学号：20150301208  爱好：乒乓球。";
            return responseMessage;
        }
        protected override IResponseMessageBase OnEvent_UnsubscribeRequest(RequestMessageEvent_Unsubscribe requestMessage)
        {
            var responseMessage = base.CreateResponseMessage<ResponseMessageText>();
            responseMessage.Content = "再会";
            return responseMessage;
        }
    }
}

 
    