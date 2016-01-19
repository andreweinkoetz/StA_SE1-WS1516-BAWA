<%@ Page Title="" Language="C#" MasterPageFile="~/default_layout.Master" AutoEventWireup="true" CodeBehind="edit_product.aspx.cs" Inherits="web.EditProduct_Code" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentBox" runat="server">
     <table style="width: 100%">
        <tr>
            <td style="text-align: left">
                <asp:Label ID="lblProductEdit" runat="server" Text="" Font-Size="Larger"></asp:Label>
            </td>
            <td style="text-align: right">
                <asp:Button ID="btBack" runat="server" Text="Zurück" OnClick="btBack_Click" Height="50px" Width="100px" BackColor="#CF323D" ForeColor="White" Font-Bold="true" />
            </td>
        </tr>
    </table>
    <hr />

    <asp:Table ID="tblProductEdit" runat="server" Width="600px">
        <asp:TableHeaderRow>
            <asp:TableHeaderCell>PID</asp:TableHeaderCell>
            <asp:TableHeaderCell>Name des Produkts</asp:TableHeaderCell>
            <asp:TableHeaderCell>Produktkategorie</asp:TableHeaderCell>
            <asp:TableHeaderCell>Preis pro Einheit</asp:TableHeaderCell>
            <asp:TableHeaderCell>Aktiv</asp:TableHeaderCell>
        </asp:TableHeaderRow>
        <asp:TableRow>
            <asp:TableCell ID="pid">
                <asp:TextBox ID="txtPid" Enabled="false" runat="server"></asp:TextBox></asp:TableCell>
            <asp:TableCell ID="pname">
                <asp:TextBox ID="txtPname" runat="server"></asp:TextBox></asp:TableCell>
            <asp:TableCell ID="category">
                <asp:DropDownList ID="ddlCategory" Width="100%" runat="server" DataSourceID="Categories" DataValueField="Key" DataTextField="Value" OnDataBound="ddlCategory_DataBound"></asp:DropDownList>
            </asp:TableCell>
            <asp:TableCell ID="pricePerUnit">
                <asp:TextBox ID="txtPpU" runat="server"></asp:TextBox></asp:TableCell>
            <asp:TableCell ID="toSell">
                <asp:CheckBox ID="chkSell" runat="server" /></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell ColumnSpan="5">
                <asp:Label ID="lblError" runat="server" Text="" ForeColor="Red"></asp:Label>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableFooterRow>
            <asp:TableCell ColumnSpan="2">
                <asp:Button ID="btEnter" runat="server" Text="" OnClick="btEnter_Click" Height="30px" Width="200px" BackColor="#CF323D" ForeColor="White" Font-Bold="true" Visible="true" />
            </asp:TableCell>
            <asp:TableCell ColumnSpan="3" HorizontalAlign="Right">
                <asp:Button ID="btDelete" runat="server" Text="Produkt löschen" OnClick="btDelete_Click" Height="30px" Width="200px" BackColor="#CF323D" ForeColor="White" Font-Bold="true" Enabled="false" />
            </asp:TableCell>
        </asp:TableFooterRow>
    </asp:Table>
    
    <asp:ObjectDataSource ID="Categories" runat="server" SelectMethod="GetAllProductCategories" TypeName="bll.clsProductFacade"></asp:ObjectDataSource>

    <hr />

    <p>
        <asp:Label ID="lblOpenOrders" runat="server" Text="" Font-Size="X-Small"></asp:Label>
    </p>

</asp:Content>
