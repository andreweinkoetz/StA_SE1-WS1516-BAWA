﻿<%@ Page Title="" Language="C#" MasterPageFile="~/default_layout.Master" AutoEventWireup="true" CodeBehind="OrderDetail.aspx.cs" Inherits="web.OrderDetail_Code" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentBox" runat="server">

    <table style="width: 100%">
        <tr>
            <td style="text-align: left">
                <asp:Label ID="lblOrderNumber" runat="server" Font-Size="X-Large" Font-Bold="true" ForeColor="Black" Text=""></asp:Label>
            </td>
            <td style="text-align: right">
                <asp:Button ID="btCancelOrder" runat="server" Text="Bestellung stornieren" OnClick="btCancelOrder_Click" Height="40px" Width="200px" BackColor="#CF323D" ForeColor="White" Font-Bold="true" />
            </td>
        </tr>
    </table>
    <hr />
    <p>
        <asp:GridView ID="gvOrderDetail" runat="server" Width="100%">
        </asp:GridView>
    </p>
    <table style="width: 100%">
        <tr>
            <td style="text-align: left">
                <asp:Button ID="btOrderOverview" runat="server" Text="Zurück zur Übersicht" OnClick="btOrderOverview_Click" Height="40px" Width="200px" BackColor="#CF323D" ForeColor="White" Font-Bold="true" />
            </td>

            <td style="text-align: right">
                <asp:Label ID="lblTotalSum" runat="server" Text=""></asp:Label>
            </td>
        </tr>
    </table>
</asp:Content>
