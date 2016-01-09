<%@ Page Title="" Language="C#" MasterPageFile="~/default_layout.Master" AutoEventWireup="true" CodeBehind="administration.aspx.cs" Inherits="web.Administration_Code" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .tbl-Style {
            text-align: center;
            margin-top: 20px;
        }

        .tbl-td-Style {
            text-align: left;
            vertical-align:top;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentBox" runat="server">
    <table style="width: 100%">
        <tr>
            <td class="auto-style1">
                <asp:Label ID="lblAdminOverview" runat="server" Text="Administration" Font-Size="X-Large" Font-Bold="true"></asp:Label>
                <p style="font-size:small">Administrationsbereich, ausschließlich für berechtigte Personen vorgesehen.</p>
            </td>
            <td style="text-align: right">
                <asp:Button ID="btLogout" runat="server" Text="Logout" OnClick="btLogout_Click" Height="50px" Width="100px" BackColor="#CF323D" ForeColor="White" Font-Bold="true" />
            </td>
        </tr>
    </table>

    <hr />

    <asp:Table ID="tblAdm" runat="server" CellSpacing="20" Width="100%" CssClass="tbl-Style">
        <asp:TableHeaderRow>
            <asp:TableHeaderCell>
                <asp:DropDownList ID="ddlistData" runat="server" Width="200px">
                    <asp:ListItem Value="0" Text="- Daten wählen"></asp:ListItem>
                    <asp:ListItem Value="1" Text="Produktdaten"></asp:ListItem>
                    <asp:ListItem Value="2" Text="Extradaten"></asp:ListItem>
                    <asp:ListItem Value="3" Text="Benutzerdaten"></asp:ListItem>
                    <asp:ListItem Value="4" Text="Größen"></asp:ListItem>
                </asp:DropDownList>
            </asp:TableHeaderCell>
            <asp:TableHeaderCell>
                <asp:DropDownList ID="ddlistOrders" runat="server" Width="200px">
                    <asp:ListItem Value="0" Text="- Daten wählen"></asp:ListItem>
                    <asp:ListItem Value="1" Text="Bestellungen verwalten"></asp:ListItem>
                    <asp:ListItem Value="2" Text="Bestellungen archivieren"></asp:ListItem>
                </asp:DropDownList>
            </asp:TableHeaderCell>
            <asp:TableHeaderCell>
            </asp:TableHeaderCell>
            <asp:TableHeaderCell></asp:TableHeaderCell>
        </asp:TableHeaderRow>
        <asp:TableRow>
            <asp:TableCell ColumnSpan="4" HorizontalAlign="Left">
                <asp:Label ID="lblErrorMsg" runat="server" Text="Bitte wählen Sie aus dem DropDown-Feld einen geeigneten Wert!" ForeColor="Red" Visible="false"></asp:Label>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell>
                <asp:Button ID="btAdmData" runat="server" Text="Stammdaten bearbeiten" Height="50px" Width="170px" BackColor="#CF323D" ForeColor="White" Font-Bold="true" OnClick="btAdmData_Click" />
            </asp:TableCell>
            <asp:TableCell>
                <asp:Button ID="btAdmOrders" runat="server" Text="Bestellungsverwaltung" Height="50px" Width="170px" BackColor="#CF323D" ForeColor="White" Font-Bold="true" OnClick="btAdmOrders_Click" />
            </asp:TableCell>
            <asp:TableCell>
                <asp:Button ID="btAdmStat" runat="server" Text="Auswertungen" Height="50px" Width="170px" BackColor="#CF323D" ForeColor="White" Font-Bold="true" OnClick="btAdmStat_Click" />
            </asp:TableCell>
            <asp:TableCell>
                <asp:Button ID="btAdmCoupons" runat="server" Text="Gutscheine verwalten" Height="50px" Width="170px" BackColor="#CF323D" ForeColor="White" Font-Bold="true" OnClick="btAdmCoupons_Click" />
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell CssClass="tbl-td-Style">
                <p><b>Stammdatenbearbeitung:</b> <br />Bearbeiten Sie hier alle Stammdaten.<br /><br />Es k&ouml;nnen neue angelegt und bestehende verändert werden. <br /><br />Sie haben die Auswahl zwischen Kunden, Produkten, Extras und Größen.</p>
            </asp:TableCell>
            <asp:TableCell CssClass="tbl-td-Style">
                <p><b>Bestellungen:</b><br />Verwalten Sie aktuelle Bestellungen oder archivieren Sie bereits abgeschlossene Bestellungen.<br /><br />Als Manager sind Sie außerdem in der Lage den Status, auch bereits abgeschlossener Bestellungen zu ändern.</p>
            </asp:TableCell>
            <asp:TableCell CssClass="tbl-td-Style">
                <p><b>Auswertungen:</b><br />Durch die Auswertungen erhalten Sie einen Überblick über die aktuelle wirtschaftliche Lage Ihres Unternehmens.<br /><br />Wählen Sie zwischen diversen Auswertungsmöglichkeiten und treffen Sie die richtigen Entscheidungen für Ihr Business.</p>
            </asp:TableCell>
            <asp:TableCell CssClass="tbl-td-Style">
                <p><b>Gutscheinverwaltung:</b><br />Verwalten Sie hier die Gutscheine. Sie können neue Gutscheine für Ihre Kunden erstellen, sowie bereits bestehende aktivieren und deaktivieren.<br /><br />Den Gutscheincode können Sie entweder selbst vergeben oder generieren lassen.<br />Die Höhe des Rabatts (in %) ist ebenfalls frei wählbar.</p>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>

</asp:Content>
