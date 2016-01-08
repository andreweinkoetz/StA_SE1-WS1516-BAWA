<%@ Page Title="" Language="C#" MasterPageFile="~/default_layout.Master" AutoEventWireup="true" CodeBehind="orders.aspx.cs" Inherits="web.Orders" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentBox" runat="server">
    <p style="font-size: 36px">
        <asp:Label ID="lblOrder" runat="server" Text="Ihre Bestellung:"></asp:Label>
    </p>
    <% if (Session["selProducts"] != null && ((List<bll.clsProductExtended>)Session["selProducts"]).Count > 0)
        {%>
    <asp:GridView ID="gvOrder" runat="server" Width="100%" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical">
        <AlternatingRowStyle BackColor="#CCCCCC" />
        <FooterStyle BackColor="#CCCCCC" />
        <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
        <SortedAscendingCellStyle BackColor="#F1F1F1" />
        <SortedAscendingHeaderStyle BackColor="#808080" />
        <SortedDescendingCellStyle BackColor="#CAC9C9" />
        <SortedDescendingHeaderStyle BackColor="#383838" />
    </asp:GridView>
    <p style="text-align: right">
        <asp:Label ID="lblSum" runat="server" Text=""></asp:Label><br />
        <asp:Label ID="lblCouponValid" runat="server" Text=""></asp:Label><br />
        <asp:Label ID="lblNewSum" Font-Bold="true" Font-Underline="true" runat="server" Text=""></asp:Label>
    </p>
    <br />
    <p style="text-align: right">
        <asp:Label ID="lblCouponCode" runat="server" Text="Gutschein:"></asp:Label>
        <asp:TextBox ID="txtCouponCode" Width="150px" runat="server"></asp:TextBox>
        <asp:Button ID="btCoupon" OnClick="btCoupon_Click" runat="server" Text="Einlösen" />
        <br />
        <asp:Label ID="lblErrorCoupon" ForeColor="Red" runat="server" Text=""></asp:Label>
    </p>

    <table style="width: 100%;">
        <tr>
            <td style="text-align: left">
                <asp:Button ID="clearCart" runat="server" Text="Warenkorb leeren" OnClick="clearCart_Click" /></td>
            <td style="text-align: right">
                <asp:CheckBox ID="chkDelivery" runat="server" Text="Liefern?" Checked="true" TextAlign="Right" OnCheckedChanged="chkDelivery_CheckedChanged" AutoPostBack="true" /><asp:Button ID="btOrder" runat="server" Text="Bestellung aufgeben" OnClick="btOrder_Click" /></td>
        </tr>
    </table>

    <asp:Label ID="lblStatus" ForeColor="Red" runat="server" Text=""></asp:Label>
    <%}
        else
        { %>

    <asp:Label ID="lblEmptyCart" runat="server" Text="Warenkorb ist leer."></asp:Label>
    <%} %>
</asp:Content>
