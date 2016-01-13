<%@ Page Title="Login" Language="C#" MasterPageFile="~/default_layout.Master" AutoEventWireup="true" CodeBehind="login_page.aspx.cs" Inherits="web.LoginPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .p-login {
            margin-top: 20px;
        }
    </style>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="contentBox" runat="server">
    <p>
        <asp:Label ID="header" runat="server" Text="Login" Font-Bold="true" Font-Size="Large"></asp:Label>
    </p>
    <p>
        <asp:Label ID="welcomeTextLogin" runat="server" Text="Herzlich willkommen bei Pizza Pizza München." Font-Size="Medium" Font-Italic="true"></asp:Label>
    </p>
    <br />
    <hr />
    <p>
        Sie bestellen zum ersten Mal bei uns? Registrieren Sie sich <a href="registry_page.aspx">hier</a>!
    </p>
    <br />
    <p>
        <asp:Label ID="loginData" runat="server" Text="Anmeldedaten" Font-Bold="true" Font-Size="Large"></asp:Label>
    </p>
    <asp:Table ID="tblLogin" runat="server">
        <asp:TableRow>
            <asp:TableCell> Benutzername </asp:TableCell>
            <asp:TableCell>
                <asp:TextBox runat="server" placeholder="Benutzername" ID="txtBoxUsername" Width="150"></asp:TextBox>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell> Passwort </asp:TableCell>
            <asp:TableCell>
                <asp:TextBox runat="server" placeholder="Passwort" ID="txtBoxPassword" Width="150" TextMode="Password"></asp:TextBox>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell></asp:TableCell>
            <asp:TableCell>
                <asp:CheckBox runat="server" ID="chkClear" OnCheckedChanged="chkBoxClear_Click" AutoPostBack="true" />
                <asp:Label ID="lblResult" runat="server" Text="Klartext anzeigen"></asp:Label>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell></asp:TableCell>
            <asp:TableCell>
                <asp:Button runat="server" Text="Bestätigen" ID="btnLogin" OnClick="btnLogin_Click" />
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
    <asp:Label ID="lblInactiveUser" runat="server" Text=""></asp:Label>
    <p class="p-login"><u>Zugänge lauten:</u></p>
    <table style="width: 600px">
        <tr>
            <td><b>Username</b></td>
            <td><b>Password</b></td>
            <td><b>Role</b></td>
            <td><b>Übernehmen</b></td>
        </tr>
        <tr>
            <td>admin@pizzapizza.de</td>
            <td>admin</td>
            <td>Administrator</td>
            <td>
                <asp:Button ID="btadmin" runat="server" Text="Admin" OnClick="btadmin_Click" Height="35px" Width="100px" /></td>
        </tr>
        <tr>
            <td>service@pizzapizza.de</td>
            <td>admin</td>
            <td>Service-Mitarbeiter</td>
            <td>
                <asp:Button ID="btservice" runat="server" Text="Service" OnClick="btservice_Click" Height="35px" Width="100px" /></td>
        </tr>
        <tr>
            <td>kunde@pizzapizza.de</td>
            <td>admin</td>
            <td>Kunde</td>
            <td>
                <asp:Button ID="btkunde" runat="server" Text="Kunde" OnClick="btkunde_Click" Height="35px" Width="100px" /></td>
        </tr>
    </table>
    <p class="p-login">Für den Gastzugang wählen Sie einfach die gewünschte Seite.</p>
</asp:Content>



