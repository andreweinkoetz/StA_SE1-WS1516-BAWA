<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="user-aendern1.aspx.cs" Inherits="web.Beispiele.user_aendern1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>RateIt - User ändern</title>
       <link rel="stylesheet" type="text/css" href="..\StyleSheet.css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <h1>Ändern User </h1>
        <br /><br />
        </div>
    <table>
        <tr>
            <td>
                Name:
            </td>
            <td>
                <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Adresse:
            </td>
            <td>
                <asp:TextBox ID="txtAddress" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Distanz:</td>
            <td>
                <asp:TextBox ID="txtDistanz" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Rolle:</td>
            <td>
                <asp:RadioButtonList ID="rblRole" runat="server">
                    <asp:ListItem Value="0">Kunde</asp:ListItem>
                    <asp:ListItem Value="1">Manager</asp:ListItem>
                    <asp:ListItem Value="2">Servicepersonal</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td>
                Aktiv:</td>
            <td>
                <asp:CheckBox ID="chkIsActive" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                Passwort:</td>
            <td>
                <asp:TextBox ID="txtPassword" runat="server"></asp:TextBox>
            </td>
        </tr>

    </table>
        <p>
        <asp:Button ID="btnUpdate" runat="server" OnClick="btnUpdate_Click" Text="Speichern" />
    </p>
    <p>
        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="Default.aspx">Home</asp:HyperLink>
    </p>
    <p>
        <asp:Label ID="lblMsg" runat="server" Style="font-family: Arial, Helvetica, sans-serif;
            color: #993300"></asp:Label>
    </p><div class="style4">
        <hr />
        <br />
        Implementierung mittels Code behind: <b>btnUpdate_Click()</b><br />
            </div>
    </form>
</body>
</html>
