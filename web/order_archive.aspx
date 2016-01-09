<%@ Page Title="" Language="C#" MasterPageFile="~/default_layout.Master" AutoEventWireup="true" CodeBehind="order_archive.aspx.cs" Inherits="web.OrderArchive_Code" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentBox" runat="server">
    <table style="width: 100%">
        <tr>
            <td style="text-align: left">
                <asp:Label ID="lblOrderArchive" runat="server" Text="Bestellungen archivieren" Font-Bold="true" Font-Size="X-Large"></asp:Label>
                <p style="font-size:small">Bestellarchivierung, ausschließlich für berechtigte Personen vorgesehen.</p>
            </td>
            <td style="text-align: right">
                <asp:Button ID="btBack" runat="server" Text="Zurück" OnClick="btBack_Click" Height="50px" Width="100px" BackColor="#CF323D" ForeColor="White" Font-Bold="true" />
            </td>
        </tr>
    </table>
    <hr />
    <br />
    <asp:GridView ID="gvOrderArchive" runat="server" AutoGenerateColumns="False" DataSourceID="FinishedOrders" Width="100%" OnSelectedIndexChanged="gvOrderArchive_SelectedIndexChanged">
        <Columns>
            <asp:CommandField ButtonType="Image" ShowSelectButton="true" SelectImageUrl="~/img/archive_icon.png" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="OrderDeliveryDate" HeaderText="Lieferzeitpunkt" SortExpression="OrderDeliveryDate" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="OrderNumber" HeaderText="Bestellnummer" SortExpression="OrderNumber" ItemStyle-HorizontalAlign="Center"  />
            <asp:BoundField DataField="UserId" HeaderText="ID des Kunden" SortExpression="UserId" ItemStyle-HorizontalAlign="Center"  />
            <asp:CheckBoxField DataField="OrderDelivery" HeaderText="Lieferung?" SortExpression="OrderDelivery" ItemStyle-HorizontalAlign="Center"  />
            <asp:BoundField DataField="OrderStatusDescription" HeaderText="Status" SortExpression="OrderStatusDescription" ItemStyle-HorizontalAlign="Center"  />
            <asp:BoundField DataField="OrderSum" DataFormatString="{0:C}" HeaderText="Gesamtsumme" SortExpression="OrderSum" ItemStyle-HorizontalAlign="Center"  />
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
