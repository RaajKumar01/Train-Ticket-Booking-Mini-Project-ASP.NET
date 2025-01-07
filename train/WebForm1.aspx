<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="train.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Label ID="Label1" runat="server" Text="username:"></asp:Label>
&nbsp;<input id="Text1" type="text" /><br />
        <br />
        <asp:Label ID="Label2" runat="server" Text="Password"></asp:Label>
&nbsp;<input id="Text2" type="text" /></div>
        <p>
            &nbsp;</p>
        <div>
<asp:Menu ID="Menu1" runat="server" Orientation="Horizontal" CssClass="menu">
    <Items>
        <asp:MenuItem Text="Home" NavigateUrl="~/Home.aspx" />
        <asp:MenuItem Text="About" NavigateUrl="~/About.aspx" />
        <asp:MenuItem Text="Services" NavigateUrl="~/Services.aspx" />
        <asp:MenuItem Text="Contact" NavigateUrl="~/Contact.aspx" />
    </Items>
</asp:Menu>
        </div>
    </form>
</body>
</html>
