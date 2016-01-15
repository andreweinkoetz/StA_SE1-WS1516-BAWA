<%@ Page Title="" Language="C#" MasterPageFile="~/default_layout.Master" AutoEventWireup="true" CodeBehind="registry_page.aspx.cs" Inherits="web.RegistryPage" %>

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
    <asp:Table ID="tblRegistry" runat="server" CellSpacing="10">
        <asp:TableRow>
            <asp:TableCell> Anrede* </asp:TableCell>
            <asp:TableCell>
                <asp:DropDownList ID="ddlTitle" runat="server" Width="70">
                    <asp:ListItem> - </asp:ListItem>
                    <asp:ListItem>Herr</asp:ListItem>
                    <asp:ListItem>Frau</asp:ListItem>
                </asp:DropDownList>
                <asp:Label ID="lblTitleError" runat="server" Text="" ForeColor="Red"></asp:Label>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell> Name* </asp:TableCell>
            <asp:TableCell>
                <p>
                    <asp:TextBox runat="server" placeholder="Name" ID="txtBoxName" Width="150" BorderStyle="Ridge" borderwith="2"></asp:TextBox>
                    <asp:TextBox runat="server" placeholder="Vorname" ID="txtBoxVorname" Width="150" BorderStyle="Ridge" borderwith="2"></asp:TextBox>
                    <asp:Label ID="lblErrorPrename" runat="server" Text="" ForeColor="Red" Visible="false"></asp:Label>
                </p>
                <p>
                    <asp:Label ID="lblErrorName" runat="server" Text="" ForeColor="Red" Visible="false"></asp:Label>
                </p>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell> Adresse* </asp:TableCell>
            <asp:TableCell>
                <p>
                    <asp:TextBox runat="server" placeholder="Straße" ID="txtBoxStraße" Width="250" BorderStyle="Ridge" borderwith="2"></asp:TextBox>
                    <asp:TextBox runat="server" placeholder="Nr." ID="txtBoxHnr" Width="50" BorderStyle="Ridge" borderwith="2"></asp:TextBox>
                    <asp:Label ID="lblErrorStreetNr" runat="server" Text="" ForeColor="Red" Visible="false"></asp:Label>
                </p>
                <p>
                    <asp:Label ID="lblErrorStreet" runat="server" Text="" ForeColor="Red" Visible="false"></asp:Label>
                </p>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell></asp:TableCell>
            <asp:TableCell>
                <p>
                    <asp:TextBox runat="server" placeholder="PLZ" ID="txtBoxPLZ" Width="80" BorderStyle="Ridge" borderwith="2"></asp:TextBox>
                    <asp:TextBox runat="server" placeholder="Ort" ID="txtBoxPlace" Width="220" BorderStyle="Ridge" borderwith="2"></asp:TextBox>
                    <asp:Label ID="lblErrorPlace" runat="server" Text="" ForeColor="Red" Visible="false"></asp:Label>
                </p>
                <p>
                    <asp:Label ID="lblErrorPLZ" runat="server" Text="" ForeColor="Red" Visible="false"></asp:Label>
                </p>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell> Telefon* </asp:TableCell>
            <asp:TableCell>
                <asp:TextBox runat="server" placeholder="Telefon" ID="txtBoxPhone" Width="300" BorderStyle="Ridge" borderwith="2"></asp:TextBox>
                <asp:Label ID="lblErrorPhone" runat="server" Text="" ForeColor="Red" Visible="false"></asp:Label>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell runat="server"> E-Mail* </asp:TableCell>
            <asp:TableCell>
                <asp:TextBox runat="server" placeholder="E-Mail" ID="txtBoxEmail" Width="300" BorderStyle="Ridge" borderwith="2"></asp:TextBox>
                <asp:Label ID="lblErrorEmail" runat="server" Text="" ForeColor="Red" Visible="false"></asp:Label>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell> Passwort* </asp:TableCell>
            <asp:TableCell>
                <asp:TextBox runat="server" placeholder="Passwort" ID="txtBoxPassword" Width="300" TextMode="Password" BorderStyle="Ridge" borderwith="2"></asp:TextBox>
                <asp:Label ID="lblErrorPwdx1" runat="server" Text="" ForeColor="Red" Visible="false"></asp:Label>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell> Passwort bestätigen* </asp:TableCell>
            <asp:TableCell>
                <asp:TextBox runat="server" placeholder="Passwort bestätigen" ID="txtBoxPasswordx2" Width="300" TextMode="Password" BorderStyle="Ridge" borderwith="2"></asp:TextBox>
                <asp:Label ID="lblErrorPwdx2" runat="server" Text="" ForeColor="Red" Visible="false"></asp:Label>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell></asp:TableCell>
            <asp:TableCell>
                <asp:Button runat="server" Text="Senden" ID="btnSubmit" OnClick="btnSubmit_Click" />
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
    <asp:Label ID="lblUserDouble" runat="server" Text="E-Mail-Adresse bereits im System vorhanden. Bitte wählen Sie eine andere E-Mail-Adresse." ForeColor="Red" Visible="false"></asp:Label>
</asp:Content>
