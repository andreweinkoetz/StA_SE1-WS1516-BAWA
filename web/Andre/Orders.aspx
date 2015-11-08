<%@ Page Title="" Language="C#" MasterPageFile="~/Andre/default_layout.Master" AutoEventWireup="true" CodeBehind="Orders.aspx.cs" Inherits="web.Andre.Orders" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentBox" runat="server">
    <p style="font-size:36px">Ihre Bestellung:</p>
    <% if (Session["selProducts"] != null) {%>
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
    <asp:Button ID="clearCart" runat="server" Text="Warenkorb leeren" OnClick="clearCart_Click" />
    <%} else { %>
    
    <asp:Label ID="Label2" runat="server" Text="Nix im Warenkorb!"></asp:Label>
    <%} %>
</asp:Content>
