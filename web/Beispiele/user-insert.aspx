<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="user-insert.aspx.cs" Inherits="web.beispiele.user_insert" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" type="text/css" href="..\StyleSheet.css" />
    <title>Pizza Pizza! - Insert User</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    <div>
    
        <h1>Einfügen in User Tabelle
        </h1>
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
                Rolle:</td>
            <td>
                <asp:RadioButtonList ID="rblRolle" runat="server">
                    <asp:ListItem Selected="True" Value="0">Kunde</asp:ListItem>
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
        <asp:Button ID="btnInsert" runat="server" OnClick="btnInsert_Click" Text="Einfügen" />
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
        Implementierung mittels Code behind: <b>btnInsert_Click()</b><br />
            </div>
    </div>
    </form>
</body>
</html>
