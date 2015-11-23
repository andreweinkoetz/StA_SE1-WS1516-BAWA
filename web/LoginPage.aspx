<%@ Page Title="Login" Language="C#" MasterPageFile="~/default_layout.Master" AutoEventWireup="true" CodeBehind="LoginPage.aspx.cs" Inherits="web.LoginPage" %>

<asp:Content ID="Content2" ContentPlaceHolderID="contentBox" runat="server">
    <p>
        <asp:Label ID="header" runat="server" Text="Login" Font-Bold="true" Font-Size="Large"></asp:Label>
    </p>
    <p>
        <asp:Label ID="welcomeTextLogin" runat="server" Text="Herzlich Willkommen bei Pizza Pizza München." Font-Size="Medium" Font-Italic="true"></asp:Label>
    </p>
    <br />
    <hr />
    <p>
     Sie bestellen zum ersten Mal bei uns? Registrieren Sie sich <a href="RegistryPage.aspx">hier</a>!
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
                <asp:CheckBox runat="server" ID="chkClear" OnCheckedChanged ="chkBoxClear_Click" AutoPostBack="true" />
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
     <asp:Label ID="lblTest" runat="server" Text=""></asp:Label>        
</asp:Content>



