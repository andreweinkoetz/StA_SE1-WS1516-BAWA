<%@ Page Title="" Language="C#" MasterPageFile="~/Andre/default_layout.Master" AutoEventWireup="true" CodeBehind="OrderManagement.aspx.cs" Inherits="web.Andre.OrderManagement_Code" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentBox" runat="server">
    <table style="width: 100%">
        <tr>
            <td style="text-align: left">
                <asp:Label ID="lblOrderMgmt" runat="server" Text="Bestellverwaltung" Font-Bold="true" Font-Size="X-Large"></asp:Label>
            </td>
            <td style="text-align: right">
                <asp:Button ID="btLogout" runat="server" Text="Logout" OnClick="btLogout_Click" Height="50px" Width="100px" BackColor="#CF323D" ForeColor="White" Font-Bold="true" />
            </td>
        </tr>
    </table>

    <hr />

    <p>
        <asp:GridView ID="gvOrderMgmt" runat="server" AutoGenerateColumns="False" DataSourceID="getOrdersNotDelivered" Width="100%">
            <Columns>
                <asp:BoundField DataField="OrderNumber" HeaderText="Bestellnummer" SortExpression="OrderNumber" />
                <asp:BoundField DataField="UserName" HeaderText="Besteller" SortExpression="UserName" />
                <asp:BoundField DataField="OrderDate" HeaderText="Zeitpunkt der Bestellung" SortExpression="OrderDate" />
                <asp:BoundField DataField="OrderDeliveryDate" HeaderText="Zeitpunkt der Lieferung" SortExpression="OrderDeliveryDate" />
                <asp:BoundField DataField="OrderStatus" HeaderText="Bestellstatus" SortExpression="OrderStatus" />
                <asp:CheckBoxField DataField="OrderDelivery" HeaderText="Lieferung?" SortExpression="OrderDelivery" />
                <asp:BoundField DataField="OrderSum" HeaderText="Gesamtsumme" SortExpression="OrderSum" />
            </Columns>
        </asp:GridView>
        <asp:ObjectDataSource ID="getOrdersNotDelivered" runat="server" SelectMethod="getOrdersNotDelivered" TypeName="bll.clsOrderFacade"></asp:ObjectDataSource>
    </p>
    <asp:Table ID="statusTable" runat="server">
        <asp:TableHeaderRow>
            <asp:TableHeaderCell ColumnSpan="2">Beachte:</asp:TableHeaderCell>
        </asp:TableHeaderRow>
        <asp:TableRow><asp:TableCell>0 - </asp:TableCell><asp:TableCell>Bestellung eingegangen</asp:TableCell></asp:TableRow>
        <asp:TableRow><asp:TableCell>1 - </asp:TableCell><asp:TableCell>Bestellung in Arbeit</asp:TableCell></asp:TableRow>
        <asp:TableRow><asp:TableCell>2 - </asp:TableCell><asp:TableCell>Bestellung ausgeliefert</asp:TableCell></asp:TableRow>
        <asp:TableRow><asp:TableCell>3 - </asp:TableCell><asp:TableCell>Bestellung storniert</asp:TableCell></asp:TableRow>
    </asp:Table>
   
</asp:Content>
