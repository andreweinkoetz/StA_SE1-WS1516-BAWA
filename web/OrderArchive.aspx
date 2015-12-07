<%@ Page Title="" Language="C#" MasterPageFile="~/default_layout.Master" AutoEventWireup="true" CodeBehind="OrderArchive.aspx.cs" Inherits="web.OrderArchive_Code" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentBox" runat="server">
    <table style="width: 100%">
        <tr>
            <td style="text-align: left">
                <asp:Label ID="lblOrderArchive" runat="server" Text="Bestellungen archivieren" Font-Bold="true" Font-Size="X-Large"></asp:Label>
            </td>
            <td style="text-align: right">
                <asp:Button ID="btBack" runat="server" Text="Zurück" OnClick="btBack_Click" Height="50px" Width="100px" BackColor="#CF323D" ForeColor="White" Font-Bold="true" />
            </td>
        </tr>
    </table>
    <hr />
    <asp:GridView ID="gvOrderArchive" runat="server" AutoGenerateColumns="False" DataSourceID="FinishedOrders" Width="100%" OnSelectedIndexChanged="gvOrderArchive_SelectedIndexChanged">
        <Columns>
            <asp:CommandField ButtonType="Button" ShowSelectButton="True" />
            <asp:BoundField DataField="OrderDeliveryDate" HeaderText="Lieferzeitpunkt" SortExpression="OrderDeliveryDate" />
            <asp:BoundField DataField="OrderNumber" HeaderText="Bestellnummer" SortExpression="OrderNumber" />
            <asp:BoundField DataField="UserId" HeaderText="ID des Kunden" SortExpression="UserId" />
            <asp:CheckBoxField DataField="OrderDelivery" HeaderText="Lieferung?" SortExpression="OrderDelivery" />
            <asp:BoundField DataField="OrderStatusDescription" HeaderText="Status" SortExpression="OrderStatusDescription" />
            <asp:BoundField DataField="OrderSum" DataFormatString="{0:C}" HeaderText="Gesamtsumme" SortExpression="OrderSum" />
        </Columns>
    </asp:GridView>
    <asp:ObjectDataSource ID="FinishedOrders" runat="server" SelectMethod="GetFinishedOrders" TypeName="bll.clsOrderFacade"></asp:ObjectDataSource>
    <p>
        <asp:Button ID="btDownloadCSV" runat="server" Text="Download Report" Visible="false" OnClick="btDownloadCSV_Click" />
    </p>
    <p>
        <asp:Label ID="lblError" runat="server" Text="" ForeColor="Red"></asp:Label>
    </p>
</asp:Content>
