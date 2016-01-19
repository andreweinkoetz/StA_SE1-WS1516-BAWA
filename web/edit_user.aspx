<%@ Page Title="" Language="C#" MasterPageFile="~/default_layout.Master" AutoEventWireup="true" CodeBehind="edit_user.aspx.cs" Inherits="web.EditUser_Code" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentBox" runat="server">
    <table style="width: 100%">
        <tr>
            <td style="text-align: left">
                <asp:Label ID="lblUserEdit" runat="server" Text="" Font-Size="Larger"></asp:Label>
            </td>
            <td style="text-align: right">
                <asp:Button ID="btBack" runat="server" Text="Zurück" OnClick="btBack_Click" Height="50px" Width="100px" BackColor="#CF323D" ForeColor="White" Font-Bold="true" />
            </td>
        </tr>
    </table>
    <hr />
    <asp:Panel runat="server" DefaultButton="btEnter">
        <asp:Table ID="tblRegistry" runat="server" CellSpacing="10">
            <asp:TableRow>
                <asp:TableCell runat="server"> User-Id </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox runat="server" ID="txtBoxId" Width="50" BorderStyle="Ridge" borderwith="2" Enabled="false"></asp:TextBox>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell runat="server"> E-Mail/Username </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox runat="server" placeholder="E-Mail/Username" ID="txtBoxEmail" Width="300" BorderStyle="Ridge" borderwith="2"></asp:TextBox>
                    <asp:Label ID="lblErrorEmail" runat="server" Text="" ForeColor="Red"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell> Anrede </asp:TableCell>
                <asp:TableCell>
                    <asp:DropDownList ID="ddlTitle" runat="server" Width="70">
                        <asp:ListItem Value="Herr">Herr</asp:ListItem>
                        <asp:ListItem Value="Frau">Frau</asp:ListItem>
                    </asp:DropDownList>
                    <asp:Label ID="lblTitleError" runat="server" Text="" ForeColor="Red"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell> Name </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox runat="server" placeholder="Name" ID="txtBoxName" Width="150" BorderStyle="Ridge" borderwith="2"></asp:TextBox>
                    <asp:TextBox runat="server" placeholder="Vorname" ID="txtBoxVorname" Width="150" BorderStyle="Ridge" borderwith="2"></asp:TextBox>
                    <asp:Label ID="lblErrorName" runat="server" Text="" ForeColor="Red"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell> Adresse </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox runat="server" placeholder="Straße" ID="txtBoxStraße" Width="250" BorderStyle="Ridge" borderwith="2"></asp:TextBox>
                    <asp:TextBox runat="server" placeholder="Nr." ID="txtBoxHnr" Width="50" BorderStyle="Ridge" borderwith="2"></asp:TextBox>
                    <asp:Label ID="lblErrorStreet" runat="server" Text="" ForeColor="Red"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell></asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox runat="server" placeholder="PLZ" ID="txtBoxPLZ" Width="80" BorderStyle="Ridge" borderwith="2"></asp:TextBox>
                    <asp:TextBox runat="server" placeholder="Ort" ID="txtBoxPlace" Width="220" BorderStyle="Ridge" borderwith="2"></asp:TextBox>
                    <asp:Label ID="lblErrorPlace" runat="server" Text="" ForeColor="Red"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell> Telefon </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox runat="server" placeholder="Telefon" ID="txtBoxPhone" Width="300" BorderStyle="Ridge" borderwith="2"></asp:TextBox>
                    <asp:Label ID="lblErrorPhone" runat="server" Text="" ForeColor="Red"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="lblUserRole" runat="server" Text="Rolle"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:DropDownList ID="ddlUserRole" runat="server">
                        <asp:ListItem Value="3" Text="Kunde"></asp:ListItem>
                        <asp:ListItem Value="2" Text="Service-Mitarbeiter"></asp:ListItem>
                        <asp:ListItem Value="1" Text="Administrator"></asp:ListItem>
                    </asp:DropDownList>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="lblActive" runat="server" Text="Aktiv?"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:CheckBox ID="chkActive" runat="server" />
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="changePwChkRow">
                <asp:TableCell>
                    <asp:Label ID="lblChangePassword" runat="server" Text="Passwort ändern"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:CheckBox ID="chkChangePassword" runat="server" AutoPostBack="true" OnCheckedChanged="chkChangePassword_CheckedChanged" />
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="pwRow" Visible="false">
                <asp:TableCell> Neues Passwort </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox runat="server" placeholder="Passwort" ID="txtBoxPassword" Width="300" TextMode="Password" BorderStyle="Ridge" borderwith="2"></asp:TextBox>
                    <asp:Label ID="lblErrorPwd" runat="server" Text="" ForeColor="Red"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="pwRow2" Visible="false">
                <asp:TableCell> Passwort bestätigen </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox runat="server" placeholder="Passwort bestätigen" ID="txtBoxPasswordx2" Width="300" TextMode="Password" BorderStyle="Ridge" borderwith="2"></asp:TextBox>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Button runat="server" Text="" ID="btEnter" OnClick="btEnter_Click" Height="30px" Width="200px" BackColor="#CF323D" ForeColor="White" Font-Bold="true" Visible="true" />
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Button runat="server" Text="Benutzer löschen" ID="btDelete" OnClick="btDelete_Click" Height="30px" Width="200px" BackColor="#CF323D" ForeColor="White" Font-Bold="true" Enabled="false" />
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        <p>
            <asp:Label ID="lblError" runat="server" Text=""></asp:Label>
        </p>
    </asp:Panel>
    <hr />
    <p>
        <asp:Label ID="lblOpenOrders" runat="server" Text="" Font-Size="X-Small"></asp:Label>
    </p>
</asp:Content>
