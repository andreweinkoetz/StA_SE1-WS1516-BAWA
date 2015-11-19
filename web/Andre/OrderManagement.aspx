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
        <asp:GridView ID="gvOrderMgmt" runat="server" AutoGenerateColumns="False" DataSourceID="getOrdersNotDelivered" Width="100%" OnSelectedIndexChanged="gvOrderMgmt_SelectedIndexChanged">
            <SelectedRowStyle BorderColor="Red" BorderStyle="Solid" BorderWidth="3px" />
            <RowStyle BorderStyle="None" />
            <Columns>
                <asp:CommandField ShowSelectButton="True" ButtonType="Button" SelectText="Wählen" />
                <asp:BoundField DataField="OrderNumber" HeaderText="Bestellung#" SortExpression="OrderNumber" />
                <asp:BoundField DataField="UserName" HeaderText="Kunde" SortExpression="UserName" />
                <asp:BoundField DataField="OrderDate" HeaderText="Bestelldatum" SortExpression="OrderDate" />
                <asp:CheckBoxField DataField="OrderDelivery" HeaderText="Lieferung?" SortExpression="OrderDelivery" />
                <asp:BoundField DataField="OrderStatusDescription" HeaderText="Status" SortExpression="OrderStatusDescription" />
                <asp:BoundField DataField="OrderDeliveryDate" HeaderText="Lieferzeitpunkt" SortExpression="OrderDeliveryDate" />
                <asp:BoundField DataField="OrderSum" HeaderText="Gesamtsumme" SortExpression="OrderSum" DataFormatString="{0:C}" />
            </Columns>
        </asp:GridView>
        <asp:ObjectDataSource ID="getOrdersNotDelivered" runat="server" SelectMethod="getOrdersNotDelivered" TypeName="bll.clsOrderFacade"></asp:ObjectDataSource>
    </p>
    <p style="width:100%; text-align:center">
        <asp:Button ID="btInProgress" runat="server" Text="Bestellung in Arbeit" Enabled="false" OnClick="btInProgress_Click" Visible="False" Height="30px" Width="150px" BackColor="#CF323D" ForeColor="White" Font-Bold="true" />
        <asp:Button ID="btDelivered" runat="server" Text="Bestellung geliefert" Enabled="false" OnClick="btDelivered_Click" Visible="False" Height="30px" Width="150px" BackColor="#CF323D" ForeColor="White" Font-Bold="true" />
    </p>
   
</asp:Content>
