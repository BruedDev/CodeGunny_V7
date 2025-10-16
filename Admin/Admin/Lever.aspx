<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Lever.aspx.cs" Inherits="Lever" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Nâng Lever</title>
</head>
<body>
<style>
body{
background:url(gtheme_files/main.jpg)
}
</style>
    <form id="form1" runat="server">
    <center>
	<br>
	<br>
	<br>
	<br>
	<asp:HyperLink ID="HyperLink2" runat="server" Font-Size=Large
                                             ForeColor="blue"> Tool Nâng Lever </asp:HyperLink><br>
	<br>
	
	<strong>
	<div>
    
        确认密码:<asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
        <br />
		<br />
        名称增加 ( 游戏名字 ):<asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
        <br />
		<br />
        <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="升级" />
        <br />
		<br />
        <asp:Label ID="Label1" runat="server" Text="Make by Boo Bjn from Kaka"></asp:Label>
        <br />
				* 注意：此功能前，请务必退出游戏 ^^!<br>
					Pass là Kaka nhé ^^!
	
		
		
    
    </div></center>
    </form>
</body>
</html>
