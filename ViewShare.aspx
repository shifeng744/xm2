<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewShare.aspx.cs" Inherits="NavShare.ViewShare_aspx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta name="viewport" content="width:device-width, initial-scale=1, minimum-scale=1, maximum-scale=1, user-scalable=no" />
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>这是关于分享记录</title>
    <%-- Bootstrap --%>
    <link href="Css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/bootstrap.min.js" type="text/javascript"></script>
    <script src="Scripts/jquery-1.9.1.min.js" type="text/javascript"></script>
</head>
<body>
    <div class="row-fluid">
        <div class="span12">
            <h1>作者姓名：施锋  性别：男  班级：2班  学号：20150301208  爱好：乒乓球。这是viewshare.aspx页面</h1>
            <h3>分享记录</h3>
            <%-- 分享记录列表 --%>
            <div class="maincontentinner1" style="margin-left: 20px">
                    <div id="Div12" class="dataTables_wrapper">
                        <table id="page-nav-table" class="table table-bordered responsive dataTable">                           
                            <%-- 分享记录列表列名 --%>
                            <thead>
                                <tr>
                                    <th>
                                        页面地址
                                    </th>
                                    <th>
                                        分享到
                                    </th>
                                    <th>
                                        分享自openid
                                    </th>
                                    <th>
                                        上一级分享openid
                                    </th>
                                    <th>
                                        分享时间
                                    </th>
                                </tr>
                            </thead>
                            <tbody id="page-nav-table-body">
                                <%-- 一行一行生成分享记录列表 --%>
                                <% foreach (NavShare.Share entity in (ViewState["ShareList"] as List<NavShare.Share>))
                                   { %>
                                    <tr class="gradeX odd">
                                        <td>
                                            <%= entity.Url%>
                                        </td>
                                        <td class=" ">
                                            <%= entity.ShareTo%>
                                        </td>
                                        <td class=" ">
                                            <%= entity.ShareOpenId%>
                                        </td>
                                        <td class=" ">
                                            <%= entity.ParentShareOpenId%>
                                        </td>
                                        <td class=" ">
                                            <%= entity.ShareTime.ToString()%>
                                        </td>
                                    </tr>
                                <% } %>
                            </tbody>
                        </table>
                    </div>
            </div>
        </div>
    </div>
</body>
</html>
