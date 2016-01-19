<%@ Page Title="" Language="C#" MasterPageFile="~/default_layout.Master" AutoEventWireup="true" CodeBehind="edit_extra.aspx.cs" Inherits="web.EditExtra_Code" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentBox" runat="server">
    <table style="width: 100%">
        <tr>
            <td style="text-align: left">
                <asp:Label ID="lblExtraEdit" runat="server" Text="" Font-Size="Larger"></asp:Label>
            </td>
            <td style="text-align: right">
                <asp:Button ID="btBack" runat="server" Text="Zurück" OnClick="btBack_Click" Height="50px" Width="100px" BackColor="#CF323D" ForeColor="White" Font-Bold="true" />
            </td>
        </tr>
    </table>
    <hr />
    <asp:Panel runat="server" DefaultButton="btEnter">
        <asp:Table ID="tblExtraEdit" runat="server" Width="600px">
            <asp:TableHeaderRow>
                <asp:TableHeaderCell>EID</asp:TableHeaderCell>
                <asp:TableHeaderCell>Name des Extras</asp:TableHeaderCell>
                <asp:TableHeaderCell>Preis pro Extra</asp:TableHeaderCell>
                <asp:TableHeaderCell>Aktiv</asp:TableHeaderCell>
            </asp:TableHeaderRow>
            <asp:TableRow>
                <asp:TableCell ID="eid">
                    <asp:TextBox ID="txtEid" Enabled="false" runat="server"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell ID="ename">
                    <asp:TextBox ID="txtEname" runat="server"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell ID="pricePerExtra">
                    <asp:TextBox ID="txtPpE" runat="server"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell ID="sell">
                    <asp:CheckBox ID="chkSell" runat="server" />
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell ColumnSpan="4">
                    <asp:Label ID="lblError" runat="server" Text="" ForeColor="Red"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableFooterRow>
                <asp:TableCell>
                    <asp:Button ID="btEnter" runat="server" Text="" OnClick="btEnter_Click" Height="30px" Width="200px" BackColor="#CF323D" ForeColor="White" Font-Bold="true" Visible="true" />
                </asp:TableCell>
                <asp:TableCell ColumnSpan="2" HorizontalAlign="Right">
                    <asp:Button ID="btDelete" runat="server" Text="Extra löschen" OnClick="btDelete_Click" Height="30px" Width="200px" BackColor="#CF323D" ForeColor="White" Font-Bold="true" Enabled="false" />
                </asp:TableCell>
            </asp:TableFooterRow>
        </asp:Table>
    </asp:Panel>
    <hr />
    <p>
        <asp:Label ID="lblOpenOrders" runat="server" Text="" Font-Size="X-Small"></asp:Label>
    </p>
</asp:Content>
