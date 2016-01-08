<%@ Page Title="" Language="C#" MasterPageFile="~/default_layout.Master" AutoEventWireup="true" CodeBehind="edit_size.aspx.cs" Inherits="web.EditSize_Code" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentBox" runat="server">
    <table style="width: 100%">
        <tr>
            <td style="text-align: left">
                <asp:Label ID="lblSizeEdit" runat="server" Text="" Font-Size="Larger"></asp:Label>
            </td>
            <td style="text-align: right">
                <asp:Button ID="btBack" runat="server" Text="Zurück" OnClick="btBack_Click" Height="50px" Width="100px" BackColor="#CF323D" ForeColor="White" Font-Bold="true" />
            </td>
        </tr>
    </table>
    <hr />
    <asp:Panel runat="server" DefaultButton="btEnter">
        <asp:Table ID="tblSizeEdit" runat="server" Width="500px">
            <asp:TableHeaderRow>
                <asp:TableHeaderCell>SID</asp:TableHeaderCell>
                <asp:TableHeaderCell>Name des Größe</asp:TableHeaderCell>
                <asp:TableHeaderCell>Wert der Größe</asp:TableHeaderCell>
                <asp:TableHeaderCell>Kategorie</asp:TableHeaderCell>
            </asp:TableHeaderRow>
            <asp:TableRow>
                <asp:TableCell ID="sid">
                    <asp:TextBox ID="txtSid" Enabled="false" runat="server"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell ID="sname">
                    <asp:TextBox ID="txtSname" runat="server"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell ID="svalue">
                    <asp:TextBox ID="txtSvalue" runat="server"></asp:TextBox>
                </asp:TableCell>
                <asp:TableCell ID="scategory">
                    <asp:DropDownList ID="ddlCategory" runat="server" OnDataBound="ddlCategory_DataBound">
                        <asp:ListItem Value="1" Text="Pizza"></asp:ListItem>
                        <asp:ListItem Value="2" Text="Getränk"></asp:ListItem>
                    </asp:DropDownList>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell ColumnSpan="4">
                    <asp:Label ID="lblError" runat="server" Text="" ForeColor="Red"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableFooterRow>
                <asp:TableCell ColumnSpan="2">
                    <asp:Button ID="btEnter" runat="server" Text="" OnClick="btEnter_Click" Height="50px" Width="200px" BackColor="#CF323D" ForeColor="White" Font-Bold="true" Visible="true" />
                </asp:TableCell>
                <asp:TableCell ColumnSpan="2" HorizontalAlign="Right">
                    <asp:Button ID="btDelete" runat="server" Text="Größe löschen" OnClick="btDelete_Click" Height="50px" Width="200px" BackColor="#CF323D" ForeColor="White" Font-Bold="true" Enabled="true" />
                </asp:TableCell>
            </asp:TableFooterRow>
        </asp:Table>
    </asp:Panel>
    <hr />

</asp:Content>
