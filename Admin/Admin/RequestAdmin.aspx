<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="RequestAdmin.aspx.cs" Inherits="WebApplication1.Admin.RequestAdmin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <p>
        <asp:Label ID="Label1" runat="server" Text="点击此链接刷新顶部用户"></asp:Label>
         <asp:HyperLink  Target="_blank" ID="HyperLink1" runat="server">点击</asp:HyperLink>
		 <a href="http://49.212.220.154/requestii/CelebList/CreateAllceleb.ashx" >点击这里更新</a>
    </p>
    <p>
       
    </p>
</asp:Content>
