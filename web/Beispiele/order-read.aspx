<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="order-read.aspx.cs" Inherits="web.Beispiele.order_read" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
        <link rel="stylesheet" type="text/css" href="..\StyleSheet.css" />
    <title>Pizza - Order Read</title>
</head>
<body>
    <form id="form1" runat="server">
   
        <h1>Lecker Pizza Admin: Lesen Order Tabelle</h1>
        <p></p>

    <div>
    
        <asp:GridView ID="grdOrderExtended" runat="server" AutoGenerateColumns="False" CellPadding="4" DataSourceID="odsOrdersExtended" ForeColor="#333333" GridLines="None">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:BoundField DataField="ID" HeaderText="ID" SortExpression="ID" />
                <asp:BoundField DataField="ProductName" HeaderText="ProductName" SortExpression="ProductName" />
                <asp:BoundField DataField="UserName" HeaderText="UserName" SortExpression="UserName" />
                <asp:BoundField DataField="OrderDate" HeaderText="OrderDate" SortExpression="OrderDate" />
                <asp:BoundField DataField="OrderSize" HeaderText="OrderSize" SortExpression="OrderSize" />
                <asp:BoundField DataField="OrderExtras" HeaderText="OrderExtras" SortExpression="OrderExtras" />
                <asp:BoundField DataField="OrderCount" HeaderText="OrderCount" SortExpression="OrderCount" />
                <asp:BoundField DataField="OrderSum" HeaderText="OrderSum" SortExpression="OrderSum" />
                <asp:CheckBoxField DataField="OrderDelivery" HeaderText="OrderDelivery" SortExpression="OrderDelivery" />
                <asp:BoundField DataField="OrderStatus" HeaderText="OrderStatus" SortExpression="OrderStatus" />
            </Columns>
            <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
            <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
            <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
            <SortedAscendingCellStyle BackColor="#FDF5AC" />
            <SortedAscendingHeaderStyle BackColor="#4D0000" />
            <SortedDescendingCellStyle BackColor="#FCF6C0" />
            <SortedDescendingHeaderStyle BackColor="#820000" />
        </asp:GridView>
        <asp:ObjectDataSource ID="odsOrdersExtended" runat="server" SelectMethod="OrdersGetAll" TypeName="bll.clsOrderFacade"></asp:ObjectDataSource>
        <br />
    
        <br />
        <asp:HyperLink ID="HyperLink1" runat="server"
            NavigateUrl="~/Default.aspx">Home</asp:HyperLink>
    
    </div>
    </form>
        <div>
        <hr />
        <br />
        <i>Implementierung:<br />
&nbsp;mittels GridView <b>grdOrder</b>, ObjectDataSource <b>odsOrder</b>, BLL.<b>clsOrderFacade</b>, Query <b>QGetAllOrders</b>, 
        Tabelle <b>TOrder<br />
        </b></i>
        <br />
        <i>grdOrder.DataSourceID = odsOrder<br />
        odsOrder.TypeName = bll.clsOrderFacade<br />
        odsOrder.SelectMethod = getAllOrders<br />
            <br />
            </i></div>
 
</body>
</html>
