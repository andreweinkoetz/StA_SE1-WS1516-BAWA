<%@ Page Title="" Language="C#" MasterPageFile="~/default_layout.Master" AutoEventWireup="true" CodeBehind="MyAccount.aspx.cs" Inherits="web.MyAccount_Code" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentBox" runat="server">
    <table style="width:100%">
        <tr>
            <td style="text-align:left">
                <asp:Label ID="lblMyOrders" runat="server" Text="Meine Bestellungen (Übersicht)" Font-Size="X-Large" Font-Bold="true"></asp:Label>
            </td>
            <td style="text-align:right">
                <asp:Button ID="btLogout" runat="server" Text="Logout" OnClick="btLogout_Click" Height="50px" Width="100px" BackColor="#CF323D" ForeColor="White" Font-Bold="true" />
            </td>
        </tr>
    </table>
    
    <hr />
    <p>
        <asp:GridView ID="gvMyOrders" runat="server" AutoGenerateColumns="False" DataSourceID="QOGetOrdersByUserID" Width="100%" BackColor="#CCCCCC" BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" CellPadding="4" CellSpacing="2" ForeColor="Black" OnSelectedIndexChanged="gvMyOrders_SelectedIndexChanged">
            <Columns>
                <asp:CommandField SelectText="Detailansicht" ShowSelectButton="True" ButtonType="Button" >
                <ControlStyle Height="30px" Width="100px" />
                </asp:CommandField>
                <asp:BoundField DataField="OrderNumber" HeaderText="Bestellnummer" SortExpression="OrderNumber" />
                <asp:BoundField DataField="OrderDate" HeaderText="Datum der Bestellung" SortExpression="OrderDate" />
                <asp:CheckBoxField DataField="OrderDelivery" HeaderText="Zum Liefern?" SortExpression="OrderDelivery" />
                <asp:BoundField DataField="OrderStatusDescription" HeaderText="Status der Bestellung" SortExpression="OrderStatusDescription" />
                <asp:BoundField DataField="OrderDeliveryDate" HeaderText="Datum der Lieferung" SortExpression="OrderDeliveryDate" />
                <asp:BoundField DataField="OrderSum" HeaderText="Gesamtsumme" SortExpression="OrderSum" DataFormatString="{0:C}" />
                <asp:BoundField DataField="CouponId" HeaderText="Gutschein#" />
            </Columns>
            <FooterStyle BackColor="#CCCCCC" />
            <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Left" />
            <RowStyle BackColor="White" />
            <SelectedRowStyle Font-Bold="True" ForeColor="Black" BorderColor="#CF323D" />
            <SortedAscendingCellStyle BackColor="#F1F1F1" />
            <SortedAscendingHeaderStyle BackColor="#808080" />
            <SortedDescendingCellStyle BackColor="#CAC9C9" />
            <SortedDescendingHeaderStyle BackColor="#383838" />
        </asp:GridView>
        <asp:ObjectDataSource ID="QOGetOrdersByUserID" runat="server" SelectMethod="getOrdersByUserID" TypeName="bll.clsOrderFacade">
            <SelectParameters>
                <asp:SessionParameter Name="_userID" SessionField="userID" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>

  

    </p>
</asp:Content>
