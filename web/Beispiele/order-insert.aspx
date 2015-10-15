<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="order-insert.aspx.cs" Inherits="web.Beispiele.order_insert" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link rel="stylesheet" type="text/css" href="..\StyleSheet.css" />
    <title>Pizza Pizza - Insert Order</title>

</head>
<body>
    <form id="form1" runat="server">
        <div>

            <div>

                <h1>Einfügen neue Order
                </h1>
                    <em>nur teilweise implementiert!</em><br />
                <br />
            </div>
            <table>
                <tr>
                    <td>Kunde:
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlUsers" runat="server" DataSourceID="odsUsers" DataTextField="Name" DataValueField="ID">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>Produkt-Id:
                    </td>
                    <td>
                        <asp:TextBox ID="txtProduct" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>Größe:</td>
                    <td>
                        <asp:TextBox ID="txtSize" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>Anzahl:</td>
                    <td>
                        <asp:TextBox ID="txtCount" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>Lieferung:</td>
                    <td>
                        <asp:CheckBox ID="chkDelivery" runat="server" />
                    </td>
                </tr>

            </table>

            <asp:ObjectDataSource ID="odsUsers" runat="server" SelectMethod="getAllUsers" TypeName="bll.clsUserCollection"></asp:ObjectDataSource>
            <p>
                <asp:Button ID="btnInsert" runat="server" OnClick="btnInsert_Click" Text="Einfügen" />
            </p>
            <p>
                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="Default.aspx">Home</asp:HyperLink>
            </p>
            <p>
                <asp:Label ID="lblMsg" runat="server" Style="font-family: Arial, Helvetica, sans-serif; color: #993300"></asp:Label>
            </p>
            <div class="style4">
                <hr />
                <br />
                Implementierung mittels Code behind: <b>btnInsert_Click()</b><br />
            </div>
        </div>
    </form>
</body>
</html>
