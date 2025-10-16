<%@ Page Title="Trang chủ" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="AdminGunny._Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
<script src="Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
<script src="Scripts/jquery.corner.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
        欢迎来到Only-The One
    </h2>
    <p>
      管理平台
    </p>
    <p>
   <ul>
     <li><a href="./Admin/sendMail5Item.aspx"> 发送一封信 </a></li>

     <li><a href="./Admin/Item.aspx"> 项目列表 </a></li>
	 
     <li><a href="./Admin/ItemFusionBox.aspx"> 编辑列表项 </a></li>
	 
	 <li><a href="./Admin/ItemFusionBox.aspx"> 管理地图 </a></li>
	 
	 <li><a href="./Admin/ItemFusionBox.aspx"> 管理人员 </a></li>
	 
	 <li><a href="./Admin/UserEdit.aspx"> 编辑成员 </a></li>
	
	 <li><a href="./Admin/ItemFusionBox.aspx"> 管理服务器 </a></li>
</ul>

    </p>
</asp:Content>
