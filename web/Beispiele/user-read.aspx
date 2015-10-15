<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="user-read.aspx.cs" Inherits="web.beispiele.user_read" %>

<!DOCTYPE html>

<html lang="de-de">
<head runat="server">
    <link rel="stylesheet" type="text/css" href="..\StyleSheet.css" />
    <meta charset = "ISO-8859-1" />
    <title>RateIt - User Read</title>
</head>
<body>
    <form id="form1" runat="server">
   
        <h1>Pizza Pizza! Admin: Lesen User Tabelle</h1>
        <p></p>

    <asp:GridView ID="grdUsers" runat="server" CellPadding="4" ForeColor="#333333" 
        GridLines="None" style="font-family: Arial, Helvetica, Sans-Serif" 
        DataSourceID="odsUsers" AutoGenerateColumns="False">
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:BoundField DataField="ID" HeaderText="ID" SortExpression="ID" />
            <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
            <asp:BoundField DataField="Address" HeaderText="Address" 
                SortExpression="Address" />
            <asp:BoundField DataField="Distance" HeaderText="Distance" SortExpression="Distance" />
            <asp:BoundField DataField="Role" HeaderText="Role" SortExpression="Role" />
            <asp:CheckBoxField DataField="IsActive" HeaderText="IsActive" SortExpression="IsActive" />
            <asp:BoundField DataField="Password" HeaderText="Password" SortExpression="Password" />
        </Columns>
        <EditRowStyle BackColor="#2461BF" />
        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#4b6c9e" Font-Bold="False" ForeColor="White" />
        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#EFF3FB" />
        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        <SortedAscendingCellStyle BackColor="#F5F7FB" />
        <SortedAscendingHeaderStyle BackColor="#6D95E1" />
        <SortedDescendingCellStyle BackColor="#E9EBEF" />
        <SortedDescendingHeaderStyle BackColor="#4870BE" />
    </asp:GridView>
    <div>
    
        <asp:ObjectDataSource ID="odsUsers" runat="server" SelectMethod="UsersGetAll" 
            TypeName="bll.clsUserFacade"></asp:ObjectDataSource>
    
        <br />
        <asp:HyperLink ID="HyperLink1" runat="server"
            NavigateUrl="~/Default.aspx">Home</asp:HyperLink>
    
    </div>
    </form>
        <div>
        <hr />
        <br />
        <i>Hinweise zur Implementierung:<br />
&nbsp;mittels GridView <b>grdUser</b>, ObjectDataSource <b>odsUser</b>, BLL.<b>clsUserFacade</b>, Query <b>QUGetAllUsers</b>, 
        Tabelle <b>TUser<br />
        </b></i>
        <br />
        <i>grdUser.DataSourceID = odsUser<br />
        odsUser.TypeName = bll.clsUserFacade<br />
        odsUser.SelectMethod = UsersGetAll</i></div>
 
</body>
</html>
