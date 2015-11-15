<%@ Page Title="" Language="C#" MasterPageFile="~/Andre/default_layout.Master" AutoEventWireup="true" CodeBehind="RegistryPage.aspx.cs" Inherits="web.RegistryPage" %>

<asp:Content ID="Content2" ContentPlaceHolderID="contentBox" runat="server">
    <p>
        <asp:Label ID="header" runat="server" Text="Registrierung" Font-Bold="true" Font-Size="Large"></asp:Label>
    </p>
    <p>
        <asp:Label ID="welcomeText" runat="server" Text="Herzlich Willkommen bei Pizza Pizza München. Wir freuen uns Sie als Kunden begrüßen zu dürfen!" Font-Size="Medium" Font-Italic="true"></asp:Label>
    </p>
    <br />
    <hr />
    <br />
    <p>
        <asp:Label ID="personalInformation" runat="server" Text="Persönliche Daten" Font-Bold="true" Font-Size="Large"></asp:Label>
    </p>
    <p>
        <asp:Label ID="required" runat="server" Text="*=Pflichtfelder" Font-Size="Small"></asp:Label>
    </p>
    <br />
    <asp:Table ID="tblRegistry" runat="server" CellSpacing="10" >
        <asp:TableRow>
            <asp:TableCell> Anrede* </asp:TableCell>
            <asp:TableCell>
                    <asp:DropDownList runat="server" Width="70">
                        <asp:ListItem> - </asp:ListItem>
                        <asp:ListItem> Herrn </asp:ListItem>
                        <asp:ListItem> Frau </asp:ListItem>
                    </asp:DropDownList>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell> Name* </asp:TableCell>
            <asp:TableCell >
                <asp:TextBox runat="server" placeholder="Name" ID="txtBoxName" Width="150"></asp:TextBox>
                <asp:TextBox runat="server" placeholder="Vorname" ID="txtBoxVorname" Width="150"></asp:TextBox>
                <asp:Label ID="lblErrorName" runat="server" Text="" ForeColor="Red"></asp:Label>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell> Adresse* </asp:TableCell>
            <asp:TableCell>
                <asp:TextBox runat="server" placeholder="Straße" ID="txtBoxStraße" Width="250"></asp:TextBox>
                <asp:TextBox runat="server" placeholder="Nr." ID="txtBoxHnr" Width="50"></asp:TextBox>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell></asp:TableCell>
            <asp:TableCell>
                <asp:TextBox runat="server" placeholder="PLZ" ID="txtBoxPLZ" Width="80"></asp:TextBox>
                <asp:TextBox runat="server" placeholder="Ort" ID="txtBoxPlace" Width="220"></asp:TextBox>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell runat="server"> E-Mail* </asp:TableCell>
            <asp:TableCell>
                <asp:TextBox runat="server" placeholder="E-Mail" ID="txtBoxEmail" Width="300"></asp:TextBox>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell> Telefon* </asp:TableCell>
            <asp:TableCell>
                <asp:TextBox runat="server" placeholder="Telefon" ID="txtBoxPhone" Width="300"></asp:TextBox>
            </asp:TableCell>
        </asp:TableRow>  
        <asp:TableRow>
            <asp:TableCell></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell> Passwort* </asp:TableCell>
            <asp:TableCell>
                <asp:TextBox runat="server" placeholder="Passwort" ID="txtBoxPassword" Width="300" TextMode="Password"></asp:TextBox>
                <asp:Label ID="lblErrorPwd" runat="server" Text="" ForeColor="Red"></asp:Label>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell> Passwort bestätigen* </asp:TableCell>
            <asp:TableCell>
                <asp:TextBox runat="server" placeholder="Passwort bestätigen" ID="txtBoxPaswordx2" Width="300" TextMode="Password"></asp:TextBox>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell></asp:TableCell>
            <asp:TableCell>
                <asp:Button runat="server" Text="Senden" ID="btnSubmit" OnClick="btnSubmit_Click" />
            </asp:TableCell>
        </asp:TableRow>    
    </asp:Table>
</asp:Content>

