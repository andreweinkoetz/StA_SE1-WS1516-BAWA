<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="session2.aspx.cs" Inherits="web.Beispiele.session2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" type="text/css" href="..\StyleSheet.css" />
    <title>Rate It!</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
             <h1>Beispiel für Session-Handling</h1>
   
    </div>
        <p>
            <asp:Label ID="lblUser" runat="server" Text="..."></asp:Label>
        </p>
        <p>
            <asp:GridView ID="grdUser" runat="server" AutoGenerateColumns="False" CellPadding="4" DataSourceID="odsUsers" ForeColor="#333333" GridLines="None">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:BoundField DataField="ID" HeaderText="ID" SortExpression="ID" />
                    <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
                    <asp:BoundField DataField="Address" HeaderText="Address" SortExpression="Address" />
                    <asp:BoundField DataField="Distance" HeaderText="Distance" SortExpression="Distance" />
                    <asp:BoundField DataField="Role" HeaderText="Role" SortExpression="Role" />
                    <asp:CheckBoxField DataField="IsActive" HeaderText="IsActive" SortExpression="IsActive" />
                    <asp:BoundField DataField="Password" HeaderText="Password" SortExpression="Password" />
                </Columns>
                <EditRowStyle BackColor="#2461BF" />
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#EFF3FB" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#F5F7FB" />
                <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                <SortedDescendingCellStyle BackColor="#E9EBEF" />
                <SortedDescendingHeaderStyle BackColor="#4870BE" />
            </asp:GridView>
            <asp:ObjectDataSource ID="odsUsers" runat="server" SelectMethod="UserGetById" TypeName="bll.clsUserFacade">
                <SelectParameters>
                    <asp:SessionParameter Name="id" SessionField="UID" Type="Int32" DefaultValue="0" />
                </SelectParameters>
            </asp:ObjectDataSource>
        </p>
        <p>
            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Beispiele/session1.aspx">Weiter</asp:HyperLink>
        </p>
        <p>
            <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/Beispiele/default.aspx">Home</asp:HyperLink>
        </p>
    </form>
</body>
</html>
