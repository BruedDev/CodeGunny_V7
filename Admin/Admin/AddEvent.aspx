<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="AddEvent.aspx.cs" Inherits="WebApplication1.Admin.AddItem" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Panel ID="SearchPanel" runat="server">
        <div class="formPanel">
            <fieldset>
                <legend>新增项目</legend>
                <p>
                    <asp:Label ID="Label1" runat="server" Text="用户名" AssociatedControlID="UserName_tbx"></asp:Label>
                    <asp:TextBox ID="UserName_tbx" runat="server" CssClass="textEntry"></asp:TextBox>
                </p>
                <p>
                    <asp:Label ID="Label2" runat="server" Text="物品模板ID" AssociatedControlID="Template_tbx"></asp:Label>
                    <asp:TextBox ID="Template_tbx" runat="server" CssClass="textEntry"></asp:TextBox>
                </p>
                <p>
                    <asp:Label ID="Label3" runat="server" Text="强度等级" AssociatedControlID="Level_tbx"></asp:Label>
                    <asp:TextBox ID="Level_tbx" runat="server" CssClass="textEntry">0</asp:TextBox>
                </p>
                <p>
                    <asp:Label ID="Label4" runat="server" Text="攻击" 
                        AssociatedControlID="Attack_tbx"></asp:Label>
                    <asp:TextBox ID="Attack_tbx" runat="server" CssClass="textEntry">0</asp:TextBox>
                </p>
                <p>
                    <asp:Label ID="Label5" runat="server" Text="防御" 
                        AssociatedControlID="Defence_tbx"></asp:Label>
                    <asp:TextBox ID="Defence_tbx" runat="server" CssClass="textEntry">0</asp:TextBox>
                </p>
                <p>
                    <asp:Label ID="Label6" runat="server" Text="幸运" 
                        AssociatedControlID="Agility_tbx"></asp:Label>
                    <asp:TextBox ID="Agility_tbx" runat="server" CssClass="textEntry">0</asp:TextBox>
                </p>
                <p>
                    <asp:Label ID="Luck" runat="server" Text="敏捷" AssociatedControlID="Luck_tbx"></asp:Label>
                    <asp:TextBox ID="Luck_tbx" runat="server" CssClass="textEntry">0</asp:TextBox>
                </p>
                <p>
                    <asp:Label ID="Error_lbl" runat="server" Text="Label"></asp:Label>
                </p>
                <p>
                    <asp:Button ID="Edit" runat="server" Text="提交" OnClick="Edit_Click" />
                </p>
            </fieldset>
        </div>
    </asp:Panel>
</asp:Content>
