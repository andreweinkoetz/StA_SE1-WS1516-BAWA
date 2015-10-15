<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="session1.aspx.cs" Inherits="web.Beispiele.session1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" type="text/css" href="..\StyleSheet.css" />
    <title>Rate It!</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
             <h1>Beispiel für Session-Handling</h1>
   
    </div>
        <p>
            <asp:Label ID="lblUser" runat="server" Text="..."></asp:Label>
        </p>
        <p>
            Hier können Sie sich (ggf. auch als jemand anderes) einloggen:</p>
        <p>
            <asp:DropDownList ID="ddlUsers" runat="server" DataSourceID="odsUsers" DataTextField="Name" DataValueField="ID">
            </asp:DropDownList>
&nbsp;
            <asp:Button ID="btnLogin" runat="server" OnClick="btnLogin_Click" Text="Login" />
&nbsp;<asp:ObjectDataSource ID="odsUsers" runat="server" SelectMethod="getAllUsers" TypeName="bll.clsUserCollection"></asp:ObjectDataSource>
        </p>
        <p>
            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Beispiele/session2.aspx">Weiter</asp:HyperLink>
        </p>
        <p>
            <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/Beispiele/default.aspx">Home</asp:HyperLink>
        </p>
    </form>
</body>
</html>
