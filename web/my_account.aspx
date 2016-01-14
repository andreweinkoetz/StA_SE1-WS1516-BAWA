<%@ Page Title="" Language="C#" MasterPageFile="~/default_layout.Master" AutoEventWireup="true" CodeBehind="my_account.aspx.cs" Inherits="web.MyAccount_Code" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentBox" runat="server">
    <table style="width: 100%">
        <tr>
            <td style="text-align: left">
                <asp:Label ID="lblMyOrders" runat="server" Text="Meine Bestellungen (Übersicht)" Font-Size="X-Large" Font-Bold="true"></asp:Label><br />
                <asp:Label ID="lblWelcome" runat="server" Text=""></asp:Label>
            </td>
            <td style="text-align: right">
                <asp:Button ID="btLogout" runat="server" Text="Logout" OnClick="btLogout_Click" Height="50px" Width="100px" BackColor="#CF323D" ForeColor="White" Font-Bold="true" />
            </td>
        </tr>
    </table>

    <hr />
    <p>
        <asp:GridView ID="gvMyOrders" runat="server" AutoGenerateColumns="False" DataSourceID="QOGetOrdersByUserID" Width="100%" BackColor="#CCCCCC" BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" CellPadding="4" CellSpacing="2" ForeColor="Black" OnSelectedIndexChanged="gvMyOrders_SelectedIndexChanged">
            <Columns>
                <asp:CommandField SelectImageUrl="~/img/detail_icon.png" ShowSelectButton="True" ButtonType="Image"></asp:CommandField>
                <asp:BoundField DataField="OrderNumber" HeaderText="Bestellnummer" SortExpression="OrderNumber">
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="OrderDate" HeaderText="Datum der Bestellung" SortExpression="OrderDate">
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:CheckBoxField DataField="OrderDelivery" HeaderText="Zum Liefern?" SortExpression="OrderDelivery">
                    <ItemStyle HorizontalAlign="Center" />
                </asp:CheckBoxField>
                <asp:BoundField DataField="OrderStatusDescription" HeaderText="Status der Bestellung" SortExpression="OrderStatusDescription">
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="OrderDeliveryDate" HeaderText="Datum der Lieferung" SortExpression="OrderDeliveryDate">
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="OrderSum" HeaderText="Gesamtsumme" SortExpression="OrderSum" DataFormatString="{0:C}">
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="CouponId" HeaderText="Gutschein#">
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
            </Columns>
            <FooterStyle BackColor="#CCCCCC" />
            <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Left" />
            <RowStyle BackColor="White" />
            <SelectedRowStyle Font-Bold="True" ForeColor="Black" BorderColor="#CF323D" />
            <SortedAscendingCellStyle BackColor="#F1F1F1" />
            <SortedAscendingHeaderStyle BackColor="#808080" />
            <SortedDescendingCellStyle BackColor="#CAC9C9" />
            <SortedDescendingHeaderStyle BackColor="#383838" />
        </asp:GridView>
        <asp:ObjectDataSource ID="QOGetOrdersByUserID" runat="server" SelectMethod="getOrdersByUserID" TypeName="bll.clsOrderFacade">
            <SelectParameters>
                <asp:SessionParameter Name="_userID" SessionField="userID" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
    </p>
    <br />
    <table>
        <tr>
            <td colspan="2" style="text-align: right">
                <b>Der Gesamtwert Ihrer Bestellungen nach Status:</b><br />
                <asp:Label ID="lblOrderSum" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td>Denken Sie daran regelmäßig Ihr Kennwort zu ändern.<br />
                Bitte verwenden Sie möglichst sichere Kennwörter (8 Zeichen, Sonderzeichen, Zahlen usw.)<br />
                Sollten sich Ihre Adressdaten geändert haben, so bitten wir Sie uns unter <a href="mailto:support@pizzapizza.de">support@pizzapizza.de</a> zu kontaktieren.<br />
                <asp:Label ID="lblErrorPasswd" runat="server" Text="" ForeColor="Red"></asp:Label>
            </td>
            <td>
                <asp:Table ID="tblPwChange" runat="server" HorizontalAlign="Center">
                    <asp:TableRow>
                        <asp:TableCell ColumnSpan="2">
                            <asp:Label ID="lblChangePasswd" runat="server" Text="Passwort ändern" Font-Bold="true"></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:TextBox runat="server" placeholder="Neues Passwort" ID="txtBoxPassword" Width="300" TextMode="Password" BorderStyle="Ridge" borderwith="2"></asp:TextBox><br />
                            <asp:TextBox runat="server" placeholder="Passwort bestätigen" ID="txtBoxPasswordx2" Width="300" TextMode="Password" BorderStyle="Ridge" borderwith="2"></asp:TextBox>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:Button ID="btChangePasswd" runat="server" Text="Ändern" Height="50px" Width="100px" BackColor="#CF323D" ForeColor="White" Font-Bold="true" OnClick="btChangePasswd_Click" />
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </td>
        </tr>
    </table>
    <br />
</asp:Content>
